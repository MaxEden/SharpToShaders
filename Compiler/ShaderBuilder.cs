using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static Compiler.ShaderBuilder;
using static System.Net.Mime.MediaTypeNames;
using FlowControl = Mono.Cecil.Cil.FlowControl;
using OpCode = Mono.Cecil.Cil.OpCode;

namespace Compiler
{
    internal class ShaderBuilder
    {
        static Dictionary<string, string> _operators = new()
        {
            { "op_Multiply", "*" },
            { "op_Division", "/" },
            { "op_Subtraction", "-" },
        };

        static Dictionary<string, string> _typeMap = new()
        {
            {"Single", "float" },
            {"Double", "double" },
            {"Boolean", "bool" },
            {"Int32", "int" }
        };

        private StringBuilder _body;
        private readonly Stack<IfItem> _ifStack = new();
        private readonly Dictionary<int, NamedStack> _namedStack = new();
        private int _stk;
        private readonly List<NamedStack> _namedStkLocals = new();
        public readonly Stack<StackItem> Stack = new();
        private string[] _locNames;
        private StringBuilder unresolved;

        internal void Build(string path, TypeDefinition type)
        {
            var method = type.GetMethods().First(p => p.Name.StartsWith("Vert", StringComparison.InvariantCultureIgnoreCase));
            //Build(path, type, method);

            var method2 = type.GetMethods().First(p => p.Name.StartsWith("Frag", StringComparison.InvariantCultureIgnoreCase));
            Build(path, type, method2);
        }

        internal string Build(string path, TypeDefinition type, MethodDefinition method)
        {
            Stack.Clear();

            var locals = method.Body.Variables.OrderBy(p => p.Index).ToArray();

            var scope = method.DebugInformation.Scope;
            var debugLocals = scope.Variables.ToArray();

            _locNames = new string[locals.Length];
            for (int i = 0; i < locals.Length; i++)
            {
                _locNames[i] = "tmp" + i;
                var dbg = debugLocals.FirstOrDefault(p => p.Index == i);
                if (dbg != null)
                {
                    _locNames[i] = dbg.Name;
                }
            }

            _body = new StringBuilder();

            method.Body.SimplifyMacros();

            var instr = method.Body.Instructions.ToArray();

            var instB = new StringBuilder();
            foreach (var op in instr)
            {
                instB.Append(op.OpCode.Code);
                instB.Append(" ");
                if (op.Operand != null)
                {
                    instB.Append(op.Operand);
                    instB.Append(" ");
                    instB.Append(op.Operand.GetType().Name);
                }

                instB.AppendLine();
            }

            File.WriteAllText(path + type.Name + "." + method.Name + ".opcodes.txt", instB.ToString());

            unresolved = new StringBuilder();

            _ifStack.Clear();
            _namedStack.Clear();
            _namedStkLocals.Clear();

            {
                var op = instr[0];
                while (op != null)
                {
                    op = ProcessInstruction(op);
                }
            }

            var sb = new StringBuilder();
            sb.AppendLine($"{method.Name}(){{");

            for (int i = 0; i < locals.Length; i++)
            {
                sb.AppendLine(
                    $"{MapTypeName(locals[i].VariableType)} {_locNames[i]};"
                );
            }

            foreach (var stack in _namedStkLocals)
            {
                sb.AppendLine(
                    $"{stack.expectedType} {stack.name};"
                );
            }

            sb.AppendLine(_body.ToString());
            sb.AppendLine("}");
            sb.AppendLine("/*___unresolved_____");
            sb.AppendLine(unresolved.ToString());
            sb.AppendLine("_____________*/");


            File.WriteAllText(path + type.Name + "." + method.Name + ".shader.txt", sb.ToString());


            var output = _body.ToString();
            return output;
        }

        private Instruction ProcessInstruction(Instruction op)
        {
            if (_ifStack.TryPeek(out var ifPeek) && ifPeek.Instruction == op)
            {
                UnwindIf(true);
                AppendLine($"}}");
            }

            switch (op.OpCode.Code)
            {
                case Code.Nop:
                    //_body.AppendLine("//nop");
                    break;
                case Code.Br:
                    {
                        if (RecognizeLoop(op, out var next))
                        {
                            return next;
                        }

                        if (_ifStack.Count == 0)
                        {
                            //empty branch
                            return op.Next;
                        }

                        var instrDef = (Instruction)op.Operand;
                        UnwindIf(false);
                        _ifStack.Push(
                            new IfItem()
                            {
                                Code = Code.Br,
                                Instruction = instrDef,
                                stackCount = Stack.Count
                            });
                        AppendLine($"}} else {{");
                    }
                    break;
                case Code.Conv_R4:
                    //_body.Append("//conv to float");
                    break;
                case Code.Brfalse:
                    {
                        AppendLine($"if ( {Pop("bool").text} ) {{");
                        var instrDef = (Instruction)op.Operand;
                        _ifStack.Push(
                            new IfItem()
                            {
                                Code = Code.Brfalse,
                                Instruction = instrDef,
                                stackCount = Stack.Count
                            });
                    }
                    break;
                case Code.Brtrue:
                    {
                        AppendLine($"if (! {Pop("bool").text} ) {{");
                        var instrDef = (Instruction)op.Operand;
                        _ifStack.Push(
                            new IfItem()
                            {
                                Code = Code.Brtrue,
                                Instruction = instrDef,
                                stackCount = Stack.Count
                            });
                    }
                    break;
                case Code.Clt:
                    {
                        PopTwo(out var left, out var right);
                        Push(new StackItem()
                        {
                            expectedType = "bool",
                            text = $"{left} < {right}"
                        });
                    }
                    break;
                case Code.Cgt:
                    {
                        PopTwo(out var left, out var right);
                        Push(new StackItem()
                        {
                            expectedType = "bool",
                            text = $"{left} > {right}"
                        });
                    }
                    break;
                case Code.Ceq:
                    {
                        PopTwo(out var left, out var right);
                        Push(new StackItem()
                        {
                            expectedType = "bool",
                            text = $"{left} == {right}"
                        });
                    }
                    break;
                case Code.Ret:
                    {
                        AppendLine($"return {Pop("float4")};");
                    }
                    break;
                case Code.Ldarg:
                case Code.Ldarga:
                    {
                        var paramRef = (ParameterReference)op.Operand;
                        var typeName = MapTypeName(paramRef.ParameterType);

                        Push(new StackItem()
                        {
                            expectedType = typeName,
                            text = paramRef.Name,
                            def = paramRef
                        });
                    }
                    break;
                case Code.Ldloc:
                case Code.Ldloca:
                    {
                        var varRef = (VariableReference)op.Operand;
                        var typeName = MapTypeName(varRef.VariableType);
                        Push(new StackItem()
                        {
                            expectedType = typeName,
                            text = _locNames[varRef.Index],
                            def = varRef
                        });
                    }
                    break;
                case Code.Ldfld:
                case Code.Ldflda:
                    {
                        var fieldRef = (FieldReference)op.Operand;
                        var typeName = MapTypeName(fieldRef.DeclaringType);
                        Push(new StackItem()
                        {
                            expectedType = typeName,
                            text = Acc(Pop().text, fieldRef.Name),
                            def = fieldRef
                        });
                    }
                    break;
                case Code.Ldsfld:
                case Code.Ldsflda:
                    {
                        var fieldRef = (FieldReference)op.Operand;
                        var typeName = MapTypeName(fieldRef.DeclaringType);
                        Push(new StackItem()
                        {
                            expectedType = typeName,
                            text = fieldRef.Name,
                            def = fieldRef
                        });
                    }
                    break;
                case Code.Stloc:
                    {
                        var varRef = (VariableReference)op.Operand;
                        var typeName = MapTypeName(varRef.VariableType);
                        AppendLine($"{_locNames[varRef.Index]} = {Pop(typeName).text};");
                    }
                    break;
                case Code.Stfld:
                    {
                        var fieldRef = (FieldReference)op.Operand;
                        var typeName = MapTypeName(fieldRef.DeclaringType);
                        PopTwo(out var left, out var right, typeName);
                        AppendLine($"{Acc(left.text, fieldRef.Name)} = {right.text};");
                    }
                    break;
                case Code.Initobj:
                    {
                        var typeDef = (TypeReference)op.Operand;
                        var typeName = MapTypeName(typeDef);
                        AppendLine($"{Pop(typeName).text} = new {typeDef.Name}();");
                    }
                    break;
                case Code.Dup:
                    {
                        Push(Peek());
                    }
                    break;
                case Code.Ldind_R4: //loads address
                    {
                        //Same stack content as a value
                    }
                    break;
                case Code.Stind_R4:
                    {
                        PopTwo(out var left, out var right, "int");
                        AppendLine(left + " = " + right + ";");
                    }
                    break;
                case Code.Ldc_R4:
                    {
                        float value = (float)op.Operand;
                        Push(new StackItem()
                        {
                            expectedType = "float",
                            text = value.ToString("0.0", CultureInfo.InvariantCulture),
                            def = value
                        });
                    }
                    break;
                case Code.Ldc_I4:
                    {
                        int value = (int)op.Operand;
                        Push(new StackItem()
                        {
                            expectedType = "int",
                            text = value.ToString("0", CultureInfo.InvariantCulture),
                            def = value
                        });
                    }
                    break;
                case Code.Call:
                    {
                        var methodRef = (MethodReference)op.Operand;

                        if (_operators.TryGetValue(methodRef.Name, out var oper))
                        {
                            Operator(oper);
                            return op.Next;
                        }

                        if (methodRef.Name == "op_Implicit")
                        {
                            var peek = Peek();
                            peek.expectedType = MapTypeName(methodRef.ReturnType);
                            return op.Next;
                        }

                        var call = ")";
                        int count = methodRef.Parameters.ToArray().Length;

                        for (int j = 0; j < count; j++)
                        {
                            call = Pop().text + call;
                            if (j < count - 1)
                            {
                                call = ", " + call;
                            }
                        }

                        call = "(" + call;

                        if (MapMethod(methodRef, call, out var result))
                        {
                            call = result;
                        }
                        else
                        {
                            var name = methodRef.Name;
                            if (name.StartsWith("get_"))
                            {
                                name = name.Substring(4);
                                call = name;
                            }
                            else
                            {
                                call = name + call;
                            }

                            if (methodRef.HasThis)
                            {
                                call = Acc(Pop().text, call);
                            }
                            else
                            {
                                call = Acc(methodRef.DeclaringType.Name, call);
                            }
                        }

                        if (methodRef.ReturnType.Name == "Void")
                        {
                            call = call + ";";
                            AppendLine(call);
                        }
                        else
                        {
                            var typeName = MapTypeName(methodRef.ReturnType);
                            Push(new StackItem()
                            {
                                expectedType = typeName,
                                text = call,
                                def = methodRef
                            });
                        }
                    }
                    break;

                case Code.Newobj:
                    {
                        var methodRef = (MethodReference)op.Operand;

                        var call = ")";
                        int count = methodRef.Parameters.ToArray().Length;

                        for (int j = 0; j < count; j++)
                        {
                            call = Pop().text + call;
                            if (j < count - 1)
                            {
                                call = ", " + call;
                            }
                        }

                        call = "(" + call;

                        if (MapMethod(methodRef, call, out var result))
                        {
                            call = result;
                        }
                        else
                        {
                            call = methodRef.DeclaringType.Name + call;
                        }

                        Push(new StackItem()
                        {
                            text = call,
                            def = methodRef
                        });
                    }
                    break;
                case Code.Add:
                    Operator("+");
                    break;
                case Code.Sub:
                    Operator("-");
                    break;
                case Code.Div:
                    Operator("/");
                    break;
                case Code.Mul:
                    Operator("*");
                    break;
                case Code.Rem:
                    Operator("%");
                    break;
                default:
                    unresolved.Append(op.OpCode.Code);
                    unresolved.Append(" ");
                    if (op.Operand != null)
                    {
                        unresolved.Append(op.Operand);
                        unresolved.Append(" ");
                        unresolved.Append(op.Operand.GetType().Name);
                    }

                    unresolved.AppendLine();
                    break;
            }

            return op.Next;
        }

        private bool RecognizeLoop(Instruction op, out Instruction next)
        {
            if (op.OpCode.Code == Code.Br)
            {
                var head = (Instruction)op.Operand;
                var inst = head;
                while (inst.OpCode.FlowControl != FlowControl.Cond_Branch)
                {
                    inst = inst.Next;
                    if (inst == null)
                    {
                        next = default;
                        return false;
                    }
                }

                var loop = (Instruction)inst.Operand;
                if (loop == op.Next)
                {
                    _body.AppendLine($"while(true){{");

                    var end = inst;
                    var proc = head;
                    while (proc != end)
                    {
                        ProcessInstruction(proc);
                        proc = proc.Next;
                    }

                    _body.AppendLine($"if(!{Pop("bool")}) break;");

                    proc = loop;

                    while (proc != head)
                    {
                        ProcessInstruction(proc);
                        proc = proc.Next;
                    }

                    _body.AppendLine("}");

                    next = end.Next;
                    return true;
                }
            }

            next = default;
            return false;
        }

        void UnwindIf(bool exit)
        {
            var ifItem = _ifStack.Peek();
            if (Stack.Count > ifItem.stackCount)
            {
                var list = new List<StackItem>();

                while (Stack.Count > ifItem.stackCount)
                {
                    var popped = Stack.Pop();
                    if (popped.name == null)
                    {
                        _stk++;
                        var named = new NamedStack()
                        {
                            id = _stk,
                            name = "stk" + _stk,
                            index = Stack.Count
                        };
                        _namedStack.Add(named.index, named);
                        popped.name = named.name;
                        AppendLine($"{popped.name} = {popped.text};");
                    }

                    if (exit)
                    {
                        list.Add(popped);
                        var named = _namedStack[Stack.Count];
                        _namedStkLocals.Add(named);
                        _namedStack.Remove(Stack.Count);
                    }
                }

                if (exit)
                {
                    foreach (var item in list)
                    {
                        Stack.Push(item);

                    }


                }
            }

            _ifStack.Pop();
        }

        public void Push(StackItem stackItem)
        {
            int index = Stack.Count;
            if (_namedStack.TryGetValue(index, out var namedStack))
            {
                AppendLine($"{namedStack.name} = {stackItem.text}");

                if (stackItem.expectedType != null)
                {
                    namedStack.expectedType = stackItem.expectedType;
                }

                Stack.Push(new StackItem()
                {
                    name = namedStack.name,
                    def = stackItem.def,
                    text = namedStack.name,
                    expectedType = stackItem.expectedType
                });
            }
            else
            {
                Stack.Push(stackItem);
            }
        }
        public StackItem Pop(string expectedType = null)
        {
            int index = Stack.Count - 1;
            if (_namedStack.TryGetValue(index, out var namedStack))
            {
                if (expectedType != null)
                {
                    namedStack.expectedType = expectedType;
                }
            }

            return Stack.Pop();
        }
        public StackItem Peek()
        {
            return Stack.Peek();
        }

        public void AppendLine(string line)
        {
            for (int i = 0; i < _ifStack.Count; i++)
            {
                _body.Append("\t");
            }

            _body.AppendLine(line);
        }
        public class IfItem
        {
            public Instruction Instruction;
            public int stackCount;
            public Code Code;
        }
        public class NamedStack
        {
            public int id;
            public string name;
            public int index;
            public string expectedType;
        }
        private string MapTypeName(TypeReference typeRef)
        {
            if (_typeMap.TryGetValue(typeRef.Name, out var name)) return name;
            return typeRef.Name;
        }
        private bool MapMethod(MethodReference methodRef, string call, out string result)
        {
            if (methodRef.DeclaringType.Name == "MathF")
            {
                result = methodRef.Name.ToLowerInvariant() + call;
                return true;
            }

            if (methodRef.DeclaringType.Name == "math")
            {
                result = methodRef.Name.ToLowerInvariant() + call;
                return true;
            }

            if (methodRef.DeclaringType.Name == "Unity")
            {
                result = methodRef.Name + call;
                return true;
            }

            if (methodRef.Name == "op_Implicit")
            {
                result = call;
                return true;
            }

            result = default;
            return false;
        }
        public string Acc(string from, string field)
        {
            if (string.IsNullOrWhiteSpace(from)) return field;
            return from + "." + field;
        }
        public void PopTwo(out StackItem left, out StackItem right, string expectedLeftType = null)
        {
            right = Pop();
            left = Pop(expectedLeftType);
        }
        public void Operator(string oper)
        {
            PopTwo(out var left, out var right);
            Push(new StackItem()
            {
                expectedType = left.expectedType,
                text = left.text + " " + oper + " " + right.text
            }
            );
        }

    }
}

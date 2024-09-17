using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using System.Globalization;
using System.Text;
using FlowControl = Mono.Cecil.Cil.FlowControl;
using OpCodes = Mono.Cecil.Cil.OpCodes;

namespace Compiler
{
    internal class ShaderBuilder
    {
        static Dictionary<string, string> _operators = new()
        {
            {"op_Addition","+"},
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

        private Dictionary<Code, string> _brComp = new()
        {
            { Code.Blt, "<" },
            { Code.Bgt, ">" },
            { Code.Bge, ">=" },
            { Code.Ble, "<=" },
            { Code.Beq, "==" },
        };

        private StringBuilder _body;
        private int _stk;
        private readonly List<NamedStack> _namedStkLocals = new();
        public readonly Stack<StackItem> Stack = new();
        public readonly Stack<IfScope> _ifScopeStack = new();
        private LocalVar[] _locals;
        private StringBuilder unresolved;

        private int _indent;
        private MethodDefinition _method;
        private HashSet<Instruction> _returnBranches;

        internal void Build(string path, TypeDefinition type)
        {
            var method2 = type.GetMethods().First(p => p.Name.StartsWith("Frag", StringComparison.InvariantCultureIgnoreCase));
            Build(path, type, method2);

            var method1 = type.GetMethods().First(p => p.Name.StartsWith("Vert", StringComparison.InvariantCultureIgnoreCase));
            Build(path, type, method1);
        }

        class LocalVar
        {
            public VariableDefinition definition;
            public string name;
            public int set;
            public int load;
            public bool canBeRef;
            public bool canBeOmitted;
            public StackItem RefValue { get; set; }

            public override string ToString()
            {
                return name;
            }
        }

        internal string Build(string path, TypeDefinition type, MethodDefinition method)
        {
            Stack.Clear();
            _indent = 0;
            _body = new StringBuilder();
            method.Body.SimplifyMacros();

            _method = method;

            LocalsSetup(method);

            var instB = new StringBuilder();
            var instr = method.Body.Instructions.ToArray();
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

            for (int i = 0; i < _locals.Length; i++)
            {
                if (_locals[i].canBeOmitted) continue;
                if (_locals[i].canBeRef) continue;
                sb.AppendLine(
                    $"{MapTypeName(_locals[i].definition.VariableType)} {_locals[i]};"
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

        private void LocalsSetup(MethodDefinition method)
        {
            var locals = method.Body.Variables.OrderBy(p => p.Index).ToArray();

            var scope = method.DebugInformation.Scope;
            var debugLocals = scope.Variables.ToArray();

            _locals = new LocalVar[locals.Length];
            for (int i = 0; i < locals.Length; i++)
            {
                _locals[i] = new LocalVar();
                _locals[i].definition = locals[i];
            }

            var instr = method.Body.Instructions.ToArray();
            foreach (var op in instr)
            {
                if (op.Operand is VariableDefinition variableDefinition)
                {
                    var local = _locals[variableDefinition.Index];

                    if (op.OpCode.Code == Code.Stloc)
                    {
                        local.set++;
                    }

                    if (op.OpCode.Code == Code.Ldloc)
                    {
                        local.load++;
                    }
                }
            }

            for (var i = 0; i < _locals.Length; i++)
            {
                var localVar = _locals[i];
                var dbg = debugLocals.FirstOrDefault(p => p.Index == i);
                if (dbg != null)
                {
                    _locals[i].name = dbg.Name;
                }
                else
                {
                    _locals[i].name = "tmp" + i;
                    if (localVar.set == 1 && localVar.load == 1)
                    {
                        localVar.canBeRef = true;
                    }

                    if (localVar.load == 0)
                    {
                        localVar.canBeOmitted = true;
                    }
                }
            }


            //==============

            var rets = instr.Where(p => p.OpCode.Code == Code.Ret);
            _returnBranches = instr
                .Where(p => p.OpCode.FlowControl == FlowControl.Branch ||
                            p.OpCode.FlowControl == FlowControl.Cond_Branch)
                .Where(p => rets.Any(x => x.Previous == p.Operand))
                .ToHashSet();


            foreach (var op in _returnBranches)
            {
                var jump = (Instruction)op.Operand;
                if (jump.Operand is VariableReference variable)
                {
                    var loc = _locals.First(p => p.definition == variable);
                    loc.name = "result" + variable.Index;
                }
            }
        }

        private Instruction ProcessInstruction(Instruction op)
        {
            switch (op.OpCode.Code)
            {
                case Code.Nop:
                    //ignore
                    break;
                case Code.Pop:
                    _body.AppendLine("//" + Pop() + " is omitted");
                    break;
                case Code.Conv_R4:
                    //_body.Append("//conv to float");
                    break;
                case Code.Br:
                    {
                        if (RecognizeReturnBranch(op, out var next1)) return next1;
                        if (RecognizeLoop(op, out var next)) return next;

                        throw new InvalidOperationException();
                    }
                    break;
                case Code.Brfalse:
                case Code.Brtrue:
                case Code.Blt:
                case Code.Bgt:
                case Code.Bge:
                case Code.Ble:
                case Code.Beq:
                    {
                        if (RecognizeBreak(op, out var next1)) return next1;
                        if (RecognizeIf(op, out var next)) return next;

                        throw new InvalidOperationException();
                    }
                    break;
                case Code.Clt:
                case Code.Cgt:
                case Code.Ceq:
                    {
                        if (RecognizeCBranch(op, out var next)) return next;
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

                        var local = _locals[varRef.Index];
                        if (local.canBeRef)
                        {
                            Push(local.RefValue);
                        }
                        else
                        {
                            Push(new StackItem()
                            {
                                expectedType = typeName,
                                text = local.name,
                                def = varRef
                            });
                        }
                    }
                    break;
                case Code.Stloc:
                    {
                        var varRef = (VariableReference)op.Operand;
                        var typeName = MapTypeName(varRef.VariableType);
                        var local = _locals[varRef.Index];

                        if (local.canBeOmitted)
                        {
                            Pop(typeName);
                        }
                        else if (local.canBeRef)
                        {
                            local.RefValue = Pop(typeName);
                        }
                        else
                        {
                            AppendLine($"{_locals[varRef.Index].name} = {Pop(typeName).text};");
                        }
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
                            text = Access(Pop().text, fieldRef.Name),
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
                case Code.Ldelem_Any:
                    {
                        var typeDef = (TypeDefinition)op.Operand;
                        var typeName = MapTypeName(typeDef);
                        var typeElemName = MapTypeName(typeDef.GetElementType());

                        PopTwo(out var left, out var right, typeElemName);
                        Push(new StackItem()
                        {
                            expectedType = typeName,
                            text = $"{left}[{right}]",
                            def = typeDef
                        });
                    }
                    break;

                case Code.Stfld:
                    {
                        var fieldRef = (FieldReference)op.Operand;
                        var typeName = MapTypeName(fieldRef.DeclaringType);
                        PopTwo(out var left, out var right, typeName);
                        AppendLine($"{Access(left.text, fieldRef.Name)} = {right.text};");
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

                        if (methodRef.Name == "op_UnaryNegation")
                        {
                            var expectedType = MapTypeName(methodRef.ReturnType);
                            var popped = Pop(expectedType);

                            Push(new StackItem()
                            {
                                def = popped.def,
                                expectedType = expectedType,
                                text = "-" + popped.text
                            });
                            return op.Next;
                        }

                        var paramStr = PopParameters(methodRef);

                        var call = "(" + paramStr + ")";

                        if (MapMethod(methodRef, call, out var result))
                        {
                            call = result;
                        }
                        else
                        {
                            var name = methodRef.Name;

                            if (name == ".ctor")
                            {
                                var expectedType = MapTypeName(methodRef.ReturnType);
                                AppendLine($"{Pop(expectedType)} = {methodRef.DeclaringType.Name}{call}");
                                return op.Next;
                            }
                            else if (name.StartsWith("get_"))
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
                                call = Access(Pop().text, call);
                            }
                            else
                            {
                                call = Access(methodRef.DeclaringType.Name, call);
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

        private string PopParameters(MethodReference methodRef)
        {
            var parameters = methodRef.Parameters.ToArray();
            int count = parameters.Length;
            var result = "";
            for (int j = count - 1; j >= 0; j--)
            {
                var expectedType = MapTypeName(parameters[j].ParameterType);
                result = Pop(expectedType).text + result;
                if (j > 0)
                {
                    result = ", " + result;
                }
            }

            return result;
        }

        private bool RecognizeReturnBranch(Instruction op, out Instruction next)
        {
            if (_returnBranches.Contains(op))
            {
                var jump = (Instruction)op.Operand;
                var value = (VariableReference)jump.Operand;
                var loc = _locals.First(p => p.definition == value);

                if (_ifScopeStack.Count > 0)
                {
                    AppendLine($"return {loc.name};");
                }


                next = op.Next;
                return true;
            }

            next = default;
            return false;
        }

        private bool IsInScope(Instruction op)
        {
            if (_ifScopeStack.TryPeek(out var ifScope))
            {
                if (op.Offset < ifScope.Start.Offset || op.Offset > ifScope.End.Offset)
                {
                    return false;
                }
            }

            return true;
        }
        private bool RecognizeIf(Instruction op, out Instruction next)
        {
            next = default;
            if (op.OpCode.FlowControl != FlowControl.Cond_Branch) return false;

            var jump = (Instruction)op.Operand;
            if (jump.Offset < op.Offset) return false;
            if (!IsInScope(jump))
            {
                return false;
            }

            var prev = jump.Previous;
            //while (prev.OpCode.Code == Code.Nop) prev = prev.Previous;

            if (prev.OpCode.Code == Code.Br && IsInScope((Instruction)prev.Operand))//IF-ELSE
            {
                var jump2 = (Instruction)prev.Operand;

                if (op.OpCode.Code == Code.Brtrue)
                    AppendLine($"if ({Pop("bool")} ) {{");
                else
                    AppendLine($"if (!({Pop("bool")}) ) {{");


                var ifStart = jump;
                var ifEnd = jump2;


                var elseStart = op.Next;
                var elseEnd = prev;

                _indent++;
                _ifScopeStack.Push(new IfScope()
                {
                    Start = ifStart,
                    End = ifEnd.Previous
                });


                int stackCount = Stack.Count;
                for (var current = ifStart; current != ifEnd;)
                {
                    current = ProcessInstruction(current);
                    if (current == null) return false;
                }

                NamedStack named = null;
                if (Stack.Count > stackCount)
                {
                    CheckStack(stackCount + 1);

                    var pop = Stack.Pop();
                    _stk++;
                    named = new NamedStack()
                    {
                        expectedType = pop.expectedType,
                        name = "sss" + _stk
                    };
                    _namedStkLocals.Add(named);

                    AppendLine($"{named.name} = {pop};");
                }

                _indent--;
                _ifScopeStack.Pop();
                AppendLine("} else {");

                _indent++;
                _ifScopeStack.Push(new IfScope()
                {
                    Start = elseStart,
                    End = elseEnd
                });

                for (var current = elseStart; current != elseEnd;)
                {
                    current = ProcessInstruction(current);
                    if (current == null) return false;
                }

                if (Stack.Count > stackCount)
                {
                    CheckStack(stackCount + 1);

                    var pop = Stack.Pop();
                    AppendLine($"{named.name} = {pop};");

                    Push(new StackItem()
                    {
                        text = named.name
                    });
                }

                _indent--;
                _ifScopeStack.Pop();
                AppendLine("}");

                next = jump2;
                return true;

            }
            else //IF
            {
                var ifStart = op.Next;
                var ifEnd = jump;

                //Invert skip span
                if (op.OpCode.Code == Code.Brtrue)
                {
                    AppendLine($"if (!({Pop("bool")})) {{");
                }
                else
                {
                    AppendLine($"if ({Pop("bool")}) {{");
                }

                _indent++;
                _ifScopeStack.Push(new IfScope()
                {
                    Start = ifStart,
                    End = ifEnd
                });

                int stackCount = Stack.Count;
                for (var current = ifStart; current != ifEnd;)
                {
                    current = ProcessInstruction(current);
                    if (current == null) return false;
                }

                CheckStack(stackCount);

                _ifScopeStack.Pop();
                _indent--;
                AppendLine("}");

                next = ifEnd;
                return true;
            }
        }
        private bool RecognizeBreak(Instruction op, out Instruction next)
        {
            next = default;
            if (_ifScopeStack.Count == 0) return false;
            if (op.OpCode.FlowControl != FlowControl.Cond_Branch) return false;
            var scope = _ifScopeStack.Peek();

            if (op.Next.OpCode.Code != Code.Br) return false;
            var jump = (Instruction)op.Next.Operand;
            if (jump != scope.End.Next) return false; // out of the loop; break;

            var code = op.OpCode.Code;

            if (_brComp.TryGetValue(code, out var comp))
            {
                PopTwo(out var left, out var right);
                AppendLine($"if(!({left} {comp} {right})) break;");
            }
            else
            {
                switch (code)
                {
                    case Code.Brfalse: AppendLine($"if({Pop("bool")}) break;"); break;
                    case Code.Brtrue: AppendLine($"if(!({Pop("bool")})) break;"); break;
                }
            }

            next = op.Next.Next;
            return true;
        }
        private bool RecognizeCBranch(Instruction op, out Instruction next)
        {
            var code = op.OpCode.Code;
            var oper = "";
            if (code == Code.Clt) oper = "<";
            if (code == Code.Cgt) oper = ">";
            if (code == Code.Ceq) oper = "==";

            if (code != Code.Ceq &&
                op.Next.OpCode == OpCodes.Ldc_I4)
            {
                int ldc = (int)op.Next.Operand;
                if (op.Next.Next.OpCode.Code == Code.Ceq)
                {
                    if (ldc == 0 || ldc == 1)
                    {
                        PopTwo(out var left, out var right);
                        Push(new StackItem()
                        {
                            expectedType = "bool",
                            text = ldc == 1
                                ? $"{left} {oper} {right}"
                                : $"!({left} {oper} {right})"
                        });
                        next = op.Next.Next.Next;
                        return true;
                    }
                }
            }

            if (code != Code.Ceq &&
                op.Next.OpCode == OpCodes.Stloc)
            {
                var varDef = (VariableDefinition)op.Next.Operand;
                if (varDef.VariableType.Name == "Boolean")
                {
                    PopTwo(out var left, out var right);
                    Push(new StackItem()
                    {
                        expectedType = "bool",
                        text = $"{left} {oper} {right}"
                    });

                    next = op.Next;
                    return true;
                }
            }

            {
                PopTwo(out var left, out var right);
                Push(new StackItem()
                {
                    expectedType = "int",
                    text = $"({left} {oper} {right})?1:0"
                });
                next = op.Next;
                return true;
            }
        }
        private bool RecognizeLoop(Instruction op, out Instruction next)
        {
            next = op.Next;
            if (op.OpCode.Code != Code.Br) return false;

            var checkStart = (Instruction)op.Operand;
            if (checkStart.Offset < op.Offset) return false;

            var current = checkStart;
            while (current.Operand is not Instruction)
            {
                current = current.Next;
                if (current == null) return false;
            }

            if (current.Operand != op.Next) return false;

            var loopStart = (Instruction)current.Operand;
            var end = current;

            AppendLine($"while(true){{");
            _indent++;
            _ifScopeStack.Push(new IfScope()
            {
                Start = loopStart,
                End = end
            });

            current = checkStart;
            while (current != end)
            {
                current = ProcessInstruction(current);
                if (current == null) return false;
            }

            //Exit the loop
            if (current.OpCode.FlowControl == FlowControl.Cond_Branch)
            {
                if (current.OpCode.Code == Code.Blt)
                {
                    PopTwo(out var left, out var right);
                    AppendLine($"if(!({left}<{right})) break;");
                }
                else
                {
                    AppendLine($"if(!({Pop("bool")})) break;");
                }
            }

            current = loopStart;
            while (current != checkStart)
            {
                current = ProcessInstruction(current);
                if (current == null) return false;
            }

            _indent--;
            _ifScopeStack.Pop();
            AppendLine("}");


            next = end.Next;
            return true;
        }
        private void CheckStack(int stackCount)
        {
            if (Stack.Count != stackCount) throw new InvalidOperationException();
        }
        public void Push(StackItem stackItem)
        {
            Stack.Push(stackItem);
        }
        public StackItem Pop(string expectedType = null)
        {
            return Stack.Pop();
        }
        public StackItem Peek()
        {
            return Stack.Peek();
        }
        public void AppendLine(string line)
        {
            for (int i = 0; i < _indent; i++)
            {
                _body.Append("\t");
            }

            _body.AppendLine(line);
        }
        public class NamedStack
        {
            public string name;
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
        public string Access(string from, string field)
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

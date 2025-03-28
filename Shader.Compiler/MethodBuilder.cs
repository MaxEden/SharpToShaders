using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using System.Globalization;
using System.Text;
using Lemon.Tools;
using FlowControl = Mono.Cecil.Cil.FlowControl;
using OpCodes = Mono.Cecil.Cil.OpCodes;
using Shader.BuildTarget;

namespace Compiler
{

    public class MethodBuilder
    {
        static Dictionary<string, string> _operators = new()
        {
            {"op_Addition","+"},
            { "op_Multiply", "*" },
            { "op_Division", "/" },
            { "op_Subtraction", "-" },
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
        private StringBuilder _unresolved;

        private int _indent;
        private MethodDefinition _method;
        private HashSet<Instruction> _returnBranches;

        private Dictionary<FieldReference, Var> _varyings = new();
        private ShaderProgram _shaderProgram;
        private IBuildTarget _buildTarget;
        private ProgramType _programType;

        public Dictionary<FieldReference, Var> Varyings => _varyings;
        public List<NamedStack> NamedStackLocals => _namedStkLocals;

        public StringBuilder Body => _body;

        public LocalVar[] Locals => _locals;

        public ProgramType ProgramType => _programType;

        public string Header { get; private set; }

        public void Build(ShaderProgram shaderProgram, MethodDefinition method, ProgramType programType)
        {
            Stack.Clear();

            _shaderProgram = shaderProgram;
            _buildTarget = shaderProgram.BuildTarget;
            _buildTarget.Context.Builder = this;

            _programType = programType;
            _indent = 0;
            _body = new StringBuilder();
            method.Body.SimplifyMacros();

            _method = method;

            BuildVaryings(method);

            LocalsSetup(method);

            BuildHeader();

            _unresolved = new StringBuilder();

            _namedStkLocals.Clear();

            _indent++;

            var op = method.Body.Instructions[0];
            while (op != null)
            {
                op = ProcessInstruction(op);
            }
            _indent--;

            var sb = new StringBuilder();

            if (_unresolved.Length > 0)
            {
                sb.AppendLine("/*___unresolved_____");
                sb.AppendLine(_unresolved.ToString());
                sb.AppendLine("_____________*/");
            }
  

           _body.Append(sb);
        }

        public string ToIdentifer(MethodReference method)
        {
            if(method.DeclaringType == _shaderProgram.MainType)
            {
                return method.Name;
            }

            //char[] chars = method.FullName.ToCharArray();
            //for (int i = 0; i < chars.Length; i++)
            //{
            //    if (!char.IsLetterOrDigit(chars[i])) chars[i]= '_';
            //}
            //return new string(chars);

            return method.DeclaringType.Name + "_" + method.Name;
        }

        private void BuildHeader()
        {
            var sb = new StringBuilder();
            sb.Append(MapTypeName(_method.ReturnType));
            sb.Append(" ");
            sb.Append(ToIdentifer(_method));
            sb.Append("(");

            int index = 0;
            foreach (var param in _method.Parameters)
            {
                if (index > 0) sb.Append(", ");
                sb.Append(MapTypeName(param.ParameterType));
                sb.Append(" ");
                sb.Append(param.Name);
                index++;                
            }

            sb.Append(")");
            Header = sb.ToString();
        }

        public static void DumpInstructions(string path, MethodDefinition method)
        {
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

            var outputPath = path + method.DeclaringType.Name + "." + method.Name + ".opcodes.txt";
            File.WriteAllText(outputPath, instB.ToString());
            Console.WriteLine("OpCodes dump:" + outputPath);
        }

        private void BuildVaryings(MethodDefinition method)
        {
            _varyings.Clear();

            if (_programType == ProgramType.Vertex)
            {
                foreach (var parameter in method.Parameters.ToArray())
                {
                    foreach (var field in parameter.ParameterType.Resolve().Fields)
                    {
                        _buildTarget.AddVarying(_programType, field, VarType.Attribute, InputType.In);
                    }
                }

                foreach (var field in method.ReturnType.Resolve().Fields)
                {
                    _buildTarget.AddVarying(_programType, field, VarType.Varying, InputType.Out);
                }
            }
            else if (_programType == ProgramType.Fragment)
            {
                foreach (var parameter in method.Parameters.ToArray())
                {
                    foreach (var field in parameter.ParameterType.Resolve().Fields)
                    {
                        _buildTarget.AddVarying(_programType, field, VarType.Varying, InputType.In);
                    }
                }
            }
            else if (_programType == ProgramType.SubFunction)
            {
                foreach (var parameter in method.Parameters.ToArray())
                {
                    if (parameter.ParameterType.IsValueType)
                    {
                        
                    }
                }
            }

            var instr = method.Body.Instructions.ToArray();
            foreach (var op in instr)
            {
                if (op.Operand is FieldDefinition field)
                {
                    if (_varyings.TryGetValue(field, out var value))
                    {
                        value.IsUsed = true;
                    }

                    if(_shaderProgram.Uniforms.TryGetValue(field, out value))
                    {
                        value.IsUsed = true;
                    }
                }
            }
        }

        private void LocalsSetup(MethodDefinition method)
        {
            var locals = method.Body.Variables.OrderBy(p => p.Index).ToArray();

            var scope = method.DebugInformation.Scope;
            var debugLocals = scope?.Variables.ToArray();

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
                var dbg = debugLocals?.FirstOrDefault(p => p.Index == i);
                if (dbg != null)
                {
                    localVar.name = dbg.Name;
                    localVar.canInline = true;

                }
                else
                {
                    localVar.name = "tmp" + i;
                    if (localVar.set == 1 && localVar.load == 1)
                    {
                        localVar.canBeRef = true;
                    }

                    if (localVar.load == 0)
                    {
                        localVar.canBeOmitted = true;
                    }
                }


                var name = _locals[i].name;
                if (_varyings.Values.Any(p => p.Name == name))
                {
                    _locals[i].name = "l" + i + "_" + name;
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
                        if (RecognizeReturn(op, out var next1)) return next1;
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
                        var typeName = MapTypeName(_method.ReturnType);
                        var popped = Pop(typeName);

                        if (MapReturn(popped, out var text))
                        {
                            AppendLine(text);
                        }
                        else
                        {
                            AppendLine($"return {popped};");
                        }
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
                            if (local.canInline && !local.isDeclared)
                            {
                                local.isDeclared = true;
                                AppendLine($"{typeName} {local.name} = {Pop(typeName).text};");
                            }
                            else
                            {
                                AppendLine($"{local.name} = {Pop(typeName).text};");
                            }
                        }
                    }
                    break;
                case Code.Ldfld:
                case Code.Ldflda:
                    {
                        var fieldRef = (FieldReference)op.Operand;
                        var typeNameFrom = MapTypeName(fieldRef.DeclaringType);
                        var typeNameTo = MapTypeName(fieldRef.FieldType);

                        if (MapField(fieldRef, out var text))
                        {
                            Pop(typeNameFrom);
                            Push(new StackItem()
                            {
                                expectedType = typeNameTo,
                                text = text,
                                def = fieldRef
                            });
                        }
                        else
                        {
                            Push(new StackItem()
                            {
                                expectedType = typeNameTo,
                                text = Access(Pop(typeNameFrom).text, fieldRef.Name),
                                def = fieldRef
                            });
                        }
                    }
                    break;
                case Code.Ldsfld:
                case Code.Ldsflda:
                    {
                        var fieldRef = (FieldReference)op.Operand;
                        var typeName = MapTypeName(fieldRef.FieldType);

                        if (MapField(fieldRef, out var text))
                        {
                            Push(new StackItem()
                            {
                                expectedType = typeName,
                                text = text,
                                def = fieldRef
                            });
                        }
                        else
                        {
                            Push(new StackItem()
                            {
                                expectedType = typeName,
                                text = fieldRef.Name,
                                def = fieldRef
                            });
                        }
                    }
                    break;
                case Code.Ldelem_Any:
                    {
                        var typeDef = (TypeReference)op.Operand;
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

                        if (MapField(fieldRef, out var text))
                        {
                            AppendLine($"{text} = {right.text};");
                        }
                        else
                        {
                            AppendLine($"{Access(left.text, fieldRef.Name)} = {right.text};");
                        }
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
                case Code.Ldobj:
                    {
                        //Same stack content as a value
                    }
                    break;
                case Code.Stind_R4:
                    {
                        PopTwo(out var left, out var right, "int");
                        AppendLine(left + " = " + right.text + ";");
                    }
                    break;
                case Code.Stobj:
                    {
                        var typeRef = (TypeReference)op.Operand;
                        PopTwo(out var left, out var right, MapTypeName(typeRef));
                        AppendLine(left + " = " + right.text + ";");
                    }
                    break;
                case Code.Ldc_R4:
                    {
                        float value = (float)op.Operand;
                        Push(new StackItem()
                        {
                            expectedType = "float",
                            text = value.ToString(CultureInfo.InvariantCulture),
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
                            text = value.ToString(CultureInfo.InvariantCulture),
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

                        var text = "";

                        if (MapMethod(methodRef, paramStr, out var result, out var needsBrackets))
                        {
                            text = result;
                        }
                        else
                        {
                            var name = methodRef.Name;

                            if (name == ".ctor")
                            {
                                var expectedType = MapTypeName(methodRef.ReturnType);
                                AppendLine($"{Pop(expectedType)} = {methodRef.DeclaringType.Name}{paramStr.ToStringBrackets()}");
                                return op.Next;
                            }
                            else if (name.StartsWith("get_"))
                            {
                                name = name.Substring(4);
                                text = name;
                            }
                            else
                            {
                                text = name + paramStr.ToStringBrackets();                                                              
                            }

                            if (methodRef.HasThis)
                            {
                                text = Access(Pop().text, text);
                            }
                            else
                            {

                                text = methodRef.DeclaringType.Name + "_" + text;
                                _unresolved.AppendLine(text);
                                //call = Access(methodRef.DeclaringType.Name, call);
                            }
                        }

                        if (methodRef.ReturnType.Name == "Void")
                        {
                            text = text + ";";
                            AppendLine(text);
                        }
                        else
                        {
                            var typeName = MapTypeName(methodRef.ReturnType);
                            Push(new StackItem()
                            {
                                expectedType = typeName,
                                text = text,
                                def = methodRef,
                                needsBrackets = needsBrackets
                            });
                        }
                    }
                    break;
                case Code.Newobj:
                    {
                        var methodRef = (MethodReference)op.Operand;
                        var paramStr = PopParameters(methodRef);

                        var text = "";

                        if (MapMethod(methodRef, paramStr, out var result, out var needsBrackets))
                        {
                            text = result;
                        }
                        else
                        {
                            text = methodRef.DeclaringType.Name + "("+ paramStr.ToString()+")";
                        }

                        Push(new StackItem()
                        {
                            text = text,
                            def = methodRef,
                            needsBrackets = needsBrackets
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
                    _unresolved.Append(op.OpCode.Code);
                    _unresolved.Append(" ");
                    if (op.Operand != null)
                    {
                        _unresolved.Append(op.Operand);
                        _unresolved.Append(" ");
                        _unresolved.Append(op.Operand.GetType().Name);
                    }

                    _unresolved.AppendLine();
                    break;
            }

            return op.Next;
        }

        private Parameters PopParameters(MethodReference methodRef)
        {
            var parameters = methodRef.Parameters.ToArray();
            int count = parameters.Length;

            var par = new Parameters();
            par.List = new StackItem[count];

            for (int j = count - 1; j >= 0; j--)
            {
                var expectedType = MapTypeName(parameters[j].ParameterType);
                var item = Pop(expectedType);
                par.List[j] = item;
            }

            return par;
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

        private bool RecognizeReturn(Instruction op, out Instruction next)
        {
            if (!_returnBranches.Contains(op))
            {
                next = default;
                return false;
            }

            var jump = (Instruction)op.Operand;
            var value = (VariableReference)jump.Operand;
            var loc = _locals.First(p => p.definition == value);

            if (_ifScopeStack.Count > 0)
            {
                if (MapReturn(new StackItem() { text = loc.name }, out var text))
                {
                    AppendLine(text);
                }
                else
                {
                    AppendLine($"return {loc.name};");
                }
            }


            next = op.Next;
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
                    AppendLine($"if (!{Pop("bool").btext} ) {{");


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
                    AppendLine($"if (!{Pop("bool").btext}) {{");
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
                    case Code.Brtrue: AppendLine($"if(!{Pop("bool").btext}) break;"); break;
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
                                : $"!({left} {oper} {right})",
                            needsBrackets = true
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
                        text = $"{left} {oper} {right}",
                        needsBrackets = true
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
                    text = $"({left} {oper} {right})?1:0",
                    needsBrackets = true
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

            //Exit the loop by condition
            if (current.OpCode.FlowControl == FlowControl.Cond_Branch)
            {
                if (_brComp.TryGetValue(current.OpCode.Code, out var comp))
                {
                    PopTwo(out var left, out var right);
                    AppendLine($"if(!({left} {comp} {right})) break;");
                }
                else
                {
                    switch (current.OpCode.Code)
                    {
                        case Code.Brfalse: AppendLine($"if({Pop("bool")}) break;"); break;
                        case Code.Brtrue: AppendLine($"if(!{Pop("bool").btext}) break;"); break;
                    }
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

        private bool MapReturn(StackItem popped, out string text)
        {
            if (_buildTarget.MapReturn(popped, out text))
            {
                return true;
            }

            text = default;
            return false;
        }
        private bool MapField(FieldReference fieldRef, out string text)
        {
            if(_shaderProgram.Uniforms.TryGetValue(fieldRef, out var uniform))
            {
                text = uniform.Name;
                return true;
            }

            if (_varyings.TryGetValue(fieldRef, out var value))
            {
                text = value.Name;
                return true;
            }

            if (_buildTarget.MapField(fieldRef, out text)) return true;

            text = default;
            return false;
        }
        public string MapTypeName(TypeReference typeRef)
        {
            if (_buildTarget.MapTypeName(typeRef, out var name)) return name;

            if (typeRef.IsArray && _buildTarget.MapTypeName(typeRef.GetElementType(), out var elType))
            {
                return elType + "[]";
            }
            return typeRef.Name;
        }

        private bool MapMethod(MethodReference methodRef, Parameters call, out string result, out bool needsBrackets)
        {         
            if (_buildTarget.MapMethod(methodRef, call, out result, out needsBrackets)) return true;

            needsBrackets = false;
            if (methodRef.Name == "op_Implicit")
            {
                result = call.ToParamString();
                return true;
            }

            if(_shaderProgram.TargetMethods.Contains(methodRef.Resolve()))
            {               
                result = ToIdentifer(methodRef) + call;
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
                text = left.btext + " " + oper + " " + right.btext,
                needsBrackets = true
            });
        }
    }
   
}

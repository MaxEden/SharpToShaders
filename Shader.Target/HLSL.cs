using Compiler;
using Mono.Cecil;
using System.Text;
using Lemon.Tools;

namespace Shader.BuildTarget
{
    public class HLSL : IBuildTarget
    {
        static Dictionary<string, string> _typeMap = new()
        {
            {"Single", "float" },
            {"Double", "double" },
            {"Boolean", "bool" },
            {"Int32", "int" },

            {"float2", "float2"},
            {"float3", "float3"},
            {"float4", "float4"},

        };

        private static Dictionary<string, string> _methodMap = new()
        {
            { "tex2D", "tex2D" }
        };
        public Context Context { get; set; }
        public bool MapReturn(StackItem popped, out string text)
        {
            if (Context.Builder.Program == ProgramType.Vertex)
            {
                text = $"return OUT;";
                return true;
            }

            if (Context.Builder.Program == ProgramType.Fragment)
            {
                text = $"return {popped};";
                return true;
            }

            text = default;
            return false;
        }
        public bool MapField(FieldReference fieldRef, out string text)
        {
            text = default;
            return false;
        }
        public bool MapTypeName(Mono.Cecil.TypeReference typeRef, out string result)
        {
            if (_typeMap.TryGetValue(typeRef.Name, out var name))
            {
                result = name;
                return true;
            }
            if (typeRef.IsArray && _typeMap.TryGetValue(typeRef.GetElementType().Name, out var elType))
            {
                result = elType + "[]";
                return true;
            }
            result = default;
            return false;
        }
        public bool MapMethod(MethodReference methodRef, string call, out string result)
        {
            if (methodRef.DeclaringType.Name == "MathF")
            {
                result = methodRef.Name.ToLowerInvariant() + call;
                return true;
            }

            if (methodRef.DeclaringType.Name == "math")
            {
                if (methodRef.Name == "lerp")
                {
                    result = "mix" + call;
                    return true;
                }

                result = methodRef.Name.ToLowerInvariant() + call;
                return true;
            }

            if (methodRef.DeclaringType.Name == "Global")
            {
                if (_methodMap.TryGetValue(methodRef.Name, out var met))
                {
                    result = met + call;
                    return true;
                }
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

        public void WriteOut(StringBuilder sb)
        {
            if(Context.Builder.Program == ProgramType.Vertex)
            {
                sb.AppendLine("struct vertex_info\n{");
                foreach (var field in Context.Builder.Varyings.Values.Where(p => p.IsUsed && !p.BuiltIn && p.Type == VarType.Attribute))
                {
                    sb.AppendLine($"{Context.Builder.MapTypeName(field.FieldType)} {field.Name.Replace("IN.", "")} : {field.Semantic};");
                }
                sb.AppendLine("}");
                sb.AppendLine();

                sb.AppendLine("struct vertex_to_pixel\n{");
                foreach (var field in Context.Builder.Varyings.Values.Where(p => p.IsUsed && p.Type == VarType.Varying))
                {
                    sb.AppendLine($"{Context.Builder.MapTypeName(field.FieldType)} {field.Name.Replace("OUT.", "")} : {field.Semantic};");
                }
                sb.AppendLine("}");
                sb.AppendLine();

                foreach (var field in Context.Builder.Varyings.Values.Where(p => p.IsUsed && !p.BuiltIn && p.Type == VarType.Uniform))
                {
                    sb.AppendLine($"{Context.Builder.MapTypeName(field.FieldType)} {field.Name};");
                }
                sb.AppendLine();

                sb.AppendLine("vertex_to_pixel main(in vertex_info IN)\n{");
                sb.AppendLine("vertex_to_pixel OUT;");
                foreach (var local in Context.Builder.Locals)
                {
                    if (local.canBeOmitted) continue;
                    if (local.canBeRef) continue;
                    if (local.canInline) continue;
                    sb.AppendLine(
                        $"{Context.Builder.MapTypeName(local.definition.VariableType)} {local};"
                    );
                }

                foreach (var named in Context.Builder.NamedStackLocals)
                {
                    sb.AppendLine(
                        $"{named.expectedType} {named.name};"
                    );
                }

                sb.AppendLine(Context.Builder.Body.ToString());
                sb.AppendLine("}");

            }
            else
            {
                sb.AppendLine("struct input_from_vertex\n{");
                foreach (var field in Context.Builder.Varyings.Values.Where(p => p.IsUsed && p.Type == VarType.Varying))
                {
                    sb.AppendLine($"{Context.Builder.MapTypeName(field.FieldType)} {field.Name.Replace("IN.","")} : {field.Semantic};");
                }
                sb.AppendLine("}");
                sb.AppendLine();

                foreach (var field in Context.Builder.Varyings.Values.Where(p => p.IsUsed && !p.BuiltIn && p.Type == VarType.Uniform))
                {
                    sb.AppendLine($"{Context.Builder.MapTypeName(field.FieldType)} {field.Name};");
                }
                sb.AppendLine();

                sb.AppendLine("float4 main(in input_from_vertex IN)\n{");
                foreach (var local in Context.Builder.Locals)
                {
                    if (local.canBeOmitted) continue;
                    if (local.canBeRef) continue;
                    if (local.canInline) continue;
                    sb.AppendLine(
                        $"{Context.Builder.MapTypeName(local.definition.VariableType)} {local};"
                    );
                }

                foreach (var named in Context.Builder.NamedStackLocals)
                {
                    sb.AppendLine(
                        $"{named.expectedType} {named.name};"
                    );
                }

                sb.AppendLine(Context.Builder.Body.ToString());

                sb.AppendLine("}");
            }

            if (Context.Builder.SubMethods != null)
                foreach (var meth in Context.Builder.SubMethods)
                {
                    sb.AppendLine(meth.Value.Header);
                    sb.AppendLine("{");
                    sb.AppendLine(meth.Value.Body.ToString());
                    sb.AppendLine("}");
                }
        }

        public void AddVarying(ProgramType programType, FieldDefinition field, VarType varType, InputType input)
        {
            var semantic = field.CustomAttributes.FirstOrDefault(p => p.AttributeType.Resolve()
            .Interfaces.Any(x => x.InterfaceType.Name == "ISemantic"));

            string semanticName = null;
            if (semantic != null)
            {
                semanticName = semantic.AttributeType.Name.Replace("Attribute", "");
            }

            if (programType == ProgramType.Vertex)
            {
                if (input == InputType.In)
                {
                    if (varType == VarType.Attribute)
                    {
                        Context.Builder.Varyings.Add(field,
                        new Var()
                        {
                            Name = "IN." + field.Name,
                            FieldType = field.FieldType,
                            Type = varType,
                            InputType = input,
                            Semantic = semanticName
                        });
                    }
                    else
                    {
                        Context.Builder.Varyings.Add(field,
                        new Var()
                        {
                            Name = field.Name,
                            FieldType = field.FieldType,
                            Type = varType,
                            InputType = input,
                            Semantic = semanticName
                        });
                    }                    
                }
                else if (input == InputType.Out)
                {
                    Context.Builder.Varyings.Add(field,
                        new Var()
                        {
                            Name = "OUT."+field.Name,
                            FieldType = field.FieldType,
                            Type = varType,
                            InputType = input,
                            Semantic = semanticName
                        });
                }
            }
            else if (programType == ProgramType.Fragment)
            {
                if (input == InputType.In)
                {
                    Context.Builder.Varyings.Add(field,
                        new Var()
                        {
                            Name = "IN."+field.Name,
                            FieldType = field.FieldType,
                            Type = varType,
                            InputType = input,
                            Semantic = semanticName
                        });
                }
            }
        }

        public void AddVarying(ProgramType programType, ParameterDefinition parameter, VarType varying, InputType @in)
        {
            throw new NotImplementedException();
        }
    }
}

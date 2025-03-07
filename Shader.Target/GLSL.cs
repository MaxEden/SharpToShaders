using Compiler;
using Mono.Cecil;
using System.Text;
using static Compiler.ShaderBuilder;
using Lemon.Tools;

namespace Shader.BuildTarget
{
    public class GLSL : IBuildTarget
    {
        static Dictionary<string, string> _typeMap = new()
        {
            {"Single", "float" },
            {"Double", "double" },
            {"Boolean", "bool" },
            {"Int32", "int" },

            {"float2", "vec2"},
            {"float3", "vec3"},
            {"float4", "vec4"},

            //{"fixed2", "vec2"},
            //{"fixed3", "vec3"},
            //{"fixed4", "vec4"},
        };

        private static Dictionary<string, string> _methodMap = new()
        {
            { "tex2D", "texture2D" }
        };
        public Context Context { get; set; }
        public bool MapReturn(StackItem popped, out string text)
        {
            if (Context.Builder.Program == ProgramType.Vertex)
            {
                text = $"return;//gl_Position is set";
                return true;
            }

            if (Context.Builder.Program == ProgramType.Fragment)
            {
                text = $"gl_FragColor = {popped}; return;";
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
            foreach (var field in Context.Builder.Varyings.Values.Where(p => p.IsUsed && !p.BuiltIn && p.Type == VarType.Attribute))
            {
                sb.AppendLine($"attribute {Context.Builder.MapTypeName(field.FieldType)} {field.Name};");
            }

            sb.AppendLine();
            foreach (var field in Context.Builder.Varyings.Values.Where(p => p.IsUsed && !p.BuiltIn && p.Type == VarType.Uniform))
            {
                sb.AppendLine($"uniform {Context.Builder.MapTypeName(field.FieldType)} {field.Name};");
            }
            sb.AppendLine();
            foreach (var field in Context.Builder.Varyings.Values.Where(p => p.IsUsed && p.Type == VarType.Varying))
            {
                sb.AppendLine($"varying {Context.Builder.MapTypeName(field.FieldType)} {field.Name};");
            }
            sb.AppendLine();
            sb.AppendLine($"main(){{");

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

        public void AddVarying(ProgramType programType, FieldDefinition field, VarType varType, InputType input)
        {
            if (programType == ProgramType.Vertex)
            {
                if (input == InputType.In)
                {
                    Context.Builder.Varyings.Add(field,
                    new Var()
                    {
                        Name = field.Name,
                        FieldType = field.FieldType,
                        Type = varType,
                        InputType = input
                    });
                }
                else if (input == InputType.Out)
                {
                    if (field.HasAttribute("POSITIONAttribute"))
                    {
                        Context.Builder.Varyings.Add(field,
                            new Var()
                            {
                                Name = "gl_Position",
                                BuiltIn = true,
                                FieldType = field.FieldType,
                                Type = VarType.Attribute,
                                InputType = input
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
                                InputType = input
                            });
                    }
                }
            }
            else if(programType == ProgramType.Fragment)
            {
                if(input == InputType.In)
                {
                    if (field.HasAttribute("POSITIONAttribute"))
                    {

                    }
                    else
                    {
                        Context.Builder.Varyings.Add(field,
                            new Var()
                            {
                                Name = field.Name,
                                FieldType = field.FieldType,
                                Type = varType,
                                InputType = input
                            });
                    }
                }
            }
        }
    }
}

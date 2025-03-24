using Compiler;
using Mono.Cecil;
using System.Text;
using Lemon.Tools;

namespace Shader.BuildTarget
{
    public class GLSL : IBuildTarget
    {
        static Dictionary<string, string> _typeMap = new()
        {
            {"Void","void"},
            {"Single", "float" },
            {"Double", "double" },
            {"Boolean", "bool" },
            {"Int32", "int" },

            {"float2", "vec2"},
            {"float3", "vec3"},
            {"float4", "vec4"},

            {"Matrix4x4","mat4x4"}
        };

        private static Dictionary<string, string> _methodMap = new()
        {
            { "tex2D", "texture2D" }
        };
        public Context Context { get; set; }
        public bool MapReturn(StackItem popped, out string text)
        {
            if (Context.Builder.ProgramType == ProgramType.Vertex)
            {
                text = $"return;//gl_Position is set";
                return true;
            }

            if (Context.Builder.ProgramType == ProgramType.Fragment)
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
        public bool MapMethod(MethodReference methodRef, Parameters call, out string result, out bool needsBrackets)
        {
            needsBrackets = false;

            if (call == null)
            {
                result = default;
                if (methodRef.DeclaringType.Name == "MathF" || methodRef.DeclaringType.Name == "math")
                {                    
                    return true;
                }

                if (methodRef.DeclaringType.Name == "Global")
                {
                    if (_methodMap.TryGetValue(methodRef.Name, out var met))
                    {
                        return true;
                    }
                }

                return false;
            }

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

                if (methodRef.Name == "mul")
                {
                    //swap sides?
                    result = call.List[0].btext + " * " + call.List[1].btext;
                    needsBrackets = true; 
                    return true;
                }

                result = methodRef.Name.ToLowerInvariant() + call;
                return true;
            }

            if (methodRef.DeclaringType.Name == "Global")
            {
                if (_methodMap.TryGetValue(methodRef.Name, out var met))
                {
                    result = met + call.ToStringBrackets();
                    return true;
                }
                //result = methodRef.Name + call;
                //return true;
            }

            result = default;
            return false;
        }

        public void WriteOut(StringBuilder sb)
        {
            foreach (var field in Context.ShaderProgram.Uniforms.Values.Where(p => p.IsUsed && !p.BuiltIn && p.Type == VarType.Uniform))
            {
                sb.AppendLine($"uniform {Context.Builder.MapTypeName(field.FieldType)} {field.Name};");
            }
            sb.AppendLine();

            if (Context.ShaderProgram.TargetMethods != null)
            {
                foreach (var meth in Context.ShaderProgram.BuiltMethods.Reverse())
                {
                    if (meth.Key == Context.ShaderProgram.MainMethod) continue;

                    sb.AppendLine(meth.Value.Header);
                    sb.AppendLine("{");
                    sb.Append(meth.Value.Body.ToString());
                    sb.AppendLine("}");
                    sb.AppendLine();
                }
            }

            foreach (var field in Context.Builder.Varyings.Values.Where(p => p.IsUsed && !p.BuiltIn && p.Type == VarType.Attribute))
            {
                sb.AppendLine($"attribute {Context.Builder.MapTypeName(field.FieldType)} {field.Name};");
            }

            sb.AppendLine();

            foreach (var field in Context.Builder.Varyings.Values.Where(p => p.IsUsed && p.Type == VarType.Varying))
            {
                sb.AppendLine($"varying {Context.Builder.MapTypeName(field.FieldType)} {field.Name};");
            }
            sb.AppendLine();
            sb.AppendLine($"void main(){{");

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

            sb.Append(Context.Builder.Body.ToString());
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
                                Name = "v_" + field.Name,
                                FieldType = field.FieldType,
                                Type = varType,
                                InputType = input
                            });
                    }
                }
            }
            else if (programType == ProgramType.Fragment)
            {
                if (input == InputType.In)
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

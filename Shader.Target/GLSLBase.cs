using Compiler;
using Mono.Cecil;
using System.Text;
using Lemon.Tools;

namespace Shader.BuildTarget
{
    public abstract class GLSLBase : IBuildTarget
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
        public virtual bool MapMethod(MethodReference methodRef, Parameters call, out string result, out bool needsBrackets)
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
                else if (methodRef.Name == "saturate")
                {
                    result = "clamp(" + call.List[0].btext + ", 0, 1)";
                    needsBrackets = false;
                    return true;
                }
                else if (methodRef.Name == "mul")
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

        public abstract void WriteOut(StringBuilder sb);
        public string Extension => "glsl";

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

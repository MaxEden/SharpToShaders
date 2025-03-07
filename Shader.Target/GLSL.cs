using Compiler;
using Mono.Cecil;
using System.Text;
using static Compiler.ShaderBuilder;

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

            {"fixed2", "vec2"},
            {"fixed3", "vec3"},
            {"fixed4", "vec4"},
        };

        private static Dictionary<string, string> _methodMap = new()
        {
            { "tex2D", "texture2D" }
        };
                public Context Context { get; set; }
        public bool MapReturn(StackItem popped, out string text)
        {
            if (Context.ProgramType == ShaderBuilder.ProgramType.Vertex)
            {
                text = $"return;//gl_Position is set";
                return true;
            }

            if (Context.ProgramType == ShaderBuilder.ProgramType.Fragment)
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

            if (methodRef.DeclaringType.Name == "Unity")
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

        public void WriteHeader(StringBuilder sb)
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

            for (int i = 0; i < usedLocals.Length; i++)
            {
                 sb.AppendLine(
                    $"{Context.Builder.MapTypeName(usedLocals[i].definition.VariableType)} {usedLocals[i]};"
                );
            }

            foreach (var stack in usedNamedLocals)
            {
                sb.AppendLine(
                    $"{stack.expectedType} {stack.name};"
                );
            }

            sb.AppendLine(Context.Builder.Body.ToString());

            sb.AppendLine("}");
        }

        public void WriteFooter(StringBuilder sb)
        {
            sb.AppendLine("}");
        }
    }
}

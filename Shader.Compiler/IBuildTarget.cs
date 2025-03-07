using Compiler;
using Mono.Cecil;
using System.Text;

namespace Shader.BuildTarget
{
    public interface IBuildTarget
    {
        Context Context { get; set; }

        bool MapField(FieldReference fieldRef, out string text);
        bool MapMethod(MethodReference methodRef, string call, out string result);
        bool MapReturn(StackItem popped, out string text);
        bool MapTypeName(TypeReference typeRef, out string result);
        void WriteFooter(StringBuilder sb);
        void WriteHeader(StringBuilder sb, ShaderBuilder.Var[] usedVaryings, ShaderBuilder.LocalVar[] usedLocals, ShaderBuilder.NamedStack[] usedNamedLocals);
    }
}
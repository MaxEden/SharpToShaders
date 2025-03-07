using Compiler;
using Mono.Cecil;
using System.Text;

namespace Shader.BuildTarget
{
    public interface IBuildTarget
    {
        Context Context { get; set; }

        void AddVarying(ProgramType programType, FieldDefinition field, VarType attribute, InputType @in);
        bool MapField(FieldReference fieldRef, out string text);
        bool MapMethod(MethodReference methodRef, string call, out string result);
        bool MapReturn(StackItem popped, out string text);
        bool MapTypeName(TypeReference typeRef, out string result);

        void WriteOut(StringBuilder sb);
    }
}
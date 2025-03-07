using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Compiler
{
    public enum ProgramType
    {
        None,
        Vertex,
        Fragment,
        SubFunction
    }

    public enum VarType
    {
        None,
        Varying,
        Attribute,
        Uniform
    }

    public enum InputType
    {
        None,
        In,
        Out
    }
    public class Var
    {
        public VarType Type;
        public TypeReference FieldType;
        public string Name;
        public bool IsUsed;
        public bool BuiltIn;
        public InputType InputType;
        public string Semantic;
    }

    public class LocalVar
    {
        public VariableDefinition definition;
        public string name;
        public int set;
        public int load;
        public bool canBeRef;
        public bool canBeOmitted;
        public bool canInline;
        public bool isDeclared;
        public StackItem RefValue { get; set; }

        public override string ToString()
        {
            return name;
        }
    }

    public class NamedStack
    {
        public string name;
        public string expectedType;
    }
}

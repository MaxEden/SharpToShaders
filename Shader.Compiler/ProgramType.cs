using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Text;

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

    public class Parameters
    {
        public StackItem[] List;
        public string ToParamString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < List.Length; i++)
            {
                sb.Append(List[i].text);
                if (i < List.Length - 1) sb.Append(", ");
            }
            return sb.ToString();
        }

        public string ToStringBrackets()
        {
            return "(" + ToParamString() + ")";
        }

        public override string ToString()
        {
            return ToStringBrackets();
        }

    }
}

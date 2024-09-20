using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil.Cil;

namespace Compiler
{
    public class StackItem
    {
        public string text;
        internal object def;
        public string name;
        public string expectedType;
        public bool needsBrackets;
        public string btext => needsBrackets ? $"({text})" : text;

        public override string ToString()
        {
            return text;
        }
    }

    public class IfScope
    {
        public Instruction Start;
        public Instruction End;
    }
}

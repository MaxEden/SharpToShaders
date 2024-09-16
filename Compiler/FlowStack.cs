using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    internal class FlowStack
    {
        private List<StackItem> _items = new List<StackItem>();
        private int minhead = 0;
        private int maxhead = 0;

        public int MinCount => minhead + 1;
        public int MaxCount => maxhead + 1;
        internal void Clear()
        {
            _items.Clear();
            minhead = 0;
            maxhead = 0;
        }

        public StackItem Pop()
        {
            var pop = _items[minhead];
            maxhead--;
            minhead--;
            return pop;
        }

        public void Push(StackItem stackItem)
        {
            maxhead++;
            minhead++;
            if (_items.Count <= minhead)
            {
                _items.Add(stackItem);
            }
            else
            {
                var old = _items[minhead];
                if (old.name != null)
                {
                    stackItem.name = old.name;
                    _items[minhead] = stackItem;
                }
            }
        }
    }

    public class StackItem
    {
        public string text;
        internal object def;
        public string name;
        public string expectedType;

        public override string ToString()
        {
            return text;
        }
    }
}

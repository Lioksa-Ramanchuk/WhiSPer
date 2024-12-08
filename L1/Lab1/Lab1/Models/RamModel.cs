using System;
using System.Collections.Generic;

namespace Lab1.Models
{
    public class RamModel
    {
        private List<int> _stack = new List<int>();

        public List<int> Stack
        {
            get => new List<int>(_stack);
            set => _stack = new List<int>(value);
        }

        public int StackCount => _stack.Count;

        public void StackPush(int item)
        {
            _stack.Add(item);
        }

        public int StackPop()
        {
            if (_stack.Count == 0)
                throw new InvalidOperationException("The stack is empty.");

            int item = _stack[_stack.Count - 1];
            _stack.RemoveAt(_stack.Count - 1);
            return item;
        }

        public int StackPeek()
        {
            if (_stack.Count == 0)
                throw new InvalidOperationException("The stack is empty.");

            return _stack[_stack.Count - 1];
        }
    }
}

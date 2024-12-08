using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Lab2.Models
{
    public class RamModel
    {
        public int Result { get; set; } = 0;
        public Stack<int> Stack { get; } = new Stack<int>();
    }
}

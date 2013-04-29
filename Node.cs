using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSharp
{
    public class Node
    {
        public Char Key;
        public Node Left, Right;

        public Node(Char NodeValue = '*')
        {
            this.Key = NodeValue;
        }
    }
}
using System;
using System.Collections.Generic;

namespace Riddley.VideoGame.Model
{
    public class Edge : Node
    {
        private readonly Node item1;
        private readonly Node item2;

        public Edge(Node item1, Node item2)
        {
            this.item1 = item1;
            this.item2 = item2;
        }

        public Node Item1 { get { return item1; } }
        public Node Item2 { get { return item2; } }
    }
}
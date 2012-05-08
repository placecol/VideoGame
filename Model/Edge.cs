using System;
using System.Collections.Generic;

namespace Riddley.VideoGame.Model
{
    public class Edge : Tuple<Node, Node> 
    {
        public Edge(Node item1, Node item2) : base(item1, item2)
        {
        }

        private readonly Dictionary<Type, object> data = new Dictionary<Type, object>();

        public void Add<T>(T obj)
        {
            data[typeof(T)] = obj;
        }

        public T Get<T>()
        {
            return (T)data[typeof(T)];
        }

        public void Remove<T>()
        {
            data.Remove(typeof(T));
        }
    }
}
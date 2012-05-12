using System;
using System.Collections.Generic;

namespace Riddley.VideoGame.Model
{
    public abstract class AbstractNode
    {
        private readonly Dictionary<Type, object> data = new Dictionary<Type, object>();

        public void Add<T>(T obj)
        {
            if (data.ContainsKey(typeof(T)))
                throw new Exception(string.Format("Node already contains an object of type {0}", typeof (T)));

            data[typeof (T)] = obj;
        }

        public T Get<T>()
        {
            return (T) data[typeof (T)];
        }

        public void Remove<T>()
        {
            data.Remove(typeof (T));
        }

        public bool Has<T>()
        {
            return data.ContainsKey(typeof(T));
        }
    }

    public class Node : AbstractNode
    {
        
    }
}
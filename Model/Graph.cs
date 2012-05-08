using System;
using System.Collections.Generic;
using System.Linq;

namespace Riddley.VideoGame.Model
{
    public class Graph
    {
        private readonly Dictionary<Node, IEnumerable<Edge>> adjacencyList;

        public Graph(Dictionary<Node, IEnumerable<Edge>> adjacencyList)
        {
            this.adjacencyList = adjacencyList;
        }

        public IEnumerable<Node> GetNeighbors(Node node, Func<Edge, bool> validEdge = null)
        {
            validEdge = validEdge ?? (edge => true);
            return adjacencyList[node]
                .Where(validEdge)
                .Select(e => e.Item1 != node ? e.Item1 : e.Item2);
        }

        public Node Find(Node start, Func<Node, int, bool> endFound, Func<Edge, bool> validEdge = null, int maxDepth = -1)
        {
            var queue = new Queue<Node>();
            queue.Enqueue(start);
            var visited = new List<Node> {start};
            var distance = 0;

            while (queue.Count > 0 && (maxDepth == -1 || distance <= maxDepth))
            {
                if (endFound(queue.Peek(), distance)) return queue.Dequeue();

                queue.Dequeue();
                distance++;
                var unvisitedNeighbors =
                    GetNeighbors(start, validEdge)
                        .Where(neighbor => !visited.Contains(neighbor));

                foreach (var neighbor in unvisitedNeighbors)
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }

            return null;
        }
    }
}
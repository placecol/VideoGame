using System.Collections.Generic;
using Riddley.VideoGame.Model;
using Riddley.VideoGame.Model.Commands;
using Xunit;

namespace Riddley.VideoGame.Test.Model.Commands
{
    public class PlaceRoadTest
    {
        [Fact]
        public void AddsARoadToAnEdge()
        {
            var node1 = new Node();
            var node2 = new Node();
            var edge = new Edge(node1, node2);
            var context = CreateContext(
                edge,
                new Dictionary<Node, IEnumerable<Edge>>
                    {
                        {node1, new[]{edge}},
                        {node2, new Edge[0]}
                    });

            new PlaceRoad().Execute(context);

            Assert.Equal(context.Player.Roads[0], edge.Get<Road>());
        }

        private CommandContext CreateContext(Edge targetEdge, Dictionary<Node, IEnumerable<Edge>> roadNodes, List<Resource> startingResources = null)
        {
            startingResources = startingResources ?? new List<Resource>
                                                         {
                                                             Resource.Wheat,
                                                             Resource.Brick,
                                                             Resource.Sheep,
                                                             Resource.Wood,
                                                             Resource.Wood
                                                         };

            var player = new Player
            {
                AvailableSettlements = new List<Settlement> { new Settlement() },
                Settlements = new List<Settlement>(),
                Resources = startingResources,
                Roads = new List<Road>()
            };
            var context = new CommandContext
                              {
                                  Game = new Game
                                             {
                                                 Board = new Board
                                                             {
                                                                 RoadGraph = new Graph(roadNodes)
                                                             },
                                                 Players = new List<Player>
                                                               {
                                                                   player
                                                               }
                                             },
                                  Player = player,
                                  TargetEdge = targetEdge
                              };
            return context;
        }
    }

    public class PlaceRoad : ICommand
    {
        public void Execute(CommandContext context)
        {
            var road = new Road();

            context.Player.Roads.Add(road);
            context.TargetEdge.Add(road);
        }
    }
}

using System;
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
            var node3 = new Node();
            var edge1 = new Edge(node1, node2);
            var edge2 = new Edge(node2, node3);
            var road = new Road();
            edge1.Add(road);
            var context = CreateContext(
                edge2,
                new Dictionary<Node, IEnumerable<Edge>>
                    {
                        {node1, new[] {edge1}},
                        {node2, new[] {edge1, edge2}},
                        {node3, new[] {edge2}}
                    });
            context.Player.Roads.Add(road);

            new PlaceRoad().Execute(context);

            Assert.Equal(context.Player.Roads[1], edge2.Get<Road>());
        }

        [Fact]
        public void RoadMustBeAdjacentToPlayerRoad()
        {
            var node1 = new Node();
            var node2 = new Node();
            var node3 = new Node();
            var edge1 = new Edge(node1, node2);
            var edge2 = new Edge(node2, node3);
            var context = CreateContext(
                edge2,
                new Dictionary<Node, IEnumerable<Edge>>
                    {
                        {node1, new[] {edge1}},
                        {node2, new[] {edge1, edge2}},
                        {node3, new[] {edge2}}
                    });

            Assert.Throws<Exception>(() => new PlaceRoad().Execute(context));
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
}

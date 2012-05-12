using System;
using System.Collections.Generic;
using Riddley.VideoGame.Model;
using Riddley.VideoGame.Model.Commands;
using Xunit;

namespace Riddley.VideoGame.Test.Model.Commands
{
    public class PlaceSettlementTest
    {
        [Fact]
        public void AddsASettlementToANode()
        {
            var node = new Node();
            var context = CreateContext(
                node,
                new Dictionary<Node, IEnumerable<Edge>>
                    {
                        {node, new Edge[0]}
                    });

            new PlaceSettlement().Execute(context);

            Assert.Equal(context.Player.Settlements[0], node.Get<Settlement>());
        }

        [Fact]
        public void OnlyOneSettlementCanBeAddedToANode()
        {
            var node = new Node();
            var context = CreateContext(
                node,
                new Dictionary<Node, IEnumerable<Edge>>
                    {
                        {node, new Edge[0]}
                    });

            new PlaceSettlement().Execute(context);

            Assert.Throws<Exception>(() => new PlaceSettlement().Execute(context));
        }

        [Fact]
        public void SettlementMustNotBeImmediatelyAdjacentToAnotherSettlement()
        {
            var nodeWithSettlement = new Node();
            nodeWithSettlement.Add(new Settlement());
            var node = new Node();
            var edge = new Edge(nodeWithSettlement, node);

            var context = CreateContext(
                node,
                new Dictionary<Node, IEnumerable<Edge>>
                    {
                        {node, new[] {edge}},
                        {nodeWithSettlement, new[] {edge}}
                    });

            Assert.Throws<Exception>(() => new PlaceSettlement().Execute(context));
        }

        [Fact]
        public void PlayerSpendsOneEachOfWheatWoodSheepBrick()
        {
            var node = new Node();
            var context = CreateContext(
                node,
                new Dictionary<Node, IEnumerable<Edge>>
                                      {
                                          {node, new Edge[0]}
                                      });

            new PlaceSettlement().Execute(context);

            Assert.Equal(1, context.Player.Resources.Count);
            Assert.Equal(Resource.Wood, context.Player.Resources[0]);
        }

        [Fact]
        public void PlayerMustHaveNecessaryResources()
        {
            var node = new Node();
            var context = CreateContext(
                node,
                new Dictionary<Node, IEnumerable<Edge>>
                    {
                        {node, new Edge[0]}
                    },
                new List<Resource>());

            Assert.Throws<Exception>(() => new PlaceSettlement().Execute(context));
        }

        [Fact]
        public void ReduceNumberOfAvailableSettlements()
        {
            var node = new Node();
            var context = CreateContext(
                node,
                new Dictionary<Node, IEnumerable<Edge>>
                                      {
                                          {node, new Edge[0]}
                                      });

            new PlaceSettlement().Execute(context);

            Assert.Equal(0, context.Player.AvailableSettlements.Count);
        }

        [Fact]
        public void PlayerMustHaveAvailableSettlement()
        {
            var node = new Node();
            var context = CreateContext(
                node,
                new Dictionary<Node, IEnumerable<Edge>>
                    {
                        {node, new Edge[0]}
                    },
                new List<Resource>());

            Assert.Throws<Exception>(() => new PlaceSettlement().Execute(context));
        }

        private CommandContext CreateContext(Node targetNode, Dictionary<Node, IEnumerable<Edge>> roadNodes, List<Resource> startingResources = null)
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
                Resources = startingResources
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
                TargetNode = targetNode
            };
            return context;
        }
    }
}

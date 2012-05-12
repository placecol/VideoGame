using System;
using System.Linq;

namespace Riddley.VideoGame.Model.Commands
{
    public class PlaceRoad : ICommand
    {
        public void Execute(CommandContext context)
        {
            if (!context.Game.Board.RoadGraph
                     .GetNeighbors(context.TargetEdge)
                     .Any(edge => 
                          edge.Has<Road>()
                          && context.Player.Roads.Contains(edge.Get<Road>())))
                throw new Exception("Target road must be adjacent to another road");

            var road = new Road();

            context.Player.Roads.Add(road);
            context.TargetEdge.Add(road);
        }
    }
}
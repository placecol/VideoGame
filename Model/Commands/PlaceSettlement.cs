using System;
using System.Linq;

namespace Riddley.VideoGame.Model.Commands
{
    public class PlaceSettlement: ICommand
    {
        public void Execute(CommandContext context)
        {
            AssertNotAdjacentToSettlement(context);

            var settlement = context.Player.UseAvailableSettlement();

            context.TargetNode.Add(settlement);

            context.Player.Spend(
                Resource.Brick,
                Resource.Wheat,
                Resource.Sheep,
                Resource.Wood);
        }

        private void AssertNotAdjacentToSettlement(CommandContext context)
        {
            if (context.Game.Board.RoadGraph.GetNeighbors(context.TargetNode).Any(n => n.Get<Settlement>() != null))
                throw new Exception("Cannot place settlement immediately next to another settlement");
        }
    }
}

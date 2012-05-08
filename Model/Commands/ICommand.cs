namespace Riddley.VideoGame.Model.Commands
{
    public interface ICommand
    {
        void Execute(CommandContext context);
    }

    public class CommandContext
    {
        public CommandContext()
        {
        }

        public CommandContext(Game game, Node targetNode, Player player)
        {
            Game = game;
            TargetNode = targetNode;
            Player = player;
        }

        public Game Game { get; set; }

        public Node TargetNode { get; set; }

        public Edge TargetEdge { get; set; }

        public Player TargetPlayer { get; set; }

        public Player Player { get; set; }
    }
}
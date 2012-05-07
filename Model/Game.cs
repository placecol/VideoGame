using System.Collections.ObjectModel;

namespace Riddley.VideoGame.Model
{
    public class Game
    {
        public string Id { get; set; }

        public Collection<Player> Players { get; set; }
    }
}

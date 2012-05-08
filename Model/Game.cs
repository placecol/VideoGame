using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Riddley.VideoGame.Model
{
    public class Game
    {
        public string Id { get; set; }

        public List<Player> Players { get; set; }

        public Board Board { get; set; }
    }
}

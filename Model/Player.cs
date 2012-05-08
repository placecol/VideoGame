using System;
using System.Collections.Generic;
using System.Linq;

namespace Riddley.VideoGame.Model
{
    public class Player
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public List<Settlement> Settlements { get; set; }

        public List<Resource> Resources { get; set; }

        public List<Settlement> AvailableSettlements { get; set; }

        public List<Road> Roads { get; set; }

        public void Spend(params Resource [] resources)
        {
            foreach (var resource in resources)
            {
                if (Resources.All(r => r != resource))
                    throw new Exception("Player does not have the necessary resources to place a settlement");
                Resources.Remove(resource);
            }
        }

        public Settlement UseAvailableSettlement()
        {
            var settlement = AvailableSettlements.FirstOrDefault();
            if (settlement == null)
                throw new Exception("Player does not have any available settlements to place");
            AvailableSettlements.Remove(settlement);
            Settlements.Add(settlement);
            return settlement;
        }
    }

    public class Road
    {
    }
}
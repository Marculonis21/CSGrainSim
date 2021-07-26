using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class BronzeMelt : Element
    {
        public BronzeMelt()
        {
            this.ID = ElementID.BRONZEMELT;

            this.name = "Bronze melt";      
            this.nameShort = "MBRNZ";
            this.description = $"{name}: Melted bronze alloy";
            this.color = Color.Orange;

            this.state = 1;           
            this.weight = 991;         
            this.move = true;
            this.spawnTemperature = 2000;

            this.heatTransfer = 26;

            this.lowLevelTemp = 900f;
            this.lowLevelTempTransition = new Reaction(this.ID, 
                                                       new List<ElementID>() {ElementID.BRONZE},
                                                       1);

            DefaultReactions(this);
        }
    }
}

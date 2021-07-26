using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class Bronze : Element
    {
        public Bronze()
        {
            this.ID = ElementID.BRONZE;

            this.name = "Bronze";      
            this.nameShort = "BRONZ";
            this.description = $"{name}: Valuable alloy";
            this.color = Color.Peru;

            this.state = 0;           
            this.weight = 999;         
            this.move = false;
            this.spawnTemperature = 20;

            this.heatTransfer = 26;

            this.highLevelTemp = 960;//913
            this.highLevelTempTransition = new Reaction(this.ID, 
                                                        new List<ElementID>() {ElementID.BRONZEMELT},
                                                        1);

            DefaultReactions(this);
        }
    }
}

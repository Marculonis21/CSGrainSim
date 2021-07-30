using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim
{
    class SaltIce : Element
    {
        public SaltIce()
        {
            this.ID = ElementID.SALTICE;

            this.name = "SALTICE";      
            this.nameShort = "SICE";
            this.description = $"{name}: frozen saltwater";
            this.color = Color.AliceBlue;

            this.state = 0;           
            this.weight = 999;         
            this.move = false;
            this.spawnTemperature = -60;

            this.heatTransfer = 2.5f;

            this.highLevelTemp = -4;
            this.highLevelTempTransition = new Reaction(this.ID,
                                                        new List<ElementID>() {ElementID.SALTWATER},
                                                        0.5f); 

            DefaultReactions(this);
        }
    }
}

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class Ice : Element
    {
        public Ice()
        {
            this.ID = ElementID.ICE;

            this.name = "Ice";      
            this.nameShort = "ICE";
            this.description = $"{name}: frozen water";
            this.color = Color.LightBlue;

            this.state = 0;           
            this.weight = 999;         
            this.move = false;
            this.spawnTemperature = -50;

            this.heatTransfer = 2.5f;

            this.highLevelTemp = 0;
            this.highLevelTempTransition = new Reaction(this.ID,
                                                        new List<ElementID>() {ElementID.WATER},
                                                        0.5f); 

            DefaultReactions(this);
        }
    }
}

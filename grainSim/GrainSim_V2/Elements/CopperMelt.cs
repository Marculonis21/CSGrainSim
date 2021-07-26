using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class CopperMelt : Element
    {
        public CopperMelt()
        {
            this.ID = ElementID.COPPERMELT;

            this.name = "Copper Melt";      
            this.nameShort = "MCOPR";
            this.description = $"{name}: Melted copper";
            this.color = new Color(235, 167, 70);

            this.state = 1;           
            this.weight = 990;         
            this.move = true;
            this.spawnTemperature = 2000f;

            this.flameable = 0;
            this.explosive = 0;   

            this.heatTransfer = 400;

            this.lowLevelTemp = 1070f;
            this.lowLevelTempTransition = new Reaction(this.ID, 
                                                       new List<ElementID>() {ElementID.COPPER},
                                                       1);
            
            this.reactions.Add(new Reaction(this.id,
                                            new List<ElementID>() {ElementID.BRONZEMELT},
                                            ElementID.TINMELT,
                                            1,
                                            0.03f,
                                            true));

            DefaultReactions(this);
        }
    }
}

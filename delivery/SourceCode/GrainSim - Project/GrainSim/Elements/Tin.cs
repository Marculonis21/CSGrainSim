using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim
{
    class Tin : Element
    {
        public Tin()
        {
            this.ID = ElementID.TIN;

            this.name = "Tin";      
            this.nameShort = "TIN";
            this.description = $"{name}: Silvery meltable metal";
            this.color = Color.Silver;

            this.state = 0;           
            this.weight = 999;         
            this.move = false;
            this.spawnTemperature = 20;

            this.heatTransfer = 75;

            this.highLevelTemp = 240;
            this.highLevelTempTransition = new Reaction(this.ID, 
                                                        new List<ElementID>() {ElementID.TINMELT},
                                                        1);

            DefaultReactions(this);
        }
    }
}

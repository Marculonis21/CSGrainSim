using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class TinMelt : Element
    {
        public TinMelt()
        {
            this.ID = ElementID.TINMELT;

            this.name = "Tin Melt";      
            this.nameShort = "MTIN";
            this.description = $"{name}: Melted tin";
            this.color = Color.LightYellow;

            this.state = 1;           
            this.weight = 989;         
            this.move = true;
            this.spawnTemperature = 400f;

            this.flameable = 0;
            this.explosive = 0;   

            this.heatTransfer = 75;

            this.lowLevelTemp = 230;
            this.lowLevelTempTransition = new Reaction(this.ID, 
                                                       new List<ElementID>() {ElementID.TIN},
                                                       1);

            DefaultReactions(this);
        }
    }
}

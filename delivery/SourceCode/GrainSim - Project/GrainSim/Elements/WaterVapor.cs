using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim
{
    class WaterVapor : Element
    {
        public WaterVapor()
        {
            this.ID = ElementID.WATERVAPOR;

            this.name = "Water Vapor";      
            this.nameShort = "WATRV";
            this.description = $"{name}: Produced by boiling water";
            this.color = Color.SkyBlue;

            this.state = 2;           
            this.weight = 0;         
            this.move = true;
            this.spawnTemperature = 150;

            this.flameable = 0;
            this.explosive = 0;   

            this.heatTransfer = 1;

            this.lowLevelTemp = 95;
            this.lowLevelTempTransition = new Reaction(this.ID,
                                                       new List<ElementID>() {ElementID.WATER},
                                                       0.5f); 

            DefaultReactions(this);
        }
    }
}

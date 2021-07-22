using Microsoft.Xna.Framework;
/* using Microsoft.Xna.Framework.Graphics; */
/* using Microsoft.Xna.Framework.Input; */

namespace GrainSim_v2
{
    class Water : Element
    {
        public Water()
        {
            this.ID = ElementID.WATER;

            this.name = "Water";      
            this.nameShort = "WATER";
            this.description = $"{name}: Pure liquid";
            this.color = Color.DeepSkyBlue;

            this.state = 1;           
            this.weight = 1;         
            this.move = true;
            this.spawnTemperature = 20;

            this.flameable = 0;
            this.explosive = 0;   

            this.heatTransfer = 1;

            this.lowLevelTemp = 0;
            this.lowLevelTempTransition = new Reaction(this.ID,
                                                       ElementID.ICE,
                                                       0.9f); 
            this.highLevelTemp = 100;
            this.highLevelTempTransition = new Reaction(this.ID,
                                                        ElementID.WATERVAPOR,
                                                        0.9f); 
        }
    }
}

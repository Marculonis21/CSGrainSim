using Microsoft.Xna.Framework;
/* using Microsoft.Xna.Framework.Graphics; */
/* using Microsoft.Xna.Framework.Input; */

namespace GrainSim_v2
{
    public class Water : Element
    {
        public Water()
        {
            this.ID = ElementID.WATER;

            this.name = "Water";      
            this.nameShort = "Watr";
            this.color = Color.DeepSkyBlue;

            this.state = 1;           
            this.weight = 1;         
            this.move = true;
            this.spawnTemperature = 15;

            this.flameable = 0;
            this.explosive = 0;   

            this.heatTransfer = 5;

            this.lowLevelTemp = 0;
            this.lowLevelTempTransition = new Reaction(this.ID,
                                                       ElementID.ICE,
                                                       1); 
            this.highLevelTemp = 100;
            this.highLevelTempTransition = new Reaction(this.ID,
                                                        ElementID.WATERVAPOR,
                                                        1); 
        }
    }
}

using Microsoft.Xna.Framework;
/* using Microsoft.Xna.Framework.Graphics; */
/* using Microsoft.Xna.Framework.Input; */

namespace grainSim
{
    public class Water : Element
    {
        public Water()
        {
            this.ID = ElementID.WATER;

            this.name = "Water";      
            this.nameShort = "Watr";
            this.color = Color.Cyan;

            this.state = 1;           
            this.gravity = 2;        
            this.weight = 1;         
            this.spawnTemperature = 15;

            this.flameable = 0;
            this.explosive = 0;   

            this.heatTransfer = 5;

            this.LowLevelTemp = 0;
            this.LowLevelTempTransition = ElementID.ICE;
            this.HighLevelTemp = 100;
            this.HighLevelTempTransition = ElementID.WATERVAPOR;
        }
    }
}

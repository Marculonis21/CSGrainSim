using Microsoft.Xna.Framework;
/* using Microsoft.Xna.Framework.Graphics; */
/* using Microsoft.Xna.Framework.Input; */

namespace TestProject
{
    public class Ice : Element
    {
        public Ice()
        {
            this.ID = ElementID.ICE;

            this.name = "Ice";      
            this.nameShort = "Ice";
            this.color = Color.LightBlue;

            this.state = 0;           
            this.gravity = 0;        
            this.weight = 100;         
            this.spawnTemperature = -10;

            this.flameable = 0;
            this.explosive = 0;   

            this.heatTransfer = 5;

            /* this.LowLevelTemp = 0; */
            /* this.LowLevelTempTransition = ElementID.ICE; */
            this.HighLevelTemp = 0.5f;
            this.HighLevelTempTransition = ElementID.WATER;
        }
    }
}

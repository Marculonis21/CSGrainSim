/* using Microsoft.Xna.Framework; */
/* using Microsoft.Xna.Framework.Graphics; */
/* using Microsoft.Xna.Framework.Input; */

namespace grainSim
{
    public class Air : Element
    {
        public Air()
        {
            this.ID = ElementID.AIR;

            this.name = "Air";      
            this.nameShort = "Air";
            /* this.color = null; */

            this.state = 2;           
            this.weight = 0;         
            this.spawnTemperature = 0;

            this.flameable = 0;
            this.explosive = 0;   

            this.heatTransfer = 10;
        }
    }
}

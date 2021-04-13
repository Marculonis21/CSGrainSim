using Microsoft.Xna.Framework;
/* using Microsoft.Xna.Framework.Graphics; */
/* using Microsoft.Xna.Framework.Input; */

namespace grainSim
{
    public class Sand : Element
    {
        public Sand()
        {
            this.ID = ElementID.SAND;

            this.name = "Sand";      
            this.nameShort = "Sand";
            this.color = Color.Yellow;

            this.state = 0;           
            this.gravity = 0.05f;        
            this.weight = 10;         
            this.spawnTemperature = 30;

            this.flameable = 0;
            this.explosive = 0;   

            this.heatTransfer = 10;

            /* float LowLevelTemp; */
            /* Element LowLevelTempTransition; */
            /* float HighLevelTemp; */
            /* Element HighLevelTempTransition; */
        }
    }
}

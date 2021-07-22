using Microsoft.Xna.Framework;
/* using Microsoft.Xna.Framework.Graphics; */
/* using Microsoft.Xna.Framework.Input; */

namespace GrainSim_v2
{
    class Ice : Element
    {
        public Ice()
        {
            this.ID = ElementID.ICE;

            this.name = "Ice";      
            this.nameShort = "ICE";
            this.color = Color.LightBlue;

            this.state = 0;           
            this.weight = 999;         
            this.move = false;
            this.spawnTemperature = -60;

            this.flameable = 0;
            this.explosive = 0;   

            this.heatTransfer = 5;

            this.highLevelTemp = 0.5f;
            this.highLevelTempTransition = new Reaction(this.ID, 
                                                        ElementID.WATER,
                                                        0.9f);
        }
    }
}

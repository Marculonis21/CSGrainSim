using Microsoft.Xna.Framework;
/* using Microsoft.Xna.Framework.Graphics; */
/* using Microsoft.Xna.Framework.Input; */

namespace grainSim
{
    public class Wall : Element
    {
        public Wall ()
        {
            this.ID = ElementID.WALL;

            this.name = "Wall";      
            this.nameShort = "Wall";
            this.color = Color.Gray;

            this.state = 0;           
            this.weight = 999;         
            this.move = false;
            this.spawnTemperature = 20;

            this.flameable = 0;
            this.explosive = 0;   
        }
    }
}
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
            this.weight = 10;         
            this.move = true;
            this.spawnTemperature = 30;

            this.flameable = 0;
            this.explosive = 0;   

            this.heatTransfer = 10;

            reactions.Add(new Reaction(this.ID, ElementID.WATER, ElementID.WATER, 1, 0.001f));
        }
    }
}

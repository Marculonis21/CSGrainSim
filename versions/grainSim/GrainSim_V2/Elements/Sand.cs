using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class Sand : Element
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
            /* this.spawnTemperature = 20; */

            this.flameable = 0;
            this.explosive = 0;   

            this.heatTransfer = 10;

            /* reactions.Add(new Reaction(this.ID, ElementID.WATER, ElementID.WATER, 1, 0.001f)); */
        }
    }
}

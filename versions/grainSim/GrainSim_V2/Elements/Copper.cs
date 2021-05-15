using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class Copper : Element
    {
        public Copper()
        {
            this.ID = ElementID.COPPER;

            this.name = "Copper";      
            this.nameShort = "COPR";
            this.color = Color.Peru;

            this.state = 0;           
            this.weight = 999;         
            this.move = false;
            this.spawnTemperature = 20;

            this.flameable = 0;
            this.explosive = 0;   

            this.heatTransfer = 400;

            this.highLevelTemp = 1100f;
            this.highLevelTempTransition = new Reaction(this.ID, ElementID.COPPERMELT, 1);
            /* reactions.Add(new Reaction(this.ID, ElementID.WATER, ElementID.WATER, 1, 0.001f)); */
        }
    }
}

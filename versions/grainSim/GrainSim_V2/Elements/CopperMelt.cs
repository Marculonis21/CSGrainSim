using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class CopperMelt : Element
    {
        public CopperMelt()
        {
            this.ID = ElementID.COPPERMELT;

            this.name = "Copper Melt";      
            this.nameShort = "MCOPR";
            this.description = $"{name}: Melted copper";
            this.color = Color.Orange;

            this.state = 1;           
            this.weight = 998;         
            this.move = true;
            this.spawnTemperature = 2000f;

            this.flameable = 0;
            this.explosive = 0;   

            this.heatTransfer = 400;

            this.lowLevelTemp = 1070f;
            this.lowLevelTempTransition = new Reaction(this.ID, ElementID.COPPER, 1);
        }
    }
}

using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class Smoke : Element
    {
        public Smoke()
        {
            this.ID = ElementID.SMOKE;

            this.name = "Fire smoke";      
            this.nameShort = "Smok";
            this.color = Color.DimGray;

            this.state = 2;           
            this.weight = 0;         
            this.move = true;
            this.spawnTemperature = 20;

            this.heatTransfer = 0.01f;

            this.maxLifeTime = 300;
            this.endOfLifeTransition = new Reaction(this.ID, 
                                                    ElementID.VOID, 
                                                    0.05f);
        }
    }
}

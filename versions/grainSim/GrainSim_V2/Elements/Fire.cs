using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class Fire : Element
    {
        public Fire()
        {
            this.ID = ElementID.FIRE;

            this.name = "Fire particles";      
            this.nameShort = "Fire";
            this.color = Color.OrangeRed;

            this.state = 2;           
            this.weight = 0;         
            this.move = true;
            this.spawnTemperature = 500;

            this.heatTransfer = 1;

            this.maxLifeTime = 50;
            this.endOfLifeTransition = new Reaction(this.ID, 
                                                    ElementID.SMOKE, 
                                                    0.5f);
        }
    }
}

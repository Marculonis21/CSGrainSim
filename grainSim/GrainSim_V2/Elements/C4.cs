using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class C4 : Element
    {
        public C4()
        {
            this.ID = ElementID.C4;

            this.name = "C4";      
            this.nameShort = "C-4";
            this.description = $"{name}: well known explosive";
            this.color = Color.LightPink;

            this.state = 0;           
            this.weight = 999;         
            this.move = false;
            this.spawnTemperature = 20;

            this.explosive = 0.8f;
            this.explosivePower = 50;

            this.burnSpeed = 2;
            this.burnElement = ElementID.C4BURN;

            this.heatTransfer = 1;

            DefaultReactions(this);
        }
    }
}

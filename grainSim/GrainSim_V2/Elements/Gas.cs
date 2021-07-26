using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class Gas : Element
    {
        public Gas()
        {
            this.ID = ElementID.GAS;

            this.name = "Gas";      
            this.nameShort = "GAS";
            this.description = $"{name}: flameable gas";
            this.color = Color.Gold;

            this.state = 2;           
            this.weight = 0.1f;         
            this.move = true;
            this.spawnTemperature = 20;

            this.explosive = 0.9f;
            this.explosivePower = 20;
            this.burnSpeed = 2;
            this.burnElement = ElementID.GASBURN;

            this.heatTransfer = 1;

            this.maxLifeTime = 150;
            this.endOfLifeTransition = new Reaction(this.id,
                                                    new List<ElementID>() {ElementID.VOID},
                                                    0.005f);

            DefaultReactions(this);
        }
    }
}

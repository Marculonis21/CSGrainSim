using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim
{
    class Smoke : Element
    {
        public Smoke()
        {
            this.ID = ElementID.SMOKE;

            this.name = "Fire smoke";      
            this.nameShort = "SMOKE";
            this.description = $"{name}: particles produced by fire";
            this.color = Color.DimGray;

            this.state = 2;           
            this.weight = 0;         
            this.move = true;
            this.spawnTemperature = 30;

            this.heatTransfer = 0.01f;

            this.maxLifeTime = 300;
            this.endOfLifeTransition = new Reaction(this.ID, 
                                                    new List<ElementID>() {ElementID.VOID}, 
                                                    0.008f);

            DefaultReactions(this);
        }
    }
}

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim
{
    class Fire : Element
    {
        public Fire()
        {
            this.ID = ElementID.FIRE;

            this.name = "Fire";      
            this.nameShort = "FIRE";
            this.description = $"Oh man that's {name}!";
            this.color = Color.OrangeRed;

            this.state = 2;           
            this.weight = 0;         
            this.move = true;
            this.spawnTemperature = 700;

            this.heatTransfer = 1;

            this.maxLifeTime = 40;
            this.endOfLifeTransition = new Reaction(this.ID, 
                                                    new List<ElementID>() {ElementID.SMOKE}, 
                                                    0.05f);

            this.reactions.Add(new Reaction(this.ID,
                                            new List<ElementID>() {ElementID.VOID},
                                            ElementID.SMOKE,
                                            8,
                                            0.25f));

            this.reactions.Add(new Reaction(this.ID,
                                            new List<ElementID>() {ElementID.FIRE, ElementID.SMOKE},
                                            0.005f));

            DefaultReactions(this);
        }
    }
}

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class WoodBurn : Element
    {
        public WoodBurn()
        {
            this.uiExclude = true;
            Element orig = elements[ElementID.WOOD];

            this.ID = ElementID.WOODBURN;

            this.color = Color.SandyBrown;

            this.state = orig.State;           
            this.weight = orig.Weight;         
            this.move = orig.Move;

            this.heatTransfer = orig.HeatTrans;

            this.maxLifeTime = orig.BurnSpeed;
            this.endOfLifeTransition = new Reaction(this.id,
                                                    new List<ElementID>() {ElementID.FIRE}, 
                                                    0.5f);

            this.reactions.Add(new Reaction(this.id,
                                            new List<ElementID>() {ElementID.WOODBURN, ElementID.FIRE}, 
                                            0.01f));

            DefaultReactions(this);
        }
    }
}

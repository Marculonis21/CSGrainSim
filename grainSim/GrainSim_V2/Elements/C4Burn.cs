using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class C4Burn : Element
    {
        public C4Burn()
        {
            this.uiExclude = true;
            Element orig = elements[ElementID.C4];

            this.ID = ElementID.C4BURN;

            this.color = Color.Pink;

            this.state = orig.State;           
            this.weight = orig.Weight;         
            this.move = orig.Move;

            this.heatTransfer = orig.HeatTrans;

            this.explosivePower = orig.ExplosivePwr;
            this.maxLifeTime = orig.BurnSpeed;

            this.endOfLifeTransition = new Reaction(this.id,
                                                    new List<ElementID>() {ElementID.EXPLOSION}, 
                                                    1f);

            DefaultReactions(this);
        }
    }
}

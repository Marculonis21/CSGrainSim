using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class GasBurn : Element
    {
        public GasBurn()
        {
            this.uiExclude = true;
            Element orig = elements[ElementID.GAS];

            this.ID = ElementID.GASBURN;

            this.color = Color.Gold;

            this.state = orig.State;           
            this.weight = orig.Weight;         
            this.move = orig.Move;

            this.heatTransfer = orig.HeatTrans;

            this.explosivePower = orig.ExplosivePwr;
            this.maxLifeTime = orig.BurnSpeed;

            this.endOfLifeTransition = new Reaction(this.id, 
                                                    new List<ElementID>() {ElementID.EXPLOSION}, 
                                                    0.9f);

            DefaultReactions(this);
        }
    }
}

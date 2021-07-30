using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim
{
    class GunpowderBurn : Element
    {
        public GunpowderBurn()
        {
            this.uiExclude = true;
            Element orig = elements[ElementID.GUNPOWDER];

            this.ID = ElementID.GUNPOWDERBURN;

            this.color = Color.Gray;

            this.state = orig.State;           
            this.weight = orig.Weight;         
            this.move = orig.Move;

            this.heatTransfer = orig.HeatTrans;

            this.explosivePower = orig.ExplosivePwr; 

            this.maxLifeTime = orig.BurnSpeed;
            this.endOfLifeTransition = new Reaction(this.id, 
                                                    new List<ElementID>() {ElementID.EXPLOSION}, 
                                                    0.8f);

            DefaultReactions(this);
        }
    }
}

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim
{
    class PlantBurn : Element
    {
        public PlantBurn()
        {
            this.uiExclude = true;
            Element orig = elements[ElementID.PLANT];

            this.ID = ElementID.PLANTBURN;

            this.color = Color.DarkGreen;

            this.state = orig.State;           
            this.weight = orig.Weight;         
            this.move = orig.Move;

            this.heatTransfer = orig.HeatTrans;

            this.maxLifeTime = orig.BurnSpeed;
            this.endOfLifeTransition = new Reaction(this.id,
                                                    new List<ElementID>() {ElementID.FIRE}, 
                                                    0.6f);

            this.reactions.Add(new Reaction(this.id,
                                            new List<ElementID>() {ElementID.PLANTBURN, ElementID.FIRE}, 
                                            0.02f));

            DefaultReactions(this);
        }
    }
}

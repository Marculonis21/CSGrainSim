using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class OilBurn : Element
    {
        public OilBurn()
        {
            this.uiExclude = true;
            Element orig = elements[ElementID.OIL];

            this.ID = ElementID.OILBURN;

            this.color = Color.Goldenrod;

            this.state = orig.State;           
            this.weight = orig.Weight;         
            this.move = orig.Move;

            this.heatTransfer = orig.HeatTrans;

            this.maxLifeTime = orig.BurnSpeed;
            this.endOfLifeTransition = new Reaction(this.id, ElementID.FIRE, 0.8f);

            DefaultReactions(this);
        }
    }
}
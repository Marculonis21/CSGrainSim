using Microsoft.Xna.Framework;

namespace GrainSim
{
    class Hot : Element
    {
        public Hot()
        {
            this.ID = ElementID.HOT;

            this.name = "Hot";      
            this.nameShort = "HOT";
            this.description = $"Makes stuff warmer";
            this.color = Color.DarkRed;
        }
    }
}

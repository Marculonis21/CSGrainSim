using Microsoft.Xna.Framework;

namespace GrainSim
{
    class Cold : Element
    {
        public Cold()
        {
            this.ID = ElementID.COLD;

            this.name = "Cold";      
            this.nameShort = "COLD";
            this.description = $"You will be much cooler";
            this.color = Color.SkyBlue;
        }
    }
}

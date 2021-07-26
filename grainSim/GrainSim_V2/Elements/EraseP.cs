using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class EraseP : Element
    {
        public EraseP()
        {
            this.ID = ElementID.ERASEP;

            this.name = "EraseP";      
            this.nameShort = "DELP";
            this.description = $"Erase particles, not walls";
            this.color = Color.Red;
        }
    }
}

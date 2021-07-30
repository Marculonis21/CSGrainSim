using Microsoft.Xna.Framework;

namespace GrainSim
{
    class Erase : Element
    {
        public Erase()
        {
            this.ID = ElementID.ERASE;

            this.name = "Erase";      
            this.nameShort = "DEL";
            this.description = $"Erase anything";
            this.color = Color.Red;
        }
    }
}

using Microsoft.Xna.Framework;

namespace GrainSim
{
    class Salt : Element
    {
        public Salt()
        {
            this.ID = ElementID.SALT;

            this.name = "Salt";      
            this.nameShort = "SALT";
            this.description = $"{name}: salt particles";
            this.color = Color.LightGray;

            this.state = 0;           
            this.weight = 10;         
            this.move = true;
            this.spawnTemperature = 20;
            this.heatTransfer = 5;

            this.destroyedByMolten = true;

            DefaultReactions(this);
        }
    }
}

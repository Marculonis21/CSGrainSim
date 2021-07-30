using Microsoft.Xna.Framework;

namespace GrainSim
{
    class Wood : Element
    {
        public Wood()
        {
            this.ID = ElementID.WOOD;

            this.name = "Wood";      
            this.nameShort = "WOOD";
            this.description = $"{name}: solid chunk of fiery food";
            this.color = Color.SaddleBrown;

            this.state = 0;           
            this.weight = 999;         
            this.move = false;
            this.spawnTemperature = 20;

            this.flameable = 0.05f;
            this.burnSpeed = 30;
            this.burnElement = ElementID.WOODBURN;

            this.heatTransfer = 1;

            DefaultReactions(this);
        }
    }
}

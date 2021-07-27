using Microsoft.Xna.Framework;

namespace GrainSim
{
    class Plant : Element
    {
        public Plant()
        {
            this.ID = ElementID.PLANT;

            this.name = "Plant";      
            this.nameShort = "PLANT";
            this.description = $"{name}: it's green and alive";
            this.color = Color.Green;

            this.state = 0;           
            this.weight = 999;         
            this.move = false;
            this.spawnTemperature = 20;

            this.flameable = 0.25f;
            this.burnSpeed = 15;
            this.burnElement = ElementID.PLANTBURN;

            this.heatTransfer = 1;

            DefaultReactions(this);
        }
    }
}

using Microsoft.Xna.Framework;

namespace GrainSim
{
    class Oil : Element
    {
        public Oil()
        {
            this.ID = ElementID.OIL;

            this.name = "Oil";      
            this.nameShort = "OIL";
            this.description = $"{name}: dirty flameable liquid";
            this.color = Color.DarkGoldenrod;

            this.state = 1;           
            this.weight = 0.9f;         
            this.move = true;
            this.spawnTemperature = 20;

            this.flameable = 0.8f;
            this.burnSpeed = 5;
            this.burnElement = ElementID.OILBURN;

            this.heatTransfer = 1;

            DefaultReactions(this);
        }
    }
}

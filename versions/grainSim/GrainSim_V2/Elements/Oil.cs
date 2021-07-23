using Microsoft.Xna.Framework;

namespace GrainSim_v2
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

            this.flameable = 0.75f;

            this.heatTransfer = 1;

            DefaultReactions(this);
        }
    }
}

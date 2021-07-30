using Microsoft.Xna.Framework;

namespace GrainSim
{
    class Gunpowder : Element
    {
        public Gunpowder()
        {
            this.ID = ElementID.GUNPOWDER;

            this.name = "Gunpowder";      
            this.nameShort = "GUNPD";
            this.description = $"{name}: explosive powdery substance";
            this.color = Color.DarkGray;

            this.state = 0;           
            this.weight = 10;         
            this.move = true;
            this.spawnTemperature = 20;

            this.explosive = 0.2f;
            this.explosivePower = 20;
            this.burnElement = ElementID.GUNPOWDERBURN;
            this.burnSpeed = 5;

            this.heatTransfer = 5;

            DefaultReactions(this);
        }
    }
}

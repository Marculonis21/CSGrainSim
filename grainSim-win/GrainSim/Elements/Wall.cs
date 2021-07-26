using Microsoft.Xna.Framework;

namespace GrainSim
{
    class Wall : Element
    {
        public Wall ()
        {
            this.ID = ElementID.WALL;

            this.name = "Wall";      
            this.nameShort = "WALL";
            this.description = $"{name}: Indestructible material";
            this.color = Color.Gray;

            this.state = 0;           
            this.weight = 999;         
            this.move = false;
            this.spawnTemperature = 20;

            this.flameable = 0;
            this.explosive = 0;   
            
            this.heatTransfer = 0;
        }
    }
}

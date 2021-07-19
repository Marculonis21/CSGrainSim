using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class WaterVapor : Element
    {
        public WaterVapor()
        {
            this.ID = ElementID.WATERVAPOR;

            this.name = "Water Vapor";      
            this.nameShort = "V-Watr";
            this.color = Color.SkyBlue;

            this.state = 2;           
            this.weight = 1;         
            this.move = true;
            this.spawnTemperature = 120;

            this.flameable = 0;
            this.explosive = 0;   

            this.heatTransfer = 1;

            this.lowLevelTemp = 95;
            this.lowLevelTempTransition = new Reaction(this.ID,
                                                       ElementID.WATER,
                                                       0.9f); 
        }
    }
}

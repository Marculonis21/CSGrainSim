using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class Sand : Element
    {
        public Sand()
        {
            this.ID = ElementID.SAND;

            this.name = "Sand";      
            this.nameShort = "SAND";
            this.description = $"{name}: small solid particles";
            this.color = Color.Yellow;

            this.state = 0;           
            this.weight = 10;         
            this.move = true;
            this.spawnTemperature = 20;

            this.destroyedByMolten = true;

            this.heatTransfer = 10;

            DefaultReactions(this);
        }
    }
}

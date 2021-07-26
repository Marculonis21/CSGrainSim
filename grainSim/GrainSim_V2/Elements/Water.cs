using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class Water : Element
    {
        public Water()
        {
            this.ID = ElementID.WATER;

            this.name = "Water";      
            this.nameShort = "WATER";
            this.description = $"{name}: Pure liquid";
            this.color = Color.DeepSkyBlue;

            this.state = 1;           
            this.weight = 1;         
            this.move = true;
            this.spawnTemperature = 20;

            this.flameable = 0;
            this.explosive = 0;   

            this.heatTransfer = 0.6f;

            this.lowLevelTemp = 0;
            this.lowLevelTempTransition = new Reaction(this.ID,
                                                       new List<ElementID>() {ElementID.ICE},
                                                       0.5f); 
            this.highLevelTemp = 100;
            this.highLevelTempTransition = new Reaction(this.ID,
                                                        new List<ElementID>() {ElementID.WATERVAPOR},
                                                        0.5f); 

            this.reactions.Add(new Reaction(this.ID,
                                            new List<ElementID>() {ElementID.SALTWATER},
                                            ElementID.SALT,
                                            1,
                                            0.03f,
                                            true));

            DefaultReactions(this);
        }
    }
}

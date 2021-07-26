using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class SaltWater : Element
    {
        public SaltWater()
        {
            this.ID = ElementID.SALTWATER;

            this.name = "Salt Water";      
            this.nameShort = "SWATR";
            this.description = $"{name}: Water with salt dissolved in it";
            this.color = Color.SkyBlue;

            this.state = 1;           
            this.weight = 1.01f; // fresh water is less dense than see water         
            this.move = true;
            this.spawnTemperature = 20;

            this.heatTransfer = 1;

            this.lowLevelTemp = -5;
            this.lowLevelTempTransition = new Reaction(this.ID,
                                                       new List<ElementID>() {ElementID.ICE},
                                                       0.5f); 
            this.highLevelTemp = 104;
            this.highLevelTempTransition = new Reaction(this.ID,
                                                        new List<ElementID>() {ElementID.WATERVAPOR},
                                                        0.5f); 

            this.reactions.Add(new Reaction(this.ID,
                                            new List<ElementID>() {ElementID.SALT, ElementID.WATERVAPOR},
                                            ElementID.WATERVAPOR,
                                            1,
                                            0.2f));

            DefaultReactions(this);
        }
    }
}

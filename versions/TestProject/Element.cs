using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestProject
{
    public enum ElementID
    {
        AIR,
        WATER,
        WATERVAPOR,
        OIL,
        SAND,
        DUST,
        ICE,
    }

    public class Element
    {
        /// <summary>
        /// Element class is the basic template for all elements added down the
        /// line.
        /// </summary>
        
        protected ElementID ID;

        protected string name;            // full name
        protected string nameShort;       // short abbreviated name
        protected Color color;            // element color

        protected int state;              // 0 - solid, particles; 1 - liquids; 2 - gasses
        protected float gravity;          // speed of fall/rise
        protected float weight;           // relative weight - heavier sinks
        protected float spawnTemperature; // start(spawn) temperature

        protected float flameable;        // burn 0 no / else how fast
        protected float explosive;        // explode no/yes

        protected float heatTransfer;     // heat transfer speed

        // TRANSITIONS
        protected float LowLevelTemp;
        protected ElementID LowLevelTempTransition;
        protected float HighLevelTemp;
        protected ElementID HighLevelTempTransition;

        /* public static Element Type(int id) */ 
        /* { */
        /*     return ElementID.GetValues(typeof(ElementID))[id]; */
        /* } */

        public string Name  { get{ return this.name; }      }
        public string Short { get{ return this.nameShort; } }
        public Color Color  { get{ return this.color; }     }

        public int State     { get{ return this.state; }   }
        public float Gravity { get{ return this.gravity; } }
        public float Weight  { get{ return this.weight; }  }
        public float STemp   { get{ return this.spawnTemperature; }  }

        public float Flamable  { get{ return this.flameable;    } }
        public float Explosive { get{ return this.explosive;    } }
        public float HeatTrans { get{ return this.heatTransfer; } }

        public List<int[]> AllowedMovement(int x, int y)
        {
            List<int[]> list = new List<int[]>();

            if(state == 0) // solids
            {
                for (int _y = 1; _y < 2; _y++)
                    for (int _x = -1; _x < 2; _x++)
                    {
                        if(Block.Type(x+_x,y+_y) == ElementID.AIR) list.Add(new int[] {x,y});
                    }
            }
            else if(state == 1) // liquids
            {
                for (int _y = 0; _y < 2; _y++)
                    for (int _x = -1; _x < 2; _x++)
                    {
                        if(Block.Type(x+_x,y+_y) == ElementID.AIR) list.Add(new int[] {x,y});
                    }
            }
            else if(state == 2) // gas
            {
                for (int _y = 0; _y > -2; _y--)
                    for (int _x = -1; _x < 2; _x++)
                    {
                        if(Block.Type(x+_x,y+_y) == ElementID.AIR) list.Add(new int[] {x,y});
                    }
            }
            return list;
        }
    }
}

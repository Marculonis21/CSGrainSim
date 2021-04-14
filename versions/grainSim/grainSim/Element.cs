using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace grainSim
{
    public enum ElementID
    {
        AIR,
        DUST,
        FIRE,
        ICE,
        OIL,
        SAND,
        SMOKE,
        VOID,
        WALL,
        WATER,
        WATERVAPOR,
    }

    public partial class Element
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
        protected float weight;           // relative weight - heavier sinks
        protected bool move;              // if it should be stationary
        protected float spawnTemperature; // start(spawn) temperature

        protected float flameable;        // burn 0 no / else how fast
        protected float explosive;        // explode no/yes

        protected float heatTransfer;     // heat transfer speed

        // TRANSITIONS
        protected List<Reaction> reactions = new List<Reaction>();  
        
        protected float lowLevelTemp;
        protected Reaction lowLevelTempTransition;
        protected float highLevelTemp;
        protected Reaction highLevelTempTransition;

        protected int maxLifeTime;
        protected int lifeTime;
        protected Reaction EndOfLifeTransition;

        public static ElementID Type(int x, int y)
        {
            try 
            {
                return MainGame.particleMap[x,y];
            }
            catch(System.IndexOutOfRangeException e) 
            {
                /* Console.WriteLine("Out of bounds test"); */
                return ElementID.VOID;
            }
        }

        public Vector2 PositionUpdate(int x, int y)
        {
            Vector2 currentPos = new Vector2(x,y);

            if(!this.move) return currentPos;

            List<Vector2> possiblePos = new List<Vector2>();
            int bounds = MainGame.particleMap.GetLength(0);

            if(state == 0) // solids
            {
                if((y+1 < bounds && Element.Type(x,y+1) == ElementID.AIR) ||
                   (y+1 < bounds && Element.elements[Element.Type(x,y+1)].weight < this.weight)) // heavier sinks
                    return new Vector2(x,y+1);

                for (int _y = 1; _y < 2; _y++)
                    for (int _x = -1; _x < 2; _x++)
                    {
                        if(x+_x >= 0 && x+_x < bounds &&
                           y+_y >= 0 && y+_y < bounds)
                        {
                            if(Element.Type(x+_x,y+_y) == ElementID.AIR) 
                                possiblePos.Add(new Vector2(x+_x,y+_y));
                        }
                    }
            }
            else if(state == 1) // LIQUID
            {
                if((y+1 < bounds && Element.Type(x,y+1) == ElementID.AIR) ||
                   (y+1 < bounds && Element.elements[Element.Type(x,y+1)].weight < this.weight)) // heavier sinks
                    return new Vector2(x,y+1);

                for (int _y = 0; _y < 2; _y++)
                    for (int _x = -1; _x < 2; _x++)
                    {
                        if(x+_x >= 0 && x+_x < bounds &&
                           y+_y >= 0 && y+_y < bounds)
                        {
                            if(Element.Type(x+_x,y+_y) == ElementID.AIR) 
                                possiblePos.Add(new Vector2(x+_x,y+_y));
                        }
                    }
            }
            else if(state == 2) // GAS
            {
                if((y-1 < bounds && Element.Type(x,y-1) == ElementID.AIR) ||
                   (y-1 < bounds && Element.elements[Element.Type(x,y-1)].weight > this.weight)) // lighter sinks
                    return new Vector2(x,y-1);

                for (int _y = 0; _y > -2; _y--)
                    for (int _x = -1; _x < 2; _x++)
                    {
                        if(x+_x >= 0 && x+_x < bounds &&
                           y+_y >= 0 && y+_y < bounds)
                        {
                            if(Element.Type(x+_x,y+_y) == ElementID.AIR) 
                                possiblePos.Add(new Vector2(x+_x,y+_y));
                        }
                    }
            }

            if(possiblePos.Count != 0)
                return possiblePos[MainGame.random.Next(0,possiblePos.Count)];
            else
                return currentPos;
        }

        // Vars
        public ElementID id    { get{ return this.ID; } }

        public string Name  { get{ return this.name;      } }
        public string Short { get{ return this.nameShort; } }
        public Color Color  { get{ return this.color;     } }

        public int State     { get{ return this.state;  }  }
        public float Weight  { get{ return this.weight; }  }
        public float STemp   { get{ return this.spawnTemperature; }  }

        public float Flamable  { get{ return this.flameable;    } }
        public float Explosive { get{ return this.explosive;    } }
        public float HeatTrans { get{ return this.heatTransfer; } }
    }
}

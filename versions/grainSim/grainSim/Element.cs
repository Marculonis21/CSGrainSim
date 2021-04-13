using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace grainSim
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
        VOID
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

        public static ElementID Type(int x, int y)
        {
            try 
            {
                return MainGame.particleMap[x,y];
            }
            catch(System.IndexOutOfRangeException e) 
            {
                Console.WriteLine("Out of bounds test");
                return ElementID.VOID;
            }

        }

        public Vector2 Update(Vector2 position)
        {
            int x = (int)position.X;
            int y = (int)position.Y;
            List<Vector2> possiblePos;
            Random random;

            int bounds = MainGame.particleMap.GetLength(0);

            if(state == 0) // solids
            {
                if(y+1 < bounds && Element.Type(x,y+1) == ElementID.AIR)
                    return new Vector2(x,y+1);

                possiblePos = new List<Vector2>();
                random = new Random();
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

                if(possiblePos.Count != 0)
                    return possiblePos[random.Next(0,possiblePos.Count)];
                else
                    return position;
            }
            else if(state == 1) // liquids
            {
                if(y+1 < bounds && Element.Type(x,y+1) == ElementID.AIR)
                {
                    return new Vector2(x,y+1);
                }

                possiblePos = new List<Vector2>();
                random = new Random();
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

                return possiblePos[random.Next(0,possiblePos.Count)];
            }
            else if(state == 2) // gas
            {
                if(y-1 >= 0 && Element.Type(x,y-1) == ElementID.AIR)
                {
                    return new Vector2(x,y+1);
                }

                possiblePos = new List<Vector2>();
                random = new Random();
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

                return possiblePos[random.Next(0,possiblePos.Count)];
            }

            return new Vector2();
        }

        // Vars
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
    }
}

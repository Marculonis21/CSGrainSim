using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
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
        protected Reaction EndOfLifeTransition;


        public Vector2 UpdatePosition(Vector2 position, ParticleMap partMap)
        {
            Vector2 currentPos = position;

            if(!this.move) return currentPos;

            List<Vector2> possiblePos = new List<Vector2>();
            int bounds = MainGame.bounds;

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
                            if((Element.Type(x+_x,y+_y) == ElementID.AIR) || 
                               (Element.elements[Element.Type(x+_x,y+_y)].weight < this.weight))
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
                            if((Element.Type(x+_x,y+_y) == ElementID.AIR) || 
                               (Element.elements[Element.Type(x+_x,y+_y)].weight < this.weight))
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
                            if((Element.Type(x+_x,y+_y) == ElementID.AIR) || 
                               (Element.elements[Element.Type(x+_x,y+_y)].weight > this.weight))
                                possiblePos.Add(new Vector2(x+_x,y+_y));
                        }
                    }
            }

            if(possiblePos.Count != 0)
                return possiblePos[MainGame.random.Next(0,possiblePos.Count)];
            else
                return currentPos;
        }

        public ElementID ReactionUpdate(int x, int y, int lifeTime)
        {
            foreach (Reaction r in this.reactions)
            {
                if(r.Eval(x,y, out ElementID result))
                {
                    return result;
                }
            }

            /* if(this.temp <= lowLevelTemp) */
            /* { */
            /*     lowLevelTempTransition.Eval(x,y, out ElementID result); */
            /*     return result; */
            /* } */
            /* if(this.temp >= highLevelTemp) */
            /* { */
            /*     highLevelTempTransition.Eval(x,y, out ElementID result); */
            /*     return result; */
            /* } */

            if(lifeTime > this.maxLifeTime)
            {
                EndOfLifeTransition.Eval(x,y, out ElementID result);
                return result;
            }

            return this.ID;
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

        public int MaxLifeTime { get{ return this.maxLifeTime; } }
    }
}

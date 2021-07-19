using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    partial class Element
    {
        /// <summary>
        /// Element class is the basic template for all elements added down the
        /// line.
        ///
        /// Thermal Conductivity source:
        /// https://www.engineeringtoolbox.com/thermal-conductivity-d_429.html
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
        protected Reaction endOfLifeTransition;

        public Point UpdatePosition(Point pos, ParticleMap partMap)
        {
            if(!this.move) return pos;

            List<Point> possiblePos = new List<Point>();

            if(state == 0) // solids
            {
                if((partMap.InBounds(new Point(pos.X,pos.Y+1)) && partMap.Type(new Point(pos.X,pos.Y+1)) == ElementID.AIR) ||
                   (partMap.InBounds(new Point(pos.X,pos.Y+1)) && Element.elements[partMap.Type(new Point(pos.X,pos.Y+1))].weight < this.weight)) // heavier sinks
                    return new Point(pos.X,pos.Y+1);

                for (int _y = 1; _y < 2; _y++)
                    for (int _x = -1; _x < 2; _x++)
                    {
                        if(partMap.InBounds(new Point(pos.X+_x,pos.Y+_y)))
                        {
                            if((partMap.Type(new Point(pos.X+_x,pos.Y+_y)) == ElementID.AIR) || 
                               (Element.elements[partMap.Type(new Point(pos.X+_x,pos.Y+_y))].weight < this.weight))
                                possiblePos.Add(new Point(pos.X+_x,pos.Y+_y));
                        }
                    }
            }
            else if(state == 1) // LIQUID
            {
                if((partMap.InBounds(new Point(pos.X,pos.Y+1)) && partMap.Type(new Point(pos.X,pos.Y+1)) == ElementID.AIR) ||
                   (partMap.InBounds(new Point(pos.X,pos.Y+1)) && Element.elements[partMap.Type(new Point(pos.X,pos.Y+1))].weight < this.weight)) // heavier sinks
                    return new Point(pos.X,pos.Y+1);

                for (int _y = 0; _y < 2; _y++)
                    for (int _x = -1; _x < 2; _x++)
                    {
                        if(partMap.InBounds(new Point(pos.X+_x,pos.Y+_y)))
                        {
                            if((partMap.Type(new Point(pos.X+_x,pos.Y+_y)) == ElementID.AIR) || 
                               (Element.elements[partMap.Type(new Point(pos.X+_x,pos.Y+_y))].weight < this.weight))
                                possiblePos.Add(new Point(pos.X+_x,pos.Y+_y));
                        }
                    }
            }
            else if(state == 2) // GAS
            {
                if((partMap.InBounds(new Point(pos.X,pos.Y-1)) && partMap.Type(new Point(pos.X,pos.Y-1)) == ElementID.AIR) ||
                   (partMap.InBounds(new Point(pos.X,pos.Y-1)) && Element.elements[partMap.Type(new Point(pos.X,pos.Y-1))].weight < this.weight)) // lighter sinks
                    return new Point(pos.X,pos.Y-1);

                for (int _y = 0; _y > -2; _y--)
                    for (int _x = -1; _x < 2; _x++)
                    {
                        if(partMap.InBounds(new Point(pos.X+_x,pos.Y+_y)))
                        {
                            if((partMap.Type(new Point(pos.X+_x,pos.Y+_y)) == ElementID.AIR) || 
                               (Element.elements[partMap.Type(new Point(pos.X+_x,pos.Y+_y))].weight < this.weight))
                                possiblePos.Add(new Point(pos.X+_x,pos.Y+_y));
                        }
                    }
            }

            if(possiblePos.Count != 0)
                return possiblePos[MainGame.random.Next(0,possiblePos.Count)];
            else
                return pos;
        }

        public ElementID UpdateReaction(Point pos, int lifeTime, ParticleMap partMap, TemperatureMap tempMap)
        {
            foreach (Reaction r in this.reactions)
            {
                if(r.Eval(pos, partMap, out ElementID result))
                {
                    return result;
                }
            }

            if(lowLevelTempTransition != null && tempMap.Get(pos) <= lowLevelTemp)
            {
                lowLevelTempTransition.Eval(pos, partMap, out ElementID result);
                tempMap.Set(pos, 0 , tempMap.Get(pos)*1.01f);

                return result;
            }
            if(highLevelTempTransition != null && tempMap.Get(pos) >= highLevelTemp)
            {
                highLevelTempTransition.Eval(pos, partMap, out ElementID result);
                tempMap.Set(pos, 0 , tempMap.Get(pos)*0.99f);
                return result;
            }

            if(endOfLifeTransition != null && lifeTime > this.maxLifeTime)
            {
                endOfLifeTransition.Eval(pos, partMap, out ElementID result);
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

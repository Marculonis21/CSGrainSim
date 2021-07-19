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

        // if decay to nothing -> react to ElementID.VOID (particle deleted)
        protected int maxLifeTime;
        protected Reaction endOfLifeTransition;

        public Point UpdatePosition(Point pos, ParticleMap partMap)
        {
            if(!this.move) return pos;

            List<Point> possiblePos = new List<Point>();

            if(state == 0) // solids
            {
                if((partMap.Type(new Point(pos.X,pos.Y+1)) == ElementID.AIR) ||
                   (Element.elements[partMap.Type(new Point(pos.X,pos.Y+1))].weight < this.weight)) // heavier sinks
                {
                    if(MainGame.random.NextDouble() <= 0.9f) // random sideways movement
                        return new Point(pos.X,pos.Y+1);
                    else
                    {
                        Point l = new Point(pos.X-1, pos.Y+1); 
                        l = partMap.Type(l) == ElementID.AIR ? l : new Point(-1, -1); 
                        Point r = new Point(pos.X+1, pos.Y+1);
                        r = partMap.Type(r) == ElementID.AIR ? r : new Point(-1, -1); 

                        if(l.X == -1 && r.X == -1)
                            return new Point(pos.X,pos.Y+1);
                        else if(l.X != -1 && r.X != -1)
                        {
                            if(MainGame.random.NextDouble() <= 0.5f)
                                return l;
                            else
                                return r;
                        }
                        else if(l.X != -1)
                            return l;
                        else
                            return r;
                    }
                }

                for (int _y = 1; _y < 2; _y++)
                    for (int _x = -1; _x < 2; _x++)
                        if((partMap.Type(new Point(pos.X+_x,pos.Y+_y)) == ElementID.AIR) || 
                           (Element.elements[partMap.Type(new Point(pos.X+_x,pos.Y+_y))].weight < this.weight))
                        {
                            possiblePos.Add(new Point(pos.X+_x,pos.Y+_y));
                        }

            }
            else if(state == 1) // LIQUID
            {
                if((partMap.Type(new Point(pos.X,pos.Y+1)) == ElementID.AIR) ||
                   (Element.elements[partMap.Type(new Point(pos.X,pos.Y+1))].weight < this.weight)) // heavier sinks
                {
                    if(MainGame.random.NextDouble() <= 0.9f) // random sideways movement
                        return new Point(pos.X,pos.Y+1);
                    else
                    {
                        Point l = new Point(pos.X-1, pos.Y+1); 
                        l = partMap.Type(l) == ElementID.AIR ? l : new Point(-1, -1); 
                        Point r = new Point(pos.X+1, pos.Y+1);
                        r = partMap.Type(r) == ElementID.AIR ? r : new Point(-1, -1); 

                        if(l.X == -1 && r.X == -1)
                            return new Point(pos.X,pos.Y+1);
                        else if(l.X != -1 && r.X != -1)
                        {
                            if(MainGame.random.NextDouble() <= 0.5f)
                                return l;
                            else
                                return r;
                        }
                        else if(l.X != -1)
                            return l;
                        else
                            return r;
                    }
                }

                for (int _y = 0; _y < 2; _y++)
                    for (int _x = -1; _x < 2; _x++)
                        if((partMap.Type(new Point(pos.X+_x,pos.Y+_y)) == ElementID.AIR) || 
                           (Element.elements[partMap.Type(new Point(pos.X+_x,pos.Y+_y))].weight < this.weight))
                        {
                            possiblePos.Add(new Point(pos.X+_x,pos.Y+_y));
                        }

            }
            else if(state == 2) // GAS
            {
                if((partMap.Type(new Point(pos.X,pos.Y-1)) == ElementID.AIR) ||
                   (Element.elements[partMap.Type(new Point(pos.X,pos.Y-1))].weight < this.weight)) // lighter sinks
                {

                    if(MainGame.random.NextDouble() <= 0.8f) // random sideways movement
                        return new Point(pos.X,pos.Y-1);
                    else
                    {
                        Point l = new Point(pos.X-1, pos.Y-1); 
                        l = partMap.Type(l) == ElementID.AIR ? l : new Point(-1, -1); 
                        Point r = new Point(pos.X+1, pos.Y-1);
                        r = partMap.Type(r) == ElementID.AIR ? r : new Point(-1, -1); 

                        if(l.X == -1 && r.X == -1)
                            return new Point(pos.X,pos.Y-1);
                        else if(l.X != -1 && r.X != -1)
                        {
                            if(MainGame.random.NextDouble() <= 0.5f)
                                return l;
                            else
                                return r;
                        }
                        else if(l.X != -1)
                            return l;
                        else
                            return r;
                    }
                }

                for (int _y = 0; _y > -2; _y--)
                    for (int _x = -1; _x < 2; _x++)
                        if((partMap.Type(new Point(pos.X+_x,pos.Y+_y)) == ElementID.AIR) || 
                           (Element.elements[partMap.Type(new Point(pos.X+_x,pos.Y+_y))].weight < this.weight))
                        {
                            possiblePos.Add(new Point(pos.X+_x,pos.Y+_y));
                        }

            }

            if(possiblePos.Count != 0) // choose random from possible positions
                return possiblePos[MainGame.random.Next(0,possiblePos.Count)];
            else
                return pos;
        }

        public ElementID UpdateReaction(Point pos, int lifeTime, ParticleMap partMap, TemperatureMap tempMap)
        {
            foreach (Reaction r in this.reactions) // evaluate all possible reactions
            {
                if(r.Eval(pos, partMap, out ElementID result))
                {
                    return result;
                }
            }

            // evaluate preset transitional reactons
            if(lowLevelTempTransition != null && tempMap.Get(pos) <= lowLevelTemp)
            {
                if(lowLevelTempTransition.Eval(pos, partMap, out ElementID result))
                {
                    tempMap.Set(pos, 0, tempMap.Get(pos)*1.05f);
                    return result;
                }
            }
            if(highLevelTempTransition != null && tempMap.Get(pos) >= highLevelTemp)
            {
                if(highLevelTempTransition.Eval(pos, partMap, out ElementID result))
                {
                    tempMap.Set(pos, 0, tempMap.Get(pos)*0.95f);
                    return result;
                }
            }

            if(endOfLifeTransition != null && lifeTime > this.maxLifeTime)
            {
                if(endOfLifeTransition.Eval(pos, partMap, out ElementID result))
                    return result;
            }

            return this.ID;
        }

        // Public get vars
        public ElementID id    { get{ return this.ID; } }

        public string Name     { get{ return this.name;      } }
        public string Short    { get{ return this.nameShort; } }
        public Color Color     { get{ return this.color;     } }

        public int State       { get{ return this.state;  }  }
        public float Weight    { get{ return this.weight; }  }
        public float STemp     { get{ return this.spawnTemperature; }  }

        public float Flamable  { get{ return this.flameable;    } }
        public float Explosive { get{ return this.explosive;    } }
        public float HeatTrans { get{ return this.heatTransfer; } }

        public int MaxLifeTime { get{ return this.maxLifeTime; } }
        
    }
}

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
        protected string description;     // short description (few words PLS)
        protected Color color;            // element color

        protected int state;              // 0 - solid, particles; 1 - liquids; 2 - gasses
        protected float weight;           // relative weight - heavier sinks
        protected bool move;              // if it should be stationary
        protected float spawnTemperature; // start(spawn) temperature

        protected float flameable;        // chance to produce fire when touched by fire (0.0-1.0) 
        protected int burnSpeed;          // the time needed to burn the particle 
        protected float explosive;        // chance to explode (0.0-1.0)
        protected int explosivePower;     // the size of the flame ball produced
        protected ElementID burnElement;  // to which element should the burn transfer

        protected bool destroyedByMolten; // destroyed on contact with molten stuff

        protected float heatTransfer;     // heat transfer speed

        protected bool uiExclude;         // if it shouldn't be visible in UI for select

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

            Point INVALID = new Point(-1, -1);
            List<Point> possiblePos = new List<Point>();

            if(state == 0) // solids
            {
                Point DOWN = new Point(pos.X, pos.Y+1);
                ElementID typeDOWN = partMap.Type(DOWN);

                if((typeDOWN == ElementID.AIR) ||
                   (Element.elements[typeDOWN].weight < this.weight)) // heavier sinks
                {
                    if(MainGame.random.NextDouble() <= 0.9f) // random sideways movement
                        return DOWN;
                    else
                    {
                        Point DOWNLEFT = new Point(pos.X-1, pos.Y+1); 
                        DOWNLEFT = (partMap.Type(DOWNLEFT) == ElementID.AIR) ? DOWNLEFT : INVALID; 

                        Point DOWNRIGHT = new Point(pos.X+1, pos.Y+1);
                        DOWNRIGHT = (partMap.Type(DOWNRIGHT) == ElementID.AIR) ? DOWNRIGHT : INVALID; 

                        if(DOWNLEFT == INVALID && DOWNRIGHT == INVALID)
                            return DOWN;
                        else if(DOWNLEFT != INVALID && DOWNRIGHT != INVALID)
                        {
                            if(MainGame.random.NextDouble() <= 0.5f)
                                return DOWNLEFT;
                            else
                                return DOWNRIGHT;
                        }
                        else if(DOWNLEFT != INVALID)
                            return DOWNLEFT;
                        else
                            return DOWNRIGHT;
                    }
                }

                Point testPos = new Point();
                ElementID typeTestPos;
                for (int _y = 1; _y < 2; _y++)
                    for (int _x = -1; _x < 2; _x++)
                    {
                        testPos.X = pos.X+_x;
                        testPos.Y = pos.Y+_y;
                        typeTestPos = partMap.Type(testPos);

                        if((typeTestPos == ElementID.AIR) || 
                           (Element.elements[typeTestPos].weight < this.weight))
                            possiblePos.Add(testPos);
                    }

            }
            else if(state == 1) // LIQUID
            {
                Point DOWN = new Point(pos.X, pos.Y+1);
                ElementID typeDOWN = partMap.Type(DOWN);

                if((typeDOWN == ElementID.AIR) ||
                   (Element.elements[typeDOWN].weight < this.weight)) // heavier sinks
                {
                    if(MainGame.random.NextDouble() <= 0.9f) // random sideways movement
                        return DOWN;
                    else
                    {
                        Point DOWNLEFT = new Point(pos.X-1, pos.Y+1); 
                        DOWNLEFT = (partMap.Type(DOWNLEFT) == ElementID.AIR) ? DOWNLEFT : INVALID; 

                        Point DOWNRIGHT = new Point(pos.X+1, pos.Y+1);
                        DOWNRIGHT = (partMap.Type(DOWNRIGHT) == ElementID.AIR) ? DOWNRIGHT : INVALID; 

                        if(DOWNLEFT == INVALID && DOWNRIGHT == INVALID)
                            return DOWN;
                        else if(DOWNLEFT != INVALID && DOWNRIGHT != INVALID)
                        {
                            if(MainGame.random.NextDouble() <= 0.5f)
                                return DOWNLEFT;
                            else
                                return DOWNRIGHT;
                        }
                        else if(DOWNLEFT != INVALID)
                            return DOWNLEFT;
                        else
                            return DOWNRIGHT;
                    }
                }

                Point testPos = new Point();
                ElementID typeTestPos;
                for (int _y = 0; _y < 2; _y++)
                    for (int _x = -1; _x < 2; _x++)
                    {
                        testPos.X = pos.X+_x;
                        testPos.Y = pos.Y+_y;
                        typeTestPos = partMap.Type(testPos);

                        if((typeTestPos == ElementID.AIR) || 
                           (Element.elements[typeTestPos].weight < this.weight))
                            possiblePos.Add(testPos);
                    }

            }
            else if(state == 2) // GAS
            {
                Point UP = new Point(pos.X, pos.Y-1);
                ElementID typeUP = partMap.Type(UP);

                if((typeUP == ElementID.AIR) ||
                   (Element.elements[typeUP].weight < this.weight)) // lighter sinks
                {

                    if(MainGame.random.NextDouble() <= 0.8f) // random sideways movement
                        return UP;
                    else
                    {
                        Point UPLEFT = new Point(pos.X-1, pos.Y-1); 
                        UPLEFT = (partMap.Type(UPLEFT) == ElementID.AIR) ? UPLEFT : INVALID; 

                        Point UPRIGHT = new Point(pos.X+1, pos.Y-1);
                        UPRIGHT = (partMap.Type(UPRIGHT) == ElementID.AIR) ? UPRIGHT : INVALID;

                        if(UPLEFT == INVALID && UPRIGHT == INVALID)
                            return UP;
                        else if(UPLEFT != INVALID && UPRIGHT != INVALID)
                        {
                            if(MainGame.random.NextDouble() <= 0.5f)
                                return UPLEFT;
                            else
                                return UPRIGHT;
                        }
                        else if(UPLEFT != INVALID)
                            return UPLEFT;
                        else
                            return UPRIGHT;
                    }
                }

                Point testPos = new Point();
                ElementID typeTestPos;
                for (int _y = 0; _y > -2; _y--)
                    for (int _x = -1; _x < 2; _x++)
                    {
                        testPos.X = pos.X+_x;
                        testPos.Y = pos.Y+_y;
                        typeTestPos = partMap.Type(testPos);

                        if((typeTestPos == ElementID.AIR) || 
                           (Element.elements[typeTestPos].weight < this.weight))
                        {
                            possiblePos.Add(testPos);
                        }
                    }
            }

            if(possiblePos.Count != 0) // choose random from possible positions
                return possiblePos[MainGame.random.Next(0,possiblePos.Count)];
            else
                return pos;
        }

        public ElementID UpdateReaction(Point pos, int lifeTime, bool stable, ParticleMap partMap, TemperatureMap tempMap)
        {
            if(!stable)
            {
                foreach (Reaction r in this.reactions) // evaluate all possible reactions
                {
                    if(r.Eval(pos, partMap, out List<ElementID> result, out Point destroy))
                    {
                        //spawn other results around this particle
                        for(int i = 1; i < result.Count; i++)
                        {
                            if(partMap.Type(new Point(pos.X, pos.Y-1)) == ElementID.AIR)
                                partMap.SpawnLater(result[i], new Point(pos.X, pos.Y-1), 1);

                            else if(partMap.Type(new Point(pos.X, pos.Y+1)) == ElementID.AIR)
                                partMap.SpawnLater(result[i], new Point(pos.X, pos.Y+1), 1);

                            else if(partMap.Type(new Point(pos.X-1, pos.Y)) == ElementID.AIR)
                                partMap.SpawnLater(result[i], new Point(pos.X-1, pos.Y), 1);

                            else if(partMap.Type(new Point(pos.X+1, pos.Y)) == ElementID.AIR)
                                partMap.SpawnLater(result[i], new Point(pos.X+1, pos.Y), 1);
                        }

                        if(partMap.InBounds(destroy))
                            partMap.DeleteLater(destroy, 0);

                        return result[0];
                    }
                }
            }

            // evaluate preset transitional reactons
            if(lowLevelTempTransition != null && tempMap.Get(pos) <= lowLevelTemp)
            {
                if(lowLevelTempTransition.Eval(pos, partMap, out List<ElementID> result, out Point destroy))
                {
                    tempMap.Set(pos, 0, tempMap.Get(pos)*1.05f);
                    return result[0];
                }
            }
            if(highLevelTempTransition != null && tempMap.Get(pos) >= highLevelTemp)
            {
                if(highLevelTempTransition.Eval(pos, partMap, out List<ElementID> result, out Point destroy))
                {
                    tempMap.Set(pos, 0, tempMap.Get(pos)*0.95f);
                    return result[0];
                }
            }

            if(endOfLifeTransition != null && lifeTime > this.maxLifeTime)
            {
                if(endOfLifeTransition.Eval(pos, partMap, out List<ElementID> result, out Point destroy))
                    return result[0];
            }

            return this.ID;
        }

        // Public get vars
        public ElementID id          { get{ return this.ID; } }

        public string Name           { get{ return this.name;        } }
        public string Short          { get{ return this.nameShort;   } }
        public string Description    { get{ return this.description; } }
        public Color Color           { get{ return this.color;       } }

        public int State             { get{ return this.state;  }  }
        public bool Move             { get{ return this.move;  }  }
        public float Weight          { get{ return this.weight; }  }
        public float STemp           { get{ return this.spawnTemperature; }  }

        public float Flamable        { get{ return this.flameable;      } }
        public int BurnSpeed         { get{ return this.burnSpeed;      } }
        public float Explosive       { get{ return this.explosive;      } }
        public int ExplosivePwr      { get{ return this.explosivePower; } }
        public ElementID BurnElement { get{ return this.burnElement; } }

        public float HeatTrans       { get{ return this.heatTransfer;   } }

        public int MaxLifeTime       { get{ return this.maxLifeTime; } }

        public bool UIExclude        { get{ return this.uiExclude; } }
    }
}

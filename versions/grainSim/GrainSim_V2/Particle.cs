using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GrainSim_v2
{
    class Particle
    {
        ElementID ID;
        Point pos;
        int lifeTime;
        bool alive;

        bool stable;
        int unstableTimeout;
        int stableTime;

        public Particle(ElementID ID, Point position)
        {
            this.ID = ID;
            this.pos = position;
            this.stable = false;
            this.unstableTimeout = 0;
            this.stableTime = 0;

            this.lifeTime = 0;

            this.alive = true;
        }

        public void Update(ParticleMap partMap, TemperatureMap tempMap)
        {
            if(!alive) return;

            UpdateReaction(partMap, tempMap);

            if(!stable)
                UpdatePosition(partMap);
            else
                stableTime++;
            

            if(Element.elements[ID].MaxLifeTime > 0) lifeTime++;
        }

        public ElementID Type()
        {
            return this.ID;
        }

        public void Render(Shapes shapes, int particleSize)
        {
            shapes.DrawRectangle(new Point(pos.X*particleSize,
                                           pos.Y*particleSize),
                                 particleSize,particleSize, 
                                 Element.elements[ID].Color);
        }

        public Point GetPosition()
        {
            return this.pos;
        }

        public void Kill() // kill set by delete from other source
        {
            this.alive = false;
        }

        public void SetPosition(Point position)
        {
            this.pos = position;
        }

        public void SetStable(bool state)
        {
            this.stable = state;
        }

        void UpdatePosition(ParticleMap partMap)
        {
            Point result = Element.elements[ID].UpdatePosition(pos, partMap);

            if(result != this.pos)
            {
                partMap.UnstableSurroundingParticles(this.pos);
                partMap.Swap(this.pos, result);
            }
            else
            {
                if(unstableTimeout < 50) // wait for a while before making it stable
                    unstableTimeout++;
                else
                {
                    SetStable(true);
                    this.stableTime = 0;
                }
            }
        }

        void UpdateReaction(ParticleMap partMap, TemperatureMap tempMap)
        {
            bool pass;
            if(!stable)
                pass = stable;
            else
            {
                // even if stable check for reactions regularly (tanks performance)
                pass = !(stableTime % 10 == 0);
            }
            ElementID result = Element.elements[ID].UpdateReaction(pos, lifeTime, pass, partMap, tempMap);

            if(result != this.ID)
            {
                SetStable(false);
                partMap.UnstableSurroundingParticles(this.pos);

                if(result == ElementID.VOID) // void == deleted
                {
                    partMap.DeleteLater(this.pos, 0);
                    SetStable(true); //dont update pos after deleting
                }
                else if(result == ElementID.EXPLOSION)
                {
                    partMap.DeleteLater(this.pos, 0);
                    partMap.SpawnLater(ElementID.FIRE, this.pos, Element.elements[ID].ExplosivePwr);
                    SetStable(true); //dont update pos after deleting
                }
                else
                {
                    this.ID = result;
                    this.lifeTime = 0;
                }
            }
        }
    }
}

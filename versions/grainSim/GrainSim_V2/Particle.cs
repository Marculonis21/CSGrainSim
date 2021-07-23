using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GrainSim_v2
{
    class Particle
    {
        ElementID ID;
        Point pos;
        int lifeTime;

        int stableTimout;
        bool stable;

        public Particle(ElementID ID, Point position)
        {
            this.ID = ID;
            this.pos = position;
            this.stable = false;
            this.stableTimout = 0;

            this.lifeTime = 0;
        }

        public void Update(ParticleMap partMap, TemperatureMap tempMap)
        {
            UpdateReaction(partMap, tempMap);
            if(!stable)
                UpdatePosition(partMap);

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
                stableTimout = 0;
            }
            else
                SetStable(true);
        }

        void UpdateReaction(ParticleMap partMap, TemperatureMap tempMap)
        {
            ElementID result = Element.elements[ID].UpdateReaction(pos, lifeTime, partMap, tempMap);

            if(result != this.ID)
            {
                SetStable(false);
                partMap.UnstableSurroundingParticles(this.pos);

                if(result == ElementID.VOID) // void == deleted
                {
                    partMap.Delete(this.pos);
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

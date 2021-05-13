using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GrainSim_v2
{
    class Particle
    {
        ElementID ID;
        Point pos;
        int lifeTime;

        public Particle(ElementID ID, Point position)
        {
            this.ID = ID;
            this.pos = position;

            this.lifeTime = 0;
        }

        public void Update(ParticleMap partMap, TemperatureMap tempMap)
        {
            UpdatePosition(partMap);
            UpdateReaction();

            if(Element.elements[ID].MaxLifeTime > 0) lifeTime++;
        }

        void UpdatePosition(ParticleMap partMap)
        {
            /* Element.elements[ID].UpdatePosition(pos, partMap); */
        }

        void UpdateReaction()
        {
            /* ElementID result = Element.elements[ID].UpdateReaction(pos, lifeTime); */
            /* this.ID = result; */
            /* this.lifeTime = 0; */
        }

        public ElementID Type()
        {
            return this.ID;
        }

        public void Draw(Shapes shapes, int particleSize)
        {
            shapes.DrawRectangle(new Point(pos.X*particleSize,
                                           pos.Y*particleSize),
                                 particleSize,particleSize, 
                                 Element.elements[ID].Color);
        }
    }
}

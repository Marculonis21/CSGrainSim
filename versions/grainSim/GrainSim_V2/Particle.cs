using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GrainSim_v2
{
    public class Particle
    {
        ElementID ID;

        Vector2 pos;

        int lifeTime;

        public Particle(ElementID ID, Vector2 position)
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
            Element.elements[ID].UpdatePosition(pos, partMap);
        }

        void UpdateReaction()
        {
            ElementID result = Element.elements[ID].UpdateReaction(pos, lifeTime);
            this.ID = result;
            this.lifeTime = 0;
        }

        /* public void Draw(Shapes shapes, int particleSize) */
        /* { */
        /*     shapes.DrawRectangle(x*particleSize, */
        /*                          y*particleSize, */
        /*                          particleSize,particleSize, */ 
        /*                          Element.elements[ID].Color); */
        /* } */
    }
}

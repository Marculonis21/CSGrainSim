using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class ParticleMap
    {
        ElementID[,] map;
        List<Particle> particles = new List<Particle>();

        int width;
        int height;

        public ParticleMap(int width, int height)
        {
            this.width = width;
            this.height = height;

            this.map = new ElementID[width,height];
        }

        public void Spawn(ElementID element, Vector2 position, int burst=1, float prob=1.0f)
        {
            /* map[x,y] = element; */
            /* particles.Add(new Particle(element, x, y)); */
        }
        
        void Swap(Vector2 position1, Vector2 position2)
        {
            /* // swap particles */
            /* Particle p1 = particles.Where(p => p.x == x1 && p.y == y1).First(); */
            /* Particle p2 = particles.Where(p => p.x == x2 && p.y == y2).First(); */

            /* // swap in map */
            /* ElementID swap = map[x1,y1]; */
            /* map[x1,y1] = map[x2,y2]; */
            /* map[x2,y2] = swap; */
        }

        public ElementID Type(Vector2 position)
        {
            if(InBounds(position.ToPoint))
            {
                int x = (int)position.X;
                int y = (int)position.X;
                return map[(int)position.X, (int)position.Y];
            }
            else
                return ElementID.WALL;
        }

        bool InBounds(Vector2 position)
        {
            return (x >= 0 && x < width) && (y >= 0 && y < height);
        }
    }
}

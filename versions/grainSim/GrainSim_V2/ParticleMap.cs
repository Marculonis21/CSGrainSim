using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class ParticleMap
    {
        GameMap gameMap;

        Particle[,] map;
        List<Particle> particles = new List<Particle>();

        int width;
        int height;

        public ParticleMap(GameMap gameMap, int width, int height)
        {
            this.gameMap = gameMap;

            this.width = width;
            this.height = height;

            this.map = new Particle[width,height];
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    map[x,y] = new Particle(ElementID.AIR, new Point(x,y));
        }

        public void Update()
        {
            foreach(Particle p in particles)
                p.Update(this.gameMap.GetParticleMap(), this.gameMap.GetTemperatureMap());
        }

        public void Spawn(ElementID element, Point position, int burst=1, float prob=1.0f)
        {
            if (!InBounds(position)) return;

            if (Type(position) == ElementID.AIR)
            {
                Particle p = new Particle(element, position);
                particles.Add(p);
                map[position.X,position.Y] = p;
            }
        }
        
        void Swap(Point position1, Point position2)
        {
            /* // swap particles */
            /* Particle p1 = particles.Where(p => p.x == x1 && p.y == y1).First(); */
            /* Particle p2 = particles.Where(p => p.x == x2 && p.y == y2).First(); */

            /* // swap in map */
            /* ElementID swap = map[x1,y1]; */
            /* map[x1,y1] = map[x2,y2]; */
            /* map[x2,y2] = swap; */
        }

        public ElementID Type(Point position)
        {
            if(InBounds(position))
                return map[position.X, position.Y].Type();
            else
                return ElementID.WALL;
        }

        public Particle GetParticle(Point position)
        {
            if(InBounds(position))
                return map[position.X, position.Y];

            return null;
        }

        bool InBounds(Point position)
        {
            return (position.X >= 0 && position.X < width) && (position.Y >= 0 && position.Y < height);
        }
    }
}

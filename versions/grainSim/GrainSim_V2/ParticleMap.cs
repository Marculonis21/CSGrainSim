using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class ParticleMap
    {
        GameMap gameMap;

        Particle[,] map;
        List<Particle> particles = new List<Particle>();
        List<Particle> toDelete = new List<Particle>();
        
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
            foreach(Particle d in toDelete)
            {  
                particles.Remove(d);
            }
            toDelete = new List<Particle>();

            Console.WriteLine("hey");
            foreach(Particle p in particles)
                p.Update(this.gameMap.GetParticleMap(), this.gameMap.GetTemperatureMap());
        }

        public void Render(Shapes shapes, int particleSize)
        {
            foreach(Particle p in particles)
                p.Render(shapes, particleSize);

            Console.WriteLine(particles.Count);
        }

        public void Spawn(ElementID element, Point position, int size = 1)
        {
            if (!InBounds(position)) return;

            if(size == 0)
            {
                if (Type(position) == ElementID.AIR)
                {
                    if(!Element.elements.ContainsKey(element))
                        throw new Exception("Element: " + element + " not introduced in the elements dictionary yet.\nTry adding to ElementsSetup first.\n");

                    Particle p = new Particle(element, position);
                    particles.Add(p);
                    map[position.X,position.Y] = p;
                }
            }
            else
            {
                double angle = (2*Math.PI)/72;

                for (int s = size; s >= 0; s--)
                {
                    for (int i = 0; i < 72; i++)
                    {
                        int _x = position.X + (int)(Math.Cos(angle*i)*s);
                        int _y = position.Y + (int)(Math.Sin(angle*i)*s);

                        Point _position = new Point(_x, _y);

                        if (Type(_position) == ElementID.AIR)
                        {
                            if(!Element.elements.ContainsKey(element))
                                throw new Exception("Element: " + element + " not introduced in the elements dictionary yet.\nTry adding to ElementsSetup first.\n");

                            Particle p = new Particle(element, _position);
                            particles.Add(p);
                            map[_position.X, _position.Y] = p;
                        }
                    }
                }
            }
        }

        public void Delete(Point position, int size = 1)
        {
            if (!InBounds(position)) return;

            if(size == 0)
            {
                Particle p = GetParticle(position);
                toDelete.Add(p); // deleting on the next update
                map[position.X,position.Y] = new Particle(ElementID.AIR, position);
            }
            else
            {
                double angle = (2*Math.PI)/72;

                for (int s = size; s >= 0; s--)
                {
                    for (int i = 0; i < 72; i++)
                    {
                        int _x = position.X + (int)(Math.Cos(angle*i)*s);
                        int _y = position.Y + (int)(Math.Sin(angle*i)*s);

                        Point _position = new Point(_x, _y);

                        Particle p = GetParticle(_position);
                        toDelete.Add(p); // deleting on the next update
                        map[_position.X,_position.Y] = new Particle(ElementID.AIR, _position);
                    }
                }
            }
        }
        
        public void Swap(Point position1, Point position2)
        {
            Particle p1 = GetParticle(position1); 
            Particle p2 = GetParticle(position2); 

            p1.SetPosition(position2);
            p2.SetPosition(position1);

            map[position1.X,position1.Y] = p2;
            map[position2.X,position2.Y] = p1;
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

        public bool InBounds(Point position)
        {
            return (position.X >= 0 && position.X < width) && (position.Y >= 0 && position.Y < height);
        }
    }
}

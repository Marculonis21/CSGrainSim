using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim
{
    class ParticleMap
    {
        GameMap gameMap;
        TemperatureMap tempMap;

        int PINDEX;
        int[,] _map;
        Dictionary<int, Particle> _particles = new Dictionary<int, Particle>();

        List<Point> toSpawn_pos = new List<Point>();
        List<ElementID> toSpawn_elem = new List<ElementID>();
        List<int> toSpawn_size = new List<int>();

        List<Point> toDelete_pos = new List<Point>();
        List<int> toDelete_size = new List<int>();
        
        int width;
        int height;

        const ElementID outOfBoundsElement = ElementID.WALL;

        public ParticleMap(GameMap gameMap, int width, int height)
        {
            this.gameMap = gameMap;

            this.width = width;
            this.height = height;

            PINDEX = 0;
            this._map = new int[width,height];
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    _particles.Add(PINDEX, new Particle(ElementID.AIR, new Point(x,y)));
                    _map[x,y] = PINDEX;
                    PINDEX++;
                }
        }
        
        public ElementID[,] Save()
        {
            ElementID[,] save = new ElementID[width,height];

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    save[x,y] = Type(new Point(x,y));
                }

            return save;
        }
                
        public void Load(ElementID[,] save)
        {
            _particles.Clear();

            PINDEX = 0;
            this._map = new int[width,height];
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    _particles.Add(PINDEX, new Particle(save[x,y], new Point(x,y)));
                    _map[x,y] = PINDEX;
                    PINDEX++;
                }
        }

        public void Update()
        {
            foreach(Particle p in _particles.Values)
            {
                Point pos = p.GetPosition();
                if(p.Type() == ElementID.AIR) continue;

                p.Update(this.gameMap.GetParticleMap(), this.gameMap.GetTemperatureMap());
            }

            for(int i = 0; i < toSpawn_elem.Count; i++)
                Spawn(toSpawn_elem[i], toSpawn_pos[i], toSpawn_size[i]);

            for(int i = 0; i < toDelete_pos.Count; i++)
                Delete(toDelete_pos[i], toDelete_size[i]);

            toSpawn_elem.Clear();
            toSpawn_pos.Clear();
            toSpawn_size.Clear();

            toDelete_pos.Clear();
            toDelete_size.Clear();
        }

        public void Render(Shapes shapes, int particleSize)
        {
            foreach(Particle p in _particles.Values)
            {
                if(p.Type() == ElementID.AIR) continue;
                p.Render(shapes, particleSize);
            }
        }

        public void Spawn(ElementID element, Point position, int size = 1)
        {
            if(!InBounds(position)) return;

            tempMap = gameMap.GetTemperatureMap();

            if (size == 0)
            {
                if (!InBounds(position)) return;

                if (Type(position) == ElementID.AIR)
                {
                    if(!Element.elements.ContainsKey(element))
                        throw new Exception("Element: " + element + " not introduced in the elements dictionary yet.\nTry adding to ElementsSetup first.\n");

                    Particle p = new Particle(element, position);
                    _particles[GetParticleID(position)] = p;
                    tempMap.Set(position, 0, Element.elements[element].STemp);
                }
            }
            else
            {
                int offset;
                if(size < 20)
                    offset = 4;
                else if (size < 50)
                    offset = 6;
                else
                    offset = 10;

                for (int y = size; y > -size; y--)
                {
                    for (int x = -size; x < size; x++)
                    {
                        if(size+offset >= x*x + y*y)
                        {
                            int _x = position.X + x;
                            int _y = position.Y + y;
                            Point _position = new Point(_x, _y);

                            if (!InBounds(_position)) continue;

                            if (Type(_position) == ElementID.AIR)
                            {
                                if(!Element.elements.ContainsKey(element))
                                    throw new Exception("Element: " + element + " not introduced in the elements dictionary yet.\nTry adding to ElementsSetup first.\n");

                                Particle p = new Particle(element, _position);
                                _particles[GetParticleID(_position)] = p;
                                tempMap.Set(_position, 0, Element.elements[element].STemp);
                            }
                        }
                    }
                }
            }
        }

        public void Delete(Point position, int size = 1, bool walls = true) // from mouse
        {
            if(!InBounds(position)) return;

            if(size == 0)
            {
                if (!InBounds(position)) return;

                int id = _map[position.X, position.Y];
                if(!walls && _particles[id].Type() == ElementID.WALL) return;

                _particles[id] = new Particle(ElementID.AIR, new Point(position.X, position.Y));

                UnstableSurroundingParticles(position);
            }
            else
            {
                int offset;
                if(size < 20)
                    offset = 4;
                else if (size < 50)
                    offset = 6;
                else
                    offset = 10;

                for (int y = size; y > -size; y--)
                {
                    for (int x = -size; x < size; x++)
                    {
                        if(size+offset >= x*x + y*y)
                        {
                            int _x = position.X + x;
                            int _y = position.Y + y;
                            Point _position = new Point(_x, _y);

                            if (!InBounds(_position)) continue;

                            int id = _map[_position.X, _position.Y];

                            if(!walls && _particles[id].Type() == ElementID.WALL) continue;
                            _particles[id] = new Particle(ElementID.AIR, new Point(_position.X, _position.Y));
                            
                            UnstableSurroundingParticles(_position);
                        }
                    }
                }
            }
        }

        public void SpawnLater(ElementID element, Point position, int size = 1)
        {
            toSpawn_elem.Add(element);
            toSpawn_pos.Add(position);
            toSpawn_size.Add(size);
        }

        public void DeleteLater(Point position, int size)
        {
            if(!InBounds(position)) return;

            toDelete_pos.Add(position);
            toDelete_size.Add(size);
        }

        public void Swap(Point position1, Point position2)
        {
            int p1 = GetParticleID(position1); 
            int p2 = GetParticleID(position2); 
            if(p1 == -1 || p2 == -1) return;

            _particles[p1].SetPosition(position2);
            _particles[p2].SetPosition(position1);

            _map[position1.X, position1.Y] = p2;
            _map[position2.X, position2.Y] = p1;
        }

        public ElementID Type(Point position)
        {
            if(InBounds(position))
                return _particles[_map[position.X, position.Y]].Type();
            else
                return outOfBoundsElement;
        }

        public int GetParticleID(Point position)
        {
            if(InBounds(position))
                return _map[position.X, position.Y];

            return -1;
        }

        public Particle GetParticle(Point position)
        {
            if(InBounds(position))
                return _particles[_map[position.X, position.Y]];

            return new Particle(ElementID.VOID, new Point(-1,-1));
        }

        public bool InBounds(Point position)
        {
            return (position.X >= 0 && position.X < width) && (position.Y >= 0 && position.Y < height);
        }

        public void UnstableSurroundingParticles(Point position)
        {
            Point point;
            for(int y = -1; y <= 1; y++)
                for(int x = -1; x <= 1; x++)
                {
                    if(x == y && x == 0) continue;

                    point = new Point(position.X + x, position.Y + y);
                    if(!InBounds(point)) continue;
                    GetParticle(point).SetStable(false);
                }
        }
    }
}

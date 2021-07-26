using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class TemperatureMap
    {
        const float flowConstant = 0.2f;
        const float diffuseRate = 0.0003f;

        GameMap gameMap;

        float[,] map;

        int width;
        int height;

        int simDir = -1; //switch directions each turn - TL X BR

        public TemperatureMap(GameMap gameMap, int width, int height)
        {
            this.gameMap = gameMap;

            this.width = width;
            this.height = height;

            this.map = new float[width,height];
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    map[x,y] = Element.elements[ElementID.AIR].STemp;
        }

        public float Get(Point position)
        {
            if(InBounds(position))
                return map[position.X,position.Y];
            else
                new Exception("Out of bounds exception - tempMap - get");
                return -1f;
        }

        public void Set(Point position, int size, float value)
        {
            if(size == 0)
            {
                if (!InBounds(position)) return;
                if(this.gameMap.GetParticleMap().Type(position) != ElementID.WALL)
                    map[position.X, position.Y] = value;
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
                            if(this.gameMap.GetParticleMap().Type(position) != ElementID.WALL)
                                map[position.X, position.Y] = value;
                        }
                    }
                }
            }
        }

        public void Increment(Point position, int size, float value)
        {
            if(size == 0)
            {
                if (!InBounds(position)) return;
                if(this.gameMap.GetParticleMap().Type(position) != ElementID.WALL)
                    map[position.X, position.Y] += value;
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
                            if(this.gameMap.GetParticleMap().Type(_position) != ElementID.WALL)
                                map[_position.X, _position.Y] += value;
                        }
                    }
                }
            }
        }

        public void Render(Shapes shapes, int particleSize)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Point pos = new Point(x,y);
                    float temp = map[x,y];
                    if(temp > 0) 
                    {
                        shapes.DrawRectangle(new Point(pos.X*particleSize,
                                                       pos.Y*particleSize),
                                             particleSize,particleSize,
                                             new Color((int)(temp/4),0,0));
                    }
                    else
                    {
                        shapes.DrawRectangle(new Point(pos.X*particleSize,
                                                       pos.Y*particleSize),
                                             particleSize,particleSize,
                                             new Color(0,0,(int)(-temp/1)));
                    }
                }
            }
        }

        public void Update()
        {
            Propagate();
            Diffuse();
        }

        void Propagate()
        {
            // propagate heat to neighbor squares
            
            simDir = (simDir + 1) % 2;

            ParticleMap partMap = gameMap.GetParticleMap();
            
            int _y;
            int _x;
            for (int y = 0; y < height; y++) 
            {
                for (int x = 0; x < width; x++)
                {
                    if(simDir == 0) // dir switching
                    {
                        _x = x;
                        _y = y;
                    }
                    else
                    {
                        _x = (width-1)  - x;
                        _y = y;
                    }

                    // Get positions of current cell
                    Point cellPos = new Point(_x,_y);
                    if (partMap.Type(cellPos) == ElementID.WALL) continue; // skip WALLS

                    // Get positions of neigbor cells
                    Point neighPos;
                    Point[] neighborPoints = FindNeighbor(cellPos);
                    
                    // cell/neighbor HT = heatTransfer amount 
                    float cellHT = Element.elements[partMap.Type(cellPos)].HeatTrans;
                    float neighHT;

                    for (int i = 0; i < 4; i++)
                    {
                        neighPos = neighborPoints[i];
                        if(neighPos.X == -1 || partMap.Type(neighPos) == ElementID.WALL) continue; // Out of bounds / WALL

                        neighHT = Element.elements[partMap.Type(neighPos)].HeatTrans;

                        float flow = map[neighPos.X, neighPos.Y] - map[cellPos.X, cellPos.Y];
                        if(flow > 0.0f)
                            flow *= neighHT;
                        else
                            flow *= cellHT;

                        if(i == 0 && flow < 0) // favors upward hot air motion
                            flow *= flowConstant*50;
                        else
                            flow *= flowConstant;

                        map[neighPos.X, neighPos.Y] -= flow/neighHT;
                        map[cellPos.X, cellPos.Y]   += flow/cellHT;

                        // kill temperature oscilations (can be really confusing)
                        if((flow > 0.0f && map[neighPos.X, neighPos.Y] < map[cellPos.X, cellPos.Y]) || (
                           flow <= 0.0f && map[neighPos.X, neighPos.Y] > map[cellPos.X, cellPos.Y]))
                        {
                            float total = (cellHT * map[cellPos.X,cellPos.Y]) + (neighHT * map[neighPos.X,neighPos.Y]);
                            float avg = total / (cellHT + neighHT);

                            map[neighPos.X, neighPos.Y] = avg;
                            map[cellPos.X, cellPos.Y]   = avg;
                        }
                    }
                }
            }
        }

        bool InBounds(Point position)
        {
            return (position.X >= 0 && position.X < width) && (position.Y >= 0 && position.Y < height);
        }

        Point[] FindNeighbor(Point position)
        {
            Point[] neighborPoints = new Point[4];

            int x = position.X;
            int y = position.Y;

            // test in all 4 direction - UP DOWN LEFT RIGHT
            if(InBounds(new Point(x, y-1)))
                neighborPoints[0] = new Point(x, y-1);
            else
                neighborPoints[0] = new Point(-1,-1);

            if(InBounds(new Point(x, y+1)))
                neighborPoints[1] = new Point(x, y+1);
            else
                neighborPoints[1] = new Point(-1,-1);

            if(InBounds(new Point(x-1, y)))
                neighborPoints[2] = new Point(x-1, y);
            else
                neighborPoints[2] = new Point(-1,-1);

            if(InBounds(new Point(x+1, y)))
                neighborPoints[3] = new Point(x+1, y);
            else
                neighborPoints[3] = new Point(-1,-1);

            return neighborPoints;
        }

        private void Diffuse()
        {
            ParticleMap partMap = gameMap.GetParticleMap();

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    map[x,y] -= (map[x,y] - Element.elements[ElementID.AIR].STemp)*diffuseRate;
                    if(map[x,y] <= -273.15f)
                        map[x,y] = -273.15f;
                }
        }
    }
}

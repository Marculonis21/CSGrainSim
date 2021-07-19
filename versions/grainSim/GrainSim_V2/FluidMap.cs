using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class FluidMap
    {
        const float MaxMass = 1f;
        const float MinMass = 0.005f;
        /* const float MaxCompression = 0.01f; */
        const float MaxCompression = 0.25f;
        const float MinFlow = 0.01f;

        GameMap gameMap;

        float[,] map;
        float[,] newMap;
        ElementID [,] elementMap;

        int width;
        int height;

        int simDir = -1; //switch directions each turn - TL X BR

        public FluidMap(GameMap gameMap, int width, int height)
        {
            this.gameMap = gameMap;

            this.width = width;
            this.height = height;

            this.map = new float[width,height];
            this.newMap = new float[width,height];
            this.elementMap = new ElementID[width,height];

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    elementMap[x,y] = ElementID.VOID; 
                }
        }

        public ElementID GetElement(Point position)
        {
            if(InBounds(position))
                return elementMap[position.X,position.Y];
            else
                new Exception("Out of bounds exception - fluidMap - get");

            return ElementID.VOID;
        }

        public float GetMass(Point position)
        {
            if(InBounds(position))
                return map[position.X,position.Y];
            else
                new Exception("Out of bounds exception - fluidMap - get");

            return 0;
        }

        public void Set(Point position, int size, ElementID element)
        {
            if(InBounds(position))
            {
                map[position.X,position.Y] += 10f;
            }
            else
                new Exception("Out of bounds exception - fluidMap - set");
        }

        public void Render(Shapes shapes, int particleSize)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Point pos = new Point(x,y);
                    if(map[x,y] > MinMass)
                        shapes.DrawRectangle(new Point(pos.X*particleSize,
                                                       pos.Y*particleSize),
                                             particleSize,particleSize,
                                             new Color(0,0,255));
                        /* shapes.DrawRectangle(new Point(pos.X*particleSize, */
                        /*                                pos.Y*particleSize), */
                        /*                      particleSize,particleSize, */
                        /*                      new Color(0,0,(int)MathF.Pow(5,map[x,y]))); */
                }
            }
        }

        public void Update()
        {
            Propagate();
        }

        float GetStable(float total)
        {
            if(total <= MaxMass)
                return MaxMass;
            else if( total < 2*MaxMass + MaxCompression )
                return (MaxMass*MaxMass + total*MaxCompression)/(MaxMass + MaxCompression);
            else
                return (total + MaxCompression)/2;
        }

        void Propagate()
        {
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
                        _x = (width-1) - x;
                        _y = y;
                    }

                    // Get positions of current cell
                    Point cellPos = new Point(_x,_y);
                    if (partMap.Type(cellPos) == ElementID.WALL) continue; // skip WALLS

                    float remainingMass = map[cellPos.X, cellPos.Y];
                    
                    // Get positions of neigbor cells
                    Point neighPos;
                    Point[] neighborPoints = FindNeighbor(cellPos);

                    for (int i = 0; i < 4; i++)
                    {
                        float flow = 0;

                        neighPos = neighborPoints[i];
                        if(neighPos.X == -1) continue; // out of bounds
                        if (partMap.Type(neighPos) == ElementID.WALL) continue; // skip WALLS

                        if (remainingMass <= 0) break; 

                        if(i == 0) // below
                        {
                            flow = GetStable(remainingMass + map[neighPos.X, neighPos.Y]) - map[neighPos.X, neighPos.Y];
                        }
                        else if(i == 1 || i == 2) // left/right
                        {
                            flow = (map[cellPos.X, cellPos.Y] - map[neighPos.X, neighPos.Y]) / 2;
                        }
                        else if(i == 3) // upwards
                        {
                            flow = remainingMass - GetStable(remainingMass + map[neighPos.X, neighPos.Y]);
                        }

                        /* if (flow > MinFlow) */
                        /*     flow *= 0.5f; */

                        // constrain flow
                        if(flow < 0)
                            flow = 0;
                        if(flow > remainingMass)
                            flow = remainingMass;

                        remainingMass -= flow;
                        map[cellPos.X, cellPos.Y]   -= flow;
                        map[neighPos.X, neighPos.Y] += flow;
                    }
                }
            }

            for (int y = 0; y < height; y++) 
            {
                for (int x = 0; x < width; x++)
                {
                    /* map[x,y] += newMap[x,y]; */
                    newMap[x,y] = 0;

                    if(map[x,y] > MinMass)
                        elementMap[x,y] = ElementID.WATER;
                    else
                    {
                        elementMap[x,y] = ElementID.AIR;
                        map[x,y] = 0;
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

            // test in all 4 direction - DOWN LEFT RIGHT UP 

            if(InBounds(new Point(x, y+1)))
                neighborPoints[0] = new Point(x, y+1);
            else
                neighborPoints[0] = new Point(-1,-1);

            if(MainGame.random.NextDouble() >= 0.5)
            {
                if(InBounds(new Point(x-1, y)))
                    neighborPoints[1] = new Point(x-1, y);
                else
                    neighborPoints[1] = new Point(-1,-1);

                if(InBounds(new Point(x+1, y)))
                    neighborPoints[2] = new Point(x+1, y);
                else
                    neighborPoints[2] = new Point(-1,-1);
            }
            else
            {
                if(InBounds(new Point(x+1, y)))
                    neighborPoints[1] = new Point(x+1, y);
                else
                    neighborPoints[1] = new Point(-1,-1);

                if(InBounds(new Point(x-1, y)))
                    neighborPoints[2] = new Point(x-1, y);
                else
                    neighborPoints[2] = new Point(-1,-1);
            }

            if(InBounds(new Point(x, y-1)))
                neighborPoints[3] = new Point(x, y-1);
            else
                neighborPoints[3] = new Point(-1,-1);

            return neighborPoints;
        }
    }
}

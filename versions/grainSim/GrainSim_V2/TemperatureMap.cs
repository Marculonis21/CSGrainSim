using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class TemperatureMap
    {
        GameMap gameMap;

        float[,] map;
        float[,] newMap;

        int width;
        int height;

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

        public void Set(Point position, float value)
        {
            if(InBounds(position))
                map[position.X,position.Y] = value;
            else
                new Exception("Out of bounds exception - tempMap - set");
        }

        public void Increment(Point position, float value)
        {
            if(InBounds(position))
                map[position.X,position.Y] += value;
            else
                new Exception("Out of bounds exception - tempMap - set");
        }

        public void Update()
        {
            ZeroOutNext();
            Propagate();
            Diffuse();

            this.map = this.newMap;
        }

        void Propagate()
        {
            float constant = 0.1f;
            // propagate heat to neighbor squares
            
            ParticleMap partMap = gameMap.GetParticleMap();
            
            for (int y = 0; y < height; y++) 
            {
                for (int x = 0; x < width; x++)
                {
                    Point cellPos = new Point(x,y);
                    Point[] neighborPoints = FindNeighbor(cellPos);
                    
                    // cell/neighbor HT = heatTransfer amount (float)
                    float cellHT = Element.elements[partMap.Type(cellPos)].HeatTrans;
                    float neighHT;
                    foreach (Point neighPos in neighborPoints)
                    {
                        if(neighPos.X == -1) continue;

                        neighHT = Element.elements[partMap.Type(neighPos)].HeatTrans;

                        float flow = map[neighPos.X, neighPos.Y] - map[cellPos.X, cellPos.Y];
                        if(flow > 0.0f)
                            flow *= neighHT;
                        else
                            flow *= cellHT;

                        flow *= constant;

                        newMap[neighPos.X, neighPos.Y] -= flow/neighHT;
                        newMap[cellPos.X, cellPos.Y]   += flow/cellHT;
                    }

                    newMap[cellPos.X, cellPos.Y] += map[cellPos.X,cellPos.Y];
                }
            }
        }

        void Diffuse()
        {
            // make everything slowly go towards its Starting temp
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

        void ZeroOutNext()
        {
            this.newMap = new float[width,height];
        }
    }
}

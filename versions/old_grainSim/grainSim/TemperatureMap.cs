using System;

namespace grainSim
{
    public class TemperatureMap : IMap
    {
        int width; 
        int height;

        ElementID[,] map;

        public ParticleMap(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public object Copy()
        {
            return this.map;
        }

        public bool InBounds(int x, int y)
        {
            return (x >= 0 && x < width) && (y >= 0 && y < height);
        }

        public void Set(object obj, int x, int y)
        {
            map[x,y] = (ElementID)obj;
        }

        public object Get(int x, int y)
        {
            return map[x,y];
        }
    }
}

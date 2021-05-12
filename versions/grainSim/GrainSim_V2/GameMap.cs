using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GrainSim_v2
{
    class GameMap
    {
        ParticleMap partMap;
        TemperatureMap tempMap;

        public int width {get; private set;}
        public int height {get; private set;}

        public GameMap(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void Update()
        {
            partMap.Update();
            tempMap.Update();
        }

        public ParticleMap Get()
        {
            return partMap;
        }

        public TemperatureMap Get()
        {
            return tempMap;
        }
    }
}


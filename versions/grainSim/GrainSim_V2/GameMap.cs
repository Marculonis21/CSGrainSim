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

            partMap = new ParticleMap(this, width, height);
            tempMap = new TemperatureMap(this, width, height);
        }

        public void Update()
        {
            partMap.Update();
            tempMap.Update();
        }

        public ParticleMap GetParticleMap()
        {
            return partMap;
        }

        public TemperatureMap GetTemperatureMap()
        {
            return tempMap;
        }
    }
}


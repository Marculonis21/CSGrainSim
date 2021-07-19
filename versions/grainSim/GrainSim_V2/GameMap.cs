using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GrainSim_v2
{
    class GameMap
    {
        ParticleMap partMap;
        TemperatureMap tempMap;
        FluidMap fluidMap;

        public int width {get; private set;}
        public int height {get; private set;}

        public GameMap(int width, int height)
        {
            this.width = width;
            this.height = height;

            partMap = new ParticleMap(this, width, height);
            tempMap = new TemperatureMap(this, width, height);
            /* fluidMap = new FluidMap(this, width, height); */
        }

        public void Update()
        {
            partMap.Update();
            tempMap.Update();
            /* fluidMap.Update(); */
        }

        public ParticleMap GetParticleMap()
        {
            return partMap;
        }

        public TemperatureMap GetTemperatureMap()
        {
            return tempMap;
        }

        /* public FluidMap GetFluidMap() */
        /* { */
        /*     return fluidMap; */
        /* } */
    }
}


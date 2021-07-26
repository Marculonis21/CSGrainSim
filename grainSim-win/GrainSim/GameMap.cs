namespace GrainSim
{
    class GameMap
    {
        ParticleMap partMap;
        TemperatureMap tempMap;
        /* FluidMap fluidMap; */

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

        public void Save(out ElementID[,] saveP, out float[,] saveT)
        {
            saveP = partMap.Save();
            saveT = tempMap.Save();
        }
        public void Load(ElementID[,] saveP, float[,] saveT)
        {
            partMap.Load(saveP);
            tempMap.Load(saveT);
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


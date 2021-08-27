using System;

namespace GrainSim
{
    [Serializable]
    class SaveContainer
    {
        public ElementID[,] saveParticles;
        public float[,]     saveTemps;

        public SaveContainer(ElementID[,] saveP, float[,] saveT)
        {
            this.saveParticles = saveP;
            this.saveTemps = saveT;
        }
    }
}


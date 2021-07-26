using System;

namespace GrainSim_v2
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


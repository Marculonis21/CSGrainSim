using System;

namespace grainSim
{
    interface IMap
    {
        object Get(int x, int y);
        void Set(object obj, int x, int y);
        bool InBounds(int x, int y);
        object Copy();
    }
}

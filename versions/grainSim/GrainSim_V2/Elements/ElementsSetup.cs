using System;
using System.Collections.Generic;

namespace GrainSim_v2
{
    public enum ElementID
    {
        AIR,
        COPPER,
        COPPERMELT,
        DUST,
        FIRE,
        ICE,
        OIL,
        SAND,
        SMOKE,
        VOID,
        WALL,
        WATER,
        WATERVAPOR,
    }

    partial class Element
    {
        public static Dictionary<ElementID, Element> elements {get; private set;} // Dict of all elements

        public static void SetupElements()
        {
            elements = new Dictionary<ElementID, Element>();

            elements.Add(ElementID.AIR, new Air());
            elements.Add(ElementID.SAND, new Sand());
            elements.Add(ElementID.WALL, new Wall());
            elements.Add(ElementID.WATER, new Water());
            elements.Add(ElementID.COPPER, new Copper());
            elements.Add(ElementID.COPPERMELT, new CopperMelt());
        }
    }
}

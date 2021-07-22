using System;
using System.Collections.Generic;

namespace GrainSim_v2
{
    public enum ElementID
    {
        AIR,
        COLD,
        COPPER,
        COPPERMELT,
        ERASE,
        ERASEP,
        FIRE,
        HOT,
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
            elements.Add(ElementID.COPPER, new Copper());
            elements.Add(ElementID.HOT, new Hot());
            elements.Add(ElementID.COLD, new Cold());
            elements.Add(ElementID.ERASE, new Erase());
            elements.Add(ElementID.ERASEP, new EraseP());
            elements.Add(ElementID.COPPERMELT, new CopperMelt());
            elements.Add(ElementID.FIRE, new Fire());
            elements.Add(ElementID.SAND, new Sand());
            elements.Add(ElementID.SMOKE, new Smoke());
            elements.Add(ElementID.WALL, new Wall());
            elements.Add(ElementID.WATER, new Water());
            elements.Add(ElementID.WATERVAPOR, new WaterVapor());
        }
    }
}

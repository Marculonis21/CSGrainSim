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
        OILBURN,
        SAND,
        SMOKE,
        VOID,
        WALL,
        WATER,
        WATERVAPOR,
        WOOD,
        WOODBURN,

        EXPLOSION //(not a special element) used as result of explosive reactions 
    }

    partial class Element
    {
        public static Dictionary<ElementID, Element> elements {get; private set;} // Dict of all elements

        public static void SetupElements()
        {
            elements = new Dictionary<ElementID, Element>();

            elements.Add(ElementID.AIR,        new Air());
            elements.Add(ElementID.COLD,       new Cold());
            elements.Add(ElementID.COPPER,     new Copper());
            elements.Add(ElementID.COPPERMELT, new CopperMelt());
            elements.Add(ElementID.ERASE,      new Erase());
            elements.Add(ElementID.ERASEP,     new EraseP());
            elements.Add(ElementID.FIRE,       new Fire());
            elements.Add(ElementID.HOT,        new Hot());
            elements.Add(ElementID.ICE,        new Ice());
            elements.Add(ElementID.OIL,        new Oil());
            elements.Add(ElementID.OILBURN,    new OilBurn());
            elements.Add(ElementID.SAND,       new Sand());
            elements.Add(ElementID.SMOKE,      new Smoke());
            elements.Add(ElementID.WALL,       new Wall());
            elements.Add(ElementID.WATER,      new Water());
            elements.Add(ElementID.WATERVAPOR, new WaterVapor());
            elements.Add(ElementID.WOOD,       new Wood());
            elements.Add(ElementID.WOODBURN,   new WoodBurn());
        }

        protected void DefaultReactions(Element element)
        {
            if(flameable > 0)
            {
                //react with fire and with its burn thing
                element.reactions.Add(new Reaction(element.id, element.burnElement, ElementID.FIRE, 1, flameable));
                element.reactions.Add(new Reaction(element.id, element.burnElement, element.burnElement, 1, flameable/10));

                //react with * molten things
                element.reactions.Add(new Reaction(element.id, ElementID.FIRE, ElementID.COPPERMELT, 1, 1));
            }

            if(explosive > 0)
            {
                //react with fire and with its burn thing
                element.reactions.Add(new Reaction(element.id, element.burnElement, ElementID.FIRE, 1, explosive));
                element.reactions.Add(new Reaction(element.id, element.burnElement, element.burnElement, 1, explosive/5));

                //react with * molten things
                element.reactions.Add(new Reaction(element.id, ElementID.EXPLOSION, ElementID.COPPERMELT, 1, 1));
            }

            if(element.destroyedByMolten)
            {
                element.reactions.Add(new Reaction(element.id, ElementID.COPPERMELT, ElementID.COPPERMELT, 1, 0.15f));
                element.reactions.Add(new Reaction(element.id, ElementID.VOID, ElementID.COPPERMELT, 1, 1f));
            }
        }
    }
}

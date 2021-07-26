using System.Collections.Generic;

namespace GrainSim
{
    public enum ElementID
    {
        AIR,
        BRONZE,
        BRONZEMELT,
        C4,
        C4BURN,
        COLD,
        COPPER,
        COPPERMELT,
        ERASE,
        ERASEP,
        FIRE,
        GAS,
        GASBURN,
        GUNPOWDER,
        GUNPOWDERBURN,
        HOT,
        ICE,
        OIL,
        OILBURN,
        SALT,
        SALTICE,
        SALTWATER,
        SAND,
        SMOKE,
        TIN,
        TINMELT,
        VOID,
        WALL,
        WATER,
        WATERVAPOR,
        WOOD,
        WOODBURN,

        MOLTEN, // used for destructive reactions with molten stuff (performance) 
        EXPLOSION //(not a special element) used as result of explosive reactions 
    }

    partial class Element
    {
        public static Dictionary<ElementID, Element> elements {get; private set;} // Dict of all elements

        public static void SetupElements()
        {
            elements = new Dictionary<ElementID, Element>();

            elements.Add(ElementID.AIR,           new Air());
            elements.Add(ElementID.BRONZE,        new Bronze());
            elements.Add(ElementID.BRONZEMELT,    new BronzeMelt());
            elements.Add(ElementID.C4,            new C4());
            elements.Add(ElementID.C4BURN,        new C4Burn());
            elements.Add(ElementID.COLD,          new Cold());
            elements.Add(ElementID.COPPER,        new Copper());
            elements.Add(ElementID.COPPERMELT,    new CopperMelt());
            elements.Add(ElementID.ERASE,         new Erase());
            elements.Add(ElementID.ERASEP,        new EraseP());
            elements.Add(ElementID.FIRE,          new Fire());
            elements.Add(ElementID.GAS,           new Gas());
            elements.Add(ElementID.GASBURN,       new GasBurn());
            elements.Add(ElementID.GUNPOWDER,     new Gunpowder());
            elements.Add(ElementID.GUNPOWDERBURN, new GunpowderBurn());
            elements.Add(ElementID.HOT,           new Hot());
            elements.Add(ElementID.ICE,           new Ice());
            elements.Add(ElementID.OIL,           new Oil());
            elements.Add(ElementID.OILBURN,       new OilBurn());
            elements.Add(ElementID.SALT,          new Salt());
            elements.Add(ElementID.SALTICE,       new SaltIce());
            elements.Add(ElementID.SALTWATER,     new SaltWater());
            elements.Add(ElementID.SAND,          new Sand());
            elements.Add(ElementID.SMOKE,         new Smoke());
            elements.Add(ElementID.TIN,           new Tin());
            elements.Add(ElementID.TINMELT,       new TinMelt());
            elements.Add(ElementID.WALL,          new Wall());
            elements.Add(ElementID.WATER,         new Water());
            elements.Add(ElementID.WATERVAPOR,    new WaterVapor());
            elements.Add(ElementID.WOOD,          new Wood());
            elements.Add(ElementID.WOODBURN,      new WoodBurn());
        }

        protected void DefaultReactions(Element element)
        {
            if(flameable > 0)
            {
                //react with fire and with its burn thing
                element.reactions.Add(new Reaction(element.id, new List<ElementID>() {element.burnElement}, ElementID.FIRE, 1, flameable));
                element.reactions.Add(new Reaction(element.id, new List<ElementID>() {element.burnElement}, element.burnElement, 1, flameable/10));

                //react with * molten things
                element.reactions.Add(new Reaction(element.id, new List<ElementID>() {ElementID.FIRE}, ElementID.MOLTEN, 1, 1));
            }

            if(explosive > 0)
            {
                //react with fire and with its burn thing
                element.reactions.Add(new Reaction(element.id, new List<ElementID>() {element.burnElement}, ElementID.FIRE, 1, explosive));
                element.reactions.Add(new Reaction(element.id, new List<ElementID>() {element.burnElement}, element.burnElement, 1, explosive/10));

                //react with * molten things
                element.reactions.Add(new Reaction(element.id, new List<ElementID>() {ElementID.EXPLOSION}, ElementID.MOLTEN, 1, 1));
            }

            if(element.destroyedByMolten)
            {
                element.reactions.Add(new Reaction(element.id, new List<ElementID>() {ElementID.MOLTEN}, ElementID.MOLTEN, 1, 0.15f));
                element.reactions.Add(new Reaction(element.id, new List<ElementID>() {ElementID.VOID}, ElementID.MOLTEN, 1, 1f));
            }
        }
    }
}

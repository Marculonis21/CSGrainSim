using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace grainSim
{
    public partial class Element
    {
        public static Dictionary<ElementID, Element> elements; // Dict of all elements

        public static void SetupElements()
        {
            elements = new Dictionary<ElementID, Element>(); // MAYBE CHANGE TO SINGLETON

            Air air = new Air();
            Ice ice = new Ice();
            Sand sand = new Sand();
            Wall wall = new Wall();
            Water water = new Water();

            elements.Add(ElementID.AIR, air);
            elements.Add(ElementID.ICE, ice);
            elements.Add(ElementID.SAND, sand);
            elements.Add(ElementID.WALL, wall);
            elements.Add(ElementID.WATER, water);
        }
    }
}
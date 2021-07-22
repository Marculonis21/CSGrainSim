using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class UIManager
    {
        /// SINGLETON
        public List<UIItem> menuElements   {get; private set;} 
        public List<UIItem> activeElements {get; private set;} 

        GameState gameState = GameState.instance;
        GraphicState graphicState = GraphicState.instance;

        private UIManager()
        {
            menuElements = new List<UIItem>();
            activeElements = new List<UIItem>();
        }

        public static readonly UIManager instance = new UIManager();

        public void AddButton(UIItem button)
        {
            menuElements.Add(button);
        }

        public void SetActiveElements(List<UIItem> list)
        {
            activeElements = list;
        }

        public bool CheckClick()
        {
            foreach(UIItem item in menuElements)
                if(item.Collide(gameState.cursorPosition))
                {
                    item.Click();
                    return true;
                }
                    
            foreach(UIItem item in activeElements)
                if(item.Collide(gameState.cursorPosition))
                {
                    item.Click();
                    return true;
                }

            return false;
        }

        public void DrawUI(Shapes shapes)
        {
            ClearUIArea(shapes);
            DrawElements(shapes);

            DrawDescriptors(shapes);
        }

        private void ClearUIArea(Shapes shapes)
        {
            // UI area cleanup
            shapes.Begin();
            shapes.DrawLine(new Point(0, graphicState.windowHeight-20*graphicState.particleSize), 
                            new Point(graphicState.windowWidth, graphicState.windowHeight-20*graphicState.particleSize), 
                            3, Color.Gray);  
            shapes.DrawRectangle(new Point(0, graphicState.windowHeight-20*graphicState.particleSize), 
                                 graphicState.windowWidth, 75, Color.Black);
            shapes.End();
        }

        private void DrawElements(Shapes shapes)
        {
            // drawing  elements
            foreach(UIItem item in menuElements)
                item.Draw(shapes);

            foreach(UIItem item in activeElements)
                item.Draw(shapes);
        }

        private void DrawDescriptors(Shapes shapes)
        {
            ElementID id = gameState.currElement;
            if(Element.elements.ContainsKey(id) && id != ElementID.AIR && id != ElementID.VOID)
                shapes.DrawText(Element.elements[id].Description, 
                                "smallButtonFont", 
                                new Vector2(graphicState.windowWidth-8, graphicState.windowHeight - 8),
                                Color.DimGray, anchor: 2);
        }

        public void Setup()
        {
            ElementID[] specialCategory = new ElementID[] {ElementID.WALL, ElementID.FIRE, ElementID.HOT, ElementID.COLD, ElementID.ERASE, ElementID.ERASEP};
            List<UIItem> solids = new List<UIItem>();
            List<UIItem> liquids = new List<UIItem>();
            List<UIItem> gasses = new List<UIItem>();
            List<UIItem> specials = new List<UIItem>();

            Vector2 startPosition = new Vector2(230, graphicState.windowHeight - 82);
            int butWidth = 70;
            int butHeight = 25;
            int borderWidth = 2;

            Vector2 butOffset = new Vector2(butWidth + 6, 0);

            foreach (ElementID id in specialCategory)
            {
                
                if(Element.elements.ContainsKey(id))
                {
                    Element elem = Element.elements[id];

                    specials.Add(new ElementButton(id,
                                                   elem.Short,
                                                   "smallButtonFont",
                                                   startPosition + specials.Count*butOffset,
                                                   butWidth, butHeight, borderWidth, 
                                                   elem.Color, 
                                                   elem.Color));
                }
            }

            foreach (ElementID id in Enum.GetValues(typeof(ElementID)))
            {
                if(Element.elements.ContainsKey(id) && id != ElementID.AIR && id != ElementID.VOID)
                {
                    Element elem = Element.elements[id];

                    if(!specialCategory.Contains(id))
                    {
                        switch(elem.State)
                        {
                            case 0:
                                solids.Add(new ElementButton(id,
                                                             elem.Short,
                                                             "smallButtonFont",
                                                             startPosition + solids.Count*butOffset,
                                                             butWidth, butHeight, borderWidth, 
                                                             elem.Color, 
                                                             elem.Color));
                                break;
                            case 1:
                                liquids.Add(new ElementButton(id,
                                                              elem.Short,
                                                              "smallButtonFont",
                                                              startPosition + liquids.Count*butOffset,
                                                              butWidth, butHeight, borderWidth, 
                                                              elem.Color, 
                                                              elem.Color));
                                break;
                            case 2:
                                gasses.Add(new ElementButton(id,
                                                             elem.Short,
                                                             "smallButtonFont",
                                                             startPosition + gasses.Count*butOffset,
                                                             butWidth, butHeight, borderWidth, 
                                                             elem.Color, 
                                                             elem.Color));
                                break;
                        }
                    }
                }
            }

            AddButton(new MenuButton(solids, 
                                     "Solids", 
                                     "buttonFont",
                                     new Vector2(7, graphicState.windowHeight-85), 
                                     92, 30, 2, 
                                     Color.White, 
                                     Color.DimGray));

            AddButton(new MenuButton(liquids, 
                                     "Liquids", 
                                     "buttonFont",
                                     new Vector2(109, graphicState.windowHeight-85), 
                                     92, 30, 2, 
                                     Color.White, 
                                     Color.DimGray));

            AddButton(new MenuButton(gasses, 
                                     "Gasses", 
                                     "buttonFont",
                                     new Vector2(7, graphicState.windowHeight-45), 
                                     92, 30, 2, 
                                     Color.White, 
                                     Color.DimGray));

            AddButton(new MenuButton(specials, 
                                     "Specials", 
                                     "buttonFont",
                                     new Vector2(109, graphicState.windowHeight-45), 
                                     92, 30, 2, 
                                     Color.White, 
                                     Color.DimGray));
        }
    }
}

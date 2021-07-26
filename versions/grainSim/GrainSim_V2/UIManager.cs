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
ParticleMap partMap; TemperatureMap tempMap;
        GameState gameState = GameState.instance;
        GraphicState graphicState = GraphicState.instance;

        string menuPos = "default";

        private UIManager()
        {
            menuElements = new List<UIItem>();
            activeElements = new List<UIItem>();
        }

        public static readonly UIManager instance = new UIManager();

        public void SetMenuElements(List<UIItem> list)
        {
            menuElements = list;
        }

        public void SetActiveElements(List<UIItem> list)
        {
            activeElements = list;
        }

        public bool CheckClick()
        {
            /* if(menuElements != null) */
            foreach(UIItem item in menuElements)
                if(item.Collide(gameState.cursorPosition))
                {
                    item.Click();
                    if(item.info != "")
                        this.menuPos = item.info;
                    return true;
                }
                    
            /* if(activeElements != null) */
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

            if(menuPos == "elements")
                DrawElementDescription(shapes);

            DrawSimDescriptors(shapes);
        }

        private void ClearUIArea(Shapes shapes)
        {
            int offset = 20;

            // UI area cleanup
            shapes.Begin();
            shapes.DrawLine(new Point(0, graphicState.windowHeight-offset*graphicState.particleSize), 
                            new Point(graphicState.windowWidth, graphicState.windowHeight-offset*graphicState.particleSize), 
                            3, Color.Gray);  
            shapes.DrawRectangle(new Point(0, graphicState.windowHeight-offset*graphicState.particleSize), 
                                 graphicState.windowWidth, offset*graphicState.particleSize, Color.Black);
            shapes.End();
        }

        private void DrawElements(Shapes shapes)
        {
            if(menuElements != null)
                foreach(UIItem item in menuElements)
                    item.Draw(shapes);

            if(menuElements != null)
                foreach(UIItem item in activeElements)
                    item.Draw(shapes);
        }

        private void DrawElementDescription(Shapes shapes)
        {
            ElementID id = gameState.currElement;
            if(Element.elements.ContainsKey(id) && id != ElementID.AIR && id != ElementID.VOID)
                shapes.DrawText(Element.elements[id].Description, 
                                "smallButtonFont", 
                                new Vector2(graphicState.windowWidth-8, graphicState.windowHeight - 8),
                                Color.DimGray, anchorX: 2);
        }

        private void DrawSimDescriptors(Shapes shapes)
        {
            string output = "";

            if(gameState.simDescriptors["drawStyle"])
                output += $"Draw style: {graphicState.drawStyle}\n";

            if(gameState.simDescriptors["selectedId"])
                output += $"Selected element: {gameState.currElement.ToString()}\n";

            if(gameState.simDescriptors["cellPos"])
                output += $"Cell position: {gameState.cursorBoardPosition.X}:{gameState.cursorBoardPosition.Y}\n";

            if(gameState.simDescriptors["cellId"])
                output += $"Cell element: {partMap.GetParticle(gameState.cursorBoardPosition).Type().ToString()}\n";

            if(gameState.simDescriptors["cellTemp"])
                output += $"Cell temperature: {tempMap.Get(gameState.cursorBoardPosition).ToString()}\n";

            graphicState.drawBoard = gameState.simDescriptors["drawBoard"];

            shapes.DrawText(output, 
                            "smallButtonFont", 
                            new Vector2(6, 6),
                            Color.DarkRed, anchorX: 0, anchorY: 0);
            shapes.DrawText(output, 
                            "smallButtonFont", 
                            new Vector2(5, 5),
                            Color.Red, anchorX: 0, anchorY: 0);
        }

        public void Setup(MainGame game, ParticleMap partMap, TemperatureMap tempMap)
        {
            this.partMap = partMap;
            this.tempMap = tempMap;

            ElementID[] specialCategory = new ElementID[] {ElementID.WALL, ElementID.FIRE, ElementID.HOT, ElementID.COLD, ElementID.ERASE, ElementID.ERASEP};
            List<UIItem> solids = new List<UIItem>();
            List<UIItem> liquids = new List<UIItem>();
            List<UIItem> gasses = new List<UIItem>();
            List<UIItem> specials = new List<UIItem>();

            List<UIItem> elementSelectMenu = new List<UIItem>();
            List<UIItem> drawStylesMenu = new List<UIItem>();
            List<UIItem> settingsMenu = new List<UIItem>(); 
            List<UIItem> fileFunctionsMenu = new List<UIItem>(); 
            List<UIItem> defaultMenu = new List<UIItem>();


            // X buttons
            Vector2 backButtonPos = new Vector2(graphicState.windowWidth - 30, graphicState.windowHeight - 95);
            int backButtonWidth = 25; 
            elementSelectMenu.Add(new MenuButton(defaultMenu, "menu",
                                                 "X",
                                                 "smallButtonFont",
                                                 backButtonPos,
                                                 backButtonWidth,
                                                 backButtonWidth,
                                                 2,
                                                 Color.White,
                                                 Color.DimGray,
                                                 "default"));

            settingsMenu.Add(new MenuButton(defaultMenu, "menu",
                                            "X",
                                            "smallButtonFont",
                                            backButtonPos,
                                            backButtonWidth,
                                            backButtonWidth,
                                            2,
                                            Color.White,
                                            Color.DimGray));


            // elementButtons
            Vector2 startPosition = new Vector2(230, graphicState.windowHeight - 82);
            int butWidth = 70;
            int butHeight = 25;
            int borderWidth = 2;
            Vector2 butXOffset = new Vector2(butWidth + 6, 0);
            Vector2 butYOffset = new Vector2(0, butHeight + 6);
            foreach (ElementID id in specialCategory)
            {
                if(Element.elements.ContainsKey(id))
                {
                    Element elem = Element.elements[id];

                    specials.Add(new ElementButton(id,
                                                   elem.Short,
                                                   "smallButtonFont",
                                                   startPosition + specials.Count*butXOffset,
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
                    if(elem.UIExclude) continue;

                    if(!specialCategory.Contains(id))
                    {
                        switch(elem.State)
                        {
                            case 0:
                                solids.Add(new ElementButton(id,
                                                             elem.Short,
                                                             "smallButtonFont",
                                                             startPosition + (solids.Count%7)*butXOffset + (solids.Count/7)*butYOffset,
                                                             butWidth, butHeight, borderWidth, 
                                                             elem.Color, 
                                                             elem.Color));
                                break;
                            case 1:
                                liquids.Add(new ElementButton(id,
                                                              elem.Short,
                                                              "smallButtonFont",
                                                              startPosition + (liquids.Count%7)*butXOffset + (liquids.Count/7)*butYOffset,
                                                              butWidth, butHeight, borderWidth, 
                                                              elem.Color, 
                                                              elem.Color));
                                break;
                            case 2:
                                gasses.Add(new ElementButton(id,
                                                             elem.Short,
                                                             "smallButtonFont",
                                                             startPosition + (gasses.Count%7)*butXOffset + (gasses.Count/7)*butYOffset,
                                                             butWidth, butHeight, borderWidth, 
                                                             elem.Color, 
                                                             elem.Color));
                                break;
                        }
                    }
                }
            }

            // ELEMENT SELECT MENU
            elementSelectMenu.Add(new MenuButton(solids, "active", 
                                                 "Solids", 
                                                 "buttonFont",
                                                 new Vector2(7, graphicState.windowHeight-85), 
                                                 92, 30, 2, 
                                                 Color.White, 
                                                 Color.DimGray));

            elementSelectMenu.Add(new MenuButton(liquids, "active", 
                                                 "Liquids", 
                                                 "buttonFont",
                                                 new Vector2(109, graphicState.windowHeight-85), 
                                                 92, 30, 2, 
                                                 Color.White, 
                                                 Color.DimGray));

            elementSelectMenu.Add(new MenuButton(gasses, "active", 
                                                 "Gasses", 
                                                 "buttonFont",
                                                 new Vector2(7, graphicState.windowHeight-45), 
                                                 92, 30, 2, 
                                                 Color.White, 
                                                 Color.DimGray));

            elementSelectMenu.Add(new MenuButton(specials, "active", 
                                                 "Specials", 
                                                 "buttonFont",
                                                 new Vector2(109, graphicState.windowHeight-45), 
                                                 92, 30, 2, 
                                                 Color.White, 
                                                 Color.DimGray));

            // DRAWSTYLES MENU
            startPosition = new Vector2(graphicState.windowWidth - 220, graphicState.windowHeight - 87);
            drawStylesMenu.Add(new DrawStyleButton(GraphicState.DRAWSTYLES.PARTICLE, 
                                                   "Particles - (F1)", 
                                                   "smallButtonFont", 
                                                   startPosition, 
                                                   200, 35, borderWidth, 
                                                   Color.White, 
                                                   Color.Green));

            drawStylesMenu.Add(new DrawStyleButton(GraphicState.DRAWSTYLES.TEMPERATURE, 
                                                   "Temperatures - (F2)", 
                                                   "smallButtonFont", 
                                                   startPosition + new Vector2(-10, 41), 
                                                   220, 35, borderWidth, 
                                                   Color.White, 
                                                   Color.DarkRed));

            // SETTINGS MENU
            startPosition = new Vector2(45, graphicState.windowHeight - 67);

            settingsMenu.Add(new RadioButton(gameState.SetDescriptor, 
                                             "drawBoard",
                                             false,
                                             "Show map board",
                                             "smallButtonFont",
                                             startPosition,
                                             185, 34, borderWidth,
                                             Color.White,
                                             Color.DimGray));


            settingsMenu.Add(new RadioButton(gameState.SetDescriptor, 
                                             "selectedId",
                                             false,
                                             "Show selected",
                                             "smallButtonFont",
                                             startPosition + new Vector2(200, 0),
                                             160, 34, borderWidth,
                                             Color.White,
                                             Color.DimGray));

            settingsMenu.Add(new RadioButton(gameState.SetDescriptor, 
                                             "cellPos",
                                             false,
                                             "Cell pos",
                                             "smallButtonFont",
                                             startPosition + new Vector2(375, 0),
                                             100, 34, borderWidth,
                                             Color.White,
                                             Color.DimGray));
            
            settingsMenu.Add(new RadioButton(gameState.SetDescriptor, 
                                             "cellId",
                                             false,
                                             "Cell ID",
                                             "smallButtonFont",
                                             startPosition + new Vector2(490, 0),
                                             90, 34, borderWidth,
                                             Color.White,
                                             Color.DimGray));

            settingsMenu.Add(new RadioButton(gameState.SetDescriptor, 
                                             "cellTemp",
                                             false,
                                             "Cell temp",
                                             "smallButtonFont",
                                             startPosition + new Vector2(595, 0),
                                             110, 34, borderWidth,
                                             Color.White,
                                             Color.DimGray));



            // FILE FUNCTIONALITIES MENU
            startPosition = new Vector2(graphicState.windowWidth - 180, graphicState.windowHeight - 87);
            fileFunctionsMenu.Add(new LoadSaveButton(game.SaveGame, 
                                                     "Save project", 
                                                     "smallButtonFont", 
                                                     startPosition, 
                                                     150, 35, borderWidth, 
                                                     Color.White, 
                                                     Color.DimGray));

            fileFunctionsMenu.Add(new LoadSaveButton(game.LoadGame, 
                                                     "Load project", 
                                                     "smallButtonFont", 
                                                     startPosition + new Vector2(0, 41), 
                                                     150, 35, borderWidth, 
                                                     Color.White, 
                                                     Color.DimGray));

            // DEFALUT MENU WITH ALL OPTIONS
            startPosition = new Vector2(15, graphicState.windowHeight - 70);
            defaultMenu.Add(new MenuButton(elementSelectMenu, "menu",
                                           "Elements", 
                                           "buttonFont", 
                                           startPosition, 
                                           110, 40, borderWidth, 
                                           Color.White, 
                                           Color.DimGray,
                                           "elements"));

            defaultMenu.Add(new MenuButton(drawStylesMenu, "active",
                                           "Draw options", 
                                           "buttonFont", 
                                           startPosition + new Vector2(120, 0), 
                                           150, 40, borderWidth, 
                                           Color.White, 
                                           Color.DimGray));

            defaultMenu.Add(new MenuButton(settingsMenu, "menu",
                                           "Settings", 
                                           "buttonFont", 
                                           startPosition + new Vector2(280, 0), 
                                           100, 40, borderWidth, 
                                           Color.White, 
                                           Color.DimGray));

            defaultMenu.Add(new MenuButton(fileFunctionsMenu, "active",
                                           "Files", 
                                           "buttonFont", 
                                           startPosition + new Vector2(390, 0), 
                                           100, 40, borderWidth, 
                                           Color.White, 
                                           Color.DimGray));


            
            SetMenuElements(defaultMenu);
        }
    }
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrainSim_v2
{
    class Graphics
    {
        GraphicState graphicState = GraphicState.instance;
        GameState gameState = GameState.instance;

        BoardGraphics boardGraphics;
        UIGraphics ui;

        GameMap gameMap;
        Shapes shapes;

        public Graphics(Game game, GameMap gameMap, SpriteBatch sb)
        {
            this.gameMap = gameMap;
            this.shapes = new Shapes(game, new Point(0, graphicState.windowHeight), sb);

            this.boardGraphics = new BoardGraphics(shapes);
            this.ui = new UIGraphics(shapes);
        }

        public void Render()
        {
            DrawBoard();
            DrawUI();
        }

        void DrawBoard()
        {
            if(graphicState.drawBoard)
                boardGraphics.DrawBoard();
            if(graphicState.drawStyle == GraphicState.DRAWSTYLES.PARTICLE)
            {
                boardGraphics.DrawParticles(gameMap.GetParticleMap());
                /* boardGraphics.DrawFluids(gameMap.GetFluidMap()); */
            }
            else if(graphicState.drawStyle == GraphicState.DRAWSTYLES.TEMPERATURE)
                boardGraphics.DrawTemperature(gameMap.GetTemperatureMap());
        }

        void DrawUI()
        {
            ui.DrawCursor();
            ui.DrawUIElements();
        }
    }
}

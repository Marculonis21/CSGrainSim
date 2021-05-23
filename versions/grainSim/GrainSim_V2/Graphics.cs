using System;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class Graphics
    {
        GameMap gameMap;
        Shapes shapes;

        BoardGraphics boardGraphics;
        UI ui;

        int winWidth;
        int winHeight;
        int particleSize;

        public Graphics(Game game, GameMap gameMap, int winWidth, int winHeight, int particleSize)
        {
            this.gameMap = gameMap;
            this.winHeight = winWidth;
            this.winHeight = winHeight;
            this.particleSize = particleSize;

            this.shapes = new Shapes(game, new Point(0, winHeight));

            this.boardGraphics = new BoardGraphics(shapes, winWidth, winHeight, particleSize);
        }

        public void Render(Vector2 mousePosition)
        {
            DrawBoard();
            DrawUI(mousePosition);
        }

        void DrawBoard()
        {
            
        }

        void DrawUI(Vector2 mousePosition)
        {
        }
    }
}

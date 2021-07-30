using Microsoft.Xna.Framework;

namespace GrainSim
{
    class UIGraphics
    {
        Shapes shapes;

        GraphicState graphicState = GraphicState.instance;
        GameState gameState = GameState.instance;
        UIManager uiManager = UIManager.instance;

        public UIGraphics(Shapes shapes)
        {
            this.shapes = shapes;
        }

        public void DrawCursor()
        {
            Point boardPos = gameState.cursorBoardPosition; 
            if(boardPos.X == -1) return;           
            int partSize = graphicState.particleSize;
            Color color = graphicState.cursorColor;
            int cursorSize = gameState.cursorSize;

            if(cursorSize == 0)
            {
                shapes.Begin();
                shapes.DrawRectangle(new Point(boardPos.X*partSize,
                                               boardPos.Y*partSize),
                                               partSize,
                                               partSize,
                                               color);
                shapes.End();
            }
            else
            {
                int offset;
                if(cursorSize < 20)
                    offset = 4;
                else if (cursorSize < 50)
                    offset = 6;
                else
                    offset = 10;

                shapes.Begin();
                for (int y = cursorSize; y > -cursorSize; y--)
                {
                    for (int x = -cursorSize; x < cursorSize; x++)
                    {
                        if(cursorSize+offset >= x*x + y*y && cursorSize-offset <= x*x + y*y)
                        {
                            int _x = boardPos.X + x;
                            int _y = boardPos.Y + y;

                            shapes.DrawRectangle(new Point(_x*partSize,
                                                           _y*partSize),
                                                 partSize,
                                                 partSize,
                                                 color);
                        }
                    }
                }
                shapes.End();
            }
        }

        public void DrawUIElements()
        {
            uiManager.DrawUI(shapes);
        }
    }
}

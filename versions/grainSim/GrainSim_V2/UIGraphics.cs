using System;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class UIGraphics
    {
        Shapes shapes;

        GraphicState graphicState = GraphicState.instance;
        GameState gameState = GameState.instance;

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



                /* int divisions; */
                /* if(cursorSize < 5) */
                /*     divisions = 40; */
                /* else if(cursorSize < 12) */
                /*     divisions = 72; */
                /* else if(cursorSize < 30) */
                /*     divisions = 180; */
                /* else */ 
                /*     divisions = 360; */

                /* double angle = (2*Math.PI)/divisions; */

                /* shapes.Begin(); */
                /* for (int i = 0; i < divisions; i++) */
                /* { */
                /*     int _x = boardPos.X + (int)(Math.Cos(angle*i)*cursorSize); */
                /*     int _y = boardPos.Y + (int)(Math.Sin(angle*i)*cursorSize); */
                /*     shapes.DrawRectangle(new Point(_x*partSize, */
                /*                                    _y*partSize), */
                /*                                    partSize, */
                /*                                    partSize, */
                /*                                    color); */
                /* } */
                /* shapes.End(); */
            }
        }
    }

}

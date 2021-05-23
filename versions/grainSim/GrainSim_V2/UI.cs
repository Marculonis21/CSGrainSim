using System;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class UI
    {
        int winWidth;
        int winHeight;
        int particleSize;

        Shapes shapes;

        public UI(Shapes shapes, int winWidth, int winHeight, int particleSize)
        {
            this.winWidth = winWidth;
            this.winHeight = winHeight;
            this.particleSize = particleSize;

            this.shapes = shapes;
        }

        public void DrawCursor(Vector2 position, int size, Color color)
        {
            Vector2 boardPos = new Vector2(position.X/particleSize, position.Y/particleSize);

            if(size == 0)
            {
                shapes.Begin();
                shapes.DrawRectangle(new Point ((int)boardPos.X*particleSize,
                                                (int)boardPos.Y*particleSize),
                                     particleSize,
                                     particleSize,color);
                shapes.End();
            }
            else
            {
                int divisions;
                if(size < 5)
                    divisions = 40;
                else if(size < 12)
                    divisions = 72;
                else if(size < 30)
                    divisions = 180;
                else 
                    divisions = 360;

                double angle = (2*Math.PI)/divisions;

                shapes.Begin();
                for (int i = 0; i < divisions; i++)
                {
                    double _x = boardPos.X + (Math.Cos(angle*i)*size);
                    double _y = boardPos.Y + (Math.Sin(angle*i)*size);
                    int __x = (int)Math.Floor(_x/particleSize);
                    int __y = (int)Math.Floor(_y/particleSize);
                    shapes.DrawRectangle(new Point((int)_x*particleSize,
                                                   (int)_y*particleSize),
                                        particleSize,
                                        particleSize,color);
                }
                shapes.End();
            }
        }
    }

}

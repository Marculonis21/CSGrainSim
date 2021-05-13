using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class GameScreen
    {
        int winWidth;
        int winHeight;
        int particleSize;

        Shapes shapes;

        public GameScreen(Game game, int winWidth, int winHeight, int particleSize)
        {
            this.winWidth = winWidth;
            this.winHeight = winHeight;
            this.particleSize = particleSize;
            
            shapes = new Shapes(game, new Point(0, winWidth));
        }

        public void DrawBoard()
        {
            shapes.Begin();
            for (int x = 0; x < winWidth/particleSize; x++)
            {
                shapes.DrawLine(new Point(particleSize*x,0), 
                                new Point(particleSize*x,winHeight),
                                1,Color.DimGray);

            }
            for (int y = 0; y < winHeight/particleSize; y++)
            {
                shapes.DrawLine(new Point(0,particleSize*y), 
                                new Point(winWidth,particleSize*y),
                                1,Color.DimGray);
            }
            shapes.End();
        }

        public void DrawParticles(ParticleMap partMap)
        {
            /* shapes.Begin(); */
            /* foreach (Particle particle in particles) */
            /* { */
            /*     particle.Draw(shapes,this.particleSize); */
            /* } */
            /* shapes.End(); */
        }

        public void DrawTemperature(TemperatureMap tempMap)
        {
            shapes.Begin();
            for (int y = 0; y < winHeight/particleSize; y++)
            {
                for (int x = 0; x < winWidth/particleSize; x++)
                {
                    Point pos = new Point(x,y);
                    float temp = tempMap.Get(pos);
                    if(temp > 0) 
                    {
                        shapes.DrawRectangle(new Point(pos.X*particleSize,
                                                       pos.Y*particleSize),
                                             particleSize,particleSize,
                                             new Color((int)((255/255)*temp),0,0));
                    }
                    else
                    {
                        shapes.DrawRectangle(new Point(pos.X*particleSize,
                                                       pos.Y*particleSize),
                                             particleSize,particleSize,
                                             new Color(0,0,(int)((255/255)*-temp)));
                    }
                }
            }

            shapes.End();
        }

        public void DrawCursor(Point position, int size, Color color)
        {
            Point boardPos = new Point(position.X/particleSize, position.Y/particleSize);

            if(size == 0)
            {
                shapes.Begin();
                shapes.DrawRectangle(new Point (boardPos.X*particleSize,
                                                boardPos.Y*particleSize),
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

                /* Console.Write("Div: " + divisions + ", "); */
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

        public Point CursorGridPosition(Vector2 position)
        {
            return (position/particleSize).ToPoint();
        }
    }
}


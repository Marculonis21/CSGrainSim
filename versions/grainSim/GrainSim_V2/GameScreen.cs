using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class GameScreen
    {
        int particleSize;
        int windowSize;

        Shapes shapes;

        public GameScreen(Game game, int particleSize, int windowSize)
        {
            this.particleSize = particleSize;
            this.windowSize = windowSize;
            
            shapes = new Shapes(game, new Vector2(0, windowSize));
        }

        public void DrawBoard()
        {
            shapes.Begin();
            for (int x = 0; x < this.windowSize/this.particleSize; x++)
            {
                shapes.DrawLine(new Vector2(this.particleSize*x,0), 
                                new Vector2(this.particleSize*x,this.windowSize),
                                1,Color.DimGray);

                shapes.DrawLine(new Vector2(0,this.particleSize*x), 
                                new Vector2(this.windowSize,this.particleSize*x),
                                1,Color.DimGray);
            }
            shapes.End();
        }

        public void DrawParticles(List<Particle> particles)
        {
            shapes.Begin();
            foreach (Particle particle in particles)
            {
                particle.Draw(shapes,this.particleSize);
            }
            shapes.End();
        }

        public void DrawCursor(Vector2 position, int size, Color color)
        {
            Vector2 boardPos = new Vector2(position.X/particleSize, position.Y/particleSize);

            if(size == 0)
            {
                shapes.Begin();
                shapes.DrawRectangle((int)boardPos.X*particleSize,
                                     (int)boardPos.Y*particleSize,
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
                    shapes.DrawRectangle((int)_x*particleSize,
                                        (int)_y*particleSize,
                                        particleSize,
                                        particleSize,color);
                }
                shapes.End();
            }
        }

        public Vector2 CursorGridPosition(Vector2 position)
        {
            return position/particleSize;
        }
    }
}


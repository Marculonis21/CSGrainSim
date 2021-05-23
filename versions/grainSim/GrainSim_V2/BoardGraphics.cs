using System;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class BoardGraphics
    {
        int winWidth;
        int winHeight;
        int particleSize;

        Shapes shapes;

        public BoardGraphics(Shapes shapes, int winWidth, int winHeight, int particleSize)
        {
            this.winWidth = winWidth;
            this.winHeight = winHeight;
            this.particleSize = particleSize;

            this.shapes = shapes;
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
            shapes.Begin();
            partMap.Render(shapes,particleSize);
            shapes.End();
        }

        public void DrawTemperature(TemperatureMap tempMap)
        {
            shapes.Begin();
            tempMap.Render(shapes, particleSize);

            shapes.End();
        }
    }
}


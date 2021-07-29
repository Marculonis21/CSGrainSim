using Microsoft.Xna.Framework;

namespace GrainSim
{
    class BoardGraphics
    {
        GraphicState graphicState;

        int winWidth;
        int winHeight;
        int particleSize;

        Shapes shapes;

        public BoardGraphics(Shapes shapes)
        {
            this.graphicState = GraphicState.instance;

            this.winWidth = graphicState.windowWidth;
            this.winHeight = graphicState.windowHeight;
            this.particleSize = graphicState.particleSize;

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
            partMap.Render(shapes, particleSize);
            shapes.End();
        }

        public void DrawTemperature(TemperatureMap tempMap)
        {
            shapes.Begin();
            tempMap.Render(shapes, particleSize);
            shapes.End();
        }

        /* public void DrawFluids(FluidMap fluidMap) */
        /* { */
        /*     shapes.Begin(); */
        /*     fluidMap.Render(shapes, particleSize); */
        /*     shapes.End(); */
        /* } */
    }
}


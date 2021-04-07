using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestProject
{
    public class GameScreen
    {
        /* Shapes shapes; */
        int particleSize;
        int windowSize;

        public GameScreen(Game game, int particleSize, int windowSize)
        {
            /* shapes = new Shapes(game); */

            this.particleSize = particleSize;
            this.windowSize = windowSize;
        }

        public void DrawMap()
        {
            Game1.shapes.Begin();
            for (int x = 0; x < this.windowSize/this.particleSize; x++)
            {
                Game1.shapes.DrawLine(new Vector2(this.particleSize*x,0), new Vector2(this.particleSize*x,this.windowSize),2,Color.White);
                Game1.shapes.DrawLine(new Vector2(0,this.particleSize*x), new Vector2(this.windowSize,this.particleSize*x),2,Color.White);
            }

            for (int y = (this.windowSize/this.particleSize)-1; y > -1; y--)
            {
                for (int x = 0; x < this.windowSize/this.particleSize; x++)
                {
                    Game1.shapes.DrawRectangle(x*particleSize,y*particleSize,particleSize,particleSize, Game1.elements[Block.Type(x,y)].Color);
                }
            }
            Game1.shapes.End();
        }
        public void DrawCursor(Vector2 position, Color color, bool debug)
        {
            int x = (int)position.X/particleSize;
            int y = (800-(int)position.Y)/particleSize;

            Game1.shapes.Begin();
            if (debug) Console.WriteLine("{0}, {1}", x.ToString(), y.ToString());
            Game1.shapes.DrawRectangle(x*particleSize,y*particleSize,particleSize,particleSize,color);
            Game1.shapes.End();
        }
        public Vector2 CursorGridPosition(Vector2 position)
        {
            int x = (int)position.X/particleSize;
            int y = (800-(int)position.Y)/particleSize;
            return new Vector2(x,y);
        }
    }
}

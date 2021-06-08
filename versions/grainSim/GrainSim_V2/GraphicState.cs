using System;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class GraphicState
    {
        /// SINGLETON
        public int windowWidth   {get; private set;} 
        public int windowHeight  {get; private set;}
        public int particleSize  {get; private set;}

        public int drawStyle     {get; private set;} 
        public bool drawBoard    {get; private set;} 
        public Color cursorColor {get; private set;} 

        private GraphicState(int drawStyle, bool drawBoard)
        {
            this.windowWidth  = 800;
            this.windowHeight = 500;
            this.particleSize = 5;

            this.drawStyle = drawStyle;
            this.drawBoard = drawBoard;
            this.cursorColor = Color.Red;
        }

        public static readonly GraphicState instance = new GraphicState(0, false);

        public void SetDrawStyle(int style)
        {
            this.drawStyle = style;
        }

        public void EnableBoard()
        {
            this.drawBoard = true;
        }
        public void DisableBoard()
        {
            this.drawBoard = false;
        }
    }
}

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrainSim
{
    class GraphicState
    {
        /// SINGLETON
        public int windowWidth   {get; private set;} 
        public int windowHeight  {get; private set;}
        public int particleSize  {get; private set;}

        public enum DRAWSTYLES
        {
            PARTICLE,
            TEMPERATURE,
            LIQUIDS,
        }

        public DRAWSTYLES drawStyle  {get; private set;} 
        public bool drawBoard        {get; set;} 
        public Color cursorColor     {get; private set;} 

        public Dictionary<string, SpriteFont> fonts {get; private set;} 

        private GraphicState()
        {
            this.windowWidth  = 800;
            this.windowHeight = 600;
            this.particleSize = 5;

            this.drawStyle = DRAWSTYLES.PARTICLE;
            this.drawBoard = false;
            this.cursorColor = Color.Red;

            fonts = new Dictionary<string, SpriteFont>();
        }

        public static readonly GraphicState instance = new GraphicState();

        public void SetDrawStyle(DRAWSTYLES style)
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

        public void AddFont(string key, SpriteFont font)
        {
            fonts.Add(key, font);
        }
    }
}

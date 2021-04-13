using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace grainSim
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        GameScreen screen;
        Vector2 mousePosition;

        int cursorSize;

        /* public static Shapes shapes; // Shape drawing class */
        public static Dictionary<ElementID, Element> elements; // All elements

        public static ElementID[,] particleMap;
        public static float[,] tempMap;
        List<Particle> particleList;

        const float startTemp = 20;

        const int windowSize = 800;
        const int particleSize = 10;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = false;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Window start
            _graphics.PreferredBackBufferWidth = windowSize;
            _graphics.PreferredBackBufferHeight = windowSize;
            _graphics.ApplyChanges();

            // Screen game stuff
            screen = new GameScreen(this,particleSize,windowSize);

            // Map start
            particleMap = new ElementID[windowSize/particleSize,windowSize/particleSize];
            tempMap = new float[windowSize/particleSize,windowSize/particleSize];
            for (int y = 0; y < windowSize/particleSize; y++)
            {
                for (int x = 0; x < windowSize/particleSize; x++)
                {
                    particleMap[x,y] = ElementID.AIR;
                    tempMap[x,y] = startTemp;
                }
            }

            // List of live blocks
            particleList = new List<Particle>();

            // ELEMENTS
            elements = new Dictionary<ElementID, Element>();
            elements.Add(ElementID.AIR, new Air());
            elements.Add(ElementID.ICE, new Ice());
            elements.Add(ElementID.SAND, new Sand());
            elements.Add(ElementID.WATER, new Water());

            /* foreach (var item in elements) */
            /* { */
            /*     Console.WriteLine(item.Value.Short); */
            /* } */

            cursorSize = 1;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            // Escape
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(Keyboard.GetState().IsKeyDown(Keys.P))
                if(cursorSize < 10)
                    cursorSize += 1;
            if(Keyboard.GetState().IsKeyDown(Keys.O))
                if(cursorSize > 0)
                    cursorSize -= 1;
        

            // MouseClick
            MouseState state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Pressed && 
                state.X >= 0 && state.X <= 800 &&
                state.Y >= 0 && state.Y <= 800)
            {
                mousePosition = new Vector2(state.X,state.Y);
                Vector2 pos = screen.CursorGridPosition(mousePosition);

                if(Element.Type((int)pos.X, (int)pos.Y) == ElementID.AIR)
                    particleList.Add(new Particle(ElementID.SAND, (int)pos.X, (int)pos.Y));
            }
            else
            {
                mousePosition = new Vector2(-1,-1);
            }

            for (int y = 0; y < windowSize/particleSize; y++)
            {
                for (int x = 0; x < windowSize/particleSize; x++)
                {
                    particleMap[x,y] = ElementID.AIR;
                }
            }

            Console.Write(particleList.Count + ", ");

            foreach (Particle particle in particleList) 
                particle.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            screen.DrawBoard();
            if(mousePosition.X != -1) 
                screen.DrawCursor(mousePosition, cursorSize, Color.Red);
            screen.DrawParticles(particleList);

            /* Console.WriteLine("TotalTime {0}; lastElapse {1}", gameTime.TotalGameTime.ToString(), gameTime.ElapsedGameTime.ToString()); */
            Console.Write((1.0f/gameTime.ElapsedGameTime.Milliseconds)*1000 + "\n");
            base.Draw(gameTime);
        }
    }
}

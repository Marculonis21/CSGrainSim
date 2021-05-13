using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GrainSim_v2
{
    public class MainGame : Game
    {
        // lets change all Vector2 to Points because of int!!!! 
        
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Random random = new Random();

        const int windowSize = 800;
        const int particleSize = 20;

        GameMap gameMap;
        GameScreen screen;
        Vector2 mousePosition;
        Point mouseBoardPosition;

        int cursorSize;
        
        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = windowSize;
            _graphics.PreferredBackBufferHeight = windowSize;
            _graphics.ApplyChanges();

            base.Initialize();

            Element.SetupElements();

            screen = new GameScreen(this, windowSize, windowSize, particleSize);
            gameMap = new GameMap(windowSize/particleSize,windowSize/particleSize);

            cursorSize = 1;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) // ESCAPE - QUIT
                Exit();

            // MouseClick
            MouseState state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Pressed && 
                state.X >= 0 && state.X <= windowSize &&
                state.Y >= 0 && state.Y <= windowSize )
            {
                mousePosition = new Vector2(state.X,state.Y);
                mouseBoardPosition = screen.CursorGridPosition(mousePosition);
                gameMap.GetTemperatureMap().Set(mouseBoardPosition, 500);
            }
            else if (state.RightButton == ButtonState.Pressed && 
                state.X >= 0 && state.X <= windowSize &&
                state.Y >= 0 && state.Y <= windowSize )
            {
                mousePosition = new Vector2(state.X,state.Y);
                mouseBoardPosition = screen.CursorGridPosition(mousePosition);
                gameMap.GetTemperatureMap().Set(mouseBoardPosition, -500);
            }
            else if (state.X >= 0 && state.X <= windowSize &&
                     state.Y >= 0 && state.Y <= windowSize )
            {
                mousePosition = new Vector2(state.X,state.Y);
                mouseBoardPosition = screen.CursorGridPosition(mousePosition);
            }
            else
            {
                mousePosition = new Vector2(-1,-1);
                mouseBoardPosition = new Point(-1,-1);
            }

            gameMap.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Console.Clear();
            GraphicsDevice.Clear(Color.Black);

            TemperatureMap tempMap = gameMap.GetTemperatureMap();
            ParticleMap partMap = gameMap.GetParticleMap();
            screen.DrawTemperature(tempMap);
            screen.DrawBoard();

            if(mouseBoardPosition.X != -1)
                Console.WriteLine("Cell " +mouseBoardPosition.X+";"+mouseBoardPosition.Y+")\nParticle: "+partMap.Type(mouseBoardPosition)+"\nTemperature: "+tempMap.Get(mouseBoardPosition));

            base.Draw(gameTime);
        }
    }
}

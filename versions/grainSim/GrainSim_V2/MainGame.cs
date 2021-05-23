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
        const int particleSize = 5;

        GameMap gameMap;
        /* GameScreen screen; */
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

            /* screen = new GameScreen(this, windowSize, windowSize, particleSize); */
            gameMap = new GameMap(windowSize/particleSize,windowSize/particleSize);

            cursorSize = 1;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        int drawStyle = 0;
        ElementID selected;
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) // ESCAPE - QUIT
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.F1)) // Enter - change draw style
                drawStyle = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.F2)) // Enter - change draw style
                drawStyle = 1;

            if (Keyboard.GetState().IsKeyDown(Keys.D0)) // Enter - change draw style
                selected = ElementID.WALL;
            if (Keyboard.GetState().IsKeyDown(Keys.D1)) // Enter - change draw style
                selected = ElementID.VOID;
            if (Keyboard.GetState().IsKeyDown(Keys.D2)) // Enter - change draw style
                selected = ElementID.SAND;
            if (Keyboard.GetState().IsKeyDown(Keys.D3)) // Enter - change draw style
                selected = ElementID.WATER;
            if (Keyboard.GetState().IsKeyDown(Keys.D4)) // Enter - change draw style
                selected = ElementID.COPPER;

            // MouseClick
            MouseState state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Pressed && 
                state.X >= 0 && state.X <= windowSize &&
                state.Y >= 0 && state.Y <= windowSize )
            {
                mousePosition = new Vector2(state.X,state.Y);
                /* mouseBoardPosition = screen.CursorGridPosition(mousePosition); */
                if(selected == ElementID.VOID)
                    gameMap.GetTemperatureMap().Increment(mouseBoardPosition, 100);
                else
                    gameMap.GetParticleMap().Spawn(selected, mouseBoardPosition);
            }
            else if (state.RightButton == ButtonState.Pressed && 
                state.X >= 0 && state.X <= windowSize &&
                state.Y >= 0 && state.Y <= windowSize )
            {
                mousePosition = new Vector2(state.X,state.Y);
                /* mouseBoardPosition = screen.CursorGridPosition(mousePosition); */
                gameMap.GetParticleMap().Delete(mouseBoardPosition);
            }
            else if (state.X >= 0 && state.X <= windowSize &&
                     state.Y >= 0 && state.Y <= windowSize )
            {
                mousePosition = new Vector2(state.X,state.Y);
                /* mouseBoardPosition = screen.CursorGridPosition(mousePosition); */
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

            /* if(drawStyle == 0) */
            /* { */
            /*     screen.DrawTemperature(tempMap); */
            /* } */
            /* else */
            /* { */
            /*     screen.DrawParticles(partMap); */
            /* } */
            /* screen.DrawBoard(); */
            /* if(mouseBoardPosition.X != -1) */
            /*     screen.DrawCursor(mousePosition, 6, Color.Red); */


            if(mouseBoardPosition.X != -1)
                Console.WriteLine("Draw style: "+drawStyle+"\nSelected: "+ selected + "\nCell " +mouseBoardPosition.X+";"+mouseBoardPosition.Y+")\nParticle: "+partMap.Type(mouseBoardPosition)+"\nTemperature: "+tempMap.Get(mouseBoardPosition));

            base.Draw(gameTime);
        }
    }
}

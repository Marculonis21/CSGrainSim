using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GrainSim_v2
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Random random = new Random();

        GameMap gameMap;
        Graphics graphics;

        GameState gameState = GameState.instance;
        GraphicState graphicState = GraphicState.instance;
        
        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = graphicState.windowWidth;
            _graphics.PreferredBackBufferHeight = graphicState.windowHeight;
            _graphics.ApplyChanges();
            base.Initialize();

            Element.SetupElements();

            gameMap = new GameMap(graphicState.windowWidth/graphicState.particleSize, 
                                  graphicState.windowHeight/graphicState.particleSize);
            graphics = new Graphics(this, 
                                    gameMap, 
                                    graphicState.windowWidth, 
                                    graphicState.windowHeight, 
                                    graphicState.particleSize);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) // ESCAPE - QUIT
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.F1)) // F1 - change draw style
                graphicState.SetDrawStyle(1);
            if (Keyboard.GetState().IsKeyDown(Keys.F2)) // F2 - change draw style
                graphicState.SetDrawStyle(1);

            if (Keyboard.GetState().IsKeyDown(Keys.D0)) // 0 - change element
                gameState.SelectElement(ElementID.WALL);
            if (Keyboard.GetState().IsKeyDown(Keys.D1)) // 1
                gameState.SelectElement(ElementID.VOID);
            if (Keyboard.GetState().IsKeyDown(Keys.D2)) // 2
                gameState.SelectElement(ElementID.SAND);
            if (Keyboard.GetState().IsKeyDown(Keys.D3)) // 3
                gameState.SelectElement(ElementID.WATER);
            if (Keyboard.GetState().IsKeyDown(Keys.D4)) // 4
                gameState.SelectElement(ElementID.COPPER);

            // MouseClick
            MouseState state = Mouse.GetState();

            if(state.X >= 0 && state.X <= graphicState.windowWidth &&
               state.Y >= 0 && state.Y <= graphicState.windowHeight)
            {
                gameState.SetCursorPosition(new Vector2(state.X, state.Y));
            }
            else
            {
                gameState.SetCursorPosition(new Vector2(-1, -1));
            }

            if (state.LeftButton == ButtonState.Pressed) 
            {
                if(gameState.currElement == ElementID.VOID)
                    gameMap.GetTemperatureMap().Increment(gameState.cursorBoardPosition, gameState.cursorSize, 100);
                else
                    gameMap.GetParticleMap().Spawn(gameState.currElement, gameState.cursorBoardPosition, gameState.cursorSize);
            }
            else if (state.RightButton == ButtonState.Pressed)
            {
                gameMap.GetParticleMap().Delete(gameState.cursorBoardPosition, gameState.cursorSize);
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

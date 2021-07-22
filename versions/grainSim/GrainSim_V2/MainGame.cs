﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GrainSim_v2
{
    class MainGame : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch spriteBatch;

        public static Random random = new Random();

        GameMap gameMap;
        ParticleMap partMap;
        TemperatureMap tempMap;
        /* FluidMap fluidMap; */

        Graphics graphics;

        GraphicState graphicState = GraphicState.instance;
        GameState gameState = GameState.instance;
        UIManager uiManager= UIManager.instance;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // graphics settings
            _graphics.PreferredBackBufferWidth = graphicState.windowWidth;
            _graphics.PreferredBackBufferHeight = graphicState.windowHeight;
            _graphics.ApplyChanges();

            base.Initialize();

            // elements setup
            Element.SetupElements();

            // gamemaps setting up
            gameMap = new GameMap(graphicState.windowWidth/graphicState.particleSize, 
                                  graphicState.windowHeight/graphicState.particleSize - 20);
            partMap = gameMap.GetParticleMap();
            tempMap = gameMap.GetTemperatureMap();

            // custom graphics class setup
            graphics = new Graphics(this, gameMap, spriteBatch);

            uiManager.Setup();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // load and add fonts to central dictionary
            SpriteFont font1 = Content.Load<SpriteFont>("Fonts/MainFont");
            SpriteFont font2 = Content.Load<SpriteFont>("Fonts/SecondaryFont");
            graphicState.AddFont("buttonFont", font1);
            graphicState.AddFont("smallButtonFont", font2);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) // ESCAPE - QUIT
                Exit();

            /* if (Keyboard.GetState().IsKeyDown(Keys.F1)) // F1 - change draw style */
            /*     graphicState.SetDrawStyle(GraphicState.DRAWSTYLES.PARTICLE); */
            /* if (Keyboard.GetState().IsKeyDown(Keys.F2)) // F2 - change draw style */
            /*     graphicState.SetDrawStyle(GraphicState.DRAWSTYLES.TEMPERATURE); */

            /* if (Keyboard.GetState().IsKeyDown(Keys.D0)) // 0 - change element */
            /*     gameState.SelectElement(ElementID.WALL); */
            /* if (Keyboard.GetState().IsKeyDown(Keys.D1)) // 1 */
            /*     gameState.SelectElement(ElementID.VOID); */
            /* if (Keyboard.GetState().IsKeyDown(Keys.D2)) // 2 */
            /*     gameState.SelectElement(ElementID.SAND); */
            /* if (Keyboard.GetState().IsKeyDown(Keys.D3)) // 3 */
            /*     gameState.SelectElement(ElementID.WATER); */
            /* if (Keyboard.GetState().IsKeyDown(Keys.D4)) // 4 */
            /*     gameState.SelectElement(ElementID.COPPER); */
            /* if (Keyboard.GetState().IsKeyDown(Keys.D5)) // 5 */
            /*     gameState.SelectElement(ElementID.FIRE); */
            /* if (Keyboard.GetState().IsKeyDown(Keys.D6)) // 6 */
            /*     gameState.SelectElement(ElementID.SMOKE); */

            if (Keyboard.GetState().IsKeyDown(Keys.Up)) // UP
                gameState.IncrementCursorSize();
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) // DOWN
                gameState.DecrementCursorSize();

            // MouseEvents 
            MouseState state = Mouse.GetState();
            if(state.X >= 0 && state.X <= graphicState.windowWidth &&
               state.Y >= 0 && state.Y <= graphicState.windowHeight)
            {
                gameState.SetCursorPosition(new Vector2(state.X, state.Y));
            } else
            {
                gameState.SetCursorPosition(new Vector2(-1, -1));
            }

            if (state.LeftButton == ButtonState.Pressed) 
            {
                if(!uiManager.CheckClick())
                {
                    switch (gameState.currElement)
                    {
                        //few special cases
                        case ElementID.HOT:
                            tempMap.Increment(gameState.cursorBoardPosition, gameState.cursorSize, 10);
                            break;
                        case ElementID.COLD:
                            tempMap.Increment(gameState.cursorBoardPosition, gameState.cursorSize, -10);
                            break;
                        case ElementID.ERASE:
                            partMap.Delete(gameState.cursorBoardPosition, gameState.cursorSize);
                            break;
                        case ElementID.ERASEP:
                            partMap.Delete(gameState.cursorBoardPosition, gameState.cursorSize, walls: false);
                            break;
                        default:
                            partMap.Spawn(gameState.currElement, gameState.cursorBoardPosition, gameState.cursorSize);
                            break;
                    }
                }
            }
            else if (state.RightButton == ButtonState.Pressed)
            {
                partMap.Delete(gameState.cursorBoardPosition, gameState.cursorSize, walls: false);
            }


            gameMap.Update();
            base.Update(gameTime);
        }

        float last;
        protected override void Draw(GameTime gameTime)
        {
            Console.Clear();
            Console.WriteLine((gameTime.TotalGameTime.Milliseconds - last).ToString());
            last = gameTime.TotalGameTime.Milliseconds;

            GraphicsDevice.Clear(Color.Black);

            if(gameState.cursorBoardPosition.X != -1)
                Console.WriteLine("Draw style: "+graphicState.drawStyle+
                                "\nSelected: "+gameState.currElement+ 
                                "\nCell "+gameState.cursorBoardPosition.X+";"+gameState.cursorBoardPosition.Y+
                               ")\nParticle: "+partMap.Type(gameState.cursorBoardPosition)+
                                "\nTemperature: "+tempMap.Get(gameState.cursorBoardPosition));

            graphics.Render();
            base.Draw(gameTime);
        }

        public void SaveGame()
        {

        }

        public void LoadGame()
        {
        }
    }
}

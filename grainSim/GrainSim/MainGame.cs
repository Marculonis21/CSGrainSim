using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GrainSim
{
    class MainGame : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch spriteBatch;

        public static Random random = new Random();

        bool clickEnabled = true;
        int clickTimeout = 20;
        int timer;

        int prevScrollWheelValue = 0; 

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
            this.Window.AllowUserResizing = false;

            // elements setup
            Element.SetupElements();

            // gamemaps setting up
            gameMap = new GameMap(graphicState.windowWidth/graphicState.particleSize, 
                                  graphicState.windowHeight/graphicState.particleSize - 20);
            partMap = gameMap.GetParticleMap();
            tempMap = gameMap.GetTemperatureMap();
            /* fluidMap = gameMap.GetFluidMap(); */

            // custom graphics class setup
            graphics = new Graphics(this, gameMap, spriteBatch);

            uiManager.Setup(this, partMap, tempMap);
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

            if (Keyboard.GetState().IsKeyDown(Keys.F1)) // F1 - change draw style
                graphicState.SetDrawStyle(GraphicState.DRAWSTYLES.PARTICLE); 
            if (Keyboard.GetState().IsKeyDown(Keys.F2)) // F2 - change draw style
                graphicState.SetDrawStyle(GraphicState.DRAWSTYLES.TEMPERATURE);

            if (Keyboard.GetState().IsKeyDown(Keys.Up)) // UP
                gameState.IncrementCursorSize();
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) // DOWN
                gameState.DecrementCursorSize();

            if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.LeftControl)) // SAVE GAME
                SaveGame();
            if (Keyboard.GetState().IsKeyDown(Keys.O) && Keyboard.GetState().IsKeyDown(Keys.LeftControl)) // LOAD GAME
                LoadGame();

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
                if(clickEnabled && uiManager.CheckClick())
                    clickEnabled = false;
                else
                {
                    switch (gameState.currElement)
                    {
                        //few special cases
                        case ElementID.HOT:
                            tempMap.Increment(gameState.cursorBoardPosition, gameState.cursorSize, 10);
                            break;
                        case ElementID.COLD:
                            tempMap.Increment(gameState.cursorBoardPosition, gameState.cursorSize, -5);
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

                            /* if(gameState.currElement == ElementID.WATER) */
                            /*     fluidMap.Set(gameState.cursorBoardPosition, gameState.cursorSize, ElementID.WATER); */
                            /* else */
                            /*     partMap.Spawn(gameState.currElement, gameState.cursorBoardPosition, gameState.cursorSize); */
                            /* break */
                    }
                }
            }
            else if (state.RightButton == ButtonState.Pressed)
            {
                partMap.Delete(gameState.cursorBoardPosition, gameState.cursorSize, walls: false);
            }

            // MOUSE WHEEL
            if(state.ScrollWheelValue > prevScrollWheelValue)
                gameState.IncrementCursorSize();
            else if (state.ScrollWheelValue < prevScrollWheelValue)
                gameState.DecrementCursorSize();
            prevScrollWheelValue = state.ScrollWheelValue;


            if(!clickEnabled)
                timer++;
            if(timer >= clickTimeout)
            {
                clickEnabled = true;
                timer = 0;
            }

            gameMap.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            graphics.Render();
            base.Draw(gameTime);
        }

        SaveContainer saveContainer;
        public void SaveGame()
        {
            gameMap.Save(out ElementID[,] saveP, out float[,] saveT);
            saveContainer = new SaveContainer(saveP, saveT);

            IFormatter formatter = new BinaryFormatter();  
            Stream stream = new FileStream("Save.grain", FileMode.Create, FileAccess.Write, FileShare.None);  
            formatter.Serialize(stream, saveContainer);  
            stream.Close();  

            Console.WriteLine("save");
        }

        public void LoadGame()
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();  
                Stream stream = new FileStream("Save.grain", FileMode.Open, FileAccess.Read, FileShare.Read);  
                SaveContainer saveContainer = (SaveContainer)formatter.Deserialize(stream);  
                stream.Close(); 

                this.gameMap.Load(saveContainer.saveParticles, saveContainer.saveTemps);

                Console.WriteLine("load");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("unable to load");
            }
        }
    }
}

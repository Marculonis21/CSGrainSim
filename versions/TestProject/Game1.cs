using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        GameScreen screen;
        Vector2 mousePosition;

        public static Dictionary<ElementID,Element> elements;
        public static Block[,] map;
        public static Shapes shapes;

        const int windowSize = 800;
        const int particleSize = 40;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            /* Content.RootDirectory = "Content"; */
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = windowSize;
            _graphics.PreferredBackBufferHeight = windowSize;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            shapes = new Shapes(this);
            screen = new GameScreen(this,particleSize,windowSize);

            elements = new Dictionary<ElementID, Element>();
            elements.Add(ElementID.AIR, new Air());
            elements.Add(ElementID.SAND, new Sand());
            elements.Add(ElementID.WATER, new Water());
            elements.Add(ElementID.ICE, new Ice());

            Console.WriteLine(elements[ElementID.SAND]);

            foreach (var item in elements)
            {
                Console.WriteLine(item.Value.Short);
            }

            mousePosition = new Vector2();

            map = new Block[windowSize/particleSize,windowSize/particleSize];
            for (int y = 0; y < windowSize/particleSize; y++)
            {
                for (int x = 0; x < windowSize/particleSize; x++)
                {
                    map[x,y] = new Block(ElementID.AIR);
                }
            }
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            /* if (Keyboard.GetState().IsKeyDown(Keys.M)) size *= 2; */
            /* if (Keyboard.GetState().IsKeyDown(Keys.N)) size /= 2; */

            MouseState state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Pressed && 
                state.X >= 0 && state.X <= 800 &&
                state.Y >= 0 && state.Y <= 800)
            {
                mousePosition = new Vector2(state.X,state.Y);
                /* Console.WriteLine("PRESSED"); */
                Vector2 pos = screen.CursorGridPosition(mousePosition);

                /* Game1.map[(int)pos.X,(int)pos.Y] = new Block(ElementID.SAND); */
                Game1.map[(int)pos.X,(int)pos.Y].Transform(ElementID.SAND);
            }
            else
            {
                mousePosition = new Vector2();
            }

            for (int y = 0; y < windowSize/particleSize; y++)
            {
                for (int x = 0; x < windowSize/particleSize; x++)
                {
                    if(Block.Type(x,y) == ElementID.SAND) Console.WriteLine("asdfho");
                    map[x,y].Update(x,y);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            screen.DrawMap();
            screen.DrawCursor(mousePosition,Color.Red, false);


            /* size = 2; //320000 vykreslování 60FPS STABILNÍ */
            /* shapes.Begin(); */
            /* for (int y = 0; y < 800/size; y++) */
            /* { */
            /*     for (int x = 0; x < 800/(size/2); x++) */
            /*     { */
            /*         shapes.DrawRectangle(size*2*x + (size*y)%(size*2), size*y, size, size, Color.Aqua); */
            /*     } */
            /* } */
            /* shapes.End(); */


            /* Console.WriteLine("TotalTime {0}; lastElapse {1}", gameTime.TotalGameTime.ToString(), gameTime.ElapsedGameTime.ToString()); */
            base.Draw(gameTime);
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestProject
{
    public class Block 
    {
        ElementID ID;
        float temperature;
        Vector2 velocity;

        public Block(ElementID ID)
        {
            this.ID = ID;
            this.temperature = Game1.elements[ID].STemp;
            this.velocity = new Vector2();
        }

        public static ElementID Type(int x, int y)
        {
            return Game1.map[x,y].ID;
        }

        public void Transform(ElementID id)
        {
            this.ID = id;
            this.temperature = Game1.elements[id].STemp;
            this.velocity = new Vector2();
        }

        public void Transform(Block next)
        {
            next.ID = this.ID;
            next.velocity = this.velocity;
            /* next.temperature = this.temperature ; */
            
            this.ID = ElementID.AIR;
            this.velocity = new Vector2();
            /* old.temperature = XXX */
        }

        bool CheckDownUpDir(int x,int y,float grav)
        {
            return ((grav > 0 && Game1.map[x,y+1].ID == ElementID.AIR) ||
                    (grav < 0 && Game1.map[x,y-1].ID == ElementID.AIR));
        }

        public void Update(int x, int y)
        {
            var element = Game1.elements[ID];
            if(element.Gravity == 0) return;

            int nX;
            int nY;
            if(CheckDownUpDir(x,y,element.Gravity))
            {
                velocity += new Vector2(0, element.Gravity);
                nX = (int)Math.Floor((double)x+velocity.X);
                nY = (int)Math.Floor((double)y+velocity.Y);
                Console.WriteLine(y+" "+nY);

                if(y != nY)
                    Transform(Game1.map[nX,nY]);
            }
            else 
            {
                velocity = new Vector2();
                nX = x;
                nY = y;

                List<int[]> list = element.AllowedMovement(nX,nY);
                Random random = new Random();
                int[] pos = list[random.Next(0, list.Count)];

                if (!(x == pos[0] && y == pos[1]))
                    Transform(Game1.map[pos[0],pos[1]]);
            }

            /* this.Draw(x, y); */
            /* this.Draw(nX, nY); */
        }

        void Draw(int x, int y)
        {
            Game1.shapes.DrawRectangle(x*40,y*40,40,40, Game1.elements[ID].Color);
        }
    }
}

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace grainSim
{
    public class Particle
    {
        ElementID ID;
        public ElementID id { get{ return ID; } }

        int posX;
        int posY;

        int lifeTime;

        public Particle(ElementID ID, int x, int y)
        {
            this.ID = ID;
            this.posX = x;
            this.posY = y;
            this.lifeTime = 0;
        }

        public void Update(List<Particle> particleList)
        {
            UpdatePosition(particleList);
            UpdateReaction();

            if(Element.elements[ID].MaxLifeTime > 0) lifeTime++;
        }

        public void UpdatePosition(List<Particle> particleList)
        {
            // Update - move position 
            Vector2 posNext = Element.elements[ID].PositionUpdate(posX, posY);

            int _x = (int)posNext.X;
            int _y = (int)posNext.Y;

            // Write into current particleMap + clear/change last position
            if(MainGame.particleMap[_x, _y] != ElementID.AIR)
            {
                foreach (Particle p in particleList)
                    if(p.posX == _x && p.posY == _y)
                    {
                        MainGame.particleMap[this.posX, this.posY] = p.ID;
                        p.posX = this.posX;
                        p.posY = this.posY;
                        break;
                    }
            }
            else
            {
                MainGame.particleMap[posX, posY] = ElementID.AIR;
            }

            this.posX = _x;
            this.posY = _y;
            MainGame.particleMap[posX, posY] = this.ID;
        }

        public void UpdateReaction()
        {
            ElementID result = Element.elements[ID].ReactionUpdate(posX, posY, lifeTime);

            MainGame.particleMap[this.posX, this.posY] = result;
            this.ID = result;
        }

        public void Draw(Shapes shapes, int particleSize)
        {
            shapes.DrawRectangle(posX*particleSize,
                                 posY*particleSize,
                                 particleSize,particleSize, 
                                 Element.elements[ID].Color);
        }
    }
}

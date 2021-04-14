using Microsoft.Xna.Framework;

namespace grainSim
{
    public class Particle
    {
        ElementID ID;
        /* Vector2 position; */
        int posX;
        int posY;

        public Particle(ElementID ID, int x, int y)
        {
            this.ID = ID;
            this.posX = x;
            this.posY = y;
        }

        public void Update()
        {
            // Update - move position 
            Vector2 posNext = Element.elements[ID].PositionUpdate(posX, posY);

            // Write into current particleMap + clear last position
            
            /* MainGame.particleMap[posX, posY] = MainGame.particleMap[(int)posNext.X,(int)posNext.Y]; */
            MainGame.particleMap[posX, posY] = ElementID.AIR;
            posX = (int)posNext.X;
            posY = (int)posNext.Y;
            MainGame.particleMap[posX, posY] = this.ID;
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

using Microsoft.Xna.Framework;

namespace grainSim
{
    public class Particle
    {
        ElementID ID;
        Vector2 position;

        public Particle(ElementID ID, int x, int y)
        {
            this.ID = ID;
            this.position = new Vector2(x,y);
        }

        public void Update()
        {
            // Update - move position 
            this.position = MainGame.elements[ID].Update(this.position);

            // Write into current particleMap
            MainGame.particleMap[(int)this.position.X,(int)this.position.Y] = this.ID;
        }

        public void Draw(Shapes shapes, int particleSize)
        {
            shapes.DrawRectangle((int)position.X*particleSize,
                                 (int)position.Y*particleSize,
                                 particleSize,particleSize, 
                                 MainGame.elements[ID].Color);
        }
    }
}

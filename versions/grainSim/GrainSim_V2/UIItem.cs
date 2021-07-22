using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    abstract class UIItem
    {
        protected string text;
        protected string font;
        protected Vector2 position;
        protected int width;
        protected int height;
        protected int borderWidth;
        protected int startBorderWidth;

        protected Color textColor;
        protected Color borderColor;

        public string info {get; protected set;}

        public UIItem(string text, string font, Vector2 position, int width, int height, int borderWidth, Color textColor, Color borderColor, string info="")
        {
            this.text = text;
            this.font = font;
            this.position = position;
            this.width = width;
            this.height = height;
            this.borderWidth = borderWidth;
            this.textColor = textColor;
            this.borderColor = borderColor;
            this.info = info;

            this.startBorderWidth = borderWidth;
        }

        abstract public void Click();

        public bool Collide(Vector2 mousePos)
        {
            return (mousePos.X > this.position.X && mousePos.Y > this.position.Y &&
                    mousePos.X < this.position.X + width && 
                    mousePos.Y < this.position.Y + height);
        }

        public bool Hover(Vector2 mousePos)
        {
            if(Collide(mousePos))
            {
                borderWidth = 2*startBorderWidth;
                return true;
            }
            else
                borderWidth = startBorderWidth;

            return false;
        }
        
        public virtual void Draw(Shapes shapes)
        {
            Hover(GameState.instance.cursorPosition);

            shapes.Begin();
            shapes.DrawBorder(position.ToPoint(), width, height, borderWidth, borderColor);
            shapes.End();

            shapes.DrawText(text, font, new Vector2(position.X+width/2+2, position.Y+height/2+2), textColor);
        }
    }
}

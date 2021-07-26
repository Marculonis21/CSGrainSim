using Microsoft.Xna.Framework;

namespace GrainSim
{
    class DrawStyleButton : UIItem
    {
        GraphicState.DRAWSTYLES style;

        public DrawStyleButton(GraphicState.DRAWSTYLES style,
                               string text, 
                               string font,
                               Vector2 position, 
                               int width, 
                               int height, 
                               int borderWidth, 
                               Color textColor, 
                               Color borderColor) : base(text, font, position, width, height, borderWidth, textColor, borderColor)
        {
            this.style = style;
        }

        public override void Click()
        {
            GraphicState.instance.SetDrawStyle(style);
        }
    }
}


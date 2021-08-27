using Microsoft.Xna.Framework;

namespace GrainSim
{
    class ElementButton : UIItem
    {
        ElementID toSelect;

        public ElementButton(ElementID toSelect,
                             string text, 
                             string font,
                             Vector2 position, 
                             int width, 
                             int height, 
                             int borderWidth, 
                             Color textColor, 
                             Color borderColor) : base(text, font, position, width, height, borderWidth, textColor, borderColor)
        {
            this.toSelect = toSelect;
        }

        public override void Click()
        {
            GameState.instance.SelectElement(this.toSelect);
        }
    }
}


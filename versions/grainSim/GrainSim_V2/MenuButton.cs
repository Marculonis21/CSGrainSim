using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class MenuButton : UIItem
    {
        List<UIItem> toActivate;

        public MenuButton(List<UIItem> toActivate,
                          string text, 
                          string font, 
                          Vector2 position, 
                          int width, 
                          int height, 
                          int borderWidth, 
                          Color textColor, 
                          Color borderColor) : base(text, font, position, width, height, borderWidth, textColor, borderColor)
        {
            this.toActivate = toActivate;
        }

        public override void Click()
        {
            UIManager.instance.SetActiveElements(toActivate);
            
        }
    }
}


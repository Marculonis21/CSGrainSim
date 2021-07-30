using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim
{
    class MenuButton : UIItem
    {
        List<UIItem> toActivate;
        string group;

        public MenuButton(List<UIItem> toActivate, string group,
                          string text, 
                          string font, 
                          Vector2 position, 
                          int width, 
                          int height, 
                          int borderWidth, 
                          Color textColor, 
                          Color borderColor,
                          string info="") : base(text, font, position, width, height, borderWidth, textColor, borderColor, info)
        {
            this.toActivate = toActivate;
            this.group = group;
        }

        public override void Click()
        {
            if(group == "menu")
            {
                UIManager.instance.SetMenuElements(toActivate);
                UIManager.instance.SetActiveElements(new List<UIItem>());
            }
            else if(group == "active")
                UIManager.instance.SetActiveElements(toActivate);
        }
    }
}


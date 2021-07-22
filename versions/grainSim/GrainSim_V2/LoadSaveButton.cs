using System;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class LoadSaveButton : UIItem
    {
        Action action;

        public LoadSaveButton(Action action,
                              string text, 
                              string font, 
                              Vector2 position, 
                              int width, 
                              int height, 
                              int borderWidth, 
                              Color textColor, 
                              Color borderColor) : base(text, font, position, width, height, borderWidth, textColor, borderColor)
        {
            this.action = action;
        }

        public override void Click()
        {
            action();
        }
    }
}


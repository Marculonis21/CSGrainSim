using System;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class RadioButton : UIItem
    {
        bool check;
        Action<string, bool> action;
        string desc;

        int checkedBorderWidth = 5;

        public RadioButton(Action<string, bool> action, 
                           string desc, 
                           bool check,
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
            this.desc = desc;
            this.check = check;
        }

        public override void Click()
        {
            check = !check;
            action(desc, check);
        }

        public override void Draw(Shapes shapes)
        {
            shapes.Begin();
            if(!Hover(GameState.instance.cursorPosition))
                shapes.DrawBorder(position.ToPoint(), width, height, check ? checkedBorderWidth : borderWidth, borderColor);
            else
                shapes.DrawBorder(position.ToPoint(), width, height, borderWidth, borderColor);


            shapes.End();

            shapes.DrawText(text, font, new Vector2(position.X+width/2+2, position.Y+height/2+2), textColor);
        }
    }
}


using System;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class GameState
    {
        /// SINGLETON
        public ElementID currElement     {get; private set;} 
        public Vector2 cursorPosition    {get; private set;} 
        public Point cursorBoardPosition {get; private set;} 
        public int cursorSize            {get; private set;} 

        private GraphicState graphicState;

        private GameState(ElementID selected, Vector2 position, Point cursorBoardPosition, int cursorSize)
        {
            this.currElement = selected;
            this.cursorPosition = position;
            this.cursorBoardPosition = cursorBoardPosition;
            this.cursorSize = cursorSize;

            this.graphicState = GraphicState.instance;
        }

        public static readonly GameState instance= new GameState(ElementID.SAND, new Vector2(-1,-1), new Point(-1,-1), 1);

        public void SelectElement(ElementID element)
        {
            this.currElement = element;
        }

        public void SetCursorPosition(Vector2 position)
        {
            if(position.X == -1)
            {
                this.cursorPosition = new Vector2(-1,-1);
                this.cursorBoardPosition = new Point(-1,-1);
            }
            else
            {
                this.cursorPosition = position;
                this.cursorBoardPosition = (position/graphicState.particleSize).ToPoint();
            }
        }

        public void IncrementCursorSize()
        {
            this.cursorSize++;
        }
        public void DecrementCursorSize()
        {
            this.cursorSize--;
        }
    }
}

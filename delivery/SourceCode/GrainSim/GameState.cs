using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim
{
    class GameState
    {
        /// SINGLETON
        public ElementID currElement      {get; private set;} 
        public Vector2 cursorPosition     {get; private set;} 
        public Point cursorBoardPosition  {get; private set;} 
        public int cursorSize             {get; private set;} 
        public Point boardBounds          {get; private set;}

        public Dictionary<string, bool> simDescriptors = new Dictionary<string, bool>();

        GraphicState graphicState;
        int maxCursorSize;

        private GameState(ElementID selected, Vector2 position, Point cursorBoardPosition, int cursorSize)
        {
            this.graphicState = GraphicState.instance;

            this.currElement = selected;
            this.cursorPosition = position;
            this.cursorBoardPosition = cursorBoardPosition;
            this.cursorSize = cursorSize;

            this.boardBounds = new Point(graphicState.windowWidth/graphicState.particleSize,
                                         graphicState.windowHeight/graphicState.particleSize);

            maxCursorSize = 100;

            simDescriptors.Add("drawStyle",false);
            simDescriptors.Add("drawBoard",false);
            simDescriptors.Add("selectedId",false);
            simDescriptors.Add("cellPos",false);
            simDescriptors.Add("cellId",false);
            simDescriptors.Add("cellTemp",false);
        }

        public static readonly GameState instance = new GameState(ElementID.SAND, new Vector2(-1,-1), new Point(-1,-1), 0);

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
            if(cursorSize < maxCursorSize)
                this.cursorSize += 3;
        }
        public void DecrementCursorSize()
        {
            if(cursorSize > 0)
                this.cursorSize -= 3;
        }

        public void SetDescriptor(string desc, bool value)
        {
            simDescriptors[desc] = value;
        }
    }
}

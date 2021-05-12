using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace grainSim
{
    public class Reaction
    {
        /// <summary>
        /// Describes a reaction transitioning one element to another.
        /// Transition can occure based on probability or having certain
        /// ammount of needed particles amount (+ probability);
        /// </summary>
        
        private ElementID FROM;
        private ElementID TO;

        private ElementID NEED; // may be VOID
        private int minNEEDAmount; // min needed extra particles

        private float probability;

        private Random random;

        public Reaction(ElementID FROM, ElementID TO, float probability) : this(FROM, TO, ElementID.VOID, 0, probability)
        {
        }

        public Reaction(ElementID FROM, ElementID TO, ElementID NEED, int minNEEDAmount, float probability)
        {
            this.FROM = FROM;
            this.TO = TO;
            this.NEED = NEED;
            this.minNEEDAmount = minNEEDAmount;
            this.probability = probability;
            this.random = MainGame.random;
        }

        public bool Eval(int x, int y, out ElementID result) // true if reaction occured and out is the result element
        {
            result = FROM;

            if(NEED == ElementID.VOID)
            {
                if(random.NextDouble() <= probability)
                    result = TO;
                    return true;
            }
            else
            {
                int bounds = MainGame.bounds;
                int occurence = 0;

                for (int _y = -1; _y < 3; _y++)
                    for (int _x = -1; _x < 3; _x++)
                        if(x+_x >= 0 && x+_x < bounds &&
                           y+_y >= 0 && y+_y < bounds)
                            if(MainGame.particleMap[x+_x,y+_y] == NEED)
                                occurence++;

                if(occurence >= minNEEDAmount)
                {
                    if(random.NextDouble() <= probability)
                        result = TO;
                        return true;
                }
            }

            return false;
        }
    }
}

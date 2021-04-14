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
            /* this.FROM = FROM; */
            /* this.TO = TO; */
            /* this.probability = probability; */
            /* this.random = random; */
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

        public ElementID Eval(int x, int y)
        {
            if(NEED == ElementID.VOID)
            {
                if(random.NextDouble() <= probability)
                    return TO;
            }
            else
            {
                int occurence = 0;

                for (int _y = -1; _y < 3; _y++)
                    for (int _x = -1; _x < 3; _x++)
                        if(MainGame.particleMap[x+_x,y+_y] == NEED)
                            occurence++;


                if(occurence >= minNEEDAmount)
                {
                    if(random.NextDouble() <= probability)
                        return TO;
                    else
                        return FROM;
                }
            }
            return FROM;
        }
    }
}

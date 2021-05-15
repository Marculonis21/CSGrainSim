using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim_v2
{
    class Reaction
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

        public bool Eval(Point pos, ParticleMap partMap, out ElementID result) // true if reaction occured and out is the result element
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
                int occurence = 0;

                for (int y = -1; y < 3; y++)
                    for (int x = -1; x < 3; x++)
                        if(partMap.InBounds(new Point(pos.X+x,pos.Y+y)))
                            if(partMap.Type(new Point(pos.X+x,pos.Y+y)) == NEED)
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

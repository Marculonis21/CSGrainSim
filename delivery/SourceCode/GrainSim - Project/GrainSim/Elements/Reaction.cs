using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GrainSim
{
    class Reaction
    {
        /// <summary>
        /// Describes a reaction transitioning one element to another.
        /// Transition can occure based on probability or having certain
        /// ammount of needed particles amount (+ probability);
        /// </summary>
        
        private ElementID FROM;
        private List<ElementID> TO;

        private ElementID NEED; // may be VOID
        private int minNEEDAmount; // min needed extra particles

        private float probability;

        private bool destroyOther;

        private Random random;

        public Reaction(ElementID FROM, List<ElementID> TO, float probability) : this(FROM, TO, ElementID.VOID, 0, probability)
        {
        }

        public Reaction(ElementID FROM, List<ElementID> TO, ElementID NEED, int minNEEDAmount, float probability, bool destroyOther = false)
        {
            this.FROM = FROM;
            this.TO = TO;
            this.NEED = NEED;
            this.minNEEDAmount = minNEEDAmount;
            this.probability = probability;
            this.destroyOther = destroyOther;
            this.random = MainGame.random;
        }

        public bool Eval(Point pos, ParticleMap partMap, out List<ElementID> result, out Point destroy) // true if reaction occured and out is the result element
        {
            result = new List<ElementID>() { FROM };
            destroy = new Point(-1, -1);

            if(NEED == ElementID.VOID) // time/prob based reactions
            {
                if(random.NextDouble() <= probability)
                {
                    result = TO;
                    return true;
                }
            }
            else if(NEED != ElementID.MOLTEN) // element based reactions
            {
                int occurence = 0;

                Point testPos = new Point();
                ElementID type;
                for (int y = -1; y < 2; y++)
                    for (int x = -1; x < 2; x++)
                    {
                        testPos.X = pos.X+x;
                        testPos.Y = pos.Y+y;
                        type = partMap.Type(testPos);

                        if(type == NEED)
                        {
                            occurence++;

                            if(destroyOther)
                                destroy = testPos;
                        }
                    }

                if(occurence >= minNEEDAmount)
                {
                    if(random.NextDouble() <= probability)
                    {
                        result = TO;
                        return true;
                    }
                }
            }
            else // special reaction with molten elements
            {
                ElementID moltenElement = ElementID.VOID;

                int occurence = 0;

                Point testPos = new Point();
                ElementID type;
                for (int y = -1; y < 2; y++)
                    for (int x = -1; x < 2; x++)
                    {
                        testPos.X = pos.X+x;
                        testPos.Y = pos.Y+y;
                        type = partMap.Type(testPos);

                        if(type == ElementID.COPPERMELT || type == ElementID.TINMELT || type == ElementID.BRONZEMELT)
                        {
                            occurence++;
                            moltenElement = type;

                            if(destroyOther)
                                destroy = testPos;
                        }
                    }

                if(occurence >= minNEEDAmount)
                {
                    if(random.NextDouble() <= probability)
                    {
                        if(TO[0] == ElementID.MOLTEN)
                            result = new List<ElementID>() {moltenElement};
                        else
                            result = TO;

                        return true;
                    }
                }
            }

            return false;
        }
    }
}

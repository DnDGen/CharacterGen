using D20Dice.Dice;
using NPCGen.Core.Characters.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPCGen.Core.Characters.Generation.Randomizers.Alignments
{
    public class NonNeutralAlignment : IAlignmentRandomizer
    {
        private IDice dice;

        public NonNeutralAlignment(IDice dice)
        {
            this.dice = dice;
        }

        public Alignment Randomize()
        {
            var alignment = new Alignment();

            do
            {
                alignment.Lawfulness = dice.d3(bonus: -2);

                var roll = dice.Percentile();
                if (roll <= 20)
                    alignment.Goodness = 1;
                else if (roll <= 50)
                    alignment.Goodness = 0;
                else
                    alignment.Goodness = -1;
            } while (alignment.Lawfulness == 0 || alignment.Goodness == 0);

            return alignment;
        }
    }
}
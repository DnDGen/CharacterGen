using D20Dice.Dice;
using NPCGen.Core.Characters.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPCGen.Core.Characters.Generation.Randomizers.Alignments
{
    public class ChaoticAlignment : IAlignmentRandomizer
    {
        private IDice dice;

        public ChaoticAlignment(IDice dice)
        {
            this.dice = dice;
        }

        public Alignment Randomize()
        {
            var alignment = new Alignment();

            alignment.Lawfulness = -1;

            var roll = dice.Percentile();
            if (roll <= 20)
                alignment.Goodness = 1;
            else if (roll <= 50)
                alignment.Goodness = 0;
            else
                alignment.Goodness = -1;

            return alignment;
        }
    }
}
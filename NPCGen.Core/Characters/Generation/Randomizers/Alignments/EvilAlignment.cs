using D20Dice.Dice;
using NPCGen.Core.Characters.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPCGen.Core.Characters.Generation.Randomizers.Alignments
{
    public class EvilAlignment : IAlignmentRandomizer
    {
        private IDice dice;

        public EvilAlignment(IDice dice)
        {
            this.dice = dice;
        }

        public Alignment Randomize()
        {
            var alignment = new Alignment();

            alignment.Lawfulness = dice.d3(bonus: -2);
            alignment.Goodness = -1;

            return alignment;
        }
    }
}
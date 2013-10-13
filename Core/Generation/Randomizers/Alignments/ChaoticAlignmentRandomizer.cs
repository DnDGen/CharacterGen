﻿using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Alignments
{
    public class ChaoticAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public ChaoticAlignmentRandomizer(IDice dice, IPercentileResultProvider provider) : base(dice, provider) { }

        public override Alignment Randomize()
        {
            var alignment = new Alignment();

            alignment.Lawfulness = AlignmentConstants.Chaotic;
            alignment.Goodness = RollGoodness();

            return alignment;
        }
    }
}
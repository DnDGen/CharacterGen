﻿using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using System;
using System.Collections.Generic;

namespace NPCGen.Core.Generation.Randomizers.Alignments
{
    public class NonEvilAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public NonEvilAlignmentRandomizer(IDice dice, IPercentileResultProvider provider) : base(dice, provider) { }

        public override Alignment Randomize()
        {
            var alignment = new Alignment();

            alignment.Lawfulness = RollLawfulness();

            do alignment.Goodness = RollGoodness();
            while (alignment.IsEvil());

            return alignment;
        }

        public override IEnumerable<Alignment> GetAllPossibleResults()
        {
            throw new NotImplementedException();
        }
    }
}
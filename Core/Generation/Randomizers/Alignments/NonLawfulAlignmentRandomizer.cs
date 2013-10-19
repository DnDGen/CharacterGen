using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using System;
using System.Collections.Generic;

namespace NPCGen.Core.Generation.Randomizers.Alignments
{
    public class NonLawfulAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public NonLawfulAlignmentRandomizer(IDice dice, IPercentileResultProvider provider) : base(dice, provider) { }

        public override Alignment Randomize()
        {
            var alignment = new Alignment();

            do alignment.Lawfulness = RollLawfulness();
            while (alignment.IsLawful());

            alignment.Goodness = RollGoodness();

            return alignment;
        }

        public override IEnumerable<Alignment> GetAllPossibleResults()
        {
            throw new NotImplementedException();
        }
    }
}
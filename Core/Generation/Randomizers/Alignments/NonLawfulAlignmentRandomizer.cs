using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
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
            var goodnesses = GetAllGoodnesses();
            var alignments = new List<Alignment>();

            foreach (var goodness in goodnesses)
            {
                alignments.Add(new Alignment() { Goodness = goodness, Lawfulness = AlignmentConstants.Neutral });
                alignments.Add(new Alignment() { Goodness = goodness, Lawfulness = AlignmentConstants.Chaotic });
            }

            return alignments;
        }
    }
}
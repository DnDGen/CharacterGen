using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using System.Collections.Generic;

namespace NPCGen.Core.Generation.Randomizers.Alignments
{
    public class GoodAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public GoodAlignmentRandomizer(IDice dice, IPercentileResultProvider provider) : base(dice, provider) { }

        public override Alignment Randomize()
        {
            var alignment = new Alignment();

            alignment.Lawfulness = RollLawfulness();
            alignment.Goodness = AlignmentConstants.Good;

            return alignment;
        }

        public override IEnumerable<Alignment> GetAllPossibleResults()
        {
            var alignments = new List<Alignment>();

            alignments.Add(new Alignment() { Goodness = AlignmentConstants.Good, Lawfulness = AlignmentConstants.Chaotic });
            alignments.Add(new Alignment() { Goodness = AlignmentConstants.Good, Lawfulness = AlignmentConstants.Neutral });
            alignments.Add(new Alignment() { Goodness = AlignmentConstants.Good, Lawfulness = AlignmentConstants.Lawful });

            return alignments;
        }
    }
}
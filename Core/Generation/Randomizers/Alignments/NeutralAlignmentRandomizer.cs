using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using System.Collections.Generic;

namespace NPCGen.Core.Generation.Randomizers.Alignments
{
    public class NeutralAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public NeutralAlignmentRandomizer(IDice dice, IPercentileResultProvider provider) : base(dice, provider) { }

        public override Alignment Randomize()
        {
            var alignment = new Alignment();

            do
            {
                alignment.Lawfulness = RollLawfulness();
                alignment.Goodness = RollGoodness();
            } while (!alignment.IsNeutral());

            return alignment;
        }

        public override IEnumerable<Alignment> GetAllPossibleResults()
        {
            var alignments = new List<Alignment>();

            alignments.Add(new Alignment() { Goodness = AlignmentConstants.Good, Lawfulness = AlignmentConstants.Neutral });
            alignments.Add(new Alignment() { Goodness = AlignmentConstants.Evil, Lawfulness = AlignmentConstants.Neutral });
            alignments.Add(new Alignment() { Goodness = AlignmentConstants.Neutral, Lawfulness = AlignmentConstants.Neutral });
            alignments.Add(new Alignment() { Goodness = AlignmentConstants.Neutral, Lawfulness = AlignmentConstants.Lawful });
            alignments.Add(new Alignment() { Goodness = AlignmentConstants.Neutral, Lawfulness = AlignmentConstants.Chaotic });

            return alignments;
        }
    }
}
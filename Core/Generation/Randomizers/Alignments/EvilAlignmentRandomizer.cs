using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using System.Collections.Generic;

namespace NPCGen.Core.Generation.Randomizers.Alignments
{
    public class EvilAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public EvilAlignmentRandomizer(IDice dice, IPercentileResultProvider provider) : base(dice, provider) { }

        public override Alignment Randomize()
        {
            var alignment = new Alignment();

            alignment.Lawfulness = RollLawfulness();
            alignment.Goodness = AlignmentConstants.Evil;

            return alignment;
        }

        public override IEnumerable<Alignment> GetAllPossibleResults()
        {
            var alignments = new List<Alignment>();

            alignments.Add(new Alignment() { Goodness = AlignmentConstants.Evil, Lawfulness = AlignmentConstants.Chaotic });
            alignments.Add(new Alignment() { Goodness = AlignmentConstants.Evil, Lawfulness = AlignmentConstants.Neutral });
            alignments.Add(new Alignment() { Goodness = AlignmentConstants.Evil, Lawfulness = AlignmentConstants.Lawful });

            return alignments;
        }
    }
}
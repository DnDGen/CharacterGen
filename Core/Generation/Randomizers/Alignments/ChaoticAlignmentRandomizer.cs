using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using System.Collections.Generic;

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

        public override IEnumerable<Alignment> GetAllPossibleResults()
        {
            var goodnesses = GetAllGoodnesses();
            var alignments = new List<Alignment>();

            foreach (var goodness in goodnesses)
                alignments.Add(new Alignment() { Goodness = goodness, Lawfulness = AlignmentConstants.Chaotic });

            return alignments;
        }
    }
}
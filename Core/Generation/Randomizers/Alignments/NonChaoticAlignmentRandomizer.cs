using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Alignments
{
    public class NonChaoticAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public NonChaoticAlignmentRandomizer(IDice dice, IPercentileResultProvider provider) : base(dice, provider) { }

        public override Alignment Randomize()
        {
            var alignment = new Alignment();

            do alignment.Lawfulness = RollLawfulness();
            while (alignment.IsChaotic());

            alignment.Goodness = RollGoodness();

            return alignment;
        }
    }
}
using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Alignments
{
    public class LawfulAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public LawfulAlignmentRandomizer(IDice dice, IPercentileResultProvider provider) : base(dice, provider) { }

        public override Alignment Randomize()
        {
            var alignment = new Alignment();

            alignment.Lawfulness = AlignmentConstants.Lawful;
            alignment.Goodness = RollGoodness();

            return alignment;
        }
    }
}
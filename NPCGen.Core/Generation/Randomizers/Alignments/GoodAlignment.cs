using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.Alignments
{
    public class GoodAlignment : BaseAlignmentRandomizer
    {
        public GoodAlignment(IDice dice) : base(dice) { }

        public override Alignment Randomize()
        {
            var alignment = new Alignment();

            alignment.Lawfulness = RollLawfulness();
            alignment.Goodness = AlignmentConstants.Good;

            return alignment;
        }
    }
}
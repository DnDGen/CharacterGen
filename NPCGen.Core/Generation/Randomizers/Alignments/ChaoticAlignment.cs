using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.Alignments
{
    public class ChaoticAlignment : BaseAlignmentRandomizer
    {
        public ChaoticAlignment(IDice dice) : base(dice) { }

        public override Alignment Randomize()
        {
            var alignment = new Alignment();

            alignment.Lawfulness = AlignmentConstants.Chaotic;
            alignment.Goodness = RollGoodness();

            return alignment;
        }
    }
}
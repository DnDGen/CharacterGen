using D20Dice.Dice;
using NPCGen.Core.Characters.Data.Alignments;

namespace NPCGen.Core.Characters.Generation.Randomizers.Alignments
{
    public class ChaoticAlignment : BaseAlignmentRandomizer
    {
        public ChaoticAlignment(IDice dice) : base(dice) { }

        public Alignment Randomize()
        {
            var alignment = new Alignment();

            alignment.Lawfulness = AlignmentConstants.Chaotic;
            alignment.Goodness = RollGoodness();

            return alignment;
        }
    }
}
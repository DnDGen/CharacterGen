using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.Alignments
{
    public class LawfulAlignment : BaseAlignmentRandomizer
    {
        public LawfulAlignment(IDice dice) : base(dice) { }

        public override Alignment Randomize()
        {
            var alignment = new Alignment();

            alignment.Lawfulness = AlignmentConstants.Lawful;
            alignment.Goodness = RollGoodness();

            return alignment;
        }
    }
}
using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.Alignments
{
    public class AnyAlignment : BaseAlignmentRandomizer
    {
        public AnyAlignment(IDice dice) : base(dice) { }

        public override Alignment Randomize()
        {
            var alignment = new Alignment();

            alignment.Lawfulness = RollLawfulness();
            alignment.Goodness = RollGoodness();

            return alignment;
        }
    }
}
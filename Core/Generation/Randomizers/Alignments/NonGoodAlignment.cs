using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.Alignments
{
    public class NonGoodAlignment : BaseAlignmentRandomizer
    {
        public NonGoodAlignment(IDice dice) : base(dice) { }

        public override Alignment Randomize()
        {
            var alignment = new Alignment();

            alignment.Lawfulness = RollLawfulness();

            do alignment.Goodness = RollGoodness();
            while (alignment.IsGood());

            return alignment;
        }
    }
}
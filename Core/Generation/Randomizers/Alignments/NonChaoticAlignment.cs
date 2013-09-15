using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.Alignments
{
    public class NonChaoticAlignment : BaseAlignmentRandomizer
    {
        public NonChaoticAlignment(IDice dice) : base(dice) { }

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
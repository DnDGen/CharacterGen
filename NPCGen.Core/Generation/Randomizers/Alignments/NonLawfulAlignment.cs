using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.Alignments
{
    public class NonLawfulAlignment : BaseAlignmentRandomizer
    {
        public NonLawfulAlignment(IDice dice) : base(dice) { }

        public override Alignment Randomize()
        {
            var alignment = new Alignment();

            do alignment.Lawfulness = RollLawfulness();
            while (alignment.IsLawful());

            alignment.Goodness = RollGoodness();

            return alignment;
        }
    }
}
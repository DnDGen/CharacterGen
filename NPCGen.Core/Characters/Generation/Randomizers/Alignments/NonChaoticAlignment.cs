using D20Dice.Dice;
using NPCGen.Core.Characters.Data.Alignments;

namespace NPCGen.Core.Characters.Generation.Randomizers.Alignments
{
    public class NonChaoticAlignment : BaseAlignmentRandomizer
    {
        public NonChaoticAlignment(IDice dice) : base(dice) { }

        public Alignment Randomize()
        {
            var alignment = new Alignment();

            do alignment.Lawfulness = RollLawfulness();
            while (alignment.IsChaotic());

            alignment.Goodness = RollGoodness();

            return alignment;
        }
    }
}
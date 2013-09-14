using D20Dice.Dice;
using NPCGen.Core.Characters.Data.Alignments;

namespace NPCGen.Core.Characters.Generation.Randomizers.Alignments
{
    public class AnyAlignment : BaseAlignmentRandomizer
    {
        public AnyAlignment(IDice dice) : base(dice) { }

        public Alignment Randomize()
        {
            var alignment = new Alignment();

            alignment.Lawfulness = RollLawfulness();
            alignment.Goodness = RollGoodness();

            return alignment;
        }
    }
}
using D20Dice.Dice;
using NPCGen.Core.Characters.Data.Alignments;

namespace NPCGen.Core.Characters.Generation.Randomizers.Alignments
{
    public class GoodAlignment : BaseAlignmentRandomizer
    {
        public GoodAlignment(IDice dice) : base(dice) { }

        public Alignment Randomize()
        {
            var alignment = new Alignment();

            alignment.Lawfulness = RollLawfulness();
            alignment.Goodness = AlignmentConstants.Good;

            return alignment;
        }
    }
}
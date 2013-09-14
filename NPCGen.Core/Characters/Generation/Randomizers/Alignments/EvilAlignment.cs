using D20Dice.Dice;
using NPCGen.Core.Characters.Data.Alignments;

namespace NPCGen.Core.Characters.Generation.Randomizers.Alignments
{
    public class EvilAlignment : BaseAlignmentRandomizer
    {
        public EvilAlignment(IDice dice) : base(dice) { }

        public Alignment Randomize()
        {
            var alignment = new Alignment();

            alignment.Lawfulness = RollLawfulness();
            alignment.Goodness = AlignmentConstants.Evil;

            return alignment;
        }
    }
}
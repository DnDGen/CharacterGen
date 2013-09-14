using D20Dice.Dice;
using NPCGen.Core.Characters.Data.Alignments;

namespace NPCGen.Core.Characters.Generation.Randomizers.Alignments
{
    public class LawfulAlignment : BaseAlignmentRandomizer
    {
        public LawfulAlignment(IDice dice) : base(dice) { }

        public Alignment Randomize()
        {
            var alignment = new Alignment();

            alignment.Lawfulness = AlignmentConstants.Lawful;
            alignment.Goodness = RollGoodness();

            return alignment;
        }
    }
}
using D20Dice.Dice;
using NPCGen.Core.Characters.Data.Alignments;

namespace NPCGen.Core.Characters.Generation.Randomizers.Alignments
{
    public class NonEvilAlignment : BaseAlignmentRandomizer
    {
        public NonEvilAlignment(IDice dice) : base(dice) { }

        public Alignment Randomize()
        {
            var alignment = new Alignment();

            alignment.Lawfulness = RollLawfulness();

            do alignment.Goodness = RollGoodness();
            while (alignment.IsEvil());

            return alignment;
        }
    }
}
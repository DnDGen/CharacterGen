using D20Dice.Dice;
using NPCGen.Core.Characters.Data.Alignments;

namespace NPCGen.Core.Characters.Generation.Randomizers.Alignments
{
    public class NonGoodAlignment : BaseAlignmentRandomizer
    {
        public NonGoodAlignment(IDice dice) : base(dice) { }

        public Alignment Randomize()
        {
            var alignment = new Alignment();

            alignment.Lawfulness = RollLawfulness();

            do alignment.Goodness = RollGoodness();
            while (alignment.IsGood());

            return alignment;
        }
    }
}
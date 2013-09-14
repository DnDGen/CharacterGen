using D20Dice.Dice;
using NPCGen.Core.Characters.Data.Alignments;

namespace NPCGen.Core.Characters.Generation.Randomizers.Alignments
{
    public class NonLawfulAlignment : BaseAlignmentRandomizer
    {
        public NonLawfulAlignment(IDice dice) : base(dice) { }

        public Alignment Randomize()
        {
            var alignment = new Alignment();

            do alignment.Lawfulness = RollLawfulness();
            while (alignment.IsLawful());

            alignment.Goodness = RollGoodness();

            return alignment;
        }
    }
}
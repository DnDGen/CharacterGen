using D20Dice.Dice;
using NPCGen.Core.Characters.Data.Alignments;

namespace NPCGen.Core.Characters.Generation.Randomizers.Alignments
{
    public class NeutralAlignment : BaseAlignmentRandomizer
    {
        public NeutralAlignment(IDice dice) : base(dice) { }

        public Alignment Randomize()
        {
            var alignment = new Alignment();

            do
            {
                alignment.Lawfulness = RollLawfulness();
                alignment.Goodness = RollGoodness();
            } while (!alignment.IsNeutral());

            return alignment;
        }
    }
}
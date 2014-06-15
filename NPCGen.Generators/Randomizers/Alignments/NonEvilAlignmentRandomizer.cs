using System;
using D20Dice;
using NPCGen.Common.Alignments;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Alignments
{
    public class NonEvilAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public NonEvilAlignmentRandomizer(IDice dice, IPercentileSelector provider) : base(dice, provider) { }

        protected override Boolean AlignmentIsAllowed(Alignment alignment)
        {
            return !alignment.IsEvil();
        }
    }
}
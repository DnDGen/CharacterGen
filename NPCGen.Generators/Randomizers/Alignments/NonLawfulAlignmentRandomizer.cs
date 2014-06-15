using System;
using D20Dice;
using NPCGen.Common.Alignments;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Alignments
{
    public class NonLawfulAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public NonLawfulAlignmentRandomizer(IDice dice, IPercentileSelector provider) : base(dice, provider) { }

        protected override Boolean AlignmentIsAllowed(Alignment alignment)
        {
            return !alignment.IsLawful();
        }
    }
}
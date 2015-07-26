using System;
using RollGen;
using CharacterGen.Common.Alignments;
using CharacterGen.Selectors;

namespace CharacterGen.Generators.Domain.Randomizers.Alignments
{
    public class LawfulAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public LawfulAlignmentRandomizer(IDice dice, IPercentileSelector Selector) : base(dice, Selector) { }

        protected override Boolean AlignmentIsAllowed(Alignment alignment)
        {
            return alignment.Lawfulness == AlignmentConstants.Lawful;
        }
    }
}
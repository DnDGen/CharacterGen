using CharacterGen.Common.Alignments;
using CharacterGen.Selectors;
using RollGen;
using System;

namespace CharacterGen.Generators.Domain.Randomizers.Alignments
{
    public class AnyAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public AnyAlignmentRandomizer(IDice dice, IPercentileSelector Selector) : base(dice, Selector) { }

        protected override Boolean AlignmentIsAllowed(Alignment alignment)
        {
            return true;
        }
    }
}
using System;
using D20Dice;
using NPCGen.Common.Alignments;
using NPCGen.Generators.Providers.Interfaces;

namespace NPCGen.Generators.Randomizers.Alignments
{
    public class NonChaoticAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public NonChaoticAlignmentRandomizer(IDice dice, IPercentileResultProvider provider) : base(dice, provider) { }

        protected override Boolean AlignmentIsAllowed(Alignment alignment)
        {
            return !alignment.IsChaotic();
        }
    }
}
using System;
using D20Dice;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;

namespace NPCGen.Core.Generation.Randomizers.Alignments
{
    public class NonGoodAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public NonGoodAlignmentRandomizer(IDice dice, IPercentileResultProvider provider) : base(dice, provider) { }

        protected override Boolean AlignmentIsAllowed(Alignment alignment)
        {
            return !alignment.IsGood();
        }
    }
}
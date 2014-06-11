﻿using System;
using D20Dice;
using NPCGen.Common.Alignments;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Alignments
{
    public class AnyAlignmentRandomizer : BaseAlignmentRandomizer
    {
        public AnyAlignmentRandomizer(IDice dice, IPercentileResultProvider provider) : base(dice, provider) { }

        protected override Boolean AlignmentIsAllowed(Alignment alignment)
        {
            return true;
        }
    }
}
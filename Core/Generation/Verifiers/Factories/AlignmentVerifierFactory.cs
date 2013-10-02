using System;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NPCGen.Core.Generation.Verifiers.Alignments;
using NPCGen.Core.Generation.Verifiers.Interfaces;

namespace NPCGen.Core.Generation.Verifiers.Factories
{
    public class AlignmentVerifierFactory
    {
        public static IAlignmentVerifier CreateUsing(IAlignmentRandomizer alignmentRandomizer)
        {
            if (alignmentRandomizer is AnyAlignmentRandomizer)
                throw new NotImplementedException();
            else if (alignmentRandomizer is ChaoticAlignmentRandomizer)
                return new ChaoticAlignmentVerifier();
            else if (alignmentRandomizer is EvilAlignmentRandomizer)
                return new EvilAlignmentVerifier();
            else if (alignmentRandomizer is GoodAlignmentRandomizer)
                return new GoodAlignmentVerifier();
            else if (alignmentRandomizer is LawfulAlignmentRandomizer)
                return new LawfulAlignmentVerifier();
            else if (alignmentRandomizer is NeutralAlignmentRandomizer)
                return new NeutralAlignmentVerifier();
            else if (alignmentRandomizer is NonChaoticAlignmentRandomizer)
                throw new NotImplementedException();
            else if (alignmentRandomizer is NonEvilAlignmentRandomizer)
                throw new NotImplementedException();
            else if (alignmentRandomizer is NonGoodAlignmentRandomizer)
                throw new NotImplementedException();
            else if (alignmentRandomizer is NonLawfulAlignmentRandomizer)
                throw new NotImplementedException();
            else if (alignmentRandomizer is NonNeutralAlignmentRandomizer)
                throw new NotImplementedException();
            else if (alignmentRandomizer is SetAlignmentRandomizer)
                throw new NotImplementedException();

            throw new ArgumentOutOfRangeException();
        }
    }
}
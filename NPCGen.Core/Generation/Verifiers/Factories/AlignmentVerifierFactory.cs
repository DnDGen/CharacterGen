using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Verifiers.Alignments;
using NPCGen.Core.Generation.Verifiers.Factories.Interfaces;
using System;

namespace NPCGen.Core.Generation.Verifiers.Factories
{
    public class AlignmentVerifierFactory : IAlignmentVerifierFactory
    {
        public IAlignmentVerifier Create(IAlignmentRandomizer alignmentRandomizer)
        {
            if (alignmentRandomizer is ChaoticAlignment)
                return new ChaoticAlignmentVerifier();
            else if (alignmentRandomizer is EvilAlignment)
                return new EvilAlignmentVerifier();
            else if (alignmentRandomizer is GoodAlignment)
                throw new NotImplementedException();
            else if (alignmentRandomizer is LawfulAlignment)
                return new LawfulAlignmentVerifier();
            else if (alignmentRandomizer is NeutralAlignment)
                return new NeutralAlignmentVerifier();
            else if (alignmentRandomizer is NonChaoticAlignment)
                throw new NotImplementedException();
            else if (alignmentRandomizer is NonEvilAlignment)
                throw new NotImplementedException();
            else if (alignmentRandomizer is NonGoodAlignment)
                throw new NotImplementedException();
            else if (alignmentRandomizer is NonLawfulAlignment)
                throw new NotImplementedException();
            else if (alignmentRandomizer is NonNeutralAlignment)
                throw new NotImplementedException();
            else if (alignmentRandomizer is SetAlignment)
                throw new NotImplementedException();

            throw new ArgumentOutOfRangeException();
        }
    }
}
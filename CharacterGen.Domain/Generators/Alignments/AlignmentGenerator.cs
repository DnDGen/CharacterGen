using CharacterGen.Alignments;
using CharacterGen.Randomizers.Alignments;

namespace CharacterGen.Domain.Generators.Alignments
{
    internal class AlignmentGenerator : IAlignmentGenerator
    {
        public Alignment GeneratePrototype(IAlignmentRandomizer alignmentRandomizer)
        {
            return alignmentRandomizer.Randomize();
        }

        public Alignment GenerateWith(Alignment alignmentPrototype)
        {
            return alignmentPrototype;
        }
    }
}
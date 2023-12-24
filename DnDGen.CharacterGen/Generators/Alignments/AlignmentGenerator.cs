using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.Randomizers.Alignments;

namespace DnDGen.CharacterGen.Generators.Alignments
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
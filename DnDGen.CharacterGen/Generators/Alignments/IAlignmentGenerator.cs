using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.Randomizers.Alignments;
using System.Collections.Generic;

namespace DnDGen.CharacterGen.Generators.Alignments
{
    internal interface IAlignmentGenerator
    {
        Alignment GenerateWith(Alignment alignmentPrototype);
        Alignment GeneratePrototype(IAlignmentRandomizer alignmentRandomizer);
        IEnumerable<Alignment> GeneratePrototypes(IAlignmentRandomizer alignmentRandomizer);
    }
}
using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.Randomizers.Alignments;
using System.Collections.Generic;

namespace DnDGen.CharacterGen.Generators.Randomizers.Alignments
{
    internal class SetAlignmentRandomizer : ISetAlignmentRandomizer
    {
        public Alignment SetAlignment { get; set; }

        public SetAlignmentRandomizer()
        {
            SetAlignment = new Alignment();
        }

        public Alignment Randomize()
        {
            return SetAlignment;
        }

        public IEnumerable<Alignment> GetAllPossibleResults()
        {
            return new[] { SetAlignment };
        }
    }
}
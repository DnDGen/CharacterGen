using System.Collections.Generic;
using CharacterGen.Common.Alignments;
using CharacterGen.Generators.Randomizers.Alignments;

namespace CharacterGen.Generators.Domain.Randomizers.Alignments
{
    public class SetAlignmentRandomizer : ISetAlignmentRandomizer
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
using System.Collections.Generic;
using NPCGen.Common.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;

namespace NPCGen.Generators.Randomizers.Alignments
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
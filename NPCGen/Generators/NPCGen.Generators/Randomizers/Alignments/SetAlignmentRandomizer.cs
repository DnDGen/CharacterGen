using NPCGen.Common.Alignments;
using System.Collections.Generic;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;

namespace NPCGen.Generators.Randomizers.Alignments
{
    public class SetAlignmentRandomizer : IAlignmentRandomizer
    {
        public Alignment Alignment { get; set; }

        public SetAlignmentRandomizer()
        {
            Alignment = new Alignment();
        }

        public Alignment Randomize()
        {
            return Alignment;
        }

        public IEnumerable<Alignment> GetAllPossibleResults()
        {
            return new[] { Alignment };
        }
    }
}
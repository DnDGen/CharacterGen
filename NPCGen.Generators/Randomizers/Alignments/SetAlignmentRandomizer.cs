using NPCGen.Common.Alignments;
using NPCGen.Generators.Randomizers.Alignments.Interfaces;
using System.Collections.Generic;

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
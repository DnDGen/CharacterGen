using System;
using System.Collections.Generic;
using NPCGen.Common.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;

namespace NPCGen.Generators.Randomizers.Alignments
{
    public class SetAlignmentRandomizer : ISetAlignmentRandomizer
    {
        public Alignment SetAlignment { get; set; }

        public Alignment Randomize()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Alignment> GetAllPossibleResults()
        {
            throw new NotImplementedException();
        }
    }
}
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using System;
using System.Collections.Generic;

namespace NPCGen.Core.Generation.Randomizers.Alignments
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
            throw new NotImplementedException();
        }
    }
}
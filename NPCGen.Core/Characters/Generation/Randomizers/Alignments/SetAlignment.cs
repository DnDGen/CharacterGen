using NPCGen.Core.Characters.Data.Alignments;
using System;

namespace NPCGen.Core.Characters.Generation.Randomizers.Alignments
{
    public class SetAlignment : IAlignmentRandomizer
    {
        public Alignment Alignment { get; set; }

        public SetAlignment(Int32 lawfulness, Int32 goodness)
        {
            Alignment = new Alignment();
            Alignment.Lawfulness = lawfulness;
            Alignment.Goodness = goodness;
        }

        public Alignment Randomize()
        {
            return Alignment;
        }
    }
}
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;

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
    }
}
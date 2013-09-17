using NPCGen.Core.Data.Alignments;

namespace NPCGen.Core.Generation.Randomizers.Alignments
{
    public class SetAlignment : IAlignmentRandomizer
    {
        public Alignment Alignment { get; set; }

        public SetAlignment()
        {
            Alignment = new Alignment();
        }

        public Alignment Randomize()
        {
            return Alignment;
        }
    }
}
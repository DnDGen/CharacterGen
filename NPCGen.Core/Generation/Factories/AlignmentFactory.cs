using NPCGen.Core.Data;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments;

namespace NPCGen.Core.Generation.Factories
{
    public class AlignmentFactory : IAlignmentFactory
    {
        public IAlignmentRandomizer AlignmentRandomizer { get; set; }

        public AlignmentFactory(IAlignmentRandomizer alignmentRandomizer)
        {
            AlignmentRandomizer = alignmentRandomizer;
        }

        public Alignment Generate()
        {
            return AlignmentRandomizer.Randomize();
        }
    }
}
using NPCGen.Core.Characters.Data;
using NPCGen.Core.Characters.Generation.Factories.Interfaces;
using NPCGen.Core.Characters.Generation.Randomizers.Alignments;

namespace NPCGen.Core.Characters.Generation.Factories
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
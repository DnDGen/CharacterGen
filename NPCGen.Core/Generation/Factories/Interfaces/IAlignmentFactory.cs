using NPCGen.Core.Data;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments;

namespace NPCGen.Core.Generation.Factories.Interfaces
{
    public interface IAlignmentFactory
    {
        IAlignmentRandomizer AlignmentRandomizer { get; }

        Alignment Generate();
    }
}
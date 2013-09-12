using NPCGen.Core.Characters.Data;
using NPCGen.Core.Characters.Generation.Randomizers.Alignments;

namespace NPCGen.Core.Characters.Generation.Factories.Interfaces
{
    public interface IAlignmentFactory
    {
        IAlignmentRandomizer AlignmentRandomizer { get; }

        Alignment Generate();
    }
}
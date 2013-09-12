using NPCGen.Core.Characters.Data;
using NPCGen.Core.Characters.Data.Classes;
using NPCGen.Core.Characters.Generation.Randomizers.CharacterClass;
using NPCGen.Core.Characters.Generation.Randomizers.Level;

namespace NPCGen.Core.Characters.Generation.Factories.Interfaces
{
    public interface ICharacterClassFactory
    {
        ILevelRandomizer LevelRandomizer { get; }
        IClassRandomizer ClassRandomizer { get; }

        CharacterClass Generate(Alignment alignment);
    }
}
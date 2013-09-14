using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.Classes;
using NPCGen.Core.Generation.Randomizers.CharacterClass;
using NPCGen.Core.Generation.Randomizers.Level;
using System;

namespace NPCGen.Core.Generation.Factories.Interfaces
{
    public interface ICharacterClassFactory
    {
        ILevelRandomizer LevelRandomizer { get; }
        IClassRandomizer ClassRandomizer { get; }

        CharacterClass Generate(Alignment alignment, Int32 constitutionBonus);
    }
}
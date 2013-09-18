using System;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NPCGen.Core.Generation.Randomizers.Level;

namespace NPCGen.Core.Generation.Factories.Interfaces
{
    public interface ICharacterClassFactory
    {
        ILevelRandomizer LevelRandomizer { get; set; }
        IClassNameRandomizer CharacterClassRandomizer { get; set; }

        CharacterClass Generate(Alignment alignment, Int32 constitutionBonus);
    }
}
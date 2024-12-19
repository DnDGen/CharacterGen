using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Randomizers.CharacterClasses;
using System.Collections.Generic;

namespace DnDGen.CharacterGen.Generators.Classes
{
    internal interface ICharacterClassGenerator
    {
        CharacterClassPrototype GeneratePrototype(Alignment alignmentPrototype, IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer);
        IEnumerable<CharacterClassPrototype> GeneratePrototypes(Alignment alignmentPrototype, IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer);
        CharacterClass GenerateWith(Alignment alignment, CharacterClassPrototype classPrototype, RacePrototype racePrototype);
    }
}
using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using CharacterGen.Randomizers.CharacterClasses;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Classes
{
    internal interface ICharacterClassGenerator
    {
        CharacterClassPrototype GeneratePrototype(Alignment alignmentPrototype, IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer);
        CharacterClass GenerateWith(Alignment alignment, CharacterClassPrototype classPrototype);
        IEnumerable<string> RegenerateSpecialistFields(Alignment alignment, CharacterClass characterClass, Race race);
    }
}
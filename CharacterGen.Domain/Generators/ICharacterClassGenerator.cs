using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using CharacterGen.Randomizers.CharacterClasses;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators
{
    internal interface ICharacterClassGenerator
    {
        CharacterClass GenerateWith(Alignment alignment, ILevelRandomizer levelRandomizer, IClassNameRandomizer classNameRandomizer);
        IEnumerable<string> RegenerateSpecialistFields(Alignment alignment, CharacterClass characterClass, Race race);
    }
}
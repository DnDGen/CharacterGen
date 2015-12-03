using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators
{
    public interface ICharacterClassGenerator
    {
        CharacterClass GenerateWith(Alignment alignment, ILevelRandomizer levelRandomizer, IClassNameRandomizer classNameRandomizer);
        IEnumerable<String> RegenerateSpecialistFields(Alignment alignment, CharacterClass characterClass, Race race);
    }
}
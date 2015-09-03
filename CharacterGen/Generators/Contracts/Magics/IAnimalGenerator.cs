using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Magics;
using System.Collections.Generic;

namespace CharacterGen.Generators.Magics
{
    public interface IAnimalGenerator
    {
        Animal GenerateFrom(CharacterClass characterClass, IEnumerable<Feat> feats);
    }
}

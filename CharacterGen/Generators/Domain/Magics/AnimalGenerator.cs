using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Magics;
using CharacterGen.Generators.Magics;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Domain.Magics
{
    public class AnimalGenerator : IAnimalGenerator
    {
        public Animal GenerateFrom(CharacterClass characterClass, IEnumerable<Feat> feats)
        {
            throw new NotImplementedException();
        }
    }
}

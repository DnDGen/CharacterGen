using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;
using CharacterGen.Generators.Magics;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Domain.Magics
{
    public class SpellsGenerator : ISpellsGenerator
    {
        public Dictionary<Int32, IEnumerable<String>> GenerateFrom(CharacterClass characterClass, IEnumerable<Feat> feats, Equipment equipment)
        {
            throw new NotImplementedException();
        }
    }
}

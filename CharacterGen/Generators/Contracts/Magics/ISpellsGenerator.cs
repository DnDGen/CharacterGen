using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Magics
{
    public interface ISpellsGenerator
    {
        Dictionary<Int32, IEnumerable<String>> GenerateFrom(CharacterClass characterClass, IEnumerable<Feat> feats, Equipment equipment);
    }
}

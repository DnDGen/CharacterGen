using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Items;
using NPCGen.Generators.Interfaces.Items;

namespace NPCGen.Generators.Items
{
    public class EquipmentGenerator : IEquipmentGenerator
    {
        public Equipment GenerateWith(IEnumerable<Feat> feats, CharacterClass characterClass)
        {
            throw new NotImplementedException();
        }
    }
}
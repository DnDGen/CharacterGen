using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Items;
using NPCGen.Generators.Interfaces.Items;

namespace NPCGen.Generators.Items
{
    public class EquipmentGenerator : IEquipmentGenerator
    {
        public Equipment GenerateWith(IEnumerable<String> feats, CharacterClass characterClass)
        {
            throw new NotImplementedException();
        }
    }
}
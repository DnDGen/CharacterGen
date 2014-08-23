using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Items;

namespace NPCGen.Generators.Interfaces.Items
{
    public interface IEquipmentGenerator
    {
        Equipment GenerateWith(IEnumerable<String> feats, CharacterClass characterClass);
    }
}
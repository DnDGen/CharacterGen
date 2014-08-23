using System;
using System.Collections.Generic;
using NPCGen.Common.Combats;
using NPCGen.Common.Items;

namespace NPCGen.Generators.Interfaces.Combats
{
    public interface IArmorClassGenerator
    {
        ArmorClass GenerateWith(Equipment equipment, Int32 adjustedDexterityBonus, IEnumerable<String> feats);
    }
}
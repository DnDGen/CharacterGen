using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Items;

namespace NPCGen.Generators.Interfaces.Combats
{
    public interface ICombatGenerator
    {
        Combat GenerateWith(CharacterClass characterClass, IEnumerable<Feat> feats, Dictionary<String, Stat> stats, Equipment equipment);
    }
}
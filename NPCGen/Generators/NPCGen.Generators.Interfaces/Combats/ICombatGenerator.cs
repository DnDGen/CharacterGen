using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Items;
using NPCGen.Common.Races;

namespace NPCGen.Generators.Interfaces.Combats
{
    public interface ICombatGenerator
    {
        Combat GenerateWith(BaseAttack baseAttack, CharacterClass characterClass, Race race, IEnumerable<String> feats, Dictionary<String, Stat> stats, Equipment equipment);
        BaseAttack GenerateBaseAttackWith(CharacterClass characterClass);
    }
}
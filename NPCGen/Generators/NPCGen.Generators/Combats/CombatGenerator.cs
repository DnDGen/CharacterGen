using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Items;
using NPCGen.Generators.Interfaces.Combats;

namespace NPCGen.Generators.Combats
{
    public class CombatGenerator : ICombatGenerator
    {
        public Combat GenerateWith(BaseAttack baseAttack, IEnumerable<Feat> feats, Dictionary<String, Stat> stats, Equipment equipment)
        {
            throw new NotImplementedException();
        }

        public BaseAttack GenerateBaseAttackWith(CharacterClass characterClass)
        {
            throw new NotImplementedException();
        }
    }
}
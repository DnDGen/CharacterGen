using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Combats;
using CharacterGen.Common.Items;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Combats;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Domain.Combats
{
    public class AnimalCombatGenerator : ICombatGenerator
    {
        public BaseAttack GenerateBaseAttackWith(CharacterClass characterClass, Race race)
        {
            throw new NotImplementedException();
        }

        public Combat GenerateWith(BaseAttack baseAttack, CharacterClass characterClass, Race race, IEnumerable<Feat> feats, Dictionary<String, Stat> stats, Equipment equipment)
        {
            throw new NotImplementedException();
        }
    }
}

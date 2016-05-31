using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Items;
using CharacterGen.Races;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Combats
{
    internal interface ICombatGenerator
    {
        Combat GenerateWith(BaseAttack baseAttack, CharacterClass characterClass, Race race, IEnumerable<Feat> feats, Dictionary<string, Stat> stats, Equipment equipment);
        BaseAttack GenerateBaseAttackWith(CharacterClass characterClass, Race race);
    }
}
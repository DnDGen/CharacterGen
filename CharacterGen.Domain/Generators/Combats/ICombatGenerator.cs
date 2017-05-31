using CharacterGen.Abilities;
using CharacterGen.Feats;
using CharacterGen.Abilities;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Feats;
using CharacterGen.Items;
using CharacterGen.Races;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Combats
{
    internal interface ICombatGenerator
    {
        Combat GenerateWith(BaseAttack baseAttack, CharacterClass characterClass, Race race, IEnumerable<Feat> feats, Dictionary<string, Ability> abilities, Equipment equipment);
        BaseAttack GenerateBaseAttackWith(CharacterClass characterClass, Race race, Dictionary<string, Ability> abilities);
    }
}
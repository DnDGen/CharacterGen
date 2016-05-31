using CharacterGen.Abilities;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Races;
using CharacterGen.Randomizers.Stats;

namespace CharacterGen.Domain.Generators.Abilities
{
    internal interface IAbilitiesGenerator
    {
        Ability GenerateWith(CharacterClass characterClass, Race race, IStatsRandomizer statsRandomizer, BaseAttack baseAttack);
    }
}
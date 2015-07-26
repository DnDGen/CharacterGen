using CharacterGen.Common.Abilities;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Combats;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.Stats;

namespace CharacterGen.Generators.Abilities
{
    public interface IAbilitiesGenerator
    {
        Ability GenerateWith(CharacterClass characterClass, Race race, IStatsRandomizer statsRandomizer, BaseAttack baseAttack);
    }
}
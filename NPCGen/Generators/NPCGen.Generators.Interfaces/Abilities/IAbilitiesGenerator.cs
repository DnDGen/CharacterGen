using NPCGen.Common.Abilities;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Stats;

namespace NPCGen.Generators.Interfaces.Abilities
{
    public interface IAbilitiesGenerator
    {
        Ability GenerateWith(CharacterClass characterClass, Race race, IStatsRandomizer statsRandomizer);
    }
}
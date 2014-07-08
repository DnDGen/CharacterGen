using System;
using NPCGen.Common.Abilities;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Randomizers.Stats;

namespace NPCGen.Generators.Abilities
{
    public class AbilitiesGenerator : IAbilitiesGenerator
    {
        public Ability GenerateWith(CharacterClass characterClass, Race race, IStatsRandomizer statsRandomizer)
        {
            throw new NotImplementedException();
        }
    }
}
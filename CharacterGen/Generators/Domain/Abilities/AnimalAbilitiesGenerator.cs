using CharacterGen.Common.Abilities;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Combats;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Randomizers.Stats;
using System;

namespace CharacterGen.Generators.Domain.Abilities
{
    public class AnimalAbilitiesGenerator : IAbilitiesGenerator
    {
        public Ability GenerateWith(CharacterClass characterClass, Race race, IStatsRandomizer statsRandomizer, BaseAttack baseAttack)
        {
            throw new NotImplementedException();
        }
    }
}

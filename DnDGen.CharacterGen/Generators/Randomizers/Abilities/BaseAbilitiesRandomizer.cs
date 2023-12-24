using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.Randomizers.Abilities;
using DnDGen.Infrastructure.Generators;
using System;
using System.Collections.Generic;

namespace DnDGen.CharacterGen.Generators.Randomizers.Abilities
{
    internal abstract class BaseAbilitiesRandomizer : IAbilitiesRandomizer
    {
        protected abstract int defaultValue { get; }

        private readonly Generator generator;

        public BaseAbilitiesRandomizer(Generator generator)
        {
            this.generator = generator;
        }

        public Dictionary<string, Ability> Randomize()
        {
            var stats = generator.Generate(
                () => RollAbilities(RollAbility),
                s => AbilitiesAreAllowed(s.Values),
                () => RollAbilities(() => defaultValue),
                s => AbilitiesInvalidMessage(s.Values),
                $"abilities of value {defaultValue}");

            return stats;
        }

        private Dictionary<string, Ability> RollAbilities(Func<int> roll)
        {
            var abilities = InitializeAbilities();

            foreach (var ability in abilities.Values)
                ability.Value = roll();

            return abilities;
        }

        private Dictionary<string, Ability> InitializeAbilities()
        {
            var abilities = new Dictionary<string, Ability>();

            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);
            abilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength);
            abilities[AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom);

            return abilities;
        }

        protected abstract int RollAbility();
        protected abstract bool AbilitiesAreAllowed(IEnumerable<Ability> abilities);
        protected abstract string AbilitiesInvalidMessage(IEnumerable<Ability> abilities);
    }
}
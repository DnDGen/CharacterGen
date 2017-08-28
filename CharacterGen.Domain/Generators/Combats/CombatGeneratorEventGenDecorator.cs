using CharacterGen.Abilities;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Feats;
using CharacterGen.Items;
using CharacterGen.Races;
using EventGen;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Combats
{
    internal class CombatGeneratorEventGenDecorator : ICombatGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly ICombatGenerator innerGenerator;

        public CombatGeneratorEventGenDecorator(ICombatGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public BaseAttack GenerateBaseAttackWith(CharacterClass characterClass, Race race, Dictionary<string, Ability> abilities)
        {
            eventQueue.Enqueue("CharacterGen", $"Generating base attack for {characterClass.Summary} {race.Summary}");
            var baseAttack = innerGenerator.GenerateBaseAttackWith(characterClass, race, abilities);
            eventQueue.Enqueue("CharacterGen", $"Generated base attack");

            return baseAttack;
        }

        public Combat GenerateWith(BaseAttack baseAttack, CharacterClass characterClass, Race race, IEnumerable<Feat> feats, Dictionary<string, Ability> abilities, Equipment equipment)
        {
            eventQueue.Enqueue("CharacterGen", $"Generating combat statistics for {characterClass.Summary} {race.Summary}");
            var alignment = innerGenerator.GenerateWith(baseAttack, characterClass, race, feats, abilities, equipment);
            eventQueue.Enqueue("CharacterGen", $"Generated combat statistics");

            return alignment;
        }
    }
}

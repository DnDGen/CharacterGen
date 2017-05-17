using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
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

        public BaseAttack GenerateBaseAttackWith(CharacterClass characterClass, Race race, Dictionary<string, Stat> stats)
        {
            eventQueue.Enqueue("CharacterGen", $"Beginning base attack generation for {characterClass.Summary} {race.Summary}");
            var baseAttack = innerGenerator.GenerateBaseAttackWith(characterClass, race, stats);
            eventQueue.Enqueue("CharacterGen", $"Completed generation of base attack");

            return baseAttack;
        }

        public Combat GenerateWith(BaseAttack baseAttack, CharacterClass characterClass, Race race, IEnumerable<Feat> feats, Dictionary<string, Stat> stats, Equipment equipment)
        {
            eventQueue.Enqueue("CharacterGen", $"Beginning combat statistic generation for {characterClass.Summary} {race.Summary}");
            var alignment = innerGenerator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);
            eventQueue.Enqueue("CharacterGen", $"Completed generation of combat statistics");

            return alignment;
        }
    }
}

using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using CharacterGen.Randomizers.Races;
using EventGen;

namespace CharacterGen.Domain.Generators.Races
{
    internal class RaceGeneratorEventGenDecorator : IRaceGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly IRaceGenerator innerGenerator;

        public RaceGeneratorEventGenDecorator(IRaceGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public RacePrototype GeneratePrototype(Alignment alignmentPrototype, CharacterClassPrototype classPrototype, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer)
        {
            var prototype = innerGenerator.GeneratePrototype(alignmentPrototype, classPrototype, baseRaceRandomizer, metaraceRandomizer);

            return prototype;
        }

        public Race GenerateWith(Alignment alignment, CharacterClass characterClass, RacePrototype racePrototype)
        {
            eventQueue.Enqueue("CharacterGen", $"Generating race for {alignment.Full} {characterClass.Summary} from prototype {racePrototype.Summary}");
            var race = innerGenerator.GenerateWith(alignment, characterClass, racePrototype);
            eventQueue.Enqueue("CharacterGen", $"Generated {race.Summary}");

            return race;
        }
    }
}

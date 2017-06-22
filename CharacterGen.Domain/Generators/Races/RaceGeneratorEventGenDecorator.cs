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

        public Race GenerateWith(Alignment alignment, CharacterClass characterClass, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer)
        {
            eventQueue.Enqueue("CharacterGen", $"Generating race for {alignment.Full} {characterClass.Summary}");
            var race = innerGenerator.GenerateWith(alignment, characterClass, baseRaceRandomizer, metaraceRandomizer);
            eventQueue.Enqueue("CharacterGen", $"Generated {race.Summary}");

            return race;
        }
    }
}

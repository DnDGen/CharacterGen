using CharacterGen.Characters;
using CharacterGen.Randomizers.Alignments;
using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Randomizers.Races;
using CharacterGen.Randomizers.Abilities;
using EventGen;

namespace CharacterGen.Domain.Generators.Characters
{
    internal class CharacterGeneratorEventGenDecorator : ICharacterGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly ICharacterGenerator innerGenerator;

        public CharacterGeneratorEventGenDecorator(ICharacterGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public Character GenerateWith(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer, IAbilitiesRandomizer statsRandomizer)
        {
            eventQueue.Enqueue("CharacterGen", "Beginning character generation");
            var character = innerGenerator.GenerateWith(alignmentRandomizer, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer, statsRandomizer);
            eventQueue.Enqueue("CharacterGen", $"Completed generation of {character.Summary}");

            return character;
        }
    }
}

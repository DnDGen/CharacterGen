using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using CharacterGen.Randomizers.CharacterClasses;
using EventGen;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Classes
{
    internal class CharacterClassGeneratorEventGenDecorator : ICharacterClassGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly ICharacterClassGenerator innerGenerator;

        public CharacterClassGeneratorEventGenDecorator(ICharacterClassGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public CharacterClassPrototype GeneratePrototype(Alignment alignmentPrototype, IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer)
        {
            var prototype = innerGenerator.GeneratePrototype(alignmentPrototype, classNameRandomizer, levelRandomizer);

            return prototype;
        }

        public CharacterClass GenerateWith(Alignment alignment, CharacterClassPrototype classPrototype)
        {
            eventQueue.Enqueue("CharacterGen", $"Generating class for {alignment.Full} from prototype {classPrototype.Summary}");
            var characterClass = innerGenerator.GenerateWith(alignment, classPrototype);
            eventQueue.Enqueue("CharacterGen", $"Generated {characterClass.Summary}");

            return characterClass;
        }

        public IEnumerable<string> RegenerateSpecialistFields(Alignment alignment, CharacterClass characterClass, Race race)
        {
            eventQueue.Enqueue("CharacterGen", $"Regenerating specialist fields for {alignment.Full} {characterClass.Summary} {race.Summary}");
            var specialistFields = innerGenerator.RegenerateSpecialistFields(alignment, characterClass, race);

            if (specialistFields.Any())
                eventQueue.Enqueue("CharacterGen", $"Regenerated specialist fields: {string.Join(", ", specialistFields)}");
            else
                eventQueue.Enqueue("CharacterGen", $"Regenerated no specialist fields");

            return specialistFields;
        }
    }
}

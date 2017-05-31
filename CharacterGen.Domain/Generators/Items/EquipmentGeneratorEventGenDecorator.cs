using CharacterGen.CharacterClasses;
using CharacterGen.Feats;
using CharacterGen.Items;
using CharacterGen.Races;
using EventGen;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Items
{
    internal class EquipmentGeneratorEventGenDecorator : IEquipmentGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly IEquipmentGenerator innerGenerator;

        public EquipmentGeneratorEventGenDecorator(IEquipmentGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public Equipment GenerateWith(IEnumerable<Feat> feats, CharacterClass characterClass, Race race)
        {
            eventQueue.Enqueue("CharacterGen", $"Beginning equipment generation for {characterClass.Summary} {race.Summary}");
            var equipment = innerGenerator.GenerateWith(feats, characterClass, race);
            eventQueue.Enqueue("CharacterGen", $"Completed generation of equipment");

            return equipment;
        }
    }
}

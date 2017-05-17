using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Stats;
using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Items;
using CharacterGen.Magics;
using CharacterGen.Races;
using EventGen;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Magics
{
    internal class MagicGeneratorEventGenDecorator : IMagicGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly IMagicGenerator innerGenerator;

        public MagicGeneratorEventGenDecorator(IMagicGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public Magic GenerateWith(Alignment alignment, CharacterClass characterClass, Race race, Dictionary<string, Stat> stats, IEnumerable<Feat> feats, Equipment equipment)
        {
            eventQueue.Enqueue("CharacterGen", $"Beginning magic generation for {alignment.Full} {characterClass.Summary} {race.Summary}");
            var magic = innerGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            eventQueue.Enqueue("CharacterGen", $"Completed generation of magic");

            return magic;
        }
    }
}

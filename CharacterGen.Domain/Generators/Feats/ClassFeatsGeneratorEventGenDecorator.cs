﻿using CharacterGen.Abilities;
using CharacterGen.CharacterClasses;
using CharacterGen.Feats;
using CharacterGen.Races;
using CharacterGen.Skills;
using EventGen;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Feats
{
    internal class ClassFeatsGeneratorEventGenDecorator : IClassFeatsGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly IClassFeatsGenerator innerGenerator;

        public ClassFeatsGeneratorEventGenDecorator(IClassFeatsGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Ability> abilities, IEnumerable<Feat> racialFeats, IEnumerable<Skill> skills)
        {
            eventQueue.Enqueue("CharacterGen", $"Generating class feats for {characterClass.Summary} {race.Summary}");
            var feats = innerGenerator.GenerateWith(characterClass, race, abilities, racialFeats, skills);

            var featNames = feats.Select(f => f.Name);
            eventQueue.Enqueue("CharacterGen", $"Generated class feats: [{string.Join(", ", featNames)}]");

            return feats;
        }
    }
}

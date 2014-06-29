﻿using Ninject;
using NPCGen.Common.Abilities;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public class HitPointsGeneratorTests : StressTests
    {
        [Inject]
        public IHitPointsGenerator HitPointsGenerator { get; set; }
        [Inject]
        public IStatsGenerator StatsGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var prototype = GetNewCharacterClassPrototype(alignment);
            var characterClass = GetNewCharacterClass(prototype);
            var race = GetNewRace(alignment, prototype);
            var stats = StatsGenerator.GenerateWith(StatsRandomizer, characterClass, race);

            var hitPoints = HitPointsGenerator.GenerateWith(characterClass, stats[StatConstants.Constitution].Bonus, race);
            Assert.That(hitPoints, Is.AtLeast(characterClass.Level));
        }
    }
}
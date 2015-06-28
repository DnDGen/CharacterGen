using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Races;
using NPCGen.Generators.Abilities.Feats;
using NPCGen.Generators.Interfaces.Abilities.Feats;
using NPCGen.Selectors.Interfaces;
using NPCGen.Selectors.Interfaces.Objects;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Abilities.Feats
{
    [TestFixture]
    public class RacialFeatsGeneratorTests
    {
        private IRacialFeatsGenerator racialFeatsGenerator;
        private Race race;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<IFeatsSelector> mockFeatsSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockFeatsSelector = new Mock<IFeatsSelector>();
            racialFeatsGenerator = new RacialFeatsGenerator(mockCollectionsSelector.Object, mockAdjustmentsSelector.Object, mockFeatsSelector.Object);
            race = new Race();

            mockFeatsSelector.Setup(s => s.SelectRacial(It.IsAny<String>())).Returns(Enumerable.Empty<RacialFeatSelection>());
        }

        [Test]
        public void GetBaseRacialFeats()
        {
            var baseRaceFeats = new[]
            {
                new RacialFeatSelection { FeatId = "base race feat 1", Focus = "focus" },
                new RacialFeatSelection { FeatId = "base race feat 2", Strength = 9266, Frequency = new Frequency { Quantity = 42, TimePeriod = "fortnight" } }
            };

            race.BaseRace.Id = "baseRaceId";
            mockFeatsSelector.Setup(s => s.SelectRacial("baseRaceId")).Returns(baseRaceFeats);

            var feats = racialFeatsGenerator.GenerateWith(race);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name.Id, Is.EqualTo("base race feat 1"));
            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Strength, Is.EqualTo(0));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);

            Assert.That(last.Name.Id, Is.EqualTo("base race feat 2"));
            Assert.That(last.Focus, Is.Empty);
            Assert.That(last.Strength, Is.EqualTo(9266));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
        }

        [Test]
        public void GetMetaracialFeats()
        {
            var metaraceFeats = new[]
            {
                new RacialFeatSelection { FeatId = "metarace feat 1", Focus = "focus" },
                new RacialFeatSelection { FeatId = "metarace feat 2", Strength = 9266, Frequency = new Frequency { Quantity = 42, TimePeriod = "fortnight" } }
            };

            race.Metarace.Id = "metaraceId";
            mockFeatsSelector.Setup(s => s.SelectRacial("metaraceId")).Returns(metaraceFeats);

            var feats = racialFeatsGenerator.GenerateWith(race);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name.Id, Is.EqualTo("metarace feat 1"));
            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Strength, Is.EqualTo(0));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);

            Assert.That(last.Name.Id, Is.EqualTo("metarace feat 2"));
            Assert.That(last.Focus, Is.Empty);
            Assert.That(last.Strength, Is.EqualTo(9266));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
        }

        [Test]
        public void GetMetaracialSpeciesFeats()
        {
            var speciesFeats = new[]
            {
                new RacialFeatSelection { FeatId = "metarace species feat 1", Focus = "focus" },
                new RacialFeatSelection { FeatId = "metarace species feat 2", Strength = 9266, Frequency = new Frequency { Quantity = 42, TimePeriod = "fortnight" } }
            };

            race.MetaraceSpecies = "metarace species";
            mockFeatsSelector.Setup(s => s.SelectRacial("metarace species")).Returns(speciesFeats);

            var feats = racialFeatsGenerator.GenerateWith(race);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name.Id, Is.EqualTo("metarace species feat 1"));
            Assert.That(first.Focus, Is.EqualTo("focus"));
            Assert.That(first.Strength, Is.EqualTo(0));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);

            Assert.That(last.Name.Id, Is.EqualTo("metarace species feat 2"));
            Assert.That(last.Focus, Is.Empty);
            Assert.That(last.Strength, Is.EqualTo(9266));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
        }

        [Test]
        public void GetAllRacialFeats()
        {
            var baseRaceFeats = new[]
            {
                new RacialFeatSelection { FeatId = "base race feat 1" }
            };

            race.BaseRace.Id = "baseRaceId";
            mockFeatsSelector.Setup(s => s.SelectRacial("baseRaceId")).Returns(baseRaceFeats);

            var metaraceFeats = new[]
            {
                new RacialFeatSelection { FeatId = "metarace feat 2" }
            };

            race.Metarace.Id = "metaraceId";
            mockFeatsSelector.Setup(s => s.SelectRacial("metaraceId")).Returns(metaraceFeats);

            var speciesFeats = new[]
            {
                new RacialFeatSelection { FeatId = "metarace species feat 1" }
            };

            race.MetaraceSpecies = "metarace species";
            mockFeatsSelector.Setup(s => s.SelectRacial("metarace species")).Returns(speciesFeats);

            var feats = racialFeatsGenerator.GenerateWith(race);
            Assert.That(feats.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DoNotGetRacialFeatThatDoNotMeetMinimumHitDiceRequirement()
        {
            var baseRaceFeats = new[]
            {
                new RacialFeatSelection { FeatId = "base race feat 1", MinimumHitDieRequirement = 2 }
            };

            var metaraceFeats = new[]
            {
                new RacialFeatSelection { FeatId = "metarace feat 2", MinimumHitDieRequirement = 2 }
            };

            var speciesFeats = new[]
            {
                new RacialFeatSelection { FeatId = "metarace species feat 1", MinimumHitDieRequirement = 2 }
            };

            race.BaseRace.Id = "baseRaceId";
            race.Metarace.Id = "metaraceId";
            race.MetaraceSpecies = "metarace species";

            var monsterHitDice = new Dictionary<String, Int32>();
            monsterHitDice[race.BaseRace.Id] = 1;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);

            mockFeatsSelector.Setup(s => s.SelectRacial("baseRaceId")).Returns(baseRaceFeats);
            mockFeatsSelector.Setup(s => s.SelectRacial("metaraceId")).Returns(metaraceFeats);
            mockFeatsSelector.Setup(s => s.SelectRacial("metarace species")).Returns(speciesFeats);

            var feats = racialFeatsGenerator.GenerateWith(race);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void GetRacialFeatThatMeetMinimumHitDiceRequirement()
        {
            var baseRaceFeats = new[]
            {
                new RacialFeatSelection { FeatId = "base race feat 1", MinimumHitDieRequirement = 2 }
            };

            var metaraceFeats = new[]
            {
                new RacialFeatSelection { FeatId = "metarace feat 2", MinimumHitDieRequirement = 2 }
            };

            var speciesFeats = new[]
            {
                new RacialFeatSelection { FeatId = "metarace species feat 1", MinimumHitDieRequirement = 2 }
            };

            race.BaseRace.Id = "baseRaceId";
            race.Metarace.Id = "metaraceId";
            race.MetaraceSpecies = "metarace species";

            var monsterHitDice = new Dictionary<String, Int32>();
            monsterHitDice[race.BaseRace.Id] = 2;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters)).Returns(new[] { "baseRaceId" });

            mockFeatsSelector.Setup(s => s.SelectRacial("baseRaceId")).Returns(baseRaceFeats);
            mockFeatsSelector.Setup(s => s.SelectRacial("metaraceId")).Returns(metaraceFeats);
            mockFeatsSelector.Setup(s => s.SelectRacial("metarace species")).Returns(speciesFeats);

            var feats = racialFeatsGenerator.GenerateWith(race);
            Assert.That(feats.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DoNotGetRacialFeatThatDoNotMeetSizeRequirement()
        {
            var baseRaceFeats = new[]
            {
                new RacialFeatSelection { FeatId = "base race feat 1", SizeRequirement = "Large" }
            };

            var metaraceFeats = new[]
            {
                new RacialFeatSelection { FeatId = "metarace feat 2", SizeRequirement = "Large" }
            };

            var speciesFeats = new[]
            {
                new RacialFeatSelection { FeatId = "metarace species feat 1", SizeRequirement = "Large" }
            };

            race.BaseRace.Id = "baseRaceId";
            race.Metarace.Id = "metaraceId";
            race.MetaraceSpecies = "metarace species";
            race.Size = "not large";

            mockFeatsSelector.Setup(s => s.SelectRacial("baseRaceId")).Returns(baseRaceFeats);
            mockFeatsSelector.Setup(s => s.SelectRacial("metaraceId")).Returns(metaraceFeats);
            mockFeatsSelector.Setup(s => s.SelectRacial("metarace species")).Returns(speciesFeats);

            var feats = racialFeatsGenerator.GenerateWith(race);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void GetRacialFeatThatMeetSizeRequirement()
        {
            var baseRaceFeats = new[]
            {
                new RacialFeatSelection { FeatId = "base race feat 1", SizeRequirement = "Large" }
            };

            var metaraceFeats = new[]
            {
                new RacialFeatSelection { FeatId = "metarace feat 2", SizeRequirement = "Large" }
            };

            var speciesFeats = new[]
            {
                new RacialFeatSelection { FeatId = "metarace species feat 1", SizeRequirement = "Large" }
            };

            race.BaseRace.Id = "baseRaceId";
            race.Metarace.Id = "metaraceId";
            race.MetaraceSpecies = "metarace species";
            race.Size = "Large";

            mockFeatsSelector.Setup(s => s.SelectRacial("baseRaceId")).Returns(baseRaceFeats);
            mockFeatsSelector.Setup(s => s.SelectRacial("metaraceId")).Returns(metaraceFeats);
            mockFeatsSelector.Setup(s => s.SelectRacial("metarace species")).Returns(speciesFeats);

            var feats = racialFeatsGenerator.GenerateWith(race);
            Assert.That(feats.Count(), Is.EqualTo(3));
        }

        [Test]
        public void NonMonstersHaveOneMonsterHitDieForSakeOfHitDiceRequirements()
        {
            var baseRaceFeats = new[]
            {
                new RacialFeatSelection { FeatId = "racial feat", MinimumHitDieRequirement = 1 }
            };

            race.BaseRace.Id = "baseRaceId";
            mockFeatsSelector.Setup(s => s.SelectRacial("baseRaceId")).Returns(baseRaceFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters)).Returns(new[] { "other base race" });

            var feats = racialFeatsGenerator.GenerateWith(race);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name.Id, Is.EqualTo("racial feat"));
            mockAdjustmentsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice), Times.Never);
        }
    }
}
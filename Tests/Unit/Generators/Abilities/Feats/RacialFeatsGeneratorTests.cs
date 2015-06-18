using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using Moq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
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
        private CharacterClass characterClass;
        private Race race;
        private Dictionary<String, Stat> stats;
        private Dictionary<String, Skill> skills;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<IFeatsSelector> mockFeatsSelector;
        private List<AdditionalFeatSelection> additionalFeatSelections;
        private Mock<IDice> mockDice;
        private BaseAttack baseAttack;
        private Mock<INameSelector> mockNameSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockFeatsSelector = new Mock<IFeatsSelector>();
            mockDice = new Mock<IDice>();
            mockNameSelector = new Mock<INameSelector>();
            racialFeatsGenerator = new RacialFeatsGenerator(mockCollectionsSelector.Object, mockAdjustmentsSelector.Object, mockFeatsSelector.Object, mockDice.Object, mockNameSelector.Object);
            characterClass = new CharacterClass();
            race = new Race();
            stats = new Dictionary<String, Stat>();
            skills = new Dictionary<String, Skill>();
            additionalFeatSelections = new List<AdditionalFeatSelection>();
            baseAttack = new BaseAttack();
            stats[StatConstants.Intelligence] = new Stat();

            mockFeatsSelector.Setup(s => s.SelectRacial(It.IsAny<String>())).Returns(Enumerable.Empty<RacialFeatSelection>());
            mockFeatsSelector.Setup(s => s.SelectAdditional()).Returns(additionalFeatSelections);
            mockFeatsSelector.Setup(s => s.SelectClass(It.IsAny<String>())).Returns(Enumerable.Empty<CharacterClassFeatSelection>());
            mockDice.Setup(d => d.Roll(1).d(It.IsAny<Int32>())).Returns(1);
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
            Assert.Fail();
        }

        [Test]
        public void IfFeatIdAndStrengthAndFocusAndTimePeriodAreEqual_CombineFrequencyQuantity()
        {
            Assert.Fail();
        }

        [Test]
        public void IfFeatIdNotEqual_DoNotCombineFrequencyQuantity()
        {
            Assert.Fail();
        }

        [Test]
        public void IfStrengthNotEqual_DoNotCombineFrequencyQuantity()
        {
            Assert.Fail();
        }

        [Test]
        public void IfFocusNotEqual_DoNotCombineFrequencyQuantity()
        {
            Assert.Fail();
        }

        [Test]
        public void IfFrequencyTimePeriodNotEqual_DoNotCombineFrequencyQuantity()
        {
            Assert.Fail();
        }

        [TestCase(FeatConstants.Frequencies.AtWill, FeatConstants.Frequencies.Day)]
        [TestCase(FeatConstants.Frequencies.Constant, FeatConstants.Frequencies.Day)]
        [TestCase(FeatConstants.Frequencies.Constant, FeatConstants.Frequencies.AtWill)]
        public void FrequencyTimePeriodTrumps(String trumpTimePeriod, String losingTimePeriod)
        {
            Assert.Fail();
        }

        [Test]
        public void DoNotGetRacialFeatThatDoNotMeetMinimumHitDiceRequirement()
        {
            Assert.Fail();
        }

        [Test]
        public void DoNotGetRacialFeatThatDoNotMeetSizeRequirement()
        {
            Assert.Fail();
        }

        [Test]
        public void DoNotDuplicateRacialFeats()
        {
            var baseRaceFeats = new[]
            {
                new RacialFeatSelection { FeatId = "feat 1", Focus = "focus" },
                new RacialFeatSelection { FeatId = "base race feat 2", Strength = 9266, Frequency = new Frequency { Quantity = 42, TimePeriod = "fortnight" } }
            };

            var metaraceFeats = new[]
            {
                new RacialFeatSelection { FeatId = "feat 1", Focus = "focus" },
                new RacialFeatSelection { FeatId = "metarace feat 2", Strength = 9266, Frequency = new Frequency { Quantity = 42, TimePeriod = "fortnight" } }
            };

            race.BaseRace.Id = "baseRaceId";
            race.Metarace.Id = "metaraceId";
            mockFeatsSelector.Setup(s => s.SelectRacial("baseRaceId")).Returns(baseRaceFeats);
            mockFeatsSelector.Setup(s => s.SelectRacial("metaraceId")).Returns(metaraceFeats);

            var feats = racialFeatsGenerator.GenerateWith(race);
            var featIds = feats.Select(s => s.Name.Id);

            Assert.That(featIds, Contains.Item("feat 1"));
            Assert.That(featIds, Contains.Item("base race feat 2"));
            Assert.That(featIds, Contains.Item("metarace feat 2"));
            Assert.That(feats.Count(), Is.EqualTo(3));
        }

        [TestCase(FeatConstants.Frequencies.AtWill)]
        [TestCase(FeatConstants.Frequencies.Constant)]
        public void IfFeatIdAndFocusAreEqualAndFrequencyTimePeriodAreStrongEnough_OnlyKeepStrongestFeat(String frequencyTimePeriod)
        {
            var baseRaceFeats = new[]
            {
                new RacialFeatSelection { FeatId = "racial feat", Focus = "focus", Strength = 42, Frequency = new Frequency { TimePeriod = frequencyTimePeriod } },
                new RacialFeatSelection { FeatId = "racial feat", Focus = "focus", Strength = 9266, Frequency = new Frequency { TimePeriod = frequencyTimePeriod } }
            };

            race.BaseRace.Id = "baseRaceId";
            mockFeatsSelector.Setup(s => s.SelectRacial("baseRaceId")).Returns(baseRaceFeats);

            var feats = racialFeatsGenerator.GenerateWith(race);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name.Id, Is.EqualTo("racial feat"));
            Assert.That(onlyFeat.Strength, Is.EqualTo(9266));
        }

        [Test]
        public void IfFeatIdNotEqual_KeepBothFeat()
        {
            Assert.Fail();
        }

        [Test]
        public void IfFocusNotEqual_KeepBothFeat()
        {
            Assert.Fail();
        }

        [TestCase(FeatConstants.Frequencies.Day)]
        public void IfFrequencyTimePeriodNotConstantOrAtWill_KeepBothFeat(String frequencyTimePeriod)
        {
            Assert.Fail();
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
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, TableNameConstants.Set.Collection.Groups.Monsters)).Returns(new[] { "other base race" });

            var feats = racialFeatsGenerator.GenerateWith(race);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name.Id, Is.EqualTo("racial feat"));
            mockAdjustmentsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice), Times.Never);
        }
    }
}
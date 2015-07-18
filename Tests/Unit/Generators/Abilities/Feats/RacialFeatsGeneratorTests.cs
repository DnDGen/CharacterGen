﻿using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NPCGen.Common.Abilities.Skills;
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
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<IFeatsSelector> mockFeatsSelector;
        private Mock<IFeatFocusGenerator> mockFeatFocusGenerator;
        private Race race;
        private List<RacialFeatSelection> baseRaceFeats;
        private List<RacialFeatSelection> metaraceFeats;
        private List<RacialFeatSelection> speciesFeats;
        private Dictionary<String, Skill> skills;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockFeatsSelector = new Mock<IFeatsSelector>();
            mockFeatFocusGenerator = new Mock<IFeatFocusGenerator>();
            racialFeatsGenerator = new RacialFeatsGenerator(mockCollectionsSelector.Object, mockAdjustmentsSelector.Object, mockFeatsSelector.Object,
                mockFeatFocusGenerator.Object);
            race = new Race();
            baseRaceFeats = new List<RacialFeatSelection>();
            metaraceFeats = new List<RacialFeatSelection>();
            speciesFeats = new List<RacialFeatSelection>();
            skills = new Dictionary<String, Skill>();

            race.BaseRace = "baseRaceId";
            race.Metarace = "metaraceId";
            race.MetaraceSpecies = "metarace species";
            mockFeatsSelector.Setup(s => s.SelectRacial("baseRaceId")).Returns(baseRaceFeats);
            mockFeatsSelector.Setup(s => s.SelectRacial("metaraceId")).Returns(metaraceFeats);
            mockFeatsSelector.Setup(s => s.SelectRacial("metarace species")).Returns(speciesFeats);
        }

        [Test]
        public void GetBaseRacialFeats()
        {
            var feat1 = new RacialFeatSelection();
            feat1.Feat = "base race feat 1";

            var feat2 = new RacialFeatSelection();
            feat2.Feat = "base race feat 2";
            feat2.Strength = 9266;
            feat2.Frequency.Quantity = 42;
            feat2.Frequency.TimePeriod = "fortnight";

            baseRaceFeats.Add(feat1);
            baseRaceFeats.Add(feat2);

            var feats = racialFeatsGenerator.GenerateWith(race, skills);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name, Is.EqualTo("base race feat 1"));
            Assert.That(first.Strength, Is.EqualTo(0));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);

            Assert.That(last.Name, Is.EqualTo("base race feat 2"));
            Assert.That(last.Strength, Is.EqualTo(9266));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
        }

        [Test]
        public void GetMetaracialFeats()
        {
            var feat1 = new RacialFeatSelection();
            feat1.Feat = "metarace feat 1";

            var feat2 = new RacialFeatSelection();
            feat2.Feat = "metarace feat 2";
            feat2.Strength = 9266;
            feat2.Frequency.Quantity = 42;
            feat2.Frequency.TimePeriod = "fortnight";

            metaraceFeats.Add(feat1);
            metaraceFeats.Add(feat2);

            var feats = racialFeatsGenerator.GenerateWith(race, skills);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name, Is.EqualTo("metarace feat 1"));
            Assert.That(first.Strength, Is.EqualTo(0));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);

            Assert.That(last.Name, Is.EqualTo("metarace feat 2"));
            Assert.That(last.Strength, Is.EqualTo(9266));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
        }

        [Test]
        public void GetMetaracialSpeciesFeats()
        {
            var feat1 = new RacialFeatSelection();
            feat1.Feat = "metarace species feat 1";

            var feat2 = new RacialFeatSelection();
            feat2.Feat = "metarace species feat 2";
            feat2.Strength = 9266;
            feat2.Frequency.Quantity = 42;
            feat2.Frequency.TimePeriod = "fortnight";

            speciesFeats.Add(feat1);
            speciesFeats.Add(feat2);

            var feats = racialFeatsGenerator.GenerateWith(race, skills);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name, Is.EqualTo("metarace species feat 1"));
            Assert.That(first.Strength, Is.EqualTo(0));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);

            Assert.That(last.Name, Is.EqualTo("metarace species feat 2"));
            Assert.That(last.Strength, Is.EqualTo(9266));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
        }

        [Test]
        public void GetAllRacialFeats()
        {
            var baseRaceFeat = new RacialFeatSelection();
            baseRaceFeat.Feat = "base race feat";
            baseRaceFeats.Add(baseRaceFeat);

            var metaraceFeat = new RacialFeatSelection();
            metaraceFeat.Feat = "metarace feat";
            metaraceFeats.Add(metaraceFeat);

            var speciesFeat = new RacialFeatSelection();
            speciesFeat.Feat = "metarace species feat";
            speciesFeats.Add(speciesFeat);

            var feats = racialFeatsGenerator.GenerateWith(race, skills);
            Assert.That(feats.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DoNotGetRacialFeatThatDoNotMeetMinimumHitDiceRequirement()
        {
            var baseRaceFeat = new RacialFeatSelection();
            baseRaceFeat.Feat = "base race feat";
            baseRaceFeat.MinimumHitDieRequirement = 2;
            baseRaceFeats.Add(baseRaceFeat);

            var metaraceFeat = new RacialFeatSelection();
            metaraceFeat.Feat = "metarace feat";
            metaraceFeat.MinimumHitDieRequirement = 2;
            metaraceFeats.Add(metaraceFeat);

            var speciesFeat = new RacialFeatSelection();
            speciesFeat.Feat = "metarace species feat";
            speciesFeat.MinimumHitDieRequirement = 2;
            speciesFeats.Add(speciesFeat);

            var monsterHitDice = new Dictionary<String, Int32>();
            monsterHitDice[race.BaseRace] = 1;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);

            var feats = racialFeatsGenerator.GenerateWith(race, skills);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void GetRacialFeatThatMeetMinimumHitDiceRequirement()
        {
            var baseRaceFeat = new RacialFeatSelection();
            baseRaceFeat.Feat = "base race feat";
            baseRaceFeat.MinimumHitDieRequirement = 2;
            baseRaceFeats.Add(baseRaceFeat);

            var metaraceFeat = new RacialFeatSelection();
            metaraceFeat.Feat = "metarace feat";
            metaraceFeat.MinimumHitDieRequirement = 2;
            metaraceFeats.Add(metaraceFeat);

            var speciesFeat = new RacialFeatSelection();
            speciesFeat.Feat = "metarace species feat";
            speciesFeat.MinimumHitDieRequirement = 2;
            speciesFeats.Add(speciesFeat);

            var monsterHitDice = new Dictionary<String, Int32>();
            monsterHitDice[race.BaseRace] = 2;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters)).Returns(new[] { "baseRaceId" });

            var feats = racialFeatsGenerator.GenerateWith(race, skills);
            Assert.That(feats.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DoNotGetRacialFeatThatDoNotMeetSizeRequirement()
        {
            var baseRaceFeat = new RacialFeatSelection();
            baseRaceFeat.Feat = "base race feat";
            baseRaceFeat.SizeRequirement = "Large";
            baseRaceFeats.Add(baseRaceFeat);

            var metaraceFeat = new RacialFeatSelection();
            metaraceFeat.Feat = "metarace feat";
            metaraceFeat.SizeRequirement = "Large";
            metaraceFeats.Add(metaraceFeat);

            var speciesFeat = new RacialFeatSelection();
            speciesFeat.Feat = "metarace species feat";
            speciesFeat.SizeRequirement = "Large";
            speciesFeats.Add(speciesFeat);

            race.Size = "not large";

            var feats = racialFeatsGenerator.GenerateWith(race, skills);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void GetRacialFeatThatMeetSizeRequirement()
        {
            var baseRaceFeat = new RacialFeatSelection();
            baseRaceFeat.Feat = "base race feat";
            baseRaceFeat.SizeRequirement = "Large";
            baseRaceFeats.Add(baseRaceFeat);

            var metaraceFeat = new RacialFeatSelection();
            metaraceFeat.Feat = "metarace feat";
            metaraceFeat.SizeRequirement = "Large";
            metaraceFeats.Add(metaraceFeat);

            var speciesFeat = new RacialFeatSelection();
            speciesFeat.Feat = "metarace species feat";
            speciesFeat.SizeRequirement = "Large";
            speciesFeats.Add(speciesFeat);

            race.Size = "Large";

            var feats = racialFeatsGenerator.GenerateWith(race, skills);
            Assert.That(feats.Count(), Is.EqualTo(3));
        }

        [Test]
        public void NonMonstersHaveOneMonsterHitDieForSakeOfHitDiceRequirements()
        {
            var baseRaceFeat = new RacialFeatSelection();
            baseRaceFeat.Feat = "racial feat";
            baseRaceFeat.MinimumHitDieRequirement = 1;
            baseRaceFeats.Add(baseRaceFeat);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters)).Returns(new[] { "other base race" });

            var feats = racialFeatsGenerator.GenerateWith(race, skills);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo("racial feat"));
            mockAdjustmentsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice), Times.Never);
        }

        [Test]
        public void GetFociForFeat()
        {
            var baseRaceFeatSelection = new RacialFeatSelection();
            baseRaceFeatSelection.Feat = "racial feat";
            baseRaceFeatSelection.FocusType = "base focus type";
            baseRaceFeats.Add(baseRaceFeatSelection);

            var metaraceFeatSelection = new RacialFeatSelection();
            metaraceFeatSelection.Feat = "metarace feat";
            metaraceFeatSelection.FocusType = "meta focus type";
            metaraceFeats.Add(metaraceFeatSelection);

            var speciesFeatSelection = new RacialFeatSelection();
            speciesFeatSelection.Feat = "metarace species feat";
            speciesFeatSelection.FocusType = "species focus type";
            speciesFeats.Add(speciesFeatSelection);

            mockFeatFocusGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("racial feat", "base focus type", skills)).Returns("base focus");
            mockFeatFocusGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("metarace feat", "meta focus type", skills)).Returns("meta focus");
            mockFeatFocusGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("metarace species feat", "species focus type", skills)).Returns("species focus");

            var feats = racialFeatsGenerator.GenerateWith(race, skills);
            var baseFeat = feats.First(f => f.Name == baseRaceFeatSelection.Feat);
            var metaFeat = feats.First(f => f.Name == metaraceFeatSelection.Feat);
            var speciesFeat = feats.First(f => f.Name == speciesFeatSelection.Feat);

            Assert.That(baseFeat.Focus, Is.EqualTo("base focus"));
            Assert.That(metaFeat.Focus, Is.EqualTo("meta focus"));
            Assert.That(speciesFeat.Focus, Is.EqualTo("species focus"));
        }
    }
}
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
using NPCGen.Common.Items;
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

        [Test]
        public void GetClassFeatsForClass()
        {
            characterClass.ClassName = "class name";
            characterClass.Level = 2;

            var classFeats = new[]
            {
                new CharacterClassFeatSelection { FeatId = "class feat 1", Strength = 9266, Frequency = new Frequency { Quantity = 0, TimePeriod = "constant" } },
                new CharacterClassFeatSelection { FeatId = "class feat 2", Frequency = new Frequency { Quantity = 600, TimePeriod = "fortnight" } }
            };

            mockFeatsSelector.Setup(s => s.SelectClass("class name")).Returns(classFeats);

            var feats = racialFeatsGenerator.GenerateWith(race);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name.Id, Is.EqualTo("class feat 1"));
            Assert.That(first.Strength, Is.EqualTo(9266));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("never"));

            Assert.That(last.Name.Id, Is.EqualTo("class feat 2"));
            Assert.That(last.Strength, Is.EqualTo(0));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(600));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
        }

        [Test]
        public void GetClassFeatForSpecializations()
        {
            Assert.Fail();
        }

        [Test]
        public void GetAllClassFeats()
        {
            Assert.Fail();
        }

        [Test]
        public void GetClassFeatsWithMatchingLevelRequirement()
        {
            characterClass.ClassName = "class name";
            characterClass.Level = 2;

            AddClassFeat("feat 1", characterClass.ClassName, characterClass.Level, 1);
            AddClassFeat("feat 2", characterClass.ClassName, characterClass.Level, 2);

            var feats = racialFeatsGenerator.GenerateWith(race);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item("feat 1"));
            Assert.That(featIds, Contains.Item("feat 2"));
        }

        [Test]
        public void GetOnlyStrongestClassFeat()
        {
            AddFeatSelections(1);

            characterClass.ClassName = "class name";
            characterClass.Level = 2;

            AddClassFeat("class feat", characterClass.ClassName, 1, 1);
            AddClassFeat("class feat", characterClass.ClassName, 2, 3);

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var classFeat = feats.First(f => f.Name.Id == "class feat");

            Assert.That(classFeat.Focus, Is.EqualTo("3"));
            Assert.That(feats.Count(f => f.Name.Id == "class feat"), Is.EqualTo(1));
        }

        [Test]
        public void DoNotGetClassFeatsIfNone()
        {
            characterClass.ClassName = "class name";
            characterClass.Level = 1;

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void DoNotGetClassFeatsIfNoneMatchRequirements()
        {
            AddFeatSelections(1);

            characterClass.ClassName = "class name";
            characterClass.Level = 2;

            AddClassFeat("class feat 1", "other class", 1, 1);
            AddClassFeat("class feat 2", characterClass.ClassName, 3, 1);

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Is.Not.Contains("class feat 1"));
            Assert.That(featNames, Is.Not.Contains("class feat 2"));
        }

        [Test]
        public void GetSkillSynergyFeatsWithMatchingRankRequirement()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergyFeats, "skill 1")).Returns(new[] { "feat 1", "feat 2" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergyFeats, "skill 2")).Returns(new[] { "feat 3" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergyFeats, "skill 3")).Returns(new[] { "feat 4" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergyFeats, "skill 4")).Returns(new[] { "feat 5", "feat 6" });

            skills["skill 1"] = new Skill { Ranks = 5, ClassSkill = true };
            skills["skill 2"] = new Skill { Ranks = 4, ClassSkill = true };
            skills["skill 3"] = new Skill { Ranks = 10, ClassSkill = false };
            skills["skill 4"] = new Skill { Ranks = 9, ClassSkill = false };

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item("feat 1"));
            Assert.That(featIds, Contains.Item("feat 2"));
            Assert.That(featIds, Is.Not.Contains("feat 3"));
            Assert.That(featIds, Contains.Item("feat 4"));
            Assert.That(featIds, Is.Not.Contains("feat 5"));
            Assert.That(featIds, Is.Not.Contains("feat 6"));
        }

        [Test]
        public void DoNotGetSkillSynergyIfNone()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergyFeats, "skill 1")).Returns(Enumerable.Empty<String>());

            skills["skill 1"] = new Skill { Ranks = 5, ClassSkill = true };

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void GetFeatFromSpecialistFields()
        {
            AddFeatSelections(2);

            characterClass.ClassName = "class name";
            characterClass.SpecialistFields = new[] { "specialist" };

            AddClassFeat("feat 1", "specialist", 0, 0);
            AddClassFeat("feat 2", "specialist", 0, 0);

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item("feat 1"));
            Assert.That(featIds, Contains.Item("feat 2"));
        }

        [Test]
        public void GetFeatWithSpecializationFromSpecialistFields()
        {
            AddFeatSelections(1);

            var weapons = new[] { "battleaxe" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "weapon")).Returns(weapons);

            characterClass.ClassName = "class name";
            characterClass.SpecialistFields = new[] { "specialist" };

            AddClassFeat("class feat", "specialist", 0, 0);
            additionalFeatSelections[1].FocusType = "weapon";

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var feat = feats.First(f => f.Name.Id == "class feat");

            Assert.That(feat.Focus, Is.EqualTo("battleaxe"));
        }

        [Test]
        public void GetFeatWithSpecializationAndPrerequisiteFromSpecialistFields()
        {
            AddFeatSelections(1);

            var weapons = new[] { "battleaxe", "kitten", "stick" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "weapon")).Returns(weapons);

            mockDice.SetupSequence(d => d.Roll(1).d(2)).Returns(2);
            mockDice.SetupSequence(d => d.Roll(1).d(3)).Returns(3).Returns(1);

            characterClass.ClassName = "class name";
            characterClass.SpecialistFields = new[] { "specialist" };

            AddClassFeat("class feat 1", "specialist", 0, 0);
            AddClassFeat("class feat 2", "specialist", 0, 0);
            additionalFeatSelections[1].FocusType = "weapon";
            additionalFeatSelections[2].RequiredFeatIds = new[] { additionalFeatSelections[1].Name.Id };
            additionalFeatSelections[2].FocusType = "weapon";

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First(f => f.Name.Id == "class feat 1");
            var secondFeat = feats.Last(f => f.Name.Id == "class feat 2");

            Assert.That(firstFeat.Focus, Is.EqualTo("stick"));
            Assert.That(secondFeat.Focus, Is.EqualTo("stick"));
        }

        [Test]
        public void DoNotGetFeatFromSpecialistFieldsIfNoneThatMatchRequirements()
        {
            AddFeatSelections(1);

            characterClass.ClassName = "class name";
            characterClass.SpecialistFields = new[] { "specialist" };

            AddClassFeat("class feat 1", "other specialist", 0, 0);
            AddClassFeat("class feat 2", "other class", 1, 0);

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Is.Not.Contains("class feat 1"));
            Assert.That(featNames, Is.Not.Contains("class feat 2"));
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 2)]
        [TestCase(5, 2)]
        [TestCase(6, 3)]
        [TestCase(7, 3)]
        [TestCase(8, 3)]
        [TestCase(9, 4)]
        [TestCase(10, 4)]
        [TestCase(11, 4)]
        [TestCase(12, 5)]
        [TestCase(13, 5)]
        [TestCase(14, 5)]
        [TestCase(15, 6)]
        [TestCase(16, 6)]
        [TestCase(17, 6)]
        [TestCase(18, 7)]
        [TestCase(19, 7)]
        [TestCase(20, 7)]
        public void GetAdditionalFeats(Int32 level, Int32 numberOfFeats)
        {
            characterClass.Level = level;
            AddFeatSelections(8);

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            for (var i = 0; i < numberOfFeats; i++)
                Assert.That(featNames, Contains.Item(additionalFeatSelections[i].Name));

            Assert.That(featNames.Count(), Is.EqualTo(numberOfFeats));
        }

        private void AddFeatSelections(Int32 quantity)
        {
            for (var i = 0; i < quantity; i++)
            {
                var id = String.Format("feat {0}", i + 1);
                AddAdditionalFeat(id);
                mockNameSelector.Setup(s => s.Select(id)).Returns(id + " name");
            }
        }

        private void AddAdditionalFeat(String featId)
        {
            var selection = new AdditionalFeatSelection();
            selection.Name.Id = featId;
            selection.Name.Name = featId + " name";

            additionalFeatSelections.Add(selection);
            mockFeatsSelector.Setup(s => s.SelectAdditional(featId)).Returns(selection);
            mockNameSelector.Setup(s => s.Select(featId)).Returns(selection.Name.Name);
        }

        [Test]
        public void DoNotGetAdditionalFeatIfNoneAvailable()
        {
            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void DoNotGetMoreAdditionalFeatsIfNoneAvailable()
        {
            characterClass.Level = 1;
            AddFeatSelections(1);
            additionalFeatSelections[0].RequiredBaseAttack = 9266;

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void FighterFeatsCanBeAdditionalFeats()
        {
            characterClass.Level = 1;
            AddFeatSelections(1);
            additionalFeatSelections[0].IsFighterFeat = true;

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].Name));
            Assert.That(featNames.Count(), Is.EqualTo(1));
        }

        [Test]
        public void AdditionalFeatsPickedAtRandom()
        {
            characterClass.Level = 3;
            AddFeatSelections(3);

            mockDice.Setup(d => d.Roll(1).d(3)).Returns(3);
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(1);

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].Name));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[2].Name));
            Assert.That(featNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void HumansGetAdditionalFeat()
        {
            race.BaseRace.Id = RaceConstants.BaseRaces.HumanId;
            characterClass.Level = 1;
            AddFeatSelections(3);

            mockDice.Setup(d => d.Roll(1).d(3)).Returns(3);
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(1);

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].Name));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[2].Name));
            Assert.That(featNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DoNotGetFeatsWithUnmetPrerequisite()
        {
            characterClass.Level = 1;
            AddFeatSelections(2);
            additionalFeatSelections[0].RequiredBaseAttack = 9266;
            mockDice.Setup(d => d.Roll(1).d(It.IsAny<Int32>())).Returns(1);

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[1].Name));
            Assert.That(featNames.Count(), Is.EqualTo(1));
        }

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 2)]
        [TestCase(4, 3)]
        [TestCase(5, 3)]
        [TestCase(6, 4)]
        [TestCase(7, 4)]
        [TestCase(8, 5)]
        [TestCase(9, 5)]
        [TestCase(10, 6)]
        [TestCase(11, 6)]
        [TestCase(12, 7)]
        [TestCase(13, 7)]
        [TestCase(14, 8)]
        [TestCase(15, 8)]
        [TestCase(16, 9)]
        [TestCase(17, 9)]
        [TestCase(18, 10)]
        [TestCase(19, 10)]
        [TestCase(20, 11)]
        public void FightersGetBonusFighterFeats(Int32 level, Int32 numberOfBonusFeats)
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = level;
            AddFeatSelections(20);
            foreach (var selection in additionalFeatSelections)
                selection.IsFighterFeat = true;

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);
            var additionalFeats = level / 3 + 1;
            var totalFeats = numberOfBonusFeats + additionalFeats;

            for (var i = 0; i < totalFeats; i++)
                Assert.That(featNames, Contains.Item(additionalFeatSelections[i].Name));

            Assert.That(featNames.Count(), Is.EqualTo(totalFeats));
        }

        [Test]
        public void DoNotGetNonFighterFeats()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(3);
            additionalFeatSelections[2].IsFighterFeat = true;

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].Name));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[2].Name));
            Assert.That(featNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DoNotGetFighterFeatsIfNoneAvailable()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(1);
            additionalFeatSelections[0].IsFighterFeat = true;

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].Name));
            Assert.That(featNames.Count(), Is.EqualTo(1));
        }

        [Test]
        public void DoNotGetMoreFighterFeatsIfNoneAvailable()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 2;
            AddFeatSelections(3);
            additionalFeatSelections[1].RequiredBaseAttack = 9266;
            additionalFeatSelections[1].IsFighterFeat = true;
            additionalFeatSelections[2].IsFighterFeat = true;

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].Name));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[2].Name));
            Assert.That(featNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void FighterFeatsPickedAtRandom()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 2;
            AddFeatSelections(4);
            foreach (var feat in additionalFeatSelections)
                feat.IsFighterFeat = true;

            mockDice.Setup(d => d.Roll(1).d(4)).Returns(4);
            mockDice.Setup(d => d.Roll(1).d(3)).Returns(1);
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].Name));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[2].Name));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[3].Name));
            Assert.That(featNames.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DoNotGetFighterFeatWithUnmetPrerequisite()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(3);
            additionalFeatSelections[1].IsFighterFeat = true;
            additionalFeatSelections[1].RequiredFeatIds = new[] { "other feat" };
            additionalFeatSelections[2].IsFighterFeat = true;

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].Name));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[2].Name));
            Assert.That(featNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNoFighterFeatsAvailable_ThenStop()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(3);
            additionalFeatSelections[1].IsFighterFeat = true;
            additionalFeatSelections[1].RequiredFeatIds = new[] { "other feat" };

            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[2].Name));
            Assert.That(featNames.Count(), Is.EqualTo(1));
        }

        [TestCase(1, 0)]
        [TestCase(2, 0)]
        [TestCase(3, 0)]
        [TestCase(4, 0)]
        [TestCase(5, 1)]
        [TestCase(6, 1)]
        [TestCase(7, 1)]
        [TestCase(8, 1)]
        [TestCase(9, 1)]
        [TestCase(10, 2)]
        [TestCase(11, 2)]
        [TestCase(12, 2)]
        [TestCase(13, 2)]
        [TestCase(14, 2)]
        [TestCase(15, 3)]
        [TestCase(16, 3)]
        [TestCase(17, 3)]
        [TestCase(18, 3)]
        [TestCase(19, 3)]
        [TestCase(20, 4)]
        public void WizardsGetBonusWizardFeats(Int32 level, Int32 numberOfBonusFeats)
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = level;
            AddFeatSelections(20);
            foreach (var selection in additionalFeatSelections)
                selection.IsWizardFeat = true;

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);
            var additionalFeats = level / 3 + 1;
            var totalFeats = numberOfBonusFeats + additionalFeats;

            for (var i = 0; i < totalFeats; i++)
                Assert.That(featNames, Contains.Item(additionalFeatSelections[i].Name));

            Assert.That(featNames.Count(), Is.EqualTo(totalFeats));
        }

        [Test]
        public void DoNotGetNonWizardFeats()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = 5;
            AddFeatSelections(5);
            additionalFeatSelections[3].IsWizardFeat = true;

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].Name));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[1].Name));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[3].Name));
            Assert.That(featNames.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DoNotGetWizardFeatsIfNoneAvailable()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = 5;
            AddFeatSelections(4);
            additionalFeatSelections[0].IsWizardFeat = true;

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].Name));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[1].Name));
            Assert.That(featNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DoNotGetMoreWizardFeatsIfNoneAvailable()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = 10;
            AddFeatSelections(6);
            additionalFeatSelections[1].IsWizardFeat = true;
            additionalFeatSelections[5].IsWizardFeat = true;

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].Name));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[1].Name));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[2].Name));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[3].Name));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[5].Name));
            Assert.That(featNames.Count(), Is.EqualTo(5));
        }

        [Test]
        public void WizardFeatsPickedAtRandom()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = 10;
            AddFeatSelections(7);
            foreach (var feat in additionalFeatSelections)
                feat.IsWizardFeat = true;

            mockDice.Setup(d => d.Roll(1).d(7)).Returns(7);
            mockDice.Setup(d => d.Roll(1).d(6)).Returns(6);
            mockDice.Setup(d => d.Roll(1).d(5)).Returns(5);
            mockDice.Setup(d => d.Roll(1).d(4)).Returns(4);
            mockDice.Setup(d => d.Roll(1).d(3)).Returns(1);
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].Name));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[2].Name));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[3].Name));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[4].Name));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[5].Name));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[6].Name));
            Assert.That(featNames.Count(), Is.EqualTo(6));
        }

        [Test]
        public void ReassessPrerequisitesEveryFeat()
        {
            characterClass.Level = 3;
            AddFeatSelections(3);
            additionalFeatSelections[1].RequiredFeatIds = new[] { additionalFeatSelections[0].Name.Id };

            mockDice.Setup(d => d.Roll(1).d(3)).Returns(1);
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(1);

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].Name));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[1].Name));
            Assert.That(featNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllTheFeats()
        {
            race.BaseRace.Id = RaceConstants.BaseRaces.HumanId;
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 20;
            characterClass.SpecialistFields = new[] { "specialist" };

            AddRacialFeat("racial feat 1");

            AddFeatSelections(20);
            foreach (var feat in additionalFeatSelections)
                feat.IsFighterFeat = true;

            AddClassFeat("class feat 1", characterClass.ClassName, 1, 1);
            AddClassFeat("class feat 2", characterClass.ClassName, 2, 3);
            AddClassFeat("class feat 3", characterClass.ClassName, 20, 20);
            AddClassFeat("class feat 4", "specialist", 0, 0);
            AddClassFeat("class feat 5", "specialist", 2, 3);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergyFeats, "skill 1")).Returns(new[] { "synergy feat 1", "synergy feat 2" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergyFeats, "skill 2")).Returns(new[] { "synergy feat 3" });

            AddAdditionalFeat("synergy feat 1");
            AddAdditionalFeat("synergy feat 2");
            AddAdditionalFeat("synergy feat 3");

            skills["skill 1"] = new Skill { Ranks = 5, ClassSkill = true };
            skills["skill 2"] = new Skill { Ranks = 10, ClassSkill = false };

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featIds = feats.Select(f => f.Name.Id);

            for (var i = 0; i < 19; i++)
                Assert.That(featIds, Contains.Item(additionalFeatSelections[i].Name.Id));

            Assert.That(featIds, Is.Not.Contains(additionalFeatSelections[19].Name.Id));
            Assert.That(featIds, Contains.Item("racial feat 1"));
            Assert.That(featIds, Contains.Item("synergy feat 1"));
            Assert.That(featIds, Contains.Item("synergy feat 2"));
            Assert.That(featIds, Contains.Item("synergy feat 3"));
            Assert.That(featIds, Contains.Item("class feat 1"));
            Assert.That(featIds, Contains.Item("class feat 2"));
            Assert.That(featIds, Contains.Item("class feat 3"));
            Assert.That(featIds, Contains.Item("class feat 4"));
            Assert.That(featIds, Contains.Item("class feat 5"));

            var featNames = feats.Select(f => f.Name.Name);

            for (var i = 0; i < 19; i++)
                Assert.That(featNames, Contains.Item(additionalFeatSelections[i].Name.Name));

            Assert.That(featNames, Is.Not.Contains(additionalFeatSelections[19].Name.Name));
            Assert.That(featNames, Contains.Item("racial feat 1 name"));
            Assert.That(featNames, Contains.Item("synergy feat 1 name"));
            Assert.That(featNames, Contains.Item("synergy feat 2 name"));
            Assert.That(featNames, Contains.Item("synergy feat 3 name"));
            Assert.That(featNames, Contains.Item("class feat 1 name"));
            Assert.That(featNames, Contains.Item("class feat 2 name"));
            Assert.That(featNames, Contains.Item("class feat 3 name"));
            Assert.That(featNames, Contains.Item("class feat 4 name"));
            Assert.That(featNames, Contains.Item("class feat 5 name"));

            Assert.That(feats.Count(), Is.EqualTo(28));
        }

        [Test]
        public void CannotGetDuplicateFeats()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            characterClass.SpecialistFields = new[] { "specialist" };

            AddFeatSelections(6);
            foreach (var feat in additionalFeatSelections)
                feat.IsFighterFeat = true;

            AddRacialFeat("feat 1");
            AddRacialFeat("auto feat 1");
            AddRacialFeat("auto feat 2");

            AddClassFeat("feat 2", characterClass.ClassName, 0, 0);
            AddClassFeat("auto feat 1", characterClass.ClassName, 0, 0);
            AddClassFeat("auto feat 3", characterClass.ClassName, 0, 0);
            AddClassFeat("auto feat 3", "specialist", 0, 0);

            //NOTE: Skill Synergy feats are unique and cannot be selected or earned
            //in any way except through rank requirements.

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featIds = feats.Select(f => f.Name.Id);

            Assert.That(featIds, Contains.Item("feat 1"));
            Assert.That(featIds, Contains.Item("feat 2"));
            Assert.That(featIds, Contains.Item("feat 3"));
            Assert.That(featIds, Contains.Item("feat 4"));
            Assert.That(featIds, Contains.Item("auto feat 1"));
            Assert.That(featIds, Contains.Item("auto feat 2"));
            Assert.That(featIds, Contains.Item("auto feat 3"));
            Assert.That(featIds.Count(), Is.EqualTo(7));
        }

        [Test]
        public void FeatsWithoutSpecificApplicationsDoNotFill()
        {
            AddFeatSelections(1);

            var schools = new[] { "school 1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "specific application")).Returns(schools);

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var onlyFeat = feats.Single();

            mockCollectionsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, It.IsAny<String>()), Times.Never);
            Assert.That(onlyFeat.Name, Is.EqualTo(additionalFeatSelections[0].Name));
            Assert.That(onlyFeat.Focus, Is.Empty);
        }

        [Test]
        public void FeatsWithSpecificApplicationsAreFilled()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].FocusType = "specific application";

            var schools = new[] { "school 1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "specific application")).Returns(schools);

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo(additionalFeatSelections[0].Name));
            Assert.That(onlyFeat.Focus, Is.EqualTo("school 1"));
        }

        [Test]
        public void FeatsWithSpecificApplicationsAreFilledRandomly()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].FocusType = "specific application";

            var schools = new[] { "school 1", "school 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "specific application")).Returns(schools);

            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo(additionalFeatSelections[0].Name));
            Assert.That(onlyFeat.Focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void FeatsWithSpecificApplicationsCanBeFilledMoreThanOnce()
        {
            characterClass.Level = 3;
            AddFeatSelections(1);
            additionalFeatSelections[0].FocusType = "specific application";

            var schools = new[] { "school 1", "school 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "specific application")).Returns(schools);

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo(additionalFeatSelections[0].Name));
            Assert.That(firstFeat.Focus, Is.EqualTo("school 1"));
            Assert.That(lastFeat.Name, Is.EqualTo(additionalFeatSelections[0].Name));
            Assert.That(lastFeat.Focus, Is.EqualTo("school 2"));
            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void SpellMasterySpecificApplicationIsNumberOfSpellsLearned()
        {
            characterClass.Level = 1;
            AddFeatSelections(1);
            additionalFeatSelections[0].Name.Id = FeatConstants.SpellMasteryId;
            stats[StatConstants.Intelligence].Value = 14;

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name.Id, Is.EqualTo(FeatConstants.SpellMasteryId));
            Assert.That(firstFeat.Focus, Is.EqualTo("2"));
            Assert.That(feats.Count(), Is.EqualTo(1));
        }

        [Test]
        public void SpellMasteryCanIncreaseSpellsKnown()
        {
            characterClass.Level = 3;
            AddFeatSelections(1);
            additionalFeatSelections[0].Name.Id = FeatConstants.SpellMasteryId;
            stats[StatConstants.Intelligence].Value = 14;

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name.Id, Is.EqualTo(FeatConstants.SpellMasteryId));
            Assert.That(firstFeat.Focus, Is.EqualTo("4"));
            Assert.That(feats.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ToughnessCanBeTakenMultipleTimes()
        {
            Assert.Fail();
        }

        [Test]
        public void ToughnessSpecificApplicationIsNumberOftimesTaken()
        {
            Assert.Fail();
        }

        [Test]
        public void SpellcastersCanSelectRayForWeaponSpecificApplications()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].FocusType = AdditionalFeatSelectionConstants.WeaponsWithUnarmedAndGrappleAndRay;

            var weapons = new[] { WeaponProficiencyConstants.Ray, "weapon" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, AdditionalFeatSelectionConstants.WeaponsWithUnarmedAndGrappleAndRay))
                .Returns(weapons);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, TableNameConstants.Set.Collection.Groups.Spellcasters))
                .Returns(new[] { characterClass.ClassName });

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo(additionalFeatSelections[0].Name));
            Assert.That(onlyFeat.Focus, Is.EqualTo(WeaponProficiencyConstants.Ray));
        }

        [Test]
        public void NonSpellcastersCannotSelectRayForWeaponSpecificApplications()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].FocusType = AdditionalFeatSelectionConstants.WeaponsWithUnarmedAndGrappleAndRay;

            var weapons = new[] { WeaponProficiencyConstants.Ray, "weapon" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, AdditionalFeatSelectionConstants.WeaponsWithUnarmedAndGrappleAndRay))
                .Returns(weapons);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, TableNameConstants.Set.Collection.Groups.Spellcasters))
                .Returns(new[] { "other class name" });

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo(additionalFeatSelections[0].Name));
            Assert.That(onlyFeat.Focus, Is.EqualTo("weapon"));
        }

        [Test]
        public void FeatsWithoutSpecificApplicationsButWithRequirementsThatHaveSpecificApplicationsDoNotUseSameSpecificApplication()
        {
            characterClass.Level = 3;
            AddFeatSelections(2);
            additionalFeatSelections[0].FocusType = "specific application";
            additionalFeatSelections[1].RequiredFeatIds = new[] { additionalFeatSelections[0].Name.Id };

            var schools = new[] { "school 1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "specific application")).Returns(schools);

            mockDice.SetupSequence(d => d.Roll(1).d(2)).Returns(2);

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo(additionalFeatSelections[0].Name));
            Assert.That(firstFeat.Focus, Is.EqualTo("school 1"));
            Assert.That(lastFeat.Name, Is.EqualTo(additionalFeatSelections[1].Name));
            Assert.That(lastFeat.Focus, Is.Empty);
        }

        [Test]
        public void FeatsWithSpecificApplicationsAndRequirementsThatHaveSpecificApplicationsUseSameSpecificApplication()
        {
            characterClass.Level = 3;
            AddFeatSelections(2);
            additionalFeatSelections[0].FocusType = "specific application";
            additionalFeatSelections[1].RequiredFeatIds = new[] { additionalFeatSelections[0].Name.Id };
            additionalFeatSelections[1].FocusType = "specific application";

            var schools = new[] { "school 1", "school 2", "school 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "specific application")).Returns(schools);

            mockDice.SetupSequence(d => d.Roll(1).d(2)).Returns(2);
            mockDice.SetupSequence(d => d.Roll(1).d(3)).Returns(3).Returns(1);

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo(additionalFeatSelections[0].Name));
            Assert.That(firstFeat.Focus, Is.EqualTo("school 3"));
            Assert.That(lastFeat.Name, Is.EqualTo(additionalFeatSelections[1].Name));
            Assert.That(lastFeat.Focus, Is.EqualTo("school 3"));
        }

        [Test]
        public void IfFeatRequirementHasMultipleSpecificApplications_PickRandomlyAmongThem()
        {
            characterClass.Level = 6;
            AddFeatSelections(2);
            additionalFeatSelections[0].FocusType = "specific application";
            additionalFeatSelections[1].RequiredFeatIds = new[] { additionalFeatSelections[0].Name.Id };
            additionalFeatSelections[1].FocusType = "specific application";

            var schools = new[] { "school 1", "school 2", "school 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "specific application")).Returns(schools);

            mockDice.SetupSequence(d => d.Roll(1).d(2)).Returns(1).Returns(1).Returns(2).Returns(2);
            mockDice.SetupSequence(d => d.Roll(1).d(3)).Returns(3).Returns(1);

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var secondFeat = feats.ElementAt(1);
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo(additionalFeatSelections[0].Name));
            Assert.That(firstFeat.Focus, Is.EqualTo("school 3"));
            Assert.That(secondFeat.Name, Is.EqualTo(additionalFeatSelections[0].Name));
            Assert.That(secondFeat.Focus, Is.EqualTo("school 1"));
            Assert.That(lastFeat.Name, Is.EqualTo(additionalFeatSelections[1].Name));
            Assert.That(lastFeat.Focus, Is.EqualTo("school 1"));
        }

        [Test]
        public void IfSpecificApplicationTypeIsSchoolOfMagic_CannotPickProhibitedFieldAsSpecificApplication()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].FocusType = AdditionalFeatSelectionConstants.SchoolsOfMagic;

            var schools = new[] { "school 1", "school 2", "school 3", "school 4" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, AdditionalFeatSelectionConstants.SchoolsOfMagic)).Returns(schools);

            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            characterClass.ProhibitedFields = new[] { "school 1", "school 3" };

            var feats = racialFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo(additionalFeatSelections[0].Name));
            Assert.That(onlyFeat.Focus, Is.EqualTo("school 4"));
        }
    }
}
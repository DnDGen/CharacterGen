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
using NPCGen.Generators.Abilities;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Selectors.Interfaces;
using NPCGen.Selectors.Interfaces.Objects;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Abilities
{
    [TestFixture]
    public class FeatsGeneratorTests
    {
        private IFeatsGenerator featsGenerator;
        private CharacterClass characterClass;
        private Race race;
        private Dictionary<String, Stat> stats;
        private Dictionary<String, Skill> skills;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<IFeatsSelector> mockFeatsSelector;
        private List<RacialFeatSelection> racialFeatSelections;
        private List<AdditionalFeatSelection> additionalFeatSelections;
        private Mock<IDice> mockDice;
        private BaseAttack baseAttack;
        private List<String> overwrittenStrengthFeats;
        private List<String> cumulativeStrengthFeats;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockFeatsSelector = new Mock<IFeatsSelector>();
            mockDice = new Mock<IDice>();
            featsGenerator = new FeatsGenerator(mockCollectionsSelector.Object, mockAdjustmentsSelector.Object,
                mockFeatsSelector.Object, mockDice.Object);
            characterClass = new CharacterClass();
            race = new Race();
            stats = new Dictionary<String, Stat>();
            skills = new Dictionary<String, Skill>();
            racialFeatSelections = new List<RacialFeatSelection>();
            additionalFeatSelections = new List<AdditionalFeatSelection>();
            baseAttack = new BaseAttack();
            stats[StatConstants.Intelligence] = new Stat();
            overwrittenStrengthFeats = new List<String>();
            cumulativeStrengthFeats = new List<String>();

            mockFeatsSelector.Setup(s => s.SelectRacial()).Returns(racialFeatSelections);
            mockFeatsSelector.Setup(s => s.SelectAdditional()).Returns(additionalFeatSelections);
            mockDice.Setup(d => d.Roll(1).d(It.IsAny<Int32>())).Returns(1);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, TableNameConstants.Set.Collection.Groups.OverwrittenStrengths))
                .Returns(overwrittenStrengthFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, TableNameConstants.Set.Collection.Groups.CumulativeStrengths))
                .Returns(cumulativeStrengthFeats);
        }

        [Test]
        public void GetRacialFeats()
        {
            racialFeatSelections.Add(new RacialFeatSelection { FeatName = "feat 1", FeatStrength = 9266 });
            racialFeatSelections.Add(new RacialFeatSelection { FeatName = "feat 2" });

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name, Is.EqualTo("feat 1"));
            Assert.That(first.SpecificApplication, Is.EqualTo("9266"));
            Assert.That(last.Name, Is.EqualTo("feat 2"));
            Assert.That(last.SpecificApplication, Is.Empty);
        }

        [Test]
        public void DoNotGetRacialFeatsThatDoNotHaveMetRequirements()
        {
            var selection = new RacialFeatSelection();
            selection.FeatName = "racial feat";
            selection.BaseRaceRequirements = new[] { "otherbaserace" };
            racialFeatSelections.Add(selection);
            race.BaseRace.Id = "baserace";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void DoNotDuplicateRacialFeats()
        {
            racialFeatSelections.Add(new RacialFeatSelection { FeatName = "feat 1" });
            racialFeatSelections.Add(new RacialFeatSelection { FeatName = "feat 1" });
            racialFeatSelections.Add(new RacialFeatSelection { FeatName = "feat 2", FeatStrength = 42 });
            racialFeatSelections.Add(new RacialFeatSelection { FeatName = "feat 2", FeatStrength = 42 });

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo("feat 1"));
            Assert.That(firstFeat.SpecificApplication, Is.Empty);
            Assert.That(lastFeat.Name, Is.EqualTo("feat 2"));
            Assert.That(lastFeat.SpecificApplication, Is.EqualTo("42"));
            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void OnlyKeepStrongestFeat()
        {
            racialFeatSelections.Add(new RacialFeatSelection { FeatName = "feat 1", FeatStrength = 9266 });
            racialFeatSelections.Add(new RacialFeatSelection { FeatName = "feat 1", FeatStrength = 42 });
            overwrittenStrengthFeats.Add("feat 1");

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo("feat 1"));
            Assert.That(onlyFeat.SpecificApplication, Is.EqualTo("9266"));
        }

        [Test]
        public void AddStrengthsOfBothFeats()
        {
            racialFeatSelections.Add(new RacialFeatSelection { FeatName = "feat 1", FeatStrength = 9266 });
            racialFeatSelections.Add(new RacialFeatSelection { FeatName = "feat 1", FeatStrength = 42 });
            cumulativeStrengthFeats.Add("feat 1");

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo("feat 1"));
            Assert.That(onlyFeat.SpecificApplication, Is.EqualTo("9308"));
        }

        [Test]
        public void KeepBothFeatsIfNotOverwrittenOrCumulativeStrengths()
        {
            racialFeatSelections.Add(new RacialFeatSelection { FeatName = "feat 1", FeatStrength = 9266 });
            racialFeatSelections.Add(new RacialFeatSelection { FeatName = "feat 1", FeatStrength = 42 });

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo("feat 1"));
            Assert.That(firstFeat.SpecificApplication, Is.EqualTo("9266"));
            Assert.That(lastFeat.Name, Is.EqualTo("feat 1"));
            Assert.That(lastFeat.SpecificApplication, Is.EqualTo("42"));
            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetMonsterHitDiceForRequirements()
        {
            var selection = new RacialFeatSelection();
            selection.FeatName = "racial feat";
            selection.HitDieRequirements = Enumerable.Range(2, 2);
            racialFeatSelections.Add(selection);

            race.BaseRace.Id = "baserace";
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.Names, TableNameConstants.Set.Collection.Groups.Monsters))
                .Returns(new[] { race.BaseRace.Id });

            var monsterHitDice = new Dictionary<String, Int32>();
            monsterHitDice[race.BaseRace.Id] = 3;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats.Single().Name, Is.EqualTo("racial feat"));
        }

        [Test]
        public void NonMonstersHaveOneMonsterHitDieForSakeOfHitDiceRequirements()
        {
            var selection = new RacialFeatSelection();
            selection.FeatName = "racial feat";
            selection.HitDieRequirements = Enumerable.Range(1, 1);
            racialFeatSelections.Add(selection);

            race.BaseRace.Id = "baserace";
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.Names, TableNameConstants.Set.Collection.Groups.Monsters)).Returns(new[] { "other base race" });

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats.Single().Name, Is.EqualTo("racial feat"));
            mockAdjustmentsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice), Times.Never);
        }

        [Test]
        public void GetClassFeatsWithMatchingLevelRequirement()
        {
            characterClass.ClassName = "class name";
            characterClass.Level = 2;
            var classFeats = new[] { "feat 1", "feat 2", "feat 3", "feat 4" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassFeats, "class name")).Returns(classFeats);

            var levelRequirements = new Dictionary<String, Int32>();
            levelRequirements["feat 1"] = 1;
            levelRequirements["feat 2"] = 1;
            levelRequirements["feat 3"] = 2;
            levelRequirements["feat 4"] = 3;
            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.CLASSFeatLevelRequirements, "class name");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(levelRequirements);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item("feat 1"));
            Assert.That(featNames, Contains.Item("feat 2"));
            Assert.That(featNames, Contains.Item("feat 3"));
            Assert.That(featNames, Is.Not.Contains("feat 4"));
        }

        [Test]
        public void DoNotGetClassFeatsIfNone()
        {
            characterClass.ClassName = "class name";
            characterClass.Level = 2;
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassFeats, "class name")).Returns(Enumerable.Empty<String>());

            var levelRequirements = new Dictionary<String, Int32>();
            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.CLASSFeatLevelRequirements, "class name");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(levelRequirements);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void DoNotGetClassFeatsIfNoneMatchRequirements()
        {
            characterClass.ClassName = "class name";
            characterClass.Level = 2;
            var classFeats = new[] { "feat 1", "feat 2", "feat 3", "feat 4" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassFeats, "class name")).Returns(classFeats);

            var levelRequirements = new Dictionary<String, Int32>();
            levelRequirements["feat 1"] = 3;
            levelRequirements["feat 2"] = 3;
            levelRequirements["feat 3"] = 3;
            levelRequirements["feat 4"] = 3;
            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.CLASSFeatLevelRequirements, "class name");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(levelRequirements);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats, Is.Empty);
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

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item("feat 1"));
            Assert.That(featNames, Contains.Item("feat 2"));
            Assert.That(featNames, Is.Not.Contains("feat 3"));
            Assert.That(featNames, Contains.Item("feat 4"));
            Assert.That(featNames, Is.Not.Contains("feat 5"));
            Assert.That(featNames, Is.Not.Contains("feat 6"));
        }

        [Test]
        public void DoNotGetSkillSynergyIfNone()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergyFeats, "skill 1")).Returns(Enumerable.Empty<String>());

            skills["skill 1"] = new Skill { Ranks = 5, ClassSkill = true };

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void GetFeatFromSpecialistFields()
        {
            characterClass.ClassName = "class name";
            characterClass.SpecialistFields = new[] { "specialist" };
            var specialistFeats = new[] { "feat 1", "feat 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassFeats, "specialist")).Returns(specialistFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item("feat 1"));
            Assert.That(featNames, Contains.Item("feat 2"));
        }

        [Test]
        public void GetFeatWithSpecializationFromSpecialistFields()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].SpecificApplicationType = "weapon";

            var weapons = new[] { "battleaxe" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatSpecificApplications, "weapon")).Returns(weapons);

            characterClass.ClassName = "class name";
            characterClass.SpecialistFields = new[] { "specialist" };
            var specialistFeats = new[] { "feat 1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassFeats, "specialist")).Returns(specialistFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var feat = feats.Single();

            Assert.That(feat.Name, Is.EqualTo("feat 1"));
            Assert.That(feat.SpecificApplication, Is.EqualTo("battleaxe"));
        }

        [Test]
        public void GetFeatWithSpecializationAndPrerequisiteFromSpecialistFields()
        {
            AddFeatSelections(2);
            additionalFeatSelections[0].SpecificApplicationType = "weapon";
            additionalFeatSelections[1].RequiredFeats = new[] { additionalFeatSelections[0].FeatName };
            additionalFeatSelections[1].SpecificApplicationType = "weapon";

            var weapons = new[] { "battleaxe", "kitten", "stick" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatSpecificApplications, "weapon")).Returns(weapons);

            mockDice.SetupSequence(d => d.Roll(1).d(2)).Returns(2);
            mockDice.SetupSequence(d => d.Roll(1).d(3)).Returns(3).Returns(1);

            characterClass.ClassName = "class name";
            characterClass.SpecialistFields = new[] { "specialist" };
            var specialistFeats = new[] { "feat 1", "feat 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassFeats, "specialist")).Returns(specialistFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var secondFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo("feat 1"));
            Assert.That(firstFeat.SpecificApplication, Is.EqualTo("stick"));
            Assert.That(secondFeat.Name, Is.EqualTo("feat 1"));
            Assert.That(secondFeat.SpecificApplication, Is.EqualTo("stick"));
        }

        [Test]
        public void DoNotGetFeatFromSpecialistFieldsIfNone()
        {
            characterClass.ClassName = "class name";
            characterClass.SpecialistFields = new[] { "specialist" };
            var specialistFeats = Enumerable.Empty<String>();
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassFeats, "specialist")).Returns(specialistFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats, Is.Empty);
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

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            for (var i = 0; i < numberOfFeats; i++)
                Assert.That(featNames, Contains.Item(additionalFeatSelections[i].FeatName));

            Assert.That(featNames.Count(), Is.EqualTo(numberOfFeats));
        }

        private void AddFeatSelections(Int32 quantity)
        {
            for (var i = 0; i < quantity; i++)
            {
                var name = String.Format("feat {0}", i + 1);
                additionalFeatSelections.Add(new AdditionalFeatSelection { FeatName = name });
            }
        }

        [Test]
        public void DoNotGetAdditionalFeatIfNoneAvailable()
        {
            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void DoNotGetMoreAdditionalFeatsIfNoneAvailable()
        {
            characterClass.Level = 1;
            AddFeatSelections(1);
            additionalFeatSelections[0].RequiredBaseAttack = 9266;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void FighterFeatsCanBeAdditionalFeats()
        {
            characterClass.Level = 1;
            AddFeatSelections(1);
            additionalFeatSelections[0].IsFighterFeat = true;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(1));
        }

        [Test]
        public void AdditionalFeatsPickedAtRandom()
        {
            characterClass.Level = 3;
            AddFeatSelections(3);

            mockDice.Setup(d => d.Roll(1).d(3)).Returns(3);
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(1);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[2].FeatName));
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

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[2].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DoNotGetFeatsWithUnmetPrerequisite()
        {
            characterClass.Level = 1;
            AddFeatSelections(2);
            additionalFeatSelections[0].RequiredBaseAttack = 9266;
            mockDice.Setup(d => d.Roll(1).d(It.IsAny<Int32>())).Returns(1);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[1].FeatName));
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

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);
            var additionalFeats = level / 3 + 1;
            var totalFeats = numberOfBonusFeats + additionalFeats;

            for (var i = 0; i < totalFeats; i++)
                Assert.That(featNames, Contains.Item(additionalFeatSelections[i].FeatName));

            Assert.That(featNames.Count(), Is.EqualTo(totalFeats));
        }

        [Test]
        public void DoNotGetNonFighterFeats()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(3);
            additionalFeatSelections[2].IsFighterFeat = true;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[2].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DoNotGetFighterFeatsIfNoneAvailable()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(1);
            additionalFeatSelections[0].IsFighterFeat = true;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].FeatName));
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

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[2].FeatName));
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

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[2].FeatName));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[3].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DoNotGetFighterFeatWithUnmetPrerequisite()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(3);
            additionalFeatSelections[1].IsFighterFeat = true;
            additionalFeatSelections[1].RequiredFeats = new[] { "other feat" };
            additionalFeatSelections[2].IsFighterFeat = true;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[2].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNoFighterFeatsAvailable_ThenStop()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(3);
            additionalFeatSelections[1].IsFighterFeat = true;
            additionalFeatSelections[1].RequiredFeats = new[] { "other feat" };

            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[2].FeatName));
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

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);
            var additionalFeats = level / 3 + 1;
            var totalFeats = numberOfBonusFeats + additionalFeats;

            for (var i = 0; i < totalFeats; i++)
                Assert.That(featNames, Contains.Item(additionalFeatSelections[i].FeatName));

            Assert.That(featNames.Count(), Is.EqualTo(totalFeats));
        }

        [Test]
        public void DoNotGetNonWizardFeats()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = 5;
            AddFeatSelections(5);
            additionalFeatSelections[3].IsWizardFeat = true;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[1].FeatName));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[3].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DoNotGetWizardFeatsIfNoneAvailable()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = 5;
            AddFeatSelections(4);
            additionalFeatSelections[0].IsWizardFeat = true;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[1].FeatName));
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

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[1].FeatName));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[2].FeatName));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[3].FeatName));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[5].FeatName));
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

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[2].FeatName));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[3].FeatName));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[4].FeatName));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[5].FeatName));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[6].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(6));
        }

        [Test]
        public void ReassessPrerequisitesEveryFeat()
        {
            characterClass.Level = 3;
            AddFeatSelections(3);
            additionalFeatSelections[1].RequiredFeats = new[] { additionalFeatSelections[0].FeatName };

            mockDice.Setup(d => d.Roll(1).d(3)).Returns(1);
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(1);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(additionalFeatSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(additionalFeatSelections[1].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllTheFeats()
        {
            race.BaseRace.Id = RaceConstants.BaseRaces.HumanId;
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 20;
            characterClass.SpecialistFields = new[] { "specialist" };

            racialFeatSelections.Add(new RacialFeatSelection { FeatName = "racial feat 1" });

            AddFeatSelections(20);
            foreach (var feat in additionalFeatSelections)
                feat.IsFighterFeat = true;

            var classFeats = new[] { "class feat 1", "class feat 2", "class feat 3", "class feat 4" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassFeats, characterClass.ClassName)).Returns(classFeats);
            var specialistFeats = new[] { "class feat 1", "class feat 5" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassFeats, "specialist")).Returns(specialistFeats);


            var levelRequirements = new Dictionary<String, Int32>();
            levelRequirements["class feat 1"] = 1;
            levelRequirements["class feat 2"] = 10;
            levelRequirements["class feat 3"] = 15;
            levelRequirements["class feat 4"] = 20;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.FighterFeatLevelRequirements)).Returns(levelRequirements);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergyFeats, "skill 1")).Returns(new[] { "synergy feat 1", "synergy feat 2" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergyFeats, "skill 2")).Returns(new[] { "synergy feat 3" });

            skills["skill 1"] = new Skill { Ranks = 5, ClassSkill = true };
            skills["skill 2"] = new Skill { Ranks = 10, ClassSkill = false };

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            for (var i = 0; i < 19; i++)
                Assert.That(featNames, Contains.Item(additionalFeatSelections[i].FeatName));

            Assert.That(featNames, Is.Not.Contains(additionalFeatSelections[19].FeatName));
            Assert.That(featNames, Contains.Item("racial feat 1"));
            Assert.That(featNames, Contains.Item("synergy feat 1"));
            Assert.That(featNames, Contains.Item("synergy feat 2"));
            Assert.That(featNames, Contains.Item("synergy feat 3"));
            Assert.That(featNames, Contains.Item("class feat 1"));
            Assert.That(featNames, Contains.Item("class feat 2"));
            Assert.That(featNames, Contains.Item("class feat 3"));
            Assert.That(featNames, Contains.Item("class feat 4"));
            Assert.That(featNames, Contains.Item("class feat 5"));
            Assert.That(featNames.Count(), Is.EqualTo(28));
        }

        [Test]
        public void CannotGetDuplicateFeats()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(6);
            foreach (var feat in additionalFeatSelections)
                feat.IsFighterFeat = true;

            racialFeatSelections.Add(new RacialFeatSelection { FeatName = "feat 1" });
            racialFeatSelections.Add(new RacialFeatSelection { FeatName = "auto feat 1" });
            racialFeatSelections.Add(new RacialFeatSelection { FeatName = "auto feat 2" });

            var classFeats = new[] { "feat 2", "auto feat 1", "auto feat 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassFeats, characterClass.ClassName)).Returns(classFeats);

            var levelRequirements = new Dictionary<String, Int32>();
            foreach (var feat in classFeats)
                levelRequirements[feat] = 0;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.FighterFeatLevelRequirements)).Returns(levelRequirements);

            //NOTE: Skill Synergy feats are unique and cannot be selected or earned
            //in any way except through rank requirements.

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item("feat 1"));
            Assert.That(featNames, Contains.Item("feat 2"));
            Assert.That(featNames, Contains.Item("feat 3"));
            Assert.That(featNames, Contains.Item("feat 4"));
            Assert.That(featNames, Contains.Item("auto feat 1"));
            Assert.That(featNames, Contains.Item("auto feat 2"));
            Assert.That(featNames, Contains.Item("auto feat 3"));
            Assert.That(featNames.Count(), Is.EqualTo(7));
        }

        [Test]
        public void FeatsWithoutSpecificApplicationsDoNotFill()
        {
            AddFeatSelections(1);

            var schools = new[] { "school 1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatSpecificApplications, "specific application")).Returns(schools);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var onlyFeat = feats.Single();

            mockCollectionsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatSpecificApplications, It.IsAny<String>()), Times.Never);
            Assert.That(onlyFeat.Name, Is.EqualTo(additionalFeatSelections[0].FeatName));
            Assert.That(onlyFeat.SpecificApplication, Is.Empty);
        }

        [Test]
        public void FeatsWithSpecificApplicationsAreFilled()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].SpecificApplicationType = "specific application";

            var schools = new[] { "school 1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatSpecificApplications, "specific application")).Returns(schools);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo(additionalFeatSelections[0].FeatName));
            Assert.That(onlyFeat.SpecificApplication, Is.EqualTo("school 1"));
        }

        [Test]
        public void FeatsWithSpecificApplicationsAreFilledRandomly()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].SpecificApplicationType = "specific application";

            var schools = new[] { "school 1", "school 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatSpecificApplications, "specific application")).Returns(schools);

            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo(additionalFeatSelections[0].FeatName));
            Assert.That(onlyFeat.SpecificApplication, Is.EqualTo("school 2"));
        }

        [Test]
        public void FeatsWithSpecificApplicationsCanBeFilledMoreThanOnce()
        {
            characterClass.Level = 3;
            AddFeatSelections(1);
            additionalFeatSelections[0].SpecificApplicationType = "specific application";

            var schools = new[] { "school 1", "school 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatSpecificApplications, "specific application")).Returns(schools);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo(additionalFeatSelections[0].FeatName));
            Assert.That(firstFeat.SpecificApplication, Is.EqualTo("school 1"));
            Assert.That(lastFeat.Name, Is.EqualTo(additionalFeatSelections[0].FeatName));
            Assert.That(lastFeat.SpecificApplication, Is.EqualTo("school 2"));
            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void SpellMasterySpecificApplicationIsNumberOfSpellsLearned()
        {
            characterClass.Level = 1;
            AddFeatSelections(1);
            additionalFeatSelections[0].FeatName = FeatConstants.SpellMastery;
            stats[StatConstants.Intelligence].Value = 14;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo(FeatConstants.SpellMastery));
            Assert.That(firstFeat.SpecificApplication, Is.EqualTo("2"));
            Assert.That(feats.Count(), Is.EqualTo(1));
        }

        [Test]
        public void SpellMasteryCanIncreaseSpellsKnown()
        {
            characterClass.Level = 3;
            AddFeatSelections(1);
            additionalFeatSelections[0].FeatName = FeatConstants.SpellMastery;
            stats[StatConstants.Intelligence].Value = 14;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo(FeatConstants.SpellMastery));
            Assert.That(firstFeat.SpecificApplication, Is.EqualTo("4"));
            Assert.That(feats.Count(), Is.EqualTo(1));
        }

        [Test]
        public void SpellcastersCanSelectRayForWeaponSpecificApplications()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].SpecificApplicationType = AdditionalFeatSelectionConstants.WeaponsWithUnarmedAndGrappleAndRay;

            var weapons = new[] { WeaponProficiencyConstants.Ray, "weapon" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatSpecificApplications, AdditionalFeatSelectionConstants.WeaponsWithUnarmedAndGrappleAndRay))
                .Returns(weapons);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, TableNameConstants.Set.Collection.Groups.Spellcasters))
                .Returns(new[] { characterClass.ClassName });

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo(additionalFeatSelections[0].FeatName));
            Assert.That(onlyFeat.SpecificApplication, Is.EqualTo(WeaponProficiencyConstants.Ray));
        }

        [Test]
        public void NonSpellcastersCannotSelectRayForWeaponSpecificApplications()
        {
            AddFeatSelections(1);
            additionalFeatSelections[0].SpecificApplicationType = AdditionalFeatSelectionConstants.WeaponsWithUnarmedAndGrappleAndRay;

            var weapons = new[] { WeaponProficiencyConstants.Ray, "weapon" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatSpecificApplications, AdditionalFeatSelectionConstants.WeaponsWithUnarmedAndGrappleAndRay))
                .Returns(weapons);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, TableNameConstants.Set.Collection.Groups.Spellcasters))
                .Returns(new[] { "other class name" });

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo(additionalFeatSelections[0].FeatName));
            Assert.That(onlyFeat.SpecificApplication, Is.EqualTo("weapon"));
        }

        [Test]
        public void FeatsWithoutSpecificApplicationsButWithRequirementsThatHaveSpecificApplicationsDoNotUseSameSpecificApplication()
        {
            characterClass.Level = 3;
            AddFeatSelections(2);
            additionalFeatSelections[0].SpecificApplicationType = "specific application";
            additionalFeatSelections[1].RequiredFeats = new[] { additionalFeatSelections[0].FeatName };

            var schools = new[] { "school 1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatSpecificApplications, "specific application")).Returns(schools);

            mockDice.SetupSequence(d => d.Roll(1).d(2)).Returns(2);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo(additionalFeatSelections[0].FeatName));
            Assert.That(firstFeat.SpecificApplication, Is.EqualTo("school 1"));
            Assert.That(lastFeat.Name, Is.EqualTo(additionalFeatSelections[1].FeatName));
            Assert.That(lastFeat.SpecificApplication, Is.Empty);
        }

        [Test]
        public void FeatsWithSpecificApplicationsAndRequirementsThatHaveSpecificApplicationsUseSameSpecificApplication()
        {
            characterClass.Level = 3;
            AddFeatSelections(2);
            additionalFeatSelections[0].SpecificApplicationType = "specific application";
            additionalFeatSelections[1].RequiredFeats = new[] { additionalFeatSelections[0].FeatName };
            additionalFeatSelections[1].SpecificApplicationType = "specific application";

            var schools = new[] { "school 1", "school 2", "school 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatSpecificApplications, "specific application")).Returns(schools);

            mockDice.SetupSequence(d => d.Roll(1).d(2)).Returns(2);
            mockDice.SetupSequence(d => d.Roll(1).d(3)).Returns(3).Returns(1);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo(additionalFeatSelections[0].FeatName));
            Assert.That(firstFeat.SpecificApplication, Is.EqualTo("school 3"));
            Assert.That(lastFeat.Name, Is.EqualTo(additionalFeatSelections[1].FeatName));
            Assert.That(lastFeat.SpecificApplication, Is.EqualTo("school 3"));
        }

        [Test]
        public void IfFeatRequirementHasMultipleSpecificApplications_PickRandomlyAmongThem()
        {
            characterClass.Level = 6;
            AddFeatSelections(2);
            additionalFeatSelections[0].SpecificApplicationType = "specific application";
            additionalFeatSelections[1].RequiredFeats = new[] { additionalFeatSelections[0].FeatName };
            additionalFeatSelections[1].SpecificApplicationType = "specific application";

            var schools = new[] { "school 1", "school 2", "school 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatSpecificApplications, "specific application")).Returns(schools);

            mockDice.SetupSequence(d => d.Roll(1).d(2)).Returns(1).Returns(1).Returns(2).Returns(2);
            mockDice.SetupSequence(d => d.Roll(1).d(3)).Returns(3).Returns(1);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var secondFeat = feats.ElementAt(1);
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo(additionalFeatSelections[0].FeatName));
            Assert.That(firstFeat.SpecificApplication, Is.EqualTo("school 3"));
            Assert.That(secondFeat.Name, Is.EqualTo(additionalFeatSelections[0].FeatName));
            Assert.That(secondFeat.SpecificApplication, Is.EqualTo("school 1"));
            Assert.That(lastFeat.Name, Is.EqualTo(additionalFeatSelections[1].FeatName));
            Assert.That(lastFeat.SpecificApplication, Is.EqualTo("school 1"));
        }
    }
}
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
        private List<FeatSelection> featSelections;
        private Mock<IDice> mockDice;
        private BaseAttack baseAttack;

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
            featSelections = new List<FeatSelection>();
            baseAttack = new BaseAttack();
            stats[StatConstants.Intelligence] = new Stat();

            mockFeatsSelector.Setup(s => s.SelectAll()).Returns(featSelections);
            mockDice.Setup(d => d.Roll(1).d(It.IsAny<Int32>())).Returns(1);
        }

        [Test]
        public void GetBaseRacialFeats()
        {
            race.BaseRace = "base race";
            var racialFeats = new[] { "feat 1", "feat 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("RacialFeats", "base race")).Returns(racialFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            foreach (var feat in racialFeats)
                Assert.That(featNames, Contains.Item(feat));
        }

        [Test]
        public void DoNotGetBaseRacialFeatsIfNone()
        {
            race.BaseRace = "base race";
            mockCollectionsSelector.Setup(s => s.SelectFrom("RacialFeats", "base race")).Returns(Enumerable.Empty<String>());

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void GetStrengthOfBaseRacialFeats()
        {
            race.BaseRace = "base race-format";
            var racialFeats = new[] { "feat 1", "feat 2", "feat 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("RacialFeats", race.BaseRace)).Returns(racialFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom("RacialFeatStrengths", race.BaseRace)).Returns(new[] { "feat 1", "feat 3" });
            mockCollectionsSelector.Setup(s => s.SelectFrom("baseraceformatFeatStrengths", "feat 1")).Returns(new[] { "+9266" });
            mockCollectionsSelector.Setup(s => s.SelectFrom("baseraceformatFeatStrengths", "feat 3")).Returns(new[] { "90210 ft." });

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats.First().SpecificApplication, Is.EqualTo("+9266"));
            Assert.That(feats.Last().SpecificApplication, Is.EqualTo("90210 ft."));
            Assert.That(feats.Where(f => !String.IsNullOrEmpty(f.SpecificApplication)), Is.EqualTo(2));
            Assert.That(feats.Where(f => String.IsNullOrEmpty(f.SpecificApplication)).Single().Name, Is.EqualTo("feat 2"));
            Assert.That(feats.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DoNotGetStrengthsOfBaseRacialFeatsIfNone()
        {
            race.BaseRace = "base race-format";
            var racialFeats = new[] { "feat 1", "feat 2", "feat 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("RacialFeats", race.BaseRace)).Returns(racialFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom("RacialFeatStrengths", race.BaseRace)).Returns(Enumerable.Empty<String>());
            mockCollectionsSelector.Setup(s => s.SelectFrom("baseraceformatFeatStrengths", It.IsAny<String>())).Returns(new[] { "strength" });

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats.All(f => String.IsNullOrEmpty(f.SpecificApplication)), Is.True);
        }

        [Test]
        public void GetMetaRacialFeats()
        {
            race.Metarace = "metarace";
            var racialFeats = new[] { "feat 1", "feat 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("RacialFeats", "metarace")).Returns(racialFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            foreach (var feat in racialFeats)
                Assert.That(featNames, Contains.Item(feat));
        }

        [Test]
        public void DoNotGetMetaracialFeatsIfNone()
        {
            race.Metarace = "metarace";
            mockCollectionsSelector.Setup(s => s.SelectFrom("RacialFeats", race.Metarace)).Returns(Enumerable.Empty<String>());

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void GetStrengthOfMetaracialFeats()
        {
            race.Metarace = "metarace-fo rmat";
            var racialFeats = new[] { "feat 1", "feat 2", "feat 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("RacialFeats", race.Metarace)).Returns(racialFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom("RacialFeatStrengths", race.Metarace)).Returns(new[] { "feat 1", "feat 3" });
            mockCollectionsSelector.Setup(s => s.SelectFrom("metaraceformatFeatStrengths", "feat 1")).Returns(new[] { "+9266" });
            mockCollectionsSelector.Setup(s => s.SelectFrom("metaraceformatFeatStrengths", "feat 3")).Returns(new[] { "90210 ft." });

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats.First().SpecificApplication, Is.EqualTo("+9266"));
            Assert.That(feats.Last().SpecificApplication, Is.EqualTo("90210 ft."));
            Assert.That(feats.Where(f => !String.IsNullOrEmpty(f.SpecificApplication)), Is.EqualTo(2));
            Assert.That(feats.Where(f => String.IsNullOrEmpty(f.SpecificApplication)).Single().Name, Is.EqualTo("feat 2"));
            Assert.That(feats.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DoNotGetStrengthsOfMetaracialFeatsIfNone()
        {
            race.Metarace = "metarace-fo rmat";
            var racialFeats = new[] { "feat 1", "feat 2", "feat 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("RacialFeats", race.Metarace)).Returns(racialFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom("RacialFeatStrengths", race.Metarace)).Returns(Enumerable.Empty<String>());
            mockCollectionsSelector.Setup(s => s.SelectFrom("metaraceformatFeatStrengths", It.IsAny<String>())).Returns(new[] { "strength" });

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats.All(f => String.IsNullOrEmpty(f.SpecificApplication)), Is.True);
        }

        [Test]
        public void OnlyKeepStrongestFeat()
        {
            Assert.Fail();
        }

        [Test]
        public void KeepBothFeats()
        {
            Assert.Fail();
        }

        [Test]
        public void AddStrengthsOfBothFeats()
        {
            Assert.Fail();
        }

        [Test]
        public void MeetMonsterHitDiceRequirementsForMetaracialFeats()
        {
            Assert.Fail();
        }

        [Test]
        public void NonMonstersHaveOneMonsterHitDieForSakeOfHitDiceRequirements()
        {
            Assert.Fail();
        }

        [Test]
        public void DoNotMeetMonsterHitDiceRequirementsForMetaracialFeats()
        {
            Assert.Fail();
        }

        [Test]
        public void IfGreaterThanMonsterHitDiceRequirementsForMetaracialFeat_DoNotGetFeat()
        {
            Assert.Fail();
        }

        [Test]
        public void MetaracialFeatForTypeOfHalfDragon()
        {
            Assert.Fail();
        }

        [Test]
        public void GetClassFeatsWithMatchingLevelRequirement()
        {
            characterClass.ClassName = "class name";
            characterClass.Level = 2;
            var classFeats = new[] { "feat 1", "feat 2", "feat 3", "feat 4" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("ClassFeats", "class name")).Returns(classFeats);

            var levelRequirements = new Dictionary<String, Int32>();
            levelRequirements["feat 1"] = 1;
            levelRequirements["feat 2"] = 1;
            levelRequirements["feat 3"] = 2;
            levelRequirements["feat 4"] = 3;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom("class nameFeatLevelRequirements")).Returns(levelRequirements);

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
            mockCollectionsSelector.Setup(s => s.SelectFrom("ClassFeats", "class name")).Returns(Enumerable.Empty<String>());

            var levelRequirements = new Dictionary<String, Int32>();
            mockAdjustmentsSelector.Setup(s => s.SelectFrom("class nameFeatLevelRequirements")).Returns(levelRequirements);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void DoNotGetClassFeatsIfNoneMatchRequirements()
        {
            characterClass.ClassName = "class name";
            characterClass.Level = 2;
            var classFeats = new[] { "feat 1", "feat 2", "feat 3", "feat 4" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("ClassFeats", "class name")).Returns(classFeats);

            var levelRequirements = new Dictionary<String, Int32>();
            levelRequirements["feat 1"] = 3;
            levelRequirements["feat 2"] = 3;
            levelRequirements["feat 3"] = 3;
            levelRequirements["feat 4"] = 3;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom("class nameFeatLevelRequirements")).Returns(levelRequirements);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void GetSkillSynergyFeatsWithMatchingRankRequirement()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom("SkillSynergyFeats", "skill 1")).Returns(new[] { "feat 1", "feat 2" });
            mockCollectionsSelector.Setup(s => s.SelectFrom("SkillSynergyFeats", "skill 2")).Returns(new[] { "feat 3" });
            mockCollectionsSelector.Setup(s => s.SelectFrom("SkillSynergyFeats", "skill 3")).Returns(new[] { "feat 4" });
            mockCollectionsSelector.Setup(s => s.SelectFrom("SkillSynergyFeats", "skill 4")).Returns(new[] { "feat 5", "feat 6" });

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
            mockCollectionsSelector.Setup(s => s.SelectFrom("SkillSynergyFeats", "skill 1")).Returns(Enumerable.Empty<String>());

            skills["skill 1"] = new Skill { Ranks = 5, ClassSkill = true };

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
                Assert.That(featNames, Contains.Item(featSelections[i].FeatName));

            Assert.That(featNames.Count(), Is.EqualTo(numberOfFeats));
        }

        private void AddFeatSelections(Int32 quantity)
        {
            for (var i = 0; i < quantity; i++)
            {
                var name = String.Format("feat {0}", i + 1);
                featSelections.Add(new FeatSelection { FeatName = name });
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
            featSelections[0].RequiredBaseAttack = 9266;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void FighterFeatsCanBeAdditionalFeats()
        {
            characterClass.Level = 1;
            AddFeatSelections(1);
            featSelections[0].IsFighterFeat = true;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[0].FeatName));
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

            Assert.That(featNames, Contains.Item(featSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[2].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void HumansGetAdditionalFeat()
        {
            race.BaseRace = RaceConstants.BaseRaces.Human;
            characterClass.Level = 1;
            AddFeatSelections(3);

            mockDice.Setup(d => d.Roll(1).d(3)).Returns(3);
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(1);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[2].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DoNotGetFeatsWithUnmetPrerequisite()
        {
            characterClass.Level = 1;
            AddFeatSelections(2);
            featSelections[0].RequiredBaseAttack = 9266;
            mockDice.Setup(d => d.Roll(1).d(It.IsAny<Int32>())).Returns(1);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[1].FeatName));
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
            foreach (var selection in featSelections)
                selection.IsFighterFeat = true;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);
            var additionalFeats = level / 3 + 1;
            var totalFeats = numberOfBonusFeats + additionalFeats;

            for (var i = 0; i < totalFeats; i++)
                Assert.That(featNames, Contains.Item(featSelections[i].FeatName));

            Assert.That(featNames.Count(), Is.EqualTo(totalFeats));
        }

        [Test]
        public void DoNotGetNonFighterFeats()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(3);
            featSelections[2].IsFighterFeat = true;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[2].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DoNotGetFighterFeatsIfNoneAvailable()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(1);
            featSelections[0].IsFighterFeat = true;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[0].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(1));
        }

        [Test]
        public void DoNotGetMoreFighterFeatsIfNoneAvailable()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 2;
            AddFeatSelections(3);
            featSelections[1].RequiredBaseAttack = 9266;
            featSelections[1].IsFighterFeat = true;
            featSelections[2].IsFighterFeat = true;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[2].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void FighterFeatsPickedAtRandom()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 2;
            AddFeatSelections(4);
            foreach (var feat in featSelections)
                feat.IsFighterFeat = true;

            mockDice.Setup(d => d.Roll(1).d(4)).Returns(4);
            mockDice.Setup(d => d.Roll(1).d(3)).Returns(1);
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[2].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[3].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DoNotGetFighterFeatWithUnmetPrerequisite()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(3);
            featSelections[1].IsFighterFeat = true;
            featSelections[1].RequiredFeats = new[] { "other feat" };
            featSelections[2].IsFighterFeat = true;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[2].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNoFighterFeatsAvailable_ThenStop()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(3);
            featSelections[1].IsFighterFeat = true;
            featSelections[1].RequiredFeats = new[] { "other feat" };

            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[2].FeatName));
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
        public void WizardsGetBonusMetamagicFeats(Int32 level, Int32 numberOfBonusFeats)
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = level;
            AddFeatSelections(20);
            foreach (var selection in featSelections)
                selection.IsMetamagicFeat = true;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);
            var additionalFeats = level / 3 + 1;
            var totalFeats = numberOfBonusFeats + additionalFeats;

            for (var i = 0; i < totalFeats; i++)
                Assert.That(featNames, Contains.Item(featSelections[i].FeatName));

            Assert.That(featNames.Count(), Is.EqualTo(totalFeats));
        }

        [Test]
        public void DoNotGetNonMetamagicFeats()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = 5;
            AddFeatSelections(5);
            featSelections[3].IsMetamagicFeat = true;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[1].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[2].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[4].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(4));
        }

        [Test]
        public void DoNotGetMetamagicFeatsIfNoneAvailable()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = 5;
            AddFeatSelections(4);
            featSelections[0].IsMetamagicFeat = true;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[1].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[2].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DoNotGetMoreMetamagicFeatsIfNoneAvailable()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = 10;
            AddFeatSelections(6);
            featSelections[1].IsMetamagicFeat = true;
            featSelections[5].IsMetamagicFeat = true;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[1].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[2].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[3].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[5].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(5));
        }

        [Test]
        public void MetamagicFeatsPickedAtRandom()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            characterClass.Level = 10;
            AddFeatSelections(7);
            foreach (var feat in featSelections)
                feat.IsMetamagicFeat = true;

            mockDice.Setup(d => d.Roll(1).d(7)).Returns(7);
            mockDice.Setup(d => d.Roll(1).d(6)).Returns(6);
            mockDice.Setup(d => d.Roll(1).d(5)).Returns(5);
            mockDice.Setup(d => d.Roll(1).d(4)).Returns(4);
            mockDice.Setup(d => d.Roll(1).d(3)).Returns(1);
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[2].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[3].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[4].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[5].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[6].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(3));
        }

        [Test]
        public void ReassessPrerequisitesEveryFeat()
        {
            characterClass.Level = 3;
            AddFeatSelections(3);
            featSelections[1].RequiredFeats = new[] { featSelections[0].FeatName };

            mockDice.Setup(d => d.Roll(1).d(3)).Returns(1);
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(1);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[0].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[1].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllTheFeats()
        {
            race.BaseRace = RaceConstants.BaseRaces.Human;
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 20;
            AddFeatSelections(20);
            foreach (var feat in featSelections)
                feat.IsFighterFeat = true;

            var baseRacialFeats = new[] { "base race feat 1", "base race feat 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("RacialFeats", race.BaseRace)).Returns(baseRacialFeats);

            race.Metarace = "metarace";
            var metaracialFeats = new[] { "metarace feat 1", "metarace feat 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("RacialFeats", race.Metarace)).Returns(metaracialFeats);

            var classFeats = new[] { "class feat 1", "class feat 2", "class feat 3", "class feat 4" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("ClassFeats", characterClass.ClassName)).Returns(classFeats);

            var levelRequirements = new Dictionary<String, Int32>();
            levelRequirements["class feat 1"] = 1;
            levelRequirements["class feat 2"] = 10;
            levelRequirements["class feat 3"] = 15;
            levelRequirements["class feat 4"] = 20;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom("FighterFeatLevelRequirements")).Returns(levelRequirements);

            mockCollectionsSelector.Setup(s => s.SelectFrom("SkillSynergyFeats", "skill 1")).Returns(new[] { "synergy feat 1", "synergy feat 2" });
            mockCollectionsSelector.Setup(s => s.SelectFrom("SkillSynergyFeats", "skill 2")).Returns(new[] { "synergy feat 3" });

            skills["skill 1"] = new Skill { Ranks = 5, ClassSkill = true };
            skills["skill 2"] = new Skill { Ranks = 10, ClassSkill = false };

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            for (var i = 0; i < 19; i++)
                Assert.That(featNames, Contains.Item(featSelections[i].FeatName));
            Assert.That(featNames, Is.Not.Contains(featSelections[19].FeatName));

            foreach (var feat in baseRacialFeats)
                Assert.That(featNames, Contains.Item(feat));

            foreach (var feat in metaracialFeats)
                Assert.That(featNames, Contains.Item(feat));

            foreach (var feat in classFeats)
                Assert.That(featNames, Contains.Item(feat));

            Assert.That(featNames, Contains.Item("synergy feat 1"));
            Assert.That(featNames, Contains.Item("synergy feat 2"));
            Assert.That(featNames, Contains.Item("synergy feat 3"));
        }

        [Test]
        public void CannotGetDuplicateFeats()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(6);
            foreach (var feat in featSelections)
                feat.IsFighterFeat = true;

            race.BaseRace = "base race";
            var baseRacialFeats = new[] { "feat 1", "auto feat 1", "auto feat 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("RacialFeats", race.BaseRace)).Returns(baseRacialFeats);

            race.Metarace = "metarace";
            var metaracialFeats = new[] { "feat 2", "auto feat 1", "auto feat 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("RacialFeats", race.Metarace)).Returns(metaracialFeats);

            var classFeats = new[] { "feat 3", "auto feat 2", "auto feat 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("ClassFeats", characterClass.ClassName)).Returns(classFeats);

            var levelRequirements = new Dictionary<String, Int32>();
            foreach (var feat in classFeats)
                levelRequirements[feat] = 0;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom("FighterFeatLevelRequirements")).Returns(levelRequirements);

            //NOTE: Skill Synergy feats are unique and cannot be selected or earned
            //in any way except through rank requirements.

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item("feat 1"));
            Assert.That(featNames, Contains.Item("feat 2"));
            Assert.That(featNames, Contains.Item("feat 3"));
            Assert.That(featNames, Contains.Item("feat 4"));
            Assert.That(featNames, Contains.Item("feat 5"));
            Assert.That(featNames, Contains.Item("auto feat 1"));
            Assert.That(featNames, Contains.Item("auto feat 2"));
            Assert.That(featNames, Contains.Item("auto feat 3"));
            Assert.That(featNames.Count(), Is.EqualTo(8));
        }

        [Test]
        public void FeatsWithoutSpecificApplicationsDoNotFill()
        {
            AddFeatSelections(1);

            var schools = new[] { "school 1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("FeatSpecificApplications", "specific application")).Returns(schools);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var onlyFeat = feats.Single();

            mockCollectionsSelector.Verify(s => s.SelectFrom("FeatSpecificApplications", It.IsAny<String>()), Times.Never);
            Assert.That(onlyFeat.Name, Is.EqualTo(featSelections[0].FeatName));
            Assert.That(onlyFeat.SpecificApplication, Is.Empty);
        }

        [Test]
        public void FeatsWithSpecificApplicationsAreFilled()
        {
            AddFeatSelections(1);
            featSelections[0].SpecificApplicationType = "specific application";

            var schools = new[] { "school 1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("FeatSpecificApplications", "specific application")).Returns(schools);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo(featSelections[0].FeatName));
            Assert.That(onlyFeat.SpecificApplication, Is.EqualTo("school 1"));
        }

        [Test]
        public void FeatsWithSpecificApplicationsAreFilledRandomly()
        {
            AddFeatSelections(1);
            featSelections[0].SpecificApplicationType = "specific application";

            var schools = new[] { "school 1", "school 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("FeatSpecificApplications", "specific application")).Returns(schools);

            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var onlyFeat = feats.Single();
            Assert.That(onlyFeat.Name, Is.EqualTo(featSelections[0].FeatName));
            Assert.That(onlyFeat.SpecificApplication, Is.EqualTo("school 2"));
        }

        [Test]
        public void FeatsWithSpecificApplicationsCanBeFilledMoreThanOnce()
        {
            characterClass.Level = 3;
            AddFeatSelections(1);
            featSelections[0].SpecificApplicationType = "specific application";

            var schools = new[] { "school 1", "school 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("FeatSpecificApplications", "specific application")).Returns(schools);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo(featSelections[0].FeatName));
            Assert.That(firstFeat.SpecificApplication, Is.EqualTo("school 1"));
            Assert.That(lastFeat.Name, Is.EqualTo(featSelections[0].FeatName));
            Assert.That(lastFeat.SpecificApplication, Is.EqualTo("school 2"));
            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void SpellMasterySpecificApplicationIsNumberOfSpellsLearned()
        {
            characterClass.Level = 1;
            AddFeatSelections(1);
            featSelections[0].FeatName = FeatConstants.SpellMastery;
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
            featSelections[0].FeatName = FeatConstants.SpellMastery;
            stats[StatConstants.Intelligence].Value = 14;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo(FeatConstants.SpellMastery));
            Assert.That(firstFeat.SpecificApplication, Is.EqualTo("4"));
            Assert.That(feats.Count(), Is.EqualTo(1));
        }

        [Test]
        public void SpellcastersCanSelectRayForSpecificApplications()
        {
            AddFeatSelections(1);
            featSelections[0].SpecificApplicationType = FeatSelectionConstants.WeaponsWithUnarmedAndGrappleAndRay;

            var weapons = new[] { WeaponProficiencyConstants.Ray, "weapon" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("FeatSpecificApplications", FeatSelectionConstants.WeaponsWithUnarmedAndGrappleAndRay))
                .Returns(weapons);

            mockCollectionsSelector.Setup(s => s.SelectFrom("ClassNameGroups", "Spellcasters")).Returns(new[] { characterClass.ClassName });

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo(featSelections[0].FeatName));
            Assert.That(onlyFeat.SpecificApplication, Is.EqualTo(WeaponProficiencyConstants.Ray));
        }

        [Test]
        public void NonSpellcastersCannotSelectRayForSpecificApplications()
        {
            AddFeatSelections(1);
            featSelections[0].SpecificApplicationType = FeatSelectionConstants.WeaponsWithUnarmedAndGrappleAndRay;

            var weapons = new[] { WeaponProficiencyConstants.Ray, "weapon" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("FeatSpecificApplications", FeatSelectionConstants.WeaponsWithUnarmedAndGrappleAndRay))
                .Returns(weapons);

            mockCollectionsSelector.Setup(s => s.SelectFrom("ClassNameGroups", "Spellcasters")).Returns(new[] { "other class name" });

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo(featSelections[0].FeatName));
            Assert.That(onlyFeat.SpecificApplication, Is.EqualTo("weapon"));
        }

        [Test]
        public void FeatsWithoutSpecificApplicationsButWithRequirementsThatHaveSpecificApplicationsDoNotUseSameSpecificApplication()
        {
            characterClass.Level = 3;
            AddFeatSelections(2);
            featSelections[0].SpecificApplicationType = "specific application";
            featSelections[1].RequiredFeats = new[] { featSelections[0].FeatName };

            var schools = new[] { "school 1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("FeatSpecificApplications", "specific application")).Returns(schools);

            mockDice.SetupSequence(d => d.Roll(1).d(2)).Returns(2);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo(featSelections[0].FeatName));
            Assert.That(firstFeat.SpecificApplication, Is.EqualTo("school 1"));
            Assert.That(lastFeat.Name, Is.EqualTo(featSelections[1].FeatName));
            Assert.That(lastFeat.SpecificApplication, Is.Empty);
        }

        [Test]
        public void FeatsWithSpecificApplicationsAndRequirementsThatHaveSpecificApplicationsUseSameSpecificApplication()
        {
            characterClass.Level = 3;
            AddFeatSelections(2);
            featSelections[0].SpecificApplicationType = "specific application";
            featSelections[1].RequiredFeats = new[] { featSelections[0].FeatName };
            featSelections[1].SpecificApplicationType = "specific application";

            var schools = new[] { "school 1", "school 2", "school 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("FeatSpecificApplications", "specific application")).Returns(schools);

            mockDice.SetupSequence(d => d.Roll(1).d(2)).Returns(2);
            mockDice.SetupSequence(d => d.Roll(1).d(3)).Returns(3).Returns(1);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo(featSelections[0].FeatName));
            Assert.That(firstFeat.SpecificApplication, Is.EqualTo("school 3"));
            Assert.That(lastFeat.Name, Is.EqualTo(featSelections[1].FeatName));
            Assert.That(lastFeat.SpecificApplication, Is.EqualTo("school 3"));
        }

        [Test]
        public void IfFeatRequirementHasMultipleSpecificApplications_PickRandomlyAmongThem()
        {
            characterClass.Level = 6;
            AddFeatSelections(2);
            featSelections[0].SpecificApplicationType = "specific application";
            featSelections[1].RequiredFeats = new[] { featSelections[0].FeatName };
            featSelections[1].SpecificApplicationType = "specific application";

            var schools = new[] { "school 1", "school 2", "school 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom("FeatSpecificApplications", "specific application")).Returns(schools);

            mockDice.SetupSequence(d => d.Roll(1).d(2)).Returns(1).Returns(1).Returns(2).Returns(2);
            mockDice.SetupSequence(d => d.Roll(1).d(3)).Returns(3).Returns(1);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var secondFeat = feats.ElementAt(1);
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo(featSelections[0].FeatName));
            Assert.That(firstFeat.SpecificApplication, Is.EqualTo("school 3"));
            Assert.That(secondFeat.Name, Is.EqualTo(featSelections[0].FeatName));
            Assert.That(secondFeat.SpecificApplication, Is.EqualTo("school 1"));
            Assert.That(lastFeat.Name, Is.EqualTo(featSelections[1].FeatName));
            Assert.That(lastFeat.SpecificApplication, Is.EqualTo("school 1"));
        }
    }
}
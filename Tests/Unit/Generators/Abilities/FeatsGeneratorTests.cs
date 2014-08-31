using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using Moq;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
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

            mockFeatsSelector.Setup(s => s.SelectAll()).Returns(featSelections);
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
        public void DoNotGetBaseRacialFeatIfNone()
        {
            race.BaseRace = "base race";
            mockCollectionsSelector.Setup(s => s.SelectFrom("RacialFeats", "base race")).Returns(Enumerable.Empty<String>());

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats, Is.Empty);
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
        public void DoNotGetMetaracialFeatIfNone()
        {
            race.Metarace = "metarace";
            mockCollectionsSelector.Setup(s => s.SelectFrom("RacialFeats", race.Metarace)).Returns(Enumerable.Empty<String>());

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats, Is.Empty);
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
            for (var i = 0; i < numberOfFeats; i++)
                Assert.That(feats, Contains.Item(featSelections[i].FeatName));

            Assert.That(feats.Count(), Is.EqualTo(numberOfFeats));
        }

        private void AddFeatSelections(Int32 quantity)
        {
            for (var i = quantity; i > 0; i--)
            {
                var name = String.Format("feat {0}", i);
                featSelections.Add(new FeatSelection { FeatName = name });
            }
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
            for (var i = 0; i < 12; i++)
                featSelections[i].IsFighterFeat = true;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            for (var i = 0; i < numberOfBonusFeats; i++)
                Assert.That(featNames, Contains.Item(featSelections[i].FeatName));

            var fighterFeats = featSelections.Where(f => f.IsFighterFeat);
            for (var i = numberOfBonusFeats; i < fighterFeats.Count(); i++)
                Assert.That(featNames, Is.Not.Contains(featSelections[i].FeatName));
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
            featSelections[0].IsFighterFeat = true;
            featSelections[1].IsFighterFeat = true;
            featSelections[1].RequiredFeats.Add(featSelections[0].FeatName);
            featSelections[1].RequiredFeats.Add("other feat");

            mockDice.Setup(d => d.Roll(1).d(3)).Returns(3);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[2].FeatName));
            Assert.That(featNames, Contains.Item(featSelections[1].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNoFighterFeatsAvailable_ThenStop()
        {
            characterClass.ClassName = CharacterClassConstants.Fighter;
            characterClass.Level = 1;
            AddFeatSelections(3);
            featSelections[1].IsFighterFeat = true;
            featSelections[1].RequiredFeats.Add("other feat");

            mockDice.Setup(d => d.Roll(1).d(3)).Returns(3);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[2].FeatName));
            Assert.That(featNames.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ReassessPrerequisitesEveryFeat()
        {
            characterClass.Level = 3;
            AddFeatSelections(3);
            featSelections[1].RequiredFeats.Add(featSelections[0].FeatName);

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
        public void FeatsWithSpecificApplicationsOfSchoolsOfMagicAreFilled()
        {
            Assert.Fail();
        }

        [Test]
        public void FeatsWithSpecificApplicationsOfSkillsAreFilled()
        {
            Assert.Fail();
        }

        [Test]
        public void SpellMasteryIsRepeatable()
        {
            Assert.Fail();
        }

        [Test]
        public void FeatsWithSpecificApplicationsOfWeaponProficienciesAreFilled()
        {
            Assert.Fail();
        }

        [Test]
        public void FeatsWithSpecificApplicationsOfWeaponsAreFilled()
        {
            Assert.Fail();
        }

        [Test]
        public void FeatsWithSpecificApplicationsOfWeaponsIncludeUnarmedStrike()
        {
            Assert.Fail();
        }

        [Test]
        public void FeatsWithSpecificApplicationsOfWeaponsIncludeGrapple()
        {
            Assert.Fail();
        }

        [Test]
        public void SpellcastersCanSelectRayAsAWeaponFocus()
        {
            Assert.Fail();
        }

        [Test]
        public void FeatsWithRequirementsThatHaveSpecificApplicationsUseSameSpecificApplication()
        {
            Assert.Fail();
        }

        [Test]
        public void IfFeatRequirementHasMultipleSpecificApplications_PickRandomlyAmongThem()
        {
            Assert.Fail();
        }
    }
}
using CharacterGen.Abilities.Feats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators.Combats;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Combats
{
    [TestFixture]
    public class HitPointsGeneratorTests
    {
        private Mock<Dice> mockDice;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private IHitPointsGenerator hitPointsGenerator;

        private CharacterClass characterClass;
        private Race race;
        private int constitutionBonus;
        private Dictionary<string, int> hitDice;
        private List<Feat> feats;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<Dice>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            hitPointsGenerator = new HitPointsGenerator(mockDice.Object, mockAdjustmentsSelector.Object, mockCollectionsSelector.Object);

            characterClass = new CharacterClass();
            race = new Race();
            feats = new List<Feat>();
            hitDice = new Dictionary<string, int>();

            characterClass.ClassName = "class name";
            race.Metarace = "metarace";
            constitutionBonus = 0;
            hitDice[characterClass.ClassName] = 9266;
            hitDice["otherclassname"] = 42;

            mockDice.Setup(d => d.Roll(0).IndividualRolls(8)).Returns(new[] { 0 });
            mockDice.Setup(d => d.Roll(1).IndividualRolls(9266)).Returns(new[] { 42 });

            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.ClassHitDice)).Returns(hitDice);
        }

        [Test]
        public void GetHitDieFromAdjustments()
        {
            characterClass.Level = 1;
            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(42));
        }

        [Test]
        public void RollHitPointsPerLevel()
        {
            characterClass.Level = 600;
            mockDice.Setup(d => d.Roll(600).IndividualRolls(9266)).Returns(new[] { 90210 });

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(90210));
        }

        [Test]
        public void ConstitutionBonusAppliedPerLevel()
        {
            characterClass.Level = 2;
            constitutionBonus = 5;
            mockDice.Setup(d => d.Roll(2).IndividualRolls(9266)).Returns(new[] { 90210, 42 });

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(90262));
        }

        [Test]
        public void CannotGainFewerThan1HitPointPerLevel()
        {
            characterClass.Level = 5;
            constitutionBonus = int.MinValue;
            mockDice.Setup(d => d.Roll(5).IndividualRolls(9266)).Returns(new[] { 90210, 42, 600, 1337, 1234 });

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(5));
        }

        [Test]
        public void NonMonsterDoNotGetAdditionalHitDice()
        {
            characterClass.Level = 2;
            mockDice.Setup(d => d.Roll(2).IndividualRolls(9266)).Returns(new[] { 90210, 42 });

            race.BaseRace = "differentbaserace";
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "otherbaserace", "baserace" });

            var monsterHitDice = new Dictionary<string, int>();
            monsterHitDice["monster"] = 1;
            monsterHitDice["baserace"] = 3;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(90252));
            mockAdjustmentsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice), Times.Never);
        }

        [Test]
        public void MonstersGetAdditionalHitDice()
        {
            characterClass.Level = 2;
            mockDice.Setup(d => d.Roll(2).IndividualRolls(9266)).Returns(new[] { 90210, 42 });

            race.BaseRace = "baserace";
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "otherbaserace", "baserace" });

            var monsterHitDice = new Dictionary<string, int>();
            monsterHitDice["monster"] = 1;
            monsterHitDice["baserace"] = 3;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);

            mockDice.Setup(d => d.Roll(3).IndividualRolls(8)).Returns(new[] { 600, 1337, 1234 });

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(93423));
        }

        [Test]
        public void MonstersApplyConstitutionBonusForEachAdditionalHitDie()
        {
            characterClass.Level = 2;
            mockDice.Setup(d => d.Roll(2).IndividualRolls(9266)).Returns(new[] { 90210, 42 });

            race.BaseRace = "baserace";
            constitutionBonus = 5;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "otherbaserace", "baserace" });

            var monsterHitDice = new Dictionary<string, int>();
            monsterHitDice["monster"] = 1;
            monsterHitDice["baserace"] = 3;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);

            mockDice.Setup(d => d.Roll(3).IndividualRolls(8)).Returns(new[] { 600, 1337, 1234 });

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(93448));
        }

        [Test]
        public void MonstersCannotGainFewerThan1HitPointPerHitDie()
        {
            characterClass.Level = 2;
            mockDice.Setup(d => d.Roll(2).IndividualRolls(9266)).Returns(new[] { 90210, 42 });

            race.BaseRace = "baserace";
            constitutionBonus = int.MinValue;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "otherbaserace", "baserace" });

            var monsterHitDice = new Dictionary<string, int>();
            monsterHitDice["monster"] = 1;
            monsterHitDice["baserace"] = 3;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);

            mockDice.Setup(d => d.Roll(3).IndividualRolls(8)).Returns(new[] { 600, 1337, 1234 });

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(5));
        }

        [Test]
        public void HalfDragonIncreasesMonsterHitDie()
        {
            characterClass.Level = 2;
            mockDice.Setup(d => d.Roll(2).IndividualRolls(9266)).Returns(new[] { 90210, 42 });

            race.BaseRace = "baserace";
            race.Metarace = RaceConstants.Metaraces.HalfDragon;
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "otherbaserace", "baserace" });

            var monsterHitDice = new Dictionary<string, int>();
            monsterHitDice["monster"] = 1;
            monsterHitDice["baserace"] = 3;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);

            mockDice.Setup(d => d.Roll(3).IndividualRolls(10)).Returns(new[] { 2345, 3456, 4567 });
            mockDice.Setup(d => d.Roll(3).IndividualRolls(8)).Returns(new[] { 600, 1337, 1234 });

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(100620));
            mockDice.Verify(d => d.Roll(It.IsAny<int>()).IndividualRolls(8), Times.Never);
        }

        [Test]
        public void UndeadIncreasesMonsterHitDie()
        {
            characterClass.Level = 2;

            var mockClassPartialRoll = new Mock<PartialRoll>();
            mockClassPartialRoll.Setup(r => r.IndividualRolls(12)).Returns(new[] { 90210, 42 });
            mockDice.Setup(d => d.Roll(2)).Returns(mockClassPartialRoll.Object);

            race.BaseRace = "baserace";
            race.Metarace = "undead";
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "otherbaserace", "baserace" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Undead))
                .Returns(new[] { "undead", "other undead" });

            var monsterHitDice = new Dictionary<string, int>();
            monsterHitDice["monster"] = 1;
            monsterHitDice["baserace"] = 3;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);

            var mockRacePartialRoll = new Mock<PartialRoll>();
            mockRacePartialRoll.Setup(r => r.IndividualRolls(12)).Returns(new[] { 5678, 6789, 7890 });
            mockRacePartialRoll.Setup(r => r.IndividualRolls(10)).Returns(new[] { 2345, 3456, 4567 });
            mockRacePartialRoll.Setup(r => r.IndividualRolls(8)).Returns(new[] { 600, 1337, 1234 });
            mockDice.Setup(d => d.Roll(3)).Returns(mockRacePartialRoll.Object);

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(110609));
            mockDice.Verify(d => d.Roll(It.IsAny<int>()).IndividualRolls(8), Times.Never);
            mockDice.Verify(d => d.Roll(It.IsAny<int>()).IndividualRolls(10), Times.Never);
        }

        [Test]
        public void UndeadSetsClassHitDieTo12()
        {
            characterClass.Level = 2;
            mockDice.Setup(d => d.Roll(2).IndividualRolls(12)).Returns(new[] { 90210, 42 });

            race.Metarace = "undead";
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Undead))
                .Returns(new[] { "undead", "other undead" });

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(90252));
            mockDice.Verify(d => d.Roll(It.IsAny<int>()).IndividualRolls(9266), Times.Never);
        }

        [Test]
        public void ToughnessIncreassHitPoints()
        {
            feats.Add(new Feat { Name = FeatConstants.Toughness, Power = 3 });

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(45));
        }

        [Test]
        public void ToughnessIncreassHitPointsMultipleTimes()
        {
            feats.Add(new Feat { Name = FeatConstants.Toughness, Power = 3 });
            feats.Add(new Feat { Name = FeatConstants.Toughness, Power = 3 });

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(48));
        }

        [Test]
        public void MinimumCheckAppliedPerLevel()
        {
            characterClass.Level = 3;
            constitutionBonus = -2;

            mockDice.SetupSequence(d => d.Roll(3).IndividualRolls(9266)).Returns(new[] { 1, 2, 4 });

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(4));
        }

        [Test]
        public void MinimumCheckAppliedPerMonsterHitDie()
        {
            characterClass.Level = 2;
            mockDice.Setup(d => d.Roll(2).IndividualRolls(9266)).Returns(new[] { 90210, 42 });

            race.BaseRace = "baserace";
            constitutionBonus = -2;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "otherbaserace", "baserace" });

            var monsterHitDice = new Dictionary<string, int>();
            monsterHitDice["monster"] = 1234;
            monsterHitDice["baserace"] = 3;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);

            mockDice.SetupSequence(d => d.Roll(3).IndividualRolls(8)).Returns(new[] { 1, 2, 4 });

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(90252));
        }

        [Test]
        public void MinimumCheckAppliedPerUndeadHitDie()
        {
            characterClass.Level = 5;
            var mockClassPartialRoll = new Mock<PartialRoll>();
            mockClassPartialRoll.Setup(r => r.IndividualRolls(12)).Returns(new[] { 4, 5, 7, 10, 12 });
            mockDice.Setup(d => d.Roll(5)).Returns(mockClassPartialRoll.Object);
            constitutionBonus = -5;

            race.BaseRace = "baserace";
            race.Metarace = "undead";
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "otherbaserace", "baserace" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Undead))
                .Returns(new[] { "undead", "other undead" });

            var monsterHitDice = new Dictionary<string, int>();
            monsterHitDice["monster"] = 1;
            monsterHitDice["baserace"] = 3;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);

            var mockRacePartialRoll = new Mock<PartialRoll>();
            mockRacePartialRoll.Setup(r => r.IndividualRolls(12)).Returns(new[] { 1, 3, 8 });
            mockDice.Setup(d => d.Roll(3)).Returns(mockRacePartialRoll.Object);

            var hitPoints = hitPointsGenerator.GenerateWith(characterClass, constitutionBonus, race, feats);
            Assert.That(hitPoints, Is.EqualTo(21));
        }
    }
}
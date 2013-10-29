using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Xml.Parsers;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.CharacterClasses
{
    [TestFixture]
    public class LevelAdjustmentsTests
    {
        private Dictionary<String, Int32> levelAdjustments;

        [SetUp]
        public void Setup()
        {
            var streamLoader = new EmbeddedResourceStreamLoader();
            var parser = new StatAdjustmentXmlParser(streamLoader);
            levelAdjustments = parser.Parse("LevelAdjustments.xml");
        }

        [Test]
        public void AasimarLevelAdjustmentIsZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.Aasimar], Is.EqualTo(0));
        }

        [Test]
        public void BugbearLevelAdjustmentIsTwo()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.Bugbear], Is.EqualTo(2));
        }

        [Test]
        public void DerroDwarfLevelAdjustmentIsOne()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.DerroDwarf], Is.EqualTo(1));
        }

        [Test]
        public void DoppelgangerLevelAdjustmentIsThree()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.Doppelganger], Is.EqualTo(3));
        }

        [Test]
        public void DrowLevelAdjustmentIsOne()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.Drow], Is.EqualTo(1));
        }

        [Test]
        public void DuergarLevelAdjustmentIsOne()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.Duergar], Is.EqualTo(1));
        }

        [Test]
        public void DeepDwarfLevelAdjustmentIsZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.DeepDwarf], Is.EqualTo(0));
        }

        [Test]
        public void HillDwarfLevelAdjustmentIsZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.HillDwarf], Is.EqualTo(0));
        }

        [Test]
        public void MountainDwarfLevelAdjustmentIsZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.MountainDwarf], Is.EqualTo(0));
        }

        [Test]
        public void GrayElfLevelAdjustmentIsZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.GrayElf], Is.EqualTo(0));
        }

        [Test]
        public void HighElfLevelAdjustmentIsZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.HighElf], Is.EqualTo(0));
        }

        [Test]
        public void WildElfLevelAdjustmentIsZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.WildElf], Is.EqualTo(0));
        }

        [Test]
        public void WoodElfLevelAdjustmentIsZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.WoodElf], Is.EqualTo(0));
        }

        [Test]
        public void GnollLevelAdjustmentIsOne()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.Gnoll], Is.EqualTo(1));
        }

        [Test]
        public void ForestGnomeLevelAdjustmentZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.ForestGnome], Is.EqualTo(0));
        }

        [Test]
        public void RockGnomeLevelAdjustmentIsZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.RockGnome], Is.EqualTo(0));
        }

        [Test]
        public void SvirfneblinLevelAdjustmentIsOne()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.Svirfneblin], Is.EqualTo(1));
        }

        [Test]
        public void GoblinLevelAdjustmentIsZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.Goblin], Is.EqualTo(0));
        }

        [Test]
        public void HalfCelestialLevelAdjustmentIsOne()
        {
            Assert.That(levelAdjustments[RaceConstants.Metaraces.HalfCelestial], Is.EqualTo(1));
        }

        [Test]
        public void HalfDragonLevelAdjustmentIsTwo()
        {
            Assert.That(levelAdjustments[RaceConstants.Metaraces.HalfDragon], Is.EqualTo(2));
        }

        [Test]
        public void HalfElfLevelAdjustmentIsZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.HalfElf], Is.EqualTo(0));
        }

        [Test]
        public void HalfFiendLevelAdjustmentIsTwo()
        {
            Assert.That(levelAdjustments[RaceConstants.Metaraces.HalfFiend], Is.EqualTo(2));
        }

        [Test]
        public void HalfOrcLevelAdjustmentIsZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.HalfOrc], Is.EqualTo(0));
        }

        [Test]
        public void DeepHalflingLevelAdjustmentIsZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.DeepHalfling], Is.EqualTo(0));
        }

        [Test]
        public void LightfootHalflingLevelAdjustmentIsZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.LightfootHalfling], Is.EqualTo(0));
        }

        [Test]
        public void TallfellowHalflingLevelAdjustmentIsZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.TallfellowHalfling], Is.EqualTo(0));
        }

        [Test]
        public void HobgoblinLevelAdjustmentIsZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.Hobgoblin], Is.EqualTo(0));
        }

        [Test]
        public void HumanLevelAdjustmentIsZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.Human], Is.EqualTo(0));
        }

        [Test]
        public void KoboldLevelAdjustmentIsZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.Kobold], Is.EqualTo(0));
        }

        [Test]
        public void LizardfolkLevelAdjustmentIsZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.Lizardfolk], Is.EqualTo(0));
        }

        [Test]
        public void MindFlayerLevelAdjustmentIsEight()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.MindFlayer], Is.EqualTo(8));
        }

        [Test]
        public void MinotaurLevelAdjustmentIsFour()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.Minotaur], Is.EqualTo(4));
        }

        [Test]
        public void OgreLevelAdjustmentIsTwo()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.Ogre], Is.EqualTo(2));
        }

        [Test]
        public void OgreMageLevelAdjustmentIsEight()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.OgreMage], Is.EqualTo(8));
        }

        [Test]
        public void OrcLevelAdjustmentIsZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.Orc], Is.EqualTo(0));
        }

        [Test]
        public void TieflingLevelAdjustmentIsZero()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.Tiefling], Is.EqualTo(0));
        }

        [Test]
        public void TroglodyteLevelAdjustmentIsOne()
        {
            Assert.That(levelAdjustments[RaceConstants.BaseRaces.Troglodyte], Is.EqualTo(1));
        }

        [Test]
        public void WerebearLevelAdjustmentIsTwo()
        {
            Assert.That(levelAdjustments[RaceConstants.Metaraces.Werebear], Is.EqualTo(2));
        }

        [Test]
        public void WereboarLevelAdjustmentIsOne()
        {
            Assert.That(levelAdjustments[RaceConstants.Metaraces.Wereboar], Is.EqualTo(1));
        }

        [Test]
        public void WereratLevelAdjustmentIsOne()
        {
            Assert.That(levelAdjustments[RaceConstants.Metaraces.Wererat], Is.EqualTo(1));
        }

        [Test]
        public void WeretigerLevelAdjustmentIsOne()
        {
            Assert.That(levelAdjustments[RaceConstants.Metaraces.Weretiger], Is.EqualTo(1));
        }

        [Test]
        public void WerewolfLevelAdjustmentIsOne()
        {
            Assert.That(levelAdjustments[RaceConstants.Metaraces.Werewolf], Is.EqualTo(1));
        }
    }
}
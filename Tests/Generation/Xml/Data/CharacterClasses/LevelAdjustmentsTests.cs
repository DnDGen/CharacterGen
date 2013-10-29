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
        private Dictionary<String, Int32> adjustments;

        [SetUp]
        public void Setup()
        {
            var streamLoader = new EmbeddedResourceStreamLoader();
            var parser = new AdjustmentXmlParser(streamLoader);
            adjustments = parser.Parse("LevelAdjustments.xml");
        }

        [Test]
        public void AasimarLevelAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Aasimar], Is.EqualTo(0));
        }

        [Test]
        public void BugbearLevelAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Bugbear], Is.EqualTo(2));
        }

        [Test]
        public void DerroDwarfLevelAdjustmentIsOne()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Derro], Is.EqualTo(1));
        }

        [Test]
        public void DoppelgangerLevelAdjustmentIsThree()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Doppelganger], Is.EqualTo(3));
        }

        [Test]
        public void DrowLevelAdjustmentIsOne()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Drow], Is.EqualTo(1));
        }

        [Test]
        public void DuergarLevelAdjustmentIsOne()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.DuergarDwarf], Is.EqualTo(1));
        }

        [Test]
        public void DeepDwarfLevelAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.DeepDwarf], Is.EqualTo(0));
        }

        [Test]
        public void HillDwarfLevelAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HillDwarf], Is.EqualTo(0));
        }

        [Test]
        public void MountainDwarfLevelAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.MountainDwarf], Is.EqualTo(0));
        }

        [Test]
        public void GrayElfLevelAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.GrayElf], Is.EqualTo(0));
        }

        [Test]
        public void HighElfLevelAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HighElf], Is.EqualTo(0));
        }

        [Test]
        public void WildElfLevelAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.WildElf], Is.EqualTo(0));
        }

        [Test]
        public void WoodElfLevelAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.WoodElf], Is.EqualTo(0));
        }

        [Test]
        public void GnollLevelAdjustmentIsOne()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Gnoll], Is.EqualTo(1));
        }

        [Test]
        public void ForestGnomeLevelAdjustmentZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.ForestGnome], Is.EqualTo(0));
        }

        [Test]
        public void RockGnomeLevelAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.RockGnome], Is.EqualTo(0));
        }

        [Test]
        public void SvirfneblinLevelAdjustmentIsOne()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Svirfneblin], Is.EqualTo(1));
        }

        [Test]
        public void GoblinLevelAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Goblin], Is.EqualTo(0));
        }

        [Test]
        public void HalfCelestialLevelAdjustmentIsOne()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.HalfCelestial], Is.EqualTo(1));
        }

        [Test]
        public void HalfDragonLevelAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.HalfDragon], Is.EqualTo(2));
        }

        [Test]
        public void HalfElfLevelAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HalfElf], Is.EqualTo(0));
        }

        [Test]
        public void HalfFiendLevelAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.HalfFiend], Is.EqualTo(2));
        }

        [Test]
        public void HalfOrcLevelAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HalfOrc], Is.EqualTo(0));
        }

        [Test]
        public void DeepHalflingLevelAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.DeepHalfling], Is.EqualTo(0));
        }

        [Test]
        public void LightfootHalflingLevelAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.LightfootHalfling], Is.EqualTo(0));
        }

        [Test]
        public void TallfellowHalflingLevelAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.TallfellowHalfling], Is.EqualTo(0));
        }

        [Test]
        public void HobgoblinLevelAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Hobgoblin], Is.EqualTo(0));
        }

        [Test]
        public void HumanLevelAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Human], Is.EqualTo(0));
        }

        [Test]
        public void KoboldLevelAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Kobold], Is.EqualTo(0));
        }

        [Test]
        public void LizardfolkLevelAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Lizardfolk], Is.EqualTo(0));
        }

        [Test]
        public void MindFlayerLevelAdjustmentIsEight()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.MindFlayer], Is.EqualTo(8));
        }

        [Test]
        public void MinotaurLevelAdjustmentIsFour()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Minotaur], Is.EqualTo(4));
        }

        [Test]
        public void OgreLevelAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Ogre], Is.EqualTo(2));
        }

        [Test]
        public void OgreMageLevelAdjustmentIsEight()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.OgreMage], Is.EqualTo(8));
        }

        [Test]
        public void OrcLevelAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Orc], Is.EqualTo(0));
        }

        [Test]
        public void TieflingLevelAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Tiefling], Is.EqualTo(0));
        }

        [Test]
        public void TroglodyteLevelAdjustmentIsOne()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Troglodyte], Is.EqualTo(1));
        }

        [Test]
        public void WerebearLevelAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Werebear], Is.EqualTo(2));
        }

        [Test]
        public void WereboarLevelAdjustmentIsOne()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Wereboar], Is.EqualTo(1));
        }

        [Test]
        public void WereratLevelAdjustmentIsOne()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Wererat], Is.EqualTo(1));
        }

        [Test]
        public void WeretigerLevelAdjustmentIsOne()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Weretiger], Is.EqualTo(1));
        }

        [Test]
        public void WerewolfLevelAdjustmentIsOne()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Werewolf], Is.EqualTo(1));
        }

        [Test]
        public void AllBaseRacesAndMetaracesInTable()
        {
            foreach (var baseRace in RaceConstants.BaseRaces.GetBaseRaces())
                Assert.That(adjustments.ContainsKey(baseRace), Is.True);
        }

        [Test]
        public void AllMetaracesInTable()
        {
            foreach (var metarace in RaceConstants.Metaraces.GetMetaraces())
                Assert.That(adjustments.ContainsKey(metarace), Is.True);

            Assert.That(adjustments.ContainsKey(String.Empty), Is.True);
        }
    }
}
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Xml.Parsers;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace NPCGen.Tests.Generation.Xml.Data.Stats
{
    [TestFixture]
    public class CharismaStatAdjustmentsTests
    {
        private Dictionary<String, Int32> charismaAdjustments;

        [SetUp]
        public void Setup()
        {
            var streamLoader = new EmbeddedResourceStreamLoader();
            var parser = new StatAdjustmentXmlParser(streamLoader);
            charismaAdjustments = parser.Parse("CharismaStatAdjustments.xml");

            Assert.Fail("Need to adjust these tests from the wisdom ones they were copied from.");
        }

        [Test]
        public void AasimarCharismaAdjustmentIsTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Aasimar], Is.EqualTo(2));
        }

        [Test]
        public void BugbearCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Bugbear], Is.EqualTo(0));
        }

        [Test]
        public void DerroDwarfCharismaAdjustmentIsSix()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.DerroDwarf], Is.EqualTo(6));
        }

        [Test]
        public void DoppelgangerCharismaAdjustmentIsFour()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Doppelganger], Is.EqualTo(4));
        }

        [Test]
        public void DrowCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Drow], Is.EqualTo(0));
        }

        [Test]
        public void DuergarCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Duergar], Is.EqualTo(0));
        }

        [Test]
        public void DeepDwarfCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.DeepDwarf], Is.EqualTo(0));
        }

        [Test]
        public void HillDwarfCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.HillDwarf], Is.EqualTo(0));
        }

        [Test]
        public void MountainDwarfCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.MountainDwarf], Is.EqualTo(0));
        }

        [Test]
        public void GrayElfCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.GrayElf], Is.EqualTo(0));
        }

        [Test]
        public void HighElfCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.HighElf], Is.EqualTo(0));
        }

        [Test]
        public void WildElfCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.WildElf], Is.EqualTo(0));
        }

        [Test]
        public void WoodElfCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.WoodElf], Is.EqualTo(0));
        }

        [Test]
        public void GnollCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Gnoll], Is.EqualTo(0));
        }

        [Test]
        public void ForestGnomeCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.ForestGnome], Is.EqualTo(0));
        }

        [Test]
        public void RockGnomeCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.RockGnome], Is.EqualTo(0));
        }

        [Test]
        public void SvirfneblinCharismaAdjustmentIsTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Svirfneblin], Is.EqualTo(2));
        }

        [Test]
        public void GoblinCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Goblin], Is.EqualTo(0));
        }

        [Test]
        public void HalfCelestialCharismaAdjustmentIsFour()
        {
            Assert.That(charismaAdjustments[RaceConstants.Metaraces.HalfCelestial], Is.EqualTo(4));
        }

        [Test]
        public void HalfDragonCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.Metaraces.HalfDragon], Is.EqualTo(0));
        }

        [Test]
        public void HalfElfCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.HalfElf], Is.EqualTo(0));
        }

        [Test]
        public void HalfFiendCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.Metaraces.HalfFiend], Is.EqualTo(0));
        }

        [Test]
        public void HalfOrcCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.HalfOrc], Is.EqualTo(0));
        }

        [Test]
        public void DeepHalflingCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.DeepHalfling], Is.EqualTo(0));
        }

        [Test]
        public void LightfootHalflingCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.LightfootHalfling], Is.EqualTo(0));
        }

        [Test]
        public void TallfellowHalflingCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.TallfellowHalfling], Is.EqualTo(0));
        }

        [Test]
        public void HobgoblinCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Hobgoblin], Is.EqualTo(0));
        }

        [Test]
        public void HumanCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Human], Is.EqualTo(0));
        }

        [Test]
        public void KoboldCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Kobold], Is.EqualTo(0));
        }

        [Test]
        public void LizardfolkCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Lizardfolk], Is.EqualTo(0));
        }

        [Test]
        public void MindFlayerCharismaAdjustmentIsSix()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.MindFlayer], Is.EqualTo(6));
        }

        [Test]
        public void MinotaurCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Minotaur], Is.EqualTo(0));
        }

        [Test]
        public void OgreCharismaAdjustmentIsMinusFour()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Ogre], Is.EqualTo(-4));
        }

        [Test]
        public void OgreMageCharismaAdjustmentIsFour()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.OgreMage], Is.EqualTo(4));
        }

        [Test]
        public void OrcCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Orc], Is.EqualTo(-2));
        }

        [Test]
        public void TieflingCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Tiefling], Is.EqualTo(0));
        }

        [Test]
        public void TroglodyteCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Troglodyte], Is.EqualTo(0));
        }

        [Test]
        public void WerebearCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.Metaraces.Werebear], Is.EqualTo(0));
        }

        [Test]
        public void WereboarCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.Metaraces.Wereboar], Is.EqualTo(0));
        }

        [Test]
        public void WereratCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.Metaraces.Wererat], Is.EqualTo(0));
        }

        [Test]
        public void WeretigerCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.Metaraces.Weretiger], Is.EqualTo(0));
        }

        [Test]
        public void WerewolfCharismaAdjustmentIsFour()
        {
            Assert.That(charismaAdjustments[RaceConstants.Metaraces.Werewolf], Is.EqualTo(4));
        }
    }
}
using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Xml.Parsers;
using NUnit.Framework;

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
        }

        [Test]
        public void AasimarCharismaAdjustmentIsTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Aasimar], Is.EqualTo(2));
        }

        [Test]
        public void BugbearCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Bugbear], Is.EqualTo(-2));
        }

        [Test]
        public void DerroDwarfCharismaAdjustmentIsSix()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.DerroDwarf], Is.EqualTo(6));
        }

        [Test]
        public void DoppelgangerCharismaAdjustmentIsTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Doppelganger], Is.EqualTo(2));
        }

        [Test]
        public void DrowCharismaAdjustmentIsTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Drow], Is.EqualTo(2));
        }

        [Test]
        public void DuergarCharismaAdjustmentIsMinusFour()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Duergar], Is.EqualTo(-4));
        }

        [Test]
        public void DeepDwarfCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.DeepDwarf], Is.EqualTo(-2));
        }

        [Test]
        public void HillDwarfCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.HillDwarf], Is.EqualTo(-2));
        }

        [Test]
        public void MountainDwarfCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.MountainDwarf], Is.EqualTo(-2));
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
        public void GnollCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Gnoll], Is.EqualTo(-2));
        }

        [Test]
        public void ForestGnomeCharismaAdjustmentIsTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.ForestGnome], Is.EqualTo(2));
        }

        [Test]
        public void RockGnomeCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.RockGnome], Is.EqualTo(0));
        }

        [Test]
        public void SvirfneblinCharismaAdjustmentIsMinusFour()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Svirfneblin], Is.EqualTo(-4));
        }

        [Test]
        public void GoblinCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Goblin], Is.EqualTo(-2));
        }

        [Test]
        public void HalfCelestialCharismaAdjustmentIsFour()
        {
            Assert.That(charismaAdjustments[RaceConstants.Metaraces.HalfCelestial], Is.EqualTo(4));
        }

        [Test]
        public void HalfDragonCharismaAdjustmentIsTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.Metaraces.HalfDragon], Is.EqualTo(2));
        }

        [Test]
        public void HalfElfCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.HalfElf], Is.EqualTo(0));
        }

        [Test]
        public void HalfFiendCharismaAdjustmentIsTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.Metaraces.HalfFiend], Is.EqualTo(2));
        }

        [Test]
        public void HalfOrcCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.HalfOrc], Is.EqualTo(-2));
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
        public void MinotaurCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Minotaur], Is.EqualTo(-2));
        }

        [Test]
        public void OgreCharismaAdjustmentIsMinusFour()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Ogre], Is.EqualTo(-4));
        }

        [Test]
        public void OgreMageCharismaAdjustmentIsSix()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.OgreMage], Is.EqualTo(6));
        }

        [Test]
        public void OrcCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Orc], Is.EqualTo(-2));
        }

        [Test]
        public void TieflingCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Tiefling], Is.EqualTo(-2));
        }

        [Test]
        public void TroglodyteCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.BaseRaces.Troglodyte], Is.EqualTo(0));
        }

        [Test]
        public void WerebearCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.Metaraces.Werebear], Is.EqualTo(-2));
        }

        [Test]
        public void WereboarCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.Metaraces.Wereboar], Is.EqualTo(-2));
        }

        [Test]
        public void WereratCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.Metaraces.Wererat], Is.EqualTo(-2));
        }

        [Test]
        public void WeretigerCharismaAdjustmentIsZero()
        {
            Assert.That(charismaAdjustments[RaceConstants.Metaraces.Weretiger], Is.EqualTo(0));
        }

        [Test]
        public void WerewolfCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(charismaAdjustments[RaceConstants.Metaraces.Werewolf], Is.EqualTo(-2));
        }
    }
}
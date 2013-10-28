using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Xml.Parsers;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Stats
{
    [TestFixture]
    public class WisdomStatAdjustmentsTests
    {
        private Dictionary<String, Int32> wisdomAdjustments;

        [SetUp]
        public void Setup()
        {
            var streamLoader = new EmbeddedResourceStreamLoader();
            var parser = new StatAdjustmentXmlParser(streamLoader);
            wisdomAdjustments = parser.Parse("WisdomStatAdjustments.xml");
        }

        [Test]
        public void AasimarWisdomAdjustmentIsTwo()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.Aasimar], Is.EqualTo(2));
        }

        [Test]
        public void BugbearWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.Bugbear], Is.EqualTo(0));
        }

        [Test]
        public void DerroDwarfWisdomAdjustmentIsMinusSix()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.DerroDwarf], Is.EqualTo(-6));
        }

        [Test]
        public void DoppelgangerWisdomAdjustmentIsFour()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.Doppelganger], Is.EqualTo(4));
        }

        [Test]
        public void DrowWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.Drow], Is.EqualTo(0));
        }

        [Test]
        public void DuergarWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.Duergar], Is.EqualTo(0));
        }

        [Test]
        public void DeepDwarfWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.DeepDwarf], Is.EqualTo(0));
        }

        [Test]
        public void HillDwarfWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.HillDwarf], Is.EqualTo(0));
        }

        [Test]
        public void MountainDwarfWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.MountainDwarf], Is.EqualTo(0));
        }

        [Test]
        public void GrayElfWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.GrayElf], Is.EqualTo(0));
        }

        [Test]
        public void HighElfWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.HighElf], Is.EqualTo(0));
        }

        [Test]
        public void WildElfWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.WildElf], Is.EqualTo(0));
        }

        [Test]
        public void WoodElfWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.WoodElf], Is.EqualTo(0));
        }

        [Test]
        public void GnollWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.Gnoll], Is.EqualTo(0));
        }

        [Test]
        public void ForestGnomeWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.ForestGnome], Is.EqualTo(0));
        }

        [Test]
        public void RockGnomeWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.RockGnome], Is.EqualTo(0));
        }

        [Test]
        public void SvirfneblinWisdomAdjustmentIsTwo()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.Svirfneblin], Is.EqualTo(2));
        }

        [Test]
        public void GoblinWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.Goblin], Is.EqualTo(0));
        }

        [Test]
        public void HalfCelestialWisdomAdjustmentIsFour()
        {
            Assert.That(wisdomAdjustments[RaceConstants.Metaraces.HalfCelestial], Is.EqualTo(4));
        }

        [Test]
        public void HalfDragonWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.Metaraces.HalfDragon], Is.EqualTo(0));
        }

        [Test]
        public void HalfElfWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.HalfElf], Is.EqualTo(0));
        }

        [Test]
        public void HalfFiendWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.Metaraces.HalfFiend], Is.EqualTo(0));
        }

        [Test]
        public void HalfOrcWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.HalfOrc], Is.EqualTo(0));
        }

        [Test]
        public void DeepHalflingWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.DeepHalfling], Is.EqualTo(0));
        }

        [Test]
        public void LightfootHalflingWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.LightfootHalfling], Is.EqualTo(0));
        }

        [Test]
        public void TallfellowHalflingWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.TallfellowHalfling], Is.EqualTo(0));
        }

        [Test]
        public void HobgoblinWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.Hobgoblin], Is.EqualTo(0));
        }

        [Test]
        public void HumanWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.Human], Is.EqualTo(0));
        }

        [Test]
        public void KoboldWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.Kobold], Is.EqualTo(0));
        }

        [Test]
        public void LizardfolkWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.Lizardfolk], Is.EqualTo(0));
        }

        [Test]
        public void MindFlayerWisdomAdjustmentIsSix()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.MindFlayer], Is.EqualTo(6));
        }

        [Test]
        public void MinotaurWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.Minotaur], Is.EqualTo(0));
        }

        [Test]
        public void OgreWisdomAdjustmentIsMinusFour()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.Ogre], Is.EqualTo(-4));
        }

        [Test]
        public void OgreMageWisdomAdjustmentIsFour()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.OgreMage], Is.EqualTo(4));
        }

        [Test]
        public void OrcWisdomAdjustmentIsMinusTwo()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.Orc], Is.EqualTo(-2));
        }

        [Test]
        public void TieflingWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.Tiefling], Is.EqualTo(0));
        }

        [Test]
        public void TroglodyteWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.BaseRaces.Troglodyte], Is.EqualTo(0));
        }

        [Test]
        public void WerebearWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.Metaraces.Werebear], Is.EqualTo(0));
        }

        [Test]
        public void WereboarWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.Metaraces.Wereboar], Is.EqualTo(0));
        }

        [Test]
        public void WereratWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.Metaraces.Wererat], Is.EqualTo(0));
        }

        [Test]
        public void WeretigerWisdomAdjustmentIsZero()
        {
            Assert.That(wisdomAdjustments[RaceConstants.Metaraces.Weretiger], Is.EqualTo(0));
        }

        [Test]
        public void WerewolfWisdomAdjustmentIsFour()
        {
            Assert.That(wisdomAdjustments[RaceConstants.Metaraces.Werewolf], Is.EqualTo(4));
        }
    }
}
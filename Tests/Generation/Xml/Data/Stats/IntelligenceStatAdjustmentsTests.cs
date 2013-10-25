using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Xml.Parsers;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Stats
{
    [TestFixture]
    public class IntelligenceStatAdjustmentsTests
    {
        private Dictionary<String, Int32> intelligenceAdjustments;

        [SetUp]
        public void Setup()
        {
            var streamLoader = new EmbeddedResourceStreamLoader();
            var parser = new StatAdjustmentXmlParser(streamLoader);
            intelligenceAdjustments = parser.Parse("IntelligenceStatAdjustments.xml");
        }

        [Test]
        public void AasimarIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.Aasimar], Is.EqualTo(0));
        }

        [Test]
        public void BugbearIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.Bugbear], Is.EqualTo(0));
        }

        [Test]
        public void DerroDwarfIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.DerroDwarf], Is.EqualTo(0));
        }

        [Test]
        public void DoppelgangerIntelligenceAdjustmentIsTwo()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.Doppelganger], Is.EqualTo(2));
        }

        [Test]
        public void DrowIntelligenceAdjustmentIsTwo()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.Drow], Is.EqualTo(2));
        }

        [Test]
        public void DuergarIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.Duergar], Is.EqualTo(0));
        }

        [Test]
        public void DeepDwarfIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.DeepDwarf], Is.EqualTo(0));
        }

        [Test]
        public void HillDwarfIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.HillDwarf], Is.EqualTo(0));
        }

        [Test]
        public void MountainDwarfIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.MountainDwarf], Is.EqualTo(0));
        }

        [Test]
        public void GrayElfIntelligenceAdjustmentIsTwo()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.GrayElf], Is.EqualTo(2));
        }

        [Test]
        public void HighElfIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.HighElf], Is.EqualTo(0));
        }

        [Test]
        public void WildElfIntelligenceAdjustmentIsMinusTwo()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.WildElf], Is.EqualTo(-2));
        }

        [Test]
        public void WoodElfIntelligenceAdjustmentIsMinusTwo()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.WoodElf], Is.EqualTo(-2));
        }

        [Test]
        public void GnollIntelligenceAdjustmentIsMinusTwo()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.Gnoll], Is.EqualTo(-2));
        }

        [Test]
        public void ForestGnomeIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.ForestGnome], Is.EqualTo(0));
        }

        [Test]
        public void RockGnomeIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.RockGnome], Is.EqualTo(0));
        }

        [Test]
        public void SvirfneblinIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.Svirfneblin], Is.EqualTo(0));
        }

        [Test]
        public void GoblinIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.Goblin], Is.EqualTo(0));
        }

        [Test]
        public void HalfCelestialIntelligenceAdjustmentIsTwo()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.Metaraces.HalfCelestial], Is.EqualTo(2));
        }

        [Test]
        public void HalfDragonIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.Metaraces.HalfDragon], Is.EqualTo(0));
        }

        [Test]
        public void HalfElfIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.HalfElf], Is.EqualTo(0));
        }

        [Test]
        public void HalfFiendIntelligenceAdjustmentIsFour()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.Metaraces.HalfFiend], Is.EqualTo(4));
        }

        [Test]
        public void HalfOrcIntelligenceAdjustmentIsMinusTwo()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.HalfOrc], Is.EqualTo(-2));
        }

        [Test]
        public void DeepHalflingIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.DeepHalfling], Is.EqualTo(0));
        }

        [Test]
        public void LightfootHalflingIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.LightfootHalfling], Is.EqualTo(0));
        }

        [Test]
        public void TallfellowHalflingIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.TallfellowHalfling], Is.EqualTo(0));
        }

        [Test]
        public void HobgoblinIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.Hobgoblin], Is.EqualTo(0));
        }

        [Test]
        public void HumanIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.Human], Is.EqualTo(0));
        }

        [Test]
        public void KoboldIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.Kobold], Is.EqualTo(0));
        }

        [Test]
        public void LizardfolkIntelligenceAdjustmentIsMinusTwo()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.Lizardfolk], Is.EqualTo(-2));
        }

        [Test]
        public void MindFlayerIntelligenceAdjustmentIsEight()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.MindFlayer], Is.EqualTo(8));
        }

        [Test]
        public void MinotaurIntelligenceAdjustmentIsMinusFour()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.Minotaur], Is.EqualTo(-4));
        }

        [Test]
        public void OgreIntelligenceAdjustmentIsMinusFour()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.Ogre], Is.EqualTo(-4));
        }

        [Test]
        public void OgreMageIntelligenceAdjustmentIsFour()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.OgreMage], Is.EqualTo(4));
        }

        [Test]
        public void OrcIntelligenceAdjustmentIsMinusTwo()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.Orc], Is.EqualTo(-2));
        }

        [Test]
        public void TieflingIntelligenceAdjustmentIsTwo()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.Tiefling], Is.EqualTo(2));
        }

        [Test]
        public void TroglodyteIntelligenceAdjustmentIsMinusTwo()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.BaseRaces.Troglodyte], Is.EqualTo(-2));
        }

        [Test]
        public void WerebearIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.Metaraces.Werebear], Is.EqualTo(0));
        }

        [Test]
        public void WereboarIntelligenceAdjustmentIsZero()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.Metaraces.Wereboar], Is.EqualTo(0));
        }

        [Test]
        public void WereratIntelligenceAdjustmentIsTwo()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.Metaraces.Wererat], Is.EqualTo(2));
        }

        [Test]
        public void WeretigerIntelligenceAdjustmentIsMinusTwo()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.Metaraces.Weretiger], Is.EqualTo(-2));
        }

        [Test]
        public void WerewolfIntelligenceAdjustmentIsMinusTwo()
        {
            Assert.That(intelligenceAdjustments[RaceConstants.Metaraces.Werewolf], Is.EqualTo(-2));
        }
    }
}
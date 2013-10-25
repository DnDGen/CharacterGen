using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Xml.Parsers;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Stats
{
    [TestFixture]
    public class StrengthStatAdjustmentsTests
    {
        private Dictionary<String, Int32> strengthAdjustments;

        [SetUp]
        public void Setup()
        {
            var streamLoader = new EmbeddedResourceStreamLoader();
            var parser = new StatAdjustmentXmlParser(streamLoader);
            strengthAdjustments = parser.Parse("StrengthStatAdjustments.xml");
        }

        [Test]
        public void AasimarStrengthAdjustmentIsZero()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.Aasimar], Is.EqualTo(0));
        }

        [Test]
        public void BugbearStrengthAdjustmentIsFour()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.Bugbear], Is.EqualTo(4));
        }

        [Test]
        public void DerroDwarfStrengthAdjustmentIsZero()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.DerroDwarf], Is.EqualTo(0));
        }

        [Test]
        public void DoppelgangerStrengthAdjustmentIsTwo()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.Doppelganger], Is.EqualTo(2));
        }

        [Test]
        public void DrowStrengthAdjustmentIsZero()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.Drow], Is.EqualTo(0));
        }

        [Test]
        public void DuergarStrengthAdjustmentIsZero()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.Duergar], Is.EqualTo(0));
        }

        [Test]
        public void DeepDwarfStrengthAdjustmentIsZero()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.DeepDwarf], Is.EqualTo(0));
        }

        [Test]
        public void HillDwarfStrengthAdjustmentIsZero()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.HillDwarf], Is.EqualTo(0));
        }

        [Test]
        public void MountainDwarfStrengthAdjustmentIsZero()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.MountainDwarf], Is.EqualTo(0));
        }

        [Test]
        public void GrayElfStrengthAdjustmentIsMinusTwo()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.GrayElf], Is.EqualTo(-2));
        }

        [Test]
        public void HighElfStrengthAdjustmentIsZero()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.HighElf], Is.EqualTo(0));
        }

        [Test]
        public void WildElfStrengthAdjustmentIsZero()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.WildElf], Is.EqualTo(0));
        }

        [Test]
        public void WoodElfStrengthAdjustmentIsTwo()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.WoodElf], Is.EqualTo(2));
        }

        [Test]
        public void GnollStrengthAdjustmentIsFour()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.Gnoll], Is.EqualTo(4));
        }

        [Test]
        public void ForestGnomeStrengthAdjustmentIsMinusTwo()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.ForestGnome], Is.EqualTo(-2));
        }

        [Test]
        public void RockGnomeStrengthAdjustmentIsMinusTwo()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.RockGnome], Is.EqualTo(-2));
        }

        [Test]
        public void SvirfneblinStrengthAdjustmentIsMinusTwo()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.Svirfneblin], Is.EqualTo(-2));
        }

        [Test]
        public void GoblinStrengthAdjustmentIsMinusTwo()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.Goblin], Is.EqualTo(-2));
        }

        [Test]
        public void HalfCelestialStrengthAdjustmentIsFour()
        {
            Assert.That(strengthAdjustments[RaceConstants.Metaraces.HalfCelestial], Is.EqualTo(4));
        }

        [Test]
        public void HalfDragonStrengthAdjustmentIsEight()
        {
            Assert.That(strengthAdjustments[RaceConstants.Metaraces.HalfDragon], Is.EqualTo(8));
        }

        [Test]
        public void HalfElfStrengthAdjustmentIsZero()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.HalfElf], Is.EqualTo(0));
        }

        [Test]
        public void HalfFiendStrengthAdjustmentIsFour()
        {
            Assert.That(strengthAdjustments[RaceConstants.Metaraces.HalfFiend], Is.EqualTo(4));
        }

        [Test]
        public void HalfOrcStrengthAdjustmentIsTwo()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.HalfOrc], Is.EqualTo(2));
        }

        [Test]
        public void DeepHalflingStrengthAdjustmentIsMinusTwo()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.DeepHalfling], Is.EqualTo(-2));
        }

        [Test]
        public void LightfootHalflingStrengthAdjustmentIsMinusTwo()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.LightfootHalfling], Is.EqualTo(-2));
        }

        [Test]
        public void TallfellowHalflingStrengthAdjustmentIsMinusTwo()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.TallfellowHalfling], Is.EqualTo(-2));
        }

        [Test]
        public void HobgoblinStrengthAdjustmentIsZero()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.Hobgoblin], Is.EqualTo(0));
        }

        [Test]
        public void HumanStrengthAdjustmentIsZero()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.Human], Is.EqualTo(0));
        }

        [Test]
        public void KoboldStrengthAdjustmentIsMinusFour()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.Kobold], Is.EqualTo(-4));
        }

        [Test]
        public void LizardfolkStrengthAdjustmentIsTwo()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.Lizardfolk], Is.EqualTo(2));
        }

        [Test]
        public void MindFlayerStrengthAdjustmentIsTwo()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.MindFlayer], Is.EqualTo(2));
        }

        [Test]
        public void MinotaurStrengthAdjustmentIsEight()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.Minotaur], Is.EqualTo(8));
        }

        [Test]
        public void OgreStrengthAdjustmentIsTen()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.Ogre], Is.EqualTo(10));
        }

        [Test]
        public void OgreMageStrengthAdjustmentIsTen()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.OgreMage], Is.EqualTo(10));
        }

        [Test]
        public void OrcStrengthAdjustmentIsFour()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.Orc], Is.EqualTo(4));
        }

        [Test]
        public void TieflingStrengthAdjustmentIsZero()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.Tiefling], Is.EqualTo(0));
        }

        [Test]
        public void TroglodyteStrengthAdjustmentIsZero()
        {
            Assert.That(strengthAdjustments[RaceConstants.BaseRaces.Troglodyte], Is.EqualTo(0));
        }

        [Test]
        public void WerebearStrengthAdjustmentIsTwo()
        {
            Assert.That(strengthAdjustments[RaceConstants.Metaraces.Werebear], Is.EqualTo(2));
        }

        [Test]
        public void WereboarStrengthAdjustmentIsTwo()
        {
            Assert.That(strengthAdjustments[RaceConstants.Metaraces.Wereboar], Is.EqualTo(2));
        }

        [Test]
        public void WereratStrengthAdjustmentIsZero()
        {
            Assert.That(strengthAdjustments[RaceConstants.Metaraces.Wererat], Is.EqualTo(0));
        }

        [Test]
        public void WeretigerStrengthAdjustmentIsTwo()
        {
            Assert.That(strengthAdjustments[RaceConstants.Metaraces.Weretiger], Is.EqualTo(2));
        }

        [Test]
        public void WerewolfStrengthAdjustmentIsTwo()
        {
            Assert.That(strengthAdjustments[RaceConstants.Metaraces.Werewolf], Is.EqualTo(2));
        }
    }
}
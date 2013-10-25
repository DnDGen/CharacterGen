using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Xml.Parsers;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Stats
{
    [TestFixture]
    public class ConstitutionStatAdjustmentsTests
    {
        private Dictionary<String, Int32> constitutionAdjustments;

        [SetUp]
        public void Setup()
        {
            var streamLoader = new EmbeddedResourceStreamLoader();
            var parser = new StatAdjustmentXmlParser(streamLoader);
            constitutionAdjustments = parser.Parse("ConstitutionStatAdjustments.xml");
        }

        [Test]
        public void AasimarConstitutionAdjustmentIsZero()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.Aasimar], Is.EqualTo(0));
        }

        [Test]
        public void BugbearConstitutionAdjustmentIsTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.Bugbear], Is.EqualTo(2));
        }

        [Test]
        public void DerroDwarfConstitutionAdjustmentIsTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.DerroDwarf], Is.EqualTo(2));
        }

        [Test]
        public void DoppelgangerConstitutionAdjustmentIsTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.Doppelganger], Is.EqualTo(2));
        }

        [Test]
        public void DrowConstitutionAdjustmentIsMinusTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.Drow], Is.EqualTo(-2));
        }

        [Test]
        public void DuergarConstitutionAdjustmentIsTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.Duergar], Is.EqualTo(2));
        }

        [Test]
        public void DeepDwarfConstitutionAdjustmentIsTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.DeepDwarf], Is.EqualTo(2));
        }

        [Test]
        public void HillDwarfConstitutionAdjustmentIsTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.HillDwarf], Is.EqualTo(2));
        }

        [Test]
        public void MountainDwarfConstitutionAdjustmentIsTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.MountainDwarf], Is.EqualTo(2));
        }

        [Test]
        public void GrayElfConstitutionAdjustmentIsMinusTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.GrayElf], Is.EqualTo(-2));
        }

        [Test]
        public void HighElfConstitutionAdjustmentIsMinusTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.HighElf], Is.EqualTo(-2));
        }

        [Test]
        public void WildElfConstitutionAdjustmentIsZero()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.WildElf], Is.EqualTo(0));
        }

        [Test]
        public void WoodElfConstitutionAdjustmentIsMinusTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.WoodElf], Is.EqualTo(-2));
        }

        [Test]
        public void GnollConstitutionAdjustmentIsTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.Gnoll], Is.EqualTo(2));
        }

        [Test]
        public void ForestGnomeConstitutionAdjustmentIsTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.ForestGnome], Is.EqualTo(2));
        }

        [Test]
        public void RockGnomeConstitutionAdjustmentIsTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.RockGnome], Is.EqualTo(2));
        }

        [Test]
        public void SvirfneblinConstitutionAdjustmentIsZero()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.Svirfneblin], Is.EqualTo(0));
        }

        [Test]
        public void GoblinConstitutionAdjustmentIsZero()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.Goblin], Is.EqualTo(0));
        }

        [Test]
        public void HalfCelestialConstitutionAdjustmentIsFour()
        {
            Assert.That(constitutionAdjustments[RaceConstants.Metaraces.HalfCelestial], Is.EqualTo(4));
        }

        [Test]
        public void HalfDragonConstitutionAdjustmentIsTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.Metaraces.HalfDragon], Is.EqualTo(2));
        }

        [Test]
        public void HalfElfConstitutionAdjustmentIsZero()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.HalfElf], Is.EqualTo(0));
        }

        [Test]
        public void HalfFiendConstitutionAdjustmentIsTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.Metaraces.HalfFiend], Is.EqualTo(2));
        }

        [Test]
        public void HalfOrcConstitutionAdjustmentIsZero()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.HalfOrc], Is.EqualTo(0));
        }

        [Test]
        public void DeepHalflingConstitutionAdjustmentIsZero()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.DeepHalfling], Is.EqualTo(0));
        }

        [Test]
        public void LightfootHalflingConstitutionAdjustmentIsZero()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.LightfootHalfling], Is.EqualTo(0));
        }

        [Test]
        public void TallfellowHalflingConstitutionAdjustmentIsZero()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.TallfellowHalfling], Is.EqualTo(0));
        }

        [Test]
        public void HobgoblinConstitutionAdjustmentIsTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.Hobgoblin], Is.EqualTo(2));
        }

        [Test]
        public void HumanConstitutionAdjustmentIsZero()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.Human], Is.EqualTo(0));
        }

        [Test]
        public void KoboldConstitutionAdjustmentIsMinusTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.Kobold], Is.EqualTo(-2));
        }

        [Test]
        public void LizardfolkConstitutionAdjustmentIsTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.Lizardfolk], Is.EqualTo(2));
        }

        [Test]
        public void MindFlayerConstitutionAdjustmentIsTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.MindFlayer], Is.EqualTo(2));
        }

        [Test]
        public void MinotaurConstitutionAdjustmentIsFour()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.Minotaur], Is.EqualTo(4));
        }

        [Test]
        public void OgreConstitutionAdjustmentIsFour()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.Ogre], Is.EqualTo(4));
        }

        [Test]
        public void OgreMageConstitutionAdjustmentIsSix()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.OgreMage], Is.EqualTo(6));
        }

        [Test]
        public void OrcConstitutionAdjustmentIsZero()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.Orc], Is.EqualTo(0));
        }

        [Test]
        public void TieflingConstitutionAdjustmentIsZero()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.Tiefling], Is.EqualTo(0));
        }

        [Test]
        public void TroglodyteConstitutionAdjustmentIsFour()
        {
            Assert.That(constitutionAdjustments[RaceConstants.BaseRaces.Troglodyte], Is.EqualTo(4));
        }

        [Test]
        public void WerebearConstitutionAdjustmentIsTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.Metaraces.Werebear], Is.EqualTo(2));
        }

        [Test]
        public void WereboarConstitutionAdjustmentIsTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.Metaraces.Wereboar], Is.EqualTo(2));
        }

        [Test]
        public void WereratConstitutionAdjustmentIsTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.Metaraces.Wererat], Is.EqualTo(2));
        }

        [Test]
        public void WeretigerConstitutionAdjustmentIsTwo()
        {
            Assert.That(constitutionAdjustments[RaceConstants.Metaraces.Weretiger], Is.EqualTo(2));
        }

        [Test]
        public void WerewolfConstitutionAdjustmentIsZero()
        {
            Assert.That(constitutionAdjustments[RaceConstants.Metaraces.Werewolf], Is.EqualTo(0));
        }
    }
}
using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Races;
using NPCGen.Mappers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Stats
{
    [TestFixture]
    public class ConstitutionStatAdjustmentsTests
    {
        [Inject]
        public IAdjustmentXmlParser AdjustmentXmlParser { get; set; }

        private Dictionary<String, Int32> adjustments;

        [SetUp]
        public void Setup()
        {
            adjustments = AdjustmentXmlParser.Parse("ConstitutionStatAdjustments.xml");
        }

        [Test]
        public void AasimarConstitutionAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Aasimar], Is.EqualTo(0));
        }

        [Test]
        public void BugbearConstitutionAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Bugbear], Is.EqualTo(2));
        }

        [Test]
        public void DerroDwarfConstitutionAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Derro], Is.EqualTo(2));
        }

        [Test]
        public void DoppelgangerConstitutionAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Doppelganger], Is.EqualTo(2));
        }

        [Test]
        public void DrowConstitutionAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Drow], Is.EqualTo(-2));
        }

        [Test]
        public void DuergarConstitutionAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.DuergarDwarf], Is.EqualTo(2));
        }

        [Test]
        public void DeepDwarfConstitutionAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.DeepDwarf], Is.EqualTo(2));
        }

        [Test]
        public void HillDwarfConstitutionAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HillDwarf], Is.EqualTo(2));
        }

        [Test]
        public void MountainDwarfConstitutionAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.MountainDwarf], Is.EqualTo(2));
        }

        [Test]
        public void GrayElfConstitutionAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.GrayElf], Is.EqualTo(-2));
        }

        [Test]
        public void HighElfConstitutionAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HighElf], Is.EqualTo(-2));
        }

        [Test]
        public void WildElfConstitutionAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.WildElf], Is.EqualTo(0));
        }

        [Test]
        public void WoodElfConstitutionAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.WoodElf], Is.EqualTo(-2));
        }

        [Test]
        public void GnollConstitutionAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Gnoll], Is.EqualTo(2));
        }

        [Test]
        public void ForestGnomeConstitutionAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.ForestGnome], Is.EqualTo(2));
        }

        [Test]
        public void RockGnomeConstitutionAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.RockGnome], Is.EqualTo(2));
        }

        [Test]
        public void SvirfneblinConstitutionAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Svirfneblin], Is.EqualTo(0));
        }

        [Test]
        public void GoblinConstitutionAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Goblin], Is.EqualTo(0));
        }

        [Test]
        public void HalfCelestialConstitutionAdjustmentIsFour()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.HalfCelestial], Is.EqualTo(4));
        }

        [Test]
        public void HalfDragonConstitutionAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.HalfDragon], Is.EqualTo(2));
        }

        [Test]
        public void HalfElfConstitutionAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HalfElf], Is.EqualTo(0));
        }

        [Test]
        public void HalfFiendConstitutionAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.HalfFiend], Is.EqualTo(2));
        }

        [Test]
        public void HalfOrcConstitutionAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HalfOrc], Is.EqualTo(0));
        }

        [Test]
        public void DeepHalflingConstitutionAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.DeepHalfling], Is.EqualTo(0));
        }

        [Test]
        public void LightfootHalflingConstitutionAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.LightfootHalfling], Is.EqualTo(0));
        }

        [Test]
        public void TallfellowHalflingConstitutionAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.TallfellowHalfling], Is.EqualTo(0));
        }

        [Test]
        public void HobgoblinConstitutionAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Hobgoblin], Is.EqualTo(2));
        }

        [Test]
        public void HumanConstitutionAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Human], Is.EqualTo(0));
        }

        [Test]
        public void KoboldConstitutionAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Kobold], Is.EqualTo(-2));
        }

        [Test]
        public void LizardfolkConstitutionAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Lizardfolk], Is.EqualTo(2));
        }

        [Test]
        public void MindFlayerConstitutionAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.MindFlayer], Is.EqualTo(2));
        }

        [Test]
        public void MinotaurConstitutionAdjustmentIsFour()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Minotaur], Is.EqualTo(4));
        }

        [Test]
        public void OgreConstitutionAdjustmentIsFour()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Ogre], Is.EqualTo(4));
        }

        [Test]
        public void OgreMageConstitutionAdjustmentIsSix()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.OgreMage], Is.EqualTo(6));
        }

        [Test]
        public void OrcConstitutionAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Orc], Is.EqualTo(0));
        }

        [Test]
        public void TieflingConstitutionAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Tiefling], Is.EqualTo(0));
        }

        [Test]
        public void TroglodyteConstitutionAdjustmentIsFour()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Troglodyte], Is.EqualTo(4));
        }

        [Test]
        public void WerebearConstitutionAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Werebear], Is.EqualTo(2));
        }

        [Test]
        public void WereboarConstitutionAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Wereboar], Is.EqualTo(2));
        }

        [Test]
        public void WereratConstitutionAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Wererat], Is.EqualTo(2));
        }

        [Test]
        public void WeretigerConstitutionAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Weretiger], Is.EqualTo(2));
        }

        [Test]
        public void WerewolfConstitutionAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Werewolf], Is.EqualTo(0));
        }

        [Test]
        public void AllBaseRacesInTable()
        {
            foreach (var baseRace in RaceConstants.BaseRaces.GetBaseRaces())
                Assert.That(adjustments.ContainsKey(baseRace), Is.True, baseRace);
        }

        [Test]
        public void AllMetaracesInTable()
        {
            foreach (var metarace in RaceConstants.Metaraces.GetMetaraces())
                Assert.That(adjustments.ContainsKey(metarace), Is.True, metarace);

            Assert.That(adjustments.ContainsKey(String.Empty), Is.True, String.Empty);
        }
    }
}
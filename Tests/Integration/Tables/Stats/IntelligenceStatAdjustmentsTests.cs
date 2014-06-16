using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Races;
using NPCGen.Mappers.Interfaces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Stats
{
    [TestFixture]
    public class IntelligenceStatAdjustmentsTests : IntegrationTests
    {
        [Inject]
        public IAdjustmentMapper AdjustmentXmlMapper { get; set; }

        private Dictionary<String, Int32> adjustments;

        [SetUp]
        public void Setup()
        {
            adjustments = AdjustmentXmlMapper.Parse("IntelligenceStatAdjustments.xml");
        }

        [Test]
        public void AasimarIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Aasimar], Is.EqualTo(0));
        }

        [Test]
        public void BugbearIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Bugbear], Is.EqualTo(0));
        }

        [Test]
        public void DerroDwarfIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Derro], Is.EqualTo(0));
        }

        [Test]
        public void DoppelgangerIntelligenceAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Doppelganger], Is.EqualTo(2));
        }

        [Test]
        public void DrowIntelligenceAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Drow], Is.EqualTo(2));
        }

        [Test]
        public void DuergarIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.DuergarDwarf], Is.EqualTo(0));
        }

        [Test]
        public void DeepDwarfIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.DeepDwarf], Is.EqualTo(0));
        }

        [Test]
        public void HillDwarfIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HillDwarf], Is.EqualTo(0));
        }

        [Test]
        public void MountainDwarfIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.MountainDwarf], Is.EqualTo(0));
        }

        [Test]
        public void GrayElfIntelligenceAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.GrayElf], Is.EqualTo(2));
        }

        [Test]
        public void HighElfIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HighElf], Is.EqualTo(0));
        }

        [Test]
        public void WildElfIntelligenceAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.WildElf], Is.EqualTo(-2));
        }

        [Test]
        public void WoodElfIntelligenceAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.WoodElf], Is.EqualTo(-2));
        }

        [Test]
        public void GnollIntelligenceAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Gnoll], Is.EqualTo(-2));
        }

        [Test]
        public void ForestGnomeIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.ForestGnome], Is.EqualTo(0));
        }

        [Test]
        public void RockGnomeIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.RockGnome], Is.EqualTo(0));
        }

        [Test]
        public void SvirfneblinIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Svirfneblin], Is.EqualTo(0));
        }

        [Test]
        public void GoblinIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Goblin], Is.EqualTo(0));
        }

        [Test]
        public void HalfCelestialIntelligenceAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.HalfCelestial], Is.EqualTo(2));
        }

        [Test]
        public void HalfDragonIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.HalfDragon], Is.EqualTo(0));
        }

        [Test]
        public void HalfElfIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HalfElf], Is.EqualTo(0));
        }

        [Test]
        public void HalfFiendIntelligenceAdjustmentIsFour()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.HalfFiend], Is.EqualTo(4));
        }

        [Test]
        public void HalfOrcIntelligenceAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HalfOrc], Is.EqualTo(-2));
        }

        [Test]
        public void DeepHalflingIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.DeepHalfling], Is.EqualTo(0));
        }

        [Test]
        public void LightfootHalflingIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.LightfootHalfling], Is.EqualTo(0));
        }

        [Test]
        public void TallfellowHalflingIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.TallfellowHalfling], Is.EqualTo(0));
        }

        [Test]
        public void HobgoblinIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Hobgoblin], Is.EqualTo(0));
        }

        [Test]
        public void HumanIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Human], Is.EqualTo(0));
        }

        [Test]
        public void KoboldIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Kobold], Is.EqualTo(0));
        }

        [Test]
        public void LizardfolkIntelligenceAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Lizardfolk], Is.EqualTo(-2));
        }

        [Test]
        public void MindFlayerIntelligenceAdjustmentIsEight()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.MindFlayer], Is.EqualTo(8));
        }

        [Test]
        public void MinotaurIntelligenceAdjustmentIsMinusFour()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Minotaur], Is.EqualTo(-4));
        }

        [Test]
        public void OgreIntelligenceAdjustmentIsMinusFour()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Ogre], Is.EqualTo(-4));
        }

        [Test]
        public void OgreMageIntelligenceAdjustmentIsFour()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.OgreMage], Is.EqualTo(4));
        }

        [Test]
        public void OrcIntelligenceAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Orc], Is.EqualTo(-2));
        }

        [Test]
        public void TieflingIntelligenceAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Tiefling], Is.EqualTo(2));
        }

        [Test]
        public void TroglodyteIntelligenceAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Troglodyte], Is.EqualTo(-2));
        }

        [Test]
        public void WerebearIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Werebear], Is.EqualTo(0));
        }

        [Test]
        public void WereboarIntelligenceAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Wereboar], Is.EqualTo(0));
        }

        [Test]
        public void WereratIntelligenceAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Wererat], Is.EqualTo(2));
        }

        [Test]
        public void WeretigerIntelligenceAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Weretiger], Is.EqualTo(-2));
        }

        [Test]
        public void WerewolfIntelligenceAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Werewolf], Is.EqualTo(-2));
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
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
    public class StrengthStatAdjustmentsTests : IntegrationTests
    {
        [Inject]
        public IAdjustmentMapper AdjustmentXmlParser { get; set; }

        private Dictionary<String, Int32> adjustments;

        [SetUp]
        public void Setup()
        {
            adjustments = AdjustmentXmlParser.Parse("StrengthStatAdjustments.xml");
        }

        [Test]
        public void AasimarStrengthAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Aasimar], Is.EqualTo(0));
        }

        [Test]
        public void BugbearStrengthAdjustmentIsFour()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Bugbear], Is.EqualTo(4));
        }

        [Test]
        public void DerroDwarfStrengthAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Derro], Is.EqualTo(0));
        }

        [Test]
        public void DoppelgangerStrengthAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Doppelganger], Is.EqualTo(2));
        }

        [Test]
        public void DrowStrengthAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Drow], Is.EqualTo(0));
        }

        [Test]
        public void DuergarStrengthAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.DuergarDwarf], Is.EqualTo(0));
        }

        [Test]
        public void DeepDwarfStrengthAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.DeepDwarf], Is.EqualTo(0));
        }

        [Test]
        public void HillDwarfStrengthAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HillDwarf], Is.EqualTo(0));
        }

        [Test]
        public void MountainDwarfStrengthAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.MountainDwarf], Is.EqualTo(0));
        }

        [Test]
        public void GrayElfStrengthAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.GrayElf], Is.EqualTo(-2));
        }

        [Test]
        public void HighElfStrengthAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HighElf], Is.EqualTo(0));
        }

        [Test]
        public void WildElfStrengthAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.WildElf], Is.EqualTo(0));
        }

        [Test]
        public void WoodElfStrengthAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.WoodElf], Is.EqualTo(2));
        }

        [Test]
        public void GnollStrengthAdjustmentIsFour()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Gnoll], Is.EqualTo(4));
        }

        [Test]
        public void ForestGnomeStrengthAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.ForestGnome], Is.EqualTo(-2));
        }

        [Test]
        public void RockGnomeStrengthAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.RockGnome], Is.EqualTo(-2));
        }

        [Test]
        public void SvirfneblinStrengthAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Svirfneblin], Is.EqualTo(-2));
        }

        [Test]
        public void GoblinStrengthAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Goblin], Is.EqualTo(-2));
        }

        [Test]
        public void HalfCelestialStrengthAdjustmentIsFour()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.HalfCelestial], Is.EqualTo(4));
        }

        [Test]
        public void HalfDragonStrengthAdjustmentIsEight()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.HalfDragon], Is.EqualTo(8));
        }

        [Test]
        public void HalfElfStrengthAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HalfElf], Is.EqualTo(0));
        }

        [Test]
        public void HalfFiendStrengthAdjustmentIsFour()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.HalfFiend], Is.EqualTo(4));
        }

        [Test]
        public void HalfOrcStrengthAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HalfOrc], Is.EqualTo(2));
        }

        [Test]
        public void DeepHalflingStrengthAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.DeepHalfling], Is.EqualTo(-2));
        }

        [Test]
        public void LightfootHalflingStrengthAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.LightfootHalfling], Is.EqualTo(-2));
        }

        [Test]
        public void TallfellowHalflingStrengthAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.TallfellowHalfling], Is.EqualTo(-2));
        }

        [Test]
        public void HobgoblinStrengthAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Hobgoblin], Is.EqualTo(0));
        }

        [Test]
        public void HumanStrengthAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Human], Is.EqualTo(0));
        }

        [Test]
        public void KoboldStrengthAdjustmentIsMinusFour()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Kobold], Is.EqualTo(-4));
        }

        [Test]
        public void LizardfolkStrengthAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Lizardfolk], Is.EqualTo(2));
        }

        [Test]
        public void MindFlayerStrengthAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.MindFlayer], Is.EqualTo(2));
        }

        [Test]
        public void MinotaurStrengthAdjustmentIsEight()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Minotaur], Is.EqualTo(8));
        }

        [Test]
        public void OgreStrengthAdjustmentIsTen()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Ogre], Is.EqualTo(10));
        }

        [Test]
        public void OgreMageStrengthAdjustmentIsTen()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.OgreMage], Is.EqualTo(10));
        }

        [Test]
        public void OrcStrengthAdjustmentIsFour()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Orc], Is.EqualTo(4));
        }

        [Test]
        public void TieflingStrengthAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Tiefling], Is.EqualTo(0));
        }

        [Test]
        public void TroglodyteStrengthAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Troglodyte], Is.EqualTo(0));
        }

        [Test]
        public void WerebearStrengthAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Werebear], Is.EqualTo(2));
        }

        [Test]
        public void WereboarStrengthAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Wereboar], Is.EqualTo(2));
        }

        [Test]
        public void WereratStrengthAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Wererat], Is.EqualTo(0));
        }

        [Test]
        public void WeretigerStrengthAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Weretiger], Is.EqualTo(2));
        }

        [Test]
        public void WerewolfStrengthAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Werewolf], Is.EqualTo(2));
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
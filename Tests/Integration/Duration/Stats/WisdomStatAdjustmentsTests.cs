using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Races;
using NPCGen.Mappers.Interfaces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Stats
{
    [TestFixture]
    public class WisdomStatAdjustmentsTests : IntegrationTest
    {
        [Inject]
        public IAdjustmentXmlParser AdjustmentXmlParser { get; set; }

        private Dictionary<String, Int32> adjustments;

        [SetUp]
        public void Setup()
        {
            adjustments = AdjustmentXmlParser.Parse("WisdomStatAdjustments.xml");
        }

        [Test]
        public void AasimarWisdomAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Aasimar], Is.EqualTo(2));
        }

        [Test]
        public void BugbearWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Bugbear], Is.EqualTo(0));
        }

        [Test]
        public void DerroDwarfWisdomAdjustmentIsMinusSix()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Derro], Is.EqualTo(-6));
        }

        [Test]
        public void DoppelgangerWisdomAdjustmentIsFour()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Doppelganger], Is.EqualTo(4));
        }

        [Test]
        public void DrowWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Drow], Is.EqualTo(0));
        }

        [Test]
        public void DuergarWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.DuergarDwarf], Is.EqualTo(0));
        }

        [Test]
        public void DeepDwarfWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.DeepDwarf], Is.EqualTo(0));
        }

        [Test]
        public void HillDwarfWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HillDwarf], Is.EqualTo(0));
        }

        [Test]
        public void MountainDwarfWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.MountainDwarf], Is.EqualTo(0));
        }

        [Test]
        public void GrayElfWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.GrayElf], Is.EqualTo(0));
        }

        [Test]
        public void HighElfWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HighElf], Is.EqualTo(0));
        }

        [Test]
        public void WildElfWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.WildElf], Is.EqualTo(0));
        }

        [Test]
        public void WoodElfWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.WoodElf], Is.EqualTo(0));
        }

        [Test]
        public void GnollWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Gnoll], Is.EqualTo(0));
        }

        [Test]
        public void ForestGnomeWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.ForestGnome], Is.EqualTo(0));
        }

        [Test]
        public void RockGnomeWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.RockGnome], Is.EqualTo(0));
        }

        [Test]
        public void SvirfneblinWisdomAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Svirfneblin], Is.EqualTo(2));
        }

        [Test]
        public void GoblinWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Goblin], Is.EqualTo(0));
        }

        [Test]
        public void HalfCelestialWisdomAdjustmentIsFour()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.HalfCelestial], Is.EqualTo(4));
        }

        [Test]
        public void HalfDragonWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.HalfDragon], Is.EqualTo(0));
        }

        [Test]
        public void HalfElfWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HalfElf], Is.EqualTo(0));
        }

        [Test]
        public void HalfFiendWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.HalfFiend], Is.EqualTo(0));
        }

        [Test]
        public void HalfOrcWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HalfOrc], Is.EqualTo(0));
        }

        [Test]
        public void DeepHalflingWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.DeepHalfling], Is.EqualTo(0));
        }

        [Test]
        public void LightfootHalflingWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.LightfootHalfling], Is.EqualTo(0));
        }

        [Test]
        public void TallfellowHalflingWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.TallfellowHalfling], Is.EqualTo(0));
        }

        [Test]
        public void HobgoblinWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Hobgoblin], Is.EqualTo(0));
        }

        [Test]
        public void HumanWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Human], Is.EqualTo(0));
        }

        [Test]
        public void KoboldWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Kobold], Is.EqualTo(0));
        }

        [Test]
        public void LizardfolkWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Lizardfolk], Is.EqualTo(0));
        }

        [Test]
        public void MindFlayerWisdomAdjustmentIsSix()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.MindFlayer], Is.EqualTo(6));
        }

        [Test]
        public void MinotaurWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Minotaur], Is.EqualTo(0));
        }

        [Test]
        public void OgreWisdomAdjustmentIsMinusFour()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Ogre], Is.EqualTo(-4));
        }

        [Test]
        public void OgreMageWisdomAdjustmentIsFour()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.OgreMage], Is.EqualTo(4));
        }

        [Test]
        public void OrcWisdomAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Orc], Is.EqualTo(-2));
        }

        [Test]
        public void TieflingWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Tiefling], Is.EqualTo(0));
        }

        [Test]
        public void TroglodyteWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Troglodyte], Is.EqualTo(0));
        }

        [Test]
        public void WerebearWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Werebear], Is.EqualTo(0));
        }

        [Test]
        public void WereboarWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Wereboar], Is.EqualTo(0));
        }

        [Test]
        public void WereratWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Wererat], Is.EqualTo(0));
        }

        [Test]
        public void WeretigerWisdomAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Weretiger], Is.EqualTo(0));
        }

        [Test]
        public void WerewolfWisdomAdjustmentIsFour()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Werewolf], Is.EqualTo(4));
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
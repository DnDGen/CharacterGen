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
        private Dictionary<String, Int32> adjustments;

        [SetUp]
        public void Setup()
        {
            var streamLoader = new EmbeddedResourceStreamLoader();
            var parser = new AdjustmentXmlParser(streamLoader);
            adjustments = parser.Parse("CharismaStatAdjustments.xml");
        }

        [Test]
        public void AasimarCharismaAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Aasimar], Is.EqualTo(2));
        }

        [Test]
        public void BugbearCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Bugbear], Is.EqualTo(-2));
        }

        [Test]
        public void DerroDwarfCharismaAdjustmentIsSix()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.DerroDwarf], Is.EqualTo(6));
        }

        [Test]
        public void DoppelgangerCharismaAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Doppelganger], Is.EqualTo(2));
        }

        [Test]
        public void DrowCharismaAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Drow], Is.EqualTo(2));
        }

        [Test]
        public void DuergarCharismaAdjustmentIsMinusFour()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Duergar], Is.EqualTo(-4));
        }

        [Test]
        public void DeepDwarfCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.DeepDwarf], Is.EqualTo(-2));
        }

        [Test]
        public void HillDwarfCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HillDwarf], Is.EqualTo(-2));
        }

        [Test]
        public void MountainDwarfCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.MountainDwarf], Is.EqualTo(-2));
        }

        [Test]
        public void GrayElfCharismaAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.GrayElf], Is.EqualTo(0));
        }

        [Test]
        public void HighElfCharismaAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HighElf], Is.EqualTo(0));
        }

        [Test]
        public void WildElfCharismaAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.WildElf], Is.EqualTo(0));
        }

        [Test]
        public void WoodElfCharismaAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.WoodElf], Is.EqualTo(0));
        }

        [Test]
        public void GnollCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Gnoll], Is.EqualTo(-2));
        }

        [Test]
        public void ForestGnomeCharismaAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.ForestGnome], Is.EqualTo(2));
        }

        [Test]
        public void RockGnomeCharismaAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.RockGnome], Is.EqualTo(0));
        }

        [Test]
        public void SvirfneblinCharismaAdjustmentIsMinusFour()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Svirfneblin], Is.EqualTo(-4));
        }

        [Test]
        public void GoblinCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Goblin], Is.EqualTo(-2));
        }

        [Test]
        public void HalfCelestialCharismaAdjustmentIsFour()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.HalfCelestial], Is.EqualTo(4));
        }

        [Test]
        public void HalfDragonCharismaAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.HalfDragon], Is.EqualTo(2));
        }

        [Test]
        public void HalfElfCharismaAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HalfElf], Is.EqualTo(0));
        }

        [Test]
        public void HalfFiendCharismaAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.HalfFiend], Is.EqualTo(2));
        }

        [Test]
        public void HalfOrcCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HalfOrc], Is.EqualTo(-2));
        }

        [Test]
        public void DeepHalflingCharismaAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.DeepHalfling], Is.EqualTo(0));
        }

        [Test]
        public void LightfootHalflingCharismaAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.LightfootHalfling], Is.EqualTo(0));
        }

        [Test]
        public void TallfellowHalflingCharismaAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.TallfellowHalfling], Is.EqualTo(0));
        }

        [Test]
        public void HobgoblinCharismaAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Hobgoblin], Is.EqualTo(0));
        }

        [Test]
        public void HumanCharismaAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Human], Is.EqualTo(0));
        }

        [Test]
        public void KoboldCharismaAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Kobold], Is.EqualTo(0));
        }

        [Test]
        public void LizardfolkCharismaAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Lizardfolk], Is.EqualTo(0));
        }

        [Test]
        public void MindFlayerCharismaAdjustmentIsSix()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.MindFlayer], Is.EqualTo(6));
        }

        [Test]
        public void MinotaurCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Minotaur], Is.EqualTo(-2));
        }

        [Test]
        public void OgreCharismaAdjustmentIsMinusFour()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Ogre], Is.EqualTo(-4));
        }

        [Test]
        public void OgreMageCharismaAdjustmentIsSix()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.OgreMage], Is.EqualTo(6));
        }

        [Test]
        public void OrcCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Orc], Is.EqualTo(-2));
        }

        [Test]
        public void TieflingCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Tiefling], Is.EqualTo(-2));
        }

        [Test]
        public void TroglodyteCharismaAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Troglodyte], Is.EqualTo(0));
        }

        [Test]
        public void WerebearCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Werebear], Is.EqualTo(-2));
        }

        [Test]
        public void WereboarCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Wereboar], Is.EqualTo(-2));
        }

        [Test]
        public void WereratCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Wererat], Is.EqualTo(-2));
        }

        [Test]
        public void WeretigerCharismaAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Weretiger], Is.EqualTo(0));
        }

        [Test]
        public void WerewolfCharismaAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Werewolf], Is.EqualTo(-2));
        }

        [Test]
        public void AllBaseRacesInTable()
        {
            foreach (var baseRace in RaceConstants.BaseRaces.GetBaseRaces())
                Assert.That(adjustments.ContainsKey(baseRace), Is.True);
        }

        [Test]
        public void AllMetaracesInTable()
        {
            foreach (var metarace in RaceConstants.Metaraces.GetMetaraces())
                Assert.That(adjustments.ContainsKey(metarace), Is.True);

            Assert.That(adjustments.ContainsKey(String.Empty), Is.True);
        }
    }
}
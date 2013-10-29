using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Xml.Parsers;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Stats
{
    [TestFixture]
    public class DexterityStatAdjustmentsTests
    {
        private Dictionary<String, Int32> adjustments;

        [SetUp]
        public void Setup()
        {
            var streamLoader = new EmbeddedResourceStreamLoader();
            var parser = new AdjustmentXmlParser(streamLoader);
            adjustments = parser.Parse("DexterityStatAdjustments.xml");
        }

        [Test]
        public void AasimarDexterityAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Aasimar], Is.EqualTo(0));
        }

        [Test]
        public void BugbearDexterityAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Bugbear], Is.EqualTo(2));
        }

        [Test]
        public void DerroDwarfDexterityAdjustmentIsFour()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Derro], Is.EqualTo(4));
        }

        [Test]
        public void DoppelgangerDexterityAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Doppelganger], Is.EqualTo(2));
        }

        [Test]
        public void DrowDexterityAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Drow], Is.EqualTo(2));
        }

        [Test]
        public void DuergarDexterityAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.DuergarDwarf], Is.EqualTo(0));
        }

        [Test]
        public void DeepDwarfDexterityAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.DeepDwarf], Is.EqualTo(0));
        }

        [Test]
        public void HillDwarfDexterityAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HillDwarf], Is.EqualTo(0));
        }

        [Test]
        public void MountainDwarfDexterityAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.MountainDwarf], Is.EqualTo(0));
        }

        [Test]
        public void GrayElfDexterityAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.GrayElf], Is.EqualTo(2));
        }

        [Test]
        public void HighElfDexterityAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HighElf], Is.EqualTo(2));
        }

        [Test]
        public void WildElfDexterityAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.WildElf], Is.EqualTo(2));
        }

        [Test]
        public void WoodElfDexterityAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.WoodElf], Is.EqualTo(2));
        }

        [Test]
        public void GnollDexterityAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Gnoll], Is.EqualTo(0));
        }

        [Test]
        public void ForestGnomeDexterityAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.ForestGnome], Is.EqualTo(0));
        }

        [Test]
        public void RockGnomeDexterityAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.RockGnome], Is.EqualTo(0));
        }

        [Test]
        public void SvirfneblinDexterityAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Svirfneblin], Is.EqualTo(2));
        }

        [Test]
        public void GoblinDexterityAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Goblin], Is.EqualTo(2));
        }

        [Test]
        public void HalfCelestialDexterityAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.HalfCelestial], Is.EqualTo(2));
        }

        [Test]
        public void HalfDragonDexterityAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.HalfDragon], Is.EqualTo(0));
        }

        [Test]
        public void HalfElfDexterityAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HalfElf], Is.EqualTo(0));
        }

        [Test]
        public void HalfFiendDexterityAdjustmentIsFour()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.HalfFiend], Is.EqualTo(4));
        }

        [Test]
        public void HalfOrcDexterityAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.HalfOrc], Is.EqualTo(0));
        }

        [Test]
        public void DeepHalflingDexterityAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.DeepHalfling], Is.EqualTo(2));
        }

        [Test]
        public void LightfootHalflingDexterityAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.LightfootHalfling], Is.EqualTo(2));
        }

        [Test]
        public void TallfellowHalflingDexterityAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.TallfellowHalfling], Is.EqualTo(2));
        }

        [Test]
        public void HobgoblinDexterityAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Hobgoblin], Is.EqualTo(2));
        }

        [Test]
        public void HumanDexterityAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Human], Is.EqualTo(0));
        }

        [Test]
        public void KoboldDexterityAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Kobold], Is.EqualTo(2));
        }

        [Test]
        public void LizardfolkDexterityAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Lizardfolk], Is.EqualTo(0));
        }

        [Test]
        public void MindFlayerDexterityAdjustmentIsFour()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.MindFlayer], Is.EqualTo(4));
        }

        [Test]
        public void MinotaurDexterityAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Minotaur], Is.EqualTo(0));
        }

        [Test]
        public void OgreDexterityAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Ogre], Is.EqualTo(-2));
        }

        [Test]
        public void OgreMageDexterityAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.OgreMage], Is.EqualTo(0));
        }

        [Test]
        public void OrcDexterityAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Orc], Is.EqualTo(0));
        }

        [Test]
        public void TieflingDexterityAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Tiefling], Is.EqualTo(2));
        }

        [Test]
        public void TroglodyteDexterityAdjustmentIsMinusTwo()
        {
            Assert.That(adjustments[RaceConstants.BaseRaces.Troglodyte], Is.EqualTo(-2));
        }

        [Test]
        public void WerebearDexterityAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Werebear], Is.EqualTo(0));
        }

        [Test]
        public void WereboarDexterityAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Wereboar], Is.EqualTo(0));
        }

        [Test]
        public void WereratDexterityAdjustmentIsTwo()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Wererat], Is.EqualTo(2));
        }

        [Test]
        public void WeretigerDexterityAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Weretiger], Is.EqualTo(0));
        }

        [Test]
        public void WerewolfDexterityAdjustmentIsZero()
        {
            Assert.That(adjustments[RaceConstants.Metaraces.Werewolf], Is.EqualTo(0));
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
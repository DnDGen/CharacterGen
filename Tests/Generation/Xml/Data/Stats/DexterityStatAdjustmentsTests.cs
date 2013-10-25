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
        private Dictionary<String, Int32> dexterityAdjustments;

        [SetUp]
        public void Setup()
        {
            var streamLoader = new EmbeddedResourceStreamLoader();
            var parser = new StatAdjustmentXmlParser(streamLoader);
            dexterityAdjustments = parser.Parse("DexterityStatAdjustments.xml");
        }

        [Test]
        public void AasimarDexterityAdjustmentIsZero()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.Aasimar], Is.EqualTo(0));
        }

        [Test]
        public void BugbearDexterityAdjustmentIsTwo()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.Bugbear], Is.EqualTo(2));
        }

        [Test]
        public void DerroDwarfDexterityAdjustmentIsFour()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.DerroDwarf], Is.EqualTo(4));
        }

        [Test]
        public void DoppelgangerDexterityAdjustmentIsTwo()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.Doppelganger], Is.EqualTo(2));
        }

        [Test]
        public void DrowDexterityAdjustmentIsTwo()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.Drow], Is.EqualTo(2));
        }

        [Test]
        public void DuergarDexterityAdjustmentIsZero()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.Duergar], Is.EqualTo(0));
        }

        [Test]
        public void DeepDwarfDexterityAdjustmentIsZero()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.DeepDwarf], Is.EqualTo(0));
        }

        [Test]
        public void HillDwarfDexterityAdjustmentIsZero()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.HillDwarf], Is.EqualTo(0));
        }

        [Test]
        public void MountainDwarfDexterityAdjustmentIsZero()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.MountainDwarf], Is.EqualTo(0));
        }

        [Test]
        public void GrayElfDexterityAdjustmentIsTwo()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.GrayElf], Is.EqualTo(2));
        }

        [Test]
        public void HighElfDexterityAdjustmentIsTwo()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.HighElf], Is.EqualTo(2));
        }

        [Test]
        public void WildElfDexterityAdjustmentIsTwo()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.WildElf], Is.EqualTo(2));
        }

        [Test]
        public void WoodElfDexterityAdjustmentIsTwo()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.WoodElf], Is.EqualTo(2));
        }

        [Test]
        public void GnollDexterityAdjustmentIsZero()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.Gnoll], Is.EqualTo(0));
        }

        [Test]
        public void ForestGnomeDexterityAdjustmentIsZero()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.ForestGnome], Is.EqualTo(0));
        }

        [Test]
        public void RockGnomeDexterityAdjustmentIsZero()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.RockGnome], Is.EqualTo(0));
        }

        [Test]
        public void SvirfneblinDexterityAdjustmentIsTwo()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.Svirfneblin], Is.EqualTo(2));
        }

        [Test]
        public void GoblinDexterityAdjustmentIsTwo()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.Goblin], Is.EqualTo(2));
        }

        [Test]
        public void HalfCelestialDexterityAdjustmentIsTwo()
        {
            Assert.That(dexterityAdjustments[RaceConstants.Metaraces.HalfCelestial], Is.EqualTo(2));
        }

        [Test]
        public void HalfDragonDexterityAdjustmentIsZero()
        {
            Assert.That(dexterityAdjustments[RaceConstants.Metaraces.HalfDragon], Is.EqualTo(0));
        }

        [Test]
        public void HalfElfDexterityAdjustmentIsZero()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.HalfElf], Is.EqualTo(0));
        }

        [Test]
        public void HalfFiendDexterityAdjustmentIsFour()
        {
            Assert.That(dexterityAdjustments[RaceConstants.Metaraces.HalfFiend], Is.EqualTo(4));
        }

        [Test]
        public void HalfOrcDexterityAdjustmentIsZero()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.HalfOrc], Is.EqualTo(0));
        }

        [Test]
        public void DeepHalflingDexterityAdjustmentIsTwo()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.DeepHalfling], Is.EqualTo(2));
        }

        [Test]
        public void LightfootHalflingDexterityAdjustmentIsTwo()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.LightfootHalfling], Is.EqualTo(2));
        }

        [Test]
        public void TallfellowHalflingDexterityAdjustmentIsTwo()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.TallfellowHalfling], Is.EqualTo(2));
        }

        [Test]
        public void HobgoblinDexterityAdjustmentIsTwo()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.Hobgoblin], Is.EqualTo(2));
        }

        [Test]
        public void HumanDexterityAdjustmentIsZero()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.Human], Is.EqualTo(0));
        }

        [Test]
        public void KoboldDexterityAdjustmentIsTwo()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.Kobold], Is.EqualTo(2));
        }

        [Test]
        public void LizardfolkDexterityAdjustmentIsZero()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.Lizardfolk], Is.EqualTo(0));
        }

        [Test]
        public void MindFlayerDexterityAdjustmentIsFour()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.MindFlayer], Is.EqualTo(4));
        }

        [Test]
        public void MinotaurDexterityAdjustmentIsZero()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.Minotaur], Is.EqualTo(0));
        }

        [Test]
        public void OgreDexterityAdjustmentIsMinusTwo()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.Ogre], Is.EqualTo(-2));
        }

        [Test]
        public void OgreMageDexterityAdjustmentIsZero()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.OgreMage], Is.EqualTo(0));
        }

        [Test]
        public void OrcDexterityAdjustmentIsZero()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.Orc], Is.EqualTo(0));
        }

        [Test]
        public void TieflingDexterityAdjustmentIsTwo()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.Tiefling], Is.EqualTo(2));
        }

        [Test]
        public void TroglodyteDexterityAdjustmentIsMinusTwo()
        {
            Assert.That(dexterityAdjustments[RaceConstants.BaseRaces.Troglodyte], Is.EqualTo(-2));
        }

        [Test]
        public void WerebearDexterityAdjustmentIsZero()
        {
            Assert.That(dexterityAdjustments[RaceConstants.Metaraces.Werebear], Is.EqualTo(0));
        }

        [Test]
        public void WereboarDexterityAdjustmentIsZero()
        {
            Assert.That(dexterityAdjustments[RaceConstants.Metaraces.Wereboar], Is.EqualTo(0));
        }

        [Test]
        public void WereratDexterityAdjustmentIsTwo()
        {
            Assert.That(dexterityAdjustments[RaceConstants.Metaraces.Wererat], Is.EqualTo(2));
        }

        [Test]
        public void WeretigerDexterityAdjustmentIsZero()
        {
            Assert.That(dexterityAdjustments[RaceConstants.Metaraces.Weretiger], Is.EqualTo(0));
        }

        [Test]
        public void WerewolfDexterityAdjustmentIsZero()
        {
            Assert.That(dexterityAdjustments[RaceConstants.Metaraces.Werewolf], Is.EqualTo(0));
        }
    }
}
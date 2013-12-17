using System.Linq;
using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Data.Races
{
    [TestFixture]
    public class RaceConstantsTests
    {
        [Test]
        public void AasimarConstant()
        {
            Assert.That(RaceConstants.BaseRaces.Aasimar, Is.EqualTo("Aasimar"));
        }

        [Test]
        public void BugbearConstant()
        {
            Assert.That(RaceConstants.BaseRaces.Bugbear, Is.EqualTo("Bugbear"));
        }

        [Test]
        public void DeepDwarfConstant()
        {
            Assert.That(RaceConstants.BaseRaces.DeepDwarf, Is.EqualTo("Deep Dwarf"));
        }

        [Test]
        public void DeepHalflingConstant()
        {
            Assert.That(RaceConstants.BaseRaces.DeepHalfling, Is.EqualTo("Deep Halfling"));
        }

        [Test]
        public void DerroConstant()
        {
            Assert.That(RaceConstants.BaseRaces.Derro, Is.EqualTo("Derro"));
        }

        [Test]
        public void DoppelgangerConstant()
        {
            Assert.That(RaceConstants.BaseRaces.Doppelganger, Is.EqualTo("Doppelganger"));
        }

        [Test]
        public void DrowConstant()
        {
            Assert.That(RaceConstants.BaseRaces.Drow, Is.EqualTo("Drow"));
        }

        [Test]
        public void DuergarDwarfConstant()
        {
            Assert.That(RaceConstants.BaseRaces.DuergarDwarf, Is.EqualTo("Duergar Dwarf"));
        }

        [Test]
        public void ForestGnomeConstant()
        {
            Assert.That(RaceConstants.BaseRaces.ForestGnome, Is.EqualTo("Forest Gnome"));
        }

        [Test]
        public void GnollConstant()
        {
            Assert.That(RaceConstants.BaseRaces.Gnoll, Is.EqualTo("Gnoll"));
        }

        [Test]
        public void GoblinConstant()
        {
            Assert.That(RaceConstants.BaseRaces.Goblin, Is.EqualTo("Goblin"));
        }

        [Test]
        public void GrayElfConstant()
        {
            Assert.That(RaceConstants.BaseRaces.GrayElf, Is.EqualTo("Gray Elf"));
        }

        [Test]
        public void HalfElfConstant()
        {
            Assert.That(RaceConstants.BaseRaces.HalfElf, Is.EqualTo("Half-Elf"));
        }

        [Test]
        public void HalfOrcConstant()
        {
            Assert.That(RaceConstants.BaseRaces.HalfOrc, Is.EqualTo("Half-Orc"));
        }

        [Test]
        public void HighElfConstant()
        {
            Assert.That(RaceConstants.BaseRaces.HighElf, Is.EqualTo("High Elf"));
        }

        [Test]
        public void HillDwarfConstant()
        {
            Assert.That(RaceConstants.BaseRaces.HillDwarf, Is.EqualTo("Hill Dwarf"));
        }

        [Test]
        public void HobgoblinConstant()
        {
            Assert.That(RaceConstants.BaseRaces.Hobgoblin, Is.EqualTo("Hobgoblin"));
        }

        [Test]
        public void HumanConstant()
        {
            Assert.That(RaceConstants.BaseRaces.Human, Is.EqualTo("Human"));
        }

        [Test]
        public void KoboldConstant()
        {
            Assert.That(RaceConstants.BaseRaces.Kobold, Is.EqualTo("Kobold"));
        }

        [Test]
        public void LightfootHalflingConstant()
        {
            Assert.That(RaceConstants.BaseRaces.LightfootHalfling, Is.EqualTo("Lightfoot Halfling"));
        }

        [Test]
        public void LizardfolkConstant()
        {
            Assert.That(RaceConstants.BaseRaces.Lizardfolk, Is.EqualTo("Lizardfolk"));
        }

        [Test]
        public void MindFlayerConstant()
        {
            Assert.That(RaceConstants.BaseRaces.MindFlayer, Is.EqualTo("Mind Flayer"));
        }

        [Test]
        public void MinotaurConstant()
        {
            Assert.That(RaceConstants.BaseRaces.Minotaur, Is.EqualTo("Minotaur"));
        }

        [Test]
        public void MountainDwarfConstant()
        {
            Assert.That(RaceConstants.BaseRaces.MountainDwarf, Is.EqualTo("Mountain Dwarf"));
        }

        [Test]
        public void OgreConstant()
        {
            Assert.That(RaceConstants.BaseRaces.Ogre, Is.EqualTo("Ogre"));
        }

        [Test]
        public void OgreMageConstant()
        {
            Assert.That(RaceConstants.BaseRaces.OgreMage, Is.EqualTo("Ogre Mage"));
        }

        [Test]
        public void OrcConstant()
        {
            Assert.That(RaceConstants.BaseRaces.Orc, Is.EqualTo("Orc"));
        }

        [Test]
        public void RockGnomeConstant()
        {
            Assert.That(RaceConstants.BaseRaces.RockGnome, Is.EqualTo("Rock Gnome"));
        }

        [Test]
        public void SvirfneblinConstant()
        {
            Assert.That(RaceConstants.BaseRaces.Svirfneblin, Is.EqualTo("Svirfneblin"));
        }

        [Test]
        public void TallfellowHalflingConstant()
        {
            Assert.That(RaceConstants.BaseRaces.TallfellowHalfling, Is.EqualTo("Tallfellow Halfling"));
        }

        [Test]
        public void TieflingConstant()
        {
            Assert.That(RaceConstants.BaseRaces.Tiefling, Is.EqualTo("Tiefling"));
        }

        [Test]
        public void TroglodyteConstant()
        {
            Assert.That(RaceConstants.BaseRaces.Troglodyte, Is.EqualTo("Troglodyte"));
        }

        [Test]
        public void WildElfConstant()
        {
            Assert.That(RaceConstants.BaseRaces.WildElf, Is.EqualTo("Wild Elf"));
        }

        [Test]
        public void WoodElfConstant()
        {
            Assert.That(RaceConstants.BaseRaces.WoodElf, Is.EqualTo("Wood Elf"));
        }

        [Test]
        public void BaseRaces()
        {
            var baseRaces = RaceConstants.BaseRaces.GetBaseRaces();

            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.Aasimar), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.Bugbear), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.DeepDwarf), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.DeepHalfling), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.Derro), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.Doppelganger), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.Drow), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.DuergarDwarf), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.ForestGnome), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.Gnoll), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.Goblin), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.GrayElf), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.HalfElf), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.HalfOrc), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.HighElf), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.HillDwarf), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.Hobgoblin), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.Human), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.Kobold), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.LightfootHalfling), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.Lizardfolk), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.MindFlayer), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.Minotaur), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.MountainDwarf), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.Ogre), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.OgreMage), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.Orc), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.RockGnome), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.Svirfneblin), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.TallfellowHalfling), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.Tiefling), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.Troglodyte), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.WildElf), Is.True);
            Assert.That(baseRaces.Contains(RaceConstants.BaseRaces.WoodElf), Is.True);
            Assert.That(baseRaces.Count(), Is.EqualTo(34));
        }

        [Test]
        public void HalfCelestialConstant()
        {
            Assert.That(RaceConstants.Metaraces.HalfCelestial, Is.EqualTo("Half-Celestial"));
        }

        [Test]
        public void HalfDragonConstant()
        {
            Assert.That(RaceConstants.Metaraces.HalfDragon, Is.EqualTo("Half-Dragon"));
        }

        [Test]
        public void HalfFiendConstant()
        {
            Assert.That(RaceConstants.Metaraces.HalfFiend, Is.EqualTo("Half-Fiend"));
        }

        [Test]
        public void WerebearConstant()
        {
            Assert.That(RaceConstants.Metaraces.Werebear, Is.EqualTo("Werebear"));
        }

        [Test]
        public void WereboarConstant()
        {
            Assert.That(RaceConstants.Metaraces.Wereboar, Is.EqualTo("Wereboar"));
        }

        [Test]
        public void WereratConstant()
        {
            Assert.That(RaceConstants.Metaraces.Wererat, Is.EqualTo("Wererat"));
        }

        [Test]
        public void WeretigerConstant()
        {
            Assert.That(RaceConstants.Metaraces.Weretiger, Is.EqualTo("Weretiger"));
        }

        [Test]
        public void WerewolfConstant()
        {
            Assert.That(RaceConstants.Metaraces.Werewolf, Is.EqualTo("Werewolf"));
        }

        [Test]
        public void Metaraces()
        {
            var metaraces = RaceConstants.Metaraces.GetMetaraces();

            Assert.That(metaraces.Contains(RaceConstants.Metaraces.HalfCelestial), Is.True);
            Assert.That(metaraces.Contains(RaceConstants.Metaraces.HalfDragon), Is.True);
            Assert.That(metaraces.Contains(RaceConstants.Metaraces.HalfFiend), Is.True);
            Assert.That(metaraces.Contains(RaceConstants.Metaraces.Werebear), Is.True);
            Assert.That(metaraces.Contains(RaceConstants.Metaraces.Wereboar), Is.True);
            Assert.That(metaraces.Contains(RaceConstants.Metaraces.Wererat), Is.True);
            Assert.That(metaraces.Contains(RaceConstants.Metaraces.Weretiger), Is.True);
            Assert.That(metaraces.Contains(RaceConstants.Metaraces.Werewolf), Is.True);
            Assert.That(metaraces.Count(), Is.EqualTo(8));
        }
    }
}
using System;
using System.Linq;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Races
{
    [TestFixture]
    public class RaceConstantsTests
    {
        [TestCase(RaceConstants.BaseRaces.Aasimar, "Aasimar")]
        [TestCase(RaceConstants.BaseRaces.Bugbear, "Bugbear")]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, "Deep Dwarf")]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, "Deep Halfling")]
        [TestCase(RaceConstants.BaseRaces.Derro, "Derro")]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, "Doppelganger")]
        [TestCase(RaceConstants.BaseRaces.Drow, "Drow")]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, "Duergar Dwarf")]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, "Forest Gnome")]
        [TestCase(RaceConstants.BaseRaces.Gnoll, "Gnoll")]
        [TestCase(RaceConstants.BaseRaces.Goblin, "Goblin")]
        [TestCase(RaceConstants.BaseRaces.GrayElf, "Gray Elf")]
        [TestCase(RaceConstants.BaseRaces.HalfElf, "Half-Elf")]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, "Half-Orc")]
        [TestCase(RaceConstants.BaseRaces.HighElf, "High Elf")]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, "Hill Dwarf")]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, "Hobgoblin")]
        [TestCase(RaceConstants.BaseRaces.Human, "Human")]
        [TestCase(RaceConstants.BaseRaces.Kobold, "Kobold")]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, "Lightfoot Halfling")]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, "Lizardfolk")]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, "Mind Flayer")]
        [TestCase(RaceConstants.BaseRaces.Minotaur, "Minotaur")]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, "Mountain Dwarf")]
        [TestCase(RaceConstants.BaseRaces.Ogre, "Ogre")]
        [TestCase(RaceConstants.BaseRaces.OgreMage, "Ogre Mage")]
        [TestCase(RaceConstants.BaseRaces.Orc, "Orc")]
        [TestCase(RaceConstants.BaseRaces.RockGnome, "Rock Gnome")]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, "Svirfneblin")]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, "Tallfellow Halfling")]
        [TestCase(RaceConstants.BaseRaces.Tiefling, "Tiefling")]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, "Troglodyte")]
        [TestCase(RaceConstants.BaseRaces.WildElf, "Wild Elf")]
        [TestCase(RaceConstants.BaseRaces.WoodElf, "Wood Elf")]
        [TestCase(RaceConstants.Metaraces.HalfCelestial, "Half-Celestial")]
        [TestCase(RaceConstants.Metaraces.HalfDragon, "Half-Dragon")]
        [TestCase(RaceConstants.Metaraces.HalfFiend, "Half-Fiend")]
        [TestCase(RaceConstants.Metaraces.Werebear, "Werebear")]
        [TestCase(RaceConstants.Metaraces.Wereboar, "Wereboar")]
        [TestCase(RaceConstants.Metaraces.Wererat, "Wererat")]
        [TestCase(RaceConstants.Metaraces.Weretiger, "Weretiger")]
        [TestCase(RaceConstants.Metaraces.Werewolf, "Werewolf")]
        [TestCase(RaceConstants.Metaraces.None, "")]
        [TestCase(RaceConstants.Sizes.Large, "Large")]
        [TestCase(RaceConstants.Sizes.Medium, "Medium")]
        [TestCase(RaceConstants.Sizes.Small, "Small")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [Test]
        public void BaseRaces()
        {
            var baseRaces = RaceConstants.BaseRaces.GetBaseRaces();

            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.Aasimar));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.Bugbear));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.DeepDwarf));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.DeepHalfling));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.Derro));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.Doppelganger));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.Drow));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.DuergarDwarf));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.ForestGnome));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.Gnoll));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.Goblin));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.GrayElf));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.HalfElf));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.HalfOrc));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.HighElf));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.HillDwarf));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.Hobgoblin));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.Human));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.Kobold));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.LightfootHalfling));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.Lizardfolk));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.MindFlayer));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.Minotaur));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.MountainDwarf));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.Ogre));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.OgreMage));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.Orc));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.RockGnome));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.Svirfneblin));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.TallfellowHalfling));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.Tiefling));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.Troglodyte));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.WildElf));
            Assert.That(baseRaces, Contains.Item(RaceConstants.BaseRaces.WoodElf));
            Assert.That(baseRaces.Count(), Is.EqualTo(34));
        }

        [Test]
        public void Metaraces()
        {
            var metaraces = RaceConstants.Metaraces.GetMetaraces();

            Assert.That(metaraces, Contains.Item(RaceConstants.Metaraces.HalfCelestial));
            Assert.That(metaraces, Contains.Item(RaceConstants.Metaraces.HalfDragon));
            Assert.That(metaraces, Contains.Item(RaceConstants.Metaraces.HalfFiend));
            Assert.That(metaraces, Contains.Item(RaceConstants.Metaraces.Werebear));
            Assert.That(metaraces, Contains.Item(RaceConstants.Metaraces.Wereboar));
            Assert.That(metaraces, Contains.Item(RaceConstants.Metaraces.Wererat));
            Assert.That(metaraces, Contains.Item(RaceConstants.Metaraces.Weretiger));
            Assert.That(metaraces, Contains.Item(RaceConstants.Metaraces.Werewolf));
            Assert.That(metaraces.Count(), Is.EqualTo(8));
        }

        [Test]
        public void AllMetaraces()
        {
            var metaraces = RaceConstants.Metaraces.GetAllMetaraces();

            Assert.That(metaraces, Contains.Item(RaceConstants.Metaraces.HalfCelestial));
            Assert.That(metaraces, Contains.Item(RaceConstants.Metaraces.HalfDragon));
            Assert.That(metaraces, Contains.Item(RaceConstants.Metaraces.HalfFiend));
            Assert.That(metaraces, Contains.Item(RaceConstants.Metaraces.Werebear));
            Assert.That(metaraces, Contains.Item(RaceConstants.Metaraces.Wereboar));
            Assert.That(metaraces, Contains.Item(RaceConstants.Metaraces.Wererat));
            Assert.That(metaraces, Contains.Item(RaceConstants.Metaraces.Weretiger));
            Assert.That(metaraces, Contains.Item(RaceConstants.Metaraces.Werewolf));
            Assert.That(metaraces, Contains.Item(RaceConstants.Metaraces.None));
            Assert.That(metaraces.Count(), Is.EqualTo(9));
        }

        [Test]
        public void Sizes()
        {
            var sizes = RaceConstants.Sizes.GetSizes();

            Assert.That(sizes, Contains.Item(RaceConstants.Sizes.Large));
            Assert.That(sizes, Contains.Item(RaceConstants.Sizes.Medium));
            Assert.That(sizes, Contains.Item(RaceConstants.Sizes.Small));
            Assert.That(sizes.Count(), Is.EqualTo(3));
        }
    }
}
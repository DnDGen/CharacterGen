using System;
using CharacterGen.Common.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Common.Races
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
        [TestCase(RaceConstants.Metaraces.Species.Bronze, "Bronze")]
        [TestCase(RaceConstants.Metaraces.Species.Black, "Black")]
        [TestCase(RaceConstants.Metaraces.Species.Blue, "Blue")]
        [TestCase(RaceConstants.Metaraces.Species.Brass, "Brass")]
        [TestCase(RaceConstants.Metaraces.Species.Copper, "Copper")]
        [TestCase(RaceConstants.Metaraces.Species.Gold, "Gold")]
        [TestCase(RaceConstants.Metaraces.Species.Green, "Green")]
        [TestCase(RaceConstants.Metaraces.Species.Red, "Red")]
        [TestCase(RaceConstants.Metaraces.Species.Silver, "Silver")]
        [TestCase(RaceConstants.Metaraces.Species.White, "White")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
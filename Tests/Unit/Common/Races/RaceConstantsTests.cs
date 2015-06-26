using System;
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
        public void ConstantName(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(RaceConstants.BaseRaces.AasimarId, "Aasimar")]
        [TestCase(RaceConstants.BaseRaces.BugbearId, "Bugbear")]
        [TestCase(RaceConstants.BaseRaces.DeepDwarfId, "DeepDwarf")]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, "DeepHalfling")]
        [TestCase(RaceConstants.BaseRaces.DerroId, "Derro")]
        [TestCase(RaceConstants.BaseRaces.DoppelgangerId, "Doppelganger")]
        [TestCase(RaceConstants.BaseRaces.DrowId, "Drow")]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarfId, "DuergarDwarf")]
        [TestCase(RaceConstants.BaseRaces.ForestGnomeId, "ForestGnome")]
        [TestCase(RaceConstants.BaseRaces.GnollId, "Gnoll")]
        [TestCase(RaceConstants.BaseRaces.GoblinId, "Goblin")]
        [TestCase(RaceConstants.BaseRaces.GrayElfId, "GrayElf")]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, "HalfElf")]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, "HalfOrc")]
        [TestCase(RaceConstants.BaseRaces.HighElfId, "HighElf")]
        [TestCase(RaceConstants.BaseRaces.HillDwarfId, "HillDwarf")]
        [TestCase(RaceConstants.BaseRaces.HobgoblinId, "Hobgoblin")]
        [TestCase(RaceConstants.BaseRaces.HumanId, "Human")]
        [TestCase(RaceConstants.BaseRaces.KoboldId, "Kobold")]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, "LightfootHalfling")]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId, "Lizardfolk")]
        [TestCase(RaceConstants.BaseRaces.MindFlayerId, "MindFlayer")]
        [TestCase(RaceConstants.BaseRaces.MinotaurId, "Minotaur")]
        [TestCase(RaceConstants.BaseRaces.MountainDwarfId, "MountainDwarf")]
        [TestCase(RaceConstants.BaseRaces.OgreId, "Ogre")]
        [TestCase(RaceConstants.BaseRaces.OgreMageId, "OgreMage")]
        [TestCase(RaceConstants.BaseRaces.OrcId, "Orc")]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, "RockGnome")]
        [TestCase(RaceConstants.BaseRaces.SvirfneblinId, "Svirfneblin")]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, "TallfellowHalfling")]
        [TestCase(RaceConstants.BaseRaces.TieflingId, "Tiefling")]
        [TestCase(RaceConstants.BaseRaces.TroglodyteId, "Troglodyte")]
        [TestCase(RaceConstants.BaseRaces.WildElfId, "WildElf")]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, "WoodElf")]
        [TestCase(RaceConstants.Metaraces.HalfCelestialId, "HalfCelestial")]
        [TestCase(RaceConstants.Metaraces.HalfDragonId, "HalfDragon")]
        [TestCase(RaceConstants.Metaraces.HalfFiendId, "HalfFiend")]
        [TestCase(RaceConstants.Metaraces.WerebearId, "Werebear")]
        [TestCase(RaceConstants.Metaraces.WereboarId, "Wereboar")]
        [TestCase(RaceConstants.Metaraces.WereratId, "Wererat")]
        [TestCase(RaceConstants.Metaraces.WeretigerId, "Weretiger")]
        [TestCase(RaceConstants.Metaraces.WerewolfId, "Werewolf")]
        [TestCase(RaceConstants.Metaraces.NoneId, "None")]
        public void ConstantId(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

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
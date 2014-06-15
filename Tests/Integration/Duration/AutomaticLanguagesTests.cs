using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NPCGen.Common;
using NPCGen.Common.Races;
using NPCGen.Mappers.Interfaces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables
{
    [TestFixture]
    public class AutomaticLanguagesTests : IntegrationTests
    {
        [Inject]
        public ILanguagesMapper LanguagesXmlParser { get; set; }

        private Dictionary<String, IEnumerable<String>> languages;

        [SetUp]
        public void Setup()
        {
            languages = LanguagesXmlParser.Parse("AutomaticLanguages.xml");
        }

        [Test]
        public void AasimarAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.Aasimar];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Celestial), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void BugbearAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.Bugbear];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Goblin), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DerroAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.Derro];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Dwarven), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DoppelgangerAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.Doppelganger];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void DrowAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.Drow];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Elven), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Undercommon), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DuergarDwarfAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.DuergarDwarf];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Dwarven), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Undercommon), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DeepDwarfAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.DeepDwarf];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Dwarven), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void HillDwarfAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.HillDwarf];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Dwarven), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void MountainDwarfAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.MountainDwarf];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Dwarven), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GrayElfAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.GrayElf];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Elven), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void HighElfAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.HighElf];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Elven), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void WildElfAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.WildElf];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Elven), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void WoodElfAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.WoodElf];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Elven), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GnollAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.Gnoll];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Gnoll), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ForestGnomeAutomaticLanguagesZero()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.ForestGnome];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Elven), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Gnome), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Sylvan), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(4));
        }

        [Test]
        public void RockGnomeAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.RockGnome];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Gnome), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void SvirfneblinAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.Svirfneblin];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Gnome), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Undercommon), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(3));
        }

        [Test]
        public void GoblinAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.Goblin];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Goblin), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void HalfCelestialAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.Metaraces.HalfCelestial];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Celestial), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void HalfDragonAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.Metaraces.HalfDragon];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Draconic), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void HalfElfAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.HalfElf];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Elven), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void HalfFiendAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.Metaraces.HalfFiend];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Infernal), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void HalfOrcAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.HalfOrc];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Orc), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DeepHalflingAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.DeepHalfling];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Halfling), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void LightfootHalflingAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.LightfootHalfling];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Halfling), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void TallfellowHalflingAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.TallfellowHalfling];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Halfling), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void HobgoblinAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.Hobgoblin];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Goblin), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void HumanAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.Human];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void KoboldAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.Kobold];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Draconic), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void LizardfolkAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.Lizardfolk];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Draconic), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void MindFlayerAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.MindFlayer];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Undercommon), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void MinotaurAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.Minotaur];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Giant), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void OgreAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.Ogre];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Giant), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void OgreMageAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.OgreMage];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Giant), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void OrcAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.Orc];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Orc), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void TieflingAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.Tiefling];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(automaticLanguages.Contains(LanguageConstants.Infernal), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void TroglodyteAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.BaseRaces.Troglodyte];
            Assert.That(automaticLanguages.Contains(LanguageConstants.Draconic), Is.True);
            Assert.That(automaticLanguages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void WerebearAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.Metaraces.Werebear];
            Assert.That(automaticLanguages.Any(), Is.False);
        }

        [Test]
        public void WereboarAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.Metaraces.Wereboar];
            Assert.That(automaticLanguages.Any(), Is.False);
        }

        [Test]
        public void WereratAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.Metaraces.Wererat];
            Assert.That(automaticLanguages.Any(), Is.False);
        }

        [Test]
        public void WeretigerAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.Metaraces.Weretiger];
            Assert.That(automaticLanguages.Any(), Is.False);
        }

        [Test]
        public void WerewolfAutomaticLanguages()
        {
            var automaticLanguages = languages[RaceConstants.Metaraces.Werewolf];
            Assert.That(automaticLanguages.Any(), Is.False);
        }

        [Test]
        public void AllBaseRacesInTable()
        {
            foreach (var baseRace in RaceConstants.BaseRaces.GetBaseRaces())
                Assert.That(languages.ContainsKey(baseRace), Is.True, baseRace);
        }

        [Test]
        public void AllMetaracesInTable()
        {
            foreach (var metarace in RaceConstants.Metaraces.GetMetaraces())
                Assert.That(languages.ContainsKey(metarace), Is.True, metarace);

            Assert.That(languages.ContainsKey(String.Empty), Is.True, String.Empty);
        }
    }
}
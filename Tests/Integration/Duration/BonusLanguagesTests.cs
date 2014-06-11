using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NPCGen.Common;
using NPCGen.Common.Races;
using NPCGen.Mappers.Interfaces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data
{
    [TestFixture]
    public class BonusLanguagesTests : IntegrationTest
    {
        [Inject]
        public ILanguagesXmlParser LanguagesXmlParser { get; set; }

        private Dictionary<String, IEnumerable<String>> languages;

        [SetUp]
        public void Setup()
        {
            languages = LanguagesXmlParser.Parse("BonusLanguages.xml");
        }

        [Test]
        public void AasimarBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.Aasimar, LanguageConstants.Dwarven, LanguageConstants.Elven,
                LanguageConstants.Gnome, LanguageConstants.Draconic, LanguageConstants.Halfling, LanguageConstants.Sylvan);
        }

        [Test]
        public void BugbearBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.Bugbear, LanguageConstants.Giant, LanguageConstants.Elven,
                LanguageConstants.Gnoll, LanguageConstants.Draconic, LanguageConstants.Orc);
        }

        [Test]
        public void DerroBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.Derro, LanguageConstants.Goblin, LanguageConstants.Gnome,
                LanguageConstants.Giant, LanguageConstants.Orc, LanguageConstants.Terran, LanguageConstants.Undercommon);
        }

        [Test]
        public void DoppelgangerBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.Doppelganger, LanguageConstants.Auran, LanguageConstants.Dwarven,
                LanguageConstants.Elven, LanguageConstants.Gnome, LanguageConstants.Giant, LanguageConstants.Halfling, LanguageConstants.Terran);
        }

        [Test]
        public void DrowBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.Drow, LanguageConstants.Abyssal, LanguageConstants.Aquan,
                LanguageConstants.Draconic, LanguageConstants.Gnome, LanguageConstants.Goblin);
        }

        [Test]
        public void DuergarDwarfBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.DuergarDwarf, LanguageConstants.Giant, LanguageConstants.Goblin,
                LanguageConstants.Orc, LanguageConstants.Draconic, LanguageConstants.Terran);
        }

        [Test]
        public void DeepDwarfBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.DeepDwarf, LanguageConstants.Goblin, LanguageConstants.Gnome,
                LanguageConstants.Giant, LanguageConstants.Orc, LanguageConstants.Terran, LanguageConstants.Undercommon);
        }

        [Test]
        public void HillDwarfBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.HillDwarf, LanguageConstants.Goblin, LanguageConstants.Gnome,
                LanguageConstants.Giant, LanguageConstants.Orc, LanguageConstants.Terran, LanguageConstants.Undercommon);
        }

        [Test]
        public void MountainDwarfBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.MountainDwarf, LanguageConstants.Goblin, LanguageConstants.Gnome,
                LanguageConstants.Giant, LanguageConstants.Orc, LanguageConstants.Terran, LanguageConstants.Undercommon);
        }

        [Test]
        public void GrayElfBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.GrayElf, LanguageConstants.Draconic, LanguageConstants.Gnoll,
                LanguageConstants.Goblin, LanguageConstants.Orc, LanguageConstants.Gnome, LanguageConstants.Sylvan);
        }

        [Test]
        public void HighElfBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.HighElf, LanguageConstants.Draconic, LanguageConstants.Gnoll,
                LanguageConstants.Goblin, LanguageConstants.Orc, LanguageConstants.Gnome, LanguageConstants.Sylvan);
        }

        [Test]
        public void WildElfBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.WildElf, LanguageConstants.Draconic, LanguageConstants.Gnoll,
                LanguageConstants.Goblin, LanguageConstants.Orc, LanguageConstants.Gnome, LanguageConstants.Sylvan);
        }

        [Test]
        public void WoodElfBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.WoodElf, LanguageConstants.Draconic, LanguageConstants.Gnoll,
                LanguageConstants.Goblin, LanguageConstants.Orc, LanguageConstants.Gnome, LanguageConstants.Sylvan);
        }

        [Test]
        public void GnollBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.Gnoll, LanguageConstants.Common, LanguageConstants.Goblin,
                LanguageConstants.Orc, LanguageConstants.Draconic, LanguageConstants.Elven);
        }

        [Test]
        public void ForestGnomeBonusLanguagesZero()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.ForestGnome, LanguageConstants.Dwarven, LanguageConstants.Elven,
                LanguageConstants.Draconic, LanguageConstants.Goblin, LanguageConstants.Giant);
        }

        [Test]
        public void RockGnomeBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.RockGnome, LanguageConstants.Dwarven, LanguageConstants.Elven,
                LanguageConstants.Draconic, LanguageConstants.Goblin, LanguageConstants.Giant, LanguageConstants.Orc);
        }

        [Test]
        public void SvirfneblinBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.Svirfneblin, LanguageConstants.Dwarven, LanguageConstants.Elven,
                LanguageConstants.Terran, LanguageConstants.Goblin, LanguageConstants.Giant, LanguageConstants.Orc);
        }

        [Test]
        public void GoblinBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.Goblin, LanguageConstants.Draconic, LanguageConstants.Elven,
                LanguageConstants.Giant, LanguageConstants.Gnoll, LanguageConstants.Orc);
        }

        [Test]
        public void HalfElfBonusLanguages()
        {
            AssertAllLanguagesAreBonusLanguages(RaceConstants.BaseRaces.HalfElf);
        }

        [Test]
        public void HalfOrcBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.HalfOrc, LanguageConstants.Abyssal, LanguageConstants.Goblin,
                LanguageConstants.Giant, LanguageConstants.Gnoll, LanguageConstants.Draconic);
        }

        [Test]
        public void DeepHalflingBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.DeepHalfling, LanguageConstants.Dwarven, LanguageConstants.Elven,
                LanguageConstants.Gnome, LanguageConstants.Goblin, LanguageConstants.Orc, LanguageConstants.Undercommon);
        }

        [Test]
        public void LightfootHalflingBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.LightfootHalfling, LanguageConstants.Dwarven, LanguageConstants.Elven,
                LanguageConstants.Gnome, LanguageConstants.Goblin, LanguageConstants.Orc);
        }

        [Test]
        public void TallfellowHalflingBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.TallfellowHalfling, LanguageConstants.Dwarven, LanguageConstants.Elven,
                LanguageConstants.Gnome, LanguageConstants.Goblin, LanguageConstants.Orc);
        }

        [Test]
        public void HobgoblinBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.Hobgoblin, LanguageConstants.Dwarven, LanguageConstants.Infernal,
                LanguageConstants.Orc, LanguageConstants.Draconic, LanguageConstants.Giant);
        }

        [Test]
        public void HumanBonusLanguages()
        {
            AssertAllLanguagesAreBonusLanguages(RaceConstants.BaseRaces.Human);
        }

        [Test]
        public void KoboldBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.Kobold, LanguageConstants.Common, LanguageConstants.Undercommon);
        }

        [Test]
        public void LizardfolkBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.Lizardfolk, LanguageConstants.Aquan, LanguageConstants.Goblin,
                LanguageConstants.Orc, LanguageConstants.Gnoll);
        }

        [Test]
        public void MindFlayerBonusLanguages()
        {
            AssertAllLanguagesAreBonusLanguages(RaceConstants.BaseRaces.MindFlayer);
        }

        [Test]
        public void MinotaurBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.Minotaur, LanguageConstants.Terran, LanguageConstants.Goblin,
                LanguageConstants.Orc);
        }

        [Test]
        public void OgreBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.Ogre, LanguageConstants.Terran, LanguageConstants.Goblin,
                LanguageConstants.Orc, LanguageConstants.Dwarven);
        }

        [Test]
        public void OgreMageBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.OgreMage, LanguageConstants.Infernal, LanguageConstants.Goblin,
                LanguageConstants.Orc, LanguageConstants.Dwarven);
        }

        [Test]
        public void OrcBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.Orc, LanguageConstants.Giant, LanguageConstants.Goblin,
                LanguageConstants.Gnoll, LanguageConstants.Dwarven, LanguageConstants.Undercommon);
        }

        [Test]
        public void TieflingBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.Tiefling, LanguageConstants.Draconic, LanguageConstants.Dwarven,
                LanguageConstants.Elven, LanguageConstants.Gnome, LanguageConstants.Goblin, LanguageConstants.Halfling, LanguageConstants.Orc);
        }

        [Test]
        public void TroglodyteBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.Troglodyte, LanguageConstants.Common, LanguageConstants.Goblin,
                LanguageConstants.Giant, LanguageConstants.Orc);
        }

        [Test]
        public void AllBaseRacesInTable()
        {
            foreach (var baseRace in RaceConstants.BaseRaces.GetBaseRaces())
                Assert.That(languages.ContainsKey(baseRace), Is.True, baseRace);
        }

        private void AssertLanguagesAreBonusLanguages(String baseRace, params String[] expectedLanguages)
        {
            var bonusLanguages = languages[baseRace];

            foreach (var language in expectedLanguages)
                Assert.That(bonusLanguages.Contains(language), Is.True, language);

            Assert.That(bonusLanguages.Count(), Is.EqualTo(expectedLanguages.Length));
        }

        private void AssertAllLanguagesAreBonusLanguages(String baseRace)
        {
            var bonusLanguages = languages[baseRace];
            var allLanguages = LanguageConstants.GetLanguages();
            var applicableLanguages = allLanguages.Except(new[] { LanguageConstants.Druidic });

            foreach (var language in applicableLanguages)
                Assert.That(bonusLanguages.Contains(language), Is.True, language);

            Assert.That(bonusLanguages.Count(), Is.EqualTo(applicableLanguages.Count()));
        }
    }
}
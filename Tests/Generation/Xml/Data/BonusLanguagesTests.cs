using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Core.Data;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Xml.Parsers;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data
{
    [TestFixture]
    public class BonusLanguagesTests
    {
        private Dictionary<String, IEnumerable<String>> languages;

        [SetUp]
        public void Setup()
        {
            var streamLoader = new EmbeddedResourceStreamLoader();
            var parser = new LanguagesXmlParser(streamLoader);
            languages = parser.Parse("BonusLanguages.xml");
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
            var bonusLanguages = languages[RaceConstants.BaseRaces.MountainDwarf];
            Assert.That(bonusLanguages.Contains(LanguageConstants.Goblin), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Gnome), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Giant), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Orc), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Terran), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Undercommon), Is.True);
            Assert.That(bonusLanguages.Count(), Is.EqualTo(6));
        }

        [Test]
        public void GrayElfBonusLanguages()
        {
            var bonusLanguages = languages[RaceConstants.BaseRaces.GrayElf];
            Assert.That(bonusLanguages.Contains(LanguageConstants.Draconic), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Gnoll), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Goblin), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Orc), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Gnome), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Sylvan), Is.True);
            Assert.That(bonusLanguages.Count(), Is.EqualTo(6));
        }

        [Test]
        public void HighElfBonusLanguages()
        {
            var bonusLanguages = languages[RaceConstants.BaseRaces.HighElf];
            Assert.That(bonusLanguages.Contains(LanguageConstants.Draconic), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Gnoll), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Goblin), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Orc), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Gnome), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Sylvan), Is.True);
            Assert.That(bonusLanguages.Count(), Is.EqualTo(6));
        }

        [Test]
        public void WildElfBonusLanguages()
        {
            var bonusLanguages = languages[RaceConstants.BaseRaces.WildElf];
            Assert.That(bonusLanguages.Contains(LanguageConstants.Draconic), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Gnoll), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Goblin), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Orc), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Gnome), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Sylvan), Is.True);
            Assert.That(bonusLanguages.Count(), Is.EqualTo(6));
        }

        [Test]
        public void WoodElfBonusLanguages()
        {
            var bonusLanguages = languages[RaceConstants.BaseRaces.WoodElf];
            Assert.That(bonusLanguages.Contains(LanguageConstants.Draconic), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Gnoll), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Goblin), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Orc), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Gnome), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Sylvan), Is.True);
            Assert.That(bonusLanguages.Count(), Is.EqualTo(6));
        }

        [Test]
        public void GnollBonusLanguages()
        {
            var bonusLanguages = languages[RaceConstants.BaseRaces.Gnoll];
            Assert.That(bonusLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Goblin), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Orc), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Draconic), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Elven), Is.True);
            Assert.That(bonusLanguages.Count(), Is.EqualTo(5));
        }

        [Test]
        public void ForestGnomeBonusLanguagesZero()
        {
            var BonusLanguages = languages[RaceConstants.BaseRaces.ForestGnome];
            Assert.That(BonusLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(BonusLanguages.Contains(LanguageConstants.Elven), Is.True);
            Assert.That(BonusLanguages.Contains(LanguageConstants.Gnome), Is.True);
            Assert.That(BonusLanguages.Contains(LanguageConstants.Sylvan), Is.True);
            Assert.That(BonusLanguages.Count(), Is.EqualTo(4));

            //            switch (Language)
            //            {
            //                case "Dwarven":
            //                case "Elven":
            //                case "Draconic":
            //                case "Goblin":
            //                case "Giant":
            //                case "Orc": return true;
            //                default: return false;
            //            }
        }

        [Test]
        public void RockGnomeBonusLanguages()
        {
            var BonusLanguages = languages[RaceConstants.BaseRaces.RockGnome];
            Assert.That(BonusLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(BonusLanguages.Contains(LanguageConstants.Gnome), Is.True);
            Assert.That(BonusLanguages.Count(), Is.EqualTo(2));

            //            switch (Language)
            //            {
            //                case "Dwarven":
            //                case "Elven":
            //                case "Draconic":
            //                case "Goblin":
            //                case "Giant":
            //                case "Orc": return true;
            //                default: return false;
            //            }
        }

        [Test]
        public void SvirfneblinBonusLanguages()
        {
            var BonusLanguages = languages[RaceConstants.BaseRaces.Svirfneblin];
            Assert.That(BonusLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(BonusLanguages.Contains(LanguageConstants.Gnome), Is.True);
            Assert.That(BonusLanguages.Contains(LanguageConstants.Undercommon), Is.True);
            Assert.That(BonusLanguages.Count(), Is.EqualTo(3));


            //        case RACE.SVIRFNEBLIN:
            //            switch (Language)
            //            {
            //                case "Dwarven":
            //                case "Elven":
            //                case "Terran":
            //                case "Goblin":
            //                case "Giant":
            //                case "Orc": return true;
            //                default: return false;
            //            }
        }

        [Test]
        public void GoblinBonusLanguages()
        {
            var bonusLanguages = languages[RaceConstants.BaseRaces.Goblin];
            Assert.That(bonusLanguages.Contains(LanguageConstants.Draconic), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Elven), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Giant), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Gnoll), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Orc), Is.True);
            Assert.That(bonusLanguages.Count(), Is.EqualTo(5));
        }

        [Test]
        public void HalfElfBonusLanguages()
        {
            AssertAllLanguagesAreBonusLanguages(RaceConstants.BaseRaces.HalfElf);
        }

        [Test]
        public void HalfOrcBonusLanguages()
        {
            var bonusLanguages = languages[RaceConstants.BaseRaces.HalfOrc];
            Assert.That(bonusLanguages.Contains(LanguageConstants.Abyssal), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Goblin), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Gnoll), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Draconic), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Giant), Is.True);
            Assert.That(bonusLanguages.Count(), Is.EqualTo(5));
        }

        [Test]
        public void DeepHalflingBonusLanguages()
        {
            AssertLanguagesAreBonusLanguages(RaceConstants.BaseRaces.DeepHalfling, LanguageConstants.Dwarven, LanguageConstants.Elven,
                LanguageConstants.Gnome, LanguageConstants.Goblin, LanguageConstants.Orc);
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
            var bonusLanguages = languages[RaceConstants.BaseRaces.Hobgoblin];
            Assert.That(bonusLanguages.Contains(LanguageConstants.Dwarven), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Infernal), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Orc), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Draconic), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Giant), Is.True);
            Assert.That(bonusLanguages.Count(), Is.EqualTo(5));
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
            var BonusLanguages = languages[RaceConstants.BaseRaces.Lizardfolk];
            Assert.That(BonusLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(BonusLanguages.Contains(LanguageConstants.Draconic), Is.True);
            Assert.That(BonusLanguages.Count(), Is.EqualTo(2));

            //        case RACE.LIZARDFOLK:
            //            switch (Language)
            //            {
            //                case "Aquan":
            //                case "Goblin":
            //                case "Orc":
            //                case "Gnoll": return true;
            //                default: return false;
            //            }
        }

        [Test]
        public void MindFlayerBonusLanguages()
        {
            AssertAllLanguagesAreBonusLanguages(RaceConstants.BaseRaces.MindFlayer);
        }

        [Test]
        public void MinotaurBonusLanguages()
        {
            var BonusLanguages = languages[RaceConstants.BaseRaces.Minotaur];
            Assert.That(BonusLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(BonusLanguages.Contains(LanguageConstants.Giant), Is.True);
            Assert.That(BonusLanguages.Count(), Is.EqualTo(2));

            //        case RACE.MINOTAUR:
            //            switch (Language)
            //            {
            //                case "Terran":
            //                case "Goblin":
            //                case "Orc": return true;
            //                default: return false;
            //            }
        }

        [Test]
        public void OgreBonusLanguages()
        {
            var BonusLanguages = languages[RaceConstants.BaseRaces.Ogre];
            Assert.That(BonusLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(BonusLanguages.Contains(LanguageConstants.Giant), Is.True);
            Assert.That(BonusLanguages.Count(), Is.EqualTo(2));

            //        case RACE.OGRE:
            //            switch (Language)
            //            {
            //                case "Dwarven":
            //                case "Goblin":
            //                case "Terran":
            //                case "Orc": return true;
            //                default: return false;
            //            }
        }

        [Test]
        public void OgreMageBonusLanguages()
        {
            var BonusLanguages = languages[RaceConstants.BaseRaces.OgreMage];
            Assert.That(BonusLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(BonusLanguages.Contains(LanguageConstants.Giant), Is.True);
            Assert.That(BonusLanguages.Count(), Is.EqualTo(2));

            //        case RACE.OGRE_MAGE:
            //            switch (Language)
            //            {
            //                case "Dwarven":
            //                case "Goblin":
            //                case "Orc":
            //                case "Infernal": return true;
            //                default: return false;
            //            } 
        }

        [Test]
        public void OrcBonusLanguages()
        {
            var BonusLanguages = languages[RaceConstants.BaseRaces.Orc];
            Assert.That(BonusLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(BonusLanguages.Contains(LanguageConstants.Orc), Is.True);
            Assert.That(BonusLanguages.Count(), Is.EqualTo(2));

            //        case RACE.ORC:
            //            switch (Language)
            //            {
            //                case "Dwarven":
            //                case "Giant":
            //                case "Gnoll":
            //                case "Goblin":
            //                case "Undercommon": return true;
            //                default: return false;
            //            }
        }

        [Test]
        public void TieflingBonusLanguages()
        {
            var BonusLanguages = languages[RaceConstants.BaseRaces.Tiefling];
            Assert.That(BonusLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(BonusLanguages.Contains(LanguageConstants.Infernal), Is.True);
            Assert.That(BonusLanguages.Count(), Is.EqualTo(2));

            //        case RACE.TIEFLING:
            //            switch (Language)
            //            {
            //                case "Draconic":
            //                case "Dwarven":
            //                case "Elven":
            //                case "Gnome":
            //                case "Goblin":
            //                case "Halfling":
            //                case "Orc": return true;
            //                default: return false;
            //            }
        }

        [Test]
        public void TroglodyteBonusLanguages()
        {
            var bonusLanguages = languages[RaceConstants.BaseRaces.Troglodyte];
            Assert.That(bonusLanguages.Contains(LanguageConstants.Common), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Goblin), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Orc), Is.True);
            Assert.That(bonusLanguages.Contains(LanguageConstants.Giant), Is.True);
            Assert.That(bonusLanguages.Count(), Is.EqualTo(4));
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
                Assert.That(bonusLanguages.Contains(language), Is.True);

            Assert.That(bonusLanguages.Count(), Is.EqualTo(expectedLanguages.Length));
        }

        private void AssertAllLanguagesAreBonusLanguages(String baseRace)
        {
            var bonusLanguages = languages[baseRace];
            var allLanguages = LanguageConstants.GetLanguages();
            var applicableLanguages = allLanguages.Except(new[] { LanguageConstants.Druidic });

            foreach (var language in applicableLanguages)
                Assert.That(bonusLanguages.Contains(language), Is.True);

            Assert.That(bonusLanguages.Count(), Is.EqualTo(applicableLanguages.Count()));
        }
    }
}
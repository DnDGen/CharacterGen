using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Abilities;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Languages
{
    [TestFixture]
    public class AutomaticLanguagesTests : CollectionTests
    {
        protected override String tableName
        {
            get { return "AutomaticLanguages"; }
        }

        protected override IEnumerable<String> nameCollection
        {
            get
            {
                var baseRaces = RaceConstants.BaseRaces.GetBaseRaces();
                var metaraces = RaceConstants.Metaraces.GetMetaraces();

                return baseRaces.Union(metaraces);
            }
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, LanguageConstants.Common,
                                                   LanguageConstants.Celestial)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, LanguageConstants.Common,
                                                   LanguageConstants.Goblin)]
        [TestCase(RaceConstants.BaseRaces.Derro, LanguageConstants.Common,
                                                 LanguageConstants.Dwarven)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, LanguageConstants.Common)]
        [TestCase(RaceConstants.BaseRaces.Drow, LanguageConstants.Common,
                                                LanguageConstants.Elven,
                                                LanguageConstants.Undercommon)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, LanguageConstants.Common,
                                                        LanguageConstants.Dwarven,
                                                        LanguageConstants.Undercommon)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, LanguageConstants.Common,
                                                     LanguageConstants.Dwarven)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, LanguageConstants.Common,
                                                     LanguageConstants.Dwarven)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, LanguageConstants.Common,
                                                         LanguageConstants.Dwarven)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, LanguageConstants.Common,
                                                   LanguageConstants.Elven)]
        [TestCase(RaceConstants.BaseRaces.HighElf, LanguageConstants.Common,
                                                   LanguageConstants.Elven)]
        [TestCase(RaceConstants.BaseRaces.WildElf, LanguageConstants.Common,
                                                   LanguageConstants.Elven)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, LanguageConstants.Common,
                                                   LanguageConstants.Elven)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, LanguageConstants.Gnoll)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, LanguageConstants.Common,
                                                       LanguageConstants.Elven,
                                                       LanguageConstants.Gnome,
                                                       LanguageConstants.Sylvan)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, LanguageConstants.Common,
                                                     LanguageConstants.Gnome)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, LanguageConstants.Common,
                                                       LanguageConstants.Gnome,
                                                       LanguageConstants.Undercommon)]
        [TestCase(RaceConstants.BaseRaces.Goblin, LanguageConstants.Common,
                                                  LanguageConstants.Goblin)]
        [TestCase(RaceConstants.Metaraces.HalfCelestial, LanguageConstants.Celestial)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, LanguageConstants.Draconic)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, LanguageConstants.Common,
                                                   LanguageConstants.Elven)]
        [TestCase(RaceConstants.Metaraces.HalfFiend, LanguageConstants.Infernal)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, LanguageConstants.Common,
                                                   LanguageConstants.Orc)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, LanguageConstants.Common,
                                                        LanguageConstants.Halfling)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, LanguageConstants.Common,
                                                             LanguageConstants.Halfling)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, LanguageConstants.Common,
                                                              LanguageConstants.Halfling)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, LanguageConstants.Common,
                                                     LanguageConstants.Goblin)]
        [TestCase(RaceConstants.BaseRaces.Human, LanguageConstants.Common)]
        [TestCase(RaceConstants.BaseRaces.Kobold, LanguageConstants.Draconic)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, LanguageConstants.Common,
                                                      LanguageConstants.Draconic)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, LanguageConstants.Common,
                                                      LanguageConstants.Undercommon)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, LanguageConstants.Common,
                                                    LanguageConstants.Giant)]
        [TestCase(RaceConstants.BaseRaces.Ogre, LanguageConstants.Common,
                                                LanguageConstants.Giant)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, LanguageConstants.Common,
                                                    LanguageConstants.Giant)]
        [TestCase(RaceConstants.BaseRaces.Orc, LanguageConstants.Common,
                                               LanguageConstants.Orc)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, LanguageConstants.Common,
                                                    LanguageConstants.Infernal)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, LanguageConstants.Draconic)]
        [TestCase(RaceConstants.Metaraces.Werebear)]
        [TestCase(RaceConstants.Metaraces.Wereboar)]
        [TestCase(RaceConstants.Metaraces.Wererat)]
        [TestCase(RaceConstants.Metaraces.Weretiger)]
        [TestCase(RaceConstants.Metaraces.Werewolf)]
        [TestCase(RaceConstants.Metaraces.None)]
        public override void Collection(String name, params String[] languages)
        {
            base.Collection(name, languages);
        }
    }
}
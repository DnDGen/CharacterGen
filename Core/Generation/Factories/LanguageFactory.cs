using System;
using System.Collections.Generic;
using D20Dice.Dice;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Providers;

namespace NPCGen.Core.Generation.Factories
{
    public static class LanguageFactory
    {
        public static IEnumerable<String> CreateUsing(Race race, CharacterClass characterClass, IDice dice)
        {
            var languages = new List<String>();
            var languagesProvider = ProviderFactory.CreateLanguagesProvider();

            var automaticLanguages = languagesProvider.GetAutomaticLanguagesFor(race);
            languages.AddRange(automaticLanguages);

            //var NumberOfLanguages = (StatScores[Stats.Intelligence] - 10) / 2;

            //switch (Race.Race)
            //{
            //    case RACE.AASIMAR: languages = "Common, Celestial"; break;
            //    case RACE.GOBLIN:
            //    case RACE.HOBGOBLIN:
            //    case RACE.BUGBEAR: languages = "Common, Goblin"; break;
            //    case RACE.DUERGAR: languages = "Common, Dwarven, Undercommon"; break;
            //    case RACE.HILL_DWARF:
            //    case RACE.MOUNTAIN_DWARF:
            //    case RACE.DEEP_DWARF:
            //    case RACE.DERRO_DWARF: languages = "Common, Dwarven"; break;
            //    case RACE.DROW: languages = "Common, Elven, Undercommon"; break;
            //    case RACE.FOREST_GNOME: languages = "Common, Gnome, Elven, Sylvan"; break;
            //    case RACE.GNOLL: languages = "Gnoll"; break;
            //    case RACE.GRAY_ELF:
            //    case RACE.HIGH_ELF:
            //    case RACE.WILD_ELF:
            //    case RACE.WOOD_ELF:
            //    case RACE.HALFELF: languages = "Common, Elven"; break;
            //    case RACE.ORC:
            //    case RACE.HALFORC: languages = "Common, Orc"; break;
            //    case RACE.LIZARDFOLK: languages = "Common, Draconic"; break;
            //    case RACE.TROGLODYTE:
            //    case RACE.KOBOLD: languages = "Draconic"; break;
            //    case RACE.DEEP_HALFLING:
            //    case RACE.TALLFELLOW_HALFLING:
            //    case RACE.LIGHTFOOT_HALFLING: languages = "Common, Halfling"; break;
            //    case RACE.MIND_FLAYER: languages = "Common, Undercommon"; break;
            //    case RACE.OGRE:
            //    case RACE.OGRE_MAGE:
            //    case RACE.MINOTAUR: languages = "Common, Giant"; break;
            //    case RACE.ROCK_GNOME: languages = "Common, Gnome"; break;
            //    case RACE.SVIRFNEBLIN: languages = "Common, Gnome, Undercommon"; break;
            //    case RACE.TIEFLING: languages = "Common, Infernal"; break;
            //    default: languages = "Common"; break;
            //}

            //switch (Race.MetaRace)
            //{
            //    case METARACE.HALF_CELESTIAL:
            //        if (!languages.Contains("Celestial"))
            //            languages += ", Celestial";
            //        break;
            //    case METARACE.HALF_DRAGON:
            //        if (!languages.Contains("Draconic"))
            //            languages += ", Draconic";
            //        break;
            //    case METARACE.HALF_FIEND:
            //        if (!languages.Contains("Infernal"))
            //            languages += ", Infernal";
            //        break;
            //    default: break;
            //}

            //if (Class == CLASS.DRUID)
            //    languages += ", Druidic";

            //while (NumberOfLanguages > 0)
            //{
            //    switch (Dice.d20())
            //    {
            //        case 1:
            //            if (!languages.Contains("Abyssal"))
            //            {
            //                if (Class == CLASS.CLERIC || Race.CanSpeak("Abyssal"))
            //                {
            //                    NumberOfLanguages--;
            //                    languages += ", Abyssal";
            //                }
            //            }
            //            break;
            //        case 2:
            //            if (!languages.Contains("Aquan"))
            //            {
            //                if (Race.CanSpeak("Aquan"))
            //                {
            //                    NumberOfLanguages--;
            //                    languages += ", Aquan";
            //                }
            //            }
            //            break;
            //        case 3:
            //            if (!languages.Contains("Auran"))
            //            {
            //                if (Race.CanSpeak("Auran"))
            //                {
            //                    NumberOfLanguages--;
            //                    languages += ", Auran";
            //                }
            //            }
            //            break;
            //        case 4:
            //            if (!languages.Contains("Celestial"))
            //            {
            //                if (Class == CLASS.CLERIC || Race.CanSpeak("Celestial"))
            //                {
            //                    NumberOfLanguages--;
            //                    languages += ", Celestial";
            //                }
            //            }
            //            break;
            //        case 5:
            //            if (!languages.Contains("Common"))
            //            {
            //                NumberOfLanguages--;
            //                languages += ", Common";
            //            }
            //            break;
            //        case 6:
            //            if (!languages.Contains("Draconic"))
            //            {
            //                if (Class == CLASS.WIZARD || Race.CanSpeak("Draconic"))
            //                {
            //                    NumberOfLanguages--;
            //                    languages += ", Draconic";
            //                }
            //            }
            //            break;
            //        case 7: break; //Druidic
            //        case 8:
            //            if (!languages.Contains("Dwarven"))
            //            {
            //                if (Race.CanSpeak("Dwarven"))
            //                {
            //                    NumberOfLanguages--;
            //                    languages += ", Dwarven";
            //                }
            //            }
            //            break;
            //        case 9:
            //            if (!languages.Contains("Elven"))
            //            {
            //                if (Race.CanSpeak("Elven"))
            //                {
            //                    NumberOfLanguages--;
            //                    languages += ", Elven";
            //                }
            //            }
            //            break;
            //        case 10:
            //            if (!languages.Contains("Giant"))
            //            {
            //                if (Race.CanSpeak("Giant"))
            //                {
            //                    NumberOfLanguages--;
            //                    languages += ", Giant";
            //                }
            //            }
            //            break;
            //        case 11:
            //            if (!languages.Contains("Gnome"))
            //            {
            //                if (Race.CanSpeak("Gnome"))
            //                {
            //                    NumberOfLanguages--;
            //                    languages += ", Gnome";
            //                }
            //            }
            //            break;
            //        case 12:
            //            if (!languages.Contains("Goblin"))
            //            {
            //                if (Race.CanSpeak("Goblin"))
            //                {
            //                    NumberOfLanguages--;
            //                    languages += ", Goblin";
            //                }
            //            }
            //            break;
            //        case 13:
            //            if (!languages.Contains("Gnoll"))
            //            {
            //                if (Race.CanSpeak("Gnoll"))
            //                {
            //                    NumberOfLanguages--;
            //                    languages += ", Gnoll";
            //                }
            //            }
            //            break;
            //        case 14:
            //            if (!languages.Contains("Halfling"))
            //            {
            //                if (Race.CanSpeak("Halfling"))
            //                {
            //                    NumberOfLanguages--;
            //                    languages += ", Halfling";
            //                }
            //            }
            //            break;
            //        case 15:
            //            if (!languages.Contains("Ignan"))
            //            {
            //                if (Race.CanSpeak("Ignan"))
            //                {
            //                    NumberOfLanguages--;
            //                    languages += ", Ignan";
            //                }
            //            }
            //            break;
            //        case 16:
            //            if (!languages.Contains("Infernal"))
            //            {
            //                if (Class == CLASS.CLERIC || Race.CanSpeak("Infernal"))
            //                {
            //                    NumberOfLanguages--;
            //                    languages += ", Infernal";
            //                }
            //            }
            //            break;
            //        case 17:
            //            if (!languages.Contains("Orc"))
            //            {
            //                if (Race.CanSpeak("Orc"))
            //                {
            //                    NumberOfLanguages--;
            //                    languages += ", Orc";
            //                }
            //            }
            //            break;
            //        case 18:
            //            if (!languages.Contains("Sylvan"))
            //            {
            //                if (Class == CLASS.DRUID || Race.CanSpeak("Sylvan"))
            //                {
            //                    NumberOfLanguages--;
            //                    languages += ", Sylvan";
            //                }
            //            }
            //            break;
            //        case 19:
            //            if (!languages.Contains("Terran"))
            //            {
            //                if (Race.CanSpeak("Terran"))
            //                {
            //                    NumberOfLanguages--;
            //                    languages += ", Terran";
            //                }
            //            }
            //            break;
            //        case 20:
            //            if (!languages.Contains("Undercommon"))
            //            {
            //                if (Race.CanSpeak("Undercommon"))
            //                {
            //                    NumberOfLanguages--;
            //                    languages += ", Undercommon";
            //                }
            //            }
            //            break;
            //        default: return "[ERROR: d20 out of loop.  Character.197]";
            //    }
            //}

            return languages;
        }
    }
}
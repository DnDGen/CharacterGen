using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice.Dice;
using NPCGen.Core.Data;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Providers;

namespace NPCGen.Core.Generation.Factories
{
    public static class LanguageFactory
    {
        public static IEnumerable<String> CreateUsing(Race race, String className, IDice dice, Int32 intelligenceBonus)
        {
            var languages = new List<String>();
            var languagesProvider = ProviderFactory.CreateLanguagesProvider();

            var automaticLanguages = languagesProvider.GetAutomaticLanguagesFor(race);
            languages.AddRange(automaticLanguages);

            if (className == CharacterClassConstants.Druid)
                languages.Add(LanguageConstants.Druidic);

            var bonusLanguages = languagesProvider.GetBonusLanguagesFor(race.BaseRace, className);
            var numberOfLanguages = intelligenceBonus;
            while(numberOfLanguages > 0 && bonusLanguages.Except(languages).Any())
            {
                var language = GetBonusLanguage();

                if (!languages.Contains(language) && bonusLanguages.Contains(language))
                {
                    languages.Add(language);
                    numberOfLanguages--;
                }
            }

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

        //    public Boolean CanSpeak(String Language)
        //{
        //    switch (Race)
        //    {
        //        case RACE.AASIMAR:
        //            switch (Language)
        //            {
        //                case "Draconic":
        //                case "Dwarven":
        //                case "Elven":
        //                case "Gnome":
        //                case "Halfling":
        //                case "Sylvan": return true;
        //                default: return false;
        //            }
        //        case RACE.BUGBEAR:
        //        case RACE.GOBLIN:
        //            switch (Language)
        //            {
        //                case "Draconic":
        //                case "Elven":
        //                case "Giant":
        //                case "Gnoll":
        //                case "Orc": return true;
        //                default: return false;
        //            }
        //        case RACE.DEEP_DWARF:
        //        case RACE.HILL_DWARF:
        //        case RACE.DERRO_DWARF:
        //        case RACE.MOUNTAIN_DWARF:
        //            switch (Language)
        //            {
        //                case "Giant":
        //                case "Goblin":
        //                case "Orc":
        //                case "Gnome":
        //                case "Terran":
        //                case "Undercommon": return true;
        //                default: return false;
        //            }
        //        case RACE.DOPPELGANGER:
        //            switch (Language)
        //            {
        //                case "Auran":
        //                case "Dwarven":
        //                case "Elven":
        //                case "Gnome":
        //                case "Giant":
        //                case "Halfling":
        //                case "Terran": return true;
        //                default: return false;
        //            }
        //        case RACE.DROW:
        //            switch (Language)
        //            {
        //                case "Abyssal":
        //                case "Aquan":
        //                case "Draconic":
        //                case "Gnome":
        //                case "Goblin": return true;
        //                default: return false;
        //            }
        //        case RACE.DUERGAR:
        //            switch (Language)
        //            {
        //                case "Giant":
        //                case "Goblin":
        //                case "Orc":
        //                case "Draconic":
        //                case "Terran": return true;
        //                default: return false;
        //            }
        //        case RACE.GNOLL:
        //            switch (Language)
        //            {
        //                case "Common":
        //                case "Goblin":
        //                case "Orc":
        //                case "Draconic":
        //                case "Elven": return true;
        //                default: return false;
        //            }
        //        case RACE.MIND_FLAYER:
        //        case RACE.HUMAN:
        //        case RACE.HALFELF: return true;
        //        case RACE.HALFORC:
        //            switch (Language)
        //            {
        //                case "Abyssal":
        //                case "Goblin":
        //                case "Gnoll":
        //                case "Draconic":
        //                case "Giant": return true;
        //                default: return false;
        //            }
        //        case RACE.HIGH_ELF:
        //        case RACE.GRAY_ELF:
        //        case RACE.WILD_ELF:
        //        case RACE.WOOD_ELF:
        //            switch (Language)
        //            {
        //                case "Draconic":
        //                case "Gnoll":
        //                case "Goblin":
        //                case "Gnome":
        //                case "Orc":
        //                case "Sylvan": return true;
        //                default: return false;
        //            }
        //        case RACE.HOBGOBLIN:
        //            switch (Language)
        //            {
        //                case "Dwarven":
        //                case "Infernal":
        //                case "Orc":
        //                case "Draconic":
        //                case "Giant": return true;
        //                default: return false;
        //            }
        //        case RACE.KOBOLD:
        //            switch (Language)
        //            {
        //                case "Common":
        //                case "Undercommon":return true;
        //                default: return false;
        //            }
        //        case RACE.LIGHTFOOT_HALFLING:
        //        case RACE.TALLFELLOW_HALFLING:
        //        case RACE.DEEP_HALFLING:
        //            switch (Language)
        //            {
        //                case "Dwarven":
        //                case "Elven":
        //                case "Gnome":
        //                case "Goblin":
        //                case "Orc": return true;
        //                default: return false;
        //            }
        //        case RACE.LIZARDFOLK:
        //            switch (Language)
        //            {
        //                case "Aquan":
        //                case "Goblin":
        //                case "Orc":
        //                case "Gnoll": return true;
        //                default: return false;
        //            }
        //        case RACE.MINOTAUR:
        //            switch (Language)
        //            {
        //                case "Terran":
        //                case "Goblin":
        //                case "Orc": return true;
        //                default: return false;
        //            }
        //        case RACE.OGRE:
        //            switch (Language)
        //            {
        //                case "Dwarven":
        //                case "Goblin":
        //                case "Terran":
        //                case "Orc": return true;
        //                default: return false;
        //            }
        //        case RACE.OGRE_MAGE:
        //            switch (Language)
        //            {
        //                case "Dwarven":
        //                case "Goblin":
        //                case "Orc":
        //                case "Infernal": return true;
        //                default: return false;
        //            }
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
        //        case RACE.FOREST_GNOME:
        //        case RACE.ROCK_GNOME:
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
        //        case RACE.TROGLODYTE:
        //            switch (Language)
        //            {
        //                case "Common":
        //                case "Goblin":
        //                case "Orc":
        //                case "Giant": return true;
        //                default: return false;
        //            }
        //        default: return true;
        //    }

            return languages;
        }

        private static String GetBonusLanguage()
        {
            throw new NotImplementedException();
        }
    }
}
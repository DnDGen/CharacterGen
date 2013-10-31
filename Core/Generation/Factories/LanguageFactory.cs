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

            return languages;
        }

        private static String GetBonusLanguage()
        {
            throw new NotImplementedException();
        }
    }
}
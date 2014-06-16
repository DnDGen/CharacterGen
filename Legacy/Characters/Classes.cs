using NPCGen.Roll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NPCGen.Characters
{
    public enum CLASS { BARBARIAN, BARD, CLERIC, DRUID, FIGHTER, MONK, PALADIN, RANGER, THIEF, SORCERER, WIZARD };
    public enum LEVELRANDOMIZER { ANY, ANY_NONEPIC, LOW_1_5, MEDIUM_6_9, HIGH_10_15, VERYHIGH_16_20, EPIC_21ORMORE };
    public enum CLASSRANDOMIZER { ANY, ANY_FIGHTER, ANY_SPELLCASTER, ANY_MAGE, ANY_HEALER, ANY_NONSPELLCASTER, ANY_ROGUE };

    public class Classes
    {
        public static String[] ClassesArray { get { return Enum.GetNames(typeof(CLASS)); } }
        public static String[] LevelRandomizerArray { get { return Enum.GetNames(typeof(LEVELRANDOMIZER)); } }
        public static String[] ClassRandomizerArray { get { return Enum.GetNames(typeof(CLASSRANDOMIZER)); } }

        public static Int32 RandomLevel(LEVELRANDOMIZER levelRandomizer)
        {
            switch (levelRandomizer)
            {
                case LEVELRANDOMIZER.ANY_NONEPIC: return Dice.d20();
                case LEVELRANDOMIZER.EPIC_21ORMORE: return Dice.d20(20);
                case LEVELRANDOMIZER.HIGH_10_15: return Dice.d6(9);
                case LEVELRANDOMIZER.LOW_1_5: return Dice.Roll(1, 5);
                case LEVELRANDOMIZER.MEDIUM_6_9: return Dice.d4(5);
                case LEVELRANDOMIZER.VERYHIGH_16_20: return Dice.Roll(1, 5, 15);
                default: return Dice.Roll(1, 40);
            }
        }

        public static CLASS RandomClass(CLASSRANDOMIZER randomizer, ALIGNMENT[] alignment)
        {
            while (true)
            {
                Application.DoEvents();
                switch (alignment[1])
                {
                    case ALIGNMENT.GOOD:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5: if (CheckClass(randomizer, CLASS.BARBARIAN, alignment)) { return CLASS.BARBARIAN; } break;
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 10: if (CheckClass(randomizer, CLASS.BARD, alignment)) { return CLASS.BARD; } break;
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15:
                            case 16:
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                            case 25:
                            case 26:
                            case 27:
                            case 28:
                            case 29:
                            case 30: if (CheckClass(randomizer, CLASS.CLERIC, alignment)) { return CLASS.CLERIC; } break;
                            case 31:
                            case 32:
                            case 33:
                            case 34:
                            case 35: if (CheckClass(randomizer, CLASS.DRUID, alignment)) { return CLASS.DRUID; } break;
                            case 36:
                            case 37:
                            case 38:
                            case 39:
                            case 40:
                            case 41:
                            case 42:
                            case 43:
                            case 44:
                            case 45: if (CheckClass(randomizer, CLASS.FIGHTER, alignment)) { return CLASS.FIGHTER; } break;
                            case 46:
                            case 47:
                            case 48:
                            case 49:
                            case 50: if (CheckClass(randomizer, CLASS.MONK, alignment)) { return CLASS.MONK; } break;
                            case 51:
                            case 52:
                            case 53:
                            case 54:
                            case 55: if (CheckClass(randomizer, CLASS.PALADIN, alignment)) { return CLASS.PALADIN; } break;
                            case 56:
                            case 57:
                            case 58:
                            case 59:
                            case 60:
                            case 61:
                            case 62:
                            case 63:
                            case 64:
                            case 65: if (CheckClass(randomizer, CLASS.RANGER, alignment)) { return CLASS.RANGER; } break;
                            case 66:
                            case 67:
                            case 68:
                            case 69:
                            case 70:
                            case 71:
                            case 72:
                            case 73:
                            case 74:
                            case 75: if (CheckClass(randomizer, CLASS.THIEF, alignment)) { return CLASS.THIEF; } break;
                            case 76:
                            case 77:
                            case 78:
                            case 79:
                            case 80: if (CheckClass(randomizer, CLASS.SORCERER, alignment)) { return CLASS.SORCERER; } break;
                            case 81:
                            case 82:
                            case 83:
                            case 84:
                            case 85:
                            case 86:
                            case 87:
                            case 88:
                            case 89:
                            case 90:
                            case 91:
                            case 92:
                            case 93:
                            case 94:
                            case 95:
                            case 96:
                            case 97:
                            case 98:
                            case 99:
                            case 100: if (CheckClass(randomizer, CLASS.WIZARD, alignment)) { return CLASS.WIZARD; } break;
                            default: return CLASS.FIGHTER;
                        }
                        break;
                    case ALIGNMENT.NEUTRAL:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5: if (CheckClass(randomizer, CLASS.BARBARIAN, alignment)) { return CLASS.BARBARIAN; } break;
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 10: if (CheckClass(randomizer, CLASS.BARD, alignment)) { return CLASS.BARD; } break;
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15: if (CheckClass(randomizer, CLASS.CLERIC, alignment)) { return CLASS.CLERIC; } break;
                            case 16:
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                            case 25: if (CheckClass(randomizer, CLASS.DRUID, alignment)) { return CLASS.DRUID; } break;
                            case 26:
                            case 27:
                            case 28:
                            case 29:
                            case 30:
                            case 31:
                            case 32:
                            case 33:
                            case 34:
                            case 35:
                            case 36:
                            case 37:
                            case 38:
                            case 39:
                            case 40:
                            case 41:
                            case 42:
                            case 43:
                            case 44:
                            case 45: if (CheckClass(randomizer, CLASS.FIGHTER, alignment)) { return CLASS.FIGHTER; } break;
                            case 46:
                            case 47:
                            case 48:
                            case 49:
                            case 50: if (CheckClass(randomizer, CLASS.MONK, alignment)) { return CLASS.MONK; } break;
                            case 51: if (CheckClass(randomizer, CLASS.PALADIN, alignment)) { return CLASS.PALADIN; } break;
                            case 52:
                            case 53:
                            case 54:
                            case 55: if (CheckClass(randomizer, CLASS.RANGER, alignment)) { return CLASS.RANGER; } break;
                            case 56:
                            case 57:
                            case 58:
                            case 59:
                            case 60:
                            case 61:
                            case 62:
                            case 63:
                            case 64:
                            case 65:
                            case 66:
                            case 67:
                            case 68:
                            case 69:
                            case 70:
                            case 71:
                            case 72:
                            case 73:
                            case 74:
                            case 75: if (CheckClass(randomizer, CLASS.THIEF, alignment)) { return CLASS.THIEF; } break;
                            case 76:
                            case 77:
                            case 78:
                            case 79:
                            case 80: if (CheckClass(randomizer, CLASS.SORCERER, alignment)) { return CLASS.SORCERER; } break;
                            case 81:
                            case 82:
                            case 83:
                            case 84:
                            case 85:
                            case 86:
                            case 87:
                            case 88:
                            case 89:
                            case 90:
                            case 91:
                            case 92:
                            case 93:
                            case 94:
                            case 95:
                            case 96:
                            case 97:
                            case 98:
                            case 99:
                            case 100: if (CheckClass(randomizer, CLASS.WIZARD, alignment)) { return CLASS.WIZARD; } break;
                            default: return CLASS.FIGHTER;
                        }
                        break;
                    case ALIGNMENT.EVIL:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 10: if (CheckClass(randomizer, CLASS.BARBARIAN, alignment)) { return CLASS.BARBARIAN; } break;
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15: if (CheckClass(randomizer, CLASS.BARD, alignment)) { return CLASS.BARD; } break;
                            case 16:
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                            case 25:
                            case 26:
                            case 27:
                            case 28:
                            case 29:
                            case 30:
                            case 31:
                            case 32:
                            case 33:
                            case 34:
                            case 35: if (CheckClass(randomizer, CLASS.CLERIC, alignment)) { return CLASS.CLERIC; } break;
                            case 36:
                            case 37:
                            case 38:
                            case 39:
                            case 40: if (CheckClass(randomizer, CLASS.DRUID, alignment)) { return CLASS.DRUID; } break;
                            case 41:
                            case 42:
                            case 43:
                            case 44:
                            case 45:
                            case 46:
                            case 47:
                            case 48:
                            case 49:
                            case 50: if (CheckClass(randomizer, CLASS.FIGHTER, alignment)) { return CLASS.FIGHTER; } break;
                            case 51:
                            case 52:
                            case 53:
                            case 54:
                            case 55: if (CheckClass(randomizer, CLASS.MONK, alignment)) { return CLASS.MONK; } break;
                            case 56: if (CheckClass(randomizer, CLASS.PALADIN, alignment)) { return CLASS.PALADIN; } break;
                            case 57:
                            case 58:
                            case 59:
                            case 60: if (CheckClass(randomizer, CLASS.RANGER, alignment)) { return CLASS.RANGER; } break;
                            case 61:
                            case 62:
                            case 63:
                            case 64:
                            case 65:
                            case 66:
                            case 67:
                            case 68:
                            case 69:
                            case 70:
                            case 71:
                            case 72:
                            case 73:
                            case 74:
                            case 75:
                            case 76:
                            case 77:
                            case 78:
                            case 79:
                            case 80: if (CheckClass(randomizer, CLASS.THIEF, alignment)) { return CLASS.THIEF; } break;
                            case 81:
                            case 82:
                            case 83:
                            case 84:
                            case 85: if (CheckClass(randomizer, CLASS.SORCERER, alignment)) { return CLASS.SORCERER; } break;
                            case 86:
                            case 87:
                            case 88:
                            case 89:
                            case 90:
                            case 91:
                            case 92:
                            case 93:
                            case 94:
                            case 95:
                            case 96:
                            case 97:
                            case 98:
                            case 99:
                            case 100: if (CheckClass(randomizer, CLASS.WIZARD, alignment)) { return CLASS.WIZARD; } break;
                            default: return CLASS.FIGHTER;
                        }
                        break;
                    default: return (CLASS)Dice.Roll(0, ClassesArray.Count());
                }
            }
        }

        private static Boolean CheckClass(CLASSRANDOMIZER randomizer, CLASS charClass, ALIGNMENT[] alignment)
        {
            switch (charClass)
            {
                case CLASS.BARBARIAN:
                    if (alignment[0] != ALIGNMENT.LAWFUL)
                    {
                        switch (randomizer)
                        {
                            case CLASSRANDOMIZER.ANY_HEALER:
                            case CLASSRANDOMIZER.ANY_MAGE:
                            case CLASSRANDOMIZER.ANY_ROGUE:
                            case CLASSRANDOMIZER.ANY_SPELLCASTER: return false;
                            default: return true;
                        }
                    }
                    return false;
                case CLASS.BARD:
                    if (alignment[0] != ALIGNMENT.LAWFUL)
                    {
                        switch (randomizer)
                        {
                            case CLASSRANDOMIZER.ANY_FIGHTER:
                            case CLASSRANDOMIZER.ANY_NONSPELLCASTER: return false;
                            default: return true;
                        }
                    }
                    return false;
                case CLASS.CLERIC:
                    switch (randomizer)
                    {
                        case CLASSRANDOMIZER.ANY_FIGHTER:
                        case CLASSRANDOMIZER.ANY_MAGE:
                        case CLASSRANDOMIZER.ANY_ROGUE:
                        case CLASSRANDOMIZER.ANY_NONSPELLCASTER: return false;
                        default: return true;
                    }
                case CLASS.DRUID:
                    if (alignment.Any(x => x == ALIGNMENT.NEUTRAL))
                    {
                        switch (randomizer)
                        {
                            case CLASSRANDOMIZER.ANY_FIGHTER:
                            case CLASSRANDOMIZER.ANY_MAGE:
                            case CLASSRANDOMIZER.ANY_ROGUE:
                            case CLASSRANDOMIZER.ANY_NONSPELLCASTER: return false;
                            default: return true;
                        }
                    }
                    return false;
                case CLASS.FIGHTER:
                    switch (randomizer)
                    {
                        case CLASSRANDOMIZER.ANY_HEALER:
                        case CLASSRANDOMIZER.ANY_MAGE:
                        case CLASSRANDOMIZER.ANY_ROGUE:
                        case CLASSRANDOMIZER.ANY_SPELLCASTER: return false;
                        default: return true;
                    }
                case CLASS.MONK:
                    if (alignment[0] == ALIGNMENT.LAWFUL)
                    {
                        switch (randomizer)
                        {
                            case CLASSRANDOMIZER.ANY_HEALER:
                            case CLASSRANDOMIZER.ANY_MAGE:
                            case CLASSRANDOMIZER.ANY_ROGUE:
                            case CLASSRANDOMIZER.ANY_SPELLCASTER: return false;
                            default: return true;
                        }
                    }
                    return false;
                case CLASS.PALADIN:
                    if (alignment[0] == ALIGNMENT.LAWFUL)
                    {
                        switch (randomizer)
                        {
                            case CLASSRANDOMIZER.ANY_MAGE:
                            case CLASSRANDOMIZER.ANY_ROGUE:
                            case CLASSRANDOMIZER.ANY_NONSPELLCASTER: return false;
                            default: return true;
                        }
                    }
                    return false;
                case CLASS.RANGER:
                    switch (randomizer)
                    {
                        case CLASSRANDOMIZER.ANY_HEALER:
                        case CLASSRANDOMIZER.ANY_MAGE:
                        case CLASSRANDOMIZER.ANY_ROGUE:
                        case CLASSRANDOMIZER.ANY_NONSPELLCASTER: return false;
                        default: return true;
                    }
                case CLASS.SORCERER:
                    switch (randomizer)
                    {
                        case CLASSRANDOMIZER.ANY_FIGHTER:
                        case CLASSRANDOMIZER.ANY_HEALER:
                        case CLASSRANDOMIZER.ANY_ROGUE:
                        case CLASSRANDOMIZER.ANY_NONSPELLCASTER: return false;
                        default: return true;
                    }
                case CLASS.THIEF:
                    switch (randomizer)
                    {
                        case CLASSRANDOMIZER.ANY_FIGHTER:
                        case CLASSRANDOMIZER.ANY_HEALER:
                        case CLASSRANDOMIZER.ANY_MAGE:
                        case CLASSRANDOMIZER.ANY_SPELLCASTER: return false;
                        default: return true;
                    }
                case CLASS.WIZARD:
                    switch (randomizer)
                    {
                        case CLASSRANDOMIZER.ANY_FIGHTER:
                        case CLASSRANDOMIZER.ANY_HEALER:
                        case CLASSRANDOMIZER.ANY_ROGUE:
                        case CLASSRANDOMIZER.ANY_NONSPELLCASTER: return false;
                        default: return true;
                    }
                default: return false;
            }
        }

        public static Int32[] Prioritize(CLASS charClass, Int32[] stats)
        {
            switch (charClass)
            {
                case CLASS.BARBARIAN: return Prioritize(stats, Stats.Strength, Stats.Dexterity);
                case CLASS.BARD: return Prioritize(stats, Stats.Charisma, Stats.Intelligence);
                case CLASS.CLERIC: return Prioritize(stats, Stats.Wisdom, Stats.Charisma);
                case CLASS.DRUID: return Prioritize(stats, Stats.Wisdom, Stats.Dexterity);
                case CLASS.FIGHTER: return Prioritize(stats, Stats.Strength, Stats.Constitution);
                case CLASS.MONK: return Prioritize(stats, Stats.Wisdom, Stats.Strength);
                case CLASS.PALADIN: return Prioritize(stats, Stats.Charisma, Stats.Strength);
                case CLASS.RANGER: return Prioritize(stats, Stats.Strength, Stats.Wisdom);
                case CLASS.THIEF: return Prioritize(stats, Stats.Dexterity, Stats.Intelligence);
                case CLASS.SORCERER: return Prioritize(stats, Stats.Charisma, Stats.Dexterity);
                case CLASS.WIZARD: return Prioritize(stats, Stats.Intelligence, Stats.Dexterity);
                default: return stats;
            }
        }

        private static Int32[] Prioritize(Int32[] stats, Int32 firstPriority, Int32 secondPriority)
        {
            var index = FindIndexOfMax(stats);

            if (index != firstPriority)
            {
                var temp = stats[index];
                stats[index] = stats[firstPriority];
                stats[firstPriority] = temp;
            }

            index = FindIndexOfMax(stats, firstPriority);

            if (index != secondPriority)
            {
                var temp = stats[index];
                stats[index] = stats[secondPriority];
                stats[secondPriority] = temp;
            }

            return stats;
        }

        private static Int32 FindIndexOfMax(Int32[] input, Int32 SkipIndex = -1)
        {
            var MaxIndex = 0;

            if (SkipIndex == 0)
                MaxIndex = 1;

            for (var i = 1; i < input.Length; i++)
                if (input[i] > input[MaxIndex] && i != SkipIndex)
                    MaxIndex = i;

            return MaxIndex;
        }

        public static Int32 HitPoints(CLASS charClass, Int32 level, Int32 constitution, Races race)
        {
            var hitDie = GetHitDie(charClass, race.MetaRace);
            var constitutionModifier = GetConstitutionModifier(charClass, constitution);

            level--;
            var HP = hitDie + constitutionModifier;

            while (level-- > 0)
            {
                var temp = Dice.Roll(1, hitDie, constitutionModifier);
                if (temp < 1)
                    temp = 1;
                HP += temp;
            }

            HP += RacialBonusHP(race.Race);

            return HP;
        }

        private static Int32 GetConstitutionModifier(CLASS charClass, Int32 constitution)
        {
            switch (constitution)
            {
                case 1: return -3;
                case 2:
                case 3: return -2;
                case 4:
                case 5:
                case 6: return -1;
                case 15: return 1;
                case 16: return 2;
                case 17:
                    if (IsFighter(charClass))
                        return 3;
                    return 2;
                case 18:
                    if (IsFighter(charClass))
                        return 4;
                    return 2;
                case 19:
                    if (IsFighter(charClass))
                        return 5;
                    return 2;
                default: return 0;
            }
        }

        private static Boolean IsFighter(CLASS charClass)
        {
            return charClass == CLASS.BARBARIAN || charClass == CLASS.FIGHTER || charClass == CLASS.MONK || charClass == CLASS.PALADIN || charClass == CLASS.RANGER;
        }

        private static Int32 RacialBonusHP(RACE race)
        {
            switch (race)
            {
                case RACE.BUGBEAR: return Dice.Roll(3, 8);
                case RACE.LIZARDFOLK:
                case RACE.TROGLODYTE:
                case RACE.GNOLL: return Dice.Roll(2, 8);
                case RACE.MIND_FLAYER: return Dice.Roll(8, 8);
                case RACE.MINOTAUR: return Dice.Roll(6, 8);
                case RACE.OGRE: return Dice.Roll(4, 8);
                case RACE.OGRE_MAGE: return Dice.Roll(5, 8);
                default: return 0;
            }
        }

        private static Int32 GetHitDie(CLASS charClass, METARACE metarace)
        {
            var hd = 0;

            switch (charClass)
            {
                case CLASS.BARBARIAN: hd = 12; break;
                case CLASS.FIGHTER:
                case CLASS.PALADIN:
                case CLASS.RANGER: hd = 10; break;
                case CLASS.CLERIC:
                case CLASS.DRUID:
                case CLASS.MONK: hd = 8; break;
                case CLASS.BARD:
                case CLASS.THIEF: hd = 6; break;
                case CLASS.SORCERER:
                case CLASS.WIZARD: hd = 4; break;
                default: return 0;
            }

            if (metarace == METARACE.HALF_DRAGON)
            {
                switch (hd)
                {
                    case 4: hd = 6; break;
                    case 6: hd = 8; break;
                    case 8: hd = 10; break;
                    case 10: hd = 12; break;
                    default: break;
                }
            }

            return hd;
        }

        public static IEnumerable<String> BardWeapons
        {
            get
            {
                var output = new List<String>();

                output.Add("longbow");
                output.Add("composite longbow");
                output.Add("longsword");
                output.Add("rapier");
                output.Add("sap");
                output.Add("composite shortbow");
                output.Add("short sword");
                output.Add("shortbow");
                output.Add("whip");
                output.Add("bow");

                return output;
            }
        }

        public static IEnumerable<String> DruidWeapons
        {
            get
            {
                var output = new List<String>();

                output.Add("club");
                output.Add("dagger");
                output.Add("dart");
                output.Add("longspear");
                output.Add("quarterstaff");
                output.Add("scimitar");
                output.Add("sickle");
                output.Add("shortspear");
                output.Add("sling");

                return output;
            }
        }

        public static IEnumerable<String> MonkWeapons
        {
            get
            {
                var output = new List<String>();

                output.Add("club");
                output.Add("light crossbow");
                output.Add("heavy crossbow");
                output.Add("dagger");
                output.Add("handaxe");
                output.Add("javelin");
                output.Add("kama");
                output.Add("nunchaku");
                output.Add("quarterstaff");
                output.Add("shuriken");
                output.Add("siangham");
                output.Add("sling");
                output.Add("crossbow");

                return output;
            }
        }

        public static IEnumerable<String> ThiefWeapons
        {
            get
            {
                var output = new List<String>();

                output.Add("light crossbow");
                output.Add("hand crossbow");
                output.Add("dagger");
                output.Add("punching dagger");
                output.Add("dart");
                output.Add("light mace");
                output.Add("sap");
                output.Add("shortbow");
                output.Add("composite shortbow");
                output.Add("short sword");
                output.Add("club");
                output.Add("heavy crossbow");
                output.Add("heavy mace");
                output.Add("morningstar");
                output.Add("qurterstaff");
                output.Add("rapier");
                output.Add("bow");
                output.Add("crossbow");

                return output;
            }
        }

        public static IEnumerable<String> WizardWeapons
        {
            get
            {
                var output = new List<String>();

                output.Add("club");
                output.Add("dagger");
                output.Add("light crossbow");
                output.Add("heavy crossbow");
                output.Add("quarterstaff");
                output.Add("crossbow");

                return output;
            }
        }

        public static Int32 MonkDamage(Int32 level)
        {
            switch (level)
            {
                case 1:
                case 2:
                case 3: return 6;
                case 4:
                case 5:
                case 6:
                case 7: return 8;
                case 8:
                case 9:
                case 10:
                case 11: return 10;
                case 12:
                case 13:
                case 14:
                case 15: return 12;
                default: return 20;
            }
        }

        public static String Familiar(CLASS charClass, Int32 level)
        {
            var animal = String.Empty;

            switch (charClass)
            {
                case CLASS.DRUID:
                    while (animal == String.Empty)
                    {
                        switch (Dice.Roll(1, 50, 0))
                        {
                            case 1: animal = "Badger"; break;
                            case 2: animal = "Camel"; break;
                            case 3: animal = "Dire Rat"; break;
                            case 4:
                            case 5: animal = "Dog"; break;
                            case 6: animal = "Riding Dog"; break;
                            case 7: animal = "Eagle"; break;
                            case 8:
                            case 9: animal = "Hawk"; break;
                            case 50:
                            case 10: animal = "Owl"; break;
                            case 11: animal = "Light Horse"; break;
                            case 12: animal = "Heavy Horse"; break;
                            case 13: animal = "Pony"; break;
                            case 14: animal = "Small Viper"; break;
                            case 15: animal = "Medium Viper"; break;
                            case 16:
                            case 17: animal = "Wolf"; break;
                            case 18:
                                if (level > 3)
                                {
                                    animal = "Ape";
                                    level -= 3;
                                }
                                break;
                            case 19:
                                if (level > 3)
                                {
                                    animal = "Black Bear";
                                    level -= 3;
                                }
                                break;
                            case 20:
                                if (level > 3)
                                {
                                    animal = "Bison";
                                    level -= 3;
                                }
                                break;
                            case 21:
                                if (level > 3)
                                {
                                    animal = "Boar";
                                    level -= 3;
                                }
                                break;
                            case 22:
                                if (level > 3)
                                {
                                    animal = "Cheetah";
                                    level -= 3;
                                }
                                break;
                            case 23:
                                if (level > 3)
                                {
                                    animal = "Dire Badger";
                                    level -= 3;
                                }
                                break;
                            case 24:
                                if (level > 3)
                                {
                                    animal = "Dire Weasel";
                                    level -= 3;
                                }
                                break;
                            case 25:
                                if (level > 3)
                                {
                                    animal = "Leopard";
                                    level -= 3;
                                }
                                break;
                            case 26:
                                if (level > 3)
                                {
                                    animal = "Monitor Lizard";
                                    level -= 3;
                                }
                                break;
                            case 27:
                                if (level > 3)
                                {
                                    animal = "Constrictor Snake";
                                    level -= 3;
                                }
                                break;
                            case 28:
                                if (level > 3)
                                {
                                    animal = "Large Viper";
                                    level -= 3;
                                }
                                break;
                            case 29:
                                if (level > 3)
                                {
                                    animal = "Wolverine";
                                    level -= 3;
                                }
                                break;
                            case 30:
                                if (level > 6)
                                {
                                    animal = "Brown Bear";
                                    level -= 6;
                                }
                                break;
                            case 31:
                                if (level > 6)
                                {
                                    animal = "Dire Wolverine";
                                    level -= 6;
                                }
                                break;
                            case 32:
                                if (level > 6)
                                {
                                    animal = "Giant Crocodile";
                                    level -= 6;
                                }
                                break;
                            case 33:
                                if (level > 6)
                                {
                                    animal = "Deinonychus";
                                    level -= 6;
                                }
                                break;
                            case 34:
                                if (level > 6)
                                {
                                    animal = "Dire Ape";
                                    level -= 6;
                                }
                                break;
                            case 35:
                                if (level > 6)
                                {
                                    animal = "Dire Boar";
                                    level -= 6;
                                }
                                break;
                            case 36:
                                if (level > 6)
                                {
                                    animal = "Dire Wolf";
                                    level -= 6;
                                }
                                break;
                            case 37:
                                if (level > 6)
                                {
                                    animal = "Lion";
                                    level -= 6;
                                }
                                break;
                            case 38:
                                if (level > 6)
                                {
                                    animal = "Rhinoceras";
                                    level -= 6;
                                }
                                break;
                            case 39:
                                if (level > 6)
                                {
                                    animal = "Huge Viper";
                                    level -= 6;
                                }
                                break;
                            case 40:
                                if (level > 6)
                                {
                                    animal = "Tiger";
                                    level -= 6;
                                }
                                break;
                            case 41:
                                if (level > 9)
                                {
                                    animal = "Polar Bear";
                                    level -= 9;
                                }
                                break;
                            case 42:
                                if (level > 9)
                                {
                                    animal = "Dire Lion";
                                    level -= 9;
                                }
                                break;
                            case 43:
                                if (level > 9)
                                {
                                    animal = "Megaraptor";
                                    level -= 9;
                                }
                                break;
                            case 44:
                                if (level > 9)
                                {
                                    animal = "Giant Constrictor Snake";
                                    level -= 9;
                                }
                                break;
                            case 45:
                                if (level > 12)
                                {
                                    animal = "Dire Bear";
                                    level -= 12;
                                }
                                break;
                            case 46:
                                if (level > 12)
                                {
                                    animal = "Elephant";
                                    level -= 12;
                                }
                                break;
                            case 47:
                                if (level > 15)
                                {
                                    animal = "Dire Tiger";
                                    level -= 15;
                                }
                                break;
                            case 48:
                                if (level > 15)
                                {
                                    animal = "Triceratops";
                                    level -= 15;
                                }
                                break;
                            case 49:
                                if (level > 15)
                                {
                                    animal = "Tyrannosaurus";
                                    level -= 15;
                                }
                                break;
                            default: return "[ERROR: Familiar out of range.  Classes.1036]";
                        }
                    }
                    return String.Format("{0}: {1}", animal, AnimalCompanionStats(level));
                case CLASS.RANGER:
                    if (level > 3)
                        return Familiar(CLASS.DRUID, level / 2);
                    return String.Empty;
                case CLASS.SORCERER:
                case CLASS.WIZARD:
                    switch (Dice.d20())
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5: return "Black Cat: Excellent night vision & superior hearing";
                        case 6:
                        case 7: return "Crow: Excellent vision";
                        case 8:
                        case 9: return "Hawk: Very superior distance vision";
                        case 10:
                        case 11: return "Owl: Night vision equals human daylight vision, superior hearing";
                        case 12:
                        case 13: return "Toad: wide-angle vision";
                        case 14:
                        case 15: return "Weasel: Superior hearing & very superior olGenerator power";
                        case 16:
                        case 17: 
                        case 18: 
                        case 19:
                        case 20: return String.Empty;
                        default: return "[ERROR: Out of range.  Classes.1068]";
                    }
                case CLASS.PALADIN:
                    switch (level)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4: return String.Empty;
                        case 5:
                        case 6:
                        case 7: return "Warhorse, +2 HD, +4 AC, +1 STR, Intelligence of 6, Improved Evasion, Shared Spells, Empathic Link, Share Saving Throws";
                        case 8:
                        case 9:
                        case 10: return "Warhorse, +4 HD, +6 AC, +2 STR, Intelligence of 7, Improved Evasion, Shared Spells, Empathic Link, Share Saving Throws";
                        case 11:
                        case 12:
                        case 13:
                        case 14: return "Warhorse, +6 HD, +8 AC, +3 STR, Intelligence of 8, Improved Evasion, Shared Spells, Empathic Link, Share Saving Throws, Command creatures of its kind";
                        default: return "Warhorse, +8 HD, +10 AC, +4 STR, Intelligence of 9, Improved Evasion, Shared Spells, Empathic Link, Share Saving Throws, Command creatures of its kind, Spell resistance";
                    }
                default: return String.Empty;
            }
        }

        private static String AnimalCompanionStats(Int32 level)
        {
            switch (level)
            {
                case 1:
                case 2: return "1 Bonus Trick, Link, Share spells";
                case 3:
                case 4:
                case 5: return "+2 HD, +2 AC, +1 Str/Dex, 2 Bonus Tricks, Link, Share spells, Evasion";
                case 6:
                case 7:
                case 8: return "+4 HD, +4 AC, +2 Str/Dex, 3 Bonus Tricks, Link, Share spells, Evasion, Devotion";
                case 9:
                case 10:
                case 11: return "+6 HD, +6 AC, +3 Str/Dex, 4 Bonus Tricks, Link, Share spells, Evasion, Devotion, Multiattack";
                case 12:
                case 13:
                case 14: return "+8 HD, +8 AC, +4 Str/Dex, 5 Bonus Tricks, Link, Share spells, Evasion, Devotion, Multiattack";
                case 15:
                case 16:
                case 17: return "+10 HD, +10 AC, +5 Str/Dex, 6 Bonus Tricks, Link, Share spells, Evasion, Devotion, Multiattack, Improved Evasion";
                default: return "+12 HD, +12 AC, +6 Str/Dex, 7 Bonus Tricks, Link, Share spells, Evasion, Devotion, Multiattack, Improved Evasion";
            }
        }

        public static Int32[] Spells(CLASS charClass, Int32 level)
        {
            switch (charClass)
            {
                case CLASS.BARD:
                    switch (level)
                    {
                        case 1: return new Int32[6] { 0, 0, 0, 0, 0, 0 };
                        case 2: return new Int32[6] { 1, 0, 0, 0, 0, 0 };
                        case 3: return new Int32[6] { 2, 0, 0, 0, 0, 0 };
                        case 4: return new Int32[6] { 2, 1, 0, 0, 0, 0 };
                        case 5: return new Int32[6] { 3, 1, 0, 0, 0, 0 };
                        case 6: return new Int32[6] { 3, 2, 0, 0, 0, 0 };
                        case 7: return new Int32[6] { 3, 2, 1, 0, 0, 0 };
                        case 8: return new Int32[6] { 3, 3, 1, 0, 0, 0 };
                        case 9: return new Int32[6] { 3, 3, 2, 0, 0, 0 };
                        case 10: return new Int32[6] { 3, 3, 2, 1, 0, 0 };
                        case 11: return new Int32[6] { 3, 3, 3, 1, 0, 0 };
                        case 12: return new Int32[6] { 3, 3, 3, 2, 0, 0 };
                        case 13: return new Int32[6] { 3, 3, 3, 2, 1, 0 };
                        case 14: return new Int32[6] { 3, 3, 3, 3, 1, 0 };
                        case 15: return new Int32[6] { 3, 3, 3, 3, 2, 0 };
                        case 16: return new Int32[6] { 4, 3, 3, 3, 2, 1 };
                        case 17: return new Int32[6] { 4, 4, 3, 3, 3, 1 };
                        case 18: return new Int32[6] { 4, 4, 4, 3, 3, 2 };
                        case 19: return new Int32[6] { 4, 4, 4, 4, 3, 2 };
                        default: return new Int32[6] { 4, 4, 4, 4, 4, 3 };
                    }
                case CLASS.CLERIC:
                case CLASS.DRUID:
                    switch (level)
                    {
                        case 1: return new Int32[7] { 1, 0, 0, 0, 0, 0, 0 };
                        case 2: return new Int32[7] { 2, 0, 0, 0, 0, 0, 0 };
                        case 3: return new Int32[7] { 2, 1, 0, 0, 0, 0, 0 };
                        case 4: return new Int32[7] { 3, 2, 0, 0, 0, 0, 0 };
                        case 5: return new Int32[7] { 3, 3, 1, 0, 0, 0, 0 };
                        case 6: return new Int32[7] { 3, 3, 2, 0, 0, 0, 0 };
                        case 7: return new Int32[7] { 3, 3, 2, 1, 0, 0, 0 };
                        case 8: return new Int32[7] { 3, 3, 3, 2, 0, 0, 0 };
                        case 9: return new Int32[7] { 4, 4, 3, 2, 1, 0, 0 };
                        case 10: return new Int32[7] { 4, 4, 3, 3, 2, 0, 0 };
                        case 11: return new Int32[7] { 5, 4, 4, 3, 2, 1, 0 };
                        case 12: return new Int32[7] { 6, 5, 5, 3, 2, 2, 0 };
                        case 13: return new Int32[7] { 6, 6, 6, 4, 2, 2, 0 };
                        case 14: return new Int32[7] { 6, 6, 6, 5, 3, 2, 1 };
                        case 15: return new Int32[7] { 6, 6, 6, 6, 4, 2, 1 };
                        case 16: return new Int32[7] { 7, 7, 7, 6, 4, 3, 1 };
                        case 17: return new Int32[7] { 7, 7, 7, 7, 5, 3, 2 };
                        case 18: return new Int32[7] { 8, 8, 8, 8, 6, 4, 2 };
                        case 19: return new Int32[7] { 9, 9, 8, 8, 6, 4, 2 };
                        default: return new Int32[7] { 9, 9, 9, 8, 7, 5, 2 };
                    }
                case CLASS.PALADIN:
                    switch (level)
                    {
                        case 1: 
                        case 2: 
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8: return new Int32[4] { 0, 0, 0, 0 };
                        case 9: return new Int32[4] { 1, 0, 0, 0 };
                        case 10: return new Int32[4] { 2, 0, 0, 0 };
                        case 11: return new Int32[4] { 2, 1, 0, 0 };
                        case 12: return new Int32[4] { 2, 2, 0, 0 };
                        case 13: return new Int32[4] { 2, 2, 1, 0 };
                        case 14: return new Int32[4] { 3, 2, 1, 0 };
                        case 15: return new Int32[4] { 3, 2, 1, 1 };
                        case 16: return new Int32[4] { 3, 3, 2, 1 };
                        case 17:
                        case 18: return new Int32[4] { 3, 3, 3, 1 };
                        case 19: return new Int32[4] { 3, 3, 3, 2 };
                        default: return new Int32[4] { 3, 3, 3, 3 };
                    }
                case CLASS.RANGER:
                    switch (level)
                    {
                        case 1: 
                        case 2: 
                        case 3: 
                        case 4: 
                        case 5: 
                        case 6:
                        case 7: return new Int32[3] { 0, 0, 0 };
                        case 8: return new Int32[3] { 1, 0, 0 };
                        case 9: return new Int32[3] { 2, 0, 0 };
                        case 10: return new Int32[3] { 2, 1, 0 };
                        case 11: return new Int32[3] { 2, 2, 0 };
                        case 12: return new Int32[3] { 2, 2, 1 };
                        case 13: return new Int32[3] { 3, 2, 1 };
                        case 14: return new Int32[3] { 3, 2, 2 };
                        case 15: return new Int32[3] { 3, 3, 2 };
                        default: return new Int32[3] { 3, 3, 3 };
                    }
                case CLASS.SORCERER:
                case CLASS.WIZARD:
                    switch (level)
                    {
                        case 1: return new Int32[9] { 1, 0, 0, 0, 0, 0, 0, 0, 0 };
                        case 2: return new Int32[9] { 2, 0, 0, 0, 0, 0, 0, 0, 0 };
                        case 3: return new Int32[9] { 2, 1, 0, 0, 0, 0, 0, 0, 0 };
                        case 4: return new Int32[9] { 3, 2, 0, 0, 0, 0, 0, 0, 0 };
                        case 5: return new Int32[9] { 4, 2, 1, 0, 0, 0, 0, 0, 0 };
                        case 6: return new Int32[9] { 4, 2, 2, 0, 0, 0, 0, 0, 0 };
                        case 7: return new Int32[9] { 4, 3, 2, 1, 0, 0, 0, 0, 0 };
                        case 8: return new Int32[9] { 4, 3, 3, 2, 0, 0, 0, 0, 0 };
                        case 9: return new Int32[9] { 4, 3, 3, 2, 1, 0, 0, 0, 0 };
                        case 10: return new Int32[9] { 4, 4, 3, 2, 2, 0, 0, 0, 0 };
                        case 11: return new Int32[9] { 4, 4, 4, 3, 3, 0, 0, 0, 0 };
                        case 12: return new Int32[9] { 4, 4, 4, 4, 4, 1, 0, 0, 0 };
                        case 13: return new Int32[9] { 5, 5, 5, 4, 4, 2, 0, 0, 0 };
                        case 14: return new Int32[9] { 5, 5, 5, 4, 4, 2, 1, 0, 0 };
                        case 15: return new Int32[9] { 5, 5, 5, 5, 5, 2, 1, 0, 0 };
                        case 16: return new Int32[9] { 5, 5, 5, 5, 5, 3, 2, 1, 0 };
                        case 17: return new Int32[9] { 5, 5, 5, 5, 5, 3, 3, 2, 0 };
                        case 18: return new Int32[9] { 5, 5, 5, 5, 5, 3, 3, 2, 1 };
                        case 19: return new Int32[9] { 5, 5, 5, 5, 5, 3, 3, 3, 1 };
                        default: return new Int32[9] { 5, 5, 5, 5, 5, 4, 3, 3, 2 };
                    }
                default: return new Int32[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }
        }

        public static String SpellString(CLASS charClass, Int32 level, Int32 intelligence, Int32 wisdom, Int32 charisma)
        {
            var spells = Spells(charClass, level);
            var output = String.Empty;

            if (IsHealer(charClass))
            {
                if (wisdom >= 13)
                    spells[0]++;
                if (wisdom >= 14)
                    spells[0]++;
                if (wisdom >= 15)
                    spells[1]++;
                if (wisdom >= 16)
                    spells[1]++;
                if (wisdom >= 17)
                    spells[2]++;
                if (wisdom >= 18)
                    spells[3]++;
                if (wisdom >= 19)
                {
                    spells[0]++;
                    if (spells.Length >= 4)
                        spells[3]++;
                }
                if (wisdom >= 20)
                {
                    spells[1]++;
                    if (spells.Length >= 4)
                        spells[3]++;
                }
                if (wisdom >= 21)
                {
                    spells[2]++;
                    if (spells.Length >= 5)
                        spells[4]++;
                }
                if (wisdom >= 22)
                {
                    if (spells.Length >= 4)
                    {
                        spells[3]++;
                        spells[4]++;
                    }
                }
                if (wisdom >= 23)
                {
                    if (spells.Length >= 5)
                    {
                        spells[4]++;
                        spells[4]++;
                    }
                }
                if (wisdom >= 24)
                {
                    if (spells.Length >= 6)
                    {
                        spells[5]++;
                        spells[5]++;
                    }
                }
                if (wisdom >= 25)
                {
                    if (spells.Length >= 7)
                    {
                        spells[5]++;
                        spells[6]++;
                    }
                }
            }
            else if (charClass == CLASS.BARD || charClass == CLASS.SORCERER)
            {
                switch (charisma)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8: return String.Empty;
                    case 9:
                        for (var i = 0; i < 4; i++)
                            if (spells[i] > 6)
                                spells[i] = 6;
                        for (var i = 4; i < spells.Length; i++)
                            spells[i] = 0;
                        break;
                    case 10:
                    case 11:
                        for (var i = 0; i < 5; i++)
                            if (spells[i] > 7)
                                spells[i] = 7;
                        for (var i = 5; i < spells.Length; i++)
                            spells[i] = 0;
                        break;
                    case 12:
                        for (var i = 0; i < 6; i++)
                            if (spells[i] > 7)
                                spells[i] = 7;
                        for (var i = 6; i < spells.Length; i++)
                            spells[i] = 0;
                        break;
                    case 13:
                        for (var i = 0; i < 6; i++)
                            if (spells[i] > 9)
                                spells[i] = 9;
                        for (var i = 6; i < spells.Length; i++)
                            spells[i] = 0;
                        break;
                    case 14:
                        if (spells.Length > 6)
                        {
                            for (var i = 0; i < 7; i++)
                                if (spells[i] > 9)
                                    spells[i] = 9;
                            for (var i = 7; i < spells.Length; i++)
                                spells[i] = 0;
                        }
                        break;
                    case 15:
                        if (spells.Length > 6)
                        {
                            for (var i = 0; i < 7; i++)
                                if (spells[i] > 11)
                                    spells[i] = 11;
                            for (var i = 7; i < spells.Length; i++)
                                spells[i] = 0;
                        }
                        break;
                    case 16:
                        if (spells.Length > 6)
                        {
                            for (var i = 0; i < 8; i++)
                                if (spells[i] > 11)
                                    spells[i] = 11;
                            for (var i = 8; i < spells.Length; i++)
                                spells[i] = 0;
                        }
                        break;
                    case 17:
                        if (spells.Length > 6)
                        {
                            for (var i = 0; i < 8; i++)
                                if (spells[i] > 14)
                                    spells[i] = 14;
                            for (var i = 8; i < spells.Length; i++)
                                spells[i] = 0;
                        }
                        break;
                    case 18:
                        if (spells.Length > 6)
                            for (var i = 0; i < 9; i++)
                                if (spells[i] > 18)
                                    spells[i] = 18;
                        break;
                    default: break;
                }
            }
            else if (charClass == CLASS.WIZARD)
            {
                switch (intelligence)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8: return String.Empty;
                    case 9:
                        for (var i = 0; i < 4; i++)
                            if (spells[i] > 6)
                                spells[i] = 6;
                        for (var i = 4; i < spells.Length; i++)
                            spells[i] = 0;
                        break;
                    case 10:
                    case 11:
                        for (var i = 0; i < 5; i++)
                            if (spells[i] > 7)
                                spells[i] = 7;
                        for (var i = 5; i < spells.Length; i++)
                            spells[i] = 0;
                        break;
                    case 12:
                        for (var i = 0; i < 6; i++)
                            if (spells[i] > 7)
                                spells[i] = 7;
                        for (var i = 6; i < spells.Length; i++)
                            spells[i] = 0;
                        break;
                    case 13:
                        for (var i = 0; i < 6; i++)
                            if (spells[i] > 9)
                                spells[i] = 9;
                        for (var i = 6; i < spells.Length; i++)
                            spells[i] = 0;
                        break;
                    case 14:
                        for (var i = 0; i < 7; i++)
                            if (spells[i] > 9)
                                spells[i] = 9;
                        for (var i = 7; i < spells.Length; i++)
                            spells[i] = 0;
                        break;
                    case 15:
                        for (var i = 0; i < 7; i++)
                            if (spells[i] > 11)
                                spells[i] = 11;
                        for (var i = 7; i < spells.Length; i++)
                            spells[i] = 0;
                        break;
                    case 16:
                        for (var i = 0; i < 8; i++)
                            if (spells[i] > 11)
                                spells[i] = 11;
                        for (var i = 8; i < spells.Length; i++)
                            spells[i] = 0;
                        break;
                    case 17:
                        for (var i = 0; i < 8; i++)
                            if (spells[i] > 14)
                                spells[i] = 14;
                        for (var i = 8; i < spells.Length; i++)
                            spells[i] = 0;
                        break;
                    case 18:
                        for (var i = 0; i < 9; i++)
                            if (spells[i] > 18)
                                spells[i] = 18;
                        break;
                    default: break;
                }
            }

            for (var i = 0; i < spells.Length; i++)
                output += spells[i] + " ";

            if (charClass == CLASS.BARD || charClass == CLASS.WIZARD)
            {
                for (var i = 0; i < spells.Length; i++)
                {
                    if (spells[i] > 0)
                        output += "\n";
                    for (var j = 0; j < spells[i]; j++)
                    {
                        if (!output.EndsWith("\n"))
                            output += ", ";
                        output += WizardMemorizedSpell(i + 1);
                    }
                }
            }

            return output;
        }

        private static Boolean IsHealer(CLASS charClass)
        {
            return charClass == CLASS.DRUID || charClass == CLASS.CLERIC || charClass == CLASS.PALADIN || charClass == CLASS.RANGER;
        }

        public static String WizardMemorizedSpell(Int32 spellLevel)
        {
            switch (spellLevel)
            {
                case 1:
                    switch (Dice.Roll(1, 45))
                    {
                        case 1: return "Affect Normal Fires";
                        case 2: return "Alarm";
                        case 3: return "Armor";
                        case 4: return "Audible Glamer";
                        case 5: return "Burning Hands";
                        case 6: return "Cantrip";
                        case 7: return "Change Self";
                        case 8: return "Charm Person";
                        case 9: return "Chill Touch";
                        case 10: return "Color Spray";
                        case 11: return "Comprehend Languages";
                        case 12: return "Dancing Lights";
                        case 13: return "Detect Magic";
                        case 14: return "Detect Undead";
                        case 15: return "Enlarge";
                        case 16: return "Erase";
                        case 17: return "Feather Fall";
                        case 18: return "Find Familiar";
                        case 19: return "Friends";
                        case 20: return "Gaze Reflection";
                        case 21: return "Grease";
                        case 22: return "Hold Portal";
                        case 23: return "Hypnotism";
                        case 24: return "Identify";
                        case 25: return "Jump";
                        case 26: return "Light";
                        case 27: return "Magic Missile";
                        case 28: return "Mending";
                        case 29: return "Message";
                        case 30: return "Mount";
                        case 31: return "Nystul's Magical Aura";
                        case 32: return "Phantasmal Force";
                        case 33: return "Protection From Evil";
                        case 34: return "Read Magic";
                        case 35: return "Shield";
                        case 36: return "Shocking Grasp";
                        case 37: return "Sleep";
                        case 38: return "Spider Climb";
                        case 39: return "Spook";
                        case 40: return "Taunt";
                        case 41: return "Tenser's Floating Disc";
                        case 42: return "Unseen Servant";
                        case 43: return "Ventriloquism";
                        case 44: return "Wall of Fog";
                        case 45: return "Wizard Mark";
                        default: return "[ERROR: Level 1 memorized spell out of range.  Classes.1321]";
                    }
                case 2:
                    switch (Dice.Roll(1, 43))
                    {
                        case 1: return "Alter Self";
                        case 2: return "Bind";
                        case 3: return "Blindness";
                        case 4: return "Blur";
                        case 5: return "Continual Light";
                        case 6: return "Darkness 15' Radius";
                        case 7: return "Deafness";
                        case 8: return "Deeppockets";
                        case 9: return "Detect Evil";
                        case 10: return "Detect Invisibility";
                        case 11: return "ESP";
                        case 12: return "Flaming Sphere";
                        case 13: return "Fog Cloud";
                        case 14: return "Fool's Gold";
                        case 15: return "Forget";
                        case 16: return "Glitterdust";
                        case 17: return "Hypnotic Pattern";
                        case 18: return "Improved Phantasmal Force";
                        case 19: return "Invisibility";
                        case 20: return "Irritation";
                        case 21: return "Knock";
                        case 22: return "Know Alignment";
                        case 23: return "Leomund's Trap";
                        case 24: return "Levitate";
                        case 25: return "Locate Object";
                        case 26: return "Magic Mouth";
                        case 27: return "Mef's Acid Arrow";
                        case 28: return "Mirror Image";
                        case 29: return "Misdirection";
                        case 30: return "Protection From Cantrips";
                        case 31: return "Pyrotechnics";
                        case 32: return "Ray of Enfeeblement";
                        case 33: return "Rope Trick";
                        case 34: return "Scare";
                        case 35: return "Shatter";
                        case 36: return "Spectral Hand";
                        case 37: return "Stinking Cloud";
                        case 38: return "Strength";
                        case 39: return "Summon Swarm";
                        case 40: return "Tasha's Uncontrollable Hideous Laughter";
                        case 41: return "Web";
                        case 42: return "Whispering Wind";
                        case 43: return "Wizard Lock";
                        default: return "[ERROR: Level 2 memorized spell out of range.  Classes.1369]";
                    }
                case 3:
                    switch (Dice.Roll(1, 36))
                    {
                        case 1: return "Blink";
                        case 2: return "Clairaudience";
                        case 3: return "Clairvoyance";
                        case 4: return "Delude";
                        case 5: return "Dispel Magic";
                        case 6: return "Explosive Runes";
                        case 7: return "Feign Death";
                        case 8: return "Fireball";
                        case 9: return "Flame Arrow";
                        case 10: return "Fly";
                        case 11: return "Gust of Wind";
                        case 12: return "Haste";
                        case 13: return "Hold Person";
                        case 14: return "Hold Undead";
                        case 15: return "Illusionary Script";
                        case 16: return "Infravision";
                        case 17: return "Invisibility 10' Radius";
                        case 18: return "Item";
                        case 19: return "Leomund's Tiny Hut";
                        case 20: return "Lightning Bolt";
                        case 21: return "Melf's Minute Meteors";
                        case 22: return "Monster Summoning I";
                        case 23: return "Non-Detection";
                        case 24: return "Phantom Steed";
                        case 25: return "Protection From Evil 10' Radius";
                        case 26: return "Protection From Normal Missiles";
                        case 27: return "Secret Page";
                        case 28: return "Sepia Snake Sigil";
                        case 29: return "Slow";
                        case 30: return "Spectral Force";
                        case 31: return "Suggestion";
                        case 32: return "Tongues";
                        case 33: return "Vampiric Touch";
                        case 34: return "Water Breathing";
                        case 35: return "Wind Wall";
                        case 36: return "Wraithform";
                        default: return "[ERROR: Level 3 memorized spell out of range.  Classes.1410]";
                    }
                case 4:
                    switch (Dice.Roll(1, 42))
                    {
                        case 1: return "Charm Monster";
                        case 2: return "Confusion";
                        case 3: return "Contagion";
                        case 4: return "Detect Scrying";
                        case 5: return "Dig";
                        case 6: return "Dimension Door";
                        case 7: return "Emotion";
                        case 8: return "Enchanted Weapon";
                        case 9: return "Enervation";
                        case 10: return "Evard's Black Tentacles";
                        case 11: return "Extension I";
                        case 12: return "Fear";
                        case 13: return "Fire Charm";
                        case 14: return "Fire Shield";
                        case 15: return "Fire Trap";
                        case 16: return "Fumble";
                        case 17: return "Hallucinatory Terrain";
                        case 18: return "Ice Storm";
                        case 19: return "Illusionary Wall";
                        case 20: return "Improved Invisibility";
                        case 21: return "Leomund's Secure Shelter";
                        case 22: return "Magic Mirror";
                        case 23: return "Massmorph";
                        case 24: return "Minor Creation";
                        case 25: return "Minor Globe of Invulnerability";
                        case 26: return "Monster Summoning II";
                        case 27: return "Otiluke's Resilient Sphere";
                        case 28: return "Phantasmal Killer";
                        case 29: return "Plant Growth";
                        case 30: return "Polymorph Other";
                        case 31: return "Polymorph Self";
                        case 32: return "Rainbow Pattern";
                        case 33: return "Rary's Mnemonic Enhancer";
                        case 34: return "Remove Curse";
                        case 35: return "Shadow Monsters";
                        case 36: return "Shout";
                        case 37: return "Solid Fog";
                        case 38: return "Stoneskin";
                        case 39: return "Vacancy";
                        case 40: return "Wall of Fire";
                        case 41: return "Wall of Ice";
                        case 42: return "Wizard Eye";
                        default: return "[ERROR: Level 4 memorized spell out of range.  Classes.1457]";
                    }
                case 5:
                    switch (Dice.Roll(1, 40))
                    {
                        case 1: return "Advanced Illusion";
                        case 2: return "Airy Water";
                        case 3: return "Animal Growth";
                        case 4: return "Animate Dead";
                        case 5: return "Avoidance";
                        case 6: return "Bigby's Interposing Hand";
                        case 7: return "Chaos";
                        case 8: return "Cloudkill";
                        case 9: return "Cone of Cold";
                        case 10: return "Conjure Elemental";
                        case 11: return "Contact Other Plane";
                        case 12: return "Demi-Shadow Monsters";
                        case 13: return "Dismissal";
                        case 14: return "Distance Distortion";
                        case 15: return "Domination";
                        case 16: return "Dream";
                        case 17: return "Extension II";
                        case 18: return "Fabricate";
                        case 19: return "False Vision";
                        case 20: return "Feeblemind";
                        case 21: return "Hold Monster";
                        case 22: return "Leomund's Lamentable Belaborment";
                        case 23: return "Leomund's Secret Chest";
                        case 24: return "Magic Jar";
                        case 25: return "Major Creation";
                        case 26: return "Monster Summoning III";
                        case 27: return "Mordenkainen's Faithful Hound";
                        case 28: return "Passwall";
                        case 29: return "Seeming";
                        case 30: return "Sending";
                        case 31: return "Shadow Door";
                        case 32: return "Shadow Magic";
                        case 33: return "Stone Shape";
                        case 34: return "Summon Shadow";
                        case 35: return "Telekinesis";
                        case 36: return "Teleport";
                        case 37: return "Transmute Rock to Mud";
                        case 38: return "Wall of Force";
                        case 39: return "Wall of Iron";
                        case 40: return "Wall of Stone";
                        default: return "[ERROR: Level 5 memorized spell out of range.  Classes.1502]";
                    }
                case 6:
                    switch (Dice.Roll(1, 40))
                    {
                        case 1: return "Anti-Magic Shell";
                        case 2: return "Bigby's Forceful Hand";
                        case 3: return "Chain Lightning";
                        case 4: return "Conjure Animals";
                        case 5: return "Contingency";
                        case 6: return "Control Weather";
                        case 7: return "Death Fog";
                        case 8: return "Death Spell";
                        case 9: return "Demi-Shadow Magic";
                        case 10: return "Disintegrate";
                        case 11: return "Enchant an Item";
                        case 12: return "Ensnarement";
                        case 13: return "Extension III";
                        case 14: return "Eyebite";
                        case 15: return "Geas";
                        case 16: return "Glassee";
                        case 17: return "Globe of Invulnerability";
                        case 18: return "Guards and Wards";
                        case 19: return "Invisible Stalker";
                        case 20: return "Legend Lore";
                        case 21: return "Lower Water";
                        case 22: return "Mass Suggestion";
                        case 23: return "Mirage Arcana";
                        case 24: return "Mislead";
                        case 25: return "Monster Summoning IV";
                        case 26: return "Mordenkainen's Lucubration";
                        case 27: return "Move Earth";
                        case 28: return "Otiluke's Freezing Sphere";
                        case 29: return "Part Water";
                        case 30: return "Permanent Illusion";
                        case 31: return "Programmed Illusion";
                        case 32: return "Project Image";
                        case 33: return "Reincarnation";
                        case 34: return "Repulsion";
                        case 35: return "Shades";
                        case 36: return "Stone to Flesh";
                        case 37: return "Tenser's Transformation";
                        case 38: return "Transmute Water to Dust";
                        case 39: return "True Seeing";
                        case 40: return "Veil";
                        default: return "[ERROR: Level 6 memorized spell out of range.  Classes.1547]";
                    }
                case 7:
                    switch (Dice.Roll(1, 26))
                    {
                        case 1: return "Banishment";
                        case 2: return "Bigby's Grasping Hand";
                        case 3: return "Charm Plants";
                        case 4: return "Control Undead";
                        case 5: return "Delayed Blast Fireball";
                        case 6: return "Drawmij's Instant Summons";
                        case 7: return "Duo-Dimension";
                        case 8: return "Finger of Death";
                        case 9: return "Forcecage";
                        case 10: return "Limited Wish";
                        case 11: return "Mass Invisibility";
                        case 12: return "Monster Summoning V";
                        case 13: return "Mordenkainen's Magnificent Mansion";
                        case 14: return "Mordenkainen's Sword";
                        case 15: return "Phase Door";
                        case 16: return "Power Word, Stun";
                        case 17: return "Prismatic Spray";
                        case 18: return "Reverse Gravity";
                        case 19: return "Sequester";
                        case 20: return "Shadow Walk";
                        case 21: return "Simulacrum";
                        case 22: return "Spell Turning";
                        case 23: return "Statue";
                        case 24: return "Teleport Without Error";
                        case 25: return "Vanish";
                        case 26: return "Vision";
                        default: return "[ERROR: Level 7 memorized spell out of range.  Classes.1578]";
                    }
                case 8:
                    switch (Dice.Roll(1, 22))
                    {
                        case 1: return "Antipathy-Sympathy";
                        case 2: return "Bigby's Clenched Fist";
                        case 3: return "Binding";
                        case 4: return "Clone";
                        case 5: return "Demand";
                        case 6: return "Glassteel";
                        case 7: return "Incendiary Cloud";
                        case 8: return "Mass Charm";
                        case 9: return "Maze";
                        case 10: return "Mind Blank";
                        case 11: return "Monster Summoning VI";
                        case 12: return "Otiluke's Telekinetic Sphere";
                        case 13: return "Otto's Irresistable Dance";
                        case 14: return "Permanency";
                        case 15: return "Polymorph Any Object";
                        case 16: return "Power Word, Blind";
                        case 17: return "Prismatic Wall";
                        case 18: return "Screen";
                        case 19: return "Serten's Spell Immunity";
                        case 20: return "Sink";
                        case 21: return "Symbol";
                        case 22: return "Trap the Soul";
                        default: return "[ERROR: Level 8 memorized spell out of range.  Classes.1605]";
                    }
                case 9:
                    switch (Dice.Roll(1, 18))
                    {
                        case 1: return "Astral Spell";
                        case 2: return "Bigby's Crushing Hand";
                        case 3: return "Crystalbrittle";
                        case 4: return "Energy Drain";
                        case 5: return "Foresight";
                        case 6: return "Gate";
                        case 7: return "Imprisonment";
                        case 8: return "Meteor Swarm";
                        case 9: return "Monster Summoning VII";
                        case 10: return "Mordenkainen's Disjunction";
                        case 11: return "Power Word, Kill";
                        case 12: return "Prismatic Sphere";
                        case 13: return "Shape Change";
                        case 14: return "Succor";
                        case 15: return "Temporal Stasis";
                        case 16: return "Time Stop";
                        case 17: return "Weird";
                        case 18: return "Wish";
                        default: return "[ERROR: Level 9 memorized spell out of range.  Classes.1628]";
                    }
                default: return "[ERROR: Spell level out of range.  Classes.1630]";
            }
        }

        public static String Abilities(CLASS charClass, Int32 level, Int32 wisdom)
        {
            var output = String.Empty;

            switch (charClass)
            {
                case CLASS.BARBARIAN:
                    output += String.Format("Rage {0}/day", level / 4 + 1);
                    if (level >= 2)
                        output += ", keeps dexterity bonus if flat-footed";
                    if (level >= 5)
                        output += ", can't be flanked";
                    if (level >= 10)
                        output += String.Format(", +{0} against traps", (level - 10) / 3 + 1);
                    if (level >= 11)
                        output += String.Format(", Damage reduction {0}/-", (level - 11) / 3 + 1);
                    break;
                case CLASS.BARD:
                    output += String.Format("Influence reactions of others (save vs. paralyzation at -{0})", level / 3);
                    output += String.Format(", Inspire allies (+1 attack, +1 saving throws, +2 morale, for {0} rounds)", level);
                    output += ", counter magical attacks with explanations, commands, or suggestions (30' around, make successful save vs. spell)";
                    output += String.Format(", Identify an item ({0}%)", level * 5);
                    break;
                case CLASS.CLERIC: return "\nTURN: (2d6 undead, * 2d4 additionally turned)" + TurnUndead(level);
                case CLASS.DRUID:
                    if (level >= 3)
                        output += "Identify plants, animals, and pure water with perfect accuracy, Pass through overgrown areas with no trail and at normal movement rate";
                    if (level >= 7)
                        output += ", Immune to charm spells from woodland creatures, shape change into an animal 3/day";
                    if (level >= 16)
                        output += ", Immunity to all natural poisons, doesn't age, change appearance at will";
                    if (level >= 17)
                        output += ", can hibernate, safely enter Earth plane at will";
                    if (level >= 18)
                        output += ", safely enter Fire plane at will";
                    if (level >= 19)
                        output += ", safely enter Water plane at will";
                    if (level >= 20)
                        output += ", safely enter Air plane at will";
                    break;
                case CLASS.MONK:
                    output += String.Format("AC {0} better", (wisdom - 10) / 2);
                    output += String.Format(", Stun attack (lasts 1 round), {0}/day", level);
                    output += ", Evasion (successful half damage saves are no damage)";
                    if (level >= 2)
                        output += ", Deflect arrows (+1 AC versus missiles)";
                    if (level >= 3)
                        output += ", +2 versus enchantment spells";
                    if (level >= 4)
                        output += String.Format(", Slow Fall (use wall to reduce fall height by {0} feet)", ((level - 2) / 2) * 10);
                    if (level >= 5)
                        output += ", Immunity to nonmagical diseases";
                    if (level >= 7)
                        output += String.Format(", Heal self ({0} points/day)", level * 2);
                    if (level >= 9)
                        output += ", Improved Evasion (failed half damage saves are still half damage)";
                    if (level >= 10)
                        output += String.Format(", Unarmed strike is magical (+{0})", (level - 10) / 3 + 1);
                    if (level >= 11)
                        output += ", Immunity to all poisons";
                    if (level >= 15)
                        output += ", Quivering palm (fatal blow, 1/week, save vs. death)";
                    if (level >= 17)
                        output += ", does not age, can speak to any living creature";
                    if (level >= 19)
                        output += String.Format(", Become ethereal at will (total {0} rounds/day)", level);
                    if (level >= 20)
                        output += ", counts as extraplanar creature, damage reduction 20/+1";
                    break;
                case CLASS.PALADIN:
                    output += "Detect Evil, +2 saving throws, immune to disease";
                    output += String.Format(", heal by laying on hands ({0} points/day)", 2 * level);
                    output += String.Format(", cure disease ({0}/week)", (level - 1) / 5 + 1);
                    output += ", protection from evil 10'";
                    if (level >= 3)
                        output += "\nTURN: (2d6 undead, * 2d4 additionally turned)" + TurnUndead(level - 2);
                    output += "\nMust tithe"; break;
                case CLASS.RANGER:
                    switch (level)
                    {
                        case 1: return "Hide in Shadows: 10% Move Silently: 15%";
                        case 2: return "Hide in Shadows: 15% Move Silently: 21%";
                        case 3: return "Hide in Shadows: 20% Move Silently: 27%";
                        case 4: return "Hide in Shadows: 25% Move Silently: 33%";
                        case 5: return "Hide in Shadows: 31% Move Silently: 40%";
                        case 6: return "Hide in Shadows: 37% Move Silently: 47%";
                        case 7: return "Hide in Shadows: 43% Move Silently: 55%";
                        case 8: return "Hide in Shadows: 49% Move Silently: 62%";
                        case 9: return "Hide in Shadows: 56% Move Silently: 70%";
                        case 10: return "Hide in Shadows: 63% Move Silently: 78%";
                        case 11: return "Hide in Shadows: 70% Move Silently: 86%";
                        case 12: return "Hide in Shadows: 77% Move Silently: 94%";
                        case 13: return "Hide in Shadows: 85% Move Silently: 99%";
                        case 14: return "Hide in Shadows: 93% Move Silently: 99%";
                        default: return "Hide in Shadows: 99% Move Silently: 99%";
                    }
                case CLASS.THIEF: return String.Format("BACKSTAB: x{0}", (level - 1) / 4 + 2);
                default: return String.Empty;
            }

            return output;
        }

        public static Int32[] ThiefAbilities(Int32 level, RACE race, Int32 dexterity, Boolean[] armor)
        {
            var abil = new Int32[8] { 15, 10, 5, 10, 5, 15, 60, 0 };

            if (armor[1])
            {
                abil[0] -= 20;
                abil[1] -= 5;
                abil[2] -= 5;
                abil[3] -= 10;
                abil[4] -= 10;
                abil[5] -= 5;
                abil[6] -= 20;
            }
            else if (armor[2])
            {
                abil[0] -= 30;
                abil[1] -= 10;
                abil[2] -= 10;
                abil[3] -= 20;
                abil[4] -= 20;
                abil[5] -= 10;
                abil[6] -= 30;
            }
            else if (armor[0])
            {
                abil[0] += 5;
                abil[3] += 10;
                abil[4] += 5;
                abil[6] += 10;
            }

            switch (dexterity)
            {
                case 1: abil[0] -= 55; abil[1] -= 50; abil[2] -= 50; abil[3] -= 60; abil[4] -= 50; break;
                case 2: abil[0] -= 50; abil[1] -= 45; abil[2] -= 45; abil[3] -= 55; abil[4] -= 45; break;
                case 3: abil[0] -= 45; abil[1] -= 40; abil[2] -= 40; abil[3] -= 50; abil[4] -= 40; break;
                case 4: abil[0] -= 40; abil[1] -= 35; abil[2] -= 35; abil[3] -= 45; abil[4] -= 35; break;
                case 5: abil[0] -= 35; abil[1] -= 30; abil[2] -= 30; abil[3] -= 40; abil[4] -= 30; break;
                case 6: abil[0] -= 30; abil[1] -= 25; abil[2] -= 25; abil[3] -= 35; abil[4] -= 25; break;
                case 7: abil[0] -= 25; abil[1] -= 20; abil[2] -= 20; abil[3] -= 30; abil[4] -= 20; break;
                case 8: abil[0] -= 20; abil[1] -= 15; abil[2] -= 15; abil[3] -= 25; abil[4] -= 15; break;
                case 9: abil[0] -= 15; abil[1] -= 10; abil[2] -= 10; abil[3] -= 20; abil[4] -= 10; break;
                case 10: abil[0] -= 10; abil[1] -= 5; abil[2] -= 10; abil[3] -= 15; abil[4] -= 5; break;
                case 11: abil[0] -= 5; abil[2] -= 5; abil[3] -= 10; break;
                case 12: abil[3] -= 5; break;
                case 16: abil[1] += 5; break;
                case 17: abil[0] += 5; abil[1] += 10; abil[3] += 5; abil[4] += 5; break;
                case 18: abil[0] += 10; abil[1] += 15; abil[2] += 5; abil[3] += 10; abil[4] += 10; break;
                case 19: abil[0] += 15; abil[1] += 20; abil[2] += 10; abil[3] += 15; abil[4] += 15; break;
                case 20: abil[0] += 20; abil[1] += 25; abil[2] += 15; abil[3] += 20; abil[4] += 20; break;
                case 21: abil[0] += 25; abil[1] += 30; abil[2] += 20; abil[3] += 25; abil[4] += 25; break;
                case 22: abil[0] += 30; abil[1] += 35; abil[2] += 25; abil[3] += 30; abil[4] += 30; break;
                case 23: abil[0] += 35; abil[1] += 40; abil[2] += 30; abil[3] += 35; abil[4] += 35; break;
                case 24: abil[0] += 40; abil[1] += 45; abil[2] += 35; abil[3] += 40; abil[4] += 40; break;
                case 25: abil[0] += 45; abil[1] += 50; abil[2] += 40; abil[3] += 45; abil[4] += 45; break;
                default: break;
            }

            switch (race)
            {
                case RACE.DEEP_DWARF:
                case RACE.DERRO_DWARF:
                case RACE.DUERGAR:
                case RACE.HILL_DWARF:
                case RACE.MOUNTAIN_DWARF: abil[1] += 10; abil[2] += 15; abil[6] -= 10; abil[7] -= 5; break;
                case RACE.DROW:
                case RACE.GRAY_ELF:
                case RACE.WILD_ELF:
                case RACE.HIGH_ELF:
                case RACE.WOOD_ELF: abil[0] += 5; abil[1] -= 5; abil[3] += 5; abil[4] += 10; abil[5] += 5; break;
                case RACE.SVIRFNEBLIN:
                case RACE.ROCK_GNOME:
                case RACE.FOREST_GNOME: abil[1] += 5; abil[2] += 10; abil[3] += 5; abil[4] += 5; abil[5] += 10; abil[6] -= 15; break;
                case RACE.HALFELF: abil[0] += 10; abil[4] += 5; break;
                case RACE.DEEP_HALFLING:
                case RACE.LIGHTFOOT_HALFLING:
                case RACE.TALLFELLOW_HALFLING: abil[0] += 5; abil[1] += 5; abil[2] += 5; abil[3] += 10; abil[4] += 15; abil[5] += 5; abil[6] -= 15; abil[7] -= 5; break;
                default: break;
            }

            var pointsToSpend = 60 + (level - 1) * 30;

            while (pointsToSpend > 0 && abil.Any(x => x < 99))
            {
                var tempStat = Dice.d8(-1);
                if (abil[tempStat] < 99)
                {
                    abil[tempStat]++;
                    pointsToSpend--;
                }
            }

            return abil;
        }

        public static Int32[] BardAbilities(Int32 Level, RACE Race, Int32 Dexterity, Boolean[] Armor)
        {
            var abil = new Int32[4] { 50, 20, 10, 5 };

            if (Armor[1])
            {
                abil[0] -= 20;
                abil[1] -= 5;
                abil[2] -= 20;
            }
            else if (Armor[2])
            {
                abil[0] -= 30;
                abil[1] -= 10;
                abil[2] -= 30;
            }
            else if (Armor[0])
            {
                abil[0] += 10;
                abil[2] += 5;
            }

            switch (Dexterity)
            {
                case 1: abil[2] -= 55; break;
                case 2: abil[2] -= 50; break;
                case 3: abil[2] -= 45; break;
                case 4: abil[2] -= 40; break;
                case 5: abil[2] -= 35; break;
                case 6: abil[2] -= 30; break;
                case 7: abil[2] -= 25; break;
                case 8: abil[2] -= 20; break;
                case 9: abil[2] -= 15; break;
                case 10: abil[2] -= 10; break;
                case 11: abil[2] -= 5; break;
                case 17: abil[2] += 5; break;
                case 18: abil[2] += 10; break;
                case 19: abil[2] += 15; break;
                case 20: abil[2] += 20; break;
                case 21: abil[2] += 25; break;
                case 22: abil[2] += 30; break;
                case 23: abil[2] += 35; break;
                case 24: abil[2] += 40; break;
                case 25: abil[2] += 45; break;
                default: break;
            }

            switch (Race)
            {
                case RACE.DEEP_DWARF:
                case RACE.DERRO_DWARF:
                case RACE.DUERGAR:
                case RACE.HILL_DWARF:
                case RACE.MOUNTAIN_DWARF: abil[1] -= 10; abil[3] -= 5; break;
                case RACE.DROW:
                case RACE.GRAY_ELF:
                case RACE.WILD_ELF:
                case RACE.HIGH_ELF:
                case RACE.WOOD_ELF: abil[2] += 5; abil[0] += 5; break;
                case RACE.SVIRFNEBLIN:
                case RACE.ROCK_GNOME:
                case RACE.FOREST_GNOME: abil[0] += 10; abil[1] -= 15; break;
                case RACE.HALFELF: abil[2] += 10; break;
                case RACE.DEEP_HALFLING:
                case RACE.LIGHTFOOT_HALFLING:
                case RACE.TALLFELLOW_HALFLING: abil[2] += 5; abil[0] += 5; abil[1] -= 15; abil[3] -= 5; break;
                default: break;
            }

            var pointsToSpend = 20 + (Level - 1) * 15;

            while (pointsToSpend > 0 && abil.Any(x => x < 99))
            {
                var tempStat = Dice.d4(-1);
                if (abil[tempStat] < 99)
                {
                    abil[tempStat]++;
                    pointsToSpend--;
                }
            }

            return abil;
        }

        public static String TurnUndead(Int32 Level)
        {
            var output = "\nHD\t1 HD\tZombie\t2 HD\t3-4 HD\t5 HD\tGhast\t6 HD\t7 HD\t8 HD\t9 HD\t10 HD\t11+ HD\tSpecial";
            
            switch (Level)
            {
                case 1: output += "\nRoll\t10\t13\t16\t19\t20\t-\t-\t-\t-\t-\t-\t-\t-"; break;
                case 2: output += "\nRoll\t7\t10\t13\t16\t  19\t  20\t -\t  -\t  -\t  -\t  -\t  -\t  -"; break;
                case 3: output += "\nRoll\t4\t  7\t  10\t  13\t  16\t  19\t  20\t -\t  -\t  -\t  -\t  -\t  -"; break;
                case 4: output += "\nRoll\tT\t  4\t  7\t  10\t  13\t  16\t  19\t  20\t -\t  -\t  -\t  -\t  -"; break;
                case 5: output += "\nRoll\tT\t  T\t  4\t  7\t  10\t  13\t  16\t  19\t  20\t -\t  -\t  -\t  -"; break;
                case 6: output += "\nRoll\tD\t  T\t  T\t  4\t  7\t  10\t  13\t  16\t  19\t  20\t -\t  -\t  -"; break;
                case 7: output += "\nRoll\tD\t  D\t  T\t  T\t  4\t  7\t  10\t  13\t  16\t  19\t  20\t -\t  -"; break;
                case 8: output += "\nRoll\tD*\t D\t  D\t  T\t  T\t  4\t  7\t  10\t  13\t  16\t  19\t  20\t -"; break;
                case 9: output += "\nRoll\tD*\t D*\t D\t  D\t  T\t  T\t  4\t  7\t  10\t  13\t  16\t  19\t  20"; break;
                case 10:
                case 11: output += "\nRoll\tD*\t D*\t D*\t D\t  D\t  T\t  T\t  4\t  7\t  10\t  13\t  16\t  19"; break;
                case 12:
                case 13: output += "\nRoll\tD*\t D*\t D*\t D*\t D\t  D\t  T\t  T\t  4\t  7\t  10\t  13\t  16"; break;
                default: output += "\nRoll\tD*\t D*\t D*\t D*\t D*\t D\t  D\t  T\t  T\t  4\t  7\t  10\t  13"; break;
            }

            return output;
        }
    }
}
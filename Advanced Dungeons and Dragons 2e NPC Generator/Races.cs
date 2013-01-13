using System;
using System.Collections.Generic;
using System.Text;

namespace NPCGen
{
    class Races
    {
        public enum RACE { AASIMAR,
            DEEP_DWARF, HILL_DWARF, MOUNTAIN_DWARF,
            GRAY_ELF, HIGH_ELF, WILD_ELF, WOOD_ELF, HALFELF,
            FOREST_GNOME, ROCK_GNOME, SVIRFNEBLIN,
            HALFORC, LIGHTFOOT_HALFLING, DEEP_HALFLING, TALLFELLOW_HALFLING,
            HUMAN, LIZARDFOLK, DOPPELGANGER, GOBLIN, HOBGOBLIN, KOBOLD, ORC,
            TIEFLING, DROW, DUERGAR, DERRO_DWARF, GNOLL, TROGLODYTE, BUGBEAR, OGRE, MINOTAUR, MIND_FLAYER, OGRE_MAGE};
        
        public enum METARACE { NONE, HALF_CELESTIAL, HALF_DRAGON, WEREBEAR, WEREBOAR, WERETIGER, WERERAT, WEREWOLF, HALF_FIEND };
        public enum METARACE_RANDOMIZER { ANY, MAYBE, ANY_LYCANTHROPE, ANY_NONLYCANTHROPE,
            ANY_GOOD, ANY_NEUTRAL, ANY_EVIL, ANY_NONGOOD, ANY_NONNEUTRAL, ANY_NONEVIL };
        public enum RACE_RANDOMIZER { ANY, ANY_STANDARD, ANY_NONSTANDARD,
            ANY_EVIL, ANY_GOOD, ANY_NEUTRAL, ANY_NONEVIL, ANY_NONNEUTRAL, ANY_NONGOOD };

        public RACE Race;
        public METARACE MetaRace;
        public bool Male;

        public string Gender
        {
            get
            {
                if (!Male)
                    return "Female";
                return "Male";
            }
        }

        public static string[] RacesArray
        {
            get
            {
                return Enum.GetNames(typeof(RACE));
            }
        }

        public static string[] MetaRacesArray
        {
            get
            {
                return Enum.GetNames(typeof(METARACE));
            }
        }

        public static string[] RaceRandomizerArray
        {
            get
            {
                return Enum.GetNames(typeof(RACE_RANDOMIZER));
            }
        }

        public static string[] MetaRaceRandomizerArray
        {
            get
            {
                return Enum.GetNames(typeof(METARACE_RANDOMIZER));
            }
        }

        public string RacialTraits
        {
            get
            {
                switch (Race)
                {
                    case RACE.BUGBEAR: return "THACO 2 better, AC 3 better, +1 on saves";
                    case RACE.DERRO_DWARF: return "+1/(3.5 CON) saving throw v. wands, staves, rods, and spells; 20% failure using non-class magic items; +1 to hit against orcs and goblins; giants and trolls -4 to hit; infravision 60'; detect grade of slope (1-5 on d6), detect new tunnel/passage construction (1-5 on d6); detect sliding/shifting walls or rooms (1-4 on d6); detect stonework traps, pits, and deadfalls (1-3 on d6); determine approx. depth underground (1-3 on d6); Blind fighting";
                    case RACE.DOPPELGANGER: return "THACO 3 better, AC 4 better";
                    case RACE.GOBLIN:
                    case RACE.HOBGOBLIN: return "Can move silently";
                    case RACE.DUERGAR: return "+1/(3.5 CON) saving throw v. wands, staves, rods, and spells; 20% failure using non-class magic items; +1 to hit against orcs and goblins; giants and trolls -4 to hit; infravision 60'; detect grade of slope (1-5 on d6), detect new tunnel/passage construction (1-5 on d6); detect sliding/shifting walls or rooms (1-4 on d6); detect stonework traps, pits, and deadfalls (1-3 on d6); determine approx. depth underground (1-3 on d6); Can move silently";
                    case RACE.GNOLL: return "THACO 1 better, AC 1 better";
                    case RACE.LIZARDFOLK: return "THACO 1 better, AC 5 better";
                    case RACE.MIND_FLAYER: return "THACO 6 better, AC 3 better";
                    case RACE.MINOTAUR: return "THACO 6 better, AC 5 better";
                    case RACE.OGRE:
                    case RACE.OGRE_MAGE: return "THACO 3 better, AC 5 better";
                    case RACE.SVIRFNEBLIN: return "+1/(3.5 CON) saving throw v. wands, staves, rods, and spells; 20% failure using non-class magic items; +1 to hit against kobolds and goblins; gnolls, bugbears, and giants -4 to hit; infravision 60'; detect grade of slope (1-5 on d6); detect unsafe walls, ceiling, and floors (1-7 on d10); determine approx. depth underground (1-4 on d6); determine approx. direction underground (1-3 on d6); Dodge bonus +4 to AC";
                    case RACE.TROGLODYTE: return "THACO 1 better, AC 6 better";
                    case RACE.DEEP_DWARF:
                    case RACE.MOUNTAIN_DWARF:
                    case RACE.HILL_DWARF: return "+1/(3.5 CON) saving throw v. wands, staves, rods, and spells; 20% failure using non-class magic items; +1 to hit against orcs and goblins; giants and trolls -4 to hit; infravision 60'; detect grade of slope (1-5 on d6), detect new tunnel/passage construction (1-5 on d6); detect sliding/shifting walls or rooms (1-4 on d6); detect stonework traps, pits, and deadfalls (1-3 on d6); determine approx. depth underground (1-3 on d6)";
                    case RACE.DROW:
                    case RACE.GRAY_ELF:
                    case RACE.WILD_ELF:
                    case RACE.WOOD_ELF:
                    case RACE.HIGH_ELF: return "90% resistance to sleep and charm spells; +1 when using bow, short sword, or longsword; infravision 60'; find secret doors (on d6: 1 passing, 2 active secret door, 3 active concealed door)";
                    case RACE.FOREST_GNOME:
                    case RACE.ROCK_GNOME: return "+1/(3.5 CON) saving throw v. wands, staves, rods, and spells; 20% failure using non-class magic items; +1 to hit against kobolds and goblins; gnolls, bugbears, and giants -4 to hit; infravision 60'; detect grade of slope (1-5 on d6); detect unsafe walls, ceiling, and floors (1-7 on d10); determine approx. depth underground (1-4 on d6); determine approx. direction underground (1-3 on d6)";
                    case RACE.HALFELF: return "30% resistance to sleep and charm spells; infravision 60'; find secret doors (on d6: 1 passing, 2 active secret door, 3 active concealed door)";
                    case RACE.DEEP_HALFLING:
                    case RACE.TALLFELLOW_HALFLING:
                    case RACE.LIGHTFOOT_HALFLING: return "+1/(3.5 CON) saving throw v. poison, wands, staves, rods, and spells; +1 with thrown missile weapons and slings; infravision 60'; detect grade of passage (1-3 on d6); detect direction (1-3 on d6)";
                    case RACE.HALFORC: return "Darkvision; can use orc weapons with no penalty";
                    default: return "";
                }
            }
        }

        public string MetaRacialTraits
        {
            get
            {
                switch (MetaRace)
                {
                    case METARACE.HALF_CELESTIAL:
                        string output = "AC 1 better"; Random random = new Random();
                        if (Dice.Percentile(ref random) <= 75)
                            output += ", angel wings (fly at double speed)";
                        return output;
                    case METARACE.HALF_DRAGON: return "If large, has wings (can fly)";
                    case METARACE.HALF_FIEND: 
                        output = "AC 1 better"; random = new Random();
                        if (Dice.Percentile(ref random) <= 50)
                            output += ", bat wings (can fly)";
                        return output;
                    case METARACE.WEREBOAR:
                    case METARACE.WERERAT:
                    case METARACE.WERETIGER:
                    case METARACE.WEREWOLF:
                    case METARACE.WEREBEAR: return "AC 2 better";
                    default: return "";
                }
            }
        }

        public static string[] RacesRandomizerArray
        {
            get
            {
                return Enum.GetNames(typeof(RACE_RANDOMIZER));
            }
        }

        public Races(Character.ALIGNMENT Alignment, Classes.CLASS Class, RACE_RANDOMIZER Randomizer, bool AllowMetaRaces, bool Male, ref Random random)
        {
            switch (Randomizer)
            {
                case RACE_RANDOMIZER.ANY_STANDARD: RaceByAlignment(Alignment, Class, AllowMetaRaces, true, false, ref random); break;
                case RACE_RANDOMIZER.ANY_NONSTANDARD: RaceByAlignment(Alignment, Class, AllowMetaRaces, false, true, ref random); break;
                case RACE_RANDOMIZER.ANY_EVIL: RaceByAlignment(Character.ALIGNMENT.EVIL, Class, AllowMetaRaces, false, false, ref random); break;
                case RACE_RANDOMIZER.ANY_GOOD: RaceByAlignment(Character.ALIGNMENT.GOOD, Class, AllowMetaRaces, false, false, ref random); break;
                case RACE_RANDOMIZER.ANY_NEUTRAL: RaceByAlignment(Character.ALIGNMENT.NEUTRAL, Class, AllowMetaRaces, false, false, ref random); break;
                case RACE_RANDOMIZER.ANY_NONEVIL:
                    if (Alignment != Character.ALIGNMENT.EVIL)
                        RaceByAlignment(Alignment, Class, AllowMetaRaces, false, false, ref random);
                    else
                    {
                        if (Dice.Roll(1, 5, 0, ref random) > 3)
                            RaceByAlignment(Character.ALIGNMENT.GOOD, Class, AllowMetaRaces, false, false, ref random);
                        else
                            RaceByAlignment(Character.ALIGNMENT.NEUTRAL, Class, AllowMetaRaces, false, false, ref random);
                    } break;
                case RACE_RANDOMIZER.ANY_NONGOOD:
                    if (Alignment != Character.ALIGNMENT.GOOD)
                        RaceByAlignment(Alignment, Class, AllowMetaRaces, false, false, ref random);
                    else
                    {
                        if (Dice.d8(ref random) > 3)
                            RaceByAlignment(Character.ALIGNMENT.EVIL, Class, AllowMetaRaces, false, false, ref random);
                        else
                            RaceByAlignment(Character.ALIGNMENT.NEUTRAL, Class, AllowMetaRaces, false, false, ref random);
                    } break;
                case RACE_RANDOMIZER.ANY_NONNEUTRAL:
                    if (Alignment != Character.ALIGNMENT.EVIL)
                        RaceByAlignment(Alignment, Class, AllowMetaRaces, false, false, ref random);
                    else
                    {
                        if (Dice.Roll(1, 7, 0, ref random) > 2)
                            RaceByAlignment(Character.ALIGNMENT.EVIL, Class, AllowMetaRaces, false, false, ref random);
                        else
                            RaceByAlignment(Character.ALIGNMENT.GOOD, Class, AllowMetaRaces, false, false, ref random);
                    } break;
                default: RaceByAlignment(Alignment, Class, AllowMetaRaces, false, false, ref random); break;
            }

            this.Male = Male;
        }

        public Races()
        {
            Race = RACE.HUMAN;
            MetaRace = METARACE.NONE;
            Male = true;
        }

        private void RaceByAlignment(Character.ALIGNMENT Alignment, Classes.CLASS Class, bool AllowMetaRaces, bool ForceStandard, bool ForceNonStandard, ref Random random)
        {
            switch (Alignment)
            {
                case Character.ALIGNMENT.GOOD:
                    switch (Class)
                    {
                        case Classes.CLASS.BARBARIAN:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                case 2:
                                    if (!ForceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);    
                                    break;
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                case 10:
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
                                case 30:
                                case 31:
                                case 32:
                                    if (!ForceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 33:
                                case 34:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 35:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 36:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 37:
                                case 38:
                                case 39:
                                case 40:
                                case 41:
                                case 42:
                                case 43:
                                case 44:
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
                                case 56:
                                case 57:
                                case 58:
                                case 59:
                                case 60:
                                case 61:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.BARD:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                    if (!ForceStandard)
                                        Race = RACE.AASIMAR;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                    if (!ForceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 7:
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                    if (!ForceStandard)
                                        Race = RACE.GRAY_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 30:
                                case 31:
                                case 32:
                                case 33:
                                case 34:
                                case 35:
                                case 36:
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 37:
                                    if (!ForceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 38:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 39:
                                    if (!ForceStandard)
                                        Race = RACE.FOREST_GNOME;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 40:
                                case 41:
                                case 42:
                                case 43:
                                case 44:
                                    if (!ForceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 54:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 55:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 56:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 57:
                                    if (!ForceNonStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 98:
                                    if (!ForceStandard)
                                        Race = RACE.SVIRFNEBLIN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces)
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                    RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces)
                                        MetaRace = METARACE.HALF_DRAGON;
                                    RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.CLERIC:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                    if (!ForceStandard)
                                        Race = RACE.AASIMAR;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 2:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                case 10:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 23:
                                case 24:
                                    if (!ForceStandard)
                                        Race = RACE.MOUNTAIN_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 25:
                                    if (!ForceStandard)
                                        Race = RACE.GRAY_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 36:
                                case 37:
                                case 38:
                                case 39:
                                case 40:
                                    if (!ForceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 41:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 42:
                                    if (!ForceStandard)
                                        Race = RACE.FOREST_GNOME;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 43:
                                case 44:
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                    if (!ForceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 52:
                                case 53:
                                case 54:
                                case 55:
                                case 56:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 67:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 68:
                                case 69:
                                    if (!ForceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 70:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 71:
                                case 72:
                                case 73:
                                case 74:
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 96:
                                    if (!ForceStandard)
                                        Race = RACE.SVIRFNEBLIN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 97:
                                case 98:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBEAR;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.DRUID:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                    if (!ForceStandard)
                                        Race = RACE.GRAY_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 32:
                                case 33:
                                case 34:
                                case 35:
                                case 36:
                                    if (!ForceStandard)
                                        Race = RACE.FOREST_GNOME;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 37:
                                    if (!ForceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 38:
                                case 39:
                                case 40:
                                case 41:
                                case 42:
                                case 43:
                                case 44:
                                case 45:
                                case 46:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 47:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 48:
                                    if (!ForceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 49:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
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
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.FIGHTER:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                case 2:
                                case 3:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 4:
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                case 10:
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
                                case 30:
                                case 31:
                                case 32:
                                case 33:
                                    if (!ForceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 34:
                                case 35:
                                case 36:
                                case 37:
                                case 38:
                                case 39:
                                case 40:
                                case 41:
                                    if (!ForceStandard)
                                        Race = RACE.MOUNTAIN_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 42:
                                    if (!ForceStandard)
                                        Race = RACE.GRAY_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 43:
                                case 44:
                                case 45:
                                case 46:
                                case 47:
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 48:
                                    if (!ForceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 49:
                                case 50:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 51:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 52:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 53:
                                case 54:
                                case 55:
                                case 56:
                                case 57:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 98:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBEAR;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.MONK:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                case 2:
                                    if (!ForceStandard)
                                        Race = RACE.AASIMAR;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 3:
                                    if (!ForceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 4:
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                case 12:
                                case 13:
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 14:
                                case 15:
                                case 16:
                                case 17:
                                case 18:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 19:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 20:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 21:
                                case 22:
                                case 23:
                                case 24:
                                case 25:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
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
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 98:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBEAR;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.PALADIN:
                            switch (Dice.Percentile(ref random))
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
                                case 10:
                                    if (!ForceStandard)
                                        Race = RACE.AASIMAR;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 21:
                                    if (!ForceStandard)
                                        Race = RACE.MOUNTAIN_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 22:
                                    if (!ForceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 23:
                                case 24:
                                case 25:
                                case 26:
                                case 27:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 28:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 29:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 30:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
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
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 98:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBEAR;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.RANGER:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                    if (!ForceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                case 10:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 21:
                                    if (!ForceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 35:
                                case 36:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 37:
                                case 38:
                                case 39:
                                case 40:
                                case 41:
                                    if (!ForceStandard)
                                        Race = RACE.FOREST_GNOME;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 42:
                                    if (!ForceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 43:
                                case 44:
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
                                case 56:
                                case 57:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 58:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 59:
                                    if (!ForceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 60:
                                case 61:
                                case 62:
                                case 63:
                                case 64:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 98:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBEAR;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.SORCERER:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                case 2:
                                    if (!ForceStandard)
                                        Race = RACE.AASIMAR;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 3:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 4:
                                case 5:
                                    if (!ForceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 6:
                                    if (!ForceStandard)
                                        Race = RACE.MOUNTAIN_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 7:
                                case 8:
                                    if (!ForceStandard)
                                        Race = RACE.GRAY_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 9:
                                case 10:
                                case 11:
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 30:
                                case 31:
                                case 32:
                                case 33:
                                case 34:
                                case 35:
                                case 36:
                                    if (!ForceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 37:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 38:
                                    if (!ForceStandard)
                                        Race = RACE.FOREST_GNOME;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 39:
                                case 40:
                                    if (!ForceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 41:
                                case 42:
                                case 43:
                                case 44:
                                case 45:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 55:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 56:
                                    if (!ForceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 57:
                                case 58:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 96:
                                    if (!ForceStandard)
                                        Race = RACE.SVIRFNEBLIN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 97:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 98:
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBEAR;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.THIEF:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                    if (!ForceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 6:
                                    if (!ForceStandard)
                                        Race = RACE.MOUNTAIN_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 7:
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                case 12:
                                case 13:
                                case 14:
                                case 15:
                                case 16:
                                case 17:
                                case 18:
                                case 19:
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 20:
                                    if (!ForceStandard)
                                        Race = RACE.FOREST_GNOME;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 21:
                                case 22:
                                case 23:
                                case 24:
                                case 25:
                                    if (!ForceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 36:
                                case 37:
                                case 38:
                                case 39:
                                case 40:
                                case 41:
                                case 42:
                                case 43:
                                case 44:
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
                                case 56:
                                case 57:
                                case 58:
                                case 59:
                                case 60:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 61:
                                case 62:
                                case 63:
                                case 64:
                                case 65:
                                case 66:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 67:
                                case 68:
                                case 69:
                                case 70:
                                case 71:
                                case 72:
                                    if (!ForceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 73:
                                case 74:
                                case 75:
                                case 76:
                                case 77:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 78:
                                case 79:
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 97:
                                    if (!ForceStandard)
                                        Race = RACE.SVIRFNEBLIN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 98:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBEAR;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.WIZARD:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                    if (!ForceStandard)
                                        Race = RACE.AASIMAR;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 2:
                                    if (!ForceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                case 7:
                                    if (!ForceStandard)
                                        Race = RACE.GRAY_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 8:
                                case 9:
                                case 10:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 42:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 43:
                                    if (!ForceStandard)
                                        Race = RACE.FOREST_GNOME;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 44:
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                    if (!ForceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
                                case 56:
                                case 57:
                                case 58:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 59:
                                case 60:
                                case 61:
                                case 62:
                                case 63:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 64:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 65:
                                case 66:
                                case 67:
                                    if (!ForceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 68:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 97:
                                    if (!ForceStandard)
                                        Race = RACE.SVIRFNEBLIN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 98:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBEAR;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                    } break;
                case Character.ALIGNMENT.NEUTRAL:
                    switch (Class)
                    {
                        case Classes.CLASS.BARBARIAN:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 2:
                                    if (!ForceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                case 12:
                                case 13:
                                    if (!ForceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 14:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 15:
                                case 16:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 17:
                                case 18:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 19:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
                                case 56:
                                case 57:
                                case 58:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
                                case 81:
                                case 82:
                                case 83:
                                case 84:
                                case 85:
                                case 86:
                                case 87:
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceStandard)
                                        Race = RACE.LIZARDFOLK;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBOAR;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WERETIGER;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.BARD:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 2:
                                case 3:
                                    if (!ForceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 4:
                                case 5:
                                    if (!ForceStandard)
                                        Race = RACE.GRAY_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                case 12:
                                case 13:
                                case 14:
                                case 15:
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 16:
                                    if (!ForceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 17:
                                case 18:
                                case 19:
                                case 20:
                                case 21:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 22:
                                case 23:
                                    if (!ForceNonStandard)
                                        Race = RACE.ROCK_GNOME; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 34:
                                case 35:
                                case 36:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 37:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 38:
                                    if (!ForceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 39:
                                case 40:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 41:
                                case 42:
                                case 43:
                                case 44:
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
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
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBOAR;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WERETIGER;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.CLERIC:
                            switch (Dice.Percentile(ref random))
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
                                case 10:
                                case 11:
                                case 12:
                                case 13:
                                case 14:
                                case 15:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_DWARF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HILL_DWARF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 26:
                                    if (!ForceStandard)
                                        Race = RACE.MOUNTAIN_DWARF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 27:
                                    if (!ForceStandard)
                                        Race = RACE.HIGH_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 28:
                                    if (!ForceStandard)
                                        Race = RACE.WILD_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 39:
                                    if (!ForceNonStandard)
                                        Race = RACE.ROCK_GNOME; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 40:
                                case 41:
                                case 42:
                                case 43:
                                case 44:
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
                                case 56:
                                case 57:
                                case 58:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 59:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 60:
                                    if (!ForceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 61:
                                case 62:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 91:
                                case 92:
                                case 93:
                                case 94:
                                case 95:
                                case 96:
                                case 97:
                                    if (!ForceStandard)
                                        Race = RACE.LIZARDFOLK; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 98:
                                    if (!ForceStandard)
                                        Race = RACE.DOPPELGANGER; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBOAR;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WERETIGER;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.DRUID:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                    if (!ForceStandard)
                                        Race = RACE.GRAY_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 7:
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                    if (!ForceStandard)
                                        Race = RACE.WILD_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 30:
                                case 31:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 32:
                                    if (!ForceStandard)
                                        Race = RACE.FOREST_GNOME; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 33:
                                case 34:
                                case 35:
                                case 36:
                                case 37:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 38:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 39:
                                    if (!ForceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 40:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 41:
                                case 42:
                                case 43:
                                case 44:
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
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
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
                                case 81:
                                case 82:
                                case 83:
                                case 84:
                                case 85:
                                case 86:
                                case 87:
                                case 88:
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceStandard)
                                        Race = RACE.LIZARDFOLK; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBOAR;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WERETIGER;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.FIGHTER:
                            switch (Dice.Percentile(ref random))
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
                                case 10:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_DWARF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HILL_DWARF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 30:
                                case 31:
                                case 32:
                                case 33:
                                case 34:
                                    if (!ForceStandard)
                                        Race = RACE.MOUNTAIN_DWARF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 35:
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 36:
                                case 37:
                                case 38:
                                case 39:
                                case 40:
                                case 41:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 42:
                                case 43:
                                case 44:
                                case 45:
                                case 46:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 47:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 48:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
                                case 56:
                                case 57:
                                case 58:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 97:
                                    if (!ForceStandard)
                                        Race = RACE.LIZARDFOLK; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 98:
                                    if (!ForceStandard)
                                        Race = RACE.DOPPELGANGER; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBOAR;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WERETIGER;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.MONK:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                case 2:
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 3:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 4:
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                case 12:
                                case 13:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 14:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 15:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
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
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
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
                                case 100:
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.PALADIN:
                            switch (Dice.Percentile(ref random))
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
                                case 10:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HILL_DWARF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 21:
                                    if (!ForceStandard)
                                        Race = RACE.MOUNTAIN_DWARF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 22:
                                    if (!ForceNonStandard)
                                        Race = RACE.ROCK_GNOME; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 23:
                                case 24:
                                case 25:
                                case 26:
                                case 27:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 28:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 29:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 30:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
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
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 98: 
                                    if (!ForceStandard)
                                        Race = RACE.DOPPELGANGER; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBOAR;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WERETIGER;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.RANGER:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                    if (!ForceNonStandard)
                                        Race = RACE.HILL_DWARF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 7:
                                    if (!ForceStandard)
                                        Race = RACE.WILD_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 8:
                                case 9:
                                case 10:
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
                                case 30:
                                case 31:
                                case 32:
                                case 33:
                                case 34:
                                case 35:
                                case 36:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 37:
                                    if (!ForceStandard)
                                        Race = RACE.FOREST_GNOME; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 38:
                                    if (!ForceNonStandard)
                                        Race = RACE.ROCK_GNOME; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 39:
                                case 40:
                                case 41:
                                case 42:
                                case 43:
                                case 44:
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 56:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 57:
                                    if (!ForceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 97:
                                case 98:
                                    if (!ForceStandard)
                                        Race = RACE.LIZARDFOLK; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBOAR;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WERETIGER;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.SORCERER:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                    if (!ForceNonStandard)
                                        Race = RACE.HILL_DWARF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 2:
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                case 12:
                                    if (!ForceStandard)
                                        Race = RACE.WILD_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 13:
                                case 14:
                                case 15:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 16:
                                    if (!ForceNonStandard)
                                        Race = RACE.ROCK_GNOME; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 42:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 43:
                                    if (!ForceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 44:
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
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
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 96:
                                case 97:
                                    if (!ForceStandard)
                                        Race = RACE.LIZARDFOLK; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 98:
                                    if (!ForceStandard)
                                        Race = RACE.DOPPELGANGER; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBOAR;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WERETIGER;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.THIEF:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_DWARF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 2:
                                case 3:
                                case 4:
                                    if (!ForceNonStandard)
                                        Race = RACE.HILL_DWARF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 9:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 10:
                                    if (!ForceNonStandard)
                                        Race = RACE.ROCK_GNOME; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 54:
                                case 55:
                                case 56:
                                case 57:
                                case 58:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 59:
                                case 60:
                                case 61:
                                case 62:
                                case 63:
                                    if (!ForceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 74:
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 98:
                                    if (!ForceStandard)
                                        Race = RACE.DOPPELGANGER; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBOAR;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WERETIGER;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.WIZARD:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                    if (!ForceStandard)
                                        Race = RACE.GRAY_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                case 10:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 27:
                                case 28:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 29:
                                    if (!ForceNonStandard)
                                        Race = RACE.ROCK_GNOME; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 45:
                                case 46:
                                case 47:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 48:
                                case 49:
                                    if (!ForceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 50:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
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
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 98:
                                    if (!ForceStandard)
                                        Race = RACE.DOPPELGANGER; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBOAR;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WERETIGER;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                    } break;
                case Character.ALIGNMENT.EVIL:
                    switch (Class)
                    {
                        case Classes.CLASS.BARBARIAN:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                    if (!ForceStandard)
                                        Race = RACE.WILD_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 2:
                                case 3:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 4:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 5:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 6:
                                    if (!ForceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 7:
                                case 8:
                                case 9:
                                case 10:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 40:
                                case 41:
                                case 42:
                                case 43:
                                case 44:
                                    if (!ForceStandard)
                                        Race = RACE.LIZARDFOLK; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 45:
                                    if (!ForceStandard)
                                        Race = RACE.GOBLIN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 46:
                                    if (!ForceStandard)
                                        Race = RACE.HOBGOBLIN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 47:
                                    if (!ForceStandard)
                                        Race = RACE.KOBOLD; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
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
                                case 75:
                                case 76:
                                case 77:
                                    if (!ForceStandard)
                                        Race = RACE.ORC; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 78:
                                    if (!ForceStandard)
                                        Race = RACE.TIEFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 79:
                                case 80:
                                case 81:
                                case 82:
                                case 83:
                                    if (!ForceStandard)
                                        Race = RACE.GNOLL; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 84:
                                    if (!ForceStandard)
                                        Race = RACE.TROGLODYTE; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 85:
                                case 86:
                                    if (!ForceStandard)
                                        Race = RACE.BUGBEAR; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 87:
                                case 88:
                                case 89:
                                case 90:
                                    if (!ForceStandard)
                                        Race = RACE.OGRE; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 91:
                                case 92:
                                case 93:
                                case 94:
                                    if (!ForceStandard)
                                        Race = RACE.MINOTAUR; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 95:
                                case 96:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREWOLF;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 97:
                                case 98:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_FIEND;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.BARD:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 2:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                case 12:
                                case 13:
                                case 14:
                                case 15:
                                case 16:
                                case 17:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 18:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 19:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 20:
                                    if (!ForceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 21:
                                case 22:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
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
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 98:
                                    if (!ForceStandard)
                                        Race = RACE.GOBLIN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (!ForceStandard)
                                        Race = RACE.TIEFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREWOLF;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.CLERIC:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                case 2:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_DWARF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 3:
                                    if (!ForceNonStandard)
                                        Race = RACE.HILL_DWARF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 4:
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 5:
                                    if (!ForceStandard)
                                        Race = RACE.WILD_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 6:
                                case 7:
                                case 8:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 9:
                                case 10:
                                case 11:
                                case 12:
                                case 13:
                                case 14:
                                case 15:
                                case 16:
                                case 17:
                                case 18:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 19:
                                case 20:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 21:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 22:
                                    if (!ForceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 23:
                                case 24:
                                case 25:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
                                case 56:
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 57:
                                case 58:
                                case 59:
                                case 60:
                                case 61:
                                case 62:
                                case 63:
                                    if (!ForceStandard)
                                        Race = RACE.LIZARDFOLK; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 64:
                                    if (!ForceStandard)
                                        Race = RACE.GOBLIN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 65:
                                    if (!ForceStandard)
                                        Race = RACE.HOBGOBLIN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 66:
                                    if (!ForceStandard)
                                        Race = RACE.KOBOLD; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 67:
                                    if (!ForceStandard)
                                        Race = RACE.ORC; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 68:
                                    if (!ForceStandard)
                                        Race = RACE.TIEFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 69:
                                case 70:
                                case 71:
                                    if (!ForceStandard)
                                    {
                                        Race = RACE.DROW;
                                        Male = false;
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 72:
                                    if (!ForceStandard)
                                        Race = RACE.DUERGAR; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 73:
                                case 74:
                                    if (!ForceStandard)
                                        Race = RACE.GNOLL; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
                                case 81:
                                case 82:
                                case 83:
                                case 84:
                                case 85:
                                case 86:
                                case 87:
                                case 88:
                                case 89:
                                    if (!ForceStandard)
                                        Race = RACE.TROGLODYTE; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 90:
                                case 91:
                                    if (!ForceStandard)
                                        Race = RACE.BUGBEAR; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 92:
                                    if (!ForceStandard)
                                        Race = RACE.OGRE; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 93:
                                    if (!ForceStandard)
                                        Race = RACE.MINOTAUR; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 94:
                                    if (!ForceStandard)
                                        Race = RACE.MIND_FLAYER; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 95:
                                    if (!ForceStandard)
                                        Race = RACE.OGRE_MAGE; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 96:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WERERAT;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 97:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREWOLF;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 98:
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_FIEND;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.DRUID:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                case 2:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 3:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 4:
                                    if (!ForceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 5:
                                case 6:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 7:
                                case 8:
                                case 9:
                                case 10:
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
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
                                case 56:
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceStandard)
                                        Race = RACE.LIZARDFOLK; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 72:
                                    if (!ForceStandard)
                                        Race = RACE.GOBLIN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 73:
                                    if (!ForceStandard)
                                        Race = RACE.HOBGOBLIN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 74:
                                    if (!ForceStandard)
                                        Race = RACE.KOBOLD; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 75:
                                    if (!ForceStandard)
                                        Race = RACE.ORC;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
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
                                case 100:
                                    if (!ForceStandard)
                                        Race = RACE.GNOLL; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.FIGHTER:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                case 2:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_DWARF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 3:
                                case 4:
                                    if (!ForceNonStandard)
                                        Race = RACE.HILL_DWARF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 5:
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 6:
                                case 7:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                case 12:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 13:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 14:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 15:
                                case 16:
                                case 17:
                                case 18:
                                case 19:
                                case 20:
                                case 21:
                                case 22:
                                case 23:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 54:
                                    if (!ForceStandard)
                                        Race = RACE.LIZARDFOLK; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 55:
                                    if (!ForceStandard)
                                        Race = RACE.GOBLIN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
                                    if (!ForceStandard)
                                        Race = RACE.HOBGOBLIN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 81:
                                    if (!ForceStandard)
                                        Race = RACE.KOBOLD;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 82:
                                case 83:
                                case 84:
                                case 85:
                                case 86:
                                    if (!ForceStandard)
                                        Race = RACE.ORC; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 87:
                                case 88:
                                    if (!ForceStandard)
                                        Race = RACE.DROW; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 89:
                                    if (!ForceStandard)
                                        Race = RACE.DUERGAR; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 90:
                                    if (!ForceStandard)
                                        Race = RACE.DERRO_DWARF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 91:
                                    if (!ForceStandard)
                                        Race = RACE.GNOLL; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 92:
                                    if (!ForceStandard)
                                        Race = RACE.TROGLODYTE; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 93:
                                    if (!ForceStandard)
                                        Race = RACE.BUGBEAR; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 94:
                                    if (!ForceStandard)
                                        Race = RACE.OGRE; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 95:
                                    if (!ForceStandard)
                                        Race = RACE.MIND_FLAYER; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 96:
                                    if (!ForceStandard)
                                        Race = RACE.OGRE_MAGE;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 97:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WERERAT;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 98:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREWOLF;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_FIEND;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.MONK:
                            switch (Dice.Percentile(ref random))
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
                                case 10:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
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
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 91:
                                case 92:
                                case 93:
                                    if (!ForceStandard)
                                        Race = RACE.HOBGOBLIN; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 94:
                                    if (!ForceStandard)
                                        Race = RACE.TIEFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 95:
                                case 96:
                                    if (!ForceStandard)
                                        Race = RACE.OGRE_MAGE; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 97:
                                case 98:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WERERAT;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_FIEND;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.PALADIN:
                            switch (Dice.Percentile(ref random))
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
                                case 10:
                                    if (!ForceStandard)
                                    {
                                        Race = RACE.DROW;
                                        Male = false;
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HILL_DWARF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 21:
                                    if (!ForceStandard)
                                        Race = RACE.DUERGAR; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 22:
                                    if (!ForceStandard)
                                        Race = RACE.DERRO_DWARF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 23:
                                case 24:
                                case 25:
                                case 26:
                                case 27:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 28:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 29:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 30:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
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
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 98:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREWOLF;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_FIEND;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.RANGER:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 29:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 30:
                                    if (!ForceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 31:
                                case 32:
                                case 33:
                                case 34:
                                case 35:
                                case 36:
                                case 37:
                                case 38:
                                case 39:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 40:
                                case 41:
                                case 42:
                                case 43:
                                case 44:
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 70:
                                case 71:
                                    if (!ForceStandard)
                                        Race = RACE.LIZARDFOLK;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 72:
                                    if (!ForceStandard)
                                        Race = RACE.HOBGOBLIN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 73:
                                case 74:
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
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
                                    if (!ForceStandard)
                                        Race = RACE.GNOLL;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 93:
                                    if (!ForceStandard)
                                        Race = RACE.BUGBEAR;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 94:
                                    if (!ForceStandard)
                                        Race = RACE.TROGLODYTE;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 95:
                                    if (!ForceStandard)
                                        Race = RACE.OGRE;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 96:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WERERAT;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 97:
                                case 98:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREWOLF;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_FIEND;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.SORCERER:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                    if (!ForceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                case 12:
                                case 13:
                                case 14:
                                case 15:
                                case 16:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 17:
                                case 18:
                                case 19:
                                case 20:
                                case 21:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 22:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 23:
                                    if (!ForceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 24:
                                case 25:
                                case 26:
                                case 27:
                                case 28:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 69:
                                    if (!ForceStandard)
                                        Race = RACE.LIZARDFOLK;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 70:
                                    if (!ForceStandard)
                                        Race = RACE.GOBLIN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 71:
                                    if (!ForceStandard)
                                        Race = RACE.HOBGOBLIN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 72:
                                case 73:
                                case 74:
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
                                case 81:
                                case 82:
                                case 83:
                                case 84:
                                case 85:
                                case 86:
                                    if (!ForceStandard)
                                        Race = RACE.KOBOLD;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 87:
                                    if (!ForceStandard)
                                        Race = RACE.GNOLL;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 88:
                                case 89:
                                case 90:
                                    if (!ForceStandard)
                                        Race = RACE.TROGLODYTE;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 91:
                                    if (!ForceStandard)
                                        Race = RACE.BUGBEAR;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 92:
                                    if (!ForceStandard)
                                        Race = RACE.OGRE;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 93:
                                    if (!ForceStandard)
                                        Race = RACE.MINOTAUR;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 94:
                                    if (!ForceStandard)
                                        Race = RACE.MIND_FLAYER;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 95:
                                    if (!ForceStandard)
                                        Race = RACE.OGRE_MAGE;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 96:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WERERAT;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 97:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREWOLF;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 98:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_FIEND;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.THIEF:
                            switch (Dice.Percentile(ref random))
                            {
                                case 1:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_DWARF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 2:
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 3:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 4:
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                case 12:
                                case 13:
                                case 14:
                                case 15:
                                case 16:
                                case 17:
                                case 18:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 35:
                                case 36:
                                case 37:
                                case 38:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 39:
                                    if (!ForceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 40:
                                    if (!ForceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 41:
                                case 42:
                                case 43:
                                case 44:
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 71:
                                case 72:
                                case 73:
                                case 74:
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                case 79:
                                case 80:
                                case 81:
                                case 82:
                                case 83:
                                case 84:
                                case 85:
                                    if (!ForceStandard)
                                        Race = RACE.GOBLIN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 86:
                                    if (!ForceStandard)
                                        Race = RACE.HOBGOBLIN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 87:
                                    if (!ForceStandard)
                                        Race = RACE.KOBOLD;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 88:
                                case 89:
                                    if (!ForceStandard)
                                        Race = RACE.TIEFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 90:
                                case 91:
                                case 92:
                                case 93:
                                    if (!ForceStandard)
                                        Race = RACE.BUGBEAR;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 94:
                                    if (!ForceStandard)
                                        Race = RACE.MIND_FLAYER;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 95:
                                case 96:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WERERAT;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 97:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREWOLF;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 98:
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_FIEND;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case Classes.CLASS.WIZARD:
                            switch (Dice.Percentile(ref random))
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
                                case 10:
                                    if (!ForceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 11:
                                    if (!ForceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 27:
                                    if (!ForceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 28:
                                    if (!ForceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING; 
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 55:
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
                                case 75:
                                case 76:
                                case 77:
                                case 78:
                                    if (!ForceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 79:
                                case 80:
                                    if (!ForceStandard)
                                        Race = RACE.HOBGOBLIN;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 81:
                                    if (!ForceStandard)
                                        Race = RACE.TIEFLING;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
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
                                    if (!ForceStandard)
                                    {
                                        Race = RACE.DROW; 
                                        Male = true;
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 92:
                                    if (!ForceStandard)
                                        Race = RACE.GNOLL;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 93:
                                    if (!ForceStandard)
                                        Race = RACE.BUGBEAR;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 94:
                                    if (!ForceStandard)
                                        Race = RACE.MIND_FLAYER;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 95:
                                case 96:
                                    if (!ForceStandard)
                                        Race = RACE.OGRE_MAGE;
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 97:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WERERAT;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 98:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREWOLF;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 99:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_FIEND;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                case 100:
                                    if (AllowMetaRaces || ForceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(Alignment, Class, false, false, false, ref random);
                                    }
                                    else
                                        RaceByAlignment(Alignment, Class, AllowMetaRaces, ForceStandard, ForceNonStandard, ref random);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                    } break;
                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
            }
        }

        public static METARACE RandomMetaRace(Character.ALIGNMENT Alignment, Classes.CLASS Class, METARACE_RANDOMIZER Randomizer, ref Random random)
        {
            int Roll;
            switch (Randomizer)
            {
                case METARACE_RANDOMIZER.ANY:
                    switch (Alignment)
                    {
                        case Character.ALIGNMENT.GOOD:
                            switch (Class)
                            {
                                case Classes.CLASS.BARBARIAN:
                                case Classes.CLASS.BARD:
                                    if (Dice.Percentile(ref random) < 51)
                                        return METARACE.HALF_CELESTIAL;
                                    return METARACE.HALF_DRAGON;
                                case Classes.CLASS.CLERIC:
                                    Roll = Dice.Percentile(ref random);
                                    if (Roll < 51)
                                        return METARACE.HALF_CELESTIAL;
                                    else if (Roll < 76)
                                        return METARACE.HALF_DRAGON;
                                    return METARACE.WEREBEAR;
                                case Classes.CLASS.DRUID: return METARACE.HALF_CELESTIAL;
                                case Classes.CLASS.FIGHTER:
                                case Classes.CLASS.MONK:
                                case Classes.CLASS.PALADIN:
                                case Classes.CLASS.RANGER:
                                case Classes.CLASS.THIEF:
                                case Classes.CLASS.WIZARD:
                                    Roll = Dice.d6(ref random);
                                    if (Roll < 3)
                                        return METARACE.HALF_CELESTIAL;
                                    else if (Roll < 5)
                                        return METARACE.HALF_DRAGON;
                                    return METARACE.WEREBEAR;
                                case Classes.CLASS.SORCERER:
                                    Roll = Dice.Percentile(ref random);
                                    if (Roll < 26)
                                        return METARACE.HALF_CELESTIAL;
                                    else if (Roll < 76)
                                        return METARACE.HALF_DRAGON;
                                    return METARACE.WEREBEAR;
                                default: return METARACE.NONE;
                            }
                        case Character.ALIGNMENT.NEUTRAL:
                            switch (Class)
                            {
                                case Classes.CLASS.BARBARIAN:
                                case Classes.CLASS.BARD:
                                case Classes.CLASS.CLERIC:
                                case Classes.CLASS.DRUID:
                                case Classes.CLASS.FIGHTER:
                                case Classes.CLASS.PALADIN:
                                case Classes.CLASS.RANGER:
                                case Classes.CLASS.SORCERER:
                                case Classes.CLASS.THIEF:
                                case Classes.CLASS.WIZARD:
                                    if (Dice.Percentile(ref random) < 51)
                                        return METARACE.WEREBOAR;
                                    return METARACE.WERETIGER;
                                default: return METARACE.NONE;
                            }
                        case Character.ALIGNMENT.EVIL:
                            switch (Class)
                            {
                                case Classes.CLASS.BARBARIAN:
                                    Roll = Dice.d6(ref random);
                                    if (Roll < 3)
                                        return METARACE.WEREWOLF;
                                    else if (Roll < 5)
                                        return METARACE.HALF_FIEND;
                                    return METARACE.HALF_DRAGON;
                                case Classes.CLASS.BARD: return METARACE.WEREWOLF;
                                case Classes.CLASS.CLERIC:
                                    Roll = Dice.Percentile(ref random);
                                    if (Roll < 21)
                                        return METARACE.WERERAT;
                                    else if (Roll < 41)
                                        return METARACE.WEREWOLF;
                                    else if (Roll < 81)
                                        return METARACE.HALF_FIEND;
                                    return METARACE.HALF_DRAGON;
                                case Classes.CLASS.FIGHTER:
                                case Classes.CLASS.WIZARD:
                                    Roll = Dice.Percentile(ref random);
                                    if (Roll < 26)
                                        return METARACE.WERERAT;
                                    else if (Roll < 51)
                                        return METARACE.WEREWOLF;
                                    else if (Roll < 76)
                                        return METARACE.HALF_FIEND;
                                    return METARACE.HALF_DRAGON;
                                case Classes.CLASS.MONK:
                                    Roll = Dice.Percentile(ref random);
                                    if (Roll < 51)
                                        return METARACE.WERERAT;
                                    else if (Roll < 76)
                                        return METARACE.HALF_FIEND;
                                    return METARACE.HALF_DRAGON;
                                case Classes.CLASS.PALADIN: return METARACE.HALF_DRAGON;
                                case Classes.CLASS.RANGER:
                                    Roll = Dice.Percentile(ref random);
                                    if (Roll < 21)
                                        return METARACE.WERERAT;
                                    else if (Roll < 61)
                                        return METARACE.WEREWOLF;
                                    else if (Roll < 81)
                                        return METARACE.HALF_FIEND;
                                    return METARACE.HALF_DRAGON;
                                case Classes.CLASS.SORCERER:
                                    Roll = Dice.Percentile(ref random);
                                    if (Roll < 21)
                                        return METARACE.WERERAT;
                                    else if (Roll < 41)
                                        return METARACE.WEREWOLF;
                                    else if (Roll < 61)
                                        return METARACE.HALF_FIEND;
                                    return METARACE.HALF_DRAGON;
                                case Classes.CLASS.THIEF:
                                    Roll = Dice.d6(ref random);
                                    if (Roll < 3)
                                        return METARACE.WERERAT;
                                    else if (Roll < 4)
                                        return METARACE.WEREWOLF;
                                    else if (Roll < 6)
                                        return METARACE.HALF_FIEND;
                                    return METARACE.HALF_DRAGON;
                                default: return METARACE.NONE;
                            }
                        default: return METARACE.NONE;
                    }
                case METARACE_RANDOMIZER.ANY_EVIL: return RandomMetaRace(Character.ALIGNMENT.EVIL, Class, METARACE_RANDOMIZER.ANY, ref random);
                case METARACE_RANDOMIZER.ANY_GOOD: return RandomMetaRace(Character.ALIGNMENT.GOOD, Class, METARACE_RANDOMIZER.ANY, ref random);
                case METARACE_RANDOMIZER.ANY_LYCANTHROPE:
                    switch (Alignment)
                    {
                        case Character.ALIGNMENT.GOOD:
                            switch (Class)
                            {
                                case Classes.CLASS.CLERIC:                                   
                                case Classes.CLASS.FIGHTER:
                                case Classes.CLASS.MONK:
                                case Classes.CLASS.PALADIN:
                                case Classes.CLASS.RANGER:
                                case Classes.CLASS.THIEF:
                                case Classes.CLASS.WIZARD:
                                case Classes.CLASS.SORCERER: return METARACE.WEREBEAR;
                                default: return METARACE.NONE;
                            }
                        case Character.ALIGNMENT.NEUTRAL:
                            switch (Class)
                            {
                                case Classes.CLASS.BARBARIAN:
                                case Classes.CLASS.BARD:
                                case Classes.CLASS.CLERIC:
                                case Classes.CLASS.DRUID:
                                case Classes.CLASS.FIGHTER:
                                case Classes.CLASS.PALADIN:
                                case Classes.CLASS.RANGER:
                                case Classes.CLASS.SORCERER:
                                case Classes.CLASS.THIEF:
                                case Classes.CLASS.WIZARD:
                                    if (Dice.Percentile(ref random) < 51)
                                        return METARACE.WEREBOAR;
                                    return METARACE.WERETIGER;
                                default: return METARACE.NONE;
                            }
                        case Character.ALIGNMENT.EVIL:
                            switch (Class)
                            {
                                case Classes.CLASS.BARBARIAN:
                                case Classes.CLASS.BARD: return METARACE.WEREWOLF;
                                case Classes.CLASS.CLERIC:
                                case Classes.CLASS.FIGHTER:
                                case Classes.CLASS.WIZARD:
                                case Classes.CLASS.SORCERER:
                                    if (Dice.Percentile(ref random) < 51)
                                        return METARACE.WERERAT;
                                    return METARACE.WEREWOLF;
                                case Classes.CLASS.MONK: return METARACE.WERERAT;
                                case Classes.CLASS.RANGER:
                                    if (Dice.d6(ref random) < 3)
                                        return METARACE.WERERAT;
                                    return METARACE.WEREWOLF;
                                case Classes.CLASS.THIEF:
                                    if (Dice.d6(ref random) < 3)
                                        return METARACE.WEREWOLF;
                                    return METARACE.WERERAT;
                                default: return METARACE.NONE;
                            }
                        default: return METARACE.NONE;
                    }
                case METARACE_RANDOMIZER.ANY_NEUTRAL: return RandomMetaRace(Character.ALIGNMENT.NEUTRAL, Class, METARACE_RANDOMIZER.ANY, ref random);
                case METARACE_RANDOMIZER.ANY_NONEVIL:
                    if (Alignment != Character.ALIGNMENT.EVIL)
                        return RandomMetaRace(Alignment, Class, METARACE_RANDOMIZER.ANY, ref random);
                    return METARACE.NONE;
                case METARACE_RANDOMIZER.ANY_NONGOOD:
                    if (Alignment != Character.ALIGNMENT.GOOD)
                        return RandomMetaRace(Alignment, Class, METARACE_RANDOMIZER.ANY, ref random);
                    return METARACE.NONE;
                case METARACE_RANDOMIZER.ANY_NONLYCANTHROPE:
                    switch (Alignment)
                    {
                        case Character.ALIGNMENT.GOOD:
                            switch (Class)
                            {
                                case Classes.CLASS.BARBARIAN:
                                case Classes.CLASS.BARD:
                                case Classes.CLASS.FIGHTER:
                                case Classes.CLASS.MONK:
                                case Classes.CLASS.PALADIN:
                                case Classes.CLASS.RANGER:
                                case Classes.CLASS.THIEF:
                                case Classes.CLASS.WIZARD:
                                    if (Dice.Percentile(ref random) < 51)
                                        return METARACE.HALF_CELESTIAL;
                                    return METARACE.HALF_DRAGON;
                                case Classes.CLASS.CLERIC:
                                    if (Dice.d6(ref random) < 5)
                                        return METARACE.HALF_CELESTIAL;
                                    return METARACE.HALF_DRAGON;
                                case Classes.CLASS.DRUID: return METARACE.HALF_CELESTIAL;
                                case Classes.CLASS.SORCERER:
                                    if (Dice.d6(ref random) < 3)
                                        return METARACE.HALF_CELESTIAL;
                                    return METARACE.HALF_DRAGON;
                                default: return METARACE.NONE;
                            }
                        case Character.ALIGNMENT.EVIL:
                            switch (Class)
                            {
                                case Classes.CLASS.BARBARIAN:
                                case Classes.CLASS.FIGHTER:
                                case Classes.CLASS.WIZARD:
                                case Classes.CLASS.MONK:
                                case Classes.CLASS.RANGER:
                                    if (Dice.d6(ref random) < 4)
                                        return METARACE.HALF_FIEND;
                                    return METARACE.HALF_DRAGON;
                                case Classes.CLASS.CLERIC:
                                case Classes.CLASS.THIEF:
                                    if (Dice.d6(ref random) < 5)
                                        return METARACE.HALF_FIEND;
                                    return METARACE.HALF_DRAGON;
                                case Classes.CLASS.PALADIN: return METARACE.HALF_DRAGON;
                                case Classes.CLASS.SORCERER:
                                    if (Dice.d6(ref random) < 3)
                                        return METARACE.HALF_FIEND;
                                    return METARACE.HALF_DRAGON;
                                default: return METARACE.NONE;
                            }
                        default: return METARACE.NONE;
                    }
                case METARACE_RANDOMIZER.ANY_NONNEUTRAL:
                    if (Alignment != Character.ALIGNMENT.NEUTRAL)
                        return RandomMetaRace(Alignment, Class, METARACE_RANDOMIZER.ANY, ref random);
                    return METARACE.NONE;
                default: return METARACE.NONE;
            }
        }

        public bool CanSpeak(string Language)
        {
            switch (Race)
            {
                case RACE.AASIMAR:
                    switch (Language)
                    {
                        case "Draconic":
                        case "Dwarven":
                        case "Elven":
                        case "Gnome":
                        case "Halfling":
                        case "Sylvan": return true;
                        default: return false;
                    }
                case RACE.BUGBEAR:
                case RACE.GOBLIN:
                    switch (Language)
                    {
                        case "Draconic":
                        case "Elven":
                        case "Giant":
                        case "Gnoll":
                        case "Orc": return true;
                        default: return false;
                    }
                case RACE.DEEP_DWARF:
                case RACE.HILL_DWARF:
                case RACE.DERRO_DWARF:
                case RACE.MOUNTAIN_DWARF:
                    switch (Language)
                    {
                        case "Giant":
                        case "Goblin":
                        case "Orc":
                        case "Gnome":
                        case "Terran":
                        case "Undercommon": return true;
                        default: return false;
                    }
                case RACE.DOPPELGANGER:
                    switch (Language)
                    {
                        case "Auran":
                        case "Dwarven":
                        case "Elven":
                        case "Gnome":
                        case "Giant":
                        case "Halfling":
                        case "Terran": return true;
                        default: return false;
                    }
                case RACE.DROW:
                    switch (Language)
                    {
                        case "Abyssal":
                        case "Aquan":
                        case "Draconic":
                        case "Gnome":
                        case "Goblin": return true;
                        default: return false;
                    }
                case RACE.DUERGAR:
                    switch (Language)
                    {
                        case "Giant":
                        case "Goblin":
                        case "Orc":
                        case "Draconic":
                        case "Terran": return true;
                        default: return false;
                    }
                case RACE.GNOLL:
                    switch (Language)
                    {
                        case "Common":
                        case "Goblin":
                        case "Orc":
                        case "Draconic":
                        case "Elven": return true;
                        default: return false;
                    }
                case RACE.MIND_FLAYER:
                case RACE.HUMAN:
                case RACE.HALFELF: return true;
                case RACE.HALFORC:
                    switch (Language)
                    {
                        case "Abyssal":
                        case "Goblin":
                        case "Gnoll":
                        case "Draconic":
                        case "Giant": return true;
                        default: return false;
                    }
                case RACE.HIGH_ELF:
                case RACE.GRAY_ELF:
                case RACE.WILD_ELF:
                case RACE.WOOD_ELF:
                    switch (Language)
                    {
                        case "Draconic":
                        case "Gnoll":
                        case "Goblin":
                        case "Gnome":
                        case "Orc":
                        case "Sylvan": return true;
                        default: return false;
                    }
                case RACE.HOBGOBLIN:
                    switch (Language)
                    {
                        case "Dwarven":
                        case "Infernal":
                        case "Orc":
                        case "Draconic":
                        case "Giant": return true;
                        default: return false;
                    }
                case RACE.KOBOLD:
                    switch (Language)
                    {
                        case "Common":
                        case "Undercommon":return true;
                        default: return false;
                    }
                case RACE.LIGHTFOOT_HALFLING:
                case RACE.TALLFELLOW_HALFLING:
                case RACE.DEEP_HALFLING:
                    switch (Language)
                    {
                        case "Dwarven":
                        case "Elven":
                        case "Gnome":
                        case "Goblin":
                        case "Orc": return true;
                        default: return false;
                    }
                case RACE.LIZARDFOLK:
                    switch (Language)
                    {
                        case "Aquan":
                        case "Goblin":
                        case "Orc":
                        case "Gnoll": return true;
                        default: return false;
                    }
                case RACE.MINOTAUR:
                    switch (Language)
                    {
                        case "Terran":
                        case "Goblin":
                        case "Orc": return true;
                        default: return false;
                    }
                case RACE.OGRE:
                    switch (Language)
                    {
                        case "Dwarven":
                        case "Goblin":
                        case "Terran":
                        case "Orc": return true;
                        default: return false;
                    }
                case RACE.OGRE_MAGE:
                    switch (Language)
                    {
                        case "Dwarven":
                        case "Goblin":
                        case "Orc":
                        case "Infernal": return true;
                        default: return false;
                    }
                case RACE.ORC:
                    switch (Language)
                    {
                        case "Dwarven":
                        case "Giant":
                        case "Gnoll":
                        case "Goblin":
                        case "Undercommon": return true;
                        default: return false;
                    }
                case RACE.FOREST_GNOME:
                case RACE.ROCK_GNOME:
                    switch (Language)
                    {
                        case "Dwarven":
                        case "Elven":
                        case "Draconic":
                        case "Goblin":
                        case "Giant":
                        case "Orc": return true;
                        default: return false;
                    }
                case RACE.SVIRFNEBLIN:
                    switch (Language)
                    {
                        case "Dwarven":
                        case "Elven":
                        case "Terran":
                        case "Goblin":
                        case "Giant":
                        case "Orc": return true;
                        default: return false;
                    }
                case RACE.TIEFLING:
                    switch (Language)
                    {
                        case "Draconic":
                        case "Dwarven":
                        case "Elven":
                        case "Gnome":
                        case "Goblin":
                        case "Halfling":
                        case "Orc": return true;
                        default: return false;
                    }
                case RACE.TROGLODYTE:
                    switch (Language)
                    {
                        case "Common":
                        case "Goblin":
                        case "Orc":
                        case "Giant": return true;
                        default: return false;
                    }
                default: return true;
            }
        }

        public override string ToString()
        {
            string MetaRaceString = "";
            if (MetaRace != METARACE.NONE)
            {
                MetaRaceString = Enum.GetName(typeof(METARACE), MetaRace) + " ";
            }
            
            return String.Format("{0} {1}{2}", Gender, MetaRaceString, Enum.GetName(typeof(RACE), Race));
        }
    }
}
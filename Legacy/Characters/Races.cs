using NPCGen.Roll;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPCGen.Characters
{
    public enum RACE
    {
        AASIMAR,
        DEEP_DWARF, HILL_DWARF, MOUNTAIN_DWARF,
        GRAY_ELF, HIGH_ELF, WILD_ELF, WOOD_ELF, HALFELF,
        FOREST_GNOME, ROCK_GNOME, SVIRFNEBLIN,
        HALFORC, LIGHTFOOT_HALFLING, DEEP_HALFLING, TALLFELLOW_HALFLING,
        HUMAN, LIZARDFOLK, DOPPELGANGER, GOBLIN, HOBGOBLIN, KOBOLD, ORC,
        TIEFLING, DROW, DUERGAR, DERRO_DWARF, GNOLL, TROGLODYTE, BUGBEAR, OGRE, MINOTAUR, MIND_FLAYER, OGRE_MAGE
    };

    public enum METARACE { NONE, HALF_CELESTIAL, HALF_DRAGON, WEREBEAR, WEREBOAR, WERETIGER, WERERAT, WEREWOLF, HALF_FIEND };
    public enum METARACE_RANDOMIZER
    {
        ANY, MAYBE, ANY_LYCANTHROPE, ANY_NONLYCANTHROPE,
        ANY_GOOD, ANY_NEUTRAL, ANY_EVIL, ANY_NONGOOD, ANY_NONNEUTRAL, ANY_NONEVIL
    };
    public enum RACE_RANDOMIZER
    {
        ANY, ANY_STANDARD, ANY_NONSTANDARD,
        ANY_EVIL, ANY_GOOD, ANY_NEUTRAL, ANY_NONEVIL, ANY_NONNEUTRAL, ANY_NONGOOD
    };

    public class Races
    {
        public RACE Race;
        public METARACE MetaRace;
        public Boolean Male;

        public String Gender
        {
            get
            {
                if (!Male)
                    return "Female";
                return "Male";
            }
        }

        public static String[] RacesArray { get { return Enum.GetNames(typeof(RACE)); } }
        public static String[] MetaRacesArray { get { return Enum.GetNames(typeof(METARACE)); } }
        public static String[] RaceRandomizerArray { get { return Enum.GetNames(typeof(RACE_RANDOMIZER)); } }
        public static String[] MetaRaceRandomizerArray { get { return Enum.GetNames(typeof(METARACE_RANDOMIZER)); } }

        public String RacialTraits
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
                    default: return String.Empty;
                }
            }
        }

        public String MetaRacialTraits
        {
            get
            {
                switch (MetaRace)
                {
                    case METARACE.HALF_CELESTIAL:
                        var output = "AC 1 better";
                        if (Dice.Percentile() <= 75)
                            output += ", angel wings (fly at double speed)";
                        return output;
                    case METARACE.HALF_DRAGON: return "If large, has wings (can fly)";
                    case METARACE.HALF_FIEND: 
                        output = "AC 1 better";
                        if (Dice.Percentile() <= 50)
                            output += ", bat wings (can fly)";
                        return output;
                    case METARACE.WEREBOAR:
                    case METARACE.WERERAT:
                    case METARACE.WERETIGER:
                    case METARACE.WEREWOLF:
                    case METARACE.WEREBEAR: return "AC 2 better";
                    default: return String.Empty;
                }
            }
        }

        public Races(ALIGNMENT alignment, CLASS charClass, RACE_RANDOMIZER randomizer, Boolean allowMetaRaces, Boolean male)
        {
            switch (randomizer)
            {
                case RACE_RANDOMIZER.ANY_STANDARD: RaceByAlignment(alignment, charClass, allowMetaRaces, true, false); break;
                case RACE_RANDOMIZER.ANY_NONSTANDARD: RaceByAlignment(alignment, charClass, allowMetaRaces, false, true); break;
                case RACE_RANDOMIZER.ANY_EVIL: RaceByAlignment(ALIGNMENT.EVIL, charClass, allowMetaRaces, false, false); break;
                case RACE_RANDOMIZER.ANY_GOOD: RaceByAlignment(ALIGNMENT.GOOD, charClass, allowMetaRaces, false, false); break;
                case RACE_RANDOMIZER.ANY_NEUTRAL: RaceByAlignment(ALIGNMENT.NEUTRAL, charClass, allowMetaRaces, false, false); break;
                case RACE_RANDOMIZER.ANY_NONEVIL:
                    if (alignment != ALIGNMENT.EVIL)
                        RaceByAlignment(alignment, charClass, allowMetaRaces, false, false);
                    else
                    {
                        if (Dice.Roll(1, 5, 0) > 3)
                            RaceByAlignment(ALIGNMENT.GOOD, charClass, allowMetaRaces, false, false);
                        else
                            RaceByAlignment(ALIGNMENT.NEUTRAL, charClass, allowMetaRaces, false, false);
                    } break;
                case RACE_RANDOMIZER.ANY_NONGOOD:
                    if (alignment != ALIGNMENT.GOOD)
                        RaceByAlignment(alignment, charClass, allowMetaRaces, false, false);
                    else
                    {
                        if (Dice.d8() > 3)
                            RaceByAlignment(ALIGNMENT.EVIL, charClass, allowMetaRaces, false, false);
                        else
                            RaceByAlignment(ALIGNMENT.NEUTRAL, charClass, allowMetaRaces, false, false);
                    } break;
                case RACE_RANDOMIZER.ANY_NONNEUTRAL:
                    if (alignment != ALIGNMENT.EVIL)
                        RaceByAlignment(alignment, charClass, allowMetaRaces, false, false);
                    else
                    {
                        if (Dice.Roll(1, 7, 0) > 2)
                            RaceByAlignment(ALIGNMENT.EVIL, charClass, allowMetaRaces, false, false);
                        else
                            RaceByAlignment(ALIGNMENT.GOOD, charClass, allowMetaRaces, false, false);
                    } break;
                default: RaceByAlignment(alignment, charClass, allowMetaRaces, false, false); break;
            }

            Male = male;
        }

        public Races()
        {
            Race = RACE.HUMAN;
            MetaRace = METARACE.NONE;
            Male = true;
        }

        private void RaceByAlignment(ALIGNMENT alignment, CLASS charClass, Boolean allowMetaRaces, Boolean forceStandard, Boolean forceNonStandard)
        {
            switch (alignment)
            {
                case ALIGNMENT.GOOD:
                    switch (charClass)
                    {
                        case CLASS.BARBARIAN:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                case 2:
                                    if (!forceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 33:
                                case 34:
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 35:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 36:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.BARD:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                    if (!forceStandard)
                                        Race = RACE.AASIMAR;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                    if (!forceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 7:
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                    if (!forceStandard)
                                        Race = RACE.GRAY_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 37:
                                    if (!forceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 38:
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 39:
                                    if (!forceStandard)
                                        Race = RACE.FOREST_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 40:
                                case 41:
                                case 42:
                                case 43:
                                case 44:
                                    if (!forceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 54:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 55:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 56:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 57:
                                    if (!forceNonStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 98:
                                    if (!forceStandard)
                                        Race = RACE.SVIRFNEBLIN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces)
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                    RaceByAlignment(alignment, charClass, false, false, false);
                                    break;
                                case 100:
                                    if (allowMetaRaces)
                                        MetaRace = METARACE.HALF_DRAGON;
                                    RaceByAlignment(alignment, charClass, false, false, false);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.CLERIC:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                    if (!forceStandard)
                                        Race = RACE.AASIMAR;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 2:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 23:
                                case 24:
                                    if (!forceStandard)
                                        Race = RACE.MOUNTAIN_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 25:
                                    if (!forceStandard)
                                        Race = RACE.GRAY_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 36:
                                case 37:
                                case 38:
                                case 39:
                                case 40:
                                    if (!forceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 41:
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 42:
                                    if (!forceStandard)
                                        Race = RACE.FOREST_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 52:
                                case 53:
                                case 54:
                                case 55:
                                case 56:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 67:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 68:
                                case 69:
                                    if (!forceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 70:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 96:
                                    if (!forceStandard)
                                        Race = RACE.SVIRFNEBLIN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 97:
                                case 98:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBEAR;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.DRUID:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                    if (!forceStandard)
                                        Race = RACE.GRAY_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 32:
                                case 33:
                                case 34:
                                case 35:
                                case 36:
                                    if (!forceStandard)
                                        Race = RACE.FOREST_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 37:
                                    if (!forceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 47:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 48:
                                    if (!forceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 49:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.FIGHTER:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                case 2:
                                case 3:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 34:
                                case 35:
                                case 36:
                                case 37:
                                case 38:
                                case 39:
                                case 40:
                                case 41:
                                    if (!forceStandard)
                                        Race = RACE.MOUNTAIN_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 42:
                                    if (!forceStandard)
                                        Race = RACE.GRAY_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 43:
                                case 44:
                                case 45:
                                case 46:
                                case 47:
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 48:
                                    if (!forceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 49:
                                case 50:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 51:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 52:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 53:
                                case 54:
                                case 55:
                                case 56:
                                case 57:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 98:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBEAR;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.MONK:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                case 2:
                                    if (!forceStandard)
                                        Race = RACE.AASIMAR;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 3:
                                    if (!forceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 14:
                                case 15:
                                case 16:
                                case 17:
                                case 18:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 19:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 20:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 21:
                                case 22:
                                case 23:
                                case 24:
                                case 25:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 98:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBEAR;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.PALADIN:
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
                                case 10:
                                    if (!forceStandard)
                                        Race = RACE.AASIMAR;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 21:
                                    if (!forceStandard)
                                        Race = RACE.MOUNTAIN_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 22:
                                    if (!forceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 23:
                                case 24:
                                case 25:
                                case 26:
                                case 27:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 28:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 29:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 30:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 98:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBEAR;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.RANGER:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                    if (!forceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 21:
                                    if (!forceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 37:
                                case 38:
                                case 39:
                                case 40:
                                case 41:
                                    if (!forceStandard)
                                        Race = RACE.FOREST_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 42:
                                    if (!forceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 58:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 59:
                                    if (!forceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 60:
                                case 61:
                                case 62:
                                case 63:
                                case 64:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 98:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBEAR;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.SORCERER:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                case 2:
                                    if (!forceStandard)
                                        Race = RACE.AASIMAR;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 3:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 4:
                                case 5:
                                    if (!forceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 6:
                                    if (!forceStandard)
                                        Race = RACE.MOUNTAIN_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 7:
                                case 8:
                                    if (!forceStandard)
                                        Race = RACE.GRAY_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 9:
                                case 10:
                                case 11:
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 37:
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 38:
                                    if (!forceStandard)
                                        Race = RACE.FOREST_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 39:
                                case 40:
                                    if (!forceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 41:
                                case 42:
                                case 43:
                                case 44:
                                case 45:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 55:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 56:
                                    if (!forceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 57:
                                case 58:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 96:
                                    if (!forceStandard)
                                        Race = RACE.SVIRFNEBLIN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 97:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 98:
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBEAR;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.THIEF:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                    if (!forceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 6:
                                    if (!forceStandard)
                                        Race = RACE.MOUNTAIN_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 20:
                                    if (!forceStandard)
                                        Race = RACE.FOREST_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 21:
                                case 22:
                                case 23:
                                case 24:
                                case 25:
                                    if (!forceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 61:
                                case 62:
                                case 63:
                                case 64:
                                case 65:
                                case 66:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 67:
                                case 68:
                                case 69:
                                case 70:
                                case 71:
                                case 72:
                                    if (!forceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 73:
                                case 74:
                                case 75:
                                case 76:
                                case 77:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 97:
                                    if (!forceStandard)
                                        Race = RACE.SVIRFNEBLIN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 98:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBEAR;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.WIZARD:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                    if (!forceStandard)
                                        Race = RACE.AASIMAR;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 2:
                                    if (!forceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                case 7:
                                    if (!forceStandard)
                                        Race = RACE.GRAY_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 42:
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 43:
                                    if (!forceStandard)
                                        Race = RACE.FOREST_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 44:
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                    if (!forceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 59:
                                case 60:
                                case 61:
                                case 62:
                                case 63:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 64:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 65:
                                case 66:
                                case 67:
                                    if (!forceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 68:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 97:
                                    if (!forceStandard)
                                        Race = RACE.SVIRFNEBLIN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 98:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_CELESTIAL;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBEAR;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                    } break;
                case ALIGNMENT.NEUTRAL:
                    switch (charClass)
                    {
                        case CLASS.BARBARIAN:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 2:
                                    if (!forceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 14:
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 15:
                                case 16:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 17:
                                case 18:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 19:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                        Race = RACE.LIZARDFOLK;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBOAR;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WERETIGER;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.BARD:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 2:
                                case 3:
                                    if (!forceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 4:
                                case 5:
                                    if (!forceStandard)
                                        Race = RACE.GRAY_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 16:
                                    if (!forceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 17:
                                case 18:
                                case 19:
                                case 20:
                                case 21:
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 22:
                                case 23:
                                    if (!forceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 34:
                                case 35:
                                case 36:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 37:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 38:
                                    if (!forceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 39:
                                case 40:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBOAR;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WERETIGER;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.CLERIC:
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
                                case 10:
                                case 11:
                                case 12:
                                case 13:
                                case 14:
                                case 15:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_DWARF; 
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 26:
                                    if (!forceStandard)
                                        Race = RACE.MOUNTAIN_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 27:
                                    if (!forceStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 28:
                                    if (!forceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 39:
                                    if (!forceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 59:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 60:
                                    if (!forceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 61:
                                case 62:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 91:
                                case 92:
                                case 93:
                                case 94:
                                case 95:
                                case 96:
                                case 97:
                                    if (!forceStandard)
                                        Race = RACE.LIZARDFOLK;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 98:
                                    if (!forceStandard)
                                        Race = RACE.DOPPELGANGER;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBOAR;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WERETIGER;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.DRUID:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                    if (!forceStandard)
                                        Race = RACE.GRAY_ELF; 
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 7:
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                    if (!forceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 32:
                                    if (!forceStandard)
                                        Race = RACE.FOREST_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 33:
                                case 34:
                                case 35:
                                case 36:
                                case 37:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 38:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 39:
                                    if (!forceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 40:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                        Race = RACE.LIZARDFOLK;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBOAR;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WERETIGER;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.FIGHTER:
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
                                case 10:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_DWARF; 
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 30:
                                case 31:
                                case 32:
                                case 33:
                                case 34:
                                    if (!forceStandard)
                                        Race = RACE.MOUNTAIN_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 35:
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 36:
                                case 37:
                                case 38:
                                case 39:
                                case 40:
                                case 41:
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 42:
                                case 43:
                                case 44:
                                case 45:
                                case 46:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 47:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 48:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 97:
                                    if (!forceStandard)
                                        Race = RACE.LIZARDFOLK;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 98:
                                    if (!forceStandard)
                                        Race = RACE.DOPPELGANGER;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBOAR;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WERETIGER;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.MONK:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                case 2:
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF; 
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 3:
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 14:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 15:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.PALADIN:
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
                                    if (!forceNonStandard)
                                        Race = RACE.HILL_DWARF; 
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 21:
                                    if (!forceStandard)
                                        Race = RACE.MOUNTAIN_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 22:
                                    if (!forceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 23:
                                case 24:
                                case 25:
                                case 26:
                                case 27:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 28:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 29:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 30:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 98:
                                    if (!forceStandard)
                                        Race = RACE.DOPPELGANGER;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBOAR;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WERETIGER;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.RANGER:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                    if (!forceNonStandard)
                                        Race = RACE.HILL_DWARF; 
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 7:
                                    if (!forceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 37:
                                    if (!forceStandard)
                                        Race = RACE.FOREST_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 38:
                                    if (!forceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 56:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 57:
                                    if (!forceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 97:
                                case 98:
                                    if (!forceStandard)
                                        Race = RACE.LIZARDFOLK;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBOAR;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WERETIGER;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.SORCERER:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                    if (!forceNonStandard)
                                        Race = RACE.HILL_DWARF; 
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 2:
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 13:
                                case 14:
                                case 15:
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 16:
                                    if (!forceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 42:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 43:
                                    if (!forceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 44:
                                case 45:
                                case 46:
                                case 47:
                                case 48:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 96:
                                case 97:
                                    if (!forceStandard)
                                        Race = RACE.LIZARDFOLK;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 98:
                                    if (!forceStandard)
                                        Race = RACE.DOPPELGANGER;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBOAR;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WERETIGER;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.THIEF:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_DWARF; 
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 2:
                                case 3:
                                case 4:
                                    if (!forceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 9:
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 10:
                                    if (!forceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 54:
                                case 55:
                                case 56:
                                case 57:
                                case 58:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 59:
                                case 60:
                                case 61:
                                case 62:
                                case 63:
                                    if (!forceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 98:
                                    if (!forceStandard)
                                        Race = RACE.DOPPELGANGER;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBOAR;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WERETIGER;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.WIZARD:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                    if (!forceStandard)
                                        Race = RACE.GRAY_ELF; 
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 27:
                                case 28:
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 29:
                                    if (!forceNonStandard)
                                        Race = RACE.ROCK_GNOME;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 45:
                                case 46:
                                case 47:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 48:
                                case 49:
                                    if (!forceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 50:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 98:
                                    if (!forceStandard)
                                        Race = RACE.DOPPELGANGER;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREBOAR;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WERETIGER;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                    } break;
                case ALIGNMENT.EVIL:
                    switch (charClass)
                    {
                        case CLASS.BARBARIAN:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                    if (!forceStandard)
                                        Race = RACE.WILD_ELF; 
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 2:
                                case 3:
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 4:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 5:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 6:
                                    if (!forceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 40:
                                case 41:
                                case 42:
                                case 43:
                                case 44:
                                    if (!forceStandard)
                                        Race = RACE.LIZARDFOLK;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 45:
                                    if (!forceStandard)
                                        Race = RACE.GOBLIN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 46:
                                    if (!forceStandard)
                                        Race = RACE.HOBGOBLIN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 47:
                                    if (!forceStandard)
                                        Race = RACE.KOBOLD;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                        Race = RACE.ORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 78:
                                    if (!forceStandard)
                                        Race = RACE.TIEFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 79:
                                case 80:
                                case 81:
                                case 82:
                                case 83:
                                    if (!forceStandard)
                                        Race = RACE.GNOLL;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 84:
                                    if (!forceStandard)
                                        Race = RACE.TROGLODYTE;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 85:
                                case 86:
                                    if (!forceStandard)
                                        Race = RACE.BUGBEAR;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 87:
                                case 88:
                                case 89:
                                case 90:
                                    if (!forceStandard)
                                        Race = RACE.OGRE;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 91:
                                case 92:
                                case 93:
                                case 94:
                                    if (!forceStandard)
                                        Race = RACE.MINOTAUR;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 95:
                                case 96:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREWOLF;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 97:
                                case 98:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_FIEND;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.BARD:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF; 
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 2:
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 18:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 19:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 20:
                                    if (!forceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 21:
                                case 22:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 98:
                                    if (!forceStandard)
                                        Race = RACE.GOBLIN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (!forceStandard)
                                        Race = RACE.TIEFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREWOLF;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.CLERIC:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                case 2:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_DWARF; 
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 3:
                                    if (!forceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 4:
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 5:
                                    if (!forceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 6:
                                case 7:
                                case 8:
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 19:
                                case 20:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 21:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 22:
                                    if (!forceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 23:
                                case 24:
                                case 25:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 57:
                                case 58:
                                case 59:
                                case 60:
                                case 61:
                                case 62:
                                case 63:
                                    if (!forceStandard)
                                        Race = RACE.LIZARDFOLK;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 64:
                                    if (!forceStandard)
                                        Race = RACE.GOBLIN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 65:
                                    if (!forceStandard)
                                        Race = RACE.HOBGOBLIN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 66:
                                    if (!forceStandard)
                                        Race = RACE.KOBOLD;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 67:
                                    if (!forceStandard)
                                        Race = RACE.ORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 68:
                                    if (!forceStandard)
                                        Race = RACE.TIEFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 69:
                                case 70:
                                case 71:
                                    if (!forceStandard)
                                    {
                                        Race = RACE.DROW;
                                        Male = false;
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 72:
                                    if (!forceStandard)
                                        Race = RACE.DUERGAR;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 73:
                                case 74:
                                    if (!forceStandard)
                                        Race = RACE.GNOLL;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                        Race = RACE.TROGLODYTE;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 90:
                                case 91:
                                    if (!forceStandard)
                                        Race = RACE.BUGBEAR;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 92:
                                    if (!forceStandard)
                                        Race = RACE.OGRE;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 93:
                                    if (!forceStandard)
                                        Race = RACE.MINOTAUR;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 94:
                                    if (!forceStandard)
                                        Race = RACE.MIND_FLAYER;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 95:
                                    if (!forceStandard)
                                        Race = RACE.OGRE_MAGE;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 96:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WERERAT;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 97:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREWOLF;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 98:
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_FIEND;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.DRUID:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                case 2:
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF; 
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 3:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 4:
                                    if (!forceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 5:
                                case 6:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                        Race = RACE.LIZARDFOLK;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 72:
                                    if (!forceStandard)
                                        Race = RACE.GOBLIN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 73:
                                    if (!forceStandard)
                                        Race = RACE.HOBGOBLIN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 74:
                                    if (!forceStandard)
                                        Race = RACE.KOBOLD;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 75:
                                    if (!forceStandard)
                                        Race = RACE.ORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                        Race = RACE.GNOLL;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.FIGHTER:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                case 2:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_DWARF; 
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 3:
                                case 4:
                                    if (!forceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 5:
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 6:
                                case 7:
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                case 12:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 13:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 14:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 54:
                                    if (!forceStandard)
                                        Race = RACE.LIZARDFOLK;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 55:
                                    if (!forceStandard)
                                        Race = RACE.GOBLIN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                        Race = RACE.HOBGOBLIN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 81:
                                    if (!forceStandard)
                                        Race = RACE.KOBOLD;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 82:
                                case 83:
                                case 84:
                                case 85:
                                case 86:
                                    if (!forceStandard)
                                        Race = RACE.ORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 87:
                                case 88:
                                    if (!forceStandard)
                                        Race = RACE.DROW;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 89:
                                    if (!forceStandard)
                                        Race = RACE.DUERGAR;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 90:
                                    if (!forceStandard)
                                        Race = RACE.DERRO_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 91:
                                    if (!forceStandard)
                                        Race = RACE.GNOLL;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 92:
                                    if (!forceStandard)
                                        Race = RACE.TROGLODYTE;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 93:
                                    if (!forceStandard)
                                        Race = RACE.BUGBEAR;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 94:
                                    if (!forceStandard)
                                        Race = RACE.OGRE;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 95:
                                    if (!forceStandard)
                                        Race = RACE.MIND_FLAYER;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 96:
                                    if (!forceStandard)
                                        Race = RACE.OGRE_MAGE;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 97:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WERERAT;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 98:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREWOLF;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_FIEND;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.MONK:
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
                                case 10:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF; 
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 91:
                                case 92:
                                case 93:
                                    if (!forceStandard)
                                        Race = RACE.HOBGOBLIN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 94:
                                    if (!forceStandard)
                                        Race = RACE.TIEFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 95:
                                case 96:
                                    if (!forceStandard)
                                        Race = RACE.OGRE_MAGE;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 97:
                                case 98:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WERERAT;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_FIEND;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.PALADIN:
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
                                case 10:
                                    if (!forceStandard)
                                    {
                                        Race = RACE.DROW;
                                        Male = false;
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HILL_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 21:
                                    if (!forceStandard)
                                        Race = RACE.DUERGAR;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 22:
                                    if (!forceStandard)
                                        Race = RACE.DERRO_DWARF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 23:
                                case 24:
                                case 25:
                                case 26:
                                case 27:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 28:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 29:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 30:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 98:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREWOLF;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_FIEND;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.RANGER:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 29:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 30:
                                    if (!forceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 70:
                                case 71:
                                    if (!forceStandard)
                                        Race = RACE.LIZARDFOLK;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 72:
                                    if (!forceStandard)
                                        Race = RACE.HOBGOBLIN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                        Race = RACE.GNOLL;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 93:
                                    if (!forceStandard)
                                        Race = RACE.BUGBEAR;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 94:
                                    if (!forceStandard)
                                        Race = RACE.TROGLODYTE;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 95:
                                    if (!forceStandard)
                                        Race = RACE.OGRE;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 96:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WERERAT;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 97:
                                case 98:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREWOLF;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_FIEND;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.SORCERER:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                    if (!forceStandard)
                                        Race = RACE.WILD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 17:
                                case 18:
                                case 19:
                                case 20:
                                case 21:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 22:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 23:
                                    if (!forceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 24:
                                case 25:
                                case 26:
                                case 27:
                                case 28:
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 69:
                                    if (!forceStandard)
                                        Race = RACE.LIZARDFOLK;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 70:
                                    if (!forceStandard)
                                        Race = RACE.GOBLIN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 71:
                                    if (!forceStandard)
                                        Race = RACE.HOBGOBLIN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                        Race = RACE.KOBOLD;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 87:
                                    if (!forceStandard)
                                        Race = RACE.GNOLL;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 88:
                                case 89:
                                case 90:
                                    if (!forceStandard)
                                        Race = RACE.TROGLODYTE;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 91:
                                    if (!forceStandard)
                                        Race = RACE.BUGBEAR;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 92:
                                    if (!forceStandard)
                                        Race = RACE.OGRE;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 93:
                                    if (!forceStandard)
                                        Race = RACE.MINOTAUR;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 94:
                                    if (!forceStandard)
                                        Race = RACE.MIND_FLAYER;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 95:
                                    if (!forceStandard)
                                        Race = RACE.OGRE_MAGE;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 96:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WERERAT;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 97:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREWOLF;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 98:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_FIEND;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.THIEF:
                            switch (Dice.Percentile())
                            {
                                case 1:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_DWARF; 
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 2:
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 3:
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 39:
                                    if (!forceStandard)
                                        Race = RACE.DEEP_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 40:
                                    if (!forceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFORC;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                        Race = RACE.GOBLIN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 86:
                                    if (!forceStandard)
                                        Race = RACE.HOBGOBLIN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 87:
                                    if (!forceStandard)
                                        Race = RACE.KOBOLD;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 88:
                                case 89:
                                    if (!forceStandard)
                                        Race = RACE.TIEFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 90:
                                case 91:
                                case 92:
                                case 93:
                                    if (!forceStandard)
                                        Race = RACE.BUGBEAR;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 94:
                                    if (!forceStandard)
                                        Race = RACE.MIND_FLAYER;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 95:
                                case 96:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WERERAT;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 97:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREWOLF;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 98:
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_FIEND;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        case CLASS.WIZARD:
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
                                case 10:
                                    if (!forceNonStandard)
                                        Race = RACE.HIGH_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 11:
                                    if (!forceStandard)
                                        Race = RACE.WOOD_ELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HALFELF;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 27:
                                    if (!forceNonStandard)
                                        Race = RACE.LIGHTFOOT_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 28:
                                    if (!forceStandard)
                                        Race = RACE.TALLFELLOW_HALFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceNonStandard)
                                        Race = RACE.HUMAN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 79:
                                case 80:
                                    if (!forceStandard)
                                        Race = RACE.HOBGOBLIN;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 81:
                                    if (!forceStandard)
                                        Race = RACE.TIEFLING;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
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
                                    if (!forceStandard)
                                    {
                                        Race = RACE.DROW;
                                        Male = true;
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 92:
                                    if (!forceStandard)
                                        Race = RACE.GNOLL;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 93:
                                    if (!forceStandard)
                                        Race = RACE.BUGBEAR;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 94:
                                    if (!forceStandard)
                                        Race = RACE.MIND_FLAYER;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 95:
                                case 96:
                                    if (!forceStandard)
                                        Race = RACE.OGRE_MAGE;
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 97:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WERERAT;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 98:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.WEREWOLF;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 99:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_FIEND;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                case 100:
                                    if (allowMetaRaces || forceNonStandard)
                                    {
                                        MetaRace = METARACE.HALF_DRAGON;
                                        RaceByAlignment(alignment, charClass, false, false, false);
                                    }
                                    else
                                        RaceByAlignment(alignment, charClass, allowMetaRaces, forceStandard, forceNonStandard);
                                    break;
                                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                            } break;
                        default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
                    } break;
                default: Race = RACE.HUMAN; MetaRace = METARACE.NONE; break;
            }
        }

        public static METARACE RandomMetaRace(ALIGNMENT Alignment, CLASS Class, METARACE_RANDOMIZER Randomizer)
        {
            switch (Randomizer)
            {
                case METARACE_RANDOMIZER.ANY:
                    switch (Alignment)
                    {
                        case ALIGNMENT.GOOD:
                            switch (Class)
                            {
                                case CLASS.BARBARIAN:
                                case CLASS.BARD:
                                    if (Dice.Percentile() < 51)
                                        return METARACE.HALF_CELESTIAL;
                                    return METARACE.HALF_DRAGON;
                                case CLASS.CLERIC:
                                    var roll = Dice.Percentile();
                                    if (roll < 51)
                                        return METARACE.HALF_CELESTIAL;
                                    else if (roll < 76)
                                        return METARACE.HALF_DRAGON;
                                    return METARACE.WEREBEAR;
                                case CLASS.DRUID: return METARACE.HALF_CELESTIAL;
                                case CLASS.FIGHTER:
                                case CLASS.MONK:
                                case CLASS.PALADIN:
                                case CLASS.RANGER:
                                case CLASS.THIEF:
                                case CLASS.WIZARD:
                                    roll = Dice.d6();
                                    if (roll < 3)
                                        return METARACE.HALF_CELESTIAL;
                                    else if (roll < 5)
                                        return METARACE.HALF_DRAGON;
                                    return METARACE.WEREBEAR;
                                case CLASS.SORCERER:
                                    roll = Dice.Percentile();
                                    if (roll < 26)
                                        return METARACE.HALF_CELESTIAL;
                                    else if (roll < 76)
                                        return METARACE.HALF_DRAGON;
                                    return METARACE.WEREBEAR;
                                default: return METARACE.NONE;
                            }
                        case ALIGNMENT.NEUTRAL:
                            switch (Class)
                            {
                                case CLASS.BARBARIAN:
                                case CLASS.BARD:
                                case CLASS.CLERIC:
                                case CLASS.DRUID:
                                case CLASS.FIGHTER:
                                case CLASS.PALADIN:
                                case CLASS.RANGER:
                                case CLASS.SORCERER:
                                case CLASS.THIEF:
                                case CLASS.WIZARD:
                                    if (Dice.Percentile() < 51)
                                        return METARACE.WEREBOAR;
                                    return METARACE.WERETIGER;
                                default: return METARACE.NONE;
                            }
                        case ALIGNMENT.EVIL:
                            switch (Class)
                            {
                                case CLASS.BARBARIAN:
                                    var roll = Dice.d6();
                                    if (roll < 3)
                                        return METARACE.WEREWOLF;
                                    else if (roll < 5)
                                        return METARACE.HALF_FIEND;
                                    return METARACE.HALF_DRAGON;
                                case CLASS.BARD: return METARACE.WEREWOLF;
                                case CLASS.CLERIC:
                                    roll = Dice.Percentile();
                                    if (roll < 21)
                                        return METARACE.WERERAT;
                                    else if (roll < 41)
                                        return METARACE.WEREWOLF;
                                    else if (roll < 81)
                                        return METARACE.HALF_FIEND;
                                    return METARACE.HALF_DRAGON;
                                case CLASS.FIGHTER:
                                case CLASS.WIZARD:
                                    roll = Dice.Percentile();
                                    if (roll < 26)
                                        return METARACE.WERERAT;
                                    else if (roll < 51)
                                        return METARACE.WEREWOLF;
                                    else if (roll < 76)
                                        return METARACE.HALF_FIEND;
                                    return METARACE.HALF_DRAGON;
                                case CLASS.MONK:
                                    roll = Dice.Percentile();
                                    if (roll < 51)
                                        return METARACE.WERERAT;
                                    else if (roll < 76)
                                        return METARACE.HALF_FIEND;
                                    return METARACE.HALF_DRAGON;
                                case CLASS.PALADIN: return METARACE.HALF_DRAGON;
                                case CLASS.RANGER:
                                    roll = Dice.Percentile();
                                    if (roll < 21)
                                        return METARACE.WERERAT;
                                    else if (roll < 61)
                                        return METARACE.WEREWOLF;
                                    else if (roll < 81)
                                        return METARACE.HALF_FIEND;
                                    return METARACE.HALF_DRAGON;
                                case CLASS.SORCERER:
                                    roll = Dice.Percentile();
                                    if (roll < 21)
                                        return METARACE.WERERAT;
                                    else if (roll < 41)
                                        return METARACE.WEREWOLF;
                                    else if (roll < 61)
                                        return METARACE.HALF_FIEND;
                                    return METARACE.HALF_DRAGON;
                                case CLASS.THIEF:
                                    roll = Dice.d6();
                                    if (roll < 3)
                                        return METARACE.WERERAT;
                                    else if (roll < 4)
                                        return METARACE.WEREWOLF;
                                    else if (roll < 6)
                                        return METARACE.HALF_FIEND;
                                    return METARACE.HALF_DRAGON;
                                default: return METARACE.NONE;
                            }
                        default: return METARACE.NONE;
                    }
                case METARACE_RANDOMIZER.ANY_EVIL: return RandomMetaRace(ALIGNMENT.EVIL, Class, METARACE_RANDOMIZER.ANY);
                case METARACE_RANDOMIZER.ANY_GOOD: return RandomMetaRace(ALIGNMENT.GOOD, Class, METARACE_RANDOMIZER.ANY);
                case METARACE_RANDOMIZER.ANY_LYCANTHROPE:
                    switch (Alignment)
                    {
                        case ALIGNMENT.GOOD:
                            switch (Class)
                            {
                                case CLASS.CLERIC:                                   
                                case CLASS.FIGHTER:
                                case CLASS.MONK:
                                case CLASS.PALADIN:
                                case CLASS.RANGER:
                                case CLASS.THIEF:
                                case CLASS.WIZARD:
                                case CLASS.SORCERER: return METARACE.WEREBEAR;
                                default: return METARACE.NONE;
                            }
                        case ALIGNMENT.NEUTRAL:
                            switch (Class)
                            {
                                case CLASS.BARBARIAN:
                                case CLASS.BARD:
                                case CLASS.CLERIC:
                                case CLASS.DRUID:
                                case CLASS.FIGHTER:
                                case CLASS.PALADIN:
                                case CLASS.RANGER:
                                case CLASS.SORCERER:
                                case CLASS.THIEF:
                                case CLASS.WIZARD:
                                    if (Dice.Percentile() < 51)
                                        return METARACE.WEREBOAR;
                                    return METARACE.WERETIGER;
                                default: return METARACE.NONE;
                            }
                        case ALIGNMENT.EVIL:
                            switch (Class)
                            {
                                case CLASS.BARBARIAN:
                                case CLASS.BARD: return METARACE.WEREWOLF;
                                case CLASS.CLERIC:
                                case CLASS.FIGHTER:
                                case CLASS.WIZARD:
                                case CLASS.SORCERER:
                                    if (Dice.Percentile() < 51)
                                        return METARACE.WERERAT;
                                    return METARACE.WEREWOLF;
                                case CLASS.MONK: return METARACE.WERERAT;
                                case CLASS.RANGER:
                                    if (Dice.d6() < 3)
                                        return METARACE.WERERAT;
                                    return METARACE.WEREWOLF;
                                case CLASS.THIEF:
                                    if (Dice.d6() < 3)
                                        return METARACE.WEREWOLF;
                                    return METARACE.WERERAT;
                                default: return METARACE.NONE;
                            }
                        default: return METARACE.NONE;
                    }
                case METARACE_RANDOMIZER.ANY_NEUTRAL: return RandomMetaRace(ALIGNMENT.NEUTRAL, Class, METARACE_RANDOMIZER.ANY);
                case METARACE_RANDOMIZER.ANY_NONEVIL:
                    if (Alignment != ALIGNMENT.EVIL)
                        return RandomMetaRace(Alignment, Class, METARACE_RANDOMIZER.ANY);
                    return METARACE.NONE;
                case METARACE_RANDOMIZER.ANY_NONGOOD:
                    if (Alignment != ALIGNMENT.GOOD)
                        return RandomMetaRace(Alignment, Class, METARACE_RANDOMIZER.ANY);
                    return METARACE.NONE;
                case METARACE_RANDOMIZER.ANY_NONLYCANTHROPE:
                    switch (Alignment)
                    {
                        case ALIGNMENT.GOOD:
                            switch (Class)
                            {
                                case CLASS.BARBARIAN:
                                case CLASS.BARD:
                                case CLASS.FIGHTER:
                                case CLASS.MONK:
                                case CLASS.PALADIN:
                                case CLASS.RANGER:
                                case CLASS.THIEF:
                                case CLASS.WIZARD:
                                    if (Dice.Percentile() < 51)
                                        return METARACE.HALF_CELESTIAL;
                                    return METARACE.HALF_DRAGON;
                                case CLASS.CLERIC:
                                    if (Dice.d6() < 5)
                                        return METARACE.HALF_CELESTIAL;
                                    return METARACE.HALF_DRAGON;
                                case CLASS.DRUID: return METARACE.HALF_CELESTIAL;
                                case CLASS.SORCERER:
                                    if (Dice.d6() < 3)
                                        return METARACE.HALF_CELESTIAL;
                                    return METARACE.HALF_DRAGON;
                                default: return METARACE.NONE;
                            }
                        case ALIGNMENT.EVIL:
                            switch (Class)
                            {
                                case CLASS.BARBARIAN:
                                case CLASS.FIGHTER:
                                case CLASS.WIZARD:
                                case CLASS.MONK:
                                case CLASS.RANGER:
                                    if (Dice.d6() < 4)
                                        return METARACE.HALF_FIEND;
                                    return METARACE.HALF_DRAGON;
                                case CLASS.CLERIC:
                                case CLASS.THIEF:
                                    if (Dice.d6() < 5)
                                        return METARACE.HALF_FIEND;
                                    return METARACE.HALF_DRAGON;
                                case CLASS.PALADIN: return METARACE.HALF_DRAGON;
                                case CLASS.SORCERER:
                                    if (Dice.d6() < 3)
                                        return METARACE.HALF_FIEND;
                                    return METARACE.HALF_DRAGON;
                                default: return METARACE.NONE;
                            }
                        default: return METARACE.NONE;
                    }
                case METARACE_RANDOMIZER.ANY_NONNEUTRAL:
                    if (Alignment != ALIGNMENT.NEUTRAL)
                        return RandomMetaRace(Alignment, Class, METARACE_RANDOMIZER.ANY);
                    return METARACE.NONE;
                default: return METARACE.NONE;
            }
        }

        public Boolean CanSpeak(String Language)
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
            var MetaRaceString = String.Empty;
            if (MetaRace != METARACE.NONE)
                MetaRaceString = Enum.GetName(typeof(METARACE), MetaRace) + " ";
            
            return String.Format("{0} {1}{2}", Gender, MetaRaceString, Enum.GetName(typeof(RACE), Race));
        }
    }
}
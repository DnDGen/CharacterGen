using System;
using System.Collections.Generic;
using System.Text;

namespace NPCGen
{
    class Encounter
    {
        public static string Generate(int Level, ref Random random)
        {
            switch (Dice.Percentile(ref random))
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    switch (Level)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4: return Level1(ref random);
                        case 5:
                        case 6: return Level2(ref random);
                        case 7: return Level3(ref random);
                        case 8: return Level4(ref random);
                        case 9: return Level5(ref random);
                        case 10: return Level6(ref random);
                        case 11: return Level7(ref random);
                        case 12: return Level8(ref random);
                        case 13: return Level9(ref random);
                        case 14: return Level10(ref random);
                        case 15: return Level11(ref random);
                        case 16: return Level12(ref random);
                        case 17: return Level13(ref random);
                        case 18: return Level14(ref random);
                        case 19: return Level15(ref random);
                        case 20: return Level16(ref random);
                        case 21: return Level17(ref random);
                        case 22: return Level18(ref random);
                        case 23: return Level19(ref random);
                        case 24: return Level20(ref random);
                        default: return "Epic-Level Monster";
                    }
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    switch (Level)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4: return Level1(ref random);
                        case 5: return Level2(ref random);
                        case 6: return Level3(ref random);
                        case 7: return Level4(ref random);
                        case 8: return Level5(ref random);
                        case 9: return Level6(ref random);
                        case 10: return Level7(ref random);
                        case 11: return Level8(ref random);
                        case 12: return Level9(ref random);
                        case 13: return Level10(ref random);
                        case 14: return Level11(ref random);
                        case 15: return Level12(ref random);
                        case 16: return Level13(ref random);
                        case 17: return Level14(ref random);
                        case 18: return Level15(ref random);
                        case 19: return Level16(ref random);
                        case 20: return Level17(ref random);
                        case 21: return Level18(ref random);
                        case 22: return Level19(ref random);
                        case 23: return Level20(ref random);
                        default: return "Epic-level Monster";
                    }
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
                    switch (Level)
                    {
                        case 1:
                        case 2: return Level1(ref random);
                        case 3:
                        case 4: return Level2(ref random);
                        case 5: return Level3(ref random);
                        case 6: return Level4(ref random);
                        case 7: return Level5(ref random);
                        case 8: return Level6(ref random);
                        case 9: return Level7(ref random);
                        case 10: return Level8(ref random);
                        case 11: return Level9(ref random);
                        case 12: return Level10(ref random);
                        case 13: return Level11(ref random);
                        case 14: return Level12(ref random);
                        case 15: return Level13(ref random);
                        case 16: return Level14(ref random);
                        case 17: return Level15(ref random);
                        case 18: return Level16(ref random);
                        case 19: return Level17(ref random);
                        case 20: return Level18(ref random);
                        case 21: return Level19(ref random);
                        case 22: return Level20(ref random);
                        default: return "Epic-level Monster";
                    }
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
                    switch (Level)
                    {
                        case 1: return Level1(ref random);
                        case 2:
                        case 3: return Level2(ref random);
                        case 4: return Level3(ref random);
                        case 5: return Level4(ref random);
                        case 6: return Level5(ref random);
                        case 7: return Level6(ref random);
                        case 8: return Level7(ref random);
                        case 9: return Level8(ref random);
                        case 10: return Level9(ref random);
                        case 11: return Level10(ref random);
                        case 12: return Level11(ref random);
                        case 13: return Level12(ref random);
                        case 14: return Level13(ref random);
                        case 15: return Level14(ref random);
                        case 16: return Level15(ref random);
                        case 17: return Level16(ref random);
                        case 18: return Level17(ref random);
                        case 19: return Level18(ref random);
                        case 20: return Level19(ref random);
                        case 21: return Level20(ref random);
                        default: return "Epic-level Monster";
                    }
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
                    switch (Level)
                    {
                        case 1: return Level2(ref random);
                        case 2: return Level3(ref random);
                        case 3: return Level4(ref random);
                        case 4: return Level5(ref random);
                        case 5: return Level6(ref random);
                        case 6: return Level7(ref random);
                        case 7: return Level8(ref random);
                        case 8: return Level9(ref random);
                        case 9: return Level10(ref random);
                        case 10: return Level11(ref random);
                        case 11: return Level12(ref random);
                        case 12: return Level13(ref random);
                        case 13: return Level14(ref random);
                        case 14: return Level15(ref random);
                        case 15: return Level16(ref random);
                        case 16: return Level17(ref random);
                        case 17: return Level18(ref random);
                        case 18:
                        case 19: return Level19(ref random);
                        case 20: return Level20(ref random);
                        default: return "Epic-level Monster";
                    }
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
                    switch (Level)
                    {
                        case 1: return Level2(ref random);
                        case 2: return Level4(ref random);
                        case 3: return Level5(ref random);
                        case 4: return Level6(ref random);
                        case 5: return Level7(ref random);
                        case 6: return Level8(ref random);
                        case 7: return Level9(ref random);
                        case 8: return Level10(ref random);
                        case 9: return Level11(ref random);
                        case 10: return Level12(ref random);
                        case 11: return Level13(ref random);
                        case 12: return Level14(ref random);
                        case 13: return Level15(ref random);
                        case 14: return Level16(ref random);
                        case 15: return Level17(ref random);
                        case 16: return Level18(ref random);
                        case 17: return Level19(ref random);
                        case 18:
                        case 19:
                        case 20: return Level20(ref random);
                        default: return "Epic-level Monster";
                    }
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
                    switch (Level)
                    {
                        case 1: return Level3(ref random);
                        case 2: return Level5(ref random);
                        case 3: return Level6(ref random);
                        case 4: return Level7(ref random);
                        case 5: return Level8(ref random);
                        case 6: return Level9(ref random);
                        case 7: return Level10(ref random);
                        case 8: return Level11(ref random);
                        case 9: return Level12(ref random);
                        case 10: return Level13(ref random);
                        case 11: return Level14(ref random);
                        case 12: return Level15(ref random);
                        case 13: return Level16(ref random);
                        case 14: return Level17(ref random);
                        case 15: return Level18(ref random);
                        case 16: return Level19(ref random);
                        case 17:
                        case 18:
                        case 19:
                        case 20: return Level20(ref random);
                        default: return "Epic-level Monster";
                    }
                default:
                    switch (Level)
                    {
                        case 1: return Level1(ref random);
                        case 2: return Level2(ref random);
                        case 3: return Level3(ref random);
                        case 4: return Level4(ref random);
                        case 5: return Level5(ref random);
                        case 6: return Level6(ref random);
                        case 7: return Level7(ref random);
                        case 8: return Level8(ref random);
                        case 9: return Level9(ref random);
                        case 10: return Level10(ref random);
                        case 11: return Level11(ref random);
                        case 12: return Level12(ref random);
                        case 13: return Level13(ref random);
                        case 14: return Level14(ref random);
                        case 15: return Level15(ref random);
                        case 16: return Level16(ref random);
                        case 17: return Level17(ref random);
                        case 18: return Level18(ref random);
                        case 19: return Level19(ref random);
                        case 20: return Level20(ref random);
                        default: return "Epic-level Monster";
                    }
            }
        }

        private static string Level1(ref Random random)
        {
            switch (Dice.Percentile(ref random))
            {
                case 1:
                case 2:
                case 3:
                case 4: return "monstrous centipedes";
                case 5:
                case 6:
                case 7:
                case 8:
                case 9: return "dire rats";
                case 10:
                case 11:
                case 12:
                case 13:
                case 14: return "giant fire beetles";
                case 15:
                case 16:
                case 17: return "monstrous scorpions";
                case 18:
                case 19:
                case 20: return "monstrous spiders";
                case 21:
                case 22:
                case 23:
                case 24:
                case 25: return Dragon(ref random);
                case 26:
                case 27:
                case 28:
                case 29:
                case 30: return "Dwarves";
                case 31:
                case 32:
                case 33:
                case 34:
                case 35: return "Elves";
                case 36:
                case 37:
                case 38:
                case 39:
                case 40: return Character.HumanoidSubtype(ref random);
                case 41:
                case 42:
                case 43:
                case 44:
                case 45: return "darkmantle";
                case 46:
                case 47:
                case 48:
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55: return "krenshar";
                case 56:
                case 57:
                case 58:
                case 59:
                case 60: return "lemure";
                case 61:
                case 62:
                case 63:
                case 64:
                case 65: return "goblins";
                case 66:
                case 67:
                case 68:
                case 69:
                case 70: return "hobgoblins";
                case 71:
                case 72:
                case 73:
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                case 79:
                case 80: return "kobolds";
                case 81:
                case 82:
                case 83:
                case 84:
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90: return "skeletons";
                default: return "zombies";
            }
        }

        private static string Level2(ref Random random)
        {
            switch (Dice.Percentile(ref random))
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5: return "monstrous centipedes";
                case 6:
                case 7:
                case 8:
                case 9:
                case 10: return "giant ants";
                case 11:
                case 12:
                case 13:
                case 14:
                case 15: return "monstrous scorpions";
                case 16:
                case 17:
                case 18:
                case 19:
                case 20: return "monstrous spiders";
                case 21:
                case 22:
                case 23:
                case 24:
                case 25: return Dragon(ref random);
                case 26:
                case 27:
                case 28:
                case 29:
                case 30: return "Elves";
                case 31:
                case 32:
                case 33:
                case 34:
                case 35: return Character.HumanoidSubtype(ref random);
                case 36:
                case 37: return "choker";
                case 38:
                case 39:
                case 40:
                case 41:
                case 42: return "ethereal marauder";
                case 43:
                case 44:
                case 45: return "shriekers";
                case 46:
                case 47:
                case 48:
                case 49:
                case 50: return "formian";
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                case 60: return "hobgoblins";
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 67:
                case 68:
                case 69:
                case 70: return "lizardfolk";
                case 71:
                case 72:
                case 73:
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                case 79:
                case 80: return "orcs";
                case 81:
                case 82:
                case 83:
                case 84:
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90: return "zombies";
                default: return "ghouls";
            }
        }

        private static string Level3(ref Random random)
        {
            switch (Dice.Percentile(ref random))
            {
                case 1:
                case 2: return "giant bombardier beetles";
                case 3:
                case 4: return "monstrous centipedes";
                case 5:
                case 6: return "dire badgers";
                case 7:
                case 8: return "dire bats";
                case 9:
                case 10:
                case 11: return "gelatinous cube";
                case 12:
                case 13: return "giant praying mantises";
                case 14: return "monstrous scorpions";
                case 15: return "monstrous spiders";
                case 16:
                case 17:
                case 18:
                case 19:
                case 20: return Dragon(ref random);
                case 21:
                case 22:
                case 23:
                case 24:
                case 25: return "imps";
                case 26:
                case 27:
                case 28:
                case 29:
                case 30: return "wererat";
                case 31:
                case 32:
                case 33:
                case 34:
                case 35: return "Dwarves";
                case 36:
                case 37:
                case 38:
                case 39:
                case 40: return Character.HumanoidSubtype(ref random);
                case 41:
                case 42:
                case 43:
                case 44: return "dretches";
                case 45:
                case 46:
                case 47:
                case 48: return "ethereal filcher";
                case 49:
                case 50:
                case 51:
                case 52: return "phantom fungus";
                case 53:
                case 54:
                case 55:
                case 56: return "thoqquas";
                case 57:
                case 58:
                case 59:
                case 60: return "vargouilles";
                case 61:
                case 62: return "bugbear";
                case 63:
                case 64:
                case 65:
                case 66:
                case 67: return "gnolls";
                case 68:
                case 69:
                case 70:
                case 71: return "wolves";
                case 72:
                case 73:
                case 74:
                case 75: return "dire weasel";
                case 76:
                case 77:
                case 78:
                case 79:
                case 80: return "troglodytes";
                case 81:
                case 82:
                case 83:
                case 84:
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90: return "shadow";
                default: return "skeletons";
            }
        }

        private static string Level4(ref Random random)
        {
            switch (Dice.Percentile(ref random))
            {
                case 1:
                case 2:
                case 3:
                case 4: return "ankhegs";
                case 5:
                case 6:
                case 7:
                case 8: return "dire weasels";
                case 9:
                case 10:
                case 11:
                case 12: return "gray ooze";
                case 13:
                case 14:
                case 15: return "viper snakes";
                case 16:
                case 17:
                case 18:
                case 19:
                case 20: return Dragon(ref random);
                case 21:
                case 22:
                case 23: return "formian";
                case 24:
                case 25:
                case 26: return "imp";
                case 27:
                case 28:
                case 29:
                case 30: return "quasits";
                case 31:
                case 32:
                case 33:
                case 34:
                case 35: return "lantern archons";
                case 36:
                case 37:
                case 38:
                case 39:
                case 40: return Character.HumanoidSubtype(ref random);
                case 41:
                case 42:
                case 43:
                case 44:
                case 45: return "carrion crawlers";
                case 46:
                case 47:
                case 48:
                case 49:
                case 50: return "mimic";
                case 51:
                case 52:
                case 53:
                case 54:
                case 55: return "rust monsters";
                case 56:
                case 57:
                case 58:
                case 59:
                case 60: return "violet fungi";
                case 61:
                case 62: return "bugbear";
                case 63:
                case 64:
                case 65: return "ettercap";
                case 66:
                case 67: return "wolf";
                case 68:
                case 69:
                case 70: return "giant lizard";
                case 71:
                case 72:
                case 73: return "magmins";
                case 74:
                case 75:
                case 76: return "ogre";
                case 77:
                case 78: return "dire boars";
                case 79:
                case 80: return "worgs";
                case 81:
                case 82:
                case 83:
                case 84:
                case 85: return "allips";
                case 86:
                case 87:
                case 88:
                case 89:
                case 90: return "ghost";
                case 91:
                case 92:
                case 93:
                case 94:
                case 95: return "vampire spawn";
                default: return "wights";
            }
        }

        private static string Level5(ref Random random)
        {
            switch (Dice.Percentile(ref random))
            {
                case 1:
                case 2: return "giant ants";
                case 3:
                case 4:
                case 5: return "dire wolverines";
                case 6:
                case 7:
                case 8:
                case 9: return "ochre jelly";
                case 10:
                case 11: return "giant constrictor snake";
                case 12: return "monstrous spiders";
                case 13:
                case 14:
                case 15: return "spider eater";
                case 16:
                case 17:
                case 18:
                case 19:
                case 20: return Dragon(ref random);
                case 21:
                case 22:
                case 23: return "doppelgangers";
                case 24:
                case 25: return "greenhag";
                case 26:
                case 27: return "mephits";
                case 28:
                case 29:
                case 30: return "wererats";
                case 31:
                case 32:
                case 33:
                case 34:
                case 35: return "blink dogs";
                case 36:
                case 37:
                case 38:
                case 39:
                case 40: return Character.HumanoidSubtype(ref random);
                case 41:
                case 42:
                case 43: return "cockatrices";
                case 44:
                case 45:
                case 46:
                case 47: return "gibbering mouther";
                case 48:
                case 49:
                case 50: return "gricks";
                case 51:
                case 52: return "hydra";
                case 53:
                case 54:
                case 55: return "nightmare";
                case 56:
                case 57:
                case 58: return "shocker lizards";
                case 59:
                case 60: return "violet fungus";
                case 61:
                case 62:
                case 63:
                case 64: return "azers";
                case 65:
                case 66:
                case 67: return "bugbears";
                case 68:
                case 69: return "ettercap";
                case 70:
                case 71:
                case 72: return "ogres";
                case 73:
                case 74:
                case 75: return "salamanders";
                case 76:
                case 77: return "troglodytes";
                case 78:
                case 79:
                case 80: return "worgs";
                case 81:
                case 82:
                case 83:
                case 84:
                case 85: return "ghast";
                case 86:
                case 87:
                case 88:
                case 89:
                case 90: return "mummies";
                case 91:
                case 92:
                case 93:
                case 94:
                case 95: return "skeletons";
                default: return "wraith";
            }
        }

        private static string Level6(ref Random random)
        {
            switch (Dice.Percentile(ref random))
            {
                case 1:
                case 2: return "digester";
                case 3:
                case 4: return "dire apes";
                case 5:
                case 6: return "dire wolves";
                case 7: return "giant stag beetles";
                case 8:
                case 9: return "giant wasp";
                case 10:
                case 11:
                case 12: return "owlbears";
                case 13:
                case 14:
                case 15: return "shambling mound";
                case 16:
                case 17:
                case 18:
                case 19:
                case 20: return Dragon(ref random);
                case 21:
                case 22: return "annis";
                case 23:
                case 24:
                case 25: return "harpies";
                case 26: return "quasit";
                case 27:
                case 28: return "wereboars";
                case 29:
                case 30: return "werewolves";
                case 31:
                case 32:
                case 33:
                case 34:
                case 35: return "werebears";
                case 36:
                case 37:
                case 38:
                case 39:
                case 40: return Character.HumanoidSubtype(ref random);
                case 41:
                case 42:
                case 43: return "arrowhawks";
                case 44:
                case 45:
                case 46: return "basilisks";
                case 47:
                case 48:
                case 49:
                case 50: return "displacer beasts";
                case 51:
                case 52:
                case 53: return "gargoyles";
                case 54:
                case 55:
                case 56: return "hell hounds";
                case 57:
                case 58:
                case 59: return "howlers";
                case 60:
                case 61:
                case 62: return "otyughs";
                case 63:
                case 64:
                case 65: return "ravid";
                case 66:
                case 67: return "xorns";
                case 68:
                case 69:
                case 70: return "yeth hounds";
                case 71:
                case 72:
                case 73:
                case 74:
                case 75:
                case 76:
                case 77: return "ettin";
                case 78:
                case 79:
                case 80:
                case 81:
                case 82: return "ogres";
                case 83:
                case 84:
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90: return "weretigers";
                default: return "zombies";
            }
        }

        private static string Level7(ref Random random)
        {
            switch (Dice.Percentile(ref random))
            {
                case 1:
                case 2:
                case 3:
                case 4: return "black pudding";
                case 5: return "monstrous centipedes";
                case 6:
                case 7:
                case 8: return "criosphinx";
                case 9:
                case 10: return "dire boars";
                case 11:
                case 12:
                case 13:
                case 14: return "remorhaz";
                case 15: return "monstrous scorpions";
                case 16:
                case 17:
                case 18:
                case 19:
                case 20: return Dragon(ref random);
                case 21:
                case 22: return "araneas";
                case 23:
                case 24: return "barghests";
                case 25:
                case 26: return "djinn";
                case 27:
                case 28: return "formian";
                case 29:
                case 30: return "jann";
                case 31:
                case 32:
                case 33:
                case 34:
                case 35: return "hound archon";
                case 36:
                case 37:
                case 38:
                case 39:
                case 40: return Character.HumanoidSubtype(ref random);
                case 41:
                case 42:
                case 43:
                case 44:
                case 45: return "cloakers";
                case 46:
                case 47:
                case 48: return "cryohydra";
                case 49:
                case 50:
                case 51:
                case 52: return "formian";
                case 53:
                case 54:
                case 55:
                case 56:
                case 57: return "invisible stalker";
                case 58:
                case 59:
                case 60: return "pyrohydra";
                case 61:
                case 62:
                case 63:
                case 64:
                case 65: return "bugbears";
                case 66:
                case 67:
                case 68:
                case 69:
                case 70: return "ettin";
                case 71:
                case 72:
                case 73:
                case 74:
                case 75: return "minotaurs";
                case 76:
                case 77:
                case 78:
                case 79:
                case 80: return "salamander";
                case 81:
                case 82:
                case 83:
                case 84:
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90: return "ghost";
                default: return "vampire";
            }
        }

        private static string Level8(ref Random random)
        {
            switch (Dice.Percentile(ref random))
            {
                case 1:
                case 2:
                case 3: return "giant ants";
                case 4:
                case 5:
                case 6:
                case 7:
                case 8: return "dire bats";
                case 9:
                case 10: return "monstrous spiders";
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20: return Dragon(ref random);
                case 21:
                case 22: return "aboleth";
                case 23:
                case 24: return "barghests";
                case 25:
                case 26: return "erinyes";
                case 27:
                case 28: return "medusa";
                case 29:
                case 30: return "mind flayer";
                case 31:
                case 32:
                case 33: return "ogre mage";
                case 34:
                case 35: return "yuan-ti";
                case 36:
                case 37:
                case 38:
                case 39:
                case 40: return "lammasu";
                case 41:
                case 42:
                case 43:
                case 44:
                case 45: return Character.HumanoidSubtype(ref random);
                case 46:
                case 47: return "achaierais";
                case 48: return "arrowhawks";
                case 49:
                case 50: return "girallons";
                case 51:
                case 52: return "flesh golems";
                case 53:
                case 54: return "gray render";
                case 55:
                case 56: return "hieracosphinxes";
                case 57:
                case 58:
                case 59: return "hydra";
                case 60: return "Lernaean hydra";
                case 61:
                case 62: return "phase spiders";
                case 63:
                case 64: return "rasts";
                case 65:
                case 66: return "shadow mastiffs";
                case 67:
                case 68: return "winter wolves";
                case 70: return "xorns";
                case 71:
                case 72:
                case 73:
                case 74: return "drider";
                case 75:
                case 76:
                case 77:
                case 78: return "ettins";
                case 79:
                case 80:
                case 81:
                case 82: return "manticores";
                case 83:
                case 84:
                case 85:
                case 86: return "salamanders";
                case 87:
                case 88:
                case 89:
                case 90: return "trolls";
                default: return "spectres";
            }
        }

        private static string Level9(ref Random random)
        {
            switch (Dice.Percentile(ref random))
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5: return "bulettes";
                case 6:
                case 7:
                case 8:
                case 9:
                case 10: return "dire lions";
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20: return Dragon(ref random);
                case 21: return "bebilith";
                case 22: return "lamias";
                case 23:
                case 24: return "mind flayer";
                case 25:
                case 26: return "night hag";
                case 27:
                case 28: return "ogre mage";
                case 29:
                case 30: return "rakshasa";
                case 31:
                case 32: return "succubus";
                case 33:
                case 34: return "xill";
                case 35: return "yuan-ti";
                case 36:
                case 37:
                case 38:
                case 39:
                case 40: return "androsphinx";
                case 41:
                case 42:
                case 43:
                case 44:
                case 45: return Character.HumanoidSubtype(ref random);
                case 46:
                case 47: return "behirs";
                case 48:
                case 49: return "belkers";
                case 50: return "cryohydra";
                case 51:
                case 52: return "delver";
                case 53:
                case 54: return "dragon turtle";
                case 55: return "pyrohydra";
                case 56:
                case 57: return "will-o'-wisps";
                case 58:
                case 59:
                case 60: return "wyverns";
                case 61:
                case 62:
                case 63:
                case 64: return "barbazu";
                case 65:
                case 66:
                case 67:
                case 68: return "hill giant";
                case 69:
                case 70:
                case 71:
                case 72: return "kytons";
                case 73:
                case 74:
                case 75:
                case 76: return "osyluths";
                case 77:
                case 78:
                case 79:
                case 80: return "trolls";
                case 81:
                case 82:
                case 83:
                case 84:
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90: return "bodaks";
                default: return "vampire";
            }
        }

        private static string Level10(ref Random random)
        {
            switch (Dice.Percentile(ref random))
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5: return "dire bears";
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15: return Dragon(ref random);
                case 16:
                case 17: return "aboleths";
                case 18:
                case 19: return "athachs";
                case 20:
                case 21: return "formian";
                case 22:
                case 23:
                case 24: return "medusas";
                case 25:
                case 26: return "water nagas";
                case 27:
                case 28: return "night hag";
                case 29:
                case 30: return "salamander";
                case 31:
                case 32: return "yuan-ti";
                case 33:
                case 34:
                case 35:
                case 36:
                case 37: return "lillends";
                case 38:
                case 39:
                case 40:
                case 41:
                case 42:
                case 43:
                case 44:
                case 45:
                case 46:
                case 47: return Character.HumanoidSubtype(ref random);
                case 48:
                case 49: return "chaos beasts";
                case 50:
                case 51: return "chimeras";
                case 52:
                case 53: return "chuuls";
                case 54: return "cryohydra";
                case 55:
                case 56: return "dragonnes";
                case 57:
                case 58: return "hellcats";
                case 59: return "hydra";
                case 60: return "phasm";
                case 61: return "pyrohydra";
                case 62:
                case 63: return "retriever";
                case 64:
                case 65: return "slaadi";
                case 66:
                case 67: return "umber hulks";
                case 68:
                case 69:
                case 70:
                case 71: return "barbazu";
                case 72:
                case 73:
                case 74:
                case 75: return "driders";
                case 76:
                case 77:
                case 78:
                case 79: return "frost giant";
                case 80:
                case 81:
                case 82:
                case 83: return "stone giant";
                case 84:
                case 85:
                case 86:
                case 87: return "hill giants";
                case 88:
                case 89:
                case 90: return "hamatula";
                default: return "ghost";
            }
        }

        private static string Level11(ref Random random)
        {
            switch (Dice.Percentile(ref random))
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5: return "dire tigers";
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15: return Dragon(ref random);
                case 16:
                case 17:
                case 18: return "hags";
                case 19:
                case 20:
                case 21: return "efreet";
                case 22:
                case 23:
                case 24: return "formian";
                case 25:
                case 26:
                case 27: return "gynosphinxes";
                case 28:
                case 29:
                case 30: return "dark nagas";
                case 31:
                case 32:
                case 33:
                case 34:
                case 35: return "avoral guardinal";
                case 36:
                case 37:
                case 38:
                case 39:
                case 40:
                case 41:
                case 42:
                case 43:
                case 44:
                case 45: return Character.HumanoidSubtype(ref random);
                case 46:
                case 47:
                case 48: return "arrowhawks";
                case 49:
                case 50:
                case 51: return "destrachans";
                case 52:
                case 53:
                case 54: return "clay golems";
                case 55:
                case 56:
                case 57: return "gorgons";
                case 58:
                case 59: return "hydra";
                case 60:
                case 61:
                case 62: return "slaadi";
                case 63:
                case 64:
                case 65: return "xorns";
                case 66:
                case 67:
                case 68:
                case 69:
                case 70: return "fire giant";
                case 71:
                case 72:
                case 73:
                case 74:
                case 75: return "stone giants";
                case 76:
                case 77:
                case 78:
                case 79:
                case 80: return "hamatulas";
                case 81:
                case 82:
                case 83:
                case 84:
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90: return "devourer";
                default: return "mohrgs";
            }
        }

        private static string Level12(ref Random random)
        {
            switch (Dice.Percentile(ref random))
            {
                case 1:
                case 2:
                case 3:
                case 4: return "purple worm";
                case 5: return "monstrous scorpions";
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15: return Dragon(ref random);
                case 16:
                case 17:
                case 18:
                case 19:
                case 20: return "mind flayers";
                case 21:
                case 22:
                case 23:
                case 24:
                case 25: return "spirit nagas";
                case 26:
                case 27:
                case 28:
                case 29:
                case 30: return "slaadi";
                case 31:
                case 32:
                case 33:
                case 34:
                case 35: return "cloud giant";
                case 51:
                case 52:
                case 53:
                case 54:
                case 55: return "cryohydra";
                case 56:
                case 57:
                case 58:
                case 59:
                case 60: return "stone golems";
                case 61:
                case 62:
                case 63:
                case 64:
                case 65: return "pyrohydra";
                case 66:
                case 67:
                case 68:
                case 69:
                case 70: return "yrthaks";
                case 71:
                case 72:
                case 73:
                case 74:
                case 75: return "cornugon";
                case 76:
                case 77:
                case 78:
                case 79:
                case 80: return "cloud giant";
                case 81:
                case 82:
                case 83:
                case 84:
                case 85: return "frost giants";
                case 86:
                case 87:
                case 88:
                case 89:
                case 90: return "salamanders";
                case 91:
                case 92:
                case 93:
                case 94:
                case 95:
                case 96:
                case 97:
                case 98:
                case 99:
                case 100: return "vampire";
                default: return Character.HumanoidSubtype(ref random);
            }
        }

        private static string Level13(ref Random random)
        {
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
                case 15: return Dragon(ref random);
                case 16:
                case 17:
                case 18:
                case 19:
                case 20: return "beholder";
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30: return "night hags";
                case 31:
                case 32:
                case 33:
                case 34:
                case 35: return "slaadi";
                case 36:
                case 37:
                case 38:
                case 39:
                case 40: return "couatls";
                case 41:
                case 42:
                case 43:
                case 44:
                case 45: return "guardian nagas";
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 67: return "frost worms";
                case 68:
                case 69:
                case 70:
                case 71:
                case 72:
                case 73: return "hydra";
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                case 79:
                case 80: return "ropers";
                case 81:
                case 82:
                case 83:
                case 84:
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90: return "cornugons";
                case 91:
                case 92:
                case 93:
                case 94:
                case 95:
                case 96:
                case 97:
                case 98:
                case 99:
                case 100: return "ghost";
                default: return Character.HumanoidSubtype(ref random);
            }
        }

        private static string Level14(ref Random random)
        {
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
                case 15: return Dragon(ref random);
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25: return "beholder";
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35: return "slaadi";
                case 36:
                case 37:
                case 38:
                case 39:
                case 40: return "cloud giant";
                case 56:
                case 57:
                case 58:
                case 59:
                case 60: return "cryohydra";
                case 61:
                case 62:
                case 63:
                case 64:
                case 65: return "iron golems";
                case 66:
                case 67:
                case 68:
                case 69:
                case 70: return "pyrohydra";
                case 71:
                case 72:
                case 73:
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                case 79:
                case 80: return "cloud giants";
                case 81:
                case 82:
                case 83:
                case 84:
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90: return "storm giant";
                case 91: return "griffin";
                case 92:
                case 93:
                case 94:
                case 95:
                case 96:
                case 97:
                case 98:
                case 99:
                case 100: return "lich";
                default: return Character.HumanoidSubtype(ref random);
            }
        }

        private static string Level15(ref Random random)
        {
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
                case 20: return Dragon(ref random);
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30: return "beholders";
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:
                case 39:
                case 40: return "slaadi";
                case 41:
                case 42:
                case 43:
                case 44:
                case 45: return "ghaeles";
                case 71:
                case 72:
                case 73:
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                case 79:
                case 80: return "hezrous";
                case 81:
                case 82:
                case 83:
                case 84:
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90: return "gelugon";
                case 91:
                case 92:
                case 93:
                case 94:
                case 95:
                case 96:
                case 97:
                case 98:
                case 99:
                case 100: return "vampire";
                default: return Character.HumanoidSubtype(ref random);
            }
        }

        private static string Level16(ref Random random)
        {
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
                case 20: return Dragon(ref random);
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30: return "pit fiend";
                case 31:
                case 32:
                case 33:
                case 34:
                case 35: return "astral devas";
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 67:
                case 68:
                case 69:
                case 70: return "gelugons";
                case 71:
                case 72:
                case 73:
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                case 79:
                case 80: return "storm giants";
                case 81:
                case 82:
                case 83:
                case 84:
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90: return "vrocks";
                case 91:
                case 92:
                case 93:
                case 94:
                case 95:
                case 96:
                case 97:
                case 98:
                case 99:
                case 100: return "ghost";
                default: return Character.HumanoidSubtype(ref random);
            }
        }

        private static string Level17(ref Random random)
        {
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
                case 20: return Dragon(ref random);
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30: return "marilith";
                case 31:
                case 32:
                case 33:
                case 34:
                case 35: return "trumpet archons";
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 67:
                case 68:
                case 69:
                case 70: return "glabrezu";
                case 71:
                case 72:
                case 73:
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                case 79:
                case 80: return "hezrous";
                case 81:
                case 82:
                case 83:
                case 84:
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90: return "lich";
                case 91:
                case 92:
                case 93:
                case 94:
                case 95:
                case 96:
                case 97:
                case 98:
                case 99:
                case 100: return "nightwings";
                default: return Character.HumanoidSubtype(ref random);
            }
        }

        private static string Level18(ref Random random)
        {
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
                case 20: return Dragon(ref random);
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30: return "balors";
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:
                case 39:
                case 40: return "pit fiend";
                case 41:
                case 42:
                case 43:
                case 44:
                case 45: return "planetars";
                case 71:
                case 72:
                case 73:
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                case 79:
                case 80: return "glabrezu";
                case 81:
                case 82:
                case 83:
                case 84:
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90: return "vampire";
                case 91:
                case 92:
                case 93:
                case 94:
                case 95:
                case 96:
                case 97:
                case 98:
                case 99:
                case 100: return "nightwalkers";
                default: return Character.HumanoidSubtype(ref random);
            }
        }

        private static string Level19(ref Random random)
        {
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
                case 20: return Dragon(ref random);
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30: return "marilith";
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:
                case 39:
                case 40: return "pit fiends";
                case 41:
                case 42:
                case 43:
                case 44:
                case 45: return "solar";
                case 71:
                case 72:
                case 73:
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                case 79:
                case 80: return "nalfeshnees";
                case 81:
                case 82:
                case 83:
                case 84:
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90: return "ghost";
                case 91:
                case 92:
                case 93:
                case 94:
                case 95:
                case 96:
                case 97:
                case 98:
                case 99:
                case 100: return "nightcrawlers";
                default: return Character.HumanoidSubtype(ref random);
            }
        }

        private static string Level20(ref Random random)
        {
            switch (Dice.Percentile(ref random))
            {
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30: return "balors";
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:
                case 39:
                case 40: return "mariliths";
                case 41:
                case 42:
                case 43:
                case 44:
                case 45: return "solar";
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
                case 70: return Character.HumanoidSubtype(ref random);
                case 71:
                case 72:
                case 73:
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                case 79:
                case 80: return "nalfeshnees";
                case 81:
                case 82:
                case 83:
                case 84:
                case 85: return "ghost";
                case 86:
                case 87:
                case 88:
                case 89:
                case 90: return "lich";
                case 91:
                case 92:
                case 93:
                case 94:
                case 95: return "nightcrawlers";
                case 96:
                case 97:
                case 98:
                case 99:
                case 100: return "vampire";
                default: return Dragon(ref random);
            }
        }

        private static string Dragon(ref Random random)
        {
            switch (Dice.Percentile(ref random))
            {
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
                case 32: return "black dragon";
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
                case 48: return "green dragon";
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
                case 64: return "blue dragon";
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
                case 80: return "red dragon";
                case 81:
                case 82:
                case 83:
                case 84: return "brass dragon";
                case 85:
                case 86:
                case 87:
                case 88: return "copper dragon";
                case 89:
                case 90:
                case 91: return "bronze dragon";
                case 92:
                case 93:
                case 94:
                case 95:
                case 96: return "silver dragon";
                case 97:
                case 98:
                case 99:
                case 100: return "gold dragon";
                default: return "white dragon";
            }
        }
    }
}
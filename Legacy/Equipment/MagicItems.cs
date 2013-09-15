using NPCGen.Characters;
using NPCGen.Roll;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPCGen.Equipment
{
    public enum POWER { NONE, MINOR, MEDIUM, MAJOR };

    public class MagicItems
    {
        public static POWER PowerByLevel(Int32 level)
        {
            switch (level)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5: return POWER.MINOR;
                case 6:
                    if (Dice.Percentile() == 100)
                        return POWER.MEDIUM;
                    return POWER.MINOR;
                case 7:
                    if (Dice.Percentile() > 97)
                        return POWER.MEDIUM;
                    return POWER.MINOR;
                case 8:
                    if (Dice.Percentile() > 96)
                        return POWER.MEDIUM;
                    return POWER.MINOR;
                case 9:
                    if (Dice.Percentile() > 91)
                        return POWER.MEDIUM;
                    return POWER.MINOR;
                case 10:
                    var roll = Dice.Percentile();
                    if (roll == 100)
                        return POWER.MAJOR;
                    else if (roll > 88)
                        return POWER.MEDIUM;
                    return POWER.MINOR;
                case 11:
                    roll = Dice.Percentile();
                    if (roll > 98)
                        return POWER.MAJOR;
                    else if (roll > 84)
                        return POWER.MEDIUM;
                    return POWER.MINOR;
                case 12:
                    roll = Dice.Percentile();
                    if (roll > 97)
                        return POWER.MAJOR;
                    else if (roll > 82)
                        return POWER.MEDIUM;
                    return POWER.MINOR;
                case 13:
                    roll = Dice.Percentile();
                    if (roll > 95)
                        return POWER.MAJOR;
                    else if (roll > 73)
                        return POWER.MEDIUM;
                    return POWER.MINOR;
                case 14:
                    roll = Dice.Percentile();
                    if (roll > 92)
                        return POWER.MAJOR;
                    else if (roll > 58)
                        return POWER.MEDIUM;
                    return POWER.MINOR;
                case 15:
                case 16:
                    roll = Dice.Percentile();
                    if (roll > 90)
                        return POWER.MAJOR;
                    else if (roll > 46)
                        return POWER.MEDIUM;
                    return POWER.MINOR;
                case 17:
                    if (Dice.Percentile() > 83)
                        return POWER.MAJOR;
                    return POWER.MEDIUM;
                case 18:
                    if (Dice.Percentile() > 80)
                        return POWER.MAJOR;
                    return POWER.MEDIUM;
                case 19:
                    if (Dice.Percentile() > 70)
                        return POWER.MAJOR;
                    return POWER.MEDIUM;
                case 20:
                    if (Dice.Percentile() > 65)
                        return POWER.MAJOR;
                    return POWER.MEDIUM;
                default: return POWER.MAJOR;
            }
        }

        public static String Generate(POWER power, Int32 quantity)
        {
            var output = String.Empty;

            while (quantity-- > 0)
            {
                if (output != String.Empty)
                    output += ", ";
                output += Generate(power);
            }

            return output;
        }

        public static String Generate(POWER power)
        {
            var output = String.Empty;
            var SpecificCursed = false;

            if (Dice.Percentile() <= 5)
                output += CursedItem.Generate(ref SpecificCursed);

            if (!SpecificCursed)
            {
                switch (power)
                {
                    case POWER.MINOR:
                        var roll = Dice.Roll(1, 21);
                        if (roll > 19)
                            output += Ring(power);
                        else if (roll > 10)
                            output += WondrousItem(power);
                        else
                            output += Wand(power);
                        break;
                    case POWER.MEDIUM:
                        roll = Dice.Roll(1, 55);
                        if (roll > 52)
                            output += Staff(power);
                        else if (roll > 42)
                            output += Ring(power);
                        else if (roll > 32)
                            output += Rod(power);
                        else if (roll > 17)
                            output += Wand(power);
                        else
                            output += WondrousItem(power);
                        break;
                    case POWER.MAJOR:
                        roll = Dice.Roll(1, 65);
                        if (roll > 45)
                            output += Staff(power);
                        else if (roll > 35)
                            output += Ring(power);
                        else if (roll > 25)
                            output += Rod(power);
                        else if (roll > 20)
                            output += Wand(power);
                        else
                            output += WondrousItem(power);
                        break;
                    default: return "ERROR: NONPOWERED MAGIC ITEM.  MagicItems.161";
                }
            }

            return output;
        }

        public static String Potion(POWER power, Int32 quantity)
        {
            var output = String.Empty;
            
            while (quantity > 0)
            {
                switch (power)
                {
                    case POWER.MINOR:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5: return "potion of jump";
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 10: return "potion of spider climb";
                            case 20: return "potion of love";
                            case 21:
                            case 22:
                            case 23:
                            case 24: return "potion of vision";
                            case 25:
                            case 26:
                            case 27:
                            case 28: return "potion of swimming";
                            case 29:
                            case 30:
                            case 31:
                            case 32: return "potion of hiding";
                            case 33:
                            case 34:
                            case 35:
                            case 36: return "potion of sneaking";
                            case 37: return "oil of timelessness";
                            case 38:
                            case 39:
                            case 40:
                            case 41:
                            case 42: return "potion of reduce (5)";
                            case 43:
                            case 44:
                            case 45:
                            case 46:
                            case 47: return "potion of enlarge (5)";
                            case 48:
                            case 49:
                            case 50: return "potion of speak with animals";
                            case 51:
                            case 52:
                            case 53: return "potion of clairvoyance/clairaudience";
                            case 54:
                            case 55:
                            case 56: return "potion of charisma";
                            case 57:
                            case 58:
                            case 59: return "potion of intelligence";
                            case 60:
                            case 61:
                            case 62: return "potion of wisdom";
                            case 63:
                            case 64:
                            case 65: return "potion of alter self";
                            case 66:
                            case 67:
                            case 68: return "potion of blur";
                            case 69:
                            case 70:
                            case 71: return "potion of darkvision";
                            case 72:
                            case 73:
                            case 74: return "potion of ghoul touch";
                            case 75:
                            case 76:
                            case 77: return "potion of delay poison";
                            case 78:
                            case 79:
                            case 80: return "potion of endurance";
                            case 81:
                            case 82:
                            case 83: return "potion of cure moderate wounds";
                            case 84:
                            case 85:
                            case 86: return "potion of detect thoughts";
                            case 87:
                            case 88:
                            case 89: return "potion of levitate";
                            case 90:
                            case 91: return "potion of aid";
                            case 92:
                            case 93: return "potion of invisibility";
                            case 94: return "potion of lesser restoration";
                            case 95: return "potion of cat's grace";
                            case 96: return "potion of bull's strength";
                            case 97: return "potion of truth";
                            case 98: return "potion of glibness";
                            case 99: return "potion of nondetection";
                            case 100: return "potion of tongues";
                            default: return "potion of cure light wounds";
                        }
                    case POWER.MEDIUM:
                        switch (Dice.Percentile())
                        {
                            case 1: return "potion of love";
                            case 2: return "potion of vision";
                            case 3: return "potion of swimming";
                            case 4: return "potion of hiding";
                            case 5: return "potion of sneaking";
                            case 6: return "oil of timelessness";
                            case 7: return "potion of reduce (5)";
                            case 8: return "potion of enlarge (5)";
                            case 9: return "potion of speak with animals";
                            case 10: return "potion of clairvoyance/clairaudience";
                            case 11:
                            case 12: return "potion of charisma";
                            case 13:
                            case 14: return "potion of intelligence";
                            case 15:
                            case 16: return "potion of wisdom";
                            case 17:
                            case 18: return "potion of alter self";
                            case 19:
                            case 20:
                            case 21: return "potion of blur";
                            case 22:
                            case 23:
                            case 24: return "potion of darkvision";
                            case 25:
                            case 26: return "potion of ghoul touch";
                            case 27:
                            case 28:
                            case 29: return "potion of delay poison";
                            case 30:
                            case 31:
                            case 32: return "potion of endurance";
                            case 33:
                            case 34:
                            case 35:
                            case 36:
                            case 37:
                            case 38:
                            case 39:
                            case 40: return "potion of cure moderate wounds";
                            case 41:
                            case 42:
                            case 43:
                            case 44:
                            case 45: return "potion of detect thoughts";
                            case 46:
                            case 47:
                            case 48:
                            case 49:
                            case 50: return "potion of levitate";
                            case 51:
                            case 52:
                            case 53:
                            case 54:
                            case 55: return "potion of aid";
                            case 56:
                            case 57:
                            case 58:
                            case 59:
                            case 60: return "potion of invisibility";
                            case 61:
                            case 62:
                            case 63:
                            case 64:
                            case 65: return "potion of lesser restoration";
                            case 66:
                            case 67:
                            case 68:
                            case 69:
                            case 70: return "potion of cat's grace";
                            case 71:
                            case 72:
                            case 73:
                            case 74:
                            case 75: return "potion of bull's strength";
                            case 76:
                            case 77: return "potion of truth";
                            case 78:
                            case 79: return "potion of glibness";
                            case 80:
                            case 81:
                            case 82:
                            case 83:
                            case 84: return "potion of nondetection";
                            case 85:
                            case 86:
                            case 87: return "potion of tongues";
                            case 88:
                            case 89:
                            case 90:
                            case 91: return "potion of water breathing";
                            case 92: return "potion of remove paralysis";
                            case 93: return "potion of remove blindness/deafness";
                            case 94: return "potion of remove disease";
                            case 95:
                            case 96: return "potion of neutralize poison";
                            case 97: return "potion of cure serious wounds";
                            case 98: return "potion of fly";
                            default: return "potion of heroism";
                        }
                    case POWER.MAJOR:
                        switch (Dice.Percentile())
                        {
                            case 1: return "potion of clairvoyance/clairaudience";
                            case 2: return "potion of charisma";
                            case 3: return "potion of intelligence";
                            case 4: return "potion of wisdom";
                            case 5: return "potion of alter self";
                            case 6:
                            case 7: return "potion of blur";
                            case 8: return "potion of darkvision";
                            case 9: return "potion of ghoul touch";
                            case 10: return "potion of delay poison";
                            case 11:
                            case 12:
                            case 13: return "potion of endurance";
                            case 14:
                            case 15:
                            case 16: return "potion of cure moderate wounds";
                            case 17:
                            case 18:
                            case 19: return "potion of detect thoughts";
                            case 20:
                            case 21:
                            case 22: return "potion of levitate";
                            case 23:
                            case 24:
                            case 25: return "potion of aid";
                            case 26:
                            case 27:
                            case 28:
                            case 29:
                            case 30: return "potion of invisibility";
                            case 31:
                            case 32:
                            case 33:
                            case 34:
                            case 35: return "potion of lesser restoration";
                            case 36:
                            case 37:
                            case 38:
                            case 39:
                            case 40: return "potion of cat's grace";
                            case 41:
                            case 42:
                            case 43:
                            case 44:
                            case 45: return "potion of bull's strength";
                            case 46: return "potion of truth";
                            case 47: return "potion of glibness";
                            case 48:
                            case 49: return "potion of nondetection";
                            case 50:
                            case 51: return "potion of tongues";
                            case 52:
                            case 53: return "potion of water breathing";
                            case 54:
                            case 55: return "potion of remove paralysis";
                            case 56:
                            case 57: return "potion of remove blindness/deafness";
                            case 58:
                            case 59: return "potion of remove disease";
                            case 60:
                            case 61:
                            case 62:
                            case 63:
                            case 64:
                            case 65:
                            case 66:
                            case 67:
                            case 68:
                            case 69: return "potion of neutralize poison";
                            case 70:
                            case 71:
                            case 72:
                            case 73: return "potion of cure serious wounds";
                            case 74:
                            case 75: return "potion of fly";
                            case 76:
                            case 77: return "potion of protection from elements (cold)";
                            case 78:
                            case 79: return "potion of protection from elements (electricity)";
                            case 80:
                            case 81:
                            case 82:
                            case 83: return "potion of protection from elements (fire)";
                            case 84:
                            case 85: return "potion of protection from elements (acid)";
                            case 86:
                            case 87: return "potion of protection from elements (sonic)";
                            case 88:
                            case 89:
                            case 90: return "potion of haste";
                            case 91:
                            case 92:
                            case 93: return "potion of gaseous form";
                            case 94:
                            case 95: return "oil of slipperiness";
                            case 96:
                            case 97:
                            case 98: return "potion of heroism";
                            default: return "potion of fire breath";
                        }
                    default: return "ERROR: NONPOWERED POTION.  MagicItems.478";
                }
            }

            return output;
        }

        private static String Ring(POWER power)
        {
            String storedSpell;

            switch (power)
            {
                case POWER.MINOR:
                    switch (Dice.Percentile())
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5: return String.Format("ring of climbing ({0})", SpecialQualities(false, 1));
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: return String.Format("ring of jumping ({0})", SpecialQualities(false, 1));
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30: return String.Format("ring of warmth ({0})", SpecialQualities(false, 1));
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40: return String.Format("ring of feather falling ({0})", SpecialQualities(false, 1));
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45: return String.Format("ring of swimming ({0})", SpecialQualities(false, 1));
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                        case 50: return String.Format("ring of sustenance ({0})", SpecialQualities(false, 1));
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                            storedSpell = String.Empty;
                            if (Dice.Percentile() <= 50)
                                storedSpell = String.Format(" (contains {0})", Scrolls.RandomSpell(6));
                            return String.Format("ring of counterspells{0} ({1})", storedSpell, SpecialQualities(false, 1));
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60: return String.Format("ring of mind shielding ({0})", SpecialQualities(false, 2));
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                        case 65:
                        case 66:
                        case 67:
                        case 68:
                        case 69:
                        case 70: return String.Format("ring of protection +2 ({0})", SpecialQualities(false, 2));
                        case 71:
                        case 72:
                        case 73:
                        case 74:
                        case 75: return String.Format("ring of force shield ({0})", SpecialQualities(false, 2));
                        case 76:
                        case 77:
                        case 78:
                        case 79:
                        case 80: return String.Format("ring of ram ({0})", SpecialQualities(false, 2));
                        case 81:
                        case 82:
                        case 83:
                        case 84:
                        case 85: return String.Format("ring of animal friendship ({0})", SpecialQualities(false, 2));
                        case 86:
                        case 87:
                        case 88:
                        case 89:
                        case 90: return String.Format("ring of chameleon power ({0})", SpecialQualities(false, 2));
                        case 91:
                        case 92:
                        case 93:
                        case 94:
                        case 95: return String.Format("ring of water walking ({0})", SpecialQualities(false, 2));
                        case 96:
                        case 97:
                        case 98:
                        case 99:
                        case 100: return String.Format("ring of minor elemental resistance ({0})", SpecialQualities(false, 2));
                        default: return String.Format("ring of protection +1 ({0})", SpecialQualities(false, 1));
                    }
                case POWER.MEDIUM:
                    switch (Dice.Percentile())
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            storedSpell = String.Empty;
                            if (Dice.Percentile() <= 50)
                                storedSpell = String.Format(" (contains {0})", Scrolls.RandomSpell(6));
                            return String.Format("ring of counterspells{0} ({1})", storedSpell, SpecialQualities(false, 1));
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: return String.Format("ring of mind shielding ({0})", SpecialQualities(false, 2));
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20: return String.Format("ring of protection +2 ({0})", SpecialQualities(false, 2));
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25: return String.Format("ring of force shield ({0})", SpecialQualities(false, 2));
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30: return String.Format("ring of ram ({0})", SpecialQualities(false, 2));
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35: return String.Format("ring of animal friendship ({0})", SpecialQualities(false, 2));
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40: return String.Format("ring of chameleon power ({0})", SpecialQualities(false, 2));
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45: return String.Format("ring of water walking ({0})", SpecialQualities(false, 2));
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                        case 50: return String.Format("ring of minor elemental resistance ({0})", SpecialQualities(false, 2));
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60: return String.Format("ring of protection +3 ({0})", SpecialQualities(false, 3));
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                        case 65:
                        case 66:
                        case 67:
                        case 68:
                        case 69:
                        case 70: return String.Format("ring of invisibility ({0})", SpecialQualities(false, 3));
                        case 71:
                        case 72:
                        case 73:
                        case 74:
                        case 75: return String.Format("ring of wizardy (I, {0})", SpecialQualities(false, 3));
                        case 76:
                        case 77:
                        case 78:
                        case 79:
                        case 80: return String.Format("ring of major elemental resistance ({0})", SpecialQualities(false, 3));
                        case 81:
                        case 82: return String.Format("ring of x-ray vision ({0})", SpecialQualities(false, 3));
                        case 83:
                        case 84: return String.Format("ring of evasion ({0})", SpecialQualities(false, 3));
                        case 85:
                        case 86: return String.Format("ring of blinking ({0})", SpecialQualities(false, 3));
                        case 87:
                        case 88: return String.Format("ring of protection +4 ({0})", SpecialQualities(false, 4));
                        case 89:
                        case 90: return String.Format("ring of wizardry (II, {0})", SpecialQualities(false, 4));
                        case 91:
                        case 92: return String.Format("ring of freedom of movement ({0})", SpecialQualities(false, 4));
                        case 93:
                        case 94: return String.Format("ring of friend shield ({0})", SpecialQualities(false, 5));
                        case 95:
                        case 96: return String.Format("ring of protection +5 ({0})", SpecialQualities(false, 5));
                        case 97:
                        case 98: return String.Format("ring of shooting stars ({0})", SpecialQualities(false, 5));
                        case 99: return String.Format("ring of telekinesis ({0})", SpecialQualities(false, 6));
                        default: return String.Format("ring of wizardry (III, {0})", SpecialQualities(false, 6));
                    }
                case POWER.MAJOR:
                    switch (Dice.Percentile())
                    {
                        case 1: return String.Format("ring of ram ({0})", SpecialQualities(false, 2));
                        case 2: return String.Format("ring of animal friendship ({0})", SpecialQualities(false, 2));
                        case 3: return String.Format("ring of chameleon power ({0})", SpecialQualities(false, 2));
                        case 4: return String.Format("ring of water walking ({0})", SpecialQualities(false, 2));
                        case 5:
                        case 6: return String.Format("ring of minor elemental resistance ({0})", SpecialQualities(false, 2));
                        case 7:
                        case 8:
                        case 9:
                        case 10: return String.Format("ring of protection +3 ({0})", SpecialQualities(false, 3));
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15: return String.Format("ring of invisibility ({0})", SpecialQualities(false, 3));
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20: return String.Format("ring of wizardy (I, {0})", SpecialQualities(false, 3));
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25: return String.Format("ring of major elemental resistance ({0})", SpecialQualities(false, 3));
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30: return String.Format("ring of x-ray vision ({0})", SpecialQualities(false, 3));
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35: return String.Format("ring of evasion ({0})", SpecialQualities(false, 3));
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40: return String.Format("ring of blinking ({0})", SpecialQualities(false, 3));
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45: return String.Format("ring of protection +4 ({0})", SpecialQualities(false, 4));
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                        case 50: return String.Format("ring of wizardry (II, {0})", SpecialQualities(false, 4));
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55: return String.Format("ring of freedom of movement ({0})", SpecialQualities(false, 4));
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60: return String.Format("ring of friend shield ({0})", SpecialQualities(false, 5));
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                        case 65: return String.Format("ring of protection +5 ({0})", SpecialQualities(false, 5));
                        case 66:
                        case 67:
                        case 68:
                        case 69:
                        case 70: return String.Format("ring of shooting stars ({0})", SpecialQualities(false, 5));
                        case 71:
                        case 72:
                        case 73:
                        case 74:
                        case 75: return String.Format("ring of telekinesis ({0})", SpecialQualities(false, 6));
                        case 76:
                        case 77:
                        case 78:
                        case 79:
                        case 80: return String.Format("ring of wizardry (III, {0})", SpecialQualities(false, 6));
                        case 81:
                        case 82:
                        case 83:
                        case 84: return String.Format("ring of spell storing ({0}, {1})", Classes.WizardMemorizedSpell(Dice.Roll(1, 9)), SpecialQualities(false, 6));
                        case 85:
                        case 86:
                        case 87: return String.Format("ring of regeneration ({0})", SpecialQualities(false, 6));
                        case 88:
                        case 89: return String.Format("ring of three wishes ({0} charges, {1})", ChargesLeft(3, false), SpecialQualities(false, 6));
                        case 90:
                        case 91:
                        case 92: return String.Format("ring of wizardry (IV, {0})", SpecialQualities(false, 7));
                        case 93:
                        case 94: return String.Format("ring of djinni calling ({0})", SpecialQualities(false, 7));
                        case 95:
                        case 96: return String.Format("ring of spell turning ({0})", SpecialQualities(false, 8));
                        case 97: return String.Format("ring of air elemental command ({0})", SpecialQualities(false, 9));
                        case 98: return String.Format("ring of earth elemental command ({0})", SpecialQualities(false, 9));
                        case 99: return String.Format("ring of fire elemental command ({0})", SpecialQualities(false, 9));
                        default: return String.Format("ring of water elemental command ({0})", SpecialQualities(false, 9));
                    }
                default: return "ERROR: NONPOWERED MAGIC RING.  MagicItems.797";
            }
        }

        private static String Rod(POWER power)
        {
            switch (power)
            {
                case POWER.MEDIUM:
                    switch (Dice.Percentile())
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6: return String.Format("an immovable rod ({0})", SpecialQualities(false, 1));
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12: return String.Format("a rod of metal and mineral detection ({0})", SpecialQualities(false, 2));
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20: return String.Format("a rod of cancellation ({0})", SpecialQualities(false, 2));
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25: return String.Format("a rod of wonder ({0})", SpecialQualities(false, 2));
                        case 26:
                        case 27:
                        case 28:
                        case 29: return String.Format("a rod of the python ({0})", SpecialQualities(false, 2));
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34: return String.Format("a rod of flame extinguishing ({0})", SpecialQualities(false, 2));
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40: return String.Format("a rod of withering ({0})", SpecialQualities(false, 2));
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45: return String.Format("a rod of the viper ({0})", SpecialQualities(false, 3));
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                        case 50:
                        case 51:
                        case 52: return String.Format("a rod of thunder and lightning ({0})", SpecialQualities(false, 3));
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60: return String.Format("a rod of enemy detection ({0})", SpecialQualities(false, 3));
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                        case 65:
                        case 66:
                        case 67:
                        case 68: return String.Format("a rod of splendor ({0})", SpecialQualities(false, 3));
                        case 69:
                        case 70:
                        case 71:
                        case 72:
                        case 73:
                        case 74:
                        case 75:
                        case 76:
                        case 77:
                        case 78: return String.Format("a rod of negation ({0})", SpecialQualities(false, 4));
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
                        case 90: return String.Format("a rod of flailing ({0})", SpecialQualities(false, 4));
                        case 91:
                        case 92:
                        case 93:
                        case 94:
                        case 95:
                        case 96: return String.Format("a rod of absorption ({0} spell levels absorbed, {1})", ChargesLeft(50, true), SpecialQualities(false, 5));
                        case 97:
                        case 98:
                        case 99: return String.Format("a rod of rulership ({0})", SpecialQualities(false, 5));
                        default: return String.Format("a rod of security ({0})", SpecialQualities(false, 5));
                    }
                case POWER.MAJOR:
                    switch (Dice.Percentile())
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5: return String.Format("a rod of cancellation ({0})", SpecialQualities(false, 2));
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: return String.Format("a rod of wonder ({0})", SpecialQualities(false, 2));
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15: return String.Format("a rod of the python ({0})", SpecialQualities(false, 2));
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20: return String.Format("a rod of flame extinguishing ({0})", SpecialQualities(false, 2));
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27: return String.Format("a rod of withering ({0})", SpecialQualities(false, 2));
                        case 28:
                        case 29:
                        case 30:
                        case 31:
                        case 32:
                        case 33: return String.Format("a rod of the viper ({0})", SpecialQualities(false, 3));
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40: return String.Format("a rod of thunder and lightning ({0})", SpecialQualities(false, 3));
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                        case 50: return String.Format("a rod of enemy detection ({0})", SpecialQualities(false, 3));
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55: return String.Format("a rod of splendor ({0})", SpecialQualities(false, 3));
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                        case 65: return String.Format("a rod of negation ({0})", SpecialQualities(false, 4));
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
                        case 80: return String.Format("a rod of flailing ({0})", SpecialQualities(false, 4));
                        case 81:
                        case 82:
                        case 83:
                        case 84:
                        case 85: return String.Format("a rod of absorption ({0} spell levels absorbed, {1})", ChargesLeft(50, true), SpecialQualities(false, 5));
                        case 86:
                        case 87:
                        case 88:
                        case 89:
                        case 90: return String.Format("a rod of rulership ({0})", SpecialQualities(false, 5));
                        case 91:
                        case 92:
                        case 93:
                        case 94: return String.Format("a rod of security ({0})", SpecialQualities(false, 5));
                        case 95:
                        case 96:
                        case 97:
                        case 98: return String.Format("a rod of lordly might ({0})", SpecialQualities(false, 5));
                        default: return String.Format("a rod of alertness ({0})", SpecialQualities(false, 6));
                    }
                default: return "ERROR: NONPOWERED OR MINOR ROD.  MagicItems.1012";
            }
        }

        private static String Staff(POWER power)
        {
            switch (power)
            {
                case POWER.MEDIUM:
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
                        case 10: return String.Format("A staff of size alteration ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20: return String.Format("A staff of charming ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30: return String.Format("A staff of healing ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40: return String.Format("A staff of fire ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                        case 50: return String.Format("A staff of swarming insects ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60: return String.Format("A staff of frost ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                        case 65:
                        case 66:
                        case 67:
                        case 68:
                        case 69:
                        case 70: return String.Format("A staff of earth and stone ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 71:
                        case 72:
                        case 73:
                        case 74:
                        case 75:
                        case 76:
                        case 77:
                        case 78:
                        case 79:
                        case 80: return String.Format("A staff of defense ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 81:
                        case 82:
                        case 83:
                        case 84:
                        case 85:
                        case 86:
                        case 87:
                        case 88:
                        case 89: return String.Format("A staff of woodlands ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 90:
                        case 91:
                        case 92:
                        case 93:
                        case 94:
                        case 95: return String.Format("A staff of life ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        default: return String.Format("A staff of passage ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                    }
                case POWER.MAJOR:
                    switch (Dice.Percentile())
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5: return String.Format("A staff of charming ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15: return String.Format("A staff of healing ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
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
                        case 30: return String.Format("A staff of fire ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40: return String.Format("A staff of swarming insects ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                        case 50: return String.Format("A staff of frost ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60: return String.Format("A staff of earth and stone ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 61:
                        case 62:
                        case 63:
                        case 64:
                        case 65:
                        case 66:
                        case 67:
                        case 68:
                        case 69:
                        case 70: return String.Format("A staff of defense ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 71:
                        case 72:
                        case 73:
                        case 74:
                        case 75:
                        case 76:
                        case 77:
                        case 78:
                        case 79:
                        case 80: return String.Format("A staff of woodlands ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 81:
                        case 82:
                        case 83:
                        case 84:
                        case 85:
                        case 86:
                        case 87:
                        case 88:
                        case 89:
                        case 90: return String.Format("A staff of life ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 91:
                        case 92:
                        case 93:
                        case 94:
                        case 95:
                        case 96: return String.Format("A staff of passage ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        default: return String.Format("A staff of power ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                    }
                default: return "ERROR: NONPOWERED OR MINOR STAFF.  MagicItems.1221";
            }
        }

        private static String Wand(POWER power)
        {
            switch (power)
            {
                case POWER.MINOR:
                    switch (Dice.Percentile())
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5: return String.Format("Wand of detect magic ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10: return String.Format("Wand of light ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15: return String.Format("Wand of detect secret doors ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20: return String.Format("Wand of color spray ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25: return String.Format("Wand of burning hands ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30: return String.Format("Wand of charm person ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35: return String.Format("Wand of enlarge ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40: return String.Format("Wand of magic missile (1, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45: return String.Format("Wand of shocking grasp ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                        case 50: return String.Format("Wand of summon monster I ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55: return String.Format("Wand of cure light wounds ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 56:
                        case 57:
                        case 58: return String.Format("Wand of magic missile (3, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 59: return String.Format("Wand of magic missile (5, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 60:
                        case 61:
                        case 62:
                        case 63: return String.Format("Wand of levitate ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 64:
                        case 65:
                        case 66: return String.Format("Wand of summon monster II ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 67:
                        case 68:
                        case 69: return String.Format("Wand of silence ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 70:
                        case 71:
                        case 72: return String.Format("Wand of knock ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 73:
                        case 74:
                        case 75: return String.Format("Wand of daylight ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 76:
                        case 77:
                        case 78: return String.Format("Wand of invisibility ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 79:
                        case 80:
                        case 81: return String.Format("Wand of shatter ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 82:
                        case 83:
                        case 84: return String.Format("Wand of bull's strength ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 85:
                        case 86:
                        case 87: return String.Format("Wand of mirror image ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 88:
                        case 89:
                        case 90: return String.Format("Wand of ghoul touch ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 91:
                        case 92:
                        case 93: return String.Format("Wand of cure moderate wounds ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 94:
                        case 95:
                        case 96: return String.Format("Wand of hold person ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 97:
                        case 98: return String.Format("Wand of acid arrow ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 99: return String.Format("Wand of web ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        default: return String.Format("Wand of darkness ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                    }
                case POWER.MEDIUM:
                    switch (Dice.Percentile())
                    {
                        case 1:
                        case 2:
                        case 3: return String.Format("Wand of charm person ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 4:
                        case 5:
                        case 6: return String.Format("Wand of enlarge ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 7:
                        case 8:
                        case 9: return String.Format("Wand of magic missile (1, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 10:
                        case 11:
                        case 12: return String.Format("Wand of shocking grasp ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 13:
                        case 14:
                        case 15: return String.Format("Wand of summon monster I ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 16:
                        case 17:
                        case 18: return String.Format("Wand of cure light wounds ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 19:
                        case 20:
                        case 21: return String.Format("Wand of magic missile (3, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 22:
                        case 23: return String.Format("Wand of magic missile (5, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 24:
                        case 25:
                        case 26: return String.Format("Wand of levitate ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 27:
                        case 28:
                        case 29: return String.Format("Wand of summon monster II ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 30:
                        case 31:
                        case 32: return String.Format("Wand of silence ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 33:
                        case 34:
                        case 35: return String.Format("Wand of knock ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 36:
                        case 37:
                        case 38: return String.Format("Wand of daylight ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 39:
                        case 40:
                        case 41: return String.Format("Wand of invisibility ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 42:
                        case 43:
                        case 44: return String.Format("Wand of shatter ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 45:
                        case 46:
                        case 47:
                        case 48: return String.Format("Wand of bull's strength ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 49:
                        case 50: return String.Format("Wand of mirror image ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 51:
                        case 52:
                        case 53: return String.Format("Wand of ghoul touch ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60: return String.Format("Wand of cure moderate wounds ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 61:
                        case 62:
                        case 63: return String.Format("Wand of hold person ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 64:
                        case 65:
                        case 66: return String.Format("Wand of acid arrow ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 67:
                        case 68:
                        case 69: return String.Format("Wand of web ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 70:
                        case 71: return String.Format("Wand of darkness ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 72: return String.Format("Wand of magic missile (7, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 73:
                        case 74:
                        case 75: return String.Format("Wand of fireball (3, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 76:
                        case 77:
                        case 78:
                        case 79:
                        case 80: return String.Format("Wand of lightning bolt (3, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 81:
                        case 82: return String.Format("Wand of summon monster III ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 83:
                        case 84: return String.Format("Wand of keen edge ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 85:
                        case 86: return String.Format("Wand of major image ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 87:
                        case 88: return String.Format("Wand of slow ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 89:
                        case 90: return String.Format("Wand of suggestion ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 91:
                        case 92: return String.Format("Wand of dispel magic ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 93:
                        case 94: return String.Format("Wand of cure serious wounds ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 95: return String.Format("Wand of contagion ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 96: return String.Format("Wand of charm person (3, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 97: return String.Format("Wand of fireball (6, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 98:
                        case 99: return String.Format("Wand of searing light (6, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        default: return String.Format("Wand of lightning bolt (6, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                    }
                case POWER.MAJOR:
                    switch (Dice.Percentile())
                    {
                        case 1:
                        case 2: return String.Format("Wand of magic missile (5, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 3: return String.Format("Wand of levitate ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 4: return String.Format("Wand of summon monster II ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 5: return String.Format("Wand of silence ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 6: return String.Format("Wand of knock ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 7: return String.Format("Wand of daylight ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 8:
                        case 9:
                        case 10: return String.Format("Wand of invisibility ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 11:
                        case 12: return String.Format("Wand of shatter ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 13:
                        case 14:
                        case 15: return String.Format("Wand of bull's strength ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 16:
                        case 17: return String.Format("Wand of mirror image ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 18:
                        case 19: return String.Format("Wand of ghoul touch ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 20:
                        case 21: return String.Format("Wand of cure moderate wounds ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 22:
                        case 23: return String.Format("Wand of hold person ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 24:
                        case 25: return String.Format("Wand of acid arrow ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 26:
                        case 27: return String.Format("Wand of web ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 28:
                        case 29:
                        case 30: return String.Format("Wand of darkness ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 31:
                        case 32:
                        case 33: return String.Format("Wand of magic missile (7, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 34:
                        case 35:
                        case 36: return String.Format("Wand of magic missile (9, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 37:
                        case 38:
                        case 39: return String.Format("Wand of fireball (3, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 40:
                        case 41: return String.Format("Wand of lightning bolt (3, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 42:
                        case 43: return String.Format("Wand of summon monster III ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 44:
                        case 45: return String.Format("Wand of keen edge ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 46:
                        case 47: return String.Format("Wand of major image ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 48:
                        case 49: return String.Format("Wand of slow ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 50:
                        case 51: return String.Format("Wand of suggestion ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 52:
                        case 53: return String.Format("Wand of dispel magic ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 54:
                        case 55: return String.Format("Wand of cure serious wounds ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 56:
                        case 57: return String.Format("Wand of contagion ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 58: return String.Format("Wand of charm person (3, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 59: return String.Format("Wand of fireball (6, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 60:
                        case 61: return String.Format("Wand of searing light (6, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 62:
                        case 63: return String.Format("Wand of lightning bolt (6, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 64:
                        case 65: return String.Format("Wand of fireball (8, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 66:
                        case 67: return String.Format("Wand of lightning bolt (8, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 68:
                        case 69: return String.Format("Wand of charm monster ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 70:
                        case 71: return String.Format("Wand of fear ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 72:
                        case 73: return String.Format("Wand of improved invisibility ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 74:
                        case 75: return String.Format("Wand of polymorph self ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 76:
                        case 77: return String.Format("Wand of polymorph other ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 78:
                        case 79: return String.Format("Wand of ice storm ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 80:
                        case 81: return String.Format("Wand of summon monster IV ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 82:
                        case 83: return String.Format("Wand of wall of ice ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 84: return String.Format("Wand of wall of fire ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 85: return String.Format("Wand of ray of enfeeblement (4, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 86: return String.Format("Wand of poison ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 87: return String.Format("Wand of suggestion (4, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 88:
                        case 89: return String.Format("Wand of neutralize poison ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 90: return String.Format("Wand of inflict critical wounds ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 91:
                        case 92: return String.Format("Wand of cure critical wounds ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 93: return String.Format("Wand of restoration ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 94: return String.Format("Wand of fireball (10, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 95: return String.Format("Wand of lightning bolt (10, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 96: return String.Format("Wand of holy smite (8, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 97: return String.Format("Wand of chaos hammer (8, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 98: return String.Format("Wand of unholy blight (8, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        case 99: return String.Format("Wand of order's wrath (8, {0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                        default: return String.Format("Wand of stoneskin ({0} charges, {1})", ChargesLeft(50, false), StaffWandSpecialQualities());
                    }
                default: return "ERROR: NONPOWERED WAND.  MagicItems.1541";
            }
        }

        private static String WondrousItem(POWER power)
        {
            switch (power)
            {
                case POWER.MINOR:
                    switch (Dice.Roll(1, 98))
                    {
                        case 1: return String.Format("ioun stone (dull gray) ({0})", SpecialQualities(false, 0));
                        case 2: return String.Format("Quaal's feather token (anchor) ({0})", SpecialQualities(true, 0));
                        case 3: return String.Format("everburning torch ({0})", SpecialQualities(false, 0));
                        case 4: return String.Format("Quaal's feather token (tree) ({0})", SpecialQualities(true, 0));
                        case 5: return String.Format("Quaal's feather token (fan) ({0})", SpecialQualities(true, 0));
                        case 6: return String.Format("dust of tracelessness ({0})", SpecialQualities(true, 0));
                        case 7: return String.Format("Quaal's feather token (bird) ({0})", SpecialQualities(true, 0));
                        case 8: return String.Format("Quaal's feather token (swan boat) ({0})", SpecialQualities(true, 0));
                        case 9: return String.Format("dust of illusion ({0})", SpecialQualities(true, 0));
                        case 10: return String.Format("necklace of prayer beads (blessing) ({0})", SpecialQualities(false, 0));
                        case 11: return String.Format("Quaal's feather token (whip) ({0})", SpecialQualities(true, 0));
                        case 12: return String.Format("flesh golembane scarab ({0})", SpecialQualities(false, 0));
                        case 13: return String.Format("gray bag of tricks ({0})", SpecialQualities(false, 0));
                        case 14: return String.Format("dust of dryness ({0})", SpecialQualities(true, 0));
                        case 15: return String.Format("+1 bracers of armor ({0})", SpecialQualities(false, 1));
                        case 16: return String.Format("+1 cloak of resistance ({0})", SpecialQualities(false, 1));
                        case 17: return String.Format("eye of the eagle ({0})", SpecialQualities(false, 1));
                        case 18: return String.Format("goggles of minute seeing ({0})", SpecialQualities(false, 1));
                        case 19: return String.Format("hand of the mage ({0})", SpecialQualities(false, 1));
                        case 20: return String.Format("pearl of power (1st-level spell) ({0})", SpecialQualities(false, 1));
                        case 21: return String.Format("phylactery of faithfulness ({0})", SpecialQualities(false, 1));
                        case 22: return String.Format("clay golembane scarab ({0})", SpecialQualities(false, 1));
                        case 23: return String.Format("stone of alarm ({0})", SpecialQualities(false, 1));
                        case 24: return String.Format("pipes of the sewers ({0})", SpecialQualities(false, 1));
                        case 25: return String.Format("stone golembane scarab ({0})", SpecialQualities(false, 1));
                        case 26: return String.Format("brooch of shielding ({0})", SpecialQualities(false, 1));
                        case 27: return String.Format("iron golembane scarab ({0})", SpecialQualities(false, 1));
                        case 28: return String.Format("Type I necklace of fireballs ({0})", SpecialQualities(true, 1));
                        case 29: return String.Format("pipes of sounding ({0})", SpecialQualities(false, 1));
                        case 30: return String.Format("quiver of Ehlonna ({0})", SpecialQualities(false, 1));
                        case 31: return String.Format("flesh and clay golembane scarab ({0})", SpecialQualities(false, 1));
                        case 32: return String.Format("horseshoes of speed ({0})", SpecialQualities(false, 1));
                        case 33: return String.Format("+1 amulet of natural armor ({0})", SpecialQualities(false, 1));
                        case 34: return String.Format("bead of force ({0})", SpecialQualities(false, 1));
                        case 35: return String.Format("boots of elvenkind ({0})", SpecialQualities(false, 1));
                        case 36: return String.Format("cloak of elvenkind ({0})", SpecialQualities(false, 1));
                        case 37: return String.Format("hat of disguise ({0})", SpecialQualities(false, 1));
                        case 38: return String.Format("Heward's handy haversack ({0})", SpecialQualities(false, 1));
                        case 39: return String.Format("horn of fog ({0})", SpecialQualities(false, 1));
                        case 40: return String.Format("slippers of spider climbing ({0})", SpecialQualities(false, 1));
                        case 41: return String.Format("universal solvent ({0})", SpecialQualities(true, 1));
                        case 42: return String.Format("vest of escape ({0})", SpecialQualities(false, 1));
                        case 43: return String.Format("dust of appearance ({0})", SpecialQualities(true, 1));
                        case 44: return String.Format("glove of storing ({0})", SpecialQualities(false, 1));
                        case 45: return String.Format("sovereign glue ({0})", SpecialQualities(true, 1));
                        case 46: return String.Format("candle of truth ({0})", SpecialQualities(true, 1));
                        case 47: return String.Format("Type 1 Bag of holding ({0})", SpecialQualities(false, 1));
                        case 48: return String.Format("boots of the winterlands ({0})", SpecialQualities(false, 1));
                        case 49: return String.Format("boots of striding and springing ({0})", SpecialQualities(false, 1));
                        case 50: return String.Format("all golems golembane scarab ({0})", SpecialQualities(false, 1));
                        case 51: return String.Format("helm of comprehending languages and reading magic ({0})", SpecialQualities(false, 1));
                        case 52: return String.Format("Type II necklace of fireballs ({0})", SpecialQualities(true, 1));
                        case 53: return String.Format("rust bag of tricks ({0})", SpecialQualities(false, 1));
                        case 54: return String.Format("chime of opening ({0})", SpecialQualities(false, 1));
                        case 55: return String.Format("rope of climbing ({0})", SpecialQualities(false, 1));
                        case 56: return String.Format("horseshoes of a zephyr ({0})", SpecialQualities(false, 1));
                        case 57: return String.Format("dust of disappearance ({0})", SpecialQualities(true, 1));
                        case 58: return String.Format("lens of detection ({0})", SpecialQualities(false, 1));
                        case 59: return String.Format("silver raven figurine of wondrous power ({0})", SpecialQualities(false, 1));
                        case 60: return String.Format("+2 bracers of armor ({0})", SpecialQualities(false, 2));
                        case 61: return String.Format("+2 cloak of resistance ({0})", SpecialQualities(false, 2));
                        case 62: return String.Format("gloves of arrow snaring ({0})", SpecialQualities(false, 2));
                        case 63: return String.Format("dusty rose prism ioun stone ({0})", SpecialQualities(false, 2));
                        case 64: return String.Format("Keoghtom's ointment ({0})", SpecialQualities(true, 2));
                        case 65: return String.Format("pearl of power (2nd-level spell) ({0})", SpecialQualities(false, 2));
                        case 66: return String.Format("periapt of proof against poison ({0})", SpecialQualities(false, 2));
                        case 67: return String.Format("stone salve ({0})", SpecialQualities(true, 2));
                        case 68: return String.Format("gauntlets of ogre power ({0})", SpecialQualities(false, 2));
                        case 69: return String.Format("+2 bracers of health ({0})", SpecialQualities(false, 2));
                        case 70: return String.Format("+2 gloves of dexterity ({0})", SpecialQualities(false, 2));
                        case 71: return String.Format("+2 headband of intellect ({0})", SpecialQualities(false, 2));
                        case 72: return String.Format("+2 periapt of wisdom ({0})", SpecialQualities(false, 2));
                        case 73: return String.Format("+2 cloak of charisma ({0})", SpecialQualities(false, 2));
                        case 74: return String.Format("Type III necklace of fireballs ({0})", SpecialQualities(true, 2));
                        case 75: return String.Format("circlet of persuasion ({0})", SpecialQualities(false, 2));
                        case 76: return String.Format("bracelet of friends ({0})", SpecialQualities(true, 2));
                        case 77: return String.Format("incense of meditation ({0})", SpecialQualities(true, 2));
                        case 78: return String.Format("Type 2 bag of holding ({0})", SpecialQualities(false, 2));
                        case 79: return String.Format("clear spindle ioun stone ({0})", SpecialQualities(false, 2));
                        case 80: return String.Format("karma necklace of prayer beads ({0})", SpecialQualities(false, 2));
                        case 81: return String.Format("bracers of archery ({0})", SpecialQualities(false, 2));
                        case 82: return String.Format("eversmoking bottle ({0})", SpecialQualities(false, 2));
                        case 83: return String.Format("Type IV necklace of fireballs ({0})", SpecialQualities(true, 2));
                        case 84: return String.Format("Murlynd's spoon ({0})", SpecialQualities(false, 2));
                        case 85: return String.Format("Nolzur's marvelous pigments ({0})", SpecialQualities(true, 2));
                        case 86: return String.Format("wind fan ({0})", SpecialQualities(false, 2));
                        case 87: return String.Format("wings of flying ({0})", SpecialQualities(false, 2));
                        case 88: return String.Format("Druid's vestment ({0})", SpecialQualities(false, 2));
                        case 89: return String.Format("cloak of arachnida ({0})", SpecialQualities(false, 2));
                        case 90: return String.Format("gloves of swimming and climbing ({0})", SpecialQualities(false, 2));
                        case 91: return String.Format("horn of goodness/evil ({0})", SpecialQualities(false, 2));
                        case 92: return String.Format("Type V necklace of fireballs ({0})", SpecialQualities(true, 2));
                        case 93: return String.Format("tan bag of tricks ({0})", SpecialQualities(false, 2));
                        case 94: return String.Format("minor circlet of blasting ({0})", SpecialQualities(false, 2));
                        case 95: return String.Format("pipes of haunting ({0})", SpecialQualities(false, 2));
                        case 96: return String.Format("robe of useful items ({0})", SpecialQualities(true, 2));
                        case 97: return String.Format("hand of glory ({0})", SpecialQualities(false, 2));
                        case 98: return String.Format("Type 3 bag of holding ({0})", SpecialQualities(false, 2));
                        default: return WondrousItem(POWER.MEDIUM);
                    }
                case POWER.MEDIUM:
                    switch (Dice.Roll(1, 99))
                    {
                        case 1: return String.Format("boots of levitation ({0})", SpecialQualities(false, 2));
                        case 2: return String.Format("harp of charming ({0})", SpecialQualities(false, 2));
                        case 3: return String.Format("periapt of health ({0})", SpecialQualities(false, 2));
                        case 4: return String.Format("candle of invocation ({0})", SpecialQualities(true, 2));
                        case 5: return String.Format("+2 amulet of natural armor ({0})", SpecialQualities(false, 2));
                        case 6: return String.Format("boots of speed ({0})", SpecialQualities(false, 2));
                        case 7: return String.Format("ioun stone (dark blue rhomboid, {0})", SpecialQualities(false, 2));
                        case 8: return String.Format("ioun stone (deep red sphere, {0})", SpecialQualities(false, 2));
                        case 9: return String.Format("ioun stone (incandescent blue sphere, {0})", SpecialQualities(false, 2));
                        case 10: return String.Format("ioun stone (pale blue rhomboid, {0})", SpecialQualities(false, 2));
                        case 11: return String.Format("ioun stone (pink rhomboid, {0})", SpecialQualities(false, 2));
                        case 12: return String.Format("ioun stone (pink and green sphere, {0})", SpecialQualities(false, 2));
                        case 13: return String.Format("ioun stone (scarlet and blue sphere, {0})", SpecialQualities(false, 2));
                        case 14: return String.Format("goggles of night ({0})", SpecialQualities(false, 2));
                        case 15: return String.Format("necklace of fireballs VI ({0})", SpecialQualities(true, 2));
                        case 16: return String.Format("monk's belt ({0})", SpecialQualities(false, 2));
                        case 17: return String.Format("bracers of armor +3 ({0})", SpecialQualities(false, 3));
                        case 18: return String.Format("cloak of resistance +3 ({0})", SpecialQualities(false, 3));
                        case 19: return String.Format("decanter of endless water ({0})", SpecialQualities(false, 3));
                        case 20: return String.Format("pearl of power (3, {0})", SpecialQualities(false, 3));
                        case 21: return String.Format("talisman of the sphere ({0})", SpecialQualities(false, 3));
                        case 22: return String.Format("figurine of wondrous power (serpentine owl, {0})", SpecialQualities(false, 3));
                        case 23: return String.Format("necklace of fireballs VII ({0})", SpecialQualities(true, 3));
                        case 24: return String.Format("deck of illusions ({0})", SpecialQualities(true, 3));
                        case 25: return String.Format("Boccob's blessed book ({0})", SpecialQualities(false, 3));
                        case 26: return String.Format("Bag of holding Type 4 ({0})", SpecialQualities(false, 3));
                        case 27: return String.Format("figurine of wondrous power (bronze griffon, {0})", SpecialQualities(false, 3));
                        case 28: return String.Format("figurine of wondrous power (ebony fly, {0})", SpecialQualities(false, 3));
                        case 29: return String.Format("necklace of prayer beads (healing, {0})", SpecialQualities(false, 3));
                        case 30: return String.Format("robe of blending ({0})", SpecialQualities(false, 3));
                        case 31: return String.Format("stone of good luck ({0})", SpecialQualities(false, 3));
                        case 32: return String.Format("stone horse (courser, {0})", SpecialQualities(false, 3));
                        case 33: return String.Format("folding boat ({0})", SpecialQualities(false, 3));
                        case 34: return String.Format("amulet of undead turning ({0})", SpecialQualities(false, 3));
                        case 35: return String.Format("gauntlet of rust ({0})", SpecialQualities(false, 3));
                        case 36: return String.Format("winged boots ({0})", SpecialQualities(false, 3));
                        case 37: return String.Format("horn of blasting ({0})", SpecialQualities(false, 3));
                        case 38: return String.Format("ioun stone (vibrant purple prism, {0}, {1})", Classes.WizardMemorizedSpell(Dice.Roll(1, 9)), SpecialQualities(false, 3));
                        case 39: return String.Format("medallion of thoughts ({0})", SpecialQualities(false, 3));
                        case 40: return String.Format("pipes of pain ({0})", SpecialQualities(false, 3));
                        case 41: return String.Format("cape of the mountebank ({0})", SpecialQualities(false, 3));
                        case 42: return String.Format("lyre of building ({0})", SpecialQualities(false, 3));
                        case 43: return String.Format("portable hole ({0})", SpecialQualities(false, 3));
                        case 44: return String.Format("bottle of air ({0})", SpecialQualities(false, 3));
                        case 45: return String.Format("stone horse (destrier, {0})", SpecialQualities(false, 3));
                        case 46: return String.Format("belt of dwarvenkind ({0})", SpecialQualities(false, 3));
                        case 47: return String.Format("ioun stone (iridescent spindle, {0})", SpecialQualities(false, 3));
                        case 48: return String.Format("necklace of prayer beads (smiting, {0})", SpecialQualities(false, 3));
                        case 49: return String.Format("periapt of wound closure ({0})", SpecialQualities(false, 3));
                        case 50: return String.Format("scabbard of keen edges ({0})", SpecialQualities(false, 3));
                        case 51: return String.Format("broom of flying ({0})", SpecialQualities(false, 3));
                        case 52: return String.Format("horn of the tritons ({0})", SpecialQualities(false, 3));
                        case 53: return String.Format("gem of brightness ({0})", SpecialQualities(false, 3));
                        case 54: return String.Format("pearl of the sirines ({0})", SpecialQualities(false, 3));
                        case 55: return String.Format("figurine of wondrous power (onyx dog, {0})", SpecialQualities(false, 3));
                        case 56: return String.Format("chime of interruption ({0})", SpecialQualities(false, 3));
                        case 57: return String.Format("bracers of armor +4 ({0})", SpecialQualities(false, 4));
                        case 58: return String.Format("cloak of resistance +4 ({0})", SpecialQualities(false, 4));
                        case 59: return String.Format("pearl of power (4, {0})", SpecialQualities(false, 4));
                        case 60: return String.Format("belt of giant strength +4 ({0})", SpecialQualities(false, 4));
                        case 61: return String.Format("gloves of dexterity +4 ({0})", SpecialQualities(false, 4));
                        case 62: return String.Format("bracers of health +4 ({0})", SpecialQualities(false, 4));
                        case 63: return String.Format("headband of intellect +4 ({0})", SpecialQualities(false, 4));
                        case 64: return String.Format("periapt of wisdom +4 ({0})", SpecialQualities(false, 4));
                        case 65: return String.Format("cloak of charisma +4 ({0})", SpecialQualities(false, 4));
                        case 66: return String.Format("figurine of wondrous power (golden lions, {0})", SpecialQualities(false, 4));
                        case 67: return String.Format("figurine of wondrous power (marble elephant, {0})", SpecialQualities(false, 4));
                        case 68: return String.Format("amulet of natural armor +3 ({0})", SpecialQualities(false, 4));
                        case 69: return String.Format("carpet of flying (3 x 5, {0})", SpecialQualities(false, 4));
                        case 70: return String.Format("necklace of adaptation ({0})", SpecialQualities(false, 4));
                        case 71: return String.Format("cloak of the manta ray ({0})", SpecialQualities(false, 4));
                        case 72: return String.Format("ioun stone (pale green prism, {0})", SpecialQualities(false, 4));
                        case 73: return String.Format("ioun stone (pale lavender ellipsoid, {0} charges, {1})", ChargesLeft(20, false), SpecialQualities(false, 4));
                        case 74: return String.Format("ioun stone (pearly white spindle, {0})", SpecialQualities(false, 4));
                        case 75: return String.Format("figurine of wondrous power (ivory goats, {0})", SpecialQualities(false, 4));
                        case 76: return String.Format("rope of entanglement ({0})", SpecialQualities(false, 4));
                        case 77: return String.Format("cube of frost resistance ({0})", SpecialQualities(false, 4));
                        case 78: return String.Format("mattock of the titans ({0})", SpecialQualities(false, 4));
                        case 79: return String.Format("major circlet of blasting ({0})", SpecialQualities(false, 4));
                        case 80: return String.Format("cloak of the bat ({0})", SpecialQualities(false, 4));
                        case 81: return String.Format("helm of underwater action ({0})", SpecialQualities(false, 4));
                        case 82: return String.Format("eyes of doom ({0})", SpecialQualities(false, 4));
                        case 83: return String.Format("minor cloak of displacement ({0})", SpecialQualities(false, 5));
                        case 84: return String.Format("cloak of resistance +5 ({0})", SpecialQualities(false, 5));
                        case 85: return String.Format("mask of the skull ({0})", SpecialQualities(false, 5));
                        case 86: return String.Format("maul of the titans ({0})", SpecialQualities(false, 5));
                        case 87: return String.Format("pearl of power (5, {0})", SpecialQualities(false, 5));
                        case 88: return String.Format("bracers of armor +5 ({0})", SpecialQualities(false, 5));
                        case 89: return String.Format("dimensional shackles ({0})", SpecialQualities(false, 5));
                        case 90: return String.Format("iron banda of Bilarro ({0})", SpecialQualities(false, 5));
                        case 91: return String.Format("robe of scintillating colors ({0})", SpecialQualities(false, 5));
                        case 92: return String.Format("manual of bodily health +1 ({0})", SpecialQualities(true, 5));
                        case 93: return String.Format("manual of gainful exercise +1 ({0})", SpecialQualities(true, 5));
                        case 94: return String.Format("manual of quickness in action +1 ({0})", SpecialQualities(true, 5));
                        case 95: return String.Format("Tome of clear thought +1 ({0})", SpecialQualities(true, 5));
                        case 96: return String.Format("Tome of leadership and influence +1 ({0})", SpecialQualities(true, 5));
                        case 97: return String.Format("Tome of understanding +1 ({0})", SpecialQualities(true, 5));
                        case 98: return String.Format("figurine of wondrous power (obsidian steed, {0})", SpecialQualities(false, 5));
                        case 99: return String.Format("carpet of flying (4 x 6, {0})", SpecialQualities(false, 5));
                        case 100: return WondrousItem(POWER.MAJOR);
                        default: return "[Error: Percentile out of range.  MagicItems.1754]";
                    }
                case POWER.MAJOR:
                    switch (Dice.Percentile())
                    {
                        case 1:
                        case 2: return String.Format("lantern of revealing ({0})", SpecialQualities(false, 5));
                        case 3:
                        case 4: return String.Format("necklace of prayer beads (wind walking, {0})", SpecialQualities(false, 5));
                        case 5:
                        case 6: return String.Format("drums of panic ({0})", SpecialQualities(false, 5));
                        case 7:
                        case 8: return String.Format("helm of telepathy ({0})", SpecialQualities(false, 5));
                        case 9:
                        case 10: return String.Format("amulet of natural armor +4 ({0})", SpecialQualities(false, 5));
                        case 11:
                        case 12: return String.Format("amulet of proof against detection and location ({0})", SpecialQualities(false, 5));
                        case 13:
                        case 14: return String.Format("bracers of armor +6 ({0})", SpecialQualities(false, 6));
                        case 15: return String.Format("belt of giant's strength +6 ({0})", SpecialQualities(false, 6));
                        case 16: return String.Format("gloves of dexterity +6 ({0})", SpecialQualities(false, 6));
                        case 17: return String.Format("bracers of health +6 ({0})", SpecialQualities(false, 6));
                        case 18: return String.Format("headband of intellect +6 ({0})", SpecialQualities(false, 6));
                        case 19: return String.Format("periapt of wisdom +6 ({0})", SpecialQualities(false, 6));
                        case 20: return String.Format("cloak of charisma +6 ({0})", SpecialQualities(false, 6));
                        case 21:
                        case 22: return String.Format("pearl of power (6, {0})", SpecialQualities(false, 6));
                        case 23:
                        case 24: return String.Format("orb of storms ({0})", SpecialQualities(false, 6));
                        case 25:
                        case 26: return String.Format("scarab of protection ({0})", SpecialQualities(false, 6));
                        case 27:
                        case 28: return String.Format("ioun stone (lavender and green ellipsoid, {0} charges left, {1})", ChargesLeft(50, false), SpecialQualities(false, 6));
                        case 29:
                        case 30: return String.Format("ring gates ({0})", SpecialQualities(false, 6));
                        case 31: return String.Format("carpet of flying (5 x 7, {0})", SpecialQualities(false, 6));
                        case 32: return String.Format("crystal ball ({0})", SpecialQualities(false, 6));
                        case 33: return String.Format("helm of teleportation ({0})", SpecialQualities(false, 6));
                        case 34: return String.Format("bracers of armor +7 ({0})", SpecialQualities(false, 7));
                        case 35: return String.Format("pearl of power (7, {0})", SpecialQualities(false, 7));
                        case 36: return String.Format("amulet of natural armor +5, ({0})", SpecialQualities(false, 7));
                        case 37: return String.Format("major cloak of displacement ({0})", SpecialQualities(false, 7));
                        case 38: return String.Format("crystal ball with detect invisibility ({0})", SpecialQualities(false, 7));
                        case 39:
                            string HornType; 
                            var roll = Dice.Percentile();
                            if (roll <= 40)
                                HornType = "silver";
                            else if (roll <= 75)
                                HornType = "brass";
                            else if (roll <= 90)
                                HornType = "bronze";
                            else
                                HornType = "iron";
                            return String.Format("{0} horn of valhalla ({1})", HornType, SpecialQualities(false, 7));
                        case 40: return String.Format("necklace of prayer beads (summons, {0})", SpecialQualities(true, 7));
                        case 41: return String.Format("crystal ball with detect thoughts ({0})", SpecialQualities(false, 7));
                        case 42: return String.Format("cloak of etherealness ({0})", SpecialQualities(false, 7));
                        case 43: return String.Format("carpet of flying (6 x 9, {0})", SpecialQualities(false, 7));
                        case 44: return String.Format("Daern's instant fortress ({0})", SpecialQualities(false, 7));
                        case 45: return String.Format("manual of bodily health +2 ({0})", SpecialQualities(true, 7));
                        case 46: return String.Format("manual of gainful exercise +2 ({0})", SpecialQualities(true, 7));
                        case 47: return String.Format("manual of quickness in action +2 ({0})", SpecialQualities(true, 7));
                        case 48: return String.Format("tome of clear thought +2 ({0})", SpecialQualities(true, 7));
                        case 49: return String.Format("tome of leadership and influence +2 ({0})", SpecialQualities(true, 7));
                        case 50: return String.Format("tome of understanding +2 ({0})", SpecialQualities(true, 7));
                        case 51: return String.Format("eyes of charming ({0})", SpecialQualities(false, 7));
                        case 52: return String.Format("robe of stars ({0})", SpecialQualities(false, 7));
                        case 53: return String.Format("darkskull ({0})", SpecialQualities(false, 7));
                        case 54: return String.Format("cube of force ({0})", SpecialQualities(false, 7));
                        case 55: return String.Format("bracers of armor +8 ({0})", SpecialQualities(false, 8));
                        case 56: return String.Format("pearl of power (8, {0})", SpecialQualities(false, 8));
                        case 57: return String.Format("crystal ball with telepathy ({0})", SpecialQualities(false, 8));
                        case 58: return String.Format("pearl of power (two spells, {0})", SpecialQualities(false, 8));
                        case 59: return String.Format("gem of seeing ({0})", SpecialQualities(false, 8));
                        case 60: return String.Format("robe of the archmagi ({0})", SpecialQualities(false, 8));
                        case 61: return String.Format("vastments of faith ({0})", SpecialQualities(false, 8));
                        case 62: return String.Format("amulet of the planes ({0})", SpecialQualities(false, 8));
                        case 63: return String.Format("crystal ball with true seeing ({0})", SpecialQualities(false, 8));
                        case 64: return String.Format("pearl of power (9, {0})", SpecialQualities(false, 9));
                        case 65: return String.Format("well of many worlds ({0})", SpecialQualities(false, 9));
                        case 66: return String.Format("manual of bodily health +3 ({0})", SpecialQualities(true, 9));
                        case 67: return String.Format("manual of gainful exercise +3 ({0})", SpecialQualities(true, 9));
                        case 68: return String.Format("manual of quickness in action +3 ({0})", SpecialQualities(true, 9));
                        case 69: return String.Format("tome of clear thought +3 ({0})", SpecialQualities(true, 9));
                        case 70: return String.Format("tome of leadership and influence +3 ({0})", SpecialQualities(true, 9));
                        case 71: return String.Format("tome of understanding +3 ({0})", SpecialQualities(true, 9));
                        case 72: return String.Format("mantle of spell resistance ({0})", SpecialQualities(false, 9));
                        case 73: return String.Format("robe of eyes ({0})", SpecialQualities(false, 9));
                        case 74: return String.Format("mirror of opposition ({0})", SpecialQualities(false, 9));
                        case 75: return String.Format("chaos diamond ({0})", SpecialQualities(false, 9));
                        case 76: return String.Format("eyes of petrification ({0})", SpecialQualities(false, 9));
                        case 77: return String.Format("bowl of commanding water elementals ({0})", SpecialQualities(false, 10));
                        case 78: return String.Format("bowl of commanding fire elementals ({0})", SpecialQualities(false, 10));
                        case 79: return String.Format("bowl of commanding air elementals ({0})", SpecialQualities(false, 10));
                        case 80: return String.Format("bowl of commanding earth elementals ({0})", SpecialQualities(false, 10));
                        case 81: return String.Format("manual of bodily health +4 ({0})", SpecialQualities(true, 10));
                        case 82: return String.Format("manual of gainful exercise +4 ({0})", SpecialQualities(true, 10));
                        case 83: return String.Format("manual of quickness in action +4 ({0})", SpecialQualities(true, 10));
                        case 84: return String.Format("tome of clear thought +4 ({0})", SpecialQualities(true, 10));
                        case 85: return String.Format("tome of leadership and influence +4 ({0})", SpecialQualities(true, 10));
                        case 86: return String.Format("tome of understanding +4 ({0})", SpecialQualities(true, 10));
                        case 87: return String.Format("apparatus of Kwalish ({0})", SpecialQualities(false, 11));
                        case 88: return String.Format("manual of bodily health +5 ({0})", SpecialQualities(true, 11));
                        case 89: return String.Format("manual of gainful exercise +5 ({0})", SpecialQualities(true, 11));
                        case 90: return String.Format("manual of quickness in action +5 ({0})", SpecialQualities(true, 11));
                        case 91: return String.Format("tome of clear thought +5 ({0})", SpecialQualities(true, 11));
                        case 92: return String.Format("tome of leadership and influence +5 ({0})", SpecialQualities(true, 11));
                        case 93: return String.Format("tome of understanding +5 ({0})", SpecialQualities(true, 11));
                        case 94: return String.Format("efreeti bottle ({0})", SpecialQualities(false, 12));
                        case 95: return String.Format("mirror of life trapping ({0})", SpecialQualities(false, 12));
                        case 96: return String.Format("cubic gate ({0})", SpecialQualities(false, 12));
                        case 97: return String.Format("helm of brilliance ({0})", SpecialQualities(false, 12));
                        case 98:
                            var creature = String.Empty;
                            switch (Dice.Percentile())
                            {
                                case 51:
                                case 52:
                                case 53:
                                case 54: creature = "large air elemental"; break;
                                case 55:
                                case 56:
                                case 57:
                                case 58: creature = "arrowhawk"; break;
                                case 59:
                                case 60:
                                case 61:
                                case 62: creature = "large earth elemental"; break;
                                case 63:
                                case 64:
                                case 65:
                                case 66: creature = "xorn"; break;
                                case 67:
                                case 68:
                                case 69:
                                case 70: creature = "large fire elemental"; break;
                                case 71:
                                case 72:
                                case 73:
                                case 74: creature = "salamander"; break;
                                case 75:
                                case 76:
                                case 77:
                                case 78: creature = "large water elemental"; break;
                                case 79:
                                case 80:
                                case 81:
                                case 82: creature = "adult tojanida"; break;
                                case 83:
                                case 84: creature = "red slaad"; break;
                                case 85:
                                case 86: creature = "formian taskmaster"; break;
                                case 87: creature = "vrock"; break;
                                case 88: creature = "hezrou"; break;
                                case 89: creature = "glabrezu"; break;
                                case 90: creature = "succubus"; break;
                                case 91: creature = "osyluth"; break;
                                case 92: creature = "barbazu"; break;
                                case 93: creature = "erinyes"; break;
                                case 94: creature = "cornugon"; break;
                                case 95: creature = "avoral"; break;
                                case 96: creature = "ghaele"; break;
                                case 97: creature = "formian myrmarch"; break;
                                case 98: creature = "blue slaad"; break;
                                case 99: creature = "rakshasa"; break;
                                case 100:
                                    if (Dice.Percentile() <= 50)
                                        creature = "balor";
                                    else
                                        creature = "pit fiend";
                                    break;
                                default: break;
                            }
                            return String.Format("iron flask ({0}, {1})", creature, SpecialQualities(false, 13));
                        case 99: return String.Format("mirror of mental prowess ({0})", SpecialQualities(false, 13));
                        case 100: return WondrousItem(POWER.MAJOR) + " and " + WondrousItem(POWER.MAJOR);
                        default: return "[Error: Major Wondrous Item out of range.  MagicItems.1930]";
                    }
                default: return "ERROR: NONPOWERED WONDROUS ITEM.  MagicItems.1932";
            }
        }

        public static String SpecialQualities(Boolean charged, Int32 bonus)
        {
            if (bonus > 0)
            {
                var roll = Dice.Percentile();

                if (roll == 1 && !charged)
                    return Intelligence.Generate(bonus);
                if (roll < 32)
                    return "markings";
            }
            return String.Empty;
        }

        private static String StaffWandSpecialQualities()
        {
            if (Dice.Percentile() <= 30)
                return "markings";
            return String.Empty;
        }

        public static Int32 ChargesLeft(Int32 fullCharges, Boolean allowZero)
        {
            if (allowZero)
                return Dice.Roll(1, fullCharges + 1, -1);
            return Dice.Roll(1, fullCharges);
        }
    }
}
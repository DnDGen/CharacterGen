using NPCGen.Characters;
using NPCGen.Roll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NPCGen.Equipment
{
    public enum SPELLTYPE { ARCANE, DIVINE };

    public class Scrolls
    {
        public static String Generate(CLASS charClass, Int32 level, Int32 quantity)
        {
            var power = MagicItems.PowerByLevel(level);
            var output = String.Empty;
            var spellType = SpellTypeByClass(charClass);

            while (quantity-- > 0)
            {
                if (output != String.Empty)
                    output += "\n";
                output += "Scroll " + spellType.ToString() + ": " + Spells(spellType, power);
            }

            return output;
        }

        private static SPELLTYPE SpellTypeByClass(CLASS charClass)
        {
            switch (charClass)
            {
                case CLASS.CLERIC:
                case CLASS.DRUID:
                case CLASS.PALADIN:
                case CLASS.RANGER: return SPELLTYPE.DIVINE;
                case CLASS.BARD:
                case CLASS.SORCERER:
                case CLASS.WIZARD: return SPELLTYPE.ARCANE;
                default:
                    if (Dice.Percentile() < 71)
                        return SPELLTYPE.ARCANE;
                    return SPELLTYPE.DIVINE;
            }
        }

        private static String Spells(SPELLTYPE spellType, POWER power)
        {
            var output = String.Empty;

            switch (power)
            {
                case POWER.MINOR:
                    for (var i = Dice.Roll(1, 3); i > 0; i--)
                    {
                        if (output != String.Empty)
                            output += ", ";
                        var roll = Dice.Percentile();
                        if (roll < 51)
                            output += String.Format("{0}", Level1(spellType));
                        else if (roll < 96)
                            output += String.Format("{0}", Level2(spellType));
                        else
                            output += String.Format("{0}", Level3(spellType));
                    }
                    return output;
                case POWER.MEDIUM:
                    for (var i = Dice.d4(); i > 0; i--)
                    {
                        if (output != String.Empty)
                            output += ", ";
                        var roll = Dice.Percentile();
                        if (roll < 6)
                            output += String.Format("{0}", Level2(spellType));
                        else if (roll < 66)
                            output += String.Format("{0}", Level3(spellType));
                        else if (roll < 96)
                            output += String.Format("{0}", Level4(spellType));
                        else
                            output += String.Format("{0}", Level5(spellType));
                    }
                    return output;
                case POWER.MAJOR:
                    for (var i = Dice.d6(); i > 0; i--)
                    {
                        if (output != String.Empty)
                            output += ", ";
                        var roll = Dice.Percentile();
                        if (roll < 6)
                            output += String.Format("{0}", Level4(spellType));
                        else if (roll < 51)
                            output += String.Format("{0}", Level5(spellType));
                        else if (roll < 71)
                            output += String.Format("{0}", Level6(spellType));
                        else if (roll < 86)
                            output += String.Format("{0}", Level7(spellType));
                        else if (roll < 96)
                            output += String.Format("{0}", Level8(spellType));
                        else
                            output += String.Format("{0}", Level9(spellType));
                    }
                    return output;
                default: return String.Empty;
            }
        }

        private static String Level1(SPELLTYPE spellType)
        {
            var level = 1;
            while (true)
            {
                Application.DoEvents();
                switch (spellType)
                {
                    case SPELLTYPE.DIVINE:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5: return String.Format("bless ({0})", level);
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 10: return String.Format("calm animals ({0})", level);
                            case 11:
                            case 12:
                            case 13:
                            case 14: return String.Format("command ({0})", level);
                            case 15:
                            case 16:
                            case 17:
                            case 18:
                            case 19: return String.Format("cure light wounds ({0})", level);
                            case 20:
                            case 21:
                            case 22: return String.Format("detect chaos ({0})", level);
                            case 23:
                            case 24:
                            case 25: return String.Format("detect evil ({0})", level);
                            case 26:
                            case 27:
                            case 28: return String.Format("detect good ({0})", level);
                            case 29:
                            case 30:
                            case 31: return String.Format("detect law ({0})", level);
                            case 32:
                            case 33:
                            case 34: return String.Format("detect snares and pits ({0})", level);
                            case 35:
                            case 36:
                            case 37:
                            case 38:
                            case 39: return String.Format("doom ({0})", level);
                            case 40:
                            case 41:
                            case 42:
                            case 43:
                            case 44: return String.Format("entangle ({0})", level);
                            case 45:
                            case 46:
                            case 47:
                            case 48:
                            case 49: return String.Format("faerie fire ({0})", level);
                            case 50:
                            case 51:
                            case 52:
                            case 53:
                            case 54: return String.Format("inflict light wounds ({0})", level);
                            case 55:
                            case 56:
                            case 57:
                            case 58:
                            case 59: return String.Format("invisibility to animals ({0})", level);
                            case 60:
                            case 61:
                            case 62:
                            case 63:
                            case 64: return String.Format("invisibility to undead ({0})", level);
                            case 65:
                            case 66:
                            case 67: return String.Format("magic fang ({0})", level);
                            case 68:
                            case 69:
                            case 70: return String.Format("magic stone ({0})", level);
                            case 71:
                            case 72:
                            case 73: return String.Format("magic weapon ({0})", level);
                            case 74:
                            case 75:
                            case 76:
                            case 77: return String.Format("sanctuary ({0})", level);
                            case 78:
                            case 79:
                            case 80:
                            case 81:
                            case 82: return String.Format("shillelagh ({0})", level);
                            case 83:
                            case 84:
                            case 85:
                            case 86: return String.Format("summon monster I ({0})", level);
                            case 87:
                            case 88:
                            case 89:
                            case 90: return String.Format("summon nature's ally I ({0})", level);
                            default: level++; break;
                        } break;
                    default:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5: return String.Format("burning hands ({0})", level);
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 10: return String.Format("change self ({0})", level);
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15: return String.Format("charm person ({0})", level);
                            case 16:
                            case 17:
                            case 18: return String.Format("color spray ({0})", level);
                            case 19:
                            case 20:
                            case 21:
                            case 22: return String.Format("detect secret doors ({0})", level);
                            case 23:
                            case 24:
                            case 25: return String.Format("detect undead ({0})", level);
                            case 26:
                            case 27:
                            case 28: return String.Format("enlarge ({0})", level);
                            case 29:
                            case 30:
                            case 31: return String.Format("erase ({0})", level);
                            case 32:
                            case 33:
                            case 34:
                            case 35:
                            case 36: return String.Format("feather fall ({0})", level);
                            case 37:
                            case 38:
                            case 39: return String.Format("grease ({0})", level);
                            case 40:
                            case 41:
                            case 42:
                            case 43:
                            case 44: return String.Format("identify ({0})", level);
                            case 45:
                            case 46:
                            case 47: return String.Format("jump ({0})", level);
                            case 48:
                            case 49:
                            case 50:
                            case 51: return String.Format("mage armor ({0})", level);
                            case 52:
                            case 53:
                            case 54: return String.Format("magic weapon ({0})", level);
                            case 55:
                            case 56:
                            case 57: return String.Format("mount ({0})", level);
                            case 58:
                            case 59:
                            case 60: return String.Format("ray of enfeeblement ({0})", level);
                            case 61:
                            case 62:
                            case 63: return String.Format("reduce ({0})", level);
                            case 64:
                            case 65:
                            case 66: return String.Format("shield ({0})", level);
                            case 67:
                            case 68:
                            case 69: return String.Format("shocking grasp ({0})", level);
                            case 70:
                            case 71:
                            case 72:
                            case 73: return String.Format("silent image ({0})", level);
                            case 74:
                            case 75:
                            case 76:
                            case 77:
                            case 78: return String.Format("sleep ({0})", level);
                            case 79:
                            case 80:
                            case 81: return String.Format("spider climb ({0})", level);
                            case 82:
                            case 83:
                            case 84: return String.Format("summon monster I ({0})", level);
                            case 85:
                            case 86:
                            case 87: return String.Format("floating disk ({0})", level);
                            case 88:
                            case 89:
                            case 90:
                            case 91:
                            case 92: return String.Format("unseen servant ({0})", level);
                            case 93:
                            case 94:
                            case 95: return String.Format("ventriloquism ({0})", level);
                            default: level++; break;
                        } break;
                }
            }
        }

        private static String Level2(SPELLTYPE spellType)
        {
            var level = 3;

            while (true)
            {
                Application.DoEvents();
                switch (spellType)
                {
                    case SPELLTYPE.DIVINE:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5: return String.Format("aid ({0})", level);
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 10: return String.Format("augury ({0})", level);
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15: return String.Format("barkskin ({0})", level);
                            case 16:
                            case 17:
                            case 18:
                            case 19:
                            case 20: return String.Format("bull's strength ({0})", level);
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                            case 25: return String.Format("charm person or animal ({0})", level);
                            case 26:
                            case 27:
                            case 28: return String.Format("chill metal ({0})", level);
                            case 29:
                            case 30:
                            case 31: return String.Format("cure moderate wounds ({0})", level);
                            case 32:
                            case 33:
                            case 34:
                            case 35:
                            case 36: return String.Format("delay poison ({0})", level);
                            case 37:
                            case 38:
                            case 39: return String.Format("flame blade ({0})", level);
                            case 40:
                            case 41:
                            case 42: return String.Format("flaming sphere ({0})", level);
                            case 43:
                            case 44:
                            case 45:
                            case 46:
                            case 47: return String.Format("heat metal ({0})", level);
                            case 48:
                            case 49:
                            case 50: return String.Format("hold animal ({0})", level);
                            case 51:
                            case 52:
                            case 53:
                            case 54:
                            case 55: return String.Format("hold person ({0})", level);
                            case 56:
                            case 57:
                            case 58: return String.Format("inflict moderate wounds ({0})", level);
                            case 59:
                            case 60:
                            case 61:
                            case 62:
                            case 63: return String.Format("lesser restoration ({0})", level);
                            case 64:
                            case 65:
                            case 66:
                            case 67: return String.Format("silence ({0})", level);
                            case 68:
                            case 69:
                            case 70: return String.Format("speak with animals ({0})", level);
                            case 71:
                            case 72:
                            case 73:
                            case 74:
                            case 75: return String.Format("spiritual weapon ({0})", level);
                            case 76:
                            case 77:
                            case 78:
                            case 79: return String.Format("summon monster II ({0})", level);
                            case 80:
                            case 81:
                            case 82:
                            case 83: return String.Format("summon nature's ally II ({0})", level);
                            case 84:
                            case 85: return String.Format("summon swarm ({0})", level);
                            case 86:
                            case 87:
                            case 88:
                            case 89:
                            case 90: return String.Format("undetectable alignment ({0})", level);
                            default: level++; break;
                        } break;
                    default:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                            case 3: return String.Format("arcane lock ({0})", level);
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8: return String.Format("blindness/deafness ({0})", level);
                            case 9:
                            case 10:
                            case 11:
                            case 12:
                            case 13: return String.Format("blur ({0})", level);
                            case 14:
                            case 15:
                            case 16:
                            case 17:
                            case 18: return String.Format("bull's strength ({0})", level);
                            case 19:
                            case 20:
                            case 21:
                            case 22: return String.Format("cat's grace ({0})", level);
                            case 23:
                            case 24:
                            case 25: return String.Format("darkvision ({0})", level);
                            case 26:
                            case 27:
                            case 28:
                            case 29:
                            case 30: return String.Format("detect thoughts ({0})", level);
                            case 31:
                            case 32:
                            case 33: return String.Format("flaming sphere ({0})", level);
                            case 34:
                            case 35:
                            case 36:
                            case 37:
                            case 38: return String.Format("invisibility ({0})", level);
                            case 39:
                            case 40:
                            case 41: return String.Format("knock ({0})", level);
                            case 42:
                            case 43:
                            case 44:
                            case 45:
                            case 46: return String.Format("levitate ({0})", level);
                            case 47:
                            case 48:
                            case 49:
                            case 50:
                            case 51: return String.Format("locate object ({0})", level);
                            case 52:
                            case 53:
                            case 54: return String.Format("acid arrow ({0})", level);
                            case 55:
                            case 56:
                            case 57:
                            case 58:
                            case 59: return String.Format("minor image ({0})", level);
                            case 60:
                            case 61:
                            case 62:
                            case 63:
                            case 64: return String.Format("mirror image ({0})", level);
                            case 65:
                            case 66:
                            case 67:
                            case 68:
                            case 69: return String.Format("misdirection ({0})", level);
                            case 70:
                            case 71:
                            case 72: return String.Format("protection from arrows ({0})", level);
                            case 73:
                            case 74:
                            case 75:
                            case 76:
                            case 77: return String.Format("see invisibility ({0})", level);
                            case 78:
                            case 79:
                            case 80: return String.Format("spectral hand ({0})", level);
                            case 81:
                            case 82:
                            case 83: return String.Format("stinking cloud ({0})", level);
                            case 84:
                            case 85:
                            case 86:
                            case 87: return String.Format("summon monster II ({0})", level);
                            case 88:
                            case 89:
                            case 90:
                            case 91:
                            case 92: return String.Format("summon swarm ({0})", level);
                            case 93:
                            case 94:
                            case 95:
                            case 96: return String.Format("web ({0})", level);
                            default: level++; break;
                        } break;
                }
            }
        }

        private static String Level3(SPELLTYPE spellType)
        {
            var level = 5;

            while (true)
            {
                Application.DoEvents();
                switch (spellType)
                {
                    case SPELLTYPE.DIVINE:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2: return String.Format("call lightning ({0})", level);
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9: return String.Format("cure serious wounds ({0})", level);
                            case 10:
                            case 11:
                            case 12:
                            case 13: return String.Format("dispel magic ({0})", level);
                            case 14:
                            case 15: return String.Format("dominate animal ({0})", level);
                            case 16:
                            case 17: return String.Format("greater magic fang ({0})", level);
                            case 18:
                            case 19: return String.Format("inflict serious wounds ({0})", level);
                            case 20:
                            case 21:
                            case 22: return String.Format("invisibility purge ({0})", level);
                            case 23:
                            case 24:
                            case 25:
                            case 26: return String.Format("locate object ({0})", level);
                            case 27:
                            case 28: return String.Format("magic circle against chaos ({0})", level);
                            case 29:
                            case 30: return String.Format("magic circle against evil ({0})", level);
                            case 31:
                            case 32: return String.Format("magic circle against good ({0})", level);
                            case 33:
                            case 34: return String.Format("magic circle against law ({0})", level);
                            case 35:
                            case 36:
                            case 37:
                            case 38: return String.Format("negative energy protection ({0})", level);
                            case 39:
                            case 40:
                            case 41: return String.Format("neutralize poison ({0})", level);
                            case 42:
                            case 43: return String.Format("plant growth ({0})", level);
                            case 44:
                            case 45:
                            case 46: return String.Format("prayer ({0})", level);
                            case 47:
                            case 48:
                            case 49:
                            case 50:
                            case 51: return String.Format("protection from elements ({0})", level);
                            case 52:
                            case 53: return String.Format("remove blindness/deafness ({0})", level);
                            case 54:
                            case 55:
                            case 56: return String.Format("remove curse ({0})", level);
                            case 57:
                            case 58:
                            case 59: return String.Format("remove disease ({0})", level);
                            case 60:
                            case 61:
                            case 62: return String.Format("searing light ({0})", level);
                            case 63:
                            case 64:
                            case 65: return String.Format("speak with dead ({0})", level);
                            case 66:
                            case 67: return String.Format("spike growth ({0})", level);
                            case 68:
                            case 69:
                            case 70:
                            case 71:
                            case 72: return String.Format("stone shape ({0})", level);
                            case 73:
                            case 74:
                            case 75: return String.Format("summon monster III ({0})", level);
                            case 76:
                            case 77:
                            case 78: return String.Format("summon nature's ally III ({0})", level);
                            case 79:
                            case 80: return String.Format("water breathing ({0})", level);
                            case 81:
                            case 82:
                            case 83:
                            case 84:
                            case 85:
                            case 86:
                            case 87:
                            case 88:
                            case 89:
                            case 90: return String.Format("water walk ({0})", level);
                            default: level++; break;
                        } break;
                    default:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5: return String.Format("blink ({0})", level);
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 10: return String.Format("clairaudience/clairvoyance ({0})", level);
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15: return String.Format("dispel magic ({0})", level);
                            case 16:
                            case 17:
                            case 18:
                            case 19:
                            case 20: return String.Format("displacement ({0})", level);
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                            case 25: return String.Format("fireball ({0})", level);
                            case 26:
                            case 27:
                            case 28: return String.Format("flame arrow ({0})", level);
                            case 29:
                            case 30:
                            case 31: return String.Format("fly ({0})", level);
                            case 32:
                            case 33: return String.Format("gaseous form ({0})", level);
                            case 34:
                            case 35:
                            case 36: return String.Format("greater magic weapon ({0})", level);
                            case 37:
                            case 38:
                            case 39: return String.Format("halt undead ({0})", level);
                            case 40:
                            case 41:
                            case 42: return String.Format("haste ({0})", level);
                            case 43:
                            case 44:
                            case 45: return String.Format("hold person ({0})", level);
                            case 46:
                            case 47: return String.Format("invisibility sphere ({0})", level);
                            case 48:
                            case 49:
                            case 50:
                            case 51:
                            case 52:
                            case 53: return String.Format("lightning bolt ({0})", level);
                            case 54: return String.Format("magic circle against chaos ({0})", level);
                            case 55: return String.Format("magic circle against evil ({0})", level);
                            case 56: return String.Format("magic circle against good ({0})", level);
                            case 57: return String.Format("magic circle against law ({0})", level);
                            case 58:
                            case 59:
                            case 60: return String.Format("nondetection ({0})", level);
                            case 61:
                            case 62:
                            case 63:
                            case 64:
                            case 65: return String.Format("slow ({0})", level);
                            case 66:
                            case 67:
                            case 68:
                            case 69:
                            case 70: return String.Format("spectral hand ({0})", level);
                            case 71:
                            case 72:
                            case 73:
                            case 74:
                            case 75: return String.Format("suggestion ({0})", level);
                            case 76:
                            case 77:
                            case 78:
                            case 79: return String.Format("summon monster III ({0})", level);
                            case 80:
                            case 81:
                            case 82:
                            case 83:
                            case 84: return String.Format("tongues ({0})", level);
                            case 85:
                            case 86:
                            case 87: return String.Format("vampiric touch ({0})", level);
                            case 88:
                            case 89:
                            case 90: return String.Format("water breathing ({0})", level);
                            default: level++; break;
                        } break;
                }
            }
        }

        private static String Level4(SPELLTYPE spellType)
        {
            var level = 7;

            while (true)
            {
                Application.DoEvents();
                switch (spellType)
                {
                    case SPELLTYPE.DIVINE:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2: return String.Format("antiplant shell ({0})", level);
                            case 3:
                            case 4:
                            case 5: return String.Format("control water ({0})", level);
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 10:
                            case 11:
                            case 12: return String.Format("cure critical wounds ({0})", level);
                            case 13:
                            case 14:
                            case 15:
                            case 16:
                            case 17:
                            case 18:
                            case 19: return String.Format("discern lies ({0})", level);
                            case 20:
                            case 21:
                            case 22:
                            case 23:
                            case 24: return String.Format("dispel magic ({0})", level);
                            case 25:
                            case 26:
                            case 27: return String.Format("divine power ({0})", level);
                            case 28:
                            case 29:
                            case 30:
                            case 31:
                            case 32:
                            case 33:
                            case 34: return String.Format("flame strike ({0})", level);
                            case 35:
                            case 36:
                            case 37:
                            case 38:
                            case 39:
                            case 40:
                            case 41: return String.Format("freedom of movement ({0})", level);
                            case 42:
                            case 43:
                            case 44:
                            case 45:
                            case 46:
                            case 47: return String.Format("giant vermin ({0})", level);
                            case 48:
                            case 49:
                            case 50: return String.Format("greater magic weapon ({0})", level);
                            case 51:
                            case 52:
                            case 53: return String.Format("inflict critical wounds ({0})", level);
                            case 54:
                            case 55: return String.Format("lesser planar ally ({0})", level);
                            case 56: 
                            case 57:
                            case 58:
                            case 59: 
                            case 60:
                            case 61:
                            case 62: return String.Format("neutralize poison ({0})", level);
                            case 63:
                            case 64:
                            case 65:
                            case 66: return String.Format("quench ({0})", level);
                            case 67:
                            case 68: return String.Format("restoration ({0})", level);
                            case 69:
                            case 70:
                            case 71: return String.Format("rusting grasp ({0})", level);
                            case 72: 
                            case 73:
                            case 74: return String.Format("spell immunity ({0})", level);
                            case 75:
                            case 76: return String.Format("spike stones ({0})", level);
                            case 77:
                            case 78: 
                            case 79:
                            case 80: return String.Format("summon monster IV ({0})", level);
                            case 81:
                            case 82: return String.Format("summon nature's ally IV ({0})", level);
                            case 83:
                            case 84:
                            case 85:
                            case 86:
                            case 87:
                            case 88:
                            case 89:
                            case 90: return String.Format("tongues ({0})", level);
                            default: level++; break;
                        } break;
                    default:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5: return String.Format("charm monster ({0})", level);
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 10: return String.Format("confusion ({0})", level);
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15: return String.Format("contagion ({0})", level);
                            case 16:
                            case 17:
                            case 18:
                            case 19:
                            case 20: return String.Format("detect scrying ({0})", level);
                            case 21:
                            case 22:
                            case 23: return String.Format("dimensional anchor ({0})", level);
                            case 24:
                            case 25:
                            case 26:
                            case 27:
                            case 28: return String.Format("dimension door ({0})", level);
                            case 29:
                            case 30:
                            case 31: 
                            case 32:
                            case 33: return String.Format("emotion ({0})", level);
                            case 34:
                            case 35:
                            case 36: return String.Format("enervation ({0})", level);
                            case 37:
                            case 38:
                            case 39: return String.Format("Evard's black tentacles ({0})", level);
                            case 40:
                            case 41:
                            case 42: 
                            case 43:
                            case 44: return String.Format("fear ({0})", level);
                            case 45: 
                            case 46:
                            case 47: return String.Format("fire shield ({0})", level);
                            case 48:
                            case 49:
                            case 50: return String.Format("ice storm ({0})", level);
                            case 51:
                            case 52:
                            case 53: 
                            case 54:
                            case 55: return String.Format("improved invisibility ({0})", level);
                            case 56: 
                            case 57:
                            case 58: return String.Format("lesser geas ({0})", level);
                            case 59:
                            case 60:
                            case 61: return String.Format("minor globe of invulnerability ({0})", level);
                            case 62:
                            case 63:
                            case 64:
                            case 65: 
                            case 66:
                            case 67: return String.Format("phantasmal killer ({0})", level);
                            case 68:
                            case 69:
                            case 70: return String.Format("polymorph other ({0})", level);
                            case 71:
                            case 72:
                            case 73: return String.Format("polymorph self ({0})", level);
                            case 74:
                            case 75:
                            case 76: return String.Format("remove curse ({0})", level);
                            case 77:
                            case 78:
                            case 79: return String.Format("shadow conjuration ({0})", level);
                            case 80:
                            case 81:
                            case 82: return String.Format("stoneskin ({0})", level);
                            case 83:
                            case 84: return String.Format("summon monster IV ({0})", level);
                            case 85:
                            case 86:
                            case 87: return String.Format("wall of fire ({0})", level);
                            case 88:
                            case 89:
                            case 90: return String.Format("wall of ice ({0})", level);
                            default: level++; break;
                        } break;
                }
            }
        }


        private static String Level5(SPELLTYPE spellType)
        {
            var level = 9;

            while (true)
            {
                Application.DoEvents();
                switch (spellType)
                {
                    case SPELLTYPE.DIVINE:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2: 
                            case 3:
                            case 4:
                            case 5: 
                            case 6:
                            case 7: return String.Format("break enchantment ({0})", level);
                            case 8:
                            case 9:
                            case 10:
                            case 11:
                            case 12:
                            case 13: return String.Format("commune ({0})", level);
                            case 14:
                            case 15: return String.Format("control winds ({0})", level);
                            case 16:
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                            case 21:
                            case 22: return String.Format("cure critical wounds ({0})", level);
                            case 23:
                            case 24: 
                            case 25:
                            case 26: return String.Format("dispel evil ({0})", level);
                            case 27: 
                            case 28:
                            case 29: return String.Format("dispel good ({0})", level);
                            case 30:
                            case 31:
                            case 32:
                            case 33:
                            case 34:
                            case 35: return String.Format("flame strike ({0})", level);
                            case 36:
                            case 37:
                            case 38: return String.Format("greater command ({0})", level);
                            case 39:
                            case 40: return String.Format("hallow ({0})", level);
                            case 41: 
                            case 42:
                            case 43: return String.Format("healing circle ({0})", level);
                            case 44:
                            case 45: return String.Format("ice storm ({0})", level);
                            case 46:
                            case 47: 
                            case 48:
                            case 49:
                            case 50: return String.Format("insect plague ({0})", level);
                            case 51:
                            case 52:
                            case 53: 
                            case 54:
                            case 55: 
                            case 56:
                            case 57: return String.Format("raise dead ({0})", level);
                            case 58:
                            case 59:
                            case 60: return String.Format("righteous might ({0})", level);
                            case 61:
                            case 62:
                            case 63: return String.Format("slay living ({0})", level);
                            case 64:
                            case 65: return String.Format("spell resistance ({0})", level);
                            case 66:
                            case 67: return String.Format("summon monster V ({0})", level);
                            case 68:
                            case 69: return String.Format("summon nature's ally V ({0})", level);
                            case 70:
                            case 71:
                            case 72: return String.Format("transmute rock to mud ({0})", level);
                            case 73:
                            case 74: return String.Format("true seeing ({0})", level);
                            case 75: return String.Format("unhallow ({0})", level);
                            case 76: 
                            case 77:
                            case 78: return String.Format("wall of fire ({0})", level);
                            case 79:
                            case 80: return String.Format("wall of stone ({0})", level);
                            case 81:
                            case 82: 
                            case 83:
                            case 84:
                            case 85:
                            case 86:
                            case 87:
                            case 88:
                            case 89:
                            case 90: return String.Format("wall of thorns ({0})", level);
                            default: level++; break;
                        } break;
                    default:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4: return String.Format("interposing hand ({0})", level);
                            case 5: 
                            case 6:
                            case 7:
                            case 8: return String.Format("cloudkill ({0})", level);
                            case 9:
                            case 10: 
                            case 11:
                            case 12:
                            case 13: return String.Format("cone of cold ({0})", level);
                            case 14:
                            case 15: 
                            case 16:
                            case 17: return String.Format("dismissal ({0})", level);
                            case 18:
                            case 19:
                            case 20:
                            case 21: return String.Format("domination ({0})", level);
                            case 22:
                            case 23:
                            case 24: return String.Format("feeblemind ({0})", level);
                            case 25:
                            case 26:
                            case 27: return String.Format("greater shadow conjuration ({0})", level);
                            case 28: 
                            case 29:
                            case 30:
                            case 31: return String.Format("hold monster ({0})", level);
                            case 32:
                            case 33:
                            case 34:
                            case 35: return String.Format("major creation ({0})", level);
                            case 36: 
                            case 37:
                            case 38:
                            case 39:
                            case 40: return String.Format("mind fog ({0})", level);
                            case 41:
                            case 42:
                            case 43:
                            case 44: return String.Format("passwall ({0})", level);
                            case 45:
                            case 46:
                            case 47: 
                            case 48:
                            case 49: return String.Format("persistent image ({0})", level);
                            case 50: 
                            case 51:
                            case 52:
                            case 53: return String.Format("shadow evocation ({0})", level);
                            case 54:
                            case 55:
                            case 56: return String.Format("stone shape ({0})", level);
                            case 57:
                            case 58: 
                            case 59:
                            case 60: return String.Format("summon monster V ({0})", level);
                            case 61: 
                            case 62:
                            case 63:
                            case 64: return String.Format("telekinesis ({0})", level);
                            case 65:
                            case 66:
                            case 67: 
                            case 68:
                            case 69: return String.Format("teleport ({0})", level);
                            case 70: 
                            case 71:
                            case 72:
                            case 73: return String.Format("transmute mud to rock ({0})", level);
                            case 74:
                            case 75:
                            case 76:
                            case 77: return String.Format("transmute rock to mud ({0})", level);
                            case 78:
                            case 79: 
                            case 80:
                            case 81: return String.Format("wall of force ({0})", level);
                            case 82: 
                            case 83:
                            case 84:
                            case 85:
                            case 86: return String.Format("wall of iron ({0})", level);
                            case 87: 
                            case 88:
                            case 89:
                            case 90: return String.Format("wall of stone ({0})", level);
                            default: level++; break;
                        } break;
                }
            }
        }

        private static String Level6(SPELLTYPE spellType)
        {
            var level = 11;

            while (true)
            {
                Application.DoEvents();
                switch (spellType)
                {
                    case SPELLTYPE.DIVINE:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8: return String.Format("antilife shell ({0})", level);
                            case 9:
                            case 10:
                            case 11:
                            case 12:
                            case 13:
                            case 14: return String.Format("blade barrier ({0})", level);
                            case 15: 
                            case 16:
                            case 17:
                            case 18:
                            case 19: return String.Format("find the path ({0})", level);
                            case 20:
                            case 21:
                            case 22:
                            case 23: return String.Format("fire seeds ({0})", level);
                            case 24:
                            case 25:
                            case 26: 
                            case 27:
                            case 28: return String.Format("Geas/Quest ({0})", level);
                            case 29: 
                            case 30:
                            case 31:
                            case 32:
                            case 33:
                            case 34: return String.Format("harm ({0})", level);
                            case 35: 
                            case 36:
                            case 37:
                            case 38: 
                            case 39:
                            case 40:
                            case 41: return String.Format("heal ({0})", level);
                            case 42:
                            case 43: 
                            case 44:
                            case 45:
                            case 46:
                            case 47: return String.Format("heroes' feast ({0})", level);
                            case 48:
                            case 49:
                            case 50:
                            case 51:
                            case 52:
                            case 53:
                            case 54:
                            case 55: return String.Format("planar ally ({0})", level);
                            case 56:
                            case 57: return String.Format("repel wood ({0})", level);
                            case 58:
                            case 59:
                            case 60: return String.Format("stone tell ({0})", level);
                            case 61:
                            case 62:
                            case 63: 
                            case 64:
                            case 65: 
                            case 66:
                            case 67:
                            case 68: return String.Format("summon monster VI ({0})", level);
                            case 69:
                            case 70:
                            case 71: return String.Format("transport via plants ({0})", level);
                            case 72: 
                            case 73:
                            case 74: 
                            case 75: 
                            case 76:
                            case 77: return String.Format("wall of stone ({0})", level);
                            case 78: 
                            case 79:
                            case 80: return String.Format("wind walk ({0})", level);
                            case 81:
                            case 82:
                            case 83:
                            case 84:
                            case 85:
                            case 86:
                            case 87:
                            case 88:
                            case 89:
                            case 90: return String.Format("word of recall ({0})", level);
                            default: level++; break;
                        } break;
                    default:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4: return String.Format("acid fog ({0})", level);
                            case 5:
                            case 6:
                            case 7: return String.Format("analyze dweomer ({0})", level);
                            case 8: 
                            case 9:
                            case 10:
                            case 11: return String.Format("antimagic field ({0})", level);
                            case 12:
                            case 13: 
                            case 14:
                            case 15: return String.Format("forceful hand ({0})", level);
                            case 16:
                            case 17: 
                            case 18:
                            case 19: return String.Format("chain lightning ({0})", level);
                            case 20:
                            case 21: 
                            case 22:
                            case 23: return String.Format("circle of death ({0})", level);
                            case 24: 
                            case 25:
                            case 26: return String.Format("control water ({0})", level);
                            case 27: 
                            case 28:
                            case 29:
                            case 30: return String.Format("disintegrate ({0})", level);
                            case 31: 
                            case 32:
                            case 33: return String.Format("eyebite ({0})", level);
                            case 34:
                            case 35:
                            case 36:
                            case 37: return String.Format("flesh to stone ({0})", level);
                            case 38:
                            case 39:
                            case 40:
                            case 41: return String.Format("globe of invulnerability ({0})", level);
                            case 42:
                            case 43:
                            case 44:
                            case 45: return String.Format("greater shadow evocation ({0})", level);
                            case 46:
                            case 47:
                            case 48:
                            case 49: return String.Format("mass suggestion ({0})", level);
                            case 50:
                            case 51:
                            case 52: return String.Format("mislead ({0})", level);
                            case 53: 
                            case 54:
                            case 55:
                            case 56:
                            case 57: return String.Format("move earth ({0})", level);
                            case 58:
                            case 59:
                            case 60:
                            case 61: return String.Format("freezing sphere ({0})", level);
                            case 62:
                            case 63:
                            case 64:
                            case 65: return String.Format("programmed image ({0})", level);
                            case 66:
                            case 67:
                            case 68:
                            case 69:
                            case 70: return String.Format("project image ({0})", level);
                            case 71:
                            case 72:
                            case 73: 
                            case 74:
                            case 75: return String.Format("repulsion ({0})", level);
                            case 76:
                            case 77:
                            case 78: return String.Format("shades ({0})", level);
                            case 79:
                            case 80:
                            case 81:
                            case 82: return String.Format("stone to flesh ({0})", level);
                            case 83:
                            case 84:
                            case 85:
                            case 86: return String.Format("summon monster VI ({0})", level);
                            case 87:
                            case 88:
                            case 89:
                            case 90: return String.Format("true seeing ({0})", level);
                            default: level++; break;
                        } break;
                }
            }
        }

        private static String Level7(SPELLTYPE spellType)
        {
            var level = 13;

            while (true)
            {
                Application.DoEvents();
                switch (spellType)
                {
                    case SPELLTYPE.DIVINE:
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
                            case 11: return String.Format("control weather ({0})", level);
                            case 12:
                            case 13:
                            case 14: 
                            case 15:
                            case 16:
                            case 17:
                            case 18: return String.Format("creeping doom ({0})", level);
                            case 19: 
                            case 20:
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                            case 25: return String.Format("destruction ({0})", level);
                            case 26:
                            case 27:
                            case 28: 
                            case 29:
                            case 30:
                            case 31:
                            case 32: return String.Format("dictum ({0})", level);
                            case 33:
                            case 34: 
                            case 35:
                            case 36: return String.Format("fire storm ({0})", level);
                            case 37:
                            case 38:
                            case 39:
                            case 40: return String.Format("greater restoration ({0})", level);
                            case 41: 
                            case 42:
                            case 43:
                            case 44:
                            case 45:
                            case 46:
                            case 47: return String.Format("holy word ({0})", level);
                            case 48:
                            case 49:
                            case 50:
                            case 51:
                            case 52:
                            case 53:
                            case 54: return String.Format("regenerate ({0})", level);
                            case 55: 
                            case 56:
                            case 57: 
                            case 58:
                            case 59:
                            case 60:
                            case 61: return String.Format("repulsion ({0})", level);
                            case 62:
                            case 63:
                            case 64:
                            case 65:
                            case 66:
                            case 67:
                            case 68: return String.Format("resurrection ({0})", level);
                            case 69:
                            case 70:
                            case 71:
                            case 72: return String.Format("summon monster VII ({0})", level);
                            case 73:
                            case 74:
                            case 75:
                            case 76: return String.Format("transmute metal to wood ({0})", level);
                            case 77: 
                            case 78:
                            case 79:
                            case 80: return String.Format("true seeing ({0})", level);
                            case 81:
                            case 82:
                            case 83:
                            case 84:
                            case 85:
                            case 86:
                            case 87:
                            case 88:
                            case 89:
                            case 90: return String.Format("word of chaos ({0})", level);
                            default: level++; break;
                        } break;
                    default:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5: return String.Format("grasping hand ({0})", level);
                            case 6:
                            case 7: 
                            case 8:
                            case 9:
                            case 10: return String.Format("control undead ({0})", level);
                            case 11: 
                            case 12:
                            case 13:
                            case 14:
                            case 15: return String.Format("forceful hand ({0})", level);
                            case 16:
                            case 17:
                            case 18:
                            case 19: return String.Format("chain lightning ({0})", level);
                            case 20:
                            case 21:
                            case 22:
                            case 23: return String.Format("circle of death ({0})", level);
                            case 24:
                            case 25:
                            case 26: return String.Format("control water ({0})", level);
                            case 27:
                            case 28:
                            case 29:
                            case 30: return String.Format("disintegrate ({0})", level);
                            case 31:
                            case 32:
                            case 33: return String.Format("eyebite ({0})", level);
                            case 34:
                            case 35:
                            case 36:
                            case 37: return String.Format("flesh to stone ({0})", level);
                            case 38:
                            case 39:
                            case 40:
                            case 41: return String.Format("globe of invulnerability ({0})", level);
                            case 42:
                            case 43:
                            case 44:
                            case 45: return String.Format("greater shadow evocation ({0})", level);
                            case 46:
                            case 47:
                            case 48:
                            case 49: return String.Format("mass suggestion ({0})", level);
                            case 50:
                            case 51:
                            case 52: return String.Format("mislead ({0})", level);
                            case 53:
                            case 54:
                            case 55:
                            case 56:
                            case 57: return String.Format("move earth ({0})", level);
                            case 58:
                            case 59:
                            case 60:
                            case 61: return String.Format("freezing sphere ({0})", level);
                            case 62:
                            case 63:
                            case 64:
                            case 65: return String.Format("programmed image ({0})", level);
                            case 66:
                            case 67:
                            case 68:
                            case 69:
                            case 70: return String.Format("project image ({0})", level);
                            case 71:
                            case 72:
                            case 73:
                            case 74:
                            case 75: return String.Format("repulsion ({0})", level);
                            case 76:
                            case 77:
                            case 78: return String.Format("shades ({0})", level);
                            case 79:
                            case 80:
                            case 81:
                            case 82: return String.Format("stone to flesh ({0})", level);
                            case 83:
                            case 84:
                            case 85:
                            case 86: return String.Format("summon monster VI ({0})", level);
                            case 87:
                            case 88:
                            case 89:
                            case 90: return String.Format("true seeing ({0})", level);
                            default: level++; break;
                        } break;
                }
            }
        }

        private static String Level8(SPELLTYPE spellType)
        {
            var level = 15;

            while (true)
            {
                Application.DoEvents();
                switch (spellType)
                {
                    case SPELLTYPE.DIVINE:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6: return String.Format("antimagic field ({0})", level);
                            case 7:
                            case 8: 
                            case 9:
                            case 10:
                            case 11:
                            case 12: return String.Format("creeping doom ({0})", level);
                            case 13:
                            case 14:
                            case 15:
                            case 16:
                            case 17:
                            case 18: return String.Format("discern location ({0})", level);
                            case 19:
                            case 20:
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                            case 25: return String.Format("earthquake ({0})", level);
                            case 26:
                            case 27:
                            case 28:
                            case 29:
                            case 30: return String.Format("finger of death ({0})", level);
                            case 31:
                            case 32: 
                            case 33:
                            case 34:
                            case 35: return String.Format("fire storm ({0})", level);
                            case 36:
                            case 37:
                            case 38:
                            case 39:
                            case 40: 
                            case 41:
                            case 42:
                            case 43:
                            case 44: return String.Format("holy aura ({0})", level);
                            case 45:
                            case 46:
                            case 47: 
                            case 48:
                            case 49:
                            case 50: return String.Format("mass heal ({0})", level);
                            case 51:
                            case 52:
                            case 53:
                            case 54:
                            case 55:
                            case 56: return String.Format("repel metal or stone ({0})", level);
                            case 57:
                            case 58:
                            case 59:
                            case 60:
                            case 61:
                            case 62: return String.Format("reverse gravity ({0})", level);
                            case 63:
                            case 64:
                            case 65:
                            case 66:
                            case 67:
                            case 68: return String.Format("summon monster VIII ({0})", level);
                            case 69:
                            case 70:
                            case 71:
                            case 72: 
                            case 73:
                            case 74: return String.Format("sunburst ({0})", level);
                            case 75:
                            case 76: 
                            case 77:
                            case 78:
                            case 79:
                            case 80: return String.Format("unholy aura ({0})", level);
                            case 81:
                            case 82:
                            case 83:
                            case 84:
                            case 85:
                            case 86:
                            case 87:
                            case 88:
                            case 89:
                            case 90: return String.Format("whirlwind ({0})", level);
                            default: level++; break;
                        } break;
                    default:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                            case 3: return String.Format("antipathy ({0})", level);
                            case 4:
                            case 5: 
                            case 6:
                            case 7:
                            case 8: return String.Format("clenched fist ({0})", level);
                            case 9:
                            case 10: 
                            case 11:
                            case 12:
                            case 13: return String.Format("clone ({0})", level);
                            case 14:
                            case 15: 
                            case 16:
                            case 17:
                            case 18: return String.Format("demand ({0})", level);
                            case 19: 
                            case 20:
                            case 21:
                            case 22:
                            case 23: return String.Format("horrid wilting ({0})", level);
                            case 24:
                            case 25:
                            case 26:
                            case 27:
                            case 28: return String.Format("incendiary cloud ({0})", level);
                            case 29:
                            case 30: 
                            case 31:
                            case 32:
                            case 33: return String.Format("mass charm ({0})", level);
                            case 34:
                            case 35:
                            case 36:
                            case 37:
                            case 38: return String.Format("maze ({0})", level);
                            case 39:
                            case 40:
                            case 41: 
                            case 42:
                            case 43: return String.Format("mind blank ({0})", level);
                            case 44:
                            case 45:
                            case 46:
                            case 47:
                            case 48: return String.Format("telekinetic sphere ({0})", level);
                            case 49: 
                            case 50:
                            case 51:
                            case 52:
                            case 53: return String.Format("irresistable dance ({0})", level);
                            case 54:
                            case 55:
                            case 56:
                            case 57:
                            case 58: return String.Format("polymorph any object ({0})", level);
                            case 59:
                            case 60:
                            case 61: 
                            case 62:
                            case 63: return String.Format("power word, blind ({0})", level);
                            case 64:
                            case 65: 
                            case 66:
                            case 67:
                            case 68: return String.Format("prismatic wall ({0})", level);
                            case 69:
                            case 70: 
                            case 71:
                            case 72:
                            case 73: return String.Format("protection from spells ({0})", level);
                            case 74:
                            case 75: 
                            case 76:
                            case 77:
                            case 78: return String.Format("screen ({0})", level);
                            case 79:
                            case 80:
                            case 81:
                            case 82:
                            case 83: return String.Format("summon monster VII ({0})", level);
                            case 84:
                            case 85:
                            case 86: 
                            case 87:
                            case 88: return String.Format("sunburst ({0})", level);
                            case 89:
                            case 90: return String.Format("sympathy ({0})", level);
                            default: level++; break;
                        } break;
                }
            }
        }

        private static String Level9(SPELLTYPE spellType)
        {
            var level = 17;

            while (true)
            {
                Application.DoEvents();
                switch (spellType)
                {
                    case SPELLTYPE.DIVINE:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4: return String.Format("antipathy ({0})", level);
                            case 5:
                            case 6:
                            case 7: return String.Format("astral projection ({0})", level);
                            case 8:
                            case 9:
                            case 10:
                            case 11:
                            case 12:
                            case 13: return String.Format("elemental swarm ({0})", level);
                            case 14:
                            case 15:
                            case 16:
                            case 17:
                            case 18:
                            case 19: return String.Format("energy drain ({0})", level);
                            case 20:
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                            case 25: return String.Format("etherealness ({0})", level);
                            case 26:
                            case 27:
                            case 28:
                            case 29:
                            case 30:
                            case 31: return String.Format("foresight ({0})", level);
                            case 32:
                            case 33:
                            case 34:
                            case 35: 
                            case 36:
                            case 37: return String.Format("gate ({0})", level);
                            case 38:
                            case 39:
                            case 40:
                            case 41:
                            case 42:
                            case 43:
                            case 44: 
                            case 45:
                            case 46: return String.Format("mass heal ({0})", level);
                            case 47:
                            case 48:
                            case 49:
                            case 50: 
                            case 51:
                            case 52:
                            case 53: return String.Format("implosion ({0})", level);
                            case 54:
                            case 55: return String.Format("miracle ({0})", level);
                            case 56: 
                            case 57:
                            case 58:
                            case 59:
                            case 60:
                            case 61: return String.Format("regenerate ({0})", level);
                            case 62: 
                            case 63:
                            case 64:
                            case 65:
                            case 66: return String.Format("shambler ({0})", level);
                            case 67:
                            case 68: 
                            case 69:
                            case 70:
                            case 71:
                            case 72: return String.Format("shapechange ({0})", level);
                            case 73:
                            case 74: 
                            case 75:
                            case 76:
                            case 77: return String.Format("soul bind ({0})", level);
                            case 78:
                            case 79:
                            case 80: 
                            case 81:
                            case 82:
                            case 83: return String.Format("storm of vengeance ({0})", level);
                            case 84:
                            case 85:
                            case 86:
                            case 87:
                            case 88:
                            case 89: return String.Format("summon monster IX ({0})", level);
                            case 90:
                            case 91:
                            case 92:
                            case 93:
                            case 94:
                            case 95: return String.Format("summon nature's ally IX ({0})", level);
                            case 96:
                            case 97:
                            case 98:
                            case 99: return String.Format("sympathy ({0})", level);
                            case 100: return String.Format("true resurrection ({0})", level);
                            default: return "[Error: 9th level divine out of range.  Scrolls.1849]";
                        }
                    default:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                            case 3: return String.Format("astral projection ({0})", level);
                            case 4:
                            case 5:
                            case 6:
                            case 7: return String.Format("crushing hand ({0})", level);
                            case 8: 
                            case 9:
                            case 10:
                            case 11:
                            case 12: return String.Format("dominate monster ({0})", level);
                            case 13:
                            case 14: 
                            case 15:
                            case 16: return String.Format("energy drain ({0})", level);
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                            case 21: return String.Format("etherealness ({0})", level);
                            case 22:
                            case 23: 
                            case 24:
                            case 25: return String.Format("foresight ({0})", level);
                            case 26:
                            case 27:
                            case 28: 
                            case 29:
                            case 30:
                            case 31: return String.Format("freedom ({0})", level);
                            case 32:
                            case 33: 
                            case 34:
                            case 35:
                            case 36: return String.Format("gate ({0})", level);
                            case 37:
                            case 38: 
                            case 39:
                            case 40: return String.Format("mass hold monster ({0})", level);
                            case 41:
                            case 42:
                            case 43:
                            case 44: return String.Format("imprisonment ({0})", level);
                            case 45:
                            case 46:
                            case 47:
                            case 48:
                            case 49: return String.Format("meteor swarm ({0})", level);
                            case 50:
                            case 51:
                            case 52:
                            case 53: return String.Format("mage's disjunction ({0})", level);
                            case 54:
                            case 55:
                            case 56:
                            case 57:
                            case 58: return String.Format("power word kill ({0})", level);
                            case 59:
                            case 60:
                            case 61:
                            case 62: return String.Format("prismatic sphere ({0})", level);
                            case 63: 
                            case 64:
                            case 65:
                            case 66: return String.Format("refuge ({0})", level);
                            case 67:
                            case 68:
                            case 69:
                            case 70: return String.Format("shades ({0})", level);
                            case 71:
                            case 72:
                            case 73: 
                            case 74:
                            case 75:
                            case 76: return String.Format("shapechange ({0})", level);
                            case 77:
                            case 78:
                            case 79: return String.Format("soul bind ({0})", level);
                            case 80:
                            case 81:
                            case 82:
                            case 83: return String.Format("Summon monster IX ({0})", level);
                            case 84:
                            case 85:
                            case 86: return String.Format("teleportation circle ({0})", level);
                            case 87:
                            case 88: 
                            case 89:
                            case 90:
                            case 91: return String.Format("time stop ({0})", level);
                            case 92:
                            case 93:
                            case 94:
                            case 95: return String.Format("wail of the banshee ({0})", level);
                            case 96:
                            case 97:
                            case 98:
                            case 99: return String.Format("weird ({0})", level);
                            case 100: return String.Format("wish ({0})", level);
                            default: return "[Error: 9th-level Arcane out of range.  Scrolls.1954]";
                        }
                }
            }
        }

        public static String RandomSpell(Int32 maxLevel)
        {
            switch (Dice.Roll(1, maxLevel))
            {
                case 1: return Level1(RandomSpellType());
                case 2: return Level2(RandomSpellType());
                case 3: return Level3(RandomSpellType());
                case 4: return Level4(RandomSpellType());
                case 5: return Level5(RandomSpellType());
                case 6: return Level6(RandomSpellType());
                case 7: return Level7(RandomSpellType());
                case 8: return Level8(RandomSpellType());
                default: return Level9(RandomSpellType());
            }
        }

        private static SPELLTYPE RandomSpellType()
        {
            return (SPELLTYPE)Dice.Roll(1, 2);
        }

        public static String RandomSpell(Int32 MaxLevel, SPELLTYPE SpellType)
        {
            switch (Dice.Roll(1, MaxLevel))
            {
                case 1: return Level1(SpellType);
                case 2: return Level2(SpellType);
                case 3: return Level3(SpellType);
                case 4: return Level4(SpellType);
                case 5: return Level5(SpellType);
                case 6: return Level6(SpellType);
                case 7: return Level7(SpellType);
                case 8: return Level8(SpellType);
                default: return Level9(SpellType);
            }
        }
    }
}
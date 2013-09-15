using NPCGen.Characters;
using NPCGen.Roll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NPCGen.Equipment
{
    public class Armor
    {
        private enum ARMORTYPE { LIGHT, MEDIUM, HEAVY };

        public static String GenerateArmor(Int32 bonus, CLASS charClass, Int32 level)
        {
            if (!CanWear(charClass))
                return String.Empty;

            var power = MagicItems.PowerByLevel(level);
            var outputBonus = 0; 
            var inputBonus = bonus;
            var abilitiesString = String.Empty;
            String cursedString = String.Empty;

            var suitofArmor = ArmorType(charClass);

            if (bonus > 0 && Dice.Percentile() <= 5)
                cursedString = CursedItem.Generate() + " ";

            while (inputBonus > 0)
            {
                switch (power)
                {
                    case POWER.MINOR:
                        if (Dice.Roll(1, 35) > 22 && outputBonus > 0)
                        {
                            if (String.IsNullOrEmpty(abilitiesString))
                                abilitiesString += " of ";
                            else
                                abilitiesString += ", ";
                            abilitiesString += ArmorSpecialAbility(power, ref inputBonus);
                        }
                        else
                        {
                            inputBonus--;
                            outputBonus++;
                        }
                        break;
                    case POWER.MEDIUM:
                        var roll = Dice.Roll(1, 67);
                        if (roll > 30 && outputBonus > 0)
                        {
                            if (String.IsNullOrEmpty(abilitiesString))
                                abilitiesString += " of ";
                            else
                                abilitiesString += ", ";
                            abilitiesString += ArmorSpecialAbility(power, ref inputBonus);
                        }
                        else if (roll > 27 && roll < 31)
                            return SpecificArmor(POWER.MEDIUM, charClass);
                        else
                        {
                            inputBonus--;
                            outputBonus++;
                        }
                        break;
                    case POWER.MAJOR:
                        roll = Dice.Roll(1, 67, 0);
                        if (roll > 30 && outputBonus > 0)
                        {
                            if (abilitiesString == String.Empty)
                                abilitiesString += " of ";
                            else
                                abilitiesString += ", ";
                            abilitiesString += ArmorSpecialAbility(power, ref inputBonus);
                        }
                        else if (roll > 27 && roll < 31)
                            return SpecificArmor(POWER.MAJOR, charClass);
                        else
                        {
                            inputBonus--;
                            outputBonus++;
                        }
                        break;
                    default: return ArmorType(charClass);
                }
            }

            var outputBonusString = String.Empty;
            if (outputBonus > 0)
                outputBonusString = String.Format("+{0} ", outputBonus);
            var specialQualitiesString = MagicItems.SpecialQualities(false, bonus);
            if (!String.IsNullOrEmpty(specialQualitiesString))
                specialQualitiesString = String.Format(" ({0})", specialQualitiesString);

            return String.Format("{0}{1}{2}{3}{4}", cursedString, outputBonusString, suitofArmor, abilitiesString, specialQualitiesString);
        }

        public static String GenerateShield(Int32 bonus, CLASS charClass, Int32 level)
        {
            if (!CanWield(charClass, false))
                return String.Empty;

            var power = MagicItems.PowerByLevel(level);
            var outputBonus = 0; 
            var inputBonus = bonus;
            var abilitiesString = String.Empty;
            var cursedString = String.Empty;

            if (bonus > 0 && Dice.Percentile() <= 5)
                cursedString = CursedItem.Generate() + " ";

            var shield = ShieldType(charClass);

            while (inputBonus > 0)
            {
                switch (power)
                {
                    case POWER.MINOR:
                        if (Dice.Roll(1, 87) > 65 && outputBonus > 0)
                        {
                            if (String.IsNullOrEmpty(abilitiesString))
                                abilitiesString += " of ";
                            else
                                abilitiesString += ", ";
                            abilitiesString += ShieldSpecialAbility(power, ref inputBonus);
                        }
                        else
                        {
                            inputBonus--;
                            outputBonus++;
                        }
                        break;
                    case POWER.MEDIUM:
                        var roll = Dice.Roll(1, 70);
                        if (roll > 33 && outputBonus > 0)
                        {
                            if (String.IsNullOrEmpty(abilitiesString))
                                abilitiesString += " of ";
                            else
                                abilitiesString += ", ";
                            abilitiesString += ShieldSpecialAbility(power, ref inputBonus);
                        }
                        else if (roll > 30 && roll < 34)
                            return SpecificShield(POWER.MEDIUM, charClass);
                        else
                        {
                            inputBonus--;
                            outputBonus++;
                        }
                        break;
                    case POWER.MAJOR:
                        roll = Dice.Roll(1, 70);
                        if (roll > 33 && outputBonus > 0)
                        {
                            if (String.IsNullOrEmpty(abilitiesString))
                                abilitiesString += " of ";
                            else
                                abilitiesString += ", ";
                            abilitiesString += ShieldSpecialAbility(power, ref inputBonus);
                        }
                        else if (roll > 30 && roll < 34)
                            return SpecificShield(POWER.MAJOR, charClass);
                        else
                        {
                            inputBonus--;
                            outputBonus++;
                        }
                        break;
                    default: return ShieldType(charClass);
                }
            }

            var outputBonusString = String.Empty;
            if (outputBonus > 0)
                outputBonusString = String.Format("+{0} ", outputBonus);
            var specialQualitiesString = MagicItems.SpecialQualities(false, bonus);
            if (!String.IsNullOrEmpty(specialQualitiesString))
                specialQualitiesString = String.Format(" ({0})", specialQualitiesString);

            return String.Format("{0}{1}{2}{3}{4}", cursedString, outputBonusString, shield, abilitiesString, specialQualitiesString);
        }

        public static Boolean CanWield(CLASS charClass, Boolean metal)
        {
            switch (charClass)
            {
                case CLASS.MONK:
                case CLASS.THIEF:
                case CLASS.SORCERER:
                case CLASS.WIZARD: return false;
                case CLASS.DRUID: return !metal;
                default: return true;
            }
        }

        private static bool CanWear(CLASS charClass, ARMORTYPE armorType, Boolean metal)
        {
            switch (charClass)
            {
                case CLASS.BARBARIAN:
                case CLASS.BARD:
                case CLASS.RANGER: return armorType != ARMORTYPE.HEAVY;
                case CLASS.CLERIC:
                case CLASS.PALADIN:
                case CLASS.FIGHTER: return true;
                case CLASS.DRUID: return armorType != ARMORTYPE.HEAVY && !metal;
                case CLASS.THIEF: return armorType == ARMORTYPE.LIGHT;
                default: return false;
            }
        }

        private static Boolean CanWear(CLASS charClass)
        {
            switch (charClass)
            {
                case CLASS.MONK:
                case CLASS.SORCERER:
                case CLASS.WIZARD: return false;
                default: return true;
            }
        }

        private static String ArmorType(CLASS charClass)
        {
            if (CanWear(charClass))
            {
                while (true)
                {
                    Application.DoEvents();
                    switch (Dice.Percentile())
                    {
                        case 1:
                            if (CanWear(charClass, ARMORTYPE.LIGHT, false))
                                return "padded armor (AC 9)";
                            break;
                        case 2:
                            if (CanWear(charClass, ARMORTYPE.LIGHT, false))
                                return "leather armor (AC 8)";
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
                            if (CanWear(charClass, ARMORTYPE.MEDIUM, false))
                                return "hide armor (AC 7)";
                            break;
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
                            if (CanWear(charClass, ARMORTYPE.LIGHT, true))
                                return "studded leather armor (AC 7)";
                            break;
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
                            if (CanWear(charClass, ARMORTYPE.LIGHT, true))
                                return "chain shirt (AC 6)";
                            break;
                        case 43:
                            if (CanWear(charClass, ARMORTYPE.MEDIUM, true))
                                return "scale mail (AC 6)";
                            break;
                        case 44:
                            if (CanWear(charClass, ARMORTYPE.MEDIUM, true))
                                return "chainmail (AC 5)";
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
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                            if (CanWear(charClass, ARMORTYPE.MEDIUM, true))
                                return "breastplate (AC 5)";
                            break;
                        case 58:
                            if (CanWear(charClass, ARMORTYPE.HEAVY, true))
                                return "splint mail (AC 4)";
                            break;
                        case 59:
                            if (CanWear(charClass, ARMORTYPE.HEAVY, true))
                                return "banded mail (AC 4)";
                            break;
                        case 60:
                            if (CanWear(charClass, ARMORTYPE.HEAVY, true))
                                return "half-plate (AC 3)";
                            break;
                        default:
                            if (CanWear(charClass, ARMORTYPE.HEAVY, true))
                                return "full plate (AC 2)";
                            break;
                    }
                }
            }
            return String.Empty;
        }

        private static String ShieldType(CLASS charClass)
        {
            while (true)
            {
                Application.DoEvents();
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
                    case 10: if (CanWield(charClass, true)) { return "buckler (AC +1)"; } break;
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15: if (CanWield(charClass, false)) { return "small wooden shield (AC +1)"; } break;
                    case 16:
                    case 17:
                    case 18:
                    case 19:
                    case 20: if (CanWield(charClass, true)) { return "small steel shield (AC +1)"; } break;
                    case 21:
                    case 22:
                    case 23:
                    case 24:
                    case 25:
                    case 26:
                    case 27:
                    case 28:
                    case 29:
                    case 30: if (CanWield(charClass, false)) { return "large wooden shield (AC +2)"; } break;
                    case 96:
                    case 97:
                    case 98:
                    case 99:
                    case 100: if (CanWield(charClass, false)) { return "tower shield"; } break;
                    default: if (CanWield(charClass, true)) { return "large steel shield (AC +2)"; } break;
                }
            }
        }

        private static String ArmorSpecialAbility(POWER power, ref Int32 bonusLimit)
        {
            while (true)
            {
                Application.DoEvents();
                switch (power)
                {
                    case POWER.MINOR:
                        switch (Dice.Percentile())
                        {
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
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "slick";
                                }
                                break;
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
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "shadow";
                                }
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
                            case 90:
                            case 91:
                            case 92:
                            case 93:
                            case 94:
                            case 95:
                            case 96:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "silent moves";
                                }
                                break;
                            case 97:
                            case 98:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "moderate fortification";
                                }
                                break;
                            case 99:
                            case 100: bonusLimit++; break;
                            default:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "glamered";
                                }
                                break;
                        } break;
                    case POWER.MEDIUM:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "light fortification";
                                }
                                break;
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "glamered";
                                }
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
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "slick";
                                }
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
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "shadow";
                                }
                                break;
                            case 50:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "spell resistance (13)";
                                }
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
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "ghost touch";
                                }
                                break;
                            case 61:
                            case 62:
                            case 63:
                            case 64:
                            case 65:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "moderate fortification";
                                }
                                break;
                            case 66:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "spell resistance (15)";
                                }
                                break;
                            case 67:
                            case 68:
                            case 69:
                            case 70:
                            case 71:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "acid resistance/10";
                                }
                                break;
                            case 72:
                            case 73:
                            case 74:
                            case 75:
                            case 76:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "cold resistance/10";
                                }
                                break;
                            case 77:
                            case 78:
                            case 79:
                            case 80:
                            case 81:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "fire resistance/10";
                                }
                                break;
                            case 82:
                            case 83:
                            case 84:
                            case 85:
                            case 86:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "lightning resistance/10";
                                }
                                break;
                            case 87:
                            case 88:
                            case 89:
                            case 90:
                            case 91:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "sonic resistance/10";
                                }
                                break;
                            case 92:
                            case 93:
                            case 94:
                                if (bonusLimit >= 4)
                                {
                                    bonusLimit -= 4;
                                    return "spell resistance (17)";
                                }
                                break;
                            case 95:
                                if (bonusLimit >= 5)
                                {
                                    bonusLimit -= 5;
                                    return "etherealness";
                                }
                                break;
                            case 96:
                            case 97:
                            case 98:
                                if (bonusLimit >= 5)
                                {
                                    bonusLimit -= 5;
                                    return "heavy fortification";
                                }
                                break;
                            case 99:
                            case 100: bonusLimit++; break;
                            default:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "silent moves";
                                }
                                break;
                        } break;
                    case POWER.MAJOR:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "light fortification";
                                }
                                break;
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "glamered";
                                }
                                break;
                            case 9:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "slick";
                                }
                                break;
                            case 10:
                            case 11:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "shadow";
                                }
                                break;
                            case 12:
                            case 13:
                            case 14:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "silent moves";
                                }
                                break;
                            case 15:
                            case 16:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "spell resistance (13)";
                                }
                                break;
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                            case 21:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "ghost touch";
                                }
                                break;
                            case 22:
                            case 23:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "invulnerability (damage reduction 5/+1)";
                                }
                                break;
                            case 24:
                            case 25:
                            case 26:
                            case 27:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "moderate fortification";
                                }
                                break;
                            case 28:
                            case 29:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "spell resistance (15)";
                                }
                                break;
                            case 30:
                            case 31:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "acid resistance/10";
                                }
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
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "cold resistance/10";
                                }
                                break;
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
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "fire resistance/10";
                                }
                                break;
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
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "lightning resistance/10";
                                }
                                break;
                            case 62:
                            case 63:
                            case 64:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "sonic resistance/10";
                                }
                                break;
                            case 65:
                            case 66:
                            case 67:
                                if (bonusLimit >= 4)
                                {
                                    bonusLimit -= 4;
                                    return "spell resistance (17)";
                                }
                                break;
                            case 68:
                            case 69:
                                if (bonusLimit >= 5)
                                {
                                    bonusLimit -= 5;
                                    return "etherealness";
                                }
                                break;
                            case 70:
                            case 71:
                            case 72:
                                if (bonusLimit >= 5)
                                {
                                    bonusLimit -= 5;
                                    return "heavy fortification";
                                }
                                break;
                            case 73:
                            case 74:
                                if (bonusLimit >= 5)
                                {
                                    bonusLimit -= 5;
                                    return "spell resistance (19)";
                                }
                                break;
                            default: bonusLimit++; break;
                        } break;
                    default: return "[ERROR: Unpowered Armor ability.  Armor.875]";
                }
            }
        }

        private static String ShieldSpecialAbility(POWER power, ref Int32 bonusLimit)
        {
            while (true)
            {
                Application.DoEvents();
                switch (power)
                {
                    case POWER.MINOR:
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
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "bashing";
                                }
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
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "blinding";
                                }
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
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "light fortification";
                                }
                                break;
                            case 100: bonusLimit++; break;
                            default:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "arrow deflection";
                                }
                                break;
                        } break;
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
                            case 10:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "arrow deflection";
                                }
                                break;
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15:
                            case 16:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "animated";
                                }
                                break;
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "spell resistance (13)";
                                }
                                break;
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                            case 25:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "ghost touch";
                                }
                                break;
                            case 26:
                            case 27:
                            case 28:
                            case 29:
                            case 30:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "moderate fortification";
                                }
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
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "acid resistance/10";
                                }
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
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "cold resistance/10";
                                }
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
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "fire resistance/10";
                                }
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
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "sonic resistance/10";
                                }
                                break;
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
                                if (bonusLimit >= 5)
                                {
                                    bonusLimit -= 5;
                                    return "reflecting";
                                }
                                break;
                            case 91:
                            case 92:
                            case 93:
                            case 94:
                            case 95:
                            case 96:
                            case 97:
                            case 98:
                            case 99:
                            case 100: bonusLimit++; break;
                            default:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "lightning resistance/10";
                                }
                                break;
                        } break;
                    case POWER.MAJOR:
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
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "animated";
                                }
                                break;
                            case 16:
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "spell resistance (13)";
                                }
                                break;
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                            case 25:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "ghost touch";
                                }
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
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "moderate fortification";
                                }
                                break;
                            case 36:
                            case 37:
                            case 38:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "acid resistance/10";
                                }
                                break;
                            case 39:
                            case 40:
                            case 41:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "cold resistance/10";
                                }
                                break;
                            case 42:
                            case 43:
                            case 44:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "fire resistance/10";
                                }
                                break;
                            case 45:
                            case 46:
                            case 47:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "lightning resistance/10";
                                }
                                break;
                            case 48:
                            case 49:
                            case 50:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "sonic resistance/10";
                                }
                                break;
                            case 51:
                            case 52:
                            case 53:
                            case 54:
                            case 55:
                                if (bonusLimit >= 3)
                                {
                                    bonusLimit -= 3;
                                    return "spell resistance (15)";
                                }
                                break;
                            case 56:
                            case 57:
                            case 58:
                            case 59:
                            case 60:
                                if (bonusLimit >= 4)
                                {
                                    bonusLimit -= 4;
                                    return "spell resistance (17)";
                                }
                                break;
                            case 61:
                            case 62:
                            case 63:
                            case 64:
                            case 65:
                                if (bonusLimit >= 5)
                                {
                                    bonusLimit -= 5;
                                    return "heavy fortification";
                                }
                                break;
                            case 66:
                            case 67:
                            case 68:
                            case 69:
                            case 70:
                                if (bonusLimit >= 5)
                                {
                                    bonusLimit -= 5;
                                    return "reflecting";
                                }
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
                                if (bonusLimit >= 5)
                                {
                                    bonusLimit -= 5;
                                    return "spell resistance (19)";
                                }
                                break;
                            default: bonusLimit++; break;
                        } break;
                    default: return "[ERROR: Unpowered Shield Ability.  Armor.1307]";
                }
            }
        }

        private static String SpecificArmor(POWER power, CLASS charClass)
        {
            while (true)
            {
                Application.DoEvents();
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
                            case 10: if (CanWear(charClass, ARMORTYPE.LIGHT, true)) { return "mithral shirt"; } break;
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
                            case 25: if (CanWear(charClass, ARMORTYPE.LIGHT, true)) { return "Elven Chain"; } break;
                            case 26:
                            case 27:
                            case 28:
                            case 29:
                            case 30:
                            case 31:
                            case 32:
                            case 33:
                            case 34:
                            case 35: if (CanWear(charClass, ARMORTYPE.MEDIUM, false)) { return "Rhino Hide"; } break;
                            case 36:
                            case 37:
                            case 38:
                            case 39:
                            case 40:
                            case 41:
                            case 42:
                            case 43:
                            case 44:
                            case 45: if (CanWear(charClass, ARMORTYPE.MEDIUM, true)) { return "adamantine breastplate"; } break;
                            case 71:
                            case 72:
                            case 73:
                            case 74:
                            case 75:
                            case 76:
                            case 77:
                            case 78:
                            case 79:
                            case 80: if (CanWear(charClass, ARMORTYPE.HEAVY, true)) { return "Plate Armor of the Deep"; } break;
                            case 81:
                            case 82:
                            case 83:
                            case 84:
                            case 85:
                            case 86:
                            case 87:
                            case 88:
                            case 89:
                            case 90: if (CanWear(charClass, ARMORTYPE.HEAVY, true)) { return "Banded Mail of Luck"; } break;
                            case 91:
                            case 92:
                            case 93:
                            case 94:
                            case 95:
                            case 96:
                            case 97:
                            case 98:
                            case 99:
                            case 100: if (CanWear(charClass, ARMORTYPE.MEDIUM, true)) { return "Breastplate of Command"; } break;
                            default: if (CanWear(charClass, ARMORTYPE.MEDIUM, true)) { return "Dwarven Plate"; } break;
                        } break;
                    case POWER.MAJOR:
                        var roll = Dice.Percentile();
                        if (!CanWear(charClass, ARMORTYPE.LIGHT, true))
                            return "Rhino Hide";
                        else if (roll > 80 && CanWear(charClass, ARMORTYPE.HEAVY, true))
                            return "Demon Armor";
                        else if (roll > 60 && roll < 81 && CanWear(charClass, ARMORTYPE.LIGHT, true))
                            return "Celestial Armor";
                        else if (roll > 40 && roll < 61 && CanWear(charClass, ARMORTYPE.MEDIUM, true))
                            return "Breastplate of Command";
                        else if (roll > 10 && roll < 41 && CanWear(charClass, ARMORTYPE.HEAVY, true))
                            return "Banded Mail of Luck";
                        else if (roll < 11 && CanWear(charClass, ARMORTYPE.HEAVY, true))
                            return "Plate Mail of the Deep";
                        break;
                    default: return "[ERROR: Unpowered or minor specific armor.  Armor.1415]";
                }
            }
        }

        private static String SpecificShield(POWER power, CLASS charClass)
        {
            while (true)
            {
                Application.DoEvents();
                String spell;
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
                            case 10: if (CanWield(charClass, false)) { return "darkwood shield"; } break;
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15:
                            case 16:
                            case 17:
                            case 18: if (CanWield(charClass, true)) { return "mithral large shield"; } break;
                            case 19:
                            case 20:
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                            case 25: if (CanWield(charClass, true)) { return "adamantine shield"; } break;
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
                            case 45: if (CanWield(charClass, true)) { return "Spined Shield"; } break;
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
                                if (CanWield(charClass, false))
                                {
                                    spell = String.Empty;
                                    if (Dice.Percentile() <= 50)
                                    {
                                        if (Dice.Percentile() <= 80)
                                            spell = String.Format(" (has {0})", Scrolls.RandomSpell(9, SPELLTYPE.DIVINE));
                                        else
                                            spell = String.Format(" (has {0})", Scrolls.RandomSpell(9, SPELLTYPE.ARCANE));
                                    }
                                    return String.Format("Caster's Shield{0}", spell);
                                } break;
                            case 91:
                            case 92:
                            case 93:
                            case 94:
                            case 95:
                            case 96:
                            case 97:
                            case 98:
                            case 99:
                            case 100: if (CanWield(charClass, false)) { return "Winged Shield"; } break;
                            default: if (CanWield(charClass, true)) { return "Lion's Shield"; } break;
                        } break;
                    case POWER.MAJOR:
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
                            case 20: if (CanWield(charClass, true)) { return "Spined Shield"; } break;
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
                                if (CanWield(charClass, false))
                                {
                                    spell = String.Empty;
                                    if (Dice.Percentile() <= 50)
                                    {
                                        if (Dice.Percentile() <= 80)
                                            spell = String.Format(" (has {0})", Scrolls.RandomSpell(9, SPELLTYPE.DIVINE));
                                        else
                                            spell = String.Format(" (has {0})", Scrolls.RandomSpell(9, SPELLTYPE.ARCANE));
                                    }
                                    return String.Format("Caster's Shield{0}", spell);
                                } break;
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
                            case 60: if (CanWield(charClass, true)) { return "Lion's Shield"; } break;
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
                            case 80: if (CanWield(charClass, false)) { return "Winged Shield"; } break;
                            default: if (CanWield(charClass, true)) { return "Absorbing Shield"; } break;
                        } break;
                    default: return "[ERROR: Non-powered or minor specific shield.  Armor.1617]";
                }
            }
        }
    }
}
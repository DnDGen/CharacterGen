using System;
using System.Collections.Generic;
using System.Text;

namespace NPCGen
{
    class Armor
    {
        private enum ARMORTYPE { LIGHT, MEDIUM, HEAVY };

        public static string GenerateArmor(int Bonus, Classes.CLASS Class, int Level, ref Random random)
        {
            if (!CanWear(Class))
                return "";

            MagicItems.POWER Power = MagicItems.PowerByLevel(Level, ref random);
            int OutputBonus = 0; int InputBonus = Bonus; string AbilitiesString = ""; int Roll;
            string OutputBonusString; string SpecialQualitiesString; string CursedString = "";

            string SuitofArmor = ArmorType(Class, ref random);

            if (Bonus > 0 && Dice.Percentile(ref random) <= 5)
                CursedString = CursedItem.Generate(ref random) + " ";

            while (InputBonus > 0)
            {
                switch (Power)
                {
                    case MagicItems.POWER.MINOR:
                        if (Dice.Roll(1, 35, 0, ref random) > 22 && OutputBonus > 0)
                        {
                            if (AbilitiesString == "")
                                AbilitiesString += " of ";
                            else
                                AbilitiesString += ", ";
                            AbilitiesString += ArmorSpecialAbility(Power, ref InputBonus, ref random);
                        }
                        else
                        {
                            InputBonus--;
                            OutputBonus++;
                        }
                        break;
                    case MagicItems.POWER.MEDIUM:
                        Roll = Dice.Roll(1, 67, 0, ref random);
                        if (Roll > 30 && OutputBonus > 0)
                        {
                            if (AbilitiesString == "")
                                AbilitiesString += " of ";
                            else
                                AbilitiesString += ", ";
                            AbilitiesString += ArmorSpecialAbility(Power, ref InputBonus, ref random);
                        }
                        else if (Roll > 27 && Roll < 31)
                            return SpecificArmor(MagicItems.POWER.MEDIUM, Class, ref random);
                        else
                        {
                            InputBonus--;
                            OutputBonus++;
                        }
                        break;
                    case MagicItems.POWER.MAJOR:
                        Roll = Dice.Roll(1, 67, 0, ref random);
                        if (Roll > 30 && OutputBonus > 0)
                        {
                            if (AbilitiesString == "")
                                AbilitiesString += " of ";
                            else
                                AbilitiesString += ", ";
                            AbilitiesString += ArmorSpecialAbility(Power, ref InputBonus, ref random);
                        }
                        else if (Roll > 27 && Roll < 31)
                            return SpecificArmor(MagicItems.POWER.MAJOR, Class, ref random);
                        else
                        {
                            InputBonus--;
                            OutputBonus++;
                        }
                        break;
                    default: return ArmorType(Class, ref random);
                }
            }

            OutputBonusString = "";
            if (OutputBonus > 0)
                OutputBonusString = String.Format("+{0} ", OutputBonus);
            SpecialQualitiesString = MagicItems.SpecialQualities(false, Bonus, ref random);
            if (SpecialQualitiesString != "")
                SpecialQualitiesString = String.Format(" ({0})", SpecialQualitiesString);

            return String.Format("{0}{1}{2}{3}{4}", CursedString, OutputBonusString, SuitofArmor, AbilitiesString, SpecialQualitiesString);
        }

        public static string GenerateShield(int Bonus, Classes.CLASS Class, int Level, ref Random random)
        {
            if (!CanWield(Class, false))
                return "";

            MagicItems.POWER Power = MagicItems.PowerByLevel(Level, ref random);
            int OutputBonus = 0; int InputBonus = Bonus; string AbilitiesString = ""; int Roll;
            string OutputBonusString; string SpecialQualitiesString; string CursedString = "";

            if (Bonus > 0 && Dice.Percentile(ref random) <= 5)
                CursedString = CursedItem.Generate(ref random) + " ";

            string Shield = ShieldType(Class, ref random);

            while (InputBonus > 0)
            {
                switch (Power)
                {
                    case MagicItems.POWER.MINOR:
                        if (Dice.Roll(1, 87, 0, ref random) > 65 && OutputBonus > 0)
                        {
                            if (AbilitiesString == "")
                                AbilitiesString += " of ";
                            else
                                AbilitiesString += ", ";
                            AbilitiesString += ShieldSpecialAbility(Power, ref InputBonus, ref random);
                        }
                        else
                        {
                            InputBonus--;
                            OutputBonus++;
                        }
                        break;
                    case MagicItems.POWER.MEDIUM:
                        Roll = Dice.Roll(1, 70, 0, ref random);
                        if (Roll > 33 && OutputBonus > 0)
                        {
                            if (AbilitiesString == "")
                                AbilitiesString += " of ";
                            else
                                AbilitiesString += ", ";
                            AbilitiesString += ShieldSpecialAbility(Power, ref InputBonus, ref random);
                        }
                        else if (Roll > 30 && Roll < 34)
                            return SpecificShield(MagicItems.POWER.MEDIUM, Class, ref random);
                        else
                        {
                            InputBonus--;
                            OutputBonus++;
                        }
                        break;
                    case MagicItems.POWER.MAJOR:
                        Roll = Dice.Roll(1, 70, 0, ref random);
                        if (Roll > 33 && OutputBonus > 0)
                        {
                            if (AbilitiesString == "")
                                AbilitiesString += " of ";
                            else
                                AbilitiesString += ", ";
                            AbilitiesString += ShieldSpecialAbility(Power, ref InputBonus, ref random);
                        }
                        else if (Roll > 30 && Roll < 34)
                            return SpecificShield(MagicItems.POWER.MAJOR, Class, ref random);
                        else
                        {
                            InputBonus--;
                            OutputBonus++;
                        }
                        break;
                    default: return ShieldType(Class, ref random);
                }
            }

            OutputBonusString = "";
            if (OutputBonus > 0)
                OutputBonusString = String.Format("+{0} ", OutputBonus);
            SpecialQualitiesString = MagicItems.SpecialQualities(false, Bonus, ref random);
            if (SpecialQualitiesString != "")
                SpecialQualitiesString = String.Format(" ({0})", SpecialQualitiesString);

            return String.Format("{0}{1}{2}{3}{4}", CursedString, OutputBonusString, Shield, AbilitiesString, SpecialQualitiesString);
        }

        public static bool CanWield(Classes.CLASS Class, bool Metal)
        {
            switch (Class)
            {
                case Classes.CLASS.MONK:
                case Classes.CLASS.THIEF:
                case Classes.CLASS.SORCERER:
                case Classes.CLASS.WIZARD: return false;
                case Classes.CLASS.DRUID:
                    if (!Metal)
                        return true;
                    return false;
                default: return true;
            }
        }

        private static bool CanWear(Classes.CLASS Class, ARMORTYPE ArmorType, bool Metal)
        {
            switch (Class)
            {
                case Classes.CLASS.BARBARIAN:
                case Classes.CLASS.BARD:
                case Classes.CLASS.RANGER:
                    if (ArmorType != ARMORTYPE.HEAVY)
                        return true;
                    return false;
                case Classes.CLASS.CLERIC:
                case Classes.CLASS.PALADIN:
                case Classes.CLASS.FIGHTER: return true;
                case Classes.CLASS.DRUID:
                    if (ArmorType != ARMORTYPE.HEAVY && !Metal)
                        return true;
                    return false;
                case Classes.CLASS.THIEF:
                    if (ArmorType == ARMORTYPE.LIGHT)
                        return true;
                    return false;
                default: return false;
            }
        }

        private static bool CanWear(Classes.CLASS Class)
        {
            switch (Class)
            {
                case Classes.CLASS.MONK:
                case Classes.CLASS.SORCERER:
                case Classes.CLASS.WIZARD: return false;
                default: return true;
            }
        }

        private static string ArmorType(Classes.CLASS Class, ref Random random)
        {
            if (CanWear(Class))
            {
                while (true)
                {
                    System.Windows.Forms.Application.DoEvents();
                    switch (Dice.Percentile(ref random))
                    {
                        case 1:
                            if (CanWear(Class, ARMORTYPE.LIGHT, false))
                                return "padded armor (AC 9)";
                            break;
                        case 2:
                            if (CanWear(Class, ARMORTYPE.LIGHT, false))
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
                            if (CanWear(Class, ARMORTYPE.MEDIUM, false))
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
                            if (CanWear(Class, ARMORTYPE.LIGHT, true))
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
                            if (CanWear(Class, ARMORTYPE.LIGHT, true))
                                return "chain shirt (AC 6)";
                            break;
                        case 43:
                            if (CanWear(Class, ARMORTYPE.MEDIUM, true))
                                return "scale mail (AC 6)";
                            break;
                        case 44:
                            if (CanWear(Class, ARMORTYPE.MEDIUM, true))
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
                            if (CanWear(Class, ARMORTYPE.MEDIUM, true))
                                return "breastplate (AC 5)";
                            break;
                        case 58:
                            if (CanWear(Class, ARMORTYPE.HEAVY, true))
                                return "splint mail (AC 4)";
                            break;
                        case 59:
                            if (CanWear(Class, ARMORTYPE.HEAVY, true))
                                return "banded mail (AC 4)";
                            break;
                        case 60:
                            if (CanWear(Class, ARMORTYPE.HEAVY, true))
                                return "half-plate (AC 3)";
                            break;
                        default:
                            if (CanWear(Class, ARMORTYPE.HEAVY, true))
                                return "full plate (AC 2)";
                            break;
                    }
                }
            }
            return "";
        }

        private static string ShieldType(Classes.CLASS Class, ref Random random)
        {
            while (true)
            {
                System.Windows.Forms.Application.DoEvents();
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
                    case 10: if (CanWield(Class, true)) { return "buckler (AC +1)"; } break;
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15: if (CanWield(Class, false)) { return "small wooden shield (AC +1)"; } break;
                    case 16:
                    case 17:
                    case 18:
                    case 19:
                    case 20: if (CanWield(Class, true)) { return "small steel shield (AC +1)"; } break;
                    case 21:
                    case 22:
                    case 23:
                    case 24:
                    case 25:
                    case 26:
                    case 27:
                    case 28:
                    case 29:
                    case 30: if (CanWield(Class, false)) { return "large wooden shield (AC +2)"; } break;
                    case 96:
                    case 97:
                    case 98:
                    case 99:
                    case 100: if (CanWield(Class, false)) { return "tower shield"; } break;
                    default: if (CanWield(Class, true)) { return "large steel shield (AC +2)"; } break;
                }
            }
        }

        private static string ArmorSpecialAbility(MagicItems.POWER Power, ref int BonusLimit, ref Random random)
        {
            while (true)
            {
                System.Windows.Forms.Application.DoEvents();
                switch (Power)
                {
                    case MagicItems.POWER.MINOR:
                        switch (Dice.Percentile(ref random))
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "silent moves";
                                }
                                break;
                            case 97:
                            case 98:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "moderate fortification";
                                }
                                break;
                            case 99:
                            case 100: BonusLimit++; break;
                            default:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "glamered";
                                }
                                break;
                        } break;
                    case MagicItems.POWER.MEDIUM:
                        switch (Dice.Percentile(ref random))
                        {
                            case 1:
                            case 2:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "light fortification";
                                }
                                break;
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "shadow";
                                }
                                break;
                            case 50:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
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
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "ghost touch";
                                }
                                break;
                            case 61:
                            case 62:
                            case 63:
                            case 64:
                            case 65:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "moderate fortification";
                                }
                                break;
                            case 66:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "spell resistance (15)";
                                }
                                break;
                            case 67:
                            case 68:
                            case 69:
                            case 70:
                            case 71:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "acid resistance/10";
                                }
                                break;
                            case 72:
                            case 73:
                            case 74:
                            case 75:
                            case 76:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "cold resistance/10";
                                }
                                break;
                            case 77:
                            case 78:
                            case 79:
                            case 80:
                            case 81:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "fire resistance/10";
                                }
                                break;
                            case 82:
                            case 83:
                            case 84:
                            case 85:
                            case 86:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "lightning resistance/10";
                                }
                                break;
                            case 87:
                            case 88:
                            case 89:
                            case 90:
                            case 91:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "sonic resistance/10";
                                }
                                break;
                            case 92:
                            case 93:
                            case 94:
                                if (BonusLimit >= 4)
                                {
                                    BonusLimit -= 4;
                                    return "spell resistance (17)";
                                }
                                break;
                            case 95:
                                if (BonusLimit >= 5)
                                {
                                    BonusLimit -= 5;
                                    return "etherealness";
                                }
                                break;
                            case 96:
                            case 97:
                            case 98:
                                if (BonusLimit >= 5)
                                {
                                    BonusLimit -= 5;
                                    return "heavy fortification";
                                }
                                break;
                            case 99:
                            case 100: BonusLimit++; break;
                            default:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "silent moves";
                                }
                                break;
                        } break;
                    case MagicItems.POWER.MAJOR:
                        switch (Dice.Percentile(ref random))
                        {
                            case 1:
                            case 2:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "light fortification";
                                }
                                break;
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "glamered";
                                }
                                break;
                            case 9:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "slick";
                                }
                                break;
                            case 10:
                            case 11:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "shadow";
                                }
                                break;
                            case 12:
                            case 13:
                            case 14:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "silent moves";
                                }
                                break;
                            case 15:
                            case 16:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "spell resistance (13)";
                                }
                                break;
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                            case 21:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "ghost touch";
                                }
                                break;
                            case 22:
                            case 23:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "invulnerability (damage reduction 5/+1)";
                                }
                                break;
                            case 24:
                            case 25:
                            case 26:
                            case 27:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "moderate fortification";
                                }
                                break;
                            case 28:
                            case 29:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "spell resistance (15)";
                                }
                                break;
                            case 30:
                            case 31:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
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
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
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
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
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
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "lightning resistance/10";
                                }
                                break;
                            case 62:
                            case 63:
                            case 64:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "sonic resistance/10";
                                }
                                break;
                            case 65:
                            case 66:
                            case 67:
                                if (BonusLimit >= 4)
                                {
                                    BonusLimit -= 4;
                                    return "spell resistance (17)";
                                }
                                break;
                            case 68:
                            case 69:
                                if (BonusLimit >= 5)
                                {
                                    BonusLimit -= 5;
                                    return "etherealness";
                                }
                                break;
                            case 70:
                            case 71:
                            case 72:
                                if (BonusLimit >= 5)
                                {
                                    BonusLimit -= 5;
                                    return "heavy fortification";
                                }
                                break;
                            case 73:
                            case 74:
                                if (BonusLimit >= 5)
                                {
                                    BonusLimit -= 5;
                                    return "spell resistance (19)";
                                }
                                break;
                            default: BonusLimit++; break;
                        } break;
                    default: return "[ERROR: Unpowered Armor ability.  Armor.875]";
                }
            }
        }

        private static string ShieldSpecialAbility(MagicItems.POWER Power, ref int BonusLimit, ref Random random)
        {
            while (true)
            {
                System.Windows.Forms.Application.DoEvents();
                switch (Power)
                {
                    case MagicItems.POWER.MINOR:
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "light fortification";
                                }
                                break;
                            case 100: BonusLimit++; break;
                            default:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "arrow deflection";
                                }
                                break;
                        } break;
                    case MagicItems.POWER.MEDIUM:
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
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "arrow deflection";
                                }
                                break;
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15:
                            case 16:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "animated";
                                }
                                break;
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "spell resistance (13)";
                                }
                                break;
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                            case 25:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "ghost touch";
                                }
                                break;
                            case 26:
                            case 27:
                            case 28:
                            case 29:
                            case 30:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
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
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
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
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
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
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
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
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
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
                                if (BonusLimit >= 5)
                                {
                                    BonusLimit -= 5;
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
                            case 100: BonusLimit++; break;
                            default:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "lightning resistance/10";
                                }
                                break;
                        } break;
                    case MagicItems.POWER.MAJOR:
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
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "animated";
                                }
                                break;
                            case 16:
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "spell resistance (13)";
                                }
                                break;
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                            case 25:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
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
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "moderate fortification";
                                }
                                break;
                            case 36:
                            case 37:
                            case 38:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "acid resistance/10";
                                }
                                break;
                            case 39:
                            case 40:
                            case 41:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "cold resistance/10";
                                }
                                break;
                            case 42:
                            case 43:
                            case 44:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "fire resistance/10";
                                }
                                break;
                            case 45:
                            case 46:
                            case 47:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "lightning resistance/10";
                                }
                                break;
                            case 48:
                            case 49:
                            case 50:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "sonic resistance/10";
                                }
                                break;
                            case 51:
                            case 52:
                            case 53:
                            case 54:
                            case 55:
                                if (BonusLimit >= 3)
                                {
                                    BonusLimit -= 3;
                                    return "spell resistance (15)";
                                }
                                break;
                            case 56:
                            case 57:
                            case 58:
                            case 59:
                            case 60:
                                if (BonusLimit >= 4)
                                {
                                    BonusLimit -= 4;
                                    return "spell resistance (17)";
                                }
                                break;
                            case 61:
                            case 62:
                            case 63:
                            case 64:
                            case 65:
                                if (BonusLimit >= 5)
                                {
                                    BonusLimit -= 5;
                                    return "heavy fortification";
                                }
                                break;
                            case 66:
                            case 67:
                            case 68:
                            case 69:
                            case 70:
                                if (BonusLimit >= 5)
                                {
                                    BonusLimit -= 5;
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
                                if (BonusLimit >= 5)
                                {
                                    BonusLimit -= 5;
                                    return "spell resistance (19)";
                                }
                                break;
                            default: BonusLimit++; break;
                        } break;
                    default: return "[ERROR: Unpowered Shield Ability.  Armor.1307]";
                }
            }
        }

        private static string SpecificArmor(MagicItems.POWER Power, Classes.CLASS Class, ref Random random)
        {
            int Roll;
            while (true)
            {
                System.Windows.Forms.Application.DoEvents();
                switch (Power)
                {
                    case MagicItems.POWER.MEDIUM:
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
                            case 10: if (CanWear(Class, ARMORTYPE.LIGHT, true)) { return "mithral shirt"; } break;
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
                            case 25: if (CanWear(Class, ARMORTYPE.LIGHT, true)) { return "Elven Chain"; } break;
                            case 26:
                            case 27:
                            case 28:
                            case 29:
                            case 30:
                            case 31:
                            case 32:
                            case 33:
                            case 34:
                            case 35: if (CanWear(Class, ARMORTYPE.MEDIUM, false)) { return "Rhino Hide"; } break;
                            case 36:
                            case 37:
                            case 38:
                            case 39:
                            case 40:
                            case 41:
                            case 42:
                            case 43:
                            case 44:
                            case 45: if (CanWear(Class, ARMORTYPE.MEDIUM, true)) { return "adamantine breastplate"; } break;
                            case 71:
                            case 72:
                            case 73:
                            case 74:
                            case 75:
                            case 76:
                            case 77:
                            case 78:
                            case 79:
                            case 80: if (CanWear(Class, ARMORTYPE.HEAVY, true)) { return "Plate Armor of the Deep"; } break;
                            case 81:
                            case 82:
                            case 83:
                            case 84:
                            case 85:
                            case 86:
                            case 87:
                            case 88:
                            case 89:
                            case 90: if (CanWear(Class, ARMORTYPE.HEAVY, true)) { return "Banded Mail of Luck"; } break;
                            case 91:
                            case 92:
                            case 93:
                            case 94:
                            case 95:
                            case 96:
                            case 97:
                            case 98:
                            case 99:
                            case 100: if (CanWear(Class, ARMORTYPE.MEDIUM, true)) { return "Breastplate of Command"; } break;
                            default: if (CanWear(Class, ARMORTYPE.MEDIUM, true)) { return "Dwarven Plate"; } break;
                        } break;
                    case MagicItems.POWER.MAJOR:
                        Roll = Dice.Percentile(ref random);
                        if (!CanWear(Class, ARMORTYPE.LIGHT, true))
                            return "Rhino Hide";
                        else if (Roll > 80 && CanWear(Class, ARMORTYPE.HEAVY, true))
                            return "Demon Armor";
                        else if (Roll > 60 && Roll < 81 && CanWear(Class, ARMORTYPE.LIGHT, true))
                            return "Celestial Armor";
                        else if (Roll > 40 && Roll < 61 && CanWear(Class, ARMORTYPE.MEDIUM, true))
                            return "Breastplate of Command";
                        else if (Roll > 10 && Roll < 41 && CanWear(Class, ARMORTYPE.HEAVY, true))
                            return "Banded Mail of Luck";
                        else if (Roll < 11 && CanWear(Class, ARMORTYPE.HEAVY, true))
                            return "Plate Mail of the Deep";
                        break;
                    default: return "[ERROR: Unpowered or minor specific armor.  Armor.1415]";
                }
            }
        }

        private static string SpecificShield(MagicItems.POWER Power, Classes.CLASS Class, ref Random random)
        {
            string spell;
            while (true)
            {
                System.Windows.Forms.Application.DoEvents();
                switch (Power)
                {
                    case MagicItems.POWER.MEDIUM:
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
                            case 10: if (CanWield(Class, false)) { return "darkwood shield"; } break;
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15:
                            case 16:
                            case 17:
                            case 18: if (CanWield(Class, true)) { return "mithral large shield"; } break;
                            case 19:
                            case 20:
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                            case 25: if (CanWield(Class, true)) { return "adamantine shield"; } break;
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
                            case 45: if (CanWield(Class, true)) { return "Spined Shield"; } break;
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
                                if (CanWield(Class, false))
                                {
                                    spell = "";
                                    if (Dice.Percentile(ref random) <= 50)
                                    {
                                        if (Dice.Percentile(ref random) <= 80)
                                            spell = String.Format(" (has {0})", Scrolls.RandomSpell(ref random, 9, Scrolls.SPELLTYPE.DIVINE));
                                        else
                                            spell = String.Format(" (has {0})", Scrolls.RandomSpell(ref random, 9, Scrolls.SPELLTYPE.ARCANE));
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
                            case 100: if (CanWield(Class, false)) { return "Winged Shield"; } break;
                            default: if (CanWield(Class, true)) { return "Lion's Shield"; } break;
                        } break;
                    case MagicItems.POWER.MAJOR:
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
                            case 20: if (CanWield(Class, true)) { return "Spined Shield"; } break;
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
                                if (CanWield(Class, false))
                                {
                                    spell = "";
                                    if (Dice.Percentile(ref random) <= 50)
                                    {
                                        if (Dice.Percentile(ref random) <= 80)
                                            spell = String.Format(" (has {0})", Scrolls.RandomSpell(ref random, 9, Scrolls.SPELLTYPE.DIVINE));
                                        else
                                            spell = String.Format(" (has {0})", Scrolls.RandomSpell(ref random, 9, Scrolls.SPELLTYPE.ARCANE));
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
                            case 60: if (CanWield(Class, true)) { return "Lion's Shield"; } break;
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
                            case 80: if (CanWield(Class, false)) { return "Winged Shield"; } break;
                            default: if (CanWield(Class, true)) { return "Absorbing Shield"; } break;
                        } break;
                    default: return "[ERROR: Non-powered or minor specific shield.  Armor.1617]";
                }
            }
        }
    }
}
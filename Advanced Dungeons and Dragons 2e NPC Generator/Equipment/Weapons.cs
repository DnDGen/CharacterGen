using NPCGen.Characters;
using NPCGen.Roll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NPCGen.Equipment
{
    public enum WEAPONTYPE { SLASH, PIERCE, BLUNT };
    public enum WEAPONFEAT { SIMPLE, MARTIAL, EXOTIC };
    public enum WEAPONRANGE { MELEE, RANGE };

    public class Weapons
    {
        public static String Generate(Int32 bonus, CLASS charClass, Int32 level, Boolean allowTwoHanded, ref Boolean twoHanded)
        {
            var outputBonus = 0; 
            var inputBonus = bonus;
            var abilitiesString = String.Empty; 
            Int32 roll;
            var type = WEAPONTYPE.BLUNT;
            var range = WEAPONRANGE.MELEE; 
            var ammunition = false;
            var cursedString = String.Empty;
            var allowRange = allowTwoHanded;
            String output = String.Empty;

            var power = MagicItems.PowerByLevel(level);
            var weapon = WeaponType(charClass, ref type, allowTwoHanded, ref twoHanded, allowRange, ref range, ref ammunition);

            if (bonus > 0 && Dice.Percentile() <= 5)
                cursedString = CursedItem.Generate() + " ";

            while (inputBonus > 0)
            {
                switch (power)
                {
                    case POWER.MEDIUM:
                        roll = Dice.Percentile();
                        if (roll > 68 && outputBonus > 0)
                        {
                            if (abilitiesString == String.Empty)
                                abilitiesString += " of ";
                            else
                                abilitiesString += ", ";
                            if (range == WEAPONRANGE.MELEE)
                                abilitiesString += MeleeSpecialAbilities(power, type, ref inputBonus);
                            else
                                abilitiesString += RangedSpecialAbilities(power, ref inputBonus);
                        }
                        else if (roll > 62 && roll <= 68)
                        {
                            range = WEAPONRANGE.MELEE;
                            output = SpecificWeapon(charClass, power, bonus, allowTwoHanded, ref twoHanded, allowRange, ref range);
                            if (range == WEAPONRANGE.RANGE)
                                output += String.Format("\n{0}", Melee(charClass, ref type, allowTwoHanded, ref twoHanded));
                            return output;
                        }
                        else
                        {
                            outputBonus++;
                            inputBonus--;
                        }
                        break;
                    case POWER.MAJOR:
                        roll = Dice.Percentile();
                        if (roll > 63 && outputBonus > 0)
                        {
                            if (abilitiesString == String.Empty)
                                abilitiesString += " of ";
                            else
                                abilitiesString += ", ";
                            if (range == WEAPONRANGE.MELEE)
                                abilitiesString += MeleeSpecialAbilities(power, type, ref inputBonus);
                            else
                                abilitiesString += RangedSpecialAbilities(power, ref inputBonus);
                        }
                        else if (roll > 49 && roll <= 63)
                        {
                            range = WEAPONRANGE.MELEE;
                            output = SpecificWeapon(charClass, power, bonus, allowTwoHanded, ref twoHanded, true, ref range);
                            if (range == WEAPONRANGE.RANGE)
                                output += String.Format("\n{0}", Melee(charClass, ref type, allowTwoHanded, ref twoHanded));
                            return output;
                        }
                        else
                        {
                            outputBonus++;
                            inputBonus--;
                        }
                        break;
                    default:
                        if (Dice.Percentile() > 85 && outputBonus > 0)
                        {
                            if (abilitiesString == String.Empty)
                                abilitiesString += " of ";
                            else
                                abilitiesString += ", ";
                            if (range == WEAPONRANGE.MELEE)
                                abilitiesString += MeleeSpecialAbilities(power, type, ref inputBonus);
                            else
                                abilitiesString += RangedSpecialAbilities(power, ref inputBonus);
                        }
                        else
                        {
                            outputBonus++;
                            inputBonus--;
                        }
                        break;
                }
            }

            var outputBonusString = String.Empty;
            if (outputBonus > 0)
                outputBonusString = String.Format("+{0} ", outputBonus);

            var specialQualitiesString = String.Empty;
            if (range == WEAPONRANGE.MELEE)
                specialQualitiesString = MeleeSpecialQualities(bonus);
            else
                specialQualitiesString = RangedSpecialQualities(bonus, ammunition);

            if (!String.IsNullOrEmpty(specialQualitiesString))
                specialQualitiesString = String.Format(" ({0})", specialQualitiesString);

            output = String.Format("{0}{1}{2}{3}{4}", cursedString, outputBonusString, weapon, abilitiesString, specialQualitiesString);
            if (range == WEAPONRANGE.RANGE)
                output += String.Format("\n{0}", Melee(charClass, ref type, allowTwoHanded, ref twoHanded));

            return output;
        }

        private static Boolean CanWield(CLASS charClass, WEAPONFEAT feat, String weapon)
        {
            switch (charClass)
            {
                case CLASS.BARBARIAN:
                case CLASS.FIGHTER:
                case CLASS.RANGER:
                case CLASS.PALADIN: return true;
                case CLASS.BARD: return feat != WEAPONFEAT.MARTIAL || Classes.BardWeapons.Contains(weapon);
                case CLASS.CLERIC:
                case CLASS.SORCERER: return feat != WEAPONFEAT.MARTIAL;
                case CLASS.DRUID: return Classes.DruidWeapons.Contains(weapon);
                case CLASS.MONK: return Classes.MonkWeapons.Contains(weapon);
                case CLASS.THIEF: return Classes.ThiefWeapons.Contains(weapon);
                case CLASS.WIZARD: return Classes.WizardWeapons.Contains(weapon);
                default: return false;
            }
        }

        private static String WeaponType(CLASS charClass, ref WEAPONTYPE type, Boolean allowTwoHanded, ref Boolean twoHanded, Boolean allowRange, ref WEAPONRANGE range, ref Boolean ammunition)
        {
            var roll = Dice.Percentile();

            range = WEAPONRANGE.MELEE;
            if (roll < 71)
                return Common(charClass, ref type, allowTwoHanded, ref twoHanded);
            if (roll < 81)
                return Uncommon(charClass, ref type, allowTwoHanded, ref twoHanded, allowRange, ref range);

            if (allowRange)
            {
                range = WEAPONRANGE.RANGE;
                return Ranged(charClass, ref type, ref ammunition);
            }
            return Common(charClass, ref type, allowTwoHanded, ref twoHanded);
        }

        public static String Melee(CLASS charClass, ref WEAPONTYPE type, Boolean allowTwoHanded, ref Boolean twoHanded)
        {
            var Range = WEAPONRANGE.MELEE;

            if (Dice.d8() < 8)
                return Common(charClass, ref type, allowTwoHanded, ref twoHanded);
            return Uncommon(charClass, ref type, allowTwoHanded, ref twoHanded, false, ref Range);
        }

        private static String Ranged(CLASS charClass, String ammunitionType)
        {
            while (true)
            {
                Application.DoEvents();
                switch (ammunitionType)
                {
                    case "arrows":
                        var roll = Dice.Roll(1, 55, 0);
                        if (roll > 50)
                            return "Mighty +4 composite longbow";
                        if (roll > 45)
                            return "Mighty +3 composite longbow";
                        if (roll > 40)
                            return "Mighty +2 composite longbow";
                        if (roll > 35)
                            return "Mighty +1 composite longbow";
                        if (roll > 30)
                            return "composite longbow";
                        if (roll > 20)
                            return "longbow";
                        if (roll > 15)
                            return "Mighty +2 composite shortbow";
                        if (roll > 10)
                            return "Mighty +1 composite shortbow";
                        if (roll > 5)
                            return "composite shortbow";
                        return "shortbow";
                    case "crossbow bolts":
                        if (Dice.d4() > 2)
                            return "heavy crossbow";
                        return "light crossbow";
                    case "sling bullets": return "sling";
                    default: return "[ERROR: Not a viable ammunition.  Weapons.227]";
                }
            }
        }

        public static String Ranged(CLASS charClass, ref WEAPONTYPE type, ref Boolean ammunition)
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
                    case 10:
                        String ammo;
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "bow"))
                        {
                            ammo = "arrows";
                            ammunition = true;
                            return String.Format("{0} (x{1}, {2})", ammo, Dice.Percentile() / 5 + 1, Ranged(charClass, ammo));
                        }
                        else if (CanWield(charClass, WEAPONFEAT.SIMPLE, "crossbow"))
                        {
                            ammo = "crossbow bolts";
                            ammunition = true;
                            return String.Format("{0} (x{1}, {2})", ammo, Dice.Percentile() / 5 + 1, Ranged(charClass, ammo));
                        }
                        else if (CanWield(charClass, WEAPONFEAT.SIMPLE, "sling"))
                        {
                            ammo = "sling bullets";
                            ammunition = true;
                            return String.Format("{0} (x{1}, {2})", ammo, Dice.Percentile() / 5 + 1, Ranged(charClass, ammo));
                        }
                        break;
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "throwing axe"))
                        {
                            type = WEAPONTYPE.SLASH;
                            return String.Format("throwing axe (x{0}, 1d6, Crit x2)", Dice.Percentile() / 5 + 1);
                        }
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
                        if (CanWield(charClass, WEAPONFEAT.SIMPLE, "heavy crossbow"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return String.Format("heavy crossbow ({0} bolts, 1d10, Crit 19-20/x2)", Dice.Percentile() / 5 + 1);
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
                        if (CanWield(charClass, WEAPONFEAT.SIMPLE, "light crossbow"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return String.Format("light crossbow ({0} bolts, 1d8, Crit 19-20/x2)", Dice.Percentile() / 5 + 1);
                        }
                        break;
                    case 36:
                    case 37:
                    case 38:
                    case 39:
                        if (CanWield(charClass, WEAPONFEAT.SIMPLE, "dart"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return String.Format("dart (x{0}, 1d4, Crit x2)", Dice.Percentile() / 5 + 1);
                        }
                        break;
                    case 40:
                    case 41:
                        if (CanWield(charClass, WEAPONFEAT.SIMPLE, "javelin"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return String.Format("javelin (x{0}, 1d6, Crit x2)", Dice.Percentile() / 5 + 1);
                        }
                        break;
                    case 42:
                    case 43:
                    case 44:
                    case 45:
                    case 46:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "shortbow"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return String.Format("shortbow ({0} arrows, 1d6, Crit x3)", Dice.Percentile() / 5 + 1);
                        }
                        break;
                    case 47:
                    case 48:
                    case 49:
                    case 50:
                    case 51:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "composite shortbow"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return String.Format("composite shortbow ({0} arrows, 1d6, Crit x3)", Dice.Percentile() / 5 + 1);
                        }
                        break;
                    case 52:
                    case 53:
                    case 54:
                    case 55:
                    case 56:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "composite shortbow"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return String.Format("Mighty +1 composite shortbow ({0} arrows, 1d6, Crit x3)", Dice.Percentile() / 5 + 1);
                        }
                        break;
                    case 57:
                    case 58:
                    case 59:
                    case 60:
                    case 61:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "composite shortbow"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return String.Format("Mighty +2 composite shortbow ({0} arrows, 1d6, Crit x3)", Dice.Percentile() / 5 + 1);
                        }
                        break;
                    case 62:
                    case 63:
                    case 64:
                    case 65:
                        if (CanWield(charClass, WEAPONFEAT.SIMPLE, "sling"))
                        {
                            type = WEAPONTYPE.BLUNT;
                            return String.Format("sling ({0} bullets, 1d4, Crit x2)", Dice.Percentile() / 5 + 1);
                        }
                        break;
                    case 76:
                    case 77:
                    case 78:
                    case 79:
                    case 80:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "composite longbow"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return String.Format("composite longbow ({0} arrows, 1d8, Crit x3)", Dice.Percentile() / 5 + 1);
                        }
                        break;
                    case 81:
                    case 82:
                    case 83:
                    case 84:
                    case 85:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "composite longbow"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return String.Format("Mighty +1 composite longbow ({0} arrows, 1d8, Crit x3)", Dice.Percentile() / 5 + 1);
                        }
                        break;
                    case 86:
                    case 87:
                    case 88:
                    case 89:
                    case 90:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "composite longbow"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return String.Format("Mighty +2 composite longbow ({0} arrows, 1d8, Crit x3)", Dice.Percentile() / 5 + 1);
                        }
                        break;
                    case 91:
                    case 92:
                    case 93:
                    case 94:
                    case 95:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "composite longbow"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return String.Format("Mighty +3 composite longbow ({0} arrows, 1d8, Crit x3)", Dice.Percentile() / 5 + 1);
                        }
                        break;
                    case 96:
                    case 97:
                    case 98:
                    case 99:
                    case 100:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "composite longbow"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return String.Format("Mighty +4 composite longbow ({0} arrows, 1d8, Crit x3)", Dice.Percentile() / 5 + 1);
                        }
                        break;
                    default:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "longbow"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return String.Format("longbow ({0} arrows)", Dice.Percentile() / 5 + 1);
                        }
                        break;
                }
            }
        }

        public static String Common(CLASS charClass, ref WEAPONTYPE type, Boolean allowTwoHanded, ref Boolean twoHanded)
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
                        if (CanWield(charClass, WEAPONFEAT.SIMPLE, "dagger"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return "dagger (1d4, Crit 19-20/x2)";
                        }
                        break;
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
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "greataxe") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.SLASH;
                            twoHanded = true;
                            return "greataxe (1d12, Crit x3)";
                        }
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
                    case 24:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "greatsword") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.SLASH;
                            twoHanded = true;
                            return "greatsword (2d6, Crit 19-20/x2)";
                        }
                        break;
                    case 25:
                    case 26:
                    case 27:
                    case 28:
                        if (CanWield(charClass, WEAPONFEAT.SIMPLE, "kama"))
                        {
                            type = WEAPONTYPE.SLASH;
                            return "kama (1d6, Crit x2)";
                        }
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
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "longsword"))
                        {
                            type = WEAPONTYPE.SLASH;
                            return "longsword (1d8, Crit 19-20/x2)";
                        }
                        break;
                    case 42:
                    case 43:
                    case 44:
                    case 45:
                        if (CanWield(charClass, WEAPONFEAT.SIMPLE, "light mace"))
                        {
                            type = WEAPONTYPE.BLUNT;
                            return "light mace (1d6, Crit x2)";
                        }
                        break;
                    case 46:
                    case 47:
                    case 48:
                    case 49:
                    case 50:
                        if (CanWield(charClass, WEAPONFEAT.SIMPLE, "heavy mace"))
                        {
                            type = WEAPONTYPE.BLUNT;
                            return "heavy mace (1d8, Crit x2)";
                        }
                        break;
                    case 51:
                    case 52:
                    case 53:
                    case 54:
                        if (CanWield(charClass, WEAPONFEAT.EXOTIC, "nunchaku"))
                        {
                            type = WEAPONTYPE.BLUNT;
                            return "nunchaku (1d6, Crit x2)";
                        }
                        break;
                    case 55:
                    case 56:
                    case 57:
                        if (CanWield(charClass, WEAPONFEAT.SIMPLE, "quarterstaff") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.BLUNT;
                            twoHanded = true;
                            return "quarterstaff (1d6/1d6, Crit x2)";
                        }
                        break;
                    case 58:
                    case 59:
                    case 60:
                    case 61:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "rapier"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return "rapier (1d6, Crit 18-20/x2)";
                        }
                        break;
                    case 62:
                    case 63:
                    case 64:
                    case 65:
                    case 66:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "scimitar"))
                        {
                            type = WEAPONTYPE.SLASH;
                            return "scimitar (1d6, Crit 18-20/x2)";
                        }
                        break;
                    case 67:
                    case 68:
                    case 69:
                    case 70:
                        if (CanWield(charClass, WEAPONFEAT.SIMPLE, "shortspear"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return "shortspear (1d8, Crit x3)";
                        }
                        break;
                    case 71:
                    case 72:
                    case 73:
                    case 74:
                        if (CanWield(charClass, WEAPONFEAT.EXOTIC, "siangham"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return "siangham (1d6, Crit x2)";
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
                        if (CanWield(charClass, WEAPONFEAT.EXOTIC, "bastard sword"))
                        {
                            type = WEAPONTYPE.SLASH;
                            return "bastard sword (1d10, Crit 19-20/x2)";
                        }
                        break;
                    case 85:
                    case 86:
                    case 87:
                    case 88:
                    case 89:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "short sword"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return "short sword (1d6, Crit 19-20/x2)";
                        }
                        break;
                    default:
                        if (CanWield(charClass, WEAPONFEAT.EXOTIC, "dwarven waraxe"))
                        {
                            type = WEAPONTYPE.SLASH;
                            return "dwarven waraxe (1d10, Crit x3)";
                        }
                        break;
                }
            }
        }

        public static String Uncommon(CLASS charClass, ref WEAPONTYPE type, Boolean allowTwoHanded, ref Boolean twoHanded, Boolean allowRanged, ref WEAPONRANGE range)
        {
            range = WEAPONRANGE.MELEE;

            while (true)
            {
                Application.DoEvents();
                switch (Dice.Percentile())
                {
                    case 1:
                    case 2:
                    case 3:
                        if (CanWield(charClass, WEAPONFEAT.EXOTIC, "orc double axe") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.SLASH;
                            twoHanded = true;
                            return "orc double axe (1d8/1d8, Crit x3)";
                        }
                        break;
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "battleaxe"))
                        {
                            type = WEAPONTYPE.SLASH;
                            return "battleaxe (1d8, Crit x3)";
                        }
                        break;
                    case 8:
                    case 9:
                    case 10:
                        if (CanWield(charClass, WEAPONFEAT.EXOTIC, "spiked chain") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.PIERCE;
                            twoHanded = true;
                            return "spiked chain (2d4, Crit x2)";
                        }
                        break;
                    case 11:
                    case 12:
                        if (CanWield(charClass, WEAPONFEAT.SIMPLE, "club"))
                        {
                            type = WEAPONTYPE.BLUNT;
                            return "club (1d6, Crit x2)";
                        }
                        break;
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                        if (CanWield(charClass, WEAPONFEAT.EXOTIC, "hand crossbow") && allowRanged)
                        {
                            type = WEAPONTYPE.PIERCE;
                            range = WEAPONRANGE.RANGE;
                            return "hand crossbow (1d4, Crit 19-20/x2)";
                        }
                        break;
                    case 17:
                    case 18:
                    case 19:
                        if (CanWield(charClass, WEAPONFEAT.EXOTIC, "repeating crossbow") && allowRanged)
                        {
                            type = WEAPONTYPE.PIERCE;
                            range = WEAPONRANGE.RANGE;
                            return "repeating crossbow (1d8, Crit 19-20/x2)";
                        }
                        break;
                    case 20:
                    case 21:
                        if (CanWield(charClass, WEAPONFEAT.SIMPLE, "punching dagger"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return "punching dagger (1d4, Crit x3)";
                        }
                        break;
                    case 22:
                    case 23:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "falchion") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.SLASH;
                            twoHanded = true;
                            return "falchion (2d4, Crit 18-20/x2)";
                        }
                        break;
                    case 24:
                    case 25:
                    case 26:
                        if (CanWield(charClass, WEAPONFEAT.EXOTIC, "dire flail") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.BLUNT;
                            twoHanded = true;
                            return "dire flail (1d8/1d8, Crit x2)";
                        }
                        break;
                    case 27:
                    case 28:
                    case 29:
                    case 30:
                    case 31:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "heavy flail") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.BLUNT;
                            twoHanded = true;
                            return "heavy flail (1d10, Crit 19-20/x2)";
                        }
                        break;
                    case 32:
                    case 33:
                    case 34:
                    case 35:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "light flail"))
                        {
                            type = WEAPONTYPE.BLUNT;
                            return "light flail (1d8, Crit x2)";
                        }
                        break;
                    case 36:
                    case 37:
                        if (CanWield(charClass, WEAPONFEAT.SIMPLE, "gauntlet") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.BLUNT;
                            twoHanded = true;
                            return "gauntlet (Unarmed strike, Crit x2)";
                        }
                        break;
                    case 38:
                    case 39:
                        if (CanWield(charClass, WEAPONFEAT.SIMPLE, "spiked gauntlet"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return "spiked gauntlet (1d4, Crit x2)";
                        }
                        break;
                    case 40:
                    case 41:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "glaive") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.SLASH;
                            twoHanded = true;
                            return "glaive (1d10, Crit x3)";
                        }
                        break;
                    case 42:
                    case 43:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "greatclub") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.BLUNT;
                            twoHanded = true;
                            return "greatclub (1d10, Crit x2)";
                        }
                        break;
                    case 44:
                    case 45:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "guisarme") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.SLASH;
                            twoHanded = true;
                            return "guisarme (2d4, Crit x3)";
                        }
                        break;
                    case 46:
                    case 47:
                    case 48:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "halberd") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.SLASH;
                            twoHanded = true;
                            return "halberd (1d10, Crit x3)";
                        }
                        break;
                    case 49:
                    case 50:
                    case 51:
                        if (CanWield(charClass, WEAPONFEAT.SIMPLE, "halfspear") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.PIERCE;
                            twoHanded = true;
                            return "halfspear (1d6, Crit x3)";
                        }
                        break;
                    case 52:
                    case 53:
                    case 54:
                        if (CanWield(charClass, WEAPONFEAT.EXOTIC, "gnome hooked hammer") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.BLUNT;
                            twoHanded = true;
                            return "gnome hooked hammer (1d6/1d4, Crit x3/x4)";
                        }
                        break;
                    case 55:
                    case 56:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "light hammer"))
                        {
                            type = WEAPONTYPE.BLUNT;
                            return "light hammer (1d4, Crit x2)";
                        }
                        break;
                    case 57:
                    case 58:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "handaxe"))
                        {
                            type = WEAPONTYPE.SLASH;
                            return "handaxe (1d6, Crit x3)";
                        }
                        break;
                    case 59:
                    case 60:
                    case 61:
                        if (CanWield(charClass, WEAPONFEAT.EXOTIC, "kukri"))
                        {
                            type = WEAPONTYPE.SLASH;
                            return "kukri (1d4, Crit 18-20/x2)";
                        }
                        break;
                    case 62:
                    case 63:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "heavy lance") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.PIERCE;
                            twoHanded = true;
                            return "heavy lance (1d8, Crit x3)";
                        }
                        break;
                    case 64:
                    case 65:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "light lance") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.PIERCE;
                            twoHanded = true;
                            return "light lance (1d6, Crit x3)";
                        }
                        break;
                    case 66:
                    case 67:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "longspear") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.PIERCE;
                            twoHanded = true;
                            return "longspear (1d8, Crit x3)";
                        }
                        break;
                    case 68:
                    case 69:
                    case 70:
                        if (CanWield(charClass, WEAPONFEAT.SIMPLE, "morningstar"))
                        {
                            type = WEAPONTYPE.BLUNT;
                            return "morningstar (1d8, Crit x2)";
                        }
                        break;
                    case 71:
                    case 72:
                        if (CanWield(charClass, WEAPONFEAT.EXOTIC, "net") && allowRanged)
                        {
                            type = WEAPONTYPE.BLUNT;
                            range = WEAPONRANGE.RANGE;
                            return "net";
                        }
                        break;
                    case 73:
                    case 74:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "heavy pick"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return "heavy pick (1d6, Crit x4)";
                        }
                        break;
                    case 75:
                    case 76:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "light pick"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return "light pick (1d4, Crit x4)";
                        }
                        break;
                    case 77:
                    case 78:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "ranseur") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.PIERCE;
                            twoHanded = true;
                            return "ranseur (2d4, Crit x3)";
                        }
                        break;
                    case 79:
                    case 80:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "sap"))
                        {
                            type = WEAPONTYPE.BLUNT;
                            return "sap (1d6, Crit x2)";
                        }
                        break;
                    case 81:
                    case 82:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "scythe") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.SLASH;
                            twoHanded = true;
                            return "scythe (2d4, Crit x4)";
                        }
                        break;
                    case 83:
                    case 84:
                        if (CanWield(charClass, WEAPONFEAT.EXOTIC, "shuriken") && allowRanged)
                        {
                            type = WEAPONTYPE.PIERCE;
                            range = WEAPONRANGE.RANGE;
                            return "shuriken (1, Crit x2)";
                        }
                        break;
                    case 85:
                    case 86:
                        if (CanWield(charClass, WEAPONFEAT.SIMPLE, "sickle"))
                        {
                            type = WEAPONTYPE.SLASH;
                            return "sickle (1d6, Crit x2)";
                        }
                        break;
                    case 87:
                    case 88:
                    case 89:
                        if (CanWield(charClass, WEAPONFEAT.EXOTIC, "two-bladed sword") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.SLASH;
                            twoHanded = true;
                            return "two-bladed sword (1d8/1d8, Crit 19-20/x2)";
                        }
                        break;
                    case 90:
                    case 91:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "trident"))
                        {
                            type = WEAPONTYPE.PIERCE;
                            return "trident (1d8, Crit x2)";
                        }
                        break;
                    case 92:
                    case 93:
                    case 94:
                        if (CanWield(charClass, WEAPONFEAT.EXOTIC, "dwarven urgrosh") && allowTwoHanded)
                        {
                            type = WEAPONTYPE.SLASH;
                            twoHanded = true;
                            return "dwarven urgrosh (1d8/1d6, Crit x3)";
                        }
                        break;
                    case 95:
                    case 96:
                    case 97:
                        if (CanWield(charClass, WEAPONFEAT.MARTIAL, "warhammer"))
                        {
                            type = WEAPONTYPE.BLUNT;
                            return "warhammer (1d8, Crit x3)";
                        }
                        break;
                    default:
                        if (CanWield(charClass, WEAPONFEAT.EXOTIC, "whip"))
                        {
                            type = WEAPONTYPE.SLASH;
                            return "whip (1d2, Crit x2)";
                        }
                        break;
                }
            }
        }

        private static String MeleeSpecialAbilities(POWER power, WEAPONTYPE type, ref Int32 bonusLimit)
        {
            var storedSpell = String.Empty;

            while (true)
            {
                Application.DoEvents();
                switch (power)
                {
                    case POWER.MINOR:
                        switch (Dice.Percentile())
                        {
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
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "flaming (1d6 fire)";
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
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "frost (1d6 ice)";
                                }
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
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "shock (1d6 lightning)";
                                }
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
                            case 55:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "ghost touch";
                                }
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
                                if (bonusLimit >= 1 && type == WEAPONTYPE.SLASH)
                                {
                                    bonusLimit--;
                                    return "keen";
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
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "mighty cleaving";
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
                                if (bonusLimit >= 1)
                                {
                                    if (Dice.Percentile() <= 50)
                                        storedSpell = String.Format(" (contains {0}", Scrolls.RandomSpell(3));
                                    bonusLimit--;
                                    return String.Format("spell storing{0}", storedSpell);
                                }
                                break;
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
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "throwing";
                                }
                                break;
                            case 100: bonusLimit++; break;
                            default:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "defending";
                                }
                                break;
                        }
                        break;
                    case POWER.MEDIUM:
                        switch (Dice.Percentile())
                        {
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "flaming (1d6 fire)";
                                }
                                break;
                            case 16:
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "frost (1d6 ice)";
                                }
                                break;
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                            case 25:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "shock (1d6 lightning)";
                                }
                                break;
                            case 26:
                            case 27:
                            case 28:
                            case 29:
                            case 30:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "ghost touch";
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
                                if (bonusLimit >= 1 && type == WEAPONTYPE.SLASH)
                                {
                                    bonusLimit--;
                                    return "keen";
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
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "mighty cleaving";
                                }
                                break;
                            case 51:
                                if (bonusLimit >= 1)
                                {
                                    if (Dice.Percentile() <= 50)
                                        storedSpell = String.Format(" (contains {0}", Scrolls.RandomSpell(3));
                                    bonusLimit--;
                                    return String.Format("spell storing{0}", storedSpell);
                                }
                                break;
                            case 52:
                            case 53:
                            case 54:
                            case 55:
                            case 56:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "throwing";
                                }
                                break;
                            case 57:
                            case 58:
                            case 59:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return String.Format("bane (versus {0}, +2 more & +2d6 damage)", Character.CreatureType());
                                }
                                break;
                            case 60:
                            case 61:
                            case 62:
                                if (bonusLimit >= 2 && type == WEAPONTYPE.BLUNT)
                                {
                                    bonusLimit -= 2;
                                    return "disruption (save DC 14)";
                                }
                                break;
                            case 63:
                            case 64:
                            case 65:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "flaming burst (1d6 fire, Crit +(multiplier-1)d10 fire)";
                                }
                                break;
                            case 66:
                            case 67:
                            case 68:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "icy burst (1d6 ice, Crit +(multiplier-1)d10 ice)";
                                }
                                break;
                            case 69:
                            case 70:
                            case 71:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "shocking burst (1d6 lightning, Crit +(multiplier-1)d10 lightning)";
                                }
                                break;
                            case 72:
                            case 73:
                            case 74:
                            case 75:
                            case 76:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "thundering (Crit +(multiplier-1)d8 sonic, save DC 14 or permanently deafened)";
                                }
                                break;
                            case 77:
                            case 78:
                            case 79:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "wounding";
                                }
                                break;
                            case 80:
                            case 81:
                            case 82:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "holy (2d6 good)";
                                }
                                break;
                            case 83:
                            case 84:
                            case 85:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "unholy (2d6 evil)";
                                }
                                break;
                            case 86:
                            case 87:
                            case 88:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "lawful (2d6 law)";
                                }
                                break;
                            case 89:
                            case 90:
                            case 91:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "chaotic (2d6 chaos)";
                                }
                                break;
                            case 92:
                                if (bonusLimit >= 4)
                                {
                                    bonusLimit -= 4;
                                    return "brilliant energy";
                                }
                                break;
                            case 93:
                                if (bonusLimit >= 4)
                                {
                                    bonusLimit -= 4;
                                    return "dancing";
                                }
                                break;
                            case 94:
                            case 95:
                                if (bonusLimit >= 4)
                                {
                                    bonusLimit -= 4;
                                    return "speed";
                                }
                                break;
                            case 96:
                            case 97:
                            case 98:
                            case 99:
                            case 100: bonusLimit++; break;
                            default:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "defending";
                                }
                                break;
                        }
                        break;
                    case POWER.MAJOR:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                            case 3:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "flaming (1d6 fire)";
                                }
                                break;
                            case 4:
                            case 5:
                            case 6:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "frost (1d6 ice)";
                                }
                                break;
                            case 7:
                            case 8:
                            case 9:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "shock (1d6 lightning)";
                                }
                                break;
                            case 10:
                            case 11:
                            case 12:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "ghost touch";
                                }
                                break;
                            case 13:
                            case 14:
                            case 15:
                            case 16:
                            case 17:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "mighty cleaving";
                                }
                                break;
                            case 18:
                            case 19:
                                if (bonusLimit >= 1)
                                {
                                    if (Dice.Percentile() <= 50)
                                        storedSpell = String.Format(" (contains {0}", Scrolls.RandomSpell(3));
                                    bonusLimit--;
                                    return String.Format("spell storing{0}", storedSpell);
                                }
                                break;
                            case 20:
                            case 21:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "throwing";
                                }
                                break;
                            case 22:
                            case 23:
                            case 24:
                            case 25:
                            case 26:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return String.Format("bane (versus {0}, +2 more & +2d6 damage)", Character.CreatureType());
                                }
                                break;
                            case 27:
                            case 28:
                            case 29:
                                if (bonusLimit >= 2 && type == WEAPONTYPE.BLUNT)
                                {
                                    bonusLimit -= 2;
                                    return "disruption (DC 14)";
                                }
                                break;
                            case 30:
                            case 31:
                            case 32:
                            case 33:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "flaming burst (1d6 fire, Crit +(multiplier-1)d10 fire)";
                                }
                                break;
                            case 34:
                            case 35:
                            case 36:
                            case 37:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "icy burst (1d6 ice, Crit +(multiplier-1)d10 ice)";
                                }
                                break;
                            case 38:
                            case 39:
                            case 40:
                            case 41:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "shocking burst (1d6 lightning, Crit +(multiplier-1)d10 lightning)";
                                }
                                break;
                            case 42:
                            case 43:
                            case 44:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "thundering (Crit (multiplier-1)d8 sonic, save DC 14 or deafened permanently)";
                                }
                                break;
                            case 45:
                            case 46:
                            case 47:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "wounding";
                                }
                                break;
                            case 48:
                            case 49:
                            case 50:
                            case 51:
                            case 52:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "holy (2d6 good)";
                                }
                                break;
                            case 53:
                            case 54:
                            case 55:
                            case 56:
                            case 57:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "unholy (2d6 evil)";
                                }
                                break;
                            case 58:
                            case 59:
                            case 60:
                            case 61:
                            case 62:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "lawful (2d6 law)";
                                }
                                break;
                            case 63:
                            case 64:
                            case 65:
                            case 66:
                            case 67:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "chaotic (2d6 chaos)";
                                }
                                break;
                            case 68:
                            case 69:
                            case 70:
                            case 71:
                                if (bonusLimit >= 4)
                                {
                                    bonusLimit -= 4;
                                    return "brilliant energy";
                                }
                                break;
                            case 72:
                            case 73:
                                if (bonusLimit >= 4)
                                {
                                    bonusLimit -= 4;
                                    return "dancing";
                                }
                                break;
                            case 74:
                            case 75:
                            case 76:
                                if (bonusLimit >= 4)
                                {
                                    bonusLimit -= 4;
                                    return "speed";
                                }
                                break;
                            case 77:
                            case 78:
                            case 79:
                            case 80:
                                if (bonusLimit >= 5 && type == WEAPONTYPE.SLASH)
                                {
                                    bonusLimit -= 5;
                                    return "vorpal";
                                }
                                break;
                            default: bonusLimit++; break;
                        }
                        break;
                    default: return "[ERROR: NONPOWERED MELEE WEAPON ABILITY.  Weapons.1630]";
                }
            }
        }

        private static String RangedSpecialAbilities(POWER power, ref Int32 bonusLimit)
        {
            while (true)
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
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "returning";
                                }
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
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "distance";
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
                                    return "flaming (1d6 fire)";
                                }
                                break;
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
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "shock (1d6 lightning)";
                                }
                                break;
                            default:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "frost (1d6 ice)";
                                }
                                break;
                        }
                        break;
                    case POWER.MEDIUM:
                        switch (Dice.Percentile())
                        {
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
                                    return "distance";
                                }
                                break;
                            case 31:
                            case 32:
                            case 33:
                            case 34:
                            case 35:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "flaming (1d6 fire)";
                                }
                                break;
                            case 36:
                            case 37:
                            case 38:
                            case 39:
                            case 40:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "shock (1d6 lightning)";
                                }
                                break;
                            case 41:
                            case 42:
                            case 43:
                            case 44:
                            case 45:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "frost (1d6 ice)";
                                }
                                break;
                            case 46:
                            case 47:
                            case 48:
                            case 49:
                            case 50:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "flaming burst (1d6 fire, Crit +(multiplier-1)d10 fire)";
                                }
                                break;
                            case 51:
                            case 52:
                            case 53:
                            case 54:
                            case 55:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "icy burst (1d6 ice, Crit +(multiplier-1)d10 ice)";
                                }
                                break;
                            case 56:
                            case 57:
                            case 58:
                            case 59:
                            case 60:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "shocking burst (1d6 lightning, Crit +(multiplier-1)d10 lightning)";
                                }
                                break;
                            case 61:
                            case 62:
                            case 63:
                            case 64:
                            case 65:
                            case 66:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return String.Format("bane (versus {0}, +2 more & +2d6 damage)", Character.CreatureType());
                                }
                                break;
                            case 67:
                            case 68:
                            case 69:
                            case 70:
                            case 71:
                            case 72:
                            case 73:
                            case 74:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "holy (2d6 good)";
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
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "unholy (2d6 evil)";
                                }
                                break;
                            case 83:
                            case 84:
                            case 85:
                            case 86:
                            case 87:
                            case 88:
                            case 89:
                            case 90:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "lawful (2d6 law)";
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
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "chaotic (2d6 chaos)";
                                }
                                break;
                            case 99:
                            case 100: bonusLimit++; break;
                            default:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "returning";
                                }
                                break;
                        }
                        break;
                    case POWER.MAJOR:
                        switch (Dice.Percentile())
                        {
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
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "shock (1d6 lightning)";
                                }
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
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "frost (1d6 ice)";
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
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "flaming burst (1d6 fire, Crit +(multiplier-1)d10 fire)";
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
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "icy burst (1d6 ice, Crit +(multiplier-1)d10 ice)";
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
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "shocking burst (1d6 lightning, Crit +(multiplier-1)d10 lightning)";
                                }
                                break;
                            case 61:
                            case 62:
                            case 63:
                            case 64:
                            case 65:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return String.Format("bane (versus {0}, +2 more & +2d6 damage)", Character.CreatureType());
                                }
                                break;
                            case 66:
                            case 67:
                            case 68:
                            case 69:
                            case 70:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "holy (2d6 good)";
                                }
                                break;
                            case 71:
                            case 72:
                            case 73:
                            case 74:
                            case 75:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "unholy (2d6 evil)";
                                }
                                break;
                            case 76:
                            case 77:
                            case 78:
                            case 79:
                            case 80:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "lawful (2d6 law)";
                                }
                                break;
                            case 81:
                            case 82:
                            case 83:
                            case 84:
                            case 85:
                                if (bonusLimit >= 2)
                                {
                                    bonusLimit -= 2;
                                    return "chaotic (2d6 chaos)";
                                }
                                break;
                            case 86:
                            case 87:
                            case 88:
                            case 89:
                            case 90:
                                if (bonusLimit >= 4)
                                {
                                    bonusLimit -= 4;
                                    return "speed";
                                }
                                break;
                            case 91:
                            case 92:
                            case 93:
                            case 94:
                            case 95:
                            case 96:
                            case 97:
                                if (bonusLimit >= 4)
                                {
                                    bonusLimit -= 4;
                                    return "brilliant energy";
                                }
                                break;
                            case 98:
                            case 99:
                            case 100: bonusLimit++; break;
                            default:
                                if (bonusLimit >= 1)
                                {
                                    bonusLimit--;
                                    return "flaming (1d6 fire)";
                                }
                                break;
                        }
                        break;
                    default: return "[ERROR: NONPOWERED RANGED WEAPON ABILITY.  Weapons.2100]";
                }
            }
        }

        private static String MeleeSpecialQualities(Int32 bonus)
        {
            if (bonus == 0)
                return String.Empty;

            var roll = Dice.Percentile();

            if (roll < 21)
                return "Sheds light";
            else if (roll < 26)
                return Intelligence.Generate(bonus);
            else if (roll < 36)
                return String.Format("Sheds light, {0}", Intelligence.Generate(bonus));
            else if (roll < 51)
                return "Markings";
            return String.Empty;
        }

        private static String RangedSpecialQualities(Int32 bonus, Boolean ammunition)
        {
            if (bonus == 0)
                return String.Empty;

            var roll = Dice.Percentile();

            if (roll < 6 && !ammunition)
                return Intelligence.Generate(bonus);
            else if (roll < 26 && roll > 5)
                return "Markings";
            return String.Empty;
        }

        private static String SpecificWeapon(CLASS charClass, POWER power, Int32 bonusLimit, Boolean allowTwoHanded, ref Boolean twoHanded, Boolean allowRanged, ref WEAPONRANGE range)
        {
            while (true)
            {
                Application.DoEvents();
                switch (power)
                {
                    case POWER.MEDIUM:
                        switch (Dice.Percentile())
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
                                if (CanWield(charClass, WEAPONFEAT.SIMPLE, "crossbow") && bonusLimit >= 3 && allowRanged)
                                {
                                    range = WEAPONRANGE.RANGE;
                                    return String.Format("Screaming Bolts (x{0}, {1})", Dice.Percentile() / 5 + 1, Ranged(charClass, "crossbow bolts"));
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
                            case 51:
                            case 52:
                            case 53:
                            case 54:
                            case 55:
                                if (CanWield(charClass, WEAPONFEAT.SIMPLE, "javelin") && bonusLimit >= 3 && allowRanged)
                                {
                                    range = WEAPONRANGE.RANGE;
                                    return String.Format("Javelin of Lightning (x{0})", Dice.Percentile() / 5 + 1);
                                }
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
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "bow") && allowRanged)
                                {
                                    range = WEAPONRANGE.RANGE;
                                    return String.Format("Slaying Arrows ({0}) (x{1}, {2})", Character.CreatureType(), Dice.Percentile() / 5 + 1, Ranged(charClass, "arrows"));
                                }
                                break;
                            case 66:
                            case 67:
                            case 68:
                            case 69:
                            case 70:
                                if (CanWield(charClass, WEAPONFEAT.SIMPLE, "dagger"))
                                    return "adamantine dagger";
                                break;
                            case 71:
                            case 72:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "trident"))
                                    return String.Format("Trident of Fish Command ({0} charges)", MagicItems.ChargesLeft(50, true));
                                break;
                            case 73:
                            case 74:
                                if (CanWield(charClass, WEAPONFEAT.SIMPLE, "dagger") && bonusLimit >= 2)
                                    return "Dagger of Venom";
                                break;
                            case 75:
                            case 76:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "battleaxe") && bonusLimit >= 2)
                                    return "adamantine battleaxe";
                                break;
                            case 77:
                            case 78:
                            case 79:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "trident") && bonusLimit >= 2)
                                    return "Trident of Warning";
                                break;
                            case 80:
                            case 81:
                            case 82:
                                if (CanWield(charClass, WEAPONFEAT.SIMPLE, "dagger") && bonusLimit >= 2)
                                    return "Assassin's Dagger";
                                break;
                            case 83:
                            case 84:
                            case 85:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "short sword") && bonusLimit >= 2)
                                    return "Sword of Subtlety";
                                break;
                            case 86:
                            case 87:
                            case 88:
                                if (CanWield(charClass, WEAPONFEAT.SIMPLE, "heavy mace") && bonusLimit >= 2)
                                    return "Mace of Terror";
                                break;
                            case 89:
                            case 90:
                            case 91:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "longsword") && bonusLimit >= 3)
                                    return String.Format("Nine Lives Stealer ({0} charges)", MagicItems.ChargesLeft(9, true));
                                break;
                            case 92:
                            case 93:
                            case 94:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "longbow") && bonusLimit >= 3 && allowRanged)
                                {
                                    range = WEAPONRANGE.RANGE;
                                    return String.Format("Oathbow (x{0} arrows)", Dice.Percentile() / 5 + 1);
                                }
                                break;
                            case 95:
                            case 96:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "longsword") && bonusLimit >= 3)
                                    return "Sword of Life Stealing";
                                break;
                            case 97:
                            case 98:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "longsword") && bonusLimit >= 4)
                                    return "Flame Tongue";
                                break;
                            case 99:
                            case 100:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "greataxe") && bonusLimit >= 4 && allowTwoHanded)
                                {
                                    twoHanded = true;
                                    return "Life-Drinker";
                                }
                                break;
                            default:
                                if (CanWield(charClass, WEAPONFEAT.SIMPLE, "bow") && bonusLimit >= 2 && allowRanged)
                                {
                                    range = WEAPONRANGE.RANGE;
                                    return String.Format("Sleep Arrows (x{0}, {1})", Dice.Percentile() / 5 + 1, Ranged(charClass, "arrows"));
                                }
                                break;
                        }
                        break;
                    case POWER.MAJOR:
                        switch (Dice.Percentile())
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                                if (CanWield(charClass, WEAPONFEAT.SIMPLE, "javelin") && bonusLimit >= 3 && allowRanged)
                                {
                                    range = WEAPONRANGE.RANGE;
                                    return String.Format("Javelin of Lightning (x{0})", Dice.Percentile() / 5 + 1);
                                }
                                break;
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "bow") && allowRanged)
                                {
                                    range = WEAPONRANGE.RANGE;
                                    return String.Format("Slaying Arrows ({0}) (x{1}, {2})", Character.CreatureType(), Dice.Percentile() / 5 + 1, Ranged(charClass, "arrows"));
                                }
                                break;
                            case 10:
                            case 11:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "trident"))
                                    return String.Format("Trident of Fish Command ({0} charges)", MagicItems.ChargesLeft(50, true));
                                break;
                            case 12:
                            case 13:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "bow") && allowRanged)
                                {
                                    range = WEAPONRANGE.RANGE;
                                    return String.Format("Greater Slaying Arrows ({0}) (x{1}, {2})", Character.CreatureType(), Dice.Percentile() / 5 + 1, Ranged(charClass, "arrows"));
                                }
                                break;
                            case 14:
                            case 15:
                            case 16:
                            case 17:
                                if (CanWield(charClass, WEAPONFEAT.SIMPLE, "dagger") && bonusLimit >= 2)
                                    return "Dagger of Venom";
                                break;
                            case 18:
                            case 19:
                            case 20:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "battleaxe") && bonusLimit >= 2)
                                    return "adamantine battleaxe";
                                break;
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                            case 25:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "trident") && bonusLimit >= 2)
                                    return "Trident of Warning";
                                break;
                            case 26:
                            case 27:
                            case 28:
                            case 29:
                            case 30:
                                if (CanWield(charClass, WEAPONFEAT.SIMPLE, "dagger") && bonusLimit >= 2)
                                    return "Assassin's Dagger";
                                break;
                            case 31:
                            case 32:
                            case 33:
                            case 34:
                            case 35:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "short sword") && bonusLimit >= 2)
                                    return "Sword of Subtlety";
                                break;
                            case 36:
                            case 37:
                            case 38:
                            case 39:
                            case 40:
                                if (CanWield(charClass, WEAPONFEAT.SIMPLE, "heavy mace") && bonusLimit >= 2)
                                    return "Mace of Terror";
                                break;
                            case 41:
                            case 42:
                            case 43:
                            case 44:
                            case 45:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "longsword") && bonusLimit >= 3)
                                    return String.Format("Nine Lives Stealer ({0} charges)", MagicItems.ChargesLeft(9, true));
                                break;
                            case 46:
                            case 47:
                            case 48:
                            case 49:
                            case 50:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "longbow") && bonusLimit >= 3 && allowRanged)
                                {
                                    range = WEAPONRANGE.RANGE;
                                    return String.Format("Oathbow (x{0} arrows)", Dice.Percentile() / 5 + 1);
                                }
                                break;
                            case 51:
                            case 52:
                            case 53:
                            case 54:
                            case 55:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "longsword") && bonusLimit >= 3)
                                    return "Sword of Life Stealing";
                                break;
                            case 56:
                            case 57:
                            case 58:
                            case 59:
                            case 60:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "longsword") && bonusLimit >= 4)
                                    return "Flame Tongue";
                                break;
                            case 61:
                            case 62:
                            case 63:
                            case 64:
                            case 65:
                            case 66:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "greataxe") && bonusLimit >= 4 && allowTwoHanded)
                                {
                                    twoHanded = true;
                                    return "Life-Drinker";
                                }
                                break;
                            case 67:
                            case 68:
                            case 69:
                            case 70:
                            case 71:
                            case 72:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "greatsword") && bonusLimit >= 4 && allowTwoHanded)
                                {
                                    twoHanded = true;
                                    return "Frost Brand";
                                }
                                break;
                            case 73:
                            case 74:
                            case 75:
                            case 76:
                            case 77:
                            case 78:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "rapier") && bonusLimit >= 5)
                                    return "Rapier of Puncturing";
                                break;
                            case 79:
                            case 80:
                            case 81:
                                if ((CanWield(charClass, WEAPONFEAT.MARTIAL, "short sword") || CanWield(charClass, WEAPONFEAT.EXOTIC, "bastard sword")) && bonusLimit >= 5)
                                    return "Sun Blade";
                                break;
                            case 82:
                            case 83:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "longsword") && bonusLimit >= 5)
                                    return "Sword of the Planes";
                                break;
                            case 84:
                            case 85:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "scimitar") && bonusLimit >= 5)
                                    return "Sylvan Scimitar";
                                break;
                            case 86:
                            case 87:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "warhammer") && bonusLimit >= 5)
                                    return "Dwarven Thrower";
                                break;
                            case 88:
                            case 89:
                            case 90:
                                if (CanWield(charClass, WEAPONFEAT.SIMPLE, "heavy mace") && bonusLimit >= 6)
                                    return "Mace of Smiting";
                                break;
                            case 91:
                            case 92:
                            case 93:
                            case 94:
                            case 95:
                            case 96:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "longsword") && bonusLimit >= 7)
                                    return "Holy Avenger";
                                break;
                            default:
                                if (CanWield(charClass, WEAPONFEAT.MARTIAL, "short sword") && bonusLimit >= 9)
                                    return String.Format("Luck Blade ({0} charges)", MagicItems.ChargesLeft(5, true));
                                break;
                        }
                        break;
                    default: return "ERROR: NONPOWERED OR MINOR SPECIFIC WEAPON.  Weapons.2493";
                }
            }
        }
    }
}
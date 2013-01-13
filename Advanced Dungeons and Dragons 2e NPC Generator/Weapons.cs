using System;
using System.Collections.Generic;
using System.Text;

namespace NPCGen
{
    class Weapons
    {
        public enum WEAPONTYPE { SLASH, PIERCE, BLUNT };
        public enum WEAPONFEAT { SIMPLE, MARTIAL, EXOTIC };
        public enum WEAPONRANGE { MELEE, RANGE };

        public static string Generate(int Bonus, Classes.CLASS Class, int Level, bool AllowTwoHanded, ref bool TwoHanded, ref Random random)
        {
            int OutputBonus = 0; int InputBonus = Bonus; string AbilitiesString = ""; int Roll;
            WEAPONTYPE Type = WEAPONTYPE.BLUNT; WEAPONRANGE Range = WEAPONRANGE.MELEE; bool Ammunition = false;
            string OutputBonusString; string SpecialQualitiesString; string output; string CursedString = "";
            
            bool AllowRange = true;
            if (!AllowTwoHanded)
                AllowRange = false;

            MagicItems.POWER Power = MagicItems.PowerByLevel(Level, ref random);
            string Weapon = WeaponType(Class, ref Type, AllowTwoHanded, ref TwoHanded, AllowRange, ref Range, ref Ammunition, ref random);

            if (Bonus > 0 && Dice.Percentile(ref random) <= 5)
                CursedString = CursedItem.Generate(ref random) + " ";

            while (InputBonus > 0)
            {
                switch (Power)
                {
                    case MagicItems.POWER.MEDIUM:
                        Roll = Dice.Percentile(ref random);
                        if (Roll > 68 && OutputBonus > 0)
                        {
                            if (AbilitiesString == "")
                                AbilitiesString += " of ";
                            else
                                AbilitiesString += ", ";
                            if (Range == WEAPONRANGE.MELEE)
                                AbilitiesString += MeleeSpecialAbilities(Power, Type, ref InputBonus, ref random);
                            else
                                AbilitiesString += RangedSpecialAbilities(Power, ref InputBonus, ref random);
                        }
                        else if (Roll > 62 && Roll <= 68)
                        {
                            Range = WEAPONRANGE.MELEE;
                            output = SpecificWeapon(Class, Power, Bonus, AllowTwoHanded, ref TwoHanded, AllowRange, ref Range, ref random);
                            if (Range == WEAPONRANGE.RANGE)
                                output += String.Format("\n{0}", Melee(Class, ref Type, AllowTwoHanded, ref TwoHanded, ref random));
                            return output;
                        }
                        else
                        {
                            OutputBonus++;
                            InputBonus--;
                        }
                        break;
                    case MagicItems.POWER.MAJOR:
                        Roll = Dice.Percentile(ref random);
                        if (Roll > 63 && OutputBonus > 0)
                        {
                            if (AbilitiesString == "")
                                AbilitiesString += " of ";
                            else
                                AbilitiesString += ", ";
                            if (Range == WEAPONRANGE.MELEE)
                                AbilitiesString += MeleeSpecialAbilities(Power, Type, ref InputBonus, ref random);
                            else
                                AbilitiesString += RangedSpecialAbilities(Power, ref InputBonus, ref random);
                        }
                        else if (Roll > 49 && Roll <= 63)
                        {
                            Range = WEAPONRANGE.MELEE;
                            output = SpecificWeapon(Class, Power, Bonus, AllowTwoHanded, ref TwoHanded, true, ref Range, ref random);
                            if (Range == WEAPONRANGE.RANGE)
                                output += String.Format("\n{0}", Melee(Class, ref Type, AllowTwoHanded, ref TwoHanded, ref random));
                            return output;
                        }
                        else
                        {
                            OutputBonus++;
                            InputBonus--;
                        }
                        break;
                    default:
                        if (Dice.Percentile(ref random) > 85 && OutputBonus > 0)
                        {
                            if (AbilitiesString == "")
                                AbilitiesString += " of ";
                            else
                                AbilitiesString += ", ";
                            if (Range == WEAPONRANGE.MELEE)
                                AbilitiesString += MeleeSpecialAbilities(Power, Type, ref InputBonus, ref random);
                            else
                                AbilitiesString += RangedSpecialAbilities(Power, ref InputBonus, ref random);
                        }
                        else
                        {
                            OutputBonus++;
                            InputBonus--;
                        }
                        break;
                }
            }

            OutputBonusString = "";
            if (OutputBonus > 0)
                OutputBonusString = String.Format("+{0} ", OutputBonus);

            if (Range == WEAPONRANGE.MELEE)
                SpecialQualitiesString = MeleeSpecialQualities(Bonus, ref random);
            else
                SpecialQualitiesString = RangedSpecialQualities(Bonus, Ammunition, ref random);

            if (SpecialQualitiesString != "")
                SpecialQualitiesString = String.Format(" ({0})", SpecialQualitiesString);

            output = String.Format("{0}{1}{2}{3}{4}", CursedString, OutputBonusString, Weapon, AbilitiesString, SpecialQualitiesString);
            if (Range == WEAPONRANGE.RANGE)
                output += String.Format("\n{0}", Melee(Class, ref Type, AllowTwoHanded, ref TwoHanded, ref random));

            return output;
        }

        private static bool CanWield(Classes.CLASS Class, WEAPONFEAT Feat, string Weapon)
        {
            switch (Class)
            {
                case Classes.CLASS.BARBARIAN:
                case Classes.CLASS.FIGHTER:
                case Classes.CLASS.RANGER:
                case Classes.CLASS.PALADIN: return true;
                case Classes.CLASS.BARD:
                    if (Feat != WEAPONFEAT.MARTIAL || Classes.BardWeapons.Contains(Weapon))
                        return true;
                    return false;
                case Classes.CLASS.CLERIC:
                case Classes.CLASS.SORCERER:
                    if (Feat != WEAPONFEAT.MARTIAL)
                        return true;
                    return false;
                case Classes.CLASS.DRUID:
                    if (Classes.DruidWeapons.Contains(Weapon))
                        return true;
                    return false;
                case Classes.CLASS.MONK:
                    if (Classes.MonkWeapons.Contains(Weapon))
                        return true;
                    return false;
                case Classes.CLASS.THIEF:
                    if (Classes.ThiefWeapons.Contains(Weapon))
                        return true;
                    return false;
                case Classes.CLASS.WIZARD:
                    if (Classes.WizardWeapons.Contains(Weapon))
                        return true;
                    return false;
                default: return false;
            }
        }

        private static string WeaponType(Classes.CLASS Class, ref WEAPONTYPE Type, bool AllowTwoHanded, ref bool TwoHanded, bool AllowRange, ref WEAPONRANGE Range, ref bool Ammunition, ref Random random)
        {
            int Roll = Dice.Percentile(ref random);

            Range = WEAPONRANGE.MELEE;
            if (Roll < 71)
                return Common(Class, ref Type, AllowTwoHanded, ref TwoHanded, ref random);
            if (Roll < 81)
                return Uncommon(Class, ref Type, AllowTwoHanded, ref TwoHanded, AllowRange, ref Range, ref random);

            if (AllowRange)
            {
                Range = WEAPONRANGE.RANGE;
                return Ranged(Class, ref Type, ref Ammunition, ref random);
            }
            else
                return Common(Class, ref Type, AllowTwoHanded, ref TwoHanded, ref random);
        }

        public static string Melee(Classes.CLASS Class, ref WEAPONTYPE Type, bool AllowTwoHanded, ref bool TwoHanded, ref Random random)
        {
            WEAPONRANGE Range = WEAPONRANGE.MELEE;
            
            if (Dice.d8(ref random) < 8)
                return Common(Class, ref Type, AllowTwoHanded, ref TwoHanded, ref random);
            return Uncommon(Class, ref Type, AllowTwoHanded, ref TwoHanded, false, ref Range, ref random);
        }

        private static string Ranged(Classes.CLASS Class, string AmmunitionType, ref Random random)
        {
            int Roll;
            
            while (true)
            {
                System.Windows.Forms.Application.DoEvents();
                switch (AmmunitionType)
                {
                    case "arrows":
                        Roll = Dice.Roll(1, 55, 0, ref random);
                        if (Roll > 50)
                            return "Mighty +4 composite longbow";
                        if (Roll > 45)
                            return "Mighty +3 composite longbow";
                        if (Roll > 40)
                            return "Mighty +2 composite longbow";
                        if (Roll > 35)
                            return "Mighty +1 composite longbow";
                        if (Roll > 30)
                            return "composite longbow";
                        if (Roll > 20)
                            return "longbow";
                        if (Roll > 15)
                            return "Mighty +2 composite shortbow";
                        if (Roll > 10)
                            return "Mighty +1 composite shortbow";
                        if (Roll > 5)
                            return "composite shortbow";
                        return "shortbow";
                    case "crossbow bolts":
                        if (Dice.d4(ref random) > 2)
                            return "heavy crossbow";
                        return "light crossbow";
                    case "sling bullets": return "sling";
                    default: return "[ERROR: Not a viable ammunition.  Weapons.227]";
                }
            }
        }

        public static string Ranged(Classes.CLASS Class, ref WEAPONTYPE Type, ref bool Ammunition, ref Random random)
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
                    case 10:
                        string ammo;
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "bow"))
                        {
                            ammo = "arrows";
                            Ammunition = true;
                            return String.Format("{0} (x{1}, {2})", ammo, Dice.Percentile(ref random) / 5 + 1, Ranged(Class, ammo, ref random));
                        }
                        else if (CanWield(Class, WEAPONFEAT.SIMPLE, "crossbow"))
                        {
                            ammo = "crossbow bolts";
                            Ammunition = true;
                            return String.Format("{0} (x{1}, {2})", ammo, Dice.Percentile(ref random) / 5 + 1, Ranged(Class, ammo, ref random));
                        }
                        else if (CanWield(Class, WEAPONFEAT.SIMPLE, "sling"))
                        {
                            ammo = "sling bullets";
                            Ammunition = true;
                            return String.Format("{0} (x{1}, {2})", ammo, Dice.Percentile(ref random) / 5 + 1, Ranged(Class, ammo, ref random));
                        }
                        break;
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "throwing axe"))
                        {
                            Type = WEAPONTYPE.SLASH;
                            return String.Format("throwing axe (x{0}, 1d6, Crit x2)", Dice.Percentile(ref random) / 5 + 1);
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
                        if (CanWield(Class, WEAPONFEAT.SIMPLE, "heavy crossbow"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return String.Format("heavy crossbow ({0} bolts, 1d10, Crit 19-20/x2)", Dice.Percentile(ref random) / 5 + 1);
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
                        if (CanWield(Class, WEAPONFEAT.SIMPLE, "light crossbow"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return String.Format("light crossbow ({0} bolts, 1d8, Crit 19-20/x2)", Dice.Percentile(ref random) / 5 + 1);
                        }
                        break;
                    case 36:
                    case 37:
                    case 38:
                    case 39:
                        if (CanWield(Class, WEAPONFEAT.SIMPLE, "dart"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return String.Format("dart (x{0}, 1d4, Crit x2)", Dice.Percentile(ref random) / 5 + 1);
                        }
                        break;
                    case 40:
                    case 41:
                        if (CanWield(Class, WEAPONFEAT.SIMPLE, "javelin"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return String.Format("javelin (x{0}, 1d6, Crit x2)", Dice.Percentile(ref random) / 5 + 1);
                        }
                        break;
                    case 42:
                    case 43:
                    case 44:
                    case 45:
                    case 46:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "shortbow"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return String.Format("shortbow ({0} arrows, 1d6, Crit x3)", Dice.Percentile(ref random) / 5 + 1);
                        }
                        break;
                    case 47:
                    case 48:
                    case 49:
                    case 50:
                    case 51:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "composite shortbow"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return String.Format("composite shortbow ({0} arrows, 1d6, Crit x3)", Dice.Percentile(ref random) / 5 + 1);
                        }
                        break;
                    case 52:
                    case 53:
                    case 54:
                    case 55:
                    case 56:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "composite shortbow"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return String.Format("Mighty +1 composite shortbow ({0} arrows, 1d6, Crit x3)", Dice.Percentile(ref random) / 5 + 1);
                        }
                        break;
                    case 57:
                    case 58:
                    case 59:
                    case 60:
                    case 61:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "composite shortbow"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return String.Format("Mighty +2 composite shortbow ({0} arrows, 1d6, Crit x3)", Dice.Percentile(ref random) / 5 + 1);
                        }
                        break;
                    case 62:
                    case 63:
                    case 64:
                    case 65:
                        if (CanWield(Class, WEAPONFEAT.SIMPLE, "sling"))
                        {
                            Type = WEAPONTYPE.BLUNT;
                            return String.Format("sling ({0} bullets, 1d4, Crit x2)", Dice.Percentile(ref random) / 5 + 1);
                        }
                        break;
                    case 76:
                    case 77:
                    case 78:
                    case 79:
                    case 80:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "composite longbow"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return String.Format("composite longbow ({0} arrows, 1d8, Crit x3)", Dice.Percentile(ref random) / 5 + 1);
                        }
                        break;
                    case 81:
                    case 82:
                    case 83:
                    case 84:
                    case 85:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "composite longbow"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return String.Format("Mighty +1 composite longbow ({0} arrows, 1d8, Crit x3)", Dice.Percentile(ref random) / 5 + 1);
                        }
                        break;
                    case 86:
                    case 87:
                    case 88:
                    case 89:
                    case 90:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "composite longbow"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return String.Format("Mighty +2 composite longbow ({0} arrows, 1d8, Crit x3)", Dice.Percentile(ref random) / 5 + 1);
                        }
                        break;
                    case 91:
                    case 92:
                    case 93:
                    case 94:
                    case 95:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "composite longbow"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return String.Format("Mighty +3 composite longbow ({0} arrows, 1d8, Crit x3)", Dice.Percentile(ref random) / 5 + 1);
                        }
                        break;
                    case 96:
                    case 97:
                    case 98:
                    case 99:
                    case 100:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "composite longbow"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return String.Format("Mighty +4 composite longbow ({0} arrows, 1d8, Crit x3)", Dice.Percentile(ref random) / 5 + 1);
                        }
                        break;
                    default:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "longbow"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return String.Format("longbow ({0} arrows)", Dice.Percentile(ref random) / 5 + 1);
                        }
                        break;
                }
            }
        }

        public static string Common(Classes.CLASS Class, ref WEAPONTYPE Type, bool AllowTwoHanded, ref bool TwoHanded, ref Random random)
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
                        if (CanWield(Class, WEAPONFEAT.SIMPLE, "dagger"))
                        {
                            Type = WEAPONTYPE.PIERCE;
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
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "greataxe") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.SLASH;
                            TwoHanded = true;
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
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "greatsword") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.SLASH;
                            TwoHanded = true;
                            return "greatsword (2d6, Crit 19-20/x2)";
                        }
                        break;
                    case 25:
                    case 26:
                    case 27:
                    case 28:
                        if (CanWield(Class, WEAPONFEAT.SIMPLE, "kama"))
                        {
                            Type = WEAPONTYPE.SLASH;
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
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "longsword"))
                        {
                            Type = WEAPONTYPE.SLASH;
                            return "longsword (1d8, Crit 19-20/x2)";
                        }
                        break;
                    case 42:
                    case 43:
                    case 44:
                    case 45:
                        if (CanWield(Class, WEAPONFEAT.SIMPLE, "light mace"))
                        {
                            Type = WEAPONTYPE.BLUNT;
                            return "light mace (1d6, Crit x2)";
                        }
                        break;
                    case 46:
                    case 47:
                    case 48:
                    case 49:
                    case 50:
                        if (CanWield(Class, WEAPONFEAT.SIMPLE, "heavy mace"))
                        {
                            Type = WEAPONTYPE.BLUNT;
                            return "heavy mace (1d8, Crit x2)";
                        }
                        break;
                    case 51:
                    case 52:
                    case 53:
                    case 54:
                        if (CanWield(Class, WEAPONFEAT.EXOTIC, "nunchaku"))
                        {
                            Type = WEAPONTYPE.BLUNT;
                            return "nunchaku (1d6, Crit x2)";
                        }
                        break;
                    case 55:
                    case 56:
                    case 57:
                        if (CanWield(Class, WEAPONFEAT.SIMPLE, "quarterstaff") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.BLUNT;
                            TwoHanded = true;
                            return "quarterstaff (1d6/1d6, Crit x2)";
                        }
                        break;
                    case 58:
                    case 59:
                    case 60:
                    case 61:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "rapier"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return "rapier (1d6, Crit 18-20/x2)";
                        }
                        break;
                    case 62:
                    case 63:
                    case 64:
                    case 65:
                    case 66:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "scimitar"))
                        {
                            Type = WEAPONTYPE.SLASH;
                            return "scimitar (1d6, Crit 18-20/x2)";
                        }
                        break;
                    case 67:
                    case 68:
                    case 69:
                    case 70:
                        if (CanWield(Class, WEAPONFEAT.SIMPLE, "shortspear"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return "shortspear (1d8, Crit x3)";
                        }
                        break;
                    case 71:
                    case 72:
                    case 73:
                    case 74:
                        if (CanWield(Class, WEAPONFEAT.EXOTIC, "siangham"))
                        {
                            Type = WEAPONTYPE.PIERCE;
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
                        if (CanWield(Class, WEAPONFEAT.EXOTIC, "bastard sword"))
                        {
                            Type = WEAPONTYPE.SLASH;
                            return "bastard sword (1d10, Crit 19-20/x2)";
                        }
                        break;
                    case 85:
                    case 86:
                    case 87:
                    case 88:
                    case 89:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "short sword"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return "short sword (1d6, Crit 19-20/x2)";
                        }
                        break;
                    default:
                        if (CanWield(Class, WEAPONFEAT.EXOTIC, "dwarven waraxe"))
                        {
                            Type = WEAPONTYPE.SLASH;
                            return "dwarven waraxe (1d10, Crit x3)";
                        }
                        break;
                }
            }
        }

        public static string Uncommon(Classes.CLASS Class, ref WEAPONTYPE Type, bool AllowTwoHanded, ref bool TwoHanded, bool AllowRanged, ref WEAPONRANGE Range, ref Random random)
        {
            Range = WEAPONRANGE.MELEE;

            while (true)
            {
                System.Windows.Forms.Application.DoEvents();
                switch (Dice.Percentile(ref random))
                {
                    case 1:
                    case 2:
                    case 3:
                        if (CanWield(Class, WEAPONFEAT.EXOTIC, "orc double axe") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.SLASH;
                            TwoHanded = true;
                            return "orc double axe (1d8/1d8, Crit x3)";
                        }
                        break;
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "battleaxe"))
                        {
                            Type = WEAPONTYPE.SLASH;
                            return "battleaxe (1d8, Crit x3)";
                        }
                        break;
                    case 8:
                    case 9:
                    case 10:
                        if (CanWield(Class, WEAPONFEAT.EXOTIC, "spiked chain") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.PIERCE;
                            TwoHanded = true;
                            return "spiked chain (2d4, Crit x2)";
                        }
                        break;
                    case 11:
                    case 12:
                        if (CanWield(Class, WEAPONFEAT.SIMPLE, "club"))
                        {
                            Type = WEAPONTYPE.BLUNT;
                            return "club (1d6, Crit x2)";
                        }
                        break;
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                        if (CanWield(Class, WEAPONFEAT.EXOTIC, "hand crossbow") && AllowRanged)
                        {
                            Type = WEAPONTYPE.PIERCE;
                            Range = WEAPONRANGE.RANGE;
                            return "hand crossbow (1d4, Crit 19-20/x2)";
                        }
                        break;
                    case 17:
                    case 18:
                    case 19:
                        if (CanWield(Class, WEAPONFEAT.EXOTIC, "repeating crossbow") && AllowRanged)
                        {
                            Type = WEAPONTYPE.PIERCE;
                            Range = WEAPONRANGE.RANGE;
                            return "repeating crossbow (1d8, Crit 19-20/x2)";
                        }
                        break;
                    case 20:
                    case 21:
                        if (CanWield(Class, WEAPONFEAT.SIMPLE, "punching dagger"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return "punching dagger (1d4, Crit x3)";
                        }
                        break;
                    case 22:
                    case 23:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "falchion") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.SLASH;
                            TwoHanded = true;
                            return "falchion (2d4, Crit 18-20/x2)";
                        }
                        break;
                    case 24:
                    case 25:
                    case 26:
                        if (CanWield(Class, WEAPONFEAT.EXOTIC, "dire flail") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.BLUNT;
                            TwoHanded = true;
                            return "dire flail (1d8/1d8, Crit x2)";
                        }
                        break;
                    case 27:
                    case 28:
                    case 29:
                    case 30:
                    case 31:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "heavy flail") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.BLUNT;
                            TwoHanded = true;
                            return "heavy flail (1d10, Crit 19-20/x2)";
                        }
                        break;
                    case 32:
                    case 33:
                    case 34:
                    case 35:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "light flail"))
                        {
                            Type = WEAPONTYPE.BLUNT;
                            return "light flail (1d8, Crit x2)";
                        }
                        break;
                    case 36:
                    case 37:
                        if (CanWield(Class, WEAPONFEAT.SIMPLE, "gauntlet") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.BLUNT;
                            TwoHanded = true;
                            return "gauntlet (Unarmed strike, Crit x2)";
                        }
                        break;
                    case 38:
                    case 39:
                        if (CanWield(Class, WEAPONFEAT.SIMPLE, "spiked gauntlet"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return "spiked gauntlet (1d4, Crit x2)";
                        }
                        break;
                    case 40:
                    case 41:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "glaive") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.SLASH;
                            TwoHanded = true;
                            return "glaive (1d10, Crit x3)";
                        }
                        break;
                    case 42:
                    case 43:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "greatclub") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.BLUNT;
                            TwoHanded = true;
                            return "greatclub (1d10, Crit x2)";
                        }
                        break;
                    case 44:
                    case 45:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "guisarme") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.SLASH;
                            TwoHanded = true;
                            return "guisarme (2d4, Crit x3)";
                        }
                        break;
                    case 46:
                    case 47:
                    case 48:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "halberd") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.SLASH;
                            TwoHanded = true;
                            return "halberd (1d10, Crit x3)";
                        }
                        break;
                    case 49:
                    case 50:
                    case 51:
                        if (CanWield(Class, WEAPONFEAT.SIMPLE, "halfspear") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.PIERCE;
                            TwoHanded = true;
                            return "halfspear (1d6, Crit x3)";
                        }
                        break;
                    case 52:
                    case 53:
                    case 54:
                        if (CanWield(Class, WEAPONFEAT.EXOTIC, "gnome hooked hammer") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.BLUNT;
                            TwoHanded = true;
                            return "gnome hooked hammer (1d6/1d4, Crit x3/x4)";
                        }
                        break;
                    case 55:
                    case 56:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "light hammer"))
                        {
                            Type = WEAPONTYPE.BLUNT;
                            return "light hammer (1d4, Crit x2)";
                        }
                        break;
                    case 57:
                    case 58:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "handaxe"))
                        {
                            Type = WEAPONTYPE.SLASH;
                            return "handaxe (1d6, Crit x3)";
                        }
                        break;
                    case 59:
                    case 60:
                    case 61:
                        if (CanWield(Class, WEAPONFEAT.EXOTIC, "kukri"))
                        {
                            Type = WEAPONTYPE.SLASH;
                            return "kukri (1d4, Crit 18-20/x2)";
                        }
                        break;
                    case 62:
                    case 63:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "heavy lance") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.PIERCE;
                            TwoHanded = true;
                            return "heavy lance (1d8, Crit x3)";
                        }
                        break;
                    case 64:
                    case 65:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "light lance") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.PIERCE;
                            TwoHanded = true;
                            return "light lance (1d6, Crit x3)";
                        }
                        break;
                    case 66:
                    case 67:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "longspear") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.PIERCE;
                            TwoHanded = true;
                            return "longspear (1d8, Crit x3)";
                        }
                        break;
                    case 68:
                    case 69:
                    case 70:
                        if (CanWield(Class, WEAPONFEAT.SIMPLE, "morningstar"))
                        {
                            Type = WEAPONTYPE.BLUNT;
                            return "morningstar (1d8, Crit x2)";
                        }
                        break;
                    case 71:
                    case 72:
                        if (CanWield(Class, WEAPONFEAT.EXOTIC, "net") && AllowRanged)
                        {
                            Type = WEAPONTYPE.BLUNT;
                            Range = WEAPONRANGE.RANGE;
                            return "net";
                        }
                        break;
                    case 73:
                    case 74:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "heavy pick"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return "heavy pick (1d6, Crit x4)";
                        }
                        break;
                    case 75:
                    case 76:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "light pick"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return "light pick (1d4, Crit x4)";
                        }
                        break;
                    case 77:
                    case 78:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "ranseur") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.PIERCE;
                            TwoHanded = true;
                            return "ranseur (2d4, Crit x3)";
                        }
                        break;
                    case 79:
                    case 80:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "sap"))
                        {
                            Type = WEAPONTYPE.BLUNT;
                            return "sap (1d6, Crit x2)";
                        }
                        break;
                    case 81:
                    case 82:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "scythe") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.SLASH;
                            TwoHanded = true;
                            return "scythe (2d4, Crit x4)";
                        }
                        break;
                    case 83:
                    case 84:
                        if (CanWield(Class, WEAPONFEAT.EXOTIC, "shuriken") && AllowRanged)
                        {
                            Type = WEAPONTYPE.PIERCE;
                            Range = WEAPONRANGE.RANGE;
                            return "shuriken (1, Crit x2)";
                        }
                        break;
                    case 85:
                    case 86:
                        if (CanWield(Class, WEAPONFEAT.SIMPLE, "sickle"))
                        {
                            Type = WEAPONTYPE.SLASH;
                            return "sickle (1d6, Crit x2)";
                        }
                        break;
                    case 87:
                    case 88:
                    case 89:
                        if (CanWield(Class, WEAPONFEAT.EXOTIC, "two-bladed sword") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.SLASH;
                            TwoHanded = true;
                            return "two-bladed sword (1d8/1d8, Crit 19-20/x2)";
                        }
                        break;
                    case 90:
                    case 91:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "trident"))
                        {
                            Type = WEAPONTYPE.PIERCE;
                            return "trident (1d8, Crit x2)";
                        }
                        break;
                    case 92:
                    case 93:
                    case 94:
                        if (CanWield(Class, WEAPONFEAT.EXOTIC, "dwarven urgrosh") && AllowTwoHanded)
                        {
                            Type = WEAPONTYPE.SLASH;
                            TwoHanded = true;
                            return "dwarven urgrosh (1d8/1d6, Crit x3)";
                        }
                        break;
                    case 95:
                    case 96:
                    case 97:
                        if (CanWield(Class, WEAPONFEAT.MARTIAL, "warhammer"))
                        {
                            Type = WEAPONTYPE.BLUNT;
                            return "warhammer (1d8, Crit x3)";
                        }
                        break;
                    default:
                        if (CanWield(Class, WEAPONFEAT.EXOTIC, "whip"))
                        {
                            Type = WEAPONTYPE.SLASH;
                            return "whip (1d2, Crit x2)";
                        }
                        break;
                }
            }
        }

        private static string MeleeSpecialAbilities(MagicItems.POWER Power, WEAPONTYPE Type, ref int BonusLimit, ref Random random)
        {
            string StoredSpell = "";

            while (true)
            {
                System.Windows.Forms.Application.DoEvents();
                switch (Power)
                {
                    case MagicItems.POWER.MINOR:
                        switch (Dice.Percentile(ref random))
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
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
                                if (BonusLimit >= 1 && Type == WEAPONTYPE.SLASH)
                                {
                                    BonusLimit--;
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
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
                                if (BonusLimit >= 1)
                                {
                                    if (Dice.Percentile(ref random) <= 50)
                                        StoredSpell = String.Format(" (contains {0}", Scrolls.RandomSpell(ref random, 3));
                                    BonusLimit--;
                                    return String.Format("spell storing{0}", StoredSpell);
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "throwing";
                                }
                                break;
                            case 100: BonusLimit++; break;
                            default:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "defending";
                                }
                                break;
                        }
                        break;
                    case MagicItems.POWER.MEDIUM:
                        switch (Dice.Percentile(ref random))
                        {
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "flaming (1d6 fire)";
                                }
                                break;
                            case 16:
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "frost (1d6 ice)";
                                }
                                break;
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                            case 25:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "shock (1d6 lightning)";
                                }
                                break;
                            case 26:
                            case 27:
                            case 28:
                            case 29:
                            case 30:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
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
                                if (BonusLimit >= 1 && Type == WEAPONTYPE.SLASH)
                                {
                                    BonusLimit--;
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "mighty cleaving";
                                }
                                break;
                            case 51:
                                if (BonusLimit >= 1)
                                {
                                    if (Dice.Percentile(ref random) <= 50)
                                        StoredSpell = String.Format(" (contains {0}", Scrolls.RandomSpell(ref random, 3));
                                    BonusLimit--;
                                    return String.Format("spell storing{0}", StoredSpell);
                                }
                                break;
                            case 52:
                            case 53:
                            case 54:
                            case 55:
                            case 56:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "throwing";
                                }
                                break;
                            case 57:
                            case 58:
                            case 59:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return String.Format("bane (versus {0}, +2 more & +2d6 damage)", Character.CreatureType(ref random));
                                }
                                break;
                            case 60:
                            case 61:
                            case 62:
                                if (BonusLimit >= 2 && Type == WEAPONTYPE.BLUNT)
                                {
                                    BonusLimit -= 2;
                                    return "disruption (save DC 14)";
                                }
                                break;
                            case 63:
                            case 64:
                            case 65:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "flaming burst (1d6 fire, Crit +(multiplier-1)d10 fire)";
                                }
                                break;
                            case 66:
                            case 67:
                            case 68:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "icy burst (1d6 ice, Crit +(multiplier-1)d10 ice)";
                                }
                                break;
                            case 69:
                            case 70:
                            case 71:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "shocking burst (1d6 lightning, Crit +(multiplier-1)d10 lightning)";
                                }
                                break;
                            case 72:
                            case 73:
                            case 74:
                            case 75:
                            case 76:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "thundering (Crit +(multiplier-1)d8 sonic, save DC 14 or permanently deafened)";
                                }
                                break;
                            case 77:
                            case 78:
                            case 79:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "wounding";
                                }
                                break;
                            case 80:
                            case 81:
                            case 82:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "holy (2d6 good)";
                                }
                                break;
                            case 83:
                            case 84:
                            case 85:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "unholy (2d6 evil)";
                                }
                                break;
                            case 86:
                            case 87:
                            case 88:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "lawful (2d6 law)";
                                }
                                break;
                            case 89:
                            case 90:
                            case 91:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "chaotic (2d6 chaos)";
                                }
                                break;
                            case 92:
                                if (BonusLimit >= 4)
                                {
                                    BonusLimit -= 4;
                                    return "brilliant energy";
                                }
                                break;
                            case 93:
                                if (BonusLimit >= 4)
                                {
                                    BonusLimit -= 4;
                                    return "dancing";
                                }
                                break;
                            case 94:
                            case 95:
                                if (BonusLimit >= 4)
                                {
                                    BonusLimit -= 4;
                                    return "speed";
                                }
                                break;
                            case 96:
                            case 97:
                            case 98:
                            case 99:
                            case 100: BonusLimit++; break;
                            default:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "defending";
                                }
                                break;
                        }
                        break;
                    case MagicItems.POWER.MAJOR:
                        switch (Dice.Percentile(ref random))
                        {
                            case 1:
                            case 2:
                            case 3:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "flaming (1d6 fire)";
                                }
                                break;
                            case 4:
                            case 5:
                            case 6:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "frost (1d6 ice)";
                                }
                                break;
                            case 7:
                            case 8:
                            case 9:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "shock (1d6 lightning)";
                                }
                                break;
                            case 10:
                            case 11:
                            case 12:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "ghost touch";
                                }
                                break;
                            case 13:
                            case 14:
                            case 15:
                            case 16:
                            case 17:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "mighty cleaving";
                                }
                                break;
                            case 18:
                            case 19:
                                if (BonusLimit >= 1)
                                {
                                    if (Dice.Percentile(ref random) <= 50)
                                        StoredSpell = String.Format(" (contains {0}", Scrolls.RandomSpell(ref random, 3));
                                    BonusLimit--;
                                    return String.Format("spell storing{0}", StoredSpell);
                                }
                                break;
                            case 20:
                            case 21:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "throwing";
                                }
                                break;
                            case 22:
                            case 23:
                            case 24:
                            case 25:
                            case 26:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return String.Format("bane (versus {0}, +2 more & +2d6 damage)", Character.CreatureType(ref random));
                                }
                                break;
                            case 27:
                            case 28:
                            case 29:
                                if (BonusLimit >= 2 && Type == WEAPONTYPE.BLUNT)
                                {
                                    BonusLimit -= 2;
                                    return "disruption (DC 14)";
                                }
                                break;
                            case 30:
                            case 31:
                            case 32:
                            case 33:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "flaming burst (1d6 fire, Crit +(multiplier-1)d10 fire)";
                                }
                                break;
                            case 34:
                            case 35:
                            case 36:
                            case 37:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "icy burst (1d6 ice, Crit +(multiplier-1)d10 ice)";
                                }
                                break;
                            case 38:
                            case 39:
                            case 40:
                            case 41:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "shocking burst (1d6 lightning, Crit +(multiplier-1)d10 lightning)";
                                }
                                break;
                            case 42:
                            case 43:
                            case 44:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "thundering (Crit (multiplier-1)d8 sonic, save DC 14 or deafened permanently)";
                                }
                                break;
                            case 45:
                            case 46:
                            case 47:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "wounding";
                                }
                                break;
                            case 48:
                            case 49:
                            case 50:
                            case 51:
                            case 52:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "holy (2d6 good)";
                                }
                                break;
                            case 53:
                            case 54:
                            case 55:
                            case 56:
                            case 57:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "unholy (2d6 evil)";
                                }
                                break;
                            case 58:
                            case 59:
                            case 60:
                            case 61:
                            case 62:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "lawful (2d6 law)";
                                }
                                break;
                            case 63:
                            case 64:
                            case 65:
                            case 66:
                            case 67:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "chaotic (2d6 chaos)";
                                }
                                break;
                            case 68:
                            case 69:
                            case 70:
                            case 71:
                                if (BonusLimit >= 4)
                                {
                                    BonusLimit -= 4;
                                    return "brilliant energy";
                                }
                                break;
                            case 72:
                            case 73:
                                if (BonusLimit >= 4)
                                {
                                    BonusLimit -= 4;
                                    return "dancing";
                                }
                                break;
                            case 74:
                            case 75:
                            case 76:
                                if (BonusLimit >= 4)
                                {
                                    BonusLimit -= 4;
                                    return "speed";
                                }
                                break;
                            case 77:
                            case 78:
                            case 79:
                            case 80:
                                if (BonusLimit >= 5 && Type == WEAPONTYPE.SLASH)
                                {
                                    BonusLimit -= 5;
                                    return "vorpal";
                                }
                                break;
                            default: BonusLimit++; break;
                        }
                        break;
                    default: return "[ERROR: NONPOWERED MELEE WEAPON ABILITY.  Weapons.1630]";
                }
            }
        }

        private static string RangedSpecialAbilities(MagicItems.POWER Power, ref int BonusLimit, ref Random random)
        {
            while (true)
            {
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "shock (1d6 lightning)";
                                }
                                break;
                            default:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "frost (1d6 ice)";
                                }
                                break;
                        }
                        break;
                    case MagicItems.POWER.MEDIUM:
                        switch (Dice.Percentile(ref random))
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "distance";
                                }
                                break;
                            case 31:
                            case 32:
                            case 33:
                            case 34:
                            case 35:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "flaming (1d6 fire)";
                                }
                                break;
                            case 36:
                            case 37:
                            case 38:
                            case 39:
                            case 40:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "shock (1d6 lightning)";
                                }
                                break;
                            case 41:
                            case 42:
                            case 43:
                            case 44:
                            case 45:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "frost (1d6 ice)";
                                }
                                break;
                            case 46:
                            case 47:
                            case 48:
                            case 49:
                            case 50:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "flaming burst (1d6 fire, Crit +(multiplier-1)d10 fire)";
                                }
                                break;
                            case 51:
                            case 52:
                            case 53:
                            case 54:
                            case 55:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "icy burst (1d6 ice, Crit +(multiplier-1)d10 ice)";
                                }
                                break;
                            case 56:
                            case 57:
                            case 58:
                            case 59:
                            case 60:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "shocking burst (1d6 lightning, Crit +(multiplier-1)d10 lightning)";
                                }
                                break;
                            case 61:
                            case 62:
                            case 63:
                            case 64:
                            case 65:
                            case 66:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return String.Format("bane (versus {0}, +2 more & +2d6 damage)", Character.CreatureType(ref random));
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
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
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
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
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
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
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
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "chaotic (2d6 chaos)";
                                }
                                break;
                            case 99:
                            case 100: BonusLimit++; break;
                            default:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "returning";
                                }
                                break;
                        }
                        break;
                    case MagicItems.POWER.MAJOR:
                        switch (Dice.Percentile(ref random))
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
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
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
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
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
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
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
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
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "shocking burst (1d6 lightning, Crit +(multiplier-1)d10 lightning)";
                                }
                                break;
                            case 61:
                            case 62:
                            case 63:
                            case 64:
                            case 65:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return String.Format("bane (versus {0}, +2 more & +2d6 damage)", Character.CreatureType(ref random));
                                }
                                break;
                            case 66:
                            case 67:
                            case 68:
                            case 69:
                            case 70:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "holy (2d6 good)";
                                }
                                break;
                            case 71:
                            case 72:
                            case 73:
                            case 74:
                            case 75:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "unholy (2d6 evil)";
                                }
                                break;
                            case 76:
                            case 77:
                            case 78:
                            case 79:
                            case 80:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "lawful (2d6 law)";
                                }
                                break;
                            case 81:
                            case 82:
                            case 83:
                            case 84:
                            case 85:
                                if (BonusLimit >= 2)
                                {
                                    BonusLimit -= 2;
                                    return "chaotic (2d6 chaos)";
                                }
                                break;
                            case 86:
                            case 87:
                            case 88:
                            case 89:
                            case 90:
                                if (BonusLimit >= 4)
                                {
                                    BonusLimit -= 4;
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
                                if (BonusLimit >= 4)
                                {
                                    BonusLimit -= 4;
                                    return "brilliant energy";
                                }
                                break;
                            case 98:
                            case 99:
                            case 100: BonusLimit++; break;
                            default:
                                if (BonusLimit >= 1)
                                {
                                    BonusLimit--;
                                    return "flaming (1d6 fire)";
                                }
                                break;
                        }
                        break;
                    default: return "[ERROR: NONPOWERED RANGED WEAPON ABILITY.  Weapons.2100]";
                }
            }
        }

        private static string MeleeSpecialQualities(int Bonus, ref Random random)
        {
            if (Bonus > 0)
            {
                int Roll = Dice.Percentile(ref random);

                if (Roll < 21)
                    return "Sheds light";
                else if (Roll < 26)
                    return Intelligence.Generate(Bonus, ref random);
                else if (Roll < 36)
                    return String.Format("Sheds light, {0}", Intelligence.Generate(Bonus, ref random));
                else if (Roll < 51)
                    return "Markings";
            }
            return "";
        }

        private static string RangedSpecialQualities(int Bonus, bool Ammunition, ref Random random)
        {
            if (Bonus > 0)
            {
                int Roll = Dice.Percentile(ref random);

                if (Roll < 6 && !Ammunition)
                    return Intelligence.Generate(Bonus, ref random);
                else if (Roll < 26 && Roll > 5)
                    return "Markings";
            }
            return "";
        }

        private static string SpecificWeapon(Classes.CLASS Class, MagicItems.POWER Power, int BonusLimit, bool AllowTwoHanded, ref bool TwoHanded, bool AllowRanged, ref WEAPONRANGE Range, ref Random random)
        {
            while (true)
            {
                System.Windows.Forms.Application.DoEvents();
                switch (Power)
                {
                    case MagicItems.POWER.MEDIUM:
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
                                if (CanWield(Class, WEAPONFEAT.SIMPLE, "crossbow") && BonusLimit >= 3 && AllowRanged)
                                {
                                    Range = WEAPONRANGE.RANGE;
                                    return String.Format("Screaming Bolts (x{0}, {1})", Dice.Percentile(ref random) / 5 + 1, Ranged(Class, "crossbow bolts", ref random));
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
                                if (CanWield(Class, WEAPONFEAT.SIMPLE, "javelin") && BonusLimit >= 3 && AllowRanged)
                                {
                                    Range = WEAPONRANGE.RANGE;
                                    return String.Format("Javelin of Lightning (x{0})", Dice.Percentile(ref random) / 5 + 1);
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
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "bow") && AllowRanged)
                                {
                                    Range = WEAPONRANGE.RANGE;
                                    return String.Format("Slaying Arrows ({0}) (x{1}, {2})", Character.CreatureType(ref random), Dice.Percentile(ref random) / 5 + 1, Ranged(Class, "arrows", ref random));
                                }
                                break;
                            case 66:
                            case 67:
                            case 68:
                            case 69:
                            case 70:
                                if (CanWield(Class, WEAPONFEAT.SIMPLE, "dagger"))
                                    return "adamantine dagger";
                                break;
                            case 71:
                            case 72:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "trident"))
                                    return String.Format("Trident of Fish Command ({0} charges)", MagicItems.ChargesLeft(ref random, 50, true));
                                break;
                            case 73:
                            case 74:
                                if (CanWield(Class, WEAPONFEAT.SIMPLE, "dagger") && BonusLimit >= 2)
                                    return "Dagger of Venom";
                                break;
                            case 75:
                            case 76:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "battleaxe") && BonusLimit >= 2)
                                    return "adamantine battleaxe";
                                break;
                            case 77:
                            case 78:
                            case 79:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "trident") && BonusLimit >= 2)
                                    return "Trident of Warning";
                                break;
                            case 80:
                            case 81:
                            case 82:
                                if (CanWield(Class, WEAPONFEAT.SIMPLE, "dagger") && BonusLimit >= 2)
                                    return "Assassin's Dagger";
                                break;
                            case 83:
                            case 84:
                            case 85:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "short sword") && BonusLimit >= 2)
                                    return "Sword of Subtlety";
                                break;
                            case 86:
                            case 87:
                            case 88:
                                if (CanWield(Class, WEAPONFEAT.SIMPLE, "heavy mace") && BonusLimit >= 2)
                                    return "Mace of Terror";
                                break;
                            case 89:
                            case 90:
                            case 91:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "longsword") && BonusLimit >= 3)
                                    return String.Format("Nine Lives Stealer ({0} charges)", MagicItems.ChargesLeft(ref random, 9, true));
                                break;
                            case 92:
                            case 93:
                            case 94:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "longbow") && BonusLimit >= 3 && AllowRanged)
                                {
                                    Range = WEAPONRANGE.RANGE;
                                    return String.Format("Oathbow (x{0} arrows)", Dice.Percentile(ref random) / 5 + 1);
                                }
                                break;
                            case 95:
                            case 96:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "longsword") && BonusLimit >= 3)
                                    return "Sword of Life Stealing";
                                break;
                            case 97:
                            case 98:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "longsword") && BonusLimit >= 4)
                                    return "Flame Tongue";
                                break;
                            case 99:
                            case 100:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "greataxe") && BonusLimit >= 4 && AllowTwoHanded)
                                {
                                    TwoHanded = true;
                                    return "Life-Drinker";
                                }
                                break;
                            default:
                                if (CanWield(Class, WEAPONFEAT.SIMPLE, "bow") && BonusLimit >= 2 && AllowRanged)
                                {
                                    Range = WEAPONRANGE.RANGE;
                                    return String.Format("Sleep Arrows (x{0}, {1})", Dice.Percentile(ref random) / 5 + 1, Ranged(Class, "arrows", ref random));
                                }
                                break;
                        }
                        break;
                    case MagicItems.POWER.MAJOR:
                        switch (Dice.Percentile(ref random))
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                                if (CanWield(Class, WEAPONFEAT.SIMPLE, "javelin") && BonusLimit >= 3 && AllowRanged)
                                {
                                    Range = WEAPONRANGE.RANGE;
                                    return String.Format("Javelin of Lightning (x{0})", Dice.Percentile(ref random) / 5 + 1);
                                }
                                break;
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "bow") && AllowRanged)
                                {
                                    Range = WEAPONRANGE.RANGE;
                                    return String.Format("Slaying Arrows ({0}) (x{1}, {2})", Character.CreatureType(ref random), Dice.Percentile(ref random) / 5 + 1, Ranged(Class, "arrows", ref random));
                                }
                                break;
                            case 10:
                            case 11:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "trident"))
                                    return String.Format("Trident of Fish Command ({0} charges)", MagicItems.ChargesLeft(ref random, 50, true));
                                break;
                            case 12:
                            case 13:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "bow") && AllowRanged)
                                {
                                    Range = WEAPONRANGE.RANGE;
                                    return String.Format("Greater Slaying Arrows ({0}) (x{1}, {2})", Character.CreatureType(ref random), Dice.Percentile(ref random) / 5 + 1, Ranged(Class, "arrows", ref random));
                                }
                                break;
                            case 14:
                            case 15:
                            case 16:
                            case 17:
                                if (CanWield(Class, WEAPONFEAT.SIMPLE, "dagger") && BonusLimit >= 2)
                                    return "Dagger of Venom";
                                break;
                            case 18:
                            case 19:
                            case 20:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "battleaxe") && BonusLimit >= 2)
                                    return "adamantine battleaxe";
                                break;
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                            case 25:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "trident") && BonusLimit >= 2)
                                    return "Trident of Warning";
                                break;
                            case 26:
                            case 27:
                            case 28:
                            case 29:
                            case 30:
                                if (CanWield(Class, WEAPONFEAT.SIMPLE, "dagger") && BonusLimit >= 2)
                                    return "Assassin's Dagger";
                                break;
                            case 31:
                            case 32:
                            case 33:
                            case 34:
                            case 35:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "short sword") && BonusLimit >= 2)
                                    return "Sword of Subtlety";
                                break;
                            case 36:
                            case 37:
                            case 38:
                            case 39:
                            case 40:
                                if (CanWield(Class, WEAPONFEAT.SIMPLE, "heavy mace") && BonusLimit >= 2)
                                    return "Mace of Terror";
                                break;
                            case 41:
                            case 42:
                            case 43:
                            case 44:
                            case 45:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "longsword") && BonusLimit >= 3)
                                    return String.Format("Nine Lives Stealer ({0} charges)", MagicItems.ChargesLeft(ref random, 9, true));
                                break;
                            case 46:
                            case 47:
                            case 48:
                            case 49:
                            case 50:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "longbow") && BonusLimit >= 3 && AllowRanged)
                                {
                                    Range = WEAPONRANGE.RANGE;
                                    return String.Format("Oathbow (x{0} arrows)", Dice.Percentile(ref random) / 5 + 1);
                                }
                                break;
                            case 51:
                            case 52:
                            case 53:
                            case 54:
                            case 55:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "longsword") && BonusLimit >= 3)
                                    return "Sword of Life Stealing";
                                break;
                            case 56:
                            case 57:
                            case 58:
                            case 59:
                            case 60:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "longsword") && BonusLimit >= 4)
                                    return "Flame Tongue";
                                break;
                            case 61:
                            case 62:
                            case 63:
                            case 64:
                            case 65:
                            case 66:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "greataxe") && BonusLimit >= 4 && AllowTwoHanded)
                                {
                                    TwoHanded = true;
                                    return "Life-Drinker";
                                }
                                break;
                            case 67:
                            case 68:
                            case 69:
                            case 70:
                            case 71:
                            case 72:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "greatsword") && BonusLimit >= 4 && AllowTwoHanded)
                                {
                                    TwoHanded = true;
                                    return "Frost Brand";
                                }
                                break;
                            case 73:
                            case 74:
                            case 75:
                            case 76:
                            case 77:
                            case 78:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "rapier") && BonusLimit >= 5)
                                    return "Rapier of Puncturing";
                                break;
                            case 79:
                            case 80:
                            case 81:
                                if ((CanWield(Class, WEAPONFEAT.MARTIAL, "short sword") || CanWield(Class, WEAPONFEAT.EXOTIC, "bastard sword")) && BonusLimit >= 5)
                                    return "Sun Blade";
                                break;
                            case 82:
                            case 83:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "longsword") && BonusLimit >= 5)
                                    return "Sword of the Planes";
                                break;
                            case 84:
                            case 85:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "scimitar") && BonusLimit >= 5)
                                    return "Sylvan Scimitar";
                                break;
                            case 86:
                            case 87:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "warhammer") && BonusLimit >= 5)
                                    return "Dwarven Thrower";
                                break;
                            case 88:
                            case 89:
                            case 90:
                                if (CanWield(Class, WEAPONFEAT.SIMPLE, "heavy mace") && BonusLimit >= 6)
                                    return "Mace of Smiting";
                                break;
                            case 91:
                            case 92:
                            case 93:
                            case 94:
                            case 95:
                            case 96:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "longsword") && BonusLimit >= 7)
                                    return "Holy Avenger";
                                break;
                            default:
                                if (CanWield(Class, WEAPONFEAT.MARTIAL, "short sword") && BonusLimit >= 9)
                                    return String.Format("Luck Blade ({0} charges)", MagicItems.ChargesLeft(ref random, 5, true));
                                break;
                        }
                        break;
                    default: return "ERROR: NONPOWERED OR MINOR SPECIFIC WEAPON.  Weapons.2493";
                }
            }
        }
    }
}
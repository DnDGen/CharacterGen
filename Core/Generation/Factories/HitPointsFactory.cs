using System;
using D20Dice.Dice;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;

namespace NPCGen.Core.Generation.Factories
{
    public static class HitPointsFactory
    {
        public static Int32 CreateUsing(IDice dice, CharacterClass characterClass, Int32 constitutionBonus, Race race)
        {
            var hitPoints = 0;

            for (var i = 0; i < characterClass.Level; i++)
            {
                var rolledHitPoints = RollHitPoints(dice, characterClass.ClassName, constitutionBonus);
                hitPoints += Math.Max(rolledHitPoints, 1);
            }

            return hitPoints;
        }

        private static Int32 RollHitPoints(IDice dice, String className, Int32 constitutionBonus)
        {
            switch (className)
            {
                case CharacterClassConstants.Fighter:
                case CharacterClassConstants.Paladin: return dice.d10(bonus: constitutionBonus);
                case CharacterClassConstants.Barbarian: return dice.d12(bonus: constitutionBonus);
                case CharacterClassConstants.Cleric:
                case CharacterClassConstants.Druid:
                case CharacterClassConstants.Monk:
                case CharacterClassConstants.Ranger: return dice.d8(bonus: constitutionBonus);
                case CharacterClassConstants.Bard:
                case CharacterClassConstants.Rogue: return dice.d6(bonus: constitutionBonus);
                case CharacterClassConstants.Sorcerer:
                case CharacterClassConstants.Wizard: return dice.d4(bonus: constitutionBonus);
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
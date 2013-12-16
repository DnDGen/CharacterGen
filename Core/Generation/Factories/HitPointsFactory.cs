using System;
using D20Dice;
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

            hitPoints += GetAdditionalMonsterHitDice(race, dice);

            return hitPoints;
        }

        private static Int32 RollHitPoints(IDice dice, String className, Int32 constitutionBonus)
        {
            switch (className)
            {
                case CharacterClassConstants.Fighter:
                case CharacterClassConstants.Paladin: return dice.d10() + constitutionBonus;
                case CharacterClassConstants.Barbarian: return dice.d12() + constitutionBonus;
                case CharacterClassConstants.Cleric:
                case CharacterClassConstants.Druid:
                case CharacterClassConstants.Monk:
                case CharacterClassConstants.Ranger: return dice.d8() + constitutionBonus;
                case CharacterClassConstants.Bard:
                case CharacterClassConstants.Rogue: return dice.d6() + constitutionBonus;
                case CharacterClassConstants.Sorcerer:
                case CharacterClassConstants.Wizard: return dice.d4() + constitutionBonus;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private static Int32 GetAdditionalMonsterHitDice(Race race, IDice dice)
        {
            var numberOfRolls = GetNumberOfAdditionalMonsterHitDice(race);

            if (race.Metarace == RaceConstants.Metaraces.HalfDragon)
                return dice.d10(numberOfRolls);

            return dice.d8(numberOfRolls);
        }

        private static Int32 GetNumberOfAdditionalMonsterHitDice(Race race)
        {
            switch (race.BaseRace)
            {
                case RaceConstants.BaseRaces.Bugbear:
                case RaceConstants.BaseRaces.Derro: return 3;
                case RaceConstants.BaseRaces.Ogre:
                case RaceConstants.BaseRaces.Doppelganger: return 4;
                case RaceConstants.BaseRaces.Gnoll:
                case RaceConstants.BaseRaces.Lizardfolk:
                case RaceConstants.BaseRaces.Troglodyte: return 2;
                case RaceConstants.BaseRaces.MindFlayer: return 8;
                case RaceConstants.BaseRaces.Minotaur: return 6;
                case RaceConstants.BaseRaces.OgreMage: return 5;
                default: return 0;
            }
        }
    }
}
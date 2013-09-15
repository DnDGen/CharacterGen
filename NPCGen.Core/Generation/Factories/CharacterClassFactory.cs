using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.Classes;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClass;
using NPCGen.Core.Generation.Randomizers.Level;
using System;

namespace NPCGen.Core.Generation.Factories
{
    public class CharacterClassFactory : ICharacterClassFactory
    {
        public ILevelRandomizer LevelRandomizer { get; set; }
        public IClassRandomizer ClassRandomizer { get; set; }

        private IDice dice;

        public CharacterClassFactory(IDice dice, ILevelRandomizer levelRandomizer,
            IClassRandomizer classRandomizer)
        {
            this.dice = dice;
            LevelRandomizer = levelRandomizer;
            ClassRandomizer = classRandomizer;
        }

        public CharacterClass Generate(Alignment alignment, Int32 constitutionBonus)
        {
            var characterClass = new CharacterClass();

            characterClass.Level = LevelRandomizer.Randomize();
            characterClass.ClassName = ClassRandomizer.Randomize(alignment);
            characterClass.BaseAttack.BaseAttackBonus = GetBaseAttackBonus(characterClass);
            characterClass.HitPoints = GetHitPoints(characterClass, constitutionBonus);

            return characterClass;
        }

        private Int32 GetBaseAttackBonus(CharacterClass characterClass)
        {
            switch (characterClass.ClassName)
            {
                case ClassConstants.FIGHTER:
                case ClassConstants.PALADIN:
                case ClassConstants.RANGER:
                case ClassConstants.BARBARIAN: return GetGoodBaseAttackBonus(characterClass.Level);
                case ClassConstants.BARD:
                case ClassConstants.CLERIC:
                case ClassConstants.MONK:
                case ClassConstants.ROGUE:
                case ClassConstants.DRUID: return GetAverageBaseAttackBonus(characterClass.Level);
                case ClassConstants.SORCERER:
                case ClassConstants.WIZARD: return GetPoorBaseAttackBonus(characterClass.Level);
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private Int32 GetGoodBaseAttackBonus(Int32 level)
        {
            return level;
        }

        private Int32 GetAverageBaseAttackBonus(Int32 level)
        {
            var rootAttack = 0;

            for (var i = level; i > 1; i--)
                if ((i - 1) % 4 != 0)
                    rootAttack++;

            return rootAttack;
        }

        private Int32 GetPoorBaseAttackBonus(Int32 level)
        {
            return level / 2;
        }

        private Int32 GetHitPoints(CharacterClass characterClass, Int32 constitutionBonus)
        {
            var hitPoints = 0;

            for (var i = 0; i < characterClass.Level; i++)
            {
                var addedHitPoints = ClassHitPoints(characterClass.ClassName, constitutionBonus);
                if (addedHitPoints < 1)
                    addedHitPoints = 1;

                hitPoints += addedHitPoints;
            }

            return hitPoints;
        }

        private Int32 ClassHitPoints(String className, Int32 constitutionBonus)
        {
            switch (className)
            {
                case ClassConstants.FIGHTER:
                case ClassConstants.PALADIN: return dice.d10(bonus: constitutionBonus);
                case ClassConstants.BARBARIAN: return dice.d12(bonus: constitutionBonus);
                case ClassConstants.CLERIC:
                case ClassConstants.DRUID:
                case ClassConstants.MONK:
                case ClassConstants.RANGER: return dice.d8(bonus: constitutionBonus);
                case ClassConstants.BARD:
                case ClassConstants.ROGUE: return dice.d6(bonus: constitutionBonus);
                case ClassConstants.SORCERER:
                case ClassConstants.WIZARD: return dice.d4(bonus: constitutionBonus);
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
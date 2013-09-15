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
        public ICharacterClassRandomizer CharacterClassRandomizer { get; set; }

        private IDice dice;

        public CharacterClassFactory(IDice dice, ILevelRandomizer levelRandomizer,
            ICharacterClassRandomizer classRandomizer)
        {
            this.dice = dice;
            LevelRandomizer = levelRandomizer;
            CharacterClassRandomizer = classRandomizer;
        }

        public CharacterClass Generate(Alignment alignment, Int32 constitutionBonus)
        {
            var characterClass = new CharacterClass();

            characterClass.Level = LevelRandomizer.Randomize();
            characterClass.ClassName = CharacterClassRandomizer.Randomize(alignment);
            characterClass.BaseAttack = GetBaseAttack(characterClass);
            characterClass.HitPoints = GetHitPoints(characterClass, constitutionBonus);

            return characterClass;
        }

        private BaseAttack GetBaseAttack(CharacterClass characterClass)
        {
            var baseAttack = new BaseAttack();
            baseAttack.BaseAttackBonus = GetBaseAttackBonus(characterClass);
            return baseAttack;
        }

        private Int32 GetBaseAttackBonus(CharacterClass characterClass)
        {
            switch (characterClass.ClassName)
            {
                case ClassConstants.Fighter:
                case ClassConstants.Paladin:
                case ClassConstants.Ranger:
                case ClassConstants.Barbarian: return GetGoodBaseAttackBonus(characterClass.Level);
                case ClassConstants.Bard:
                case ClassConstants.Cleric:
                case ClassConstants.Monk:
                case ClassConstants.Rogue:
                case ClassConstants.Druid: return GetAverageBaseAttackBonus(characterClass.Level);
                case ClassConstants.Sorcerer:
                case ClassConstants.Wizard: return GetPoorBaseAttackBonus(characterClass.Level);
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private Int32 GetGoodBaseAttackBonus(Int32 level)
        {
            return level;
        }

        private Int32 GetAverageBaseAttackBonus(Int32 level)
        {
            return level * 3 / 4;
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
                var rolledHitPoints = RollHitPoints(characterClass.ClassName, constitutionBonus);
                hitPoints += Math.Max(rolledHitPoints, 1);
            }

            return hitPoints;
        }

        private Int32 RollHitPoints(String className, Int32 constitutionBonus)
        {
            switch (className)
            {
                case ClassConstants.Fighter:
                case ClassConstants.Paladin: return dice.d10(bonus: constitutionBonus);
                case ClassConstants.Barbarian: return dice.d12(bonus: constitutionBonus);
                case ClassConstants.Cleric:
                case ClassConstants.Druid:
                case ClassConstants.Monk:
                case ClassConstants.Ranger: return dice.d8(bonus: constitutionBonus);
                case ClassConstants.Bard:
                case ClassConstants.Rogue: return dice.d6(bonus: constitutionBonus);
                case ClassConstants.Sorcerer:
                case ClassConstants.Wizard: return dice.d4(bonus: constitutionBonus);
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
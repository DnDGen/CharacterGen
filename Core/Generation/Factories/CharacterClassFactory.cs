using System;
using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NPCGen.Core.Generation.Randomizers.Level;

namespace NPCGen.Core.Generation.Factories
{
    public class CharacterClassFactory : ICharacterClassFactory
    {
        public ILevelRandomizer LevelRandomizer { get; set; }
        public IClassNameRandomizer CharacterClassRandomizer { get; set; }

        private IDice dice;

        public CharacterClassFactory(IDice dice, ILevelRandomizer levelRandomizer,
            IClassNameRandomizer classRandomizer)
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
                case CharacterClassConstants.Fighter:
                case CharacterClassConstants.Paladin:
                case CharacterClassConstants.Ranger:
                case CharacterClassConstants.Barbarian: return GetGoodBaseAttackBonus(characterClass.Level);
                case CharacterClassConstants.Bard:
                case CharacterClassConstants.Cleric:
                case CharacterClassConstants.Monk:
                case CharacterClassConstants.Rogue:
                case CharacterClassConstants.Druid: return GetAverageBaseAttackBonus(characterClass.Level);
                case CharacterClassConstants.Sorcerer:
                case CharacterClassConstants.Wizard: return GetPoorBaseAttackBonus(characterClass.Level);
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
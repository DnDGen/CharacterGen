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

        private IBaseAttackFactory baseAttackFactory;
        private IDice dice;

        public CharacterClassFactory(IDice dice, IBaseAttackFactory baseAttackFactory)
        {
            this.dice = dice;
            this.baseAttackFactory = baseAttackFactory;
        }

        public override CharacterClass Generate(Alignment alignment, Int32 constitutionBonus)
        {
            var characterClass = new CharacterClass();

            characterClass.Level = LevelRandomizer.Randomize();
            characterClass.ClassName = ClassRandomizer.Randomize(alignment);
            characterClass.BaseAttack = baseAttackFactory.Generate(characterClass);
            characterClass.HitPoints = GetHitPoints(characterClass, constitutionBonus);

            throw new NotImplementedException();
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
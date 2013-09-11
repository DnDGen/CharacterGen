using D20Dice.Dice;
using NPCGen.Core.Characters.Data;
using NPCGen.Core.Characters.Data.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPCGen.Core.Characters.Generation.Randomizers.CharacterClass
{
    public class AnyClass : IClassRandomizer
    {
        private IDice dice;

        public AnyClass(IDice dice)
        {
            this.dice = dice;
        }

        public String Randomize(Alignment alignment)
        {
            var characterClass = String.Empty;

            do
            {
                if (alignment.Goodness == 1)
                    characterClass = GoodClass();
                else if (alignment.Goodness == 0)
                    characterClass = NeutralClass();
                else if (alignment.Goodness == -1)
                    characterClass = EvilClass();
            } while (!ClassIsAllowed(characterClass, alignment));
            
            return characterClass;
        }

        private String GoodClass()
        {
            var roll = dice.Percentile();

            if (roll <= 5)
                return ClassConstants.BARBARIAN;
            else if (roll <= 10)
                return ClassConstants.BARD;
            else if (roll <= 30)
                return ClassConstants.CLERIC;
            else if (roll <= 35)
                return ClassConstants.DRUID;
            else if (roll <= 45)
                return ClassConstants.FIGHTER;
            else if (roll <= 50)
                return ClassConstants.MONK;
            else if (roll <= 55)
                return ClassConstants.PALADIN;
            else if (roll <= 65)
                return ClassConstants.RANGER;
            else if (roll <= 75)
                return ClassConstants.THIEF;
            else if (roll <= 80)
                return ClassConstants.SORCERER;

            return ClassConstants.WIZARD;
        }

        private String NeutralClass()
        {            
            var roll = dice.Percentile();

            if (roll <= 5)
                return ClassConstants.BARBARIAN;
            else if (roll <= 10)
                return ClassConstants.BARD;
            else if (roll <= 15)
                return ClassConstants.CLERIC;
            else if (roll <= 25)
                return ClassConstants.DRUID;
            else if (roll <= 45)
                return ClassConstants.FIGHTER;
            else if (roll <= 50)
                return ClassConstants.MONK;
            else if (roll <= 55)
                return ClassConstants.RANGER;
            else if (roll <= 75)
                return ClassConstants.THIEF;
            else if (roll <= 80)
                return ClassConstants.SORCERER;

            return ClassConstants.WIZARD;
        }

        private String EvilClass()
        {
            var roll = dice.Percentile();

            if (roll <= 10)
                return ClassConstants.BARBARIAN;
            else if (roll <= 15)
                return ClassConstants.BARD;
            else if (roll <= 35)
                return ClassConstants.CLERIC;
            else if (roll <= 40)
                return ClassConstants.DRUID;
            else if (roll <= 50)
                return ClassConstants.FIGHTER;
            else if (roll <= 55)
                return ClassConstants.MONK;
            else if (roll <= 60)
                return ClassConstants.RANGER;
            else if (roll <= 80)
                return ClassConstants.THIEF;
            else if (roll <= 85)
                return ClassConstants.SORCERER;
            
            return ClassConstants.WIZARD;
        }

        private Boolean ClassIsAllowed(String characterClass, Alignment alignment)
        {
            switch (characterClass)
            {
                case ClassConstants.BARBARIAN:
                case ClassConstants.BARD: return !alignment.IsLawful();
                case ClassConstants.DRUID: return alignment.IsNeutral();
                case ClassConstants.MONK:
                case ClassConstants.PALADIN: return alignment.IsLawful();
                case ClassConstants.FIGHTER:
                case ClassConstants.CLERIC:
                case ClassConstants.RANGER:
                case ClassConstants.SORCERER:
                case ClassConstants.THIEF:
                case ClassConstants.WIZARD: return true;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
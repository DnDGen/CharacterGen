using D20Dice.Dice;
using NPCGen.Core.Data;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.Classes;
using System;

namespace NPCGen.Core.Generation.Randomizers.CharacterClass
{
    public abstract class BaseClassRandomizer : IClassRandomizer
    {
        private IDice dice;

        public BaseClassRandomizer(IDice dice)
        {
            this.dice = dice;
        }

        public String Randomize(Alignment alignment)
        {
            var className = String.Empty;

            do
            {
                if (alignment.Goodness == 1)
                    className = GoodClass();
                else if (alignment.Goodness == 0)
                    className = NeutralClass();
                else if (alignment.Goodness == -1)
                    className = EvilClass();
            } while (!ClassIsAllowed(className, alignment));

            return className;
        }

        private String GoodClass()
        {
            var roll = dice.Percentile();

            if (roll <= 5)
                return ClassConstants.Barbarian;
            else if (roll <= 10)
                return ClassConstants.Bard;
            else if (roll <= 30)
                return ClassConstants.Cleric;
            else if (roll <= 35)
                return ClassConstants.Druid;
            else if (roll <= 45)
                return ClassConstants.Fighter;
            else if (roll <= 50)
                return ClassConstants.Monk;
            else if (roll <= 55)
                return ClassConstants.Paladin;
            else if (roll <= 65)
                return ClassConstants.Ranger;
            else if (roll <= 75)
                return ClassConstants.Rogue;
            else if (roll <= 80)
                return ClassConstants.Sorcerer;

            return ClassConstants.Wizard;
        }

        private String NeutralClass()
        {
            var roll = dice.Percentile();

            if (roll <= 5)
                return ClassConstants.Barbarian;
            else if (roll <= 10)
                return ClassConstants.Bard;
            else if (roll <= 15)
                return ClassConstants.Cleric;
            else if (roll <= 25)
                return ClassConstants.Druid;
            else if (roll <= 45)
                return ClassConstants.Fighter;
            else if (roll <= 50)
                return ClassConstants.Monk;
            else if (roll <= 55)
                return ClassConstants.Ranger;
            else if (roll <= 75)
                return ClassConstants.Rogue;
            else if (roll <= 80)
                return ClassConstants.Sorcerer;

            return ClassConstants.Wizard;
        }

        private String EvilClass()
        {
            var roll = dice.Percentile();

            if (roll <= 10)
                return ClassConstants.Barbarian;
            else if (roll <= 15)
                return ClassConstants.Bard;
            else if (roll <= 35)
                return ClassConstants.Cleric;
            else if (roll <= 40)
                return ClassConstants.Druid;
            else if (roll <= 50)
                return ClassConstants.Fighter;
            else if (roll <= 55)
                return ClassConstants.Monk;
            else if (roll <= 60)
                return ClassConstants.Ranger;
            else if (roll <= 80)
                return ClassConstants.Rogue;
            else if (roll <= 85)
                return ClassConstants.Sorcerer;

            return ClassConstants.Wizard;
        }

        protected abstract Boolean ClassIsAllowed(String className, Alignment alignment);
    }
}
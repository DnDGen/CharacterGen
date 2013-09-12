using System;

namespace NPCGen.Core.Characters.Data.Classes
{
    public class CharacterClass
    {
        public Int32 HitPoints { get; set; }
        public Int32 Level { get; set; }
        public BaseAttack BaseAttack { get; set; }
        public String ClassName { get; set; }

        public Boolean IsWarrior()
        {
            switch (ClassName)
            {
                case ClassConstants.BARBARIAN:
                case ClassConstants.FIGHTER:
                case ClassConstants.MONK:
                case ClassConstants.PALADIN:
                case ClassConstants.RANGER: return true;
                default: return false;
            }
        }

        public Boolean IsHealer()
        {
            switch (ClassName)
            {
                case ClassConstants.BARD:
                case ClassConstants.CLERIC:
                case ClassConstants.DRUID:
                case ClassConstants.PALADIN:
                case ClassConstants.RANGER: return true;
                default: return false;
            }
        }

        public Boolean IsRogue()
        {
            switch (ClassName)
            {
                case ClassConstants.BARD:
                case ClassConstants.RANGER:
                case ClassConstants.ROGUE: return true;
                default: return false;
            }
        }

        public Boolean IsMage()
        {
            switch (ClassName)
            {
                case ClassConstants.BARD:
                case ClassConstants.RANGER:
                case ClassConstants.SORCERER:
                case ClassConstants.WIZARD: return true;
                default: return false;
            }
        }

        public Boolean IsSpellcaster()
        {
            return IsMage() || IsHealer();
        }
    }
}
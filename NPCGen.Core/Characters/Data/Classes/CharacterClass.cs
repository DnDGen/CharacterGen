using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPCGen.Core.Characters.Data.Classes
{
    public class CharacterClass
    {
        public Int32 HitPoints { get; set; }
        public Int32 Level { get; set; }
        public Int32 BaseAttack { get; set; }
        public String ClassName { get; set; }

        public Boolean IsCombatant()
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
                case ClassConstants.THIEF: return true;
                default: return false;
            }
        }

        public Boolean IsMage()
        {
            switch (ClassName)
            {
                case ClassConstants.BARD:
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
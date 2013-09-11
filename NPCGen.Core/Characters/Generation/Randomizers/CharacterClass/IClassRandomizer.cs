using NPCGen.Core.Characters.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPCGen.Core.Characters.Generation.Randomizers.CharacterClass
{
    public enum CLASSRANDOMIZER { ANY, ANY_FIGHTER, ANY_SPELLCASTER, ANY_MAGE, ANY_HEALER, ANY_NONSPELLCASTER, ANY_ROGUE };

    public interface IClassRandomizer
    {
        String Randomize(Alignment alignment);
    }
}
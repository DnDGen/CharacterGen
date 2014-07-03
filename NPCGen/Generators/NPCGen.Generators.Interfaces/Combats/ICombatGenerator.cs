using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;

namespace NPCGen.Generators.Interfaces.Combats
{
    public interface ICombatGenerator
    {
        Combat GenerateWith(CharacterClass characterClass, IEnumerable<Feat> feats, Dictionary<String, Stat> stats);
    }
}
using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Feats;
using NPCGen.Common.Races;
using NPCGen.Common.Stats;

namespace NPCGen.Generators.Interfaces
{
    public interface IFeatsGenerator
    {
        IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats);
    }
}
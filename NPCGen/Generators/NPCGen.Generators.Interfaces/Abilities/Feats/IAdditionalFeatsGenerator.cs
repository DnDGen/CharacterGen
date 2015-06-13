using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Races;

namespace NPCGen.Generators.Interfaces.Abilities.Feats
{
    public interface IAdditionalFeatsGenerator
    {
        IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats, Dictionary<String, Skill> skills,
            BaseAttack baseAttack, IEnumerable<Feat> preselectedFeats);
    }
}
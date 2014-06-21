using NPCGen.Generators.Interfaces.Randomizers.Races;
using System;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;

namespace NPCGen.Generators.Interfaces
{
    public interface IRaceGenerator
    {
        Race GenerateWith(String goodnessString, CharacterClassPrototype prototype, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer);
    }
}
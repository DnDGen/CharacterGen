using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;

namespace NPCGen.Core.Generation.Factories.Interfaces
{
    public interface IRaceFactory
    {
        IBaseRaceRandomizer BaseRaceRandomizer { get; }
        IMetaraceRandomizer MetaraceRandomizer { get; }

        Race Generate(Alignment alignment, CharacterClass characterClass);
    }
}
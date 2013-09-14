using NPCGen.Core.Characters.Data.Alignments;
using NPCGen.Core.Characters.Data.Classes;
using NPCGen.Core.Characters.Data.Races;
using NPCGen.Core.Characters.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Characters.Generation.Randomizers.Races.Metaraces;

namespace NPCGen.Core.Characters.Generation.Factories.Interfaces
{
    public interface IRaceFactory
    {
        IBaseRaceRandomizer BaseRaceRandomizer { get; }
        IMetaraceRandomizer MetaraceRandomizer { get; }

        Race Generate(Alignment alignment, CharacterClass characterClass);
    }
}
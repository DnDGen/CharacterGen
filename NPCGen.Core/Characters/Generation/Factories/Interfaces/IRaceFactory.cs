using NPCGen.Core.Characters.Data;
using NPCGen.Core.Characters.Data.Classes;
using NPCGen.Core.Characters.Data.Races;
using NPCGen.Core.Characters.Generation.Randomizers.Races;

namespace NPCGen.Core.Characters.Generation.Factories.Interfaces
{
    public interface IRaceFactory
    {
        IRaceRandomizer RaceRandomizer { get; }
        IMetaraceRandomizer MetaraceRandomizer { get; }

        Race Generate(Alignment alignment, CharacterClass characterClass);
    }
}
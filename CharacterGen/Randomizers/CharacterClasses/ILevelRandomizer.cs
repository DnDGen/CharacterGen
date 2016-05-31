using System.Collections.Generic;

namespace CharacterGen.Randomizers.CharacterClasses
{
    public interface ILevelRandomizer
    {
        int Randomize();
        IEnumerable<int> GetAllPossibleResults();
    }
}
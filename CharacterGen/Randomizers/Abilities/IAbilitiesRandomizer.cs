using CharacterGen.Abilities;
using System.Collections.Generic;

namespace CharacterGen.Randomizers.Abilities
{
    public interface IAbilitiesRandomizer
    {
        Dictionary<string, Ability> Randomize();
    }
}
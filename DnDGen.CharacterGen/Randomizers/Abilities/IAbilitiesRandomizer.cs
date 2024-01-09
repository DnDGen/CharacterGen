using DnDGen.CharacterGen.Abilities;
using System.Collections.Generic;

namespace DnDGen.CharacterGen.Randomizers.Abilities
{
    public interface IAbilitiesRandomizer
    {
        Dictionary<string, Ability> Randomize();
    }
}
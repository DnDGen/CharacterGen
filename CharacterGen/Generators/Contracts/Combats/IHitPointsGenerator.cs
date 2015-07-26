using System;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;

namespace CharacterGen.Generators.Combats
{
    public interface IHitPointsGenerator
    {
        Int32 GenerateWith(CharacterClass characterClass, Int32 constitutionBonus, Race race);
    }
}
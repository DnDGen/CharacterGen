using System;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;

namespace NPCGen.Generators.Interfaces.Combats
{
    public interface IHitPointsGenerator
    {
        Int32 GenerateWith(CharacterClass characterClass, Int32 constitutionBonus, Race race);
    }
}
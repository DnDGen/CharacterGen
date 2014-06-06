using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using System;

namespace NPCGen.Core.Generation.Factories.Interfaces
{
    public interface IHitPointsFactory
    {
        Int32 CreateWith(CharacterClass characterClass, Int32 constitutionBonus, Race race);
    }
}
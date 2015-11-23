using CharacterGen.Common;
using System;

namespace CharacterGen.Generators
{
    public interface ILeadershipGenerator
    {
        Leadership GenerateLeadership(Int32 level, Int32 charismaBonus, String leaderAnimal);
        Character GenerateCohort(Int32 cohortScore, Int32 leaderLevel, String leaderAlignment);
        Character GenerateFollower(Int32 level, String leaderAlignment);
    }
}

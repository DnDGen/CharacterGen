using DnDGen.CharacterGen.Characters;

namespace DnDGen.CharacterGen.Leaders
{
    public interface ILeadershipGenerator
    {
        Leadership GenerateLeadership(int level, int charismaBonus, string leaderAnimal);
        Character GenerateCohort(int cohortScore, int leaderLevel, string leaderAlignment, string leaderClassName);
        Character GenerateFollower(int level, string leaderAlignment, string leaderClassName);
    }
}

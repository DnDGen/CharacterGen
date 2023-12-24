using DnDGen.CharacterGen.Leaders;

namespace DnDGen.CharacterGen.Selectors.Collections
{
    internal interface ILeadershipSelector
    {
        int SelectCohortLevelFor(int leadershipScore);
        FollowerQuantities SelectFollowerQuantitiesFor(int leadershipScore);
    }
}

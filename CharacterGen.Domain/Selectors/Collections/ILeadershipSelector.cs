namespace CharacterGen.Domain.Selectors.Collections
{
    internal interface ILeadershipSelector
    {
        int SelectCohortLevelFor(int leadershipScore);
        FollowerQuantities SelectFollowerQuantitiesFor(int leadershipScore);
    }
}

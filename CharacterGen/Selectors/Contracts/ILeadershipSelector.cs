using System;
using CharacterGen.Selectors.Objects;

namespace CharacterGen.Selectors
{
    public interface ILeadershipSelector
    {
        Int32 SelectCohortLevelFor(Int32 leadershipScore);
        FollowerQuantities SelectFollowerQuantitiesFor(Int32 leadershipScore);
    }
}

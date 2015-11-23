using CharacterGen.Common;
using System;

namespace CharacterGen.Selectors
{
    public interface ILeadershipSelector
    {
        Int32 SelectCohortLevelFor(Int32 leadershipScore);
        FollowerQuantities SelectFollowerQuantitiesFor(Int32 leadershipScore);
    }
}

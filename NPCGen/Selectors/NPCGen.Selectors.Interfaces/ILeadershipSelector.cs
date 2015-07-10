using System;
using NPCGen.Selectors.Interfaces.Objects;

namespace NPCGen.Selectors.Interfaces
{
    public interface ILeadershipSelector
    {
        Int32 SelectCohortLevelFor(Int32 leadershipScore);
        FollowerQuantities SelectFollowerQuantitiesFor(Int32 leadershipScore);
    }
}

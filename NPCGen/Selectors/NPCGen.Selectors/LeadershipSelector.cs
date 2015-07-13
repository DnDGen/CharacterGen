using System;
using NPCGen.Selectors.Interfaces;
using NPCGen.Selectors.Interfaces.Objects;

namespace NPCGen.Selectors
{
    public class LeadershipSelector : ILeadershipSelector
    {
        public Int32 SelectCohortLevelFor(Int32 leadershipScore)
        {
            throw new NotImplementedException();
        }

        public FollowerQuantities SelectFollowerQuantitiesFor(Int32 leadershipScore)
        {
            throw new NotImplementedException();
        }
    }
}

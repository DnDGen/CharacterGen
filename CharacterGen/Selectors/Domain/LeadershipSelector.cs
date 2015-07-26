using CharacterGen.Selectors.Objects;
using System;

namespace CharacterGen.Selectors.Domain
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

using CharacterGen.Selectors.Objects;
using CharacterGen.Tables;
using System;
using System.Linq;

namespace CharacterGen.Selectors.Domain
{
    public class LeadershipSelector : ILeadershipSelector
    {
        private IAdjustmentsSelector adjustmentsSelector;

        public LeadershipSelector(IAdjustmentsSelector adjustmentsSelector)
        {
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public Int32 SelectCohortLevelFor(Int32 leadershipScore)
        {
            return GetAppropriateAdjustment(leadershipScore, TableNameConstants.Set.Adjustments.CohortLevels);
        }

        public FollowerQuantities SelectFollowerQuantitiesFor(Int32 leadershipScore)
        {
            var followerQuantities = new FollowerQuantities();

            followerQuantities.Level1 = GetQuantityOfFollowersAtLevel(1, leadershipScore);
            followerQuantities.Level2 = GetQuantityOfFollowersAtLevel(2, leadershipScore);
            followerQuantities.Level3 = GetQuantityOfFollowersAtLevel(3, leadershipScore);
            followerQuantities.Level4 = GetQuantityOfFollowersAtLevel(4, leadershipScore);
            followerQuantities.Level5 = GetQuantityOfFollowersAtLevel(5, leadershipScore);
            followerQuantities.Level6 = GetQuantityOfFollowersAtLevel(6, leadershipScore);

            return followerQuantities;
        }

        private Int32 GetQuantityOfFollowersAtLevel(Int32 level, Int32 leadershipScore)
        {
            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, level);
            return GetAppropriateAdjustment(leadershipScore, tableName);
        }

        private Int32 GetAppropriateAdjustment(Int32 leadershipScore, String tableName)
        {
            var adjustments = adjustmentsSelector.SelectFrom(tableName);
            var numericScores = adjustments.Keys.Select(k => Convert.ToInt32(k));
            var maxScore = numericScores.Max();

            if (leadershipScore > maxScore)
                return adjustments[maxScore.ToString()];

            var minScore = numericScores.Min();

            if (leadershipScore < minScore)
                return 0;

            return adjustments[leadershipScore.ToString()];
        }
    }
}

using CharacterGen.Common;
using CharacterGen.Common.Alignments;
using CharacterGen.Generators.Randomizers.Alignments;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Randomizers.Stats;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain
{
    public class LeadershipGenerator : ILeadershipGenerator
    {
        private ICharacterGenerator characterGenerator;
        private ILeadershipSelector leadershipSelector;
        private IPercentileSelector percentileSelector;
        private IAdjustmentsSelector adjustmentsSelector;
        private ISetLevelRandomizer setLevelRandomizer;
        private ISetAlignmentRandomizer setAlignmentRandomizer;
        private IAlignmentRandomizer anyAlignmentRandomizer;
        private IClassNameRandomizer anyClassNameRandomizer;
        private RaceRandomizer anyBaseRaceRandomizer;
        private RaceRandomizer anyMetaraceRandomizer;
        private IStatsRandomizer rawStatsRandomizer;
        private IBooleanPercentileSelector booleanPercentileSelector;
        private ICollectionsSelector collectionsSelector;
        private IAlignmentGenerator alignmentGenerator;
        private Generator generator;

        public LeadershipGenerator(ICharacterGenerator characterGenerator, ILeadershipSelector leadershipSelector, IPercentileSelector percentileSelector,
            IAdjustmentsSelector adjustmentsSelector, ISetLevelRandomizer setLevelRandomizer, ISetAlignmentRandomizer setAlignmentRandomizer,
            IAlignmentRandomizer anyAlignmentRandomizer, IClassNameRandomizer anyClassNameRandomizer, RaceRandomizer anyBaseRaceRandomizer,
            RaceRandomizer anyMetaraceRandomizer, IStatsRandomizer rawStatsRandomizer, IBooleanPercentileSelector booleanPercentileSelector,
            ICollectionsSelector collectionsSelector, IAlignmentGenerator alignmentGenerator, Generator generator)
        {
            this.characterGenerator = characterGenerator;
            this.leadershipSelector = leadershipSelector;
            this.percentileSelector = percentileSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.setLevelRandomizer = setLevelRandomizer;
            this.setAlignmentRandomizer = setAlignmentRandomizer;
            this.anyAlignmentRandomizer = anyAlignmentRandomizer;
            this.anyClassNameRandomizer = anyClassNameRandomizer;
            this.anyBaseRaceRandomizer = anyBaseRaceRandomizer;
            this.anyMetaraceRandomizer = anyMetaraceRandomizer;
            this.rawStatsRandomizer = rawStatsRandomizer;
            this.booleanPercentileSelector = booleanPercentileSelector;
            this.collectionsSelector = collectionsSelector;
            this.alignmentGenerator = alignmentGenerator;
            this.generator = generator;
        }

        public Leadership GenerateLeadership(Int32 level, Int32 charismaBonus, String leaderAnimal)
        {
            var leadership = new Leadership();
            leadership.Score = level + charismaBonus;

            var leadershipModifiers = new List<String>();
            var reputation = percentileSelector.SelectFrom(TableNameConstants.Set.Percentile.Reputation);
            var leadershipAdjustments = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LeadershipModifiers);

            if (String.IsNullOrEmpty(reputation) == false)
            {
                leadershipModifiers.Add(reputation);
                leadership.Score += leadershipAdjustments[reputation];
            }

            leadership.CohortScore = leadership.Score;
            var cohortDeaths = 0;

            while (booleanPercentileSelector.SelectFrom(TableNameConstants.Set.TrueOrFalse.KilledCohort))
                cohortDeaths++;

            leadership.CohortScore -= cohortDeaths * 2;

            if (cohortDeaths > 0)
            {
                var modifier = String.Format("Caused the death of {0} cohort(s)", cohortDeaths);
                leadershipModifiers.Add(modifier);
            }

            if (String.IsNullOrEmpty(leaderAnimal) == false)
                leadership.CohortScore -= 2;

            var followerScore = leadership.Score;
            var leaderMovement = percentileSelector.SelectFrom(TableNameConstants.Set.Percentile.LeadershipMovement);

            if (String.IsNullOrEmpty(leaderMovement) == false)
            {
                leadershipModifiers.Add(leaderMovement);
                followerScore += leadershipAdjustments[leaderMovement];
            }

            if (booleanPercentileSelector.SelectFrom(TableNameConstants.Set.TrueOrFalse.KilledFollowers))
            {
                leadershipModifiers.Add("Caused the death of followers");
                followerScore--;
            }

            leadership.LeadershipModifiers = leadershipModifiers;
            leadership.FollowerQuantities = leadershipSelector.SelectFollowerQuantitiesFor(followerScore);

            return leadership;
        }

        public Character GenerateCohort(Int32 cohortScore, Int32 leaderLevel, String leaderAlignment)
        {
            var alignmentDiffers = booleanPercentileSelector.SelectFrom(TableNameConstants.Set.TrueOrFalse.AttractCohortOfDifferentAlignment);
            if (alignmentDiffers)
                cohortScore--;

            var cohortLevel = leadershipSelector.SelectCohortLevelFor(cohortScore);
            cohortLevel = Math.Min(leaderLevel - 2, cohortLevel);

            if (cohortLevel <= 0)
                return null;

            if (alignmentDiffers == false)
            {
                setAlignmentRandomizer.SetAlignment = new Alignment(leaderAlignment);
                return GenerateFollower(setAlignmentRandomizer, cohortLevel, leaderAlignment);
            }

            return GenerateFollower(cohortLevel, leaderAlignment);
        }

        public Character GenerateFollower(Int32 level, String leaderAlignment)
        {
            return GenerateFollower(anyAlignmentRandomizer, level, leaderAlignment);
        }

        private Character GenerateFollower(IAlignmentRandomizer alignmentRandomizer, Int32 level, String leaderAlignment)
        {
            setLevelRandomizer.SetLevel = level;
            var allowedAlignments = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AlignmentGroups, leaderAlignment);

            setAlignmentRandomizer.SetAlignment = generator.Generate(() => alignmentGenerator.GenerateWith(alignmentRandomizer),
                a => allowedAlignments.Contains(a.ToString()));

            return characterGenerator.GenerateWith(setAlignmentRandomizer, anyClassNameRandomizer, setLevelRandomizer, anyBaseRaceRandomizer, anyMetaraceRandomizer, rawStatsRandomizer);
        }
    }
}

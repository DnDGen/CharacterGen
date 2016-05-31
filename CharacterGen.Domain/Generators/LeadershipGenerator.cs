using CharacterGen.Alignments;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Randomizers.Alignments;
using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Randomizers.Races;
using CharacterGen.Randomizers.Stats;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators
{
    internal class LeadershipGenerator : ILeadershipGenerator
    {
        private ICharacterGenerator characterGenerator;
        private ILeadershipSelector leadershipSelector;
        private IPercentileSelector percentileSelector;
        private IAdjustmentsSelector adjustmentsSelector;
        private ISetLevelRandomizer setLevelRandomizer;
        private ISetAlignmentRandomizer setAlignmentRandomizer;
        private IAlignmentRandomizer anyAlignmentRandomizer;
        private IClassNameRandomizer anyPlayerClassNameRandomizer;
        private IClassNameRandomizer anyNPCClassNameRandomizer;
        private RaceRandomizer anyBaseRaceRandomizer;
        private RaceRandomizer anyMetaraceRandomizer;
        private IStatsRandomizer rawStatsRandomizer;
        private IBooleanPercentileSelector booleanPercentileSelector;
        private ICollectionsSelector collectionsSelector;
        private IAlignmentGenerator alignmentGenerator;
        private Generator generator;

        public LeadershipGenerator(ICharacterGenerator characterGenerator, ILeadershipSelector leadershipSelector, IPercentileSelector percentileSelector,
            IAdjustmentsSelector adjustmentsSelector, ISetLevelRandomizer setLevelRandomizer, ISetAlignmentRandomizer setAlignmentRandomizer,
            IAlignmentRandomizer anyAlignmentRandomizer, IClassNameRandomizer anyPlayerClassNameRandomizer, RaceRandomizer anyBaseRaceRandomizer,
            RaceRandomizer anyMetaraceRandomizer, IStatsRandomizer rawStatsRandomizer, IBooleanPercentileSelector booleanPercentileSelector,
            ICollectionsSelector collectionsSelector, IAlignmentGenerator alignmentGenerator, Generator generator, IClassNameRandomizer anyNPCClassNameRandomizer)
        {
            this.characterGenerator = characterGenerator;
            this.leadershipSelector = leadershipSelector;
            this.percentileSelector = percentileSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.setLevelRandomizer = setLevelRandomizer;
            this.setAlignmentRandomizer = setAlignmentRandomizer;
            this.anyAlignmentRandomizer = anyAlignmentRandomizer;
            this.anyPlayerClassNameRandomizer = anyPlayerClassNameRandomizer;
            this.anyBaseRaceRandomizer = anyBaseRaceRandomizer;
            this.anyMetaraceRandomizer = anyMetaraceRandomizer;
            this.rawStatsRandomizer = rawStatsRandomizer;
            this.booleanPercentileSelector = booleanPercentileSelector;
            this.collectionsSelector = collectionsSelector;
            this.alignmentGenerator = alignmentGenerator;
            this.generator = generator;
            this.anyNPCClassNameRandomizer = anyNPCClassNameRandomizer;
        }

        public Leadership GenerateLeadership(int level, int charismaBonus, string leaderAnimal)
        {
            var leadership = new Leadership();
            leadership.Score = level + charismaBonus;

            var leadershipModifiers = new List<string>();
            var reputation = percentileSelector.SelectFrom(TableNameConstants.Set.Percentile.Reputation);
            var leadershipAdjustments = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LeadershipModifiers);

            if (string.IsNullOrEmpty(reputation) == false)
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
                var modifier = string.Format("Caused the death of {0} cohort(s)", cohortDeaths);
                leadershipModifiers.Add(modifier);
            }

            if (string.IsNullOrEmpty(leaderAnimal) == false)
                leadership.CohortScore -= 2;

            var followerScore = leadership.Score;
            var leaderMovement = percentileSelector.SelectFrom(TableNameConstants.Set.Percentile.LeadershipMovement);

            if (string.IsNullOrEmpty(leaderMovement) == false)
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

        public Character GenerateCohort(int cohortScore, int leaderLevel, string leaderAlignment, string leaderClass)
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
                return GenerateFollower(setAlignmentRandomizer, cohortLevel, leaderAlignment, leaderClass);
            }

            return GenerateFollower(cohortLevel, leaderAlignment, leaderClass);
        }

        public Character GenerateFollower(int level, string leaderAlignment, string leaderClass)
        {
            return GenerateFollower(anyAlignmentRandomizer, level, leaderAlignment, leaderClass);
        }

        private Character GenerateFollower(IAlignmentRandomizer alignmentRandomizer, int level, string leaderAlignment, string leaderClass)
        {
            setLevelRandomizer.SetLevel = level;
            var allowedAlignments = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AlignmentGroups, leaderAlignment);

            setAlignmentRandomizer.SetAlignment = generator.Generate(() => alignmentGenerator.GenerateWith(alignmentRandomizer),
                a => allowedAlignments.Contains(a.ToString()));

            var npcs = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.NPCs);

            if (npcs.Contains(leaderClass))
                return characterGenerator.GenerateWith(setAlignmentRandomizer, anyNPCClassNameRandomizer, setLevelRandomizer, anyBaseRaceRandomizer, anyMetaraceRandomizer, rawStatsRandomizer);

            return characterGenerator.GenerateWith(setAlignmentRandomizer, anyPlayerClassNameRandomizer, setLevelRandomizer, anyBaseRaceRandomizer, anyMetaraceRandomizer, rawStatsRandomizer);
        }
    }
}

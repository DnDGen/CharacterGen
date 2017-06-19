using CharacterGen.Alignments;
using CharacterGen.Characters;
using CharacterGen.Domain.Generators.Factories;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Leaders;
using CharacterGen.Randomizers.Abilities;
using CharacterGen.Randomizers.Alignments;
using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Randomizers.Races;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators
{
    internal class LeadershipGenerator : ILeadershipGenerator
    {
        private readonly ICharacterGenerator characterGenerator;
        private readonly ILeadershipSelector leadershipSelector;
        private readonly IPercentileSelector percentileSelector;
        private readonly IAdjustmentsSelector adjustmentsSelector;
        private readonly IBooleanPercentileSelector booleanPercentileSelector;
        private readonly ICollectionsSelector collectionsSelector;
        private readonly Generator generator;
        private readonly JustInTimeFactory justInTimeFactrory;

        public LeadershipGenerator(ICharacterGenerator characterGenerator,
            ILeadershipSelector leadershipSelector,
            IPercentileSelector percentileSelector,
            IAdjustmentsSelector adjustmentsSelector,
            IBooleanPercentileSelector booleanPercentileSelector,
            ICollectionsSelector collectionsSelector,
            Generator generator,
            JustInTimeFactory justInTimeFactrory)
        {
            this.characterGenerator = characterGenerator;
            this.leadershipSelector = leadershipSelector;
            this.percentileSelector = percentileSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.booleanPercentileSelector = booleanPercentileSelector;
            this.collectionsSelector = collectionsSelector;
            this.generator = generator;
            this.justInTimeFactrory = justInTimeFactrory;
        }

        public Leadership GenerateLeadership(int level, int charismaBonus, string leaderAnimal)
        {
            var leadership = new Leadership();
            leadership.Score = level + charismaBonus;

            var leadershipModifiers = new List<string>();
            var reputation = percentileSelector.SelectFrom(TableNameConstants.Set.Percentile.Reputation);
            var leadershipAdjustments = adjustmentsSelector.SelectAllFrom(TableNameConstants.Set.Adjustments.LeadershipModifiers);

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

            var alignment = new Alignment(leaderAlignment);

            if (alignmentDiffers)
                alignment = GetAlignment(leaderAlignment, false);

            return GenerateFollower(alignment, cohortLevel, leaderClass);
        }

        public Character GenerateFollower(int level, string leaderAlignment, string leaderClass)
        {
            var alignment = GetAlignment(leaderAlignment);
            return GenerateFollower(alignment, level, leaderClass);
        }

        private Character GenerateFollower(Alignment alignment, int level, string leaderClass)
        {
            var setLevelRandomizer = justInTimeFactrory.Build<ISetLevelRandomizer>();
            var setAlignmentRandomizer = justInTimeFactrory.Build<ISetAlignmentRandomizer>();
            var anyBaseRaceRandomizer = justInTimeFactrory.Build<RaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.AnyBase);
            var anyMetaraceRandomizer = justInTimeFactrory.Build<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.AnyMeta);
            var rawAbilitiesRandomizer = justInTimeFactrory.Build<IAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.Raw);

            setLevelRandomizer.SetLevel = level;
            setAlignmentRandomizer.SetAlignment = alignment;

            var npcs = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.NPCs);

            if (npcs.Contains(leaderClass))
            {
                var anyNPCClassNameRandomizer = justInTimeFactrory.Build<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyNPC);
                return characterGenerator.GenerateWith(setAlignmentRandomizer, anyNPCClassNameRandomizer, setLevelRandomizer, anyBaseRaceRandomizer, anyMetaraceRandomizer, rawAbilitiesRandomizer);
            }

            var anyPlayerClassNameRandomizer = justInTimeFactrory.Build<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyPlayer);
            return characterGenerator.GenerateWith(setAlignmentRandomizer, anyPlayerClassNameRandomizer, setLevelRandomizer, anyBaseRaceRandomizer, anyMetaraceRandomizer, rawAbilitiesRandomizer);
        }

        private Alignment GetAlignment(string leaderAlignment, bool allowLeaderAlignment = true)
        {
            var alignment = generator.Generate(
                () => collectionsSelector.SelectRandomFrom(TableNameConstants.Set.Collection.AlignmentGroups, leaderAlignment),
                "Cohort or follower alignment",
                a => allowLeaderAlignment || a != leaderAlignment,
                () => leaderAlignment,
                $"Cohort or follower alignment of {leaderAlignment}");

            return new Alignment(alignment);
        }
    }
}

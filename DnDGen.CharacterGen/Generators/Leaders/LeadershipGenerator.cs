using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.Characters;
using DnDGen.CharacterGen.Generators.Characters;
using DnDGen.CharacterGen.Leaders;
using DnDGen.CharacterGen.Randomizers.Abilities;
using DnDGen.CharacterGen.Randomizers.Alignments;
using DnDGen.CharacterGen.Randomizers.CharacterClasses;
using DnDGen.CharacterGen.Randomizers.Races;
using DnDGen.CharacterGen.Selectors.Collections;
using DnDGen.CharacterGen.Tables;
using DnDGen.Infrastructure.Generators;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Percentiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CharacterGen.Generators
{
    internal class LeadershipGenerator : ILeadershipGenerator
    {
        private readonly ICharacterGenerator characterGenerator;
        private readonly ILeadershipSelector leadershipSelector;
        private readonly IPercentileSelector percentileSelector;
        private readonly IAdjustmentsSelector adjustmentsSelector;
        private readonly ICollectionSelector collectionsSelector;
        private readonly Generator generator;
        private readonly JustInTimeFactory justInTimeFactrory;

        public LeadershipGenerator(ICharacterGenerator characterGenerator,
            ILeadershipSelector leadershipSelector,
            IPercentileSelector percentileSelector,
            IAdjustmentsSelector adjustmentsSelector,
            ICollectionSelector collectionsSelector,
            Generator generator,
            JustInTimeFactory justInTimeFactrory)
        {
            this.characterGenerator = characterGenerator;
            this.leadershipSelector = leadershipSelector;
            this.percentileSelector = percentileSelector;
            this.adjustmentsSelector = adjustmentsSelector;
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

            while (percentileSelector.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.KilledCohort))
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

            if (percentileSelector.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.KilledFollowers))
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
            var alignmentDiffers = percentileSelector.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.AttractCohortOfDifferentAlignment);
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
                a => allowLeaderAlignment || a != leaderAlignment,
                () => leaderAlignment,
                a => $"{a} cannot be the same as {leaderAlignment}",
                $"Cohort or follower alignment of {leaderAlignment}");

            return new Alignment(alignment);
        }
    }
}

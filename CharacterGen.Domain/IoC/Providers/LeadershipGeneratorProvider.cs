using CharacterGen.Domain.Generators;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Randomizers.Alignments;
using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Randomizers.Races;
using CharacterGen.Randomizers.Stats;
using Ninject;
using Ninject.Activation;

namespace CharacterGen.Domain.IoC.Providers
{
    class LeadershipGeneratorProvider : Provider<ILeadershipGenerator>
    {
        protected override ILeadershipGenerator CreateInstance(IContext context)
        {
            var alignmentGenerator = context.Kernel.Get<IAlignmentGenerator>();
            var percentileSelector = context.Kernel.Get<IPercentileSelector>();
            var characterGenerator = context.Kernel.Get<ICharacterGenerator>();
            var setLevelRandomizer = context.Kernel.Get<ISetLevelRandomizer>();
            var setAlignmentRandomizer = context.Kernel.Get<ISetAlignmentRandomizer>();
            var anyAlignmentRandomizer = context.Kernel.Get<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);
            var anyPlayerClassNameRandomizer = context.Kernel.Get<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyPlayer);
            var anyNPCClassNameRandomizer = context.Kernel.Get<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyNPC);
            var anyBaseRaceRandomizer = context.Kernel.Get<RaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.AnyBase);
            var anyMetaraceRandomizer = context.Kernel.Get<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.AnyMeta);
            var rawStatsRandomizer = context.Kernel.Get<IStatsRandomizer>(StatsRandomizerTypeConstants.Raw);
            var adjustmentsSelector = context.Kernel.Get<IAdjustmentsSelector>();
            var booleanPercentileSelector = context.Kernel.Get<IBooleanPercentileSelector>();
            var leadershipSelector = context.Kernel.Get<ILeadershipSelector>();
            var collectionsSelector = context.Kernel.Get<ICollectionsSelector>();
            var generator = context.Kernel.Get<Generator>();

            return new LeadershipGenerator(characterGenerator, leadershipSelector, percentileSelector, adjustmentsSelector,
                setLevelRandomizer, setAlignmentRandomizer, anyAlignmentRandomizer, anyPlayerClassNameRandomizer, anyBaseRaceRandomizer, anyMetaraceRandomizer,
                rawStatsRandomizer, booleanPercentileSelector, collectionsSelector, alignmentGenerator, generator, anyNPCClassNameRandomizer);
        }
    }
}

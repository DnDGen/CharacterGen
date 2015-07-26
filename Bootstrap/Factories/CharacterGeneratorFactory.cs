using CharacterGen.Generators;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Domain;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Magics;
using CharacterGen.Generators.Randomizers.Alignments;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Randomizers.Stats;
using CharacterGen.Generators.Verifiers;
using CharacterGen.Selectors;
using Ninject;

namespace CharacterGen.Bootstrap.Factories
{
    public static class CharacterGeneratorFactory
    {
        public static ICharacterGenerator CreateWith(IKernel kernel)
        {
            var alignmentGenerator = kernel.Get<IAlignmentGenerator>();
            var characterClassGenerator = kernel.Get<ICharacterClassGenerator>();
            var raceGenerator = kernel.Get<IRaceGenerator>();
            var randomizerVerifier = kernel.Get<IRandomizerVerifier>();
            var percentileSelector = kernel.Get<IPercentileSelector>();
            var abilitiesGenerator = kernel.Get<IAbilitiesGenerator>();
            var combatGenerator = kernel.Get<ICombatGenerator>();
            var TreasureGenerator = kernel.Get<ITreasureGenerator>();
            var setLevelRandomizer = kernel.Get<ISetLevelRandomizer>();
            var setAlignmentRandomizer = kernel.Get<ISetAlignmentRandomizer>();
            var anyAlignmentRandomizer = kernel.Get<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);
            var anyClassNameRandomizer = kernel.Get<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.Any);
            var anyBaseRaceRandomizer = kernel.Get<IBaseRaceRandomizer>(BaseRaceRandomizerTypeConstants.Any);
            var anyMetaraceRandomizer = kernel.Get<IMetaraceRandomizer>(MetaraceRandomizerTypeConstants.Any);
            var rawStatsRandomizer = kernel.Get<IStatsRandomizer>(StatsRandomizerTypeConstants.Raw);
            var adjustmentsSelector = kernel.Get<IAdjustmentsSelector>();
            var booleanPercentileSelector = kernel.Get<IBooleanPercentileSelector>();
            var leadershipSelector = kernel.Get<ILeadershipSelector>();
            var collectionsSelector = kernel.Get<ICollectionsSelector>();
            var magicGenerator = kernel.Get<IMagicGenerator>();

            return new CharacterGenerator(alignmentGenerator, characterClassGenerator, raceGenerator, adjustmentsSelector,
                randomizerVerifier, percentileSelector, abilitiesGenerator, combatGenerator, TreasureGenerator, setAlignmentRandomizer,
                setLevelRandomizer, anyAlignmentRandomizer, anyClassNameRandomizer, anyBaseRaceRandomizer, anyMetaraceRandomizer,
                rawStatsRandomizer, booleanPercentileSelector, leadershipSelector, collectionsSelector, magicGenerator);
        }
    }
}
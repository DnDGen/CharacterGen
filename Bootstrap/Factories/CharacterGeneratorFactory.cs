using Ninject;
using NPCGen.Generators;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Combats;
using NPCGen.Generators.Interfaces.Items;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Generators.Interfaces.Verifiers;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Bootstrap.Factories
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
            var equipmentGenerator = kernel.Get<IEquipmentGenerator>();
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

            return new CharacterGenerator(alignmentGenerator, characterClassGenerator, raceGenerator, adjustmentsSelector,
                randomizerVerifier, percentileSelector, abilitiesGenerator, combatGenerator, equipmentGenerator, setAlignmentRandomizer,
                setLevelRandomizer, anyAlignmentRandomizer, anyClassNameRandomizer, anyBaseRaceRandomizer, anyMetaraceRandomizer,
                rawStatsRandomizer, booleanPercentileSelector, leadershipSelector, collectionsSelector);
        }
    }
}
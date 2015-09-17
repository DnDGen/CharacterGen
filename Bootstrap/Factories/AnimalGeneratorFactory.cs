using CharacterGen.Generators;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Domain.Magics;
using CharacterGen.Generators.Magics;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Randomizers.Stats;
using CharacterGen.Selectors;
using Ninject;

namespace CharacterGen.Bootstrap.Factories
{
    public static class AnimalGeneratorFactory
    {
        public static IAnimalGenerator CreateWith(IKernel kernel)
        {
            var collectionsSelector = kernel.Get<ICollectionsSelector>();
            var raceGenerator = kernel.Get<IRaceGenerator>();
            var animalBaseRaceRandomizer = kernel.Get<IBaseRaceRandomizer>(BaseRaceRandomizerTypeConstants.Animal);
            var noMetaraceRandomizer = kernel.Get<IMetaraceRandomizer>(MetaraceRandomizerTypeConstants.None);
            var adjustmentsSelector = kernel.Get<IAdjustmentsSelector>();
            var animalAbilitiesGenerator = kernel.Get<IAbilitiesGenerator>(AbilitiesGeneratorTypeConstants.Animal);
            var setStatsRandomizer = kernel.Get<ISetStatsRandomizer>();
            var animalCombatGenerator = kernel.Get<ICombatGenerator>(CombatGeneratorTypeConstants.Animal);

            return new AnimalGenerator(collectionsSelector, raceGenerator, animalBaseRaceRandomizer, noMetaraceRandomizer, adjustmentsSelector, animalAbilitiesGenerator,
                setStatsRandomizer, animalCombatGenerator);
        }
    }
}

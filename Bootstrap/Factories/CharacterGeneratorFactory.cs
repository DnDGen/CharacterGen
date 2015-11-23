using CharacterGen.Generators;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Domain;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Magics;
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
            var abilitiesGenerator = kernel.Get<IAbilitiesGenerator>(AbilitiesGeneratorTypeConstants.Character);
            var combatGenerator = kernel.Get<ICombatGenerator>(CombatGeneratorTypeConstants.Character);
            var equipmentGenerator = kernel.Get<IEquipmentGenerator>();
            var adjustmentsSelector = kernel.Get<IAdjustmentsSelector>();
            var magicGenerator = kernel.Get<IMagicGenerator>();
            var generator = kernel.Get<Generator>();

            return new CharacterGenerator(alignmentGenerator, characterClassGenerator, raceGenerator, adjustmentsSelector,
                randomizerVerifier, percentileSelector, abilitiesGenerator, combatGenerator, equipmentGenerator,
                magicGenerator, generator);
        }
    }
}
using CharacterGen.Domain.Generators;
using CharacterGen.Domain.Generators.Items;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using Ninject;
using Ninject.Activation;
using TreasureGen.Items;
using TreasureGen.Items.Magical;
using TreasureGen.Items.Mundane;

namespace CharacterGen.Domain.IoC.Providers
{
    class WeaponGeneratorProvider : Provider<IWeaponGenerator>
    {
        protected override IWeaponGenerator CreateInstance(IContext context)
        {
            var collectionsSelector = context.Kernel.Get<ICollectionsSelector>();
            var percentileSelector = context.Kernel.Get<IPercentileSelector>();
            var mundaneWeaponGenerator = context.Kernel.Get<MundaneItemGenerator>(ItemTypeConstants.Weapon);
            var magicalWeaponGenerator = context.Kernel.Get<MagicalItemGenerator>(ItemTypeConstants.Weapon);
            var generator = context.Kernel.Get<Generator>();

            return new WeaponGenerator(collectionsSelector, percentileSelector, mundaneWeaponGenerator,
                magicalWeaponGenerator, generator);
        }
    }
}

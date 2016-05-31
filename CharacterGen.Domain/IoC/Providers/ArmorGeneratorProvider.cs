using CharacterGen.Domain.Generators;
using CharacterGen.Domain.Generators.Items;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using Ninject;
using Ninject.Activation;
using TreasureGen.Common.Items;
using TreasureGen.Generators.Items.Magical;
using TreasureGen.Generators.Items.Mundane;

namespace CharacterGen.Domain.IoC.Providers
{
    class ArmorGeneratorProvider : Provider<IArmorGenerator>
    {
        protected override IArmorGenerator CreateInstance(IContext context)
        {
            var collectionsSelector = context.Kernel.Get<ICollectionsSelector>();
            var percentileSelector = context.Kernel.Get<IPercentileSelector>();
            var mundaneWeaponGenerator = context.Kernel.Get<MundaneItemGenerator>(ItemTypeConstants.Armor);
            var magicalWeaponGenerator = context.Kernel.Get<MagicalItemGenerator>(ItemTypeConstants.Armor);
            var generator = context.Kernel.Get<Generator>();

            return new ArmorGenerator(collectionsSelector, percentileSelector, mundaneWeaponGenerator,
                magicalWeaponGenerator, generator);
        }
    }
}

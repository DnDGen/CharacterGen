using CharacterGen.Generators.Domain.Items;
using CharacterGen.Generators.Items;
using CharacterGen.Selectors;
using Ninject;
using TreasureGen.Common.Items;
using TreasureGen.Generators.Items.Magical;
using TreasureGen.Generators.Items.Mundane;

namespace CharacterGen.Bootstrap.Factories
{
    public static class ArmorGeneratorFactory
    {
        public static GearGenerator CreateWith(IKernel kernel)
        {
            var collectionsSelector = kernel.Get<ICollectionsSelector>();
            var percentileSelector = kernel.Get<IPercentileSelector>();
            var mundaneWeaponGenerator = kernel.Get<IMundaneItemGenerator>(ItemTypeConstants.Armor);
            var magicalWeaponGenerator = kernel.Get<IMagicalItemGenerator>(ItemTypeConstants.Armor);

            return new ArmorGenerator(collectionsSelector, percentileSelector, mundaneWeaponGenerator,
                magicalWeaponGenerator);
        }
    }
}

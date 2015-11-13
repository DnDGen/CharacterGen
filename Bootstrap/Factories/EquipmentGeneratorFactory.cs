using CharacterGen.Generators;
using CharacterGen.Generators.Domain.Items;
using CharacterGen.Generators.Items;
using CharacterGen.Selectors;
using Ninject;
using TreasureGen.Common.Items;
using TreasureGen.Generators;

namespace CharacterGen.Bootstrap.Factories
{
    public static class EquipmentGeneratorFactory
    {
        public static IEquipmentGenerator CreateWith(IKernel kernel)
        {
            var collectionsSelector = kernel.Get<ICollectionsSelector>();
            var weaponGenerator = kernel.Get<GearGenerator>(ItemTypeConstants.Weapon);
            var treasureGenerator = kernel.Get<ITreasureGenerator>();
            var armorGenerator = kernel.Get<GearGenerator>(ItemTypeConstants.Armor);
            var generator = kernel.Get<Generator>();

            return new EquipmentGenerator(collectionsSelector, weaponGenerator, treasureGenerator, armorGenerator, generator);
        }
    }
}

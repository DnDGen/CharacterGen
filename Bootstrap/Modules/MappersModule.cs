using CharacterGen.Bootstrap.Factories;
using CharacterGen.Mappers;
using Ninject.Modules;

namespace CharacterGen.Bootstrap.Modules
{
    public class MappersModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICollectionsMapper>().ToMethod(c => CollectionsMapperFactory.CreateWith(c.Kernel)).InSingletonScope();
            Bind<IPercentileMapper>().ToMethod(c => PercentileMapperFactory.CreateWith(c.Kernel)).InSingletonScope();
        }
    }
}
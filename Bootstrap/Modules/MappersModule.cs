using Ninject.Modules;
using NPCGen.Bootstrap.Factories;
using NPCGen.Mappers.Interfaces;

namespace NPCGen.Bootstrap.Modules
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
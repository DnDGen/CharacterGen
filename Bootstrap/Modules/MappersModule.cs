using Ninject;
using Ninject.Modules;
using NPCGen.Bootstrap.Factories;
using NPCGen.Mappers.Collections;
using NPCGen.Mappers.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Bootstrap.Modules
{
    public class MappersModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAdjustmentMapper>().To<AdjustmentsMapper>();
            Bind<ICollectionsMapper>().ToMethod(c => CollectionsMapperFactory.CreateWith(c.Kernel.Get<IStreamLoader>())).InSingletonScope();
            Bind<IPercentileMapper>().ToMethod(c => PercentileMapperFactory.CreateWith(c.Kernel.Get<IStreamLoader>())).InSingletonScope();
            Bind<IStatPriorityMapper>().To<StatPriorityMapper>();
        }
    }
}
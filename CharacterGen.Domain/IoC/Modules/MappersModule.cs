using CharacterGen.Domain.Mappers.Collections;
using CharacterGen.Domain.Mappers.Percentiles;
using Ninject.Modules;

namespace CharacterGen.Domain.IoC.Modules
{
    internal class MappersModule : NinjectModule
    {
        public override void Load()
        {
            Bind<PercentileMapper>().To<PercentileXmlMapper>().WhenInjectedInto<PercentileMapperCachingProxy>();
            Bind<PercentileMapper>().To<PercentileMapperCachingProxy>().InSingletonScope();

            Bind<CollectionsMapper>().To<CollectionsXmlMapper>().WhenInjectedInto<CollectionsMapperCachingProxy>();
            Bind<CollectionsMapper>().To<CollectionsMapperCachingProxy>().InSingletonScope();
        }
    }
}
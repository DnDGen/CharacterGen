using Ninject;
using NPCGen.Mappers.Collections;
using NPCGen.Mappers.Interfaces;

namespace NPCGen.Bootstrap.Factories
{
    public static class CollectionsMapperFactory
    {
        public static ICollectionsMapper CreateWith(IKernel kernel)
        {
            ICollectionsMapper mapper = kernel.Get<CollectionsXmlMapper>();
            mapper = new CollectionsMapperCachingProxy(mapper);

            return mapper;
        }
    }
}
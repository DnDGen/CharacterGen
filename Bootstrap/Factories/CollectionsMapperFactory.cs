using NPCGen.Mappers.Collections;
using NPCGen.Mappers.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Bootstrap.Factories
{
    public static class CollectionsMapperFactory
    {
        public static ICollectionsMapper CreateWith(IStreamLoader streamLoader)
        {
            ICollectionsMapper mapper = new CollectionsXmlMapper(streamLoader);
            mapper = new CollectionsMapperCachingProxy(mapper);

            return mapper;
        }
    }
}
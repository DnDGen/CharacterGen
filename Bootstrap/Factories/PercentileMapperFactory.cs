using NPCGen.Mappers.Interfaces;
using NPCGen.Mappers.Percentiles;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Bootstrap.Factories
{
    public static class PercentileMapperFactory
    {
        public static IPercentileMapper CreateWith(IStreamLoader streamLoader)
        {
            IPercentileMapper mapper = new PercentileXmlMapper(streamLoader);
            mapper = new PercentileMapperCachingProxy(mapper);

            return mapper;
        }
    }
}
using Ninject;
using NPCGen.Mappers.Interfaces;
using NPCGen.Mappers.Percentiles;

namespace NPCGen.Bootstrap.Factories
{
    public static class PercentileMapperFactory
    {
        public static IPercentileMapper CreateWith(IKernel kernel)
        {
            IPercentileMapper mapper = kernel.Get<PercentileXmlMapper>();
            mapper = new PercentileMapperCachingProxy(mapper);

            return mapper;
        }
    }
}
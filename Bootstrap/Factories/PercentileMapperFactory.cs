using CharacterGen.Mappers;
using CharacterGen.Mappers.Domain.Percentiles;
using Ninject;

namespace CharacterGen.Bootstrap.Factories
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
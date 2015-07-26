using CharacterGen.Mappers;
using CharacterGen.Mappers.Domain.Collections;
using Ninject;

namespace CharacterGen.Bootstrap.Factories
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
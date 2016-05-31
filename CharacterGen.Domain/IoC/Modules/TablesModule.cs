using CharacterGen.Domain.Tables;
using Ninject.Modules;

namespace CharacterGen.Domain.IoC.Modules
{
    internal class TablesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<StreamLoader>().To<EmbeddedResourceStreamLoader>();
        }
    }
}
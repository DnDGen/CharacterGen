using CharacterGen.Tables;
using CharacterGen.Tables.Domain;
using Ninject.Modules;

namespace CharacterGen.Bootstrap.Modules
{
    public class TablesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<StreamLoader>().To<EmbeddedResourceStreamLoader>();
        }
    }
}
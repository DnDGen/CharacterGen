using Ninject.Modules;
using NPCGen.Tables;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Bootstrap.Modules
{
    public class TablesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IStreamLoader>().To<EmbeddedResourceStreamLoader>();
        }
    }
}
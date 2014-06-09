using Ninject;
using NPCGen.Bootstrap.Modules;

namespace NPCGen.Bootstrap
{
    public class NPCGenModuleLoader
    {
        public void LoadModules(IKernel kernel)
        {
            kernel.Load<GeneratorsModule>();
            kernel.Load<SelectorsModule>();
            kernel.Load<MappersModule>();
            kernel.Load<TablesModule>();
        }
    }
}
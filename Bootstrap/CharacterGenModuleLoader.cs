using Ninject;
using CharacterGen.Bootstrap.Modules;

namespace CharacterGen.Bootstrap
{
    public class CharacterGenModuleLoader
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
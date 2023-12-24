using DnDGen.CharacterGen.IoC.Modules;
using Ninject;

namespace DnDGen.CharacterGen.IoC
{
    public class CharacterGenModuleLoader
    {
        public void LoadModules(IKernel kernel)
        {
            kernel.Load<GeneratorsModule>();
            kernel.Load<SelectorsModule>();
        }
    }
}
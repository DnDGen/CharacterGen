using CharacterGen.Domain.IoC.Modules;
using Ninject;

namespace CharacterGen.Domain.IoC
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
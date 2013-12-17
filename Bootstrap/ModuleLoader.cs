using Ninject;

namespace NPCGen.Bootstrap
{
    public class ModuleLoader
    {
        public void LoadModules(IKernel kernel)
        {
            kernel.Load<CoreModule>();
        }
    }
}
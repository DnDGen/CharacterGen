using Ninject;

namespace NPCGen.Bootstrap
{
    public class NPCGenModuleLoader
    {
        public void LoadModules(IKernel kernel)
        {
            kernel.Load<CoreModule>();
        }
    }
}
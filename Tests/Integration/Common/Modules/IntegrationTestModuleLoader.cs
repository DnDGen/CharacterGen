using Ninject;

namespace NPCGen.Tests.Integration.Common.Modules
{
    public class IntegrationTestModuleLoader
    {
        public void LoadModules(IKernel kernel)
        {
            kernel.Load<IntegrationTestModule>();
        }
    }
}
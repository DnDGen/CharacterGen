using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using NPCGen.Selectors;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Bootstrap.Modules
{
    public class SelectorsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILanguageProvider>().To<LanguagesProvider>();
            Bind<ILevelAdjustmentsProvider>().To<LevelAdjustmentsProvider>();
            Bind<IPercentileResultProvider>().To<PercentileResultProvider>().InSingletonScope();
            Bind<IStatPriorityProvider>().To<StatPriorityProvider>();
            Bind<IStatAdjustmentsProvider>().To<StatAdjustmentsProvider>();
        }
    }
}
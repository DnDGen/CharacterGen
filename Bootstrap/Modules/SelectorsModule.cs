using Ninject.Modules;
using NPCGen.Selectors;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Bootstrap.Modules
{
    public class SelectorsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILanguagesSelector>().To<LanguagesSelector>();
            Bind<ILevelAdjustmentsSelector>().To<LevelAdjustmentsSelector>();
            Bind<IPercentileSelector>().To<PercentileSelector>();
            Bind<IStatPrioritySelector>().To<StatPrioritySelector>();
            Bind<IStatAdjustmentsSelector>().To<StatAdjustmentsSelector>();
        }
    }
}
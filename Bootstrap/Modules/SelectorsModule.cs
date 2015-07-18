using Ninject.Modules;
using NPCGen.Selectors;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Bootstrap.Modules
{
    public class SelectorsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILanguageCollectionsSelector>().To<LanguageCollectionsSelector>();
            Bind<IAdjustmentsSelector>().To<AdjustmentsSelector>();
            Bind<IPercentileSelector>().To<PercentileSelector>();
            Bind<IStatPrioritySelector>().To<StatPrioritySelector>();
            Bind<IStatAdjustmentsSelector>().To<StatAdjustmentsSelector>();
            Bind<ICollectionsSelector>().To<CollectionsSelector>();
            Bind<ISkillSelector>().To<SkillSelector>();
            Bind<IFeatsSelector>().To<FeatsSelector>();
            Bind<IBooleanPercentileSelector>().To<BooleanPercentileSelector>();
            Bind<ILeadershipSelector>().To<LeadershipSelector>();
        }
    }
}
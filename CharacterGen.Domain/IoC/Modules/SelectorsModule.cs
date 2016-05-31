using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using Ninject.Modules;

namespace CharacterGen.Domain.IoC.Modules
{
    internal class SelectorsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILanguageCollectionsSelector>().To<LanguageCollectionsSelector>();
            Bind<IAdjustmentsSelector>().To<AdjustmentsSelector>();
            Bind<IPercentileSelector>().To<PercentileSelector>();
            Bind<IStatAdjustmentsSelector>().To<StatAdjustmentsSelector>();
            Bind<ICollectionsSelector>().To<CollectionsSelector>();
            Bind<ISkillSelector>().To<SkillSelector>();
            Bind<IFeatsSelector>().To<FeatsSelector>();
            Bind<IBooleanPercentileSelector>().To<BooleanPercentileSelector>();
            Bind<ILeadershipSelector>().To<LeadershipSelector>();
        }
    }
}
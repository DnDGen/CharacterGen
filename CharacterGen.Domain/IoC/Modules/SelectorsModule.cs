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
            Bind<IAbilityAdjustmentsSelector>().To<AbilityAdjustmentsSelector>();
            Bind<ISkillSelector>().To<SkillSelector>();
            Bind<IBooleanPercentileSelector>().To<BooleanPercentileSelector>();
            Bind<ILeadershipSelector>().To<LeadershipSelector>();

            Bind<IFeatsSelector>().To<FeatsSelector>().WhenInjectedInto<FeatsSelectorEventGenDecorator>();
            Bind<IFeatsSelector>().To<FeatsSelectorEventGenDecorator>();

            Bind<ICollectionsSelector>().To<CollectionsSelector>().WhenInjectedInto<CollectionsSelectorEventGenDecorator>();
            Bind<ICollectionsSelector>().To<CollectionsSelectorEventGenDecorator>();
        }
    }
}
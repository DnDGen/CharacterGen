using D20Dice;
using Ninject.Modules;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Providers;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Verifiers;
using NPCGen.Core.Generation.Verifiers.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;

namespace NPCGen.Bootstrap
{
    public class CoreModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAdjustmentXmlParser>().To<AdjustmentXmlParser>();
            Bind<IAlignmentFactory>().To<AlignmentFactory>();
            Bind<ICharacterClassFactory>().To<CharacterClassFactory>();
            Bind<ICharacterFactory>().To<CharacterFactory>();
            Bind<IDice>().ToMethod(c => DiceFactory.Create()).InSingletonScope();
            Bind<IHitPointsFactory>().To<HitPointsFactory>();
            Bind<ILanguageFactory>().To<LanguageFactory>();
            Bind<ILanguageProvider>().To<LanguagesProvider>();
            Bind<ILanguagesXmlParser>().To<LanguagesXmlParser>();
            Bind<ILevelAdjustmentsProvider>().To<LevelAdjustmentsProvider>();
            Bind<IPercentileResultProvider>().To<PercentileResultProvider>();
            Bind<IPercentileXmlParser>().To<PercentileXmlParser>();
            Bind<IRaceFactory>().To<RaceFactory>();
            Bind<IRandomizerVerifier>().To<RandomizerVerifier>();
            Bind<IStatsFactory>().To<StatsFactory>();
            Bind<IStatPriorityProvider>().To<StatPriorityProvider>();
            Bind<IStatPriorityXmlParser>().To<StatPriorityXmlParser>();
            Bind<IStatAdjustmentsProvider>().To<StatAdjustmentsProvider>();
            Bind<IStreamLoader>().To<EmbeddedResourceStreamLoader>();
        }
    }
}
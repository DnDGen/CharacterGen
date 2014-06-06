using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using NPCGen.Generators;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Verifiers;
using NPCGen.Generators.Verifiers;

namespace NPCGen.Bootstrap.Modules
{
    public class GeneratorsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAlignmentFactory>().To<AlignmentFactory>();
            Bind<ICharacterClassFactory>().To<CharacterClassFactory>();
            Bind<ICharacterFactory>().To<CharacterFactory>();
            Bind<IHitPointsFactory>().To<HitPointsFactory>();
            Bind<ILanguageFactory>().To<LanguageFactory>();
            Bind<IRaceFactory>().To<RaceFactory>();
            Bind<IRandomizerVerifier>().To<RandomizerVerifier>();
            Bind<IStatsFactory>().To<StatsFactory>();
        }
    }
}
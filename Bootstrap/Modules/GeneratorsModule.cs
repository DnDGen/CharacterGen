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
            Bind<IAlignmentGenerator>().To<AlignmentGenerator>();
            Bind<ICharacterClassGenerator>().To<CharacterClassGenerator>();
            Bind<ICharacterGenerator>().To<CharacterGenerator>();
            Bind<IHitPointsGenerator>().To<HitPointsGenerator>();
            Bind<ILanguageGenerator>().To<LanguageGenerator>();
            Bind<IRaceGenerator>().To<RaceGenerator>();
            Bind<IRandomizerVerifier>().To<RandomizerVerifier>();
            Bind<IStatsGenerator>().To<StatsGenerator>();
        }
    }
}
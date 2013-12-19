using System;
using System.Collections.Generic;
using Ninject;
using Ninject.Modules;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Core.Generation.Randomizers.Stats;
using NPCGen.Core.Generation.Verifiers.Interfaces;

namespace NPCGen.Tests.Integration.Common.Modules
{
    public class IntegrationTestModule : NinjectModule
    {
        public override void Load()
        {
            Bind<Alignment>().ToMethod(c => GenerateAlignment(c.Kernel)).InSingletonScope();
            Bind<CharacterClass>().ToMethod(c => c.Kernel.Get<ICharacterClassFactory>().CreateWith(
                c.Kernel.Get<CharacterClassPrototype>())).InSingletonScope();
            Bind<CharacterClassPrototype>().ToMethod(c => GenerateCharacterClassPrototype(c.Kernel)).InSingletonScope();
            Bind<Race>().ToMethod(c => GenerateRace(c.Kernel)).InSingletonScope();
            Bind<Dictionary<String, Stat>>().ToMethod(c => c.Kernel.Get<IStatsFactory>().CreateWith(c.Kernel.Get<RawStatsRandomizer>(),
                c.Kernel.Get<CharacterClass>(), c.Kernel.Get<Race>())).InSingletonScope();
        }

        private Alignment GenerateAlignment(IKernel kernel)
        {
            Alignment alignment;

            do alignment = kernel.Get<IAlignmentFactory>().CreateWith(kernel.Get<AnyAlignmentRandomizer>());
            while (!kernel.Get<IRandomizerVerifier>().VerifyAlignmentCompatibility(alignment, kernel.Get<AnyClassNameRandomizer>(),
                kernel.Get<AnyLevelRandomizer>(), kernel.Get<AnyBaseRaceRandomizer>(), kernel.Get<AnyMetaraceRandomizer>()));

            return alignment;
        }

        private CharacterClassPrototype GenerateCharacterClassPrototype(IKernel kernel)
        {
            CharacterClassPrototype prototype;

            do prototype = kernel.Get<ICharacterClassFactory>().CreatePrototypeWith(kernel.Get<Alignment>(),
                kernel.Get<AnyLevelRandomizer>(), kernel.Get<AnyClassNameRandomizer>());
            while (!kernel.Get<IRandomizerVerifier>().VerifyCharacterClassCompatibility(kernel.Get<Alignment>().Goodness, prototype,
                kernel.Get<AnyBaseRaceRandomizer>(), kernel.Get<AnyMetaraceRandomizer>()));

            return prototype;
        }

        private Race GenerateRace(IKernel kernel)
        {
            Race race;
            var levelAdjustments = kernel.Get<ILevelAdjustmentsProvider>().GetLevelAdjustments();

            do race = kernel.Get<IRaceFactory>().CreateWith(kernel.Get<Alignment>().Goodness, kernel.Get<CharacterClassPrototype>(),
                kernel.Get<AnyBaseRaceRandomizer>(), kernel.Get<AnyMetaraceRandomizer>());
            while (levelAdjustments[race.BaseRace] + levelAdjustments[race.Metarace] >= kernel.Get<CharacterClassPrototype>().Level);

            return race;
        }
    }
}
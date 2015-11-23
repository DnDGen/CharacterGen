using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.Races;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NonNeutralMetaraceRandomizerTests : ForcableMetaraceRandomizerTests
    {
        [Inject, Named(RaceRandomizerTypeConstants.Metarace.NonNeutralMeta)]
        public override IForcableMetaraceRandomizer ForcableMetaraceRandomizer { get; set; }

        protected override IEnumerable<String> allowedMetaraces
        {
            get
            {
                return new[]
                {
                    RaceConstants.Metaraces.HalfDragon,
                    RaceConstants.Metaraces.HalfFiend,
                    RaceConstants.Metaraces.HalfCelestial,
                    RaceConstants.Metaraces.Werebear,
                    RaceConstants.Metaraces.Wererat,
                    RaceConstants.Metaraces.Werewolf,
                    RaceConstants.Metaraces.Ghost,
                    RaceConstants.Metaraces.Lich,
                    RaceConstants.Metaraces.Vampire,
                    RaceConstants.Metaraces.None
                };
            }
        }

        [TestCase("NonNeutralMetaraceRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        [Test]
        public override void MetaraceForced()
        {
            AssertForcedMetarace();
        }

        [Test]
        public override void MetaraceNotForced()
        {
            AssertUnforcedMetarace();
        }
    }
}
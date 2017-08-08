using CharacterGen.Races;
using CharacterGen.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class AnyMetaraceRandomizerTests : ForcableMetaraceRandomizerTests
    {
        protected override IEnumerable<string> allowedMetaraces
        {
            get
            {
                return new[] {
                    RaceConstants.Metaraces.Ghost,
                    RaceConstants.Metaraces.HalfCelestial,
                    RaceConstants.Metaraces.HalfDragon,
                    RaceConstants.Metaraces.HalfFiend,
                    RaceConstants.Metaraces.Lich,
                    RaceConstants.Metaraces.Mummy,
                    RaceConstants.Metaraces.None,
                    RaceConstants.Metaraces.Vampire,
                    RaceConstants.Metaraces.Werebear,
                    RaceConstants.Metaraces.Wereboar,
                    RaceConstants.Metaraces.Wererat,
                    RaceConstants.Metaraces.Weretiger,
                    RaceConstants.Metaraces.Werewolf,
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            forcableMetaraceRandomizer = GetNewInstanceOf<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.AnyMeta);
        }

        [Test]
        public void StressAnyMetarace()
        {
            stressor.Stress(AssertMetarace);
        }

        [Test]
        public override void StressForcedMetarace()
        {
            stressor.Stress(AssertForcedMetarace);
        }
    }
}
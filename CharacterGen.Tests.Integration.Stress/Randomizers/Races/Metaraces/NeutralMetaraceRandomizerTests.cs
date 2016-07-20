using CharacterGen.Races;
using CharacterGen.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NeutralMetaraceRandomizerTests : ForcableMetaraceRandomizerTests
    {
        protected override IEnumerable<string> allowedMetaraces
        {
            get
            {
                return new[]
                {
                    RaceConstants.Metaraces.Wereboar,
                    RaceConstants.Metaraces.Weretiger,
                    RaceConstants.Metaraces.Ghost,
                    RaceConstants.Metaraces.None
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            forcableMetaraceRandomizer = GetNewInstanceOf<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.NeutralMeta);
        }

        [Test]
        public void StressMetarace()
        {
            Stress(AssertMetarace);
        }

        [Test]
        public override void StressForcedMetarace()
        {
            Stress(AssertForcedMetarace);
        }
    }
}
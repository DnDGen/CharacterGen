using CharacterGen.Races;
using CharacterGen.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class EvilMetaraceRandomizerTests : ForcableMetaraceRandomizerTests
    {
        protected override IEnumerable<string> allowedMetaraces
        {
            get
            {
                return new[]
                {
                    RaceConstants.Metaraces.HalfDragon,
                    RaceConstants.Metaraces.HalfFiend,
                    RaceConstants.Metaraces.Wererat,
                    RaceConstants.Metaraces.Werewolf,
                    RaceConstants.Metaraces.Ghost,
                    RaceConstants.Metaraces.Lich,
                    RaceConstants.Metaraces.Vampire,
                    RaceConstants.Metaraces.None
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            forcableMetaraceRandomizer = GetNewInstanceOf<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.EvilMeta);
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
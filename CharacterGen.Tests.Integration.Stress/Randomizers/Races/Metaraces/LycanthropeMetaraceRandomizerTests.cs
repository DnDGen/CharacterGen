﻿using CharacterGen.Races;
using CharacterGen.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class LycanthropeMetaraceRandomizerTests : ForcableMetaraceRandomizerTests
    {
        protected override IEnumerable<string> allowedMetaraces
        {
            get
            {
                return new[]
                {
                    RaceConstants.Metaraces.Werebear,
                    RaceConstants.Metaraces.Wereboar,
                    RaceConstants.Metaraces.Wererat,
                    RaceConstants.Metaraces.Weretiger,
                    RaceConstants.Metaraces.Werewolf,
                    RaceConstants.Metaraces.None
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            forcableMetaraceRandomizer = GetNewInstanceOf<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.LycanthropeMeta);
        }

        [Test]
        public void StressLycanthropeMetarace()
        {
            stressor.Stress(GenerateAndAssertMetarace);
        }

        [Test]
        [Ignore("Because lycanthropes can only have a particular alignment, generating alignments that satisfy the randomizer takes over 200% of stress test time limit")]
        public void StressForcedLycanthropeMetarace()
        {
            stressor.Stress(GenerateAndAssertForcedMetarace);
        }
    }
}
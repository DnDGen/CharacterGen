using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class NonNeutralBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        [Inject, Named(BaseRaceRandomizerTypeConstants.NonNeutral)]
        public override IBaseRaceRandomizer BaseRaceRandomizer { get; set; }

        protected override IEnumerable<String> particularBaseRaces
        {
            get
            {
                var neutralBaseRaces = new[]
                {
                    RaceConstants.BaseRaces.Doppelganger,
                    RaceConstants.BaseRaces.Lizardfolk
                };

                return RaceConstants.BaseRaces.GetBaseRaces().Except(neutralBaseRaces);
            }
        }
    }
}
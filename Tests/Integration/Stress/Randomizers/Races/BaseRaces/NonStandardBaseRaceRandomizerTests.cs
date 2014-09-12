using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class NonStandardBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        [Inject, Named(BaseRaceRandomizerTypeConstants.NonStandard)]
        public override IBaseRaceRandomizer BaseRaceRandomizer { get; set; }

        protected override IEnumerable<String> allowedBaseRaces
        {
            get
            {
                return new[]
                {
                    RaceConstants.BaseRaces.Svirfneblin,
                    RaceConstants.BaseRaces.Aasimar,
                    RaceConstants.BaseRaces.Derro,
                    RaceConstants.BaseRaces.Drow,
                    RaceConstants.BaseRaces.DuergarDwarf,
                    RaceConstants.BaseRaces.Goblin,
                    RaceConstants.BaseRaces.Hobgoblin,
                    RaceConstants.BaseRaces.Ogre,
                    RaceConstants.BaseRaces.OgreMage,
                    RaceConstants.BaseRaces.Orc,
                    RaceConstants.BaseRaces.Troglodyte,
                    RaceConstants.BaseRaces.MindFlayer,
                    RaceConstants.BaseRaces.Minotaur,
                    RaceConstants.BaseRaces.Gnoll,
                    RaceConstants.BaseRaces.Bugbear,
                    RaceConstants.BaseRaces.Tiefling,
                    RaceConstants.BaseRaces.Kobold,
                    RaceConstants.BaseRaces.Doppelganger,
                    RaceConstants.BaseRaces.Lizardfolk,
                    RaceConstants.BaseRaces.MountainDwarf,
                    RaceConstants.BaseRaces.ForestGnome,
                    RaceConstants.BaseRaces.TallfellowHalfling,
                    RaceConstants.BaseRaces.WildElf,
                    RaceConstants.BaseRaces.DeepDwarf,
                    RaceConstants.BaseRaces.DeepHalfling,
                    RaceConstants.BaseRaces.GrayElf,
                    RaceConstants.BaseRaces.WoodElf
                };
            }
        }

        [Test]
        public override void Stress()
        {
            do MakeAssertions();
            while (TestShouldKeepRunning());

            AssertIterations();
        }
    }
}
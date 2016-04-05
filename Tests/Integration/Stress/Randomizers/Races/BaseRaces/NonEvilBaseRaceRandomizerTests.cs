using CharacterGen.Common.Races;
using CharacterGen.Generators.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class NonEvilBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        protected override IEnumerable<string> allowedBaseRaces
        {
            get
            {
                return new[] {
                    RaceConstants.BaseRaces.Aasimar,
                    RaceConstants.BaseRaces.DeepDwarf,
                    RaceConstants.BaseRaces.DeepHalfling,
                    RaceConstants.BaseRaces.Doppelganger,
                    RaceConstants.BaseRaces.ForestGnome,
                    RaceConstants.BaseRaces.GrayElf,
                    RaceConstants.BaseRaces.HalfElf,
                    RaceConstants.BaseRaces.HalfOrc,
                    RaceConstants.BaseRaces.HighElf,
                    RaceConstants.BaseRaces.HillDwarf,
                    RaceConstants.BaseRaces.Human,
                    RaceConstants.BaseRaces.LightfootHalfling,
                    RaceConstants.BaseRaces.Lizardfolk,
                    RaceConstants.BaseRaces.MountainDwarf,
                    RaceConstants.BaseRaces.RockGnome,
                    RaceConstants.BaseRaces.Svirfneblin,
                    RaceConstants.BaseRaces.TallfellowHalfling,
                    RaceConstants.BaseRaces.WildElf,
                    RaceConstants.BaseRaces.WoodElf
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            BaseRaceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.NonEvilBase);
        }

        [TestCase("Non-Evil Base Race Randomizer")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }
    }
}
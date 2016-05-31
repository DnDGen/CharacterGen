using CharacterGen.Races;
using CharacterGen.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class EvilBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        protected override IEnumerable<string> allowedBaseRaces
        {
            get
            {
                return new[]
                {
                    RaceConstants.BaseRaces.Bugbear,
                    RaceConstants.BaseRaces.Derro,
                    RaceConstants.BaseRaces.Drow,
                    RaceConstants.BaseRaces.DuergarDwarf,
                    RaceConstants.BaseRaces.Gnoll,
                    RaceConstants.BaseRaces.Goblin,
                    RaceConstants.BaseRaces.Hobgoblin,
                    RaceConstants.BaseRaces.Kobold,
                    RaceConstants.BaseRaces.Ogre,
                    RaceConstants.BaseRaces.OgreMage,
                    RaceConstants.BaseRaces.Orc,
                    RaceConstants.BaseRaces.Troglodyte,
                    RaceConstants.BaseRaces.MindFlayer,
                    RaceConstants.BaseRaces.Minotaur,
                    RaceConstants.BaseRaces.Tiefling,
                    RaceConstants.BaseRaces.Lizardfolk,
                    RaceConstants.BaseRaces.DeepDwarf,
                    RaceConstants.BaseRaces.DeepHalfling,
                    RaceConstants.BaseRaces.HalfElf,
                    RaceConstants.BaseRaces.HalfOrc,
                    RaceConstants.BaseRaces.HighElf,
                    RaceConstants.BaseRaces.HillDwarf,
                    RaceConstants.BaseRaces.Human,
                    RaceConstants.BaseRaces.LightfootHalfling,
                    RaceConstants.BaseRaces.TallfellowHalfling,
                    RaceConstants.BaseRaces.WildElf,
                    RaceConstants.BaseRaces.WoodElf
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            BaseRaceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.EvilBase);
        }

        [TestCase("Evil Base Race Randomizer")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }
    }
}
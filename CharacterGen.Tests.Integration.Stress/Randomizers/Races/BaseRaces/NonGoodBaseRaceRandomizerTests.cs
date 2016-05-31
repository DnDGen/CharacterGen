using CharacterGen.Races;
using CharacterGen.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class NonGoodBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        protected override IEnumerable<string> allowedBaseRaces
        {
            get
            {
                return new[] {
                    RaceConstants.BaseRaces.Bugbear,
                    RaceConstants.BaseRaces.DeepDwarf,
                    RaceConstants.BaseRaces.DeepHalfling,
                    RaceConstants.BaseRaces.Derro,
                    RaceConstants.BaseRaces.Doppelganger,
                    RaceConstants.BaseRaces.Drow,
                    RaceConstants.BaseRaces.DuergarDwarf,
                    RaceConstants.BaseRaces.ForestGnome,
                    RaceConstants.BaseRaces.Gnoll,
                    RaceConstants.BaseRaces.Goblin,
                    RaceConstants.BaseRaces.GrayElf,
                    RaceConstants.BaseRaces.HalfElf,
                    RaceConstants.BaseRaces.HalfOrc,
                    RaceConstants.BaseRaces.HighElf,
                    RaceConstants.BaseRaces.HillDwarf,
                    RaceConstants.BaseRaces.Hobgoblin,
                    RaceConstants.BaseRaces.Human,
                    RaceConstants.BaseRaces.Kobold,
                    RaceConstants.BaseRaces.LightfootHalfling,
                    RaceConstants.BaseRaces.Lizardfolk,
                    RaceConstants.BaseRaces.MindFlayer,
                    RaceConstants.BaseRaces.Minotaur,
                    RaceConstants.BaseRaces.MountainDwarf,
                    RaceConstants.BaseRaces.Ogre,
                    RaceConstants.BaseRaces.OgreMage,
                    RaceConstants.BaseRaces.Orc,
                    RaceConstants.BaseRaces.RockGnome,
                    RaceConstants.BaseRaces.TallfellowHalfling,
                    RaceConstants.BaseRaces.Tiefling,
                    RaceConstants.BaseRaces.Troglodyte,
                    RaceConstants.BaseRaces.WildElf,
                    RaceConstants.BaseRaces.WoodElf
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            BaseRaceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.NonGoodBase);
        }

        [TestCase("Non-Good Base Race Randomizer")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }
    }
}
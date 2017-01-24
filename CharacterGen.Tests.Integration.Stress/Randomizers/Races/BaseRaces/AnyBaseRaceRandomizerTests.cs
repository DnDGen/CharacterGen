using CharacterGen.Races;
using CharacterGen.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class AnyBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        protected override IEnumerable<string> allowedBaseRaces
        {
            get
            {
                return new[] {
                    RaceConstants.BaseRaces.Aasimar,
                    RaceConstants.BaseRaces.Bugbear,
                    RaceConstants.BaseRaces.Centaur,
                    RaceConstants.BaseRaces.CloudGiant,
                    RaceConstants.BaseRaces.DeepDwarf,
                    RaceConstants.BaseRaces.DeepHalfling,
                    RaceConstants.BaseRaces.Derro,
                    RaceConstants.BaseRaces.Doppelganger,
                    RaceConstants.BaseRaces.Drow,
                    RaceConstants.BaseRaces.DuergarDwarf,
                    RaceConstants.BaseRaces.FireGiant,
                    RaceConstants.BaseRaces.ForestGnome,
                    RaceConstants.BaseRaces.FrostGiant,
                    RaceConstants.BaseRaces.Gnoll,
                    RaceConstants.BaseRaces.Goblin,
                    RaceConstants.BaseRaces.GrayElf,
                    RaceConstants.BaseRaces.Grimlock,
                    RaceConstants.BaseRaces.HalfElf,
                    RaceConstants.BaseRaces.HalfOrc,
                    RaceConstants.BaseRaces.Harpy,
                    RaceConstants.BaseRaces.HighElf,
                    RaceConstants.BaseRaces.HillDwarf,
                    RaceConstants.BaseRaces.HillGiant,
                    RaceConstants.BaseRaces.Hobgoblin,
                    RaceConstants.BaseRaces.Human,
                    RaceConstants.BaseRaces.Janni,
                    RaceConstants.BaseRaces.Kobold,
                    RaceConstants.BaseRaces.LightfootHalfling,
                    RaceConstants.BaseRaces.Lizardfolk,
                    RaceConstants.BaseRaces.MindFlayer,
                    RaceConstants.BaseRaces.Minotaur,
                    RaceConstants.BaseRaces.MountainDwarf,
                    RaceConstants.BaseRaces.Ogre,
                    RaceConstants.BaseRaces.OgreMage,
                    RaceConstants.BaseRaces.Orc,
                    RaceConstants.BaseRaces.Pixie,
                    RaceConstants.BaseRaces.Rakshasa,
                    RaceConstants.BaseRaces.RockGnome,
                    RaceConstants.BaseRaces.Satyr,
                    RaceConstants.BaseRaces.Scorpionfolk,
                    RaceConstants.BaseRaces.StoneGiant,
                    RaceConstants.BaseRaces.StormGiant,
                    RaceConstants.BaseRaces.Svirfneblin,
                    RaceConstants.BaseRaces.TallfellowHalfling,
                    RaceConstants.BaseRaces.Tiefling,
                    RaceConstants.BaseRaces.Troglodyte,
                    RaceConstants.BaseRaces.Troll,
                    RaceConstants.BaseRaces.WildElf,
                    RaceConstants.BaseRaces.WoodElf
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            BaseRaceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.AnyBase);
        }

        [Test]
        public void StressBaseRace()
        {
            Stress(AssertBaseRace);
        }
    }
}
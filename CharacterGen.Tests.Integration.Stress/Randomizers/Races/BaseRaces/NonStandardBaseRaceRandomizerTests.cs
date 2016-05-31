using CharacterGen.Races;
using CharacterGen.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class NonStandardBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        protected override IEnumerable<string> allowedBaseRaces
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

        [SetUp]
        public void Setup()
        {
            BaseRaceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.NonStandardBase);
        }

        [TestCase("Non-Standard Base Race Randomizer")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }
    }
}
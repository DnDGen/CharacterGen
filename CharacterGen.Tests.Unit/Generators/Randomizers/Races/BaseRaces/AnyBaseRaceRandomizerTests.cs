using CharacterGen.Races;
using CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class AnyBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        protected override IEnumerable<String> baseRaces
        {
            get
            {
                return new[] {
                    RaceConstants.BaseRaces.Aasimar,
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
                    RaceConstants.BaseRaces.Svirfneblin,
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
            randomizer = new AnyBaseRaceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object, generator);
        }

        [Test]
        public void AllBaseRacesAllowed()
        {
            var allBaseRaces = randomizer.GetAllPossible(alignment, characterClass);
            foreach (var baseRace in baseRaces)
                Assert.That(allBaseRaces, Contains.Item(baseRace));
        }
    }
}
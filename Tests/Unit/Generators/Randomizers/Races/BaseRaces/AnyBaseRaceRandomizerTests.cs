using System;
using System.Collections.Generic;
using NPCGen.Common.Races;
using NPCGen.Generators.Randomizers.Races.BaseRaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class AnyBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        protected override IEnumerable<String> baseRaceIds
        {
            get
            {
                return new[] {
                    RaceConstants.BaseRaces.AasimarId,
                    RaceConstants.BaseRaces.BugbearId,
                    RaceConstants.BaseRaces.DeepDwarfId,
                    RaceConstants.BaseRaces.DeepHalflingId,
                    RaceConstants.BaseRaces.DerroId,
                    RaceConstants.BaseRaces.DoppelgangerId,
                    RaceConstants.BaseRaces.DrowId,
                    RaceConstants.BaseRaces.DuergarDwarfId,
                    RaceConstants.BaseRaces.ForestGnomeId,
                    RaceConstants.BaseRaces.GnollId,
                    RaceConstants.BaseRaces.GoblinId,
                    RaceConstants.BaseRaces.GrayElfId,
                    RaceConstants.BaseRaces.HalfElfId,
                    RaceConstants.BaseRaces.HalfOrcId,
                    RaceConstants.BaseRaces.HighElfId,
                    RaceConstants.BaseRaces.HillDwarfId,
                    RaceConstants.BaseRaces.HobgoblinId,
                    RaceConstants.BaseRaces.HumanId,
                    RaceConstants.BaseRaces.KoboldId,
                    RaceConstants.BaseRaces.LightfootHalflingId,
                    RaceConstants.BaseRaces.LizardfolkId,
                    RaceConstants.BaseRaces.MindFlayerId,
                    RaceConstants.BaseRaces.MinotaurId,
                    RaceConstants.BaseRaces.MountainDwarfId,
                    RaceConstants.BaseRaces.OgreId,
                    RaceConstants.BaseRaces.OgreMageId,
                    RaceConstants.BaseRaces.OrcId,
                    RaceConstants.BaseRaces.RockGnomeId,
                    RaceConstants.BaseRaces.SvirfneblinId,
                    RaceConstants.BaseRaces.TallfellowHalflingId,
                    RaceConstants.BaseRaces.TieflingId,
                    RaceConstants.BaseRaces.TroglodyteId,
                    RaceConstants.BaseRaces.WildElfId,
                    RaceConstants.BaseRaces.WoodElfId
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            randomizer = new AnyBaseRaceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object, mockNameSelector.Object);
        }

        [Test]
        public void AllBaseRacesAllowed()
        {
            var allBaseRaces = randomizer.GetAllPossibleIds(String.Empty, characterClass);
            foreach (var baseRace in baseRaceIds)
                Assert.That(allBaseRaces, Contains.Item(baseRace));
        }
    }
}
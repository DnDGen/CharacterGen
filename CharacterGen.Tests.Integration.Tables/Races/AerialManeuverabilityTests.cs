using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Races
{
    [TestFixture]
    public class AerialManeuverabilityTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.AerialManeuverability; }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var metaraceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.MetaraceGroups);

            var names = baseRaceGroups[GroupConstants.All].Union(metaraceGroups[GroupConstants.All]);
            AssertCollectionNames(names);
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, "")]
        [TestCase(RaceConstants.BaseRaces.Azer, "")]
        [TestCase(RaceConstants.BaseRaces.BlueSlaad, "")]
        [TestCase(RaceConstants.BaseRaces.Bugbear, "")]
        [TestCase(RaceConstants.BaseRaces.Centaur, "")]
        [TestCase(RaceConstants.BaseRaces.CloudGiant, "")]
        [TestCase(RaceConstants.BaseRaces.DeathSlaad, "")]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, "")]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, "")]
        [TestCase(RaceConstants.BaseRaces.Derro, "")]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, "")]
        [TestCase(RaceConstants.BaseRaces.Drow, "")]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, "")]
        [TestCase(RaceConstants.BaseRaces.FireGiant, "")]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, "")]
        [TestCase(RaceConstants.BaseRaces.FrostGiant, "")]
        [TestCase(RaceConstants.BaseRaces.Gargoyle, "Average Maneuverability")]
        [TestCase(RaceConstants.BaseRaces.Gnoll, "")]
        [TestCase(RaceConstants.BaseRaces.Goblin, "")]
        [TestCase(RaceConstants.BaseRaces.GrayElf, "")]
        [TestCase(RaceConstants.BaseRaces.GraySlaad, "")]
        [TestCase(RaceConstants.BaseRaces.GreenSlaad, "")]
        [TestCase(RaceConstants.BaseRaces.Grimlock, "")]
        [TestCase(RaceConstants.BaseRaces.HalfElf, "")]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, "")]
        [TestCase(RaceConstants.BaseRaces.Harpy, "Average Maneuverability")]
        [TestCase(RaceConstants.BaseRaces.HighElf, "")]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, "")]
        [TestCase(RaceConstants.BaseRaces.HillGiant, "")]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, "")]
        [TestCase(RaceConstants.BaseRaces.HoundArchon, "")]
        [TestCase(RaceConstants.BaseRaces.Human, "")]
        [TestCase(RaceConstants.BaseRaces.Janni, "Perfect Maneuverability")]
        [TestCase(RaceConstants.BaseRaces.Kobold, "")]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, "")]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, "")]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, "")]
        [TestCase(RaceConstants.BaseRaces.Minotaur, "")]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, "")]
        [TestCase(RaceConstants.BaseRaces.Ogre, "")]
        [TestCase(RaceConstants.BaseRaces.OgreMage, "Good Maneuverability")]
        [TestCase(RaceConstants.BaseRaces.Orc, "")]
        [TestCase(RaceConstants.BaseRaces.Pixie, "Good Maneuverability")]
        [TestCase(RaceConstants.BaseRaces.Rakshasa, "")]
        [TestCase(RaceConstants.BaseRaces.RedSlaad, "")]
        [TestCase(RaceConstants.BaseRaces.RockGnome, "")]
        [TestCase(RaceConstants.BaseRaces.Satyr, "")]
        [TestCase(RaceConstants.BaseRaces.Scorpionfolk, "")]
        [TestCase(RaceConstants.BaseRaces.StoneGiant, "")]
        [TestCase(RaceConstants.BaseRaces.StormGiant, "")]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, "")]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, "")]
        [TestCase(RaceConstants.BaseRaces.Tiefling, "")]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, "")]
        [TestCase(RaceConstants.BaseRaces.Troll, "")]
        [TestCase(RaceConstants.BaseRaces.WildElf, "")]
        [TestCase(RaceConstants.BaseRaces.WoodElf, "")]
        [TestCase(RaceConstants.BaseRaces.YuanTiAbomination, "")]
        [TestCase(RaceConstants.BaseRaces.YuanTiHalfblood, "")]
        [TestCase(RaceConstants.BaseRaces.YuanTiPureblood, "")]
        [TestCase(RaceConstants.Metaraces.Ghost, "Perfect Maneuverability")]
        [TestCase(RaceConstants.Metaraces.HalfCelestial, "Good Maneuverability")]
        [TestCase(RaceConstants.Metaraces.HalfDragon, "Average Maneuverability")]
        [TestCase(RaceConstants.Metaraces.HalfFiend, "Average Maneuverability")]
        [TestCase(RaceConstants.Metaraces.Lich, "")]
        [TestCase(RaceConstants.Metaraces.Mummy, "")]
        [TestCase(RaceConstants.Metaraces.None, "")]
        [TestCase(RaceConstants.Metaraces.Vampire, "")]
        [TestCase(RaceConstants.Metaraces.Werebear, "")]
        [TestCase(RaceConstants.Metaraces.Wereboar, "")]
        [TestCase(RaceConstants.Metaraces.Wererat, "")]
        [TestCase(RaceConstants.Metaraces.Weretiger, "")]
        [TestCase(RaceConstants.Metaraces.Werewolf, "")]
        public void AerialManeuverability(string name, string maneuverability)
        {
            DistinctCollection(name, maneuverability);
        }

        [Test]
        public void AerialSpeedOf0HasEmptyManeuverability()
        {
            var aerialSpeeds = CollectionsMapper.Map(TableNameConstants.Set.Adjustments.AerialSpeeds);
            var aerialManeuverabilities = CollectionsMapper.Map(TableNameConstants.Set.Collection.AerialManeuverability);

            var raceWithNoAerialSpeed = aerialSpeeds.Where(s => s.Value.Single() == "0").Select(s => s.Key);

            foreach (var race in raceWithNoAerialSpeed)
                Assert.That(aerialManeuverabilities[race].Single(), Is.Empty, race);
        }

        [Test]
        public void PositiveAerialSpeedHasManeuverability()
        {
            var aerialSpeeds = CollectionsMapper.Map(TableNameConstants.Set.Adjustments.AerialSpeeds);
            var aerialManeuverabilities = CollectionsMapper.Map(TableNameConstants.Set.Collection.AerialManeuverability);

            var raceWithAerialSpeed = aerialSpeeds.Where(s => s.Value.Single() != "0").Select(s => s.Key);

            foreach (var race in raceWithAerialSpeed)
                Assert.That(aerialManeuverabilities[race].Single(), Is.Not.Empty, race);
        }
    }
}
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Weights
{
    [TestFixture]
    public class FemaleWeightsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.GENDERWeights, "Female"); }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            AssertCollectionNames(allBaseRaces);
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, 90)]
        [TestCase(RaceConstants.BaseRaces.AquaticElf, 80)]
        [TestCase(RaceConstants.BaseRaces.Azer, 100)]
        [TestCase(RaceConstants.BaseRaces.BlueSlaad, 900)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 250)]
        [TestCase(RaceConstants.BaseRaces.Centaur, 365)]
        [TestCase(RaceConstants.BaseRaces.CloudGiant, 4200)]
        [TestCase(RaceConstants.BaseRaces.DeathSlaad, 85)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 100)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 25)]
        [TestCase(RaceConstants.BaseRaces.Derro, 100)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 120)]
        [TestCase(RaceConstants.BaseRaces.Drow, 80)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 100)]
        [TestCase(RaceConstants.BaseRaces.FireGiant, 6200)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 35)]
        [TestCase(RaceConstants.BaseRaces.FrostGiant, 2000)]
        [TestCase(RaceConstants.BaseRaces.Gargoyle, 220)]
        [TestCase(RaceConstants.BaseRaces.Githyanki, 130)]
        [TestCase(RaceConstants.BaseRaces.Githzerai, 130)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 150)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 25)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 80)]
        [TestCase(RaceConstants.BaseRaces.GraySlaad, 85)]
        [TestCase(RaceConstants.BaseRaces.GreenSlaad, 900)]
        [TestCase(RaceConstants.BaseRaces.Grimlock, 85)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 80)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 110)]
        [TestCase(RaceConstants.BaseRaces.Harpy, 50)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 80)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 100)]
        [TestCase(RaceConstants.BaseRaces.HillGiant, 300)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 145)]
        [TestCase(RaceConstants.BaseRaces.HoundArchon, 150)]
        [TestCase(RaceConstants.BaseRaces.Human, 85)]
        [TestCase(RaceConstants.BaseRaces.Janni, 85)]
        [TestCase(RaceConstants.BaseRaces.Kapoacinth, 220)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 20)]
        [TestCase(RaceConstants.BaseRaces.KuoToa, 90)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 25)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 130)]
        [TestCase(RaceConstants.BaseRaces.Locathah, 120)]
        [TestCase(RaceConstants.BaseRaces.Merfolk, 35)]
        [TestCase(RaceConstants.BaseRaces.Merrow, 600)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 105)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 400)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 100)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 600)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 600)]
        [TestCase(RaceConstants.BaseRaces.Orc, 120)]
        [TestCase(RaceConstants.BaseRaces.Pixie, 4)]
        [TestCase(RaceConstants.BaseRaces.Rakshasa, 85)]
        [TestCase(RaceConstants.BaseRaces.RedSlaad, 550)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 35)]
        [TestCase(RaceConstants.BaseRaces.Sahuagin, 85)]
        [TestCase(RaceConstants.BaseRaces.Satyr, 130)]
        [TestCase(RaceConstants.BaseRaces.Scorpionfolk, 260)]
        [TestCase(RaceConstants.BaseRaces.Scrag, 450)]
        [TestCase(RaceConstants.BaseRaces.StoneGiant, 700)]
        [TestCase(RaceConstants.BaseRaces.StormGiant, 11200)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 35)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 25)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 90)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 120)]
        [TestCase(RaceConstants.BaseRaces.Troll, 450)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 80)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 80)]
        [TestCase(RaceConstants.BaseRaces.YuanTiAbomination, 196)]
        [TestCase(RaceConstants.BaseRaces.YuanTiHalfblood, 85)]
        [TestCase(RaceConstants.BaseRaces.YuanTiPureblood, 85)]
        public void FemaleWeight(string name, int adjustment)
        {
            Adjustment(name, adjustment);
        }
    }
}

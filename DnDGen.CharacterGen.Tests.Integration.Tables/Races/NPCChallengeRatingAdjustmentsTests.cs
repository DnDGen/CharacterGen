using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races
{
    [TestFixture]
    public class NPCChallengeRatingAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName => TableNameConstants.Set.Adjustments.NPCChallengeRatingAdjustments;

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = GetTable(TableNameConstants.Set.Collection.BaseRaceGroups);
            AssertCollectionNames(baseRaceGroups[GroupConstants.All]);
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, -1)]
        [TestCase(RaceConstants.BaseRaces.AquaticElf, -1)]
        [TestCase(RaceConstants.BaseRaces.Azer, -1)]
        [TestCase(RaceConstants.BaseRaces.BlueSlaad, -1)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, -1)]
        [TestCase(RaceConstants.BaseRaces.Centaur, -1)]
        [TestCase(RaceConstants.BaseRaces.CloudGiant, -1)]
        [TestCase(RaceConstants.BaseRaces.DeathSlaad, -1)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, -1)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, -1)]
        [TestCase(RaceConstants.BaseRaces.Derro, -1)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, -1)]
        [TestCase(RaceConstants.BaseRaces.Drow, 0)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.FireGiant, -1)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, -1)]
        [TestCase(RaceConstants.BaseRaces.FrostGiant, -1)]
        [TestCase(RaceConstants.BaseRaces.Gargoyle, -1)]
        [TestCase(RaceConstants.BaseRaces.Githyanki, 0)]
        [TestCase(RaceConstants.BaseRaces.Githzerai, 0)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, -1)]
        [TestCase(RaceConstants.BaseRaces.Goblin, -2)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, -1)]
        [TestCase(RaceConstants.BaseRaces.GraySlaad, -1)]
        [TestCase(RaceConstants.BaseRaces.GreenSlaad, -1)]
        [TestCase(RaceConstants.BaseRaces.Grimlock, -1)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, -1)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, -1)]
        [TestCase(RaceConstants.BaseRaces.Harpy, -1)]
        [TestCase(RaceConstants.BaseRaces.HighElf, -1)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, -1)]
        [TestCase(RaceConstants.BaseRaces.HillGiant, -1)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, -1)]
        [TestCase(RaceConstants.BaseRaces.HoundArchon, -1)]
        [TestCase(RaceConstants.BaseRaces.Human, -1)]
        [TestCase(RaceConstants.BaseRaces.Janni, -1)]
        [TestCase(RaceConstants.BaseRaces.Kapoacinth, -1)]
        [TestCase(RaceConstants.BaseRaces.Kobold, -3)]
        [TestCase(RaceConstants.BaseRaces.KuoToa, -1)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, -1)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, -1)]
        [TestCase(RaceConstants.BaseRaces.Locathah, -1)]
        [TestCase(RaceConstants.BaseRaces.Merfolk, -1)]
        [TestCase(RaceConstants.BaseRaces.Merrow, -1)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, -1)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, -1)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, -1)]
        [TestCase(RaceConstants.BaseRaces.Ogre, -1)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, -1)]
        [TestCase(RaceConstants.BaseRaces.Orc, -1)]
        [TestCase(RaceConstants.BaseRaces.Pixie, -1)]
        [TestCase(RaceConstants.BaseRaces.Rakshasa, -1)]
        [TestCase(RaceConstants.BaseRaces.RedSlaad, -1)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, -1)]
        [TestCase(RaceConstants.BaseRaces.Sahuagin, -1)]
        [TestCase(RaceConstants.BaseRaces.Satyr, -1)]
        [TestCase(RaceConstants.BaseRaces.Scorpionfolk, -1)]
        [TestCase(RaceConstants.BaseRaces.Scrag, -1)]
        [TestCase(RaceConstants.BaseRaces.StoneGiant, -1)]
        [TestCase(RaceConstants.BaseRaces.StormGiant, -1)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 0)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, -1)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, -1)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, -1)]
        [TestCase(RaceConstants.BaseRaces.Troll, -1)]
        [TestCase(RaceConstants.BaseRaces.WildElf, -1)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, -1)]
        [TestCase(RaceConstants.BaseRaces.YuanTiAbomination, -1)]
        [TestCase(RaceConstants.BaseRaces.YuanTiHalfblood, -1)]
        [TestCase(RaceConstants.BaseRaces.YuanTiPureblood, -1)]
        public void ChallengeRating(string name, int challengeRating)
        {
            base.Adjustment(name, challengeRating);
        }
    }
}

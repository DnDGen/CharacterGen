using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Skills
{
    [TestFixture]
    public class SkillPointsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.SkillPoints; }
        }

        [Test]
        public override void CollectionNames()
        {
            var classGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.ClassNameGroups);
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);

            var names = classGroups[GroupConstants.All].Union(baseRaceGroups[GroupConstants.Monsters]);

            AssertCollectionNames(names);
        }

        [TestCase(CharacterClassConstants.Adept, 2)]
        [TestCase(CharacterClassConstants.Aristocrat, 4)]
        [TestCase(CharacterClassConstants.Barbarian, 4)]
        [TestCase(CharacterClassConstants.Bard, 6)]
        [TestCase(CharacterClassConstants.Cleric, 2)]
        [TestCase(CharacterClassConstants.Commoner, 2)]
        [TestCase(CharacterClassConstants.Druid, 4)]
        [TestCase(CharacterClassConstants.Expert, 6)]
        [TestCase(CharacterClassConstants.Fighter, 2)]
        [TestCase(CharacterClassConstants.Monk, 4)]
        [TestCase(CharacterClassConstants.Paladin, 2)]
        [TestCase(CharacterClassConstants.Ranger, 6)]
        [TestCase(CharacterClassConstants.Rogue, 8)]
        [TestCase(CharacterClassConstants.Sorcerer, 2)]
        [TestCase(CharacterClassConstants.Warrior, 2)]
        [TestCase(CharacterClassConstants.Wizard, 2)]
        [TestCase(RaceConstants.BaseRaces.Azer, 8)]
        [TestCase(RaceConstants.BaseRaces.BlueSlaad, 2)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 2)]
        [TestCase(RaceConstants.BaseRaces.Centaur, 2)]
        [TestCase(RaceConstants.BaseRaces.CloudGiant, 2)]
        [TestCase(RaceConstants.BaseRaces.DeathSlaad, 2)]
        [TestCase(RaceConstants.BaseRaces.Derro, 2)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 2)]
        [TestCase(RaceConstants.BaseRaces.FireGiant, 2)]
        [TestCase(RaceConstants.BaseRaces.FrostGiant, 2)]
        [TestCase(RaceConstants.BaseRaces.Gargoyle, 2)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 2)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 2)]
        [TestCase(RaceConstants.BaseRaces.GraySlaad, 2)]
        [TestCase(RaceConstants.BaseRaces.GreenSlaad, 2)]
        [TestCase(RaceConstants.BaseRaces.Grimlock, 2)]
        [TestCase(RaceConstants.BaseRaces.Harpy, 2)]
        [TestCase(RaceConstants.BaseRaces.HillGiant, 2)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 2)]
        [TestCase(RaceConstants.BaseRaces.HoundArchon, 8)]
        [TestCase(RaceConstants.BaseRaces.Janni, 8)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 2)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 2)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 2)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 2)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 2)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 2)]
        [TestCase(RaceConstants.BaseRaces.Orc, 2)]
        [TestCase(RaceConstants.BaseRaces.Pixie, 2)]
        [TestCase(RaceConstants.BaseRaces.Rakshasa, 8)]
        [TestCase(RaceConstants.BaseRaces.RedSlaad, 2)]
        [TestCase(RaceConstants.BaseRaces.Satyr, 6)]
        [TestCase(RaceConstants.BaseRaces.Scorpionfolk, 2)]
        [TestCase(RaceConstants.BaseRaces.StoneGiant, 2)]
        [TestCase(RaceConstants.BaseRaces.StormGiant, 2)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 2)]
        [TestCase(RaceConstants.BaseRaces.Troll, 2)]
        [TestCase(RaceConstants.BaseRaces.YuanTiAbomination, 2)]
        [TestCase(RaceConstants.BaseRaces.YuanTiHalfblood, 2)]
        [TestCase(RaceConstants.BaseRaces.YuanTiPureblood, 2)]
        public void SkillPoints(string name, int points)
        {
            base.Adjustment(name, points);
        }
    }
}
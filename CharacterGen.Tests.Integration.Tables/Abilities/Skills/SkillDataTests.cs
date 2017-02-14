using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Skills
{
    [TestFixture]
    public class SkillDataTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.SkillData; }
        }

        [Test]
        public override void CollectionNames()
        {
            var classSkills = CollectionsMapper.Map(TableNameConstants.Set.Collection.ClassSkills);
            var crossClassSkills = CollectionsMapper.Map(TableNameConstants.Set.Collection.CrossClassSkills);

            var allClassSkills = classSkills.Values.SelectMany(v => v);
            var allCrossClassSkills = crossClassSkills.Values.SelectMany(v => v);

            var names = allClassSkills.Union(allCrossClassSkills);

            AssertCollectionNames(names);
        }

        [TestCase(SkillConstants.Appraise, StatConstants.Intelligence)]
        [TestCase(SkillConstants.Balance, StatConstants.Dexterity)]
        [TestCase(SkillConstants.Bluff, StatConstants.Charisma)]
        [TestCase(SkillConstants.Climb, StatConstants.Strength)]
        [TestCase(SkillConstants.Concentration, StatConstants.Constitution)]
        [TestCase(SkillConstants.DecipherScript, StatConstants.Intelligence)]
        [TestCase(SkillConstants.Diplomacy, StatConstants.Charisma)]
        [TestCase(SkillConstants.DisableDevice, StatConstants.Intelligence)]
        [TestCase(SkillConstants.Disguise, StatConstants.Charisma)]
        [TestCase(SkillConstants.EscapeArtist, StatConstants.Dexterity)]
        [TestCase(SkillConstants.Forgery, StatConstants.Intelligence)]
        [TestCase(SkillConstants.GatherInformation, StatConstants.Charisma)]
        [TestCase(SkillConstants.HandleAnimal, StatConstants.Charisma)]
        [TestCase(SkillConstants.Heal, StatConstants.Wisdom)]
        [TestCase(SkillConstants.Hide, StatConstants.Dexterity)]
        [TestCase(SkillConstants.Intimidate, StatConstants.Charisma)]
        [TestCase(SkillConstants.Jump, StatConstants.Strength)]
        [TestCase(SkillConstants.KnowledgeArcana, StatConstants.Intelligence)]
        [TestCase(SkillConstants.KnowledgeArchitectureAndEngineering, StatConstants.Intelligence)]
        [TestCase(SkillConstants.KnowledgeDungeoneering, StatConstants.Intelligence)]
        [TestCase(SkillConstants.KnowledgeGeography, StatConstants.Intelligence)]
        [TestCase(SkillConstants.KnowledgeHistory, StatConstants.Intelligence)]
        [TestCase(SkillConstants.KnowledgeLocal, StatConstants.Intelligence)]
        [TestCase(SkillConstants.KnowledgeNature, StatConstants.Intelligence)]
        [TestCase(SkillConstants.KnowledgeNobilityAndRoyalty, StatConstants.Intelligence)]
        [TestCase(SkillConstants.KnowledgeReligion, StatConstants.Intelligence)]
        [TestCase(SkillConstants.KnowledgeThePlanes, StatConstants.Intelligence)]
        [TestCase(SkillConstants.Listen, StatConstants.Wisdom)]
        [TestCase(SkillConstants.MoveSilently, StatConstants.Dexterity)]
        [TestCase(SkillConstants.OpenLock, StatConstants.Dexterity)]
        [TestCase(SkillConstants.Perform, StatConstants.Charisma)]
        [TestCase(SkillConstants.Ride, StatConstants.Dexterity)]
        [TestCase(SkillConstants.Search, StatConstants.Intelligence)]
        [TestCase(SkillConstants.SenseMotive, StatConstants.Wisdom)]
        [TestCase(SkillConstants.SleightOfHand, StatConstants.Dexterity)]
        [TestCase(SkillConstants.Spellcraft, StatConstants.Intelligence)]
        [TestCase(SkillConstants.Spot, StatConstants.Wisdom)]
        [TestCase(SkillConstants.Survival, StatConstants.Wisdom)]
        [TestCase(SkillConstants.Swim, StatConstants.Strength)]
        [TestCase(SkillConstants.Tumble, StatConstants.Dexterity)]
        [TestCase(SkillConstants.UseMagicDevice, StatConstants.Charisma)]
        [TestCase(SkillConstants.UseRope, StatConstants.Dexterity)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer + SkillConstants.Knowledge, StatConstants.Intelligence, SkillConstants.Knowledge, 1)]
        [TestCase(RaceConstants.BaseRaces.DeathSlaad + SkillConstants.Knowledge, StatConstants.Intelligence, SkillConstants.Knowledge, 2)]
        [TestCase(RaceConstants.BaseRaces.YuanTiPureblood + SkillConstants.Knowledge, StatConstants.Intelligence, SkillConstants.Knowledge, 1)]
        [TestCase(RaceConstants.BaseRaces.YuanTiHalfblood + SkillConstants.Knowledge, StatConstants.Intelligence, SkillConstants.Knowledge, 2)]
        [TestCase(RaceConstants.BaseRaces.YuanTiAbomination + SkillConstants.Knowledge, StatConstants.Intelligence, SkillConstants.Knowledge, 2)]
        public void SkillSelectionData(string name, string baseStat, string skillName = "", int randomFoci = 0)
        {
            if (string.IsNullOrEmpty(skillName))
                skillName = name;

            var collection = new string[3];
            collection[DataIndexConstants.SkillSelectionData.BaseStatName] = baseStat;
            collection[DataIndexConstants.SkillSelectionData.RandomFociQuantity] = randomFoci.ToString();
            collection[DataIndexConstants.SkillSelectionData.SkillName] = skillName;

            base.OrderedCollection(name, collection);
        }
    }
}
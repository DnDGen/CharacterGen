using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.Domain.Tables;
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
            var skillGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SkillGroups);
            var featFoci = CollectionsMapper.Map(TableNameConstants.Set.Collection.FeatFoci);

            var allClassSkills = classSkills.Values.SelectMany(v => v);

            var names = allClassSkills
                .Union(skillGroups[GroupConstants.All])
                .Union(skillGroups[GroupConstants.Untrained])
                .Union(featFoci[GroupConstants.Skills]);

            AssertCollectionNames(names);
        }

        [TestCase(SkillConstants.Appraise, StatConstants.Intelligence)]
        [TestCase(SkillConstants.Balance, StatConstants.Dexterity)]
        [TestCase(SkillConstants.Bluff, StatConstants.Charisma)]
        [TestCase(SkillConstants.Climb, StatConstants.Strength)]
        [TestCase(SkillConstants.Concentration, StatConstants.Constitution)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Craft, 1)]
        [TestCase(SkillConstants.Craft + "2", StatConstants.Intelligence, SkillConstants.Craft, 2)]
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
        [TestCase(SkillConstants.Knowledge, StatConstants.Intelligence, SkillConstants.Knowledge, 1)]
        [TestCase(SkillConstants.Knowledge + "2", StatConstants.Intelligence, SkillConstants.Knowledge, 2)]
        [TestCase(SkillConstants.Knowledge + GroupConstants.All, StatConstants.Intelligence, SkillConstants.Knowledge, SkillConstants.Foci.QuantityOfAll)]
        [TestCase(SkillConstants.Listen, StatConstants.Wisdom)]
        [TestCase(SkillConstants.MoveSilently, StatConstants.Dexterity)]
        [TestCase(SkillConstants.OpenLock, StatConstants.Dexterity)]
        [TestCase(SkillConstants.Perform, StatConstants.Charisma, SkillConstants.Perform, 1)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Profession, 1)]
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
        public void SkillSelectionData(string name, string baseStat, string skillName = "", int randomFoci = 0)
        {
            if (string.IsNullOrEmpty(skillName))
                skillName = name;

            var collection = new string[4];
            collection[DataIndexConstants.SkillSelectionData.BaseStatName] = baseStat;
            collection[DataIndexConstants.SkillSelectionData.RandomFociQuantity] = randomFoci.ToString();
            collection[DataIndexConstants.SkillSelectionData.SkillName] = skillName;
            collection[DataIndexConstants.SkillSelectionData.Focus] = string.Empty;

            base.OrderedCollection(name, collection);
        }

        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Alchemy)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Armorsmithing)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Blacksmithing)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Bookbinding)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Bowmaking)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Brassmaking)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Brewing)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Candlemaking)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Cloth)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Coppersmithing)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Dyemaking)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Gemcutting)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Glass)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Goldsmithing)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Hatmaking)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Hornworking)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Jewelmaking)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Leather)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Locksmithing)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Mapmaking)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Milling)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Painting)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Parchmentmaking)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Pewtermaking)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Potterymaking)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Sculpting)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Shipmaking)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Shoemaking)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Silversmithing)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Skinning)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Soapmaking)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Stonemasonry)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Tanning)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Trapmaking)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Weaponsmithing)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Weaving)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Wheelmaking)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Winemaking)]
        [TestCase(SkillConstants.Craft, StatConstants.Intelligence, SkillConstants.Foci.Craft.Woodworking)]
        [TestCase(SkillConstants.Knowledge, StatConstants.Intelligence, SkillConstants.Foci.Knowledge.Arcana)]
        [TestCase(SkillConstants.Knowledge, StatConstants.Intelligence, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)]
        [TestCase(SkillConstants.Knowledge, StatConstants.Intelligence, SkillConstants.Foci.Knowledge.Dungeoneering)]
        [TestCase(SkillConstants.Knowledge, StatConstants.Intelligence, SkillConstants.Foci.Knowledge.Geography)]
        [TestCase(SkillConstants.Knowledge, StatConstants.Intelligence, SkillConstants.Foci.Knowledge.History)]
        [TestCase(SkillConstants.Knowledge, StatConstants.Intelligence, SkillConstants.Foci.Knowledge.Local)]
        [TestCase(SkillConstants.Knowledge, StatConstants.Intelligence, SkillConstants.Foci.Knowledge.Nature)]
        [TestCase(SkillConstants.Knowledge, StatConstants.Intelligence, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)]
        [TestCase(SkillConstants.Knowledge, StatConstants.Intelligence, SkillConstants.Foci.Knowledge.Religion)]
        [TestCase(SkillConstants.Knowledge, StatConstants.Intelligence, SkillConstants.Foci.Knowledge.ThePlanes)]
        [TestCase(SkillConstants.Perform, StatConstants.Charisma, SkillConstants.Foci.Perform.Act)]
        [TestCase(SkillConstants.Perform, StatConstants.Charisma, SkillConstants.Foci.Perform.Comedy)]
        [TestCase(SkillConstants.Perform, StatConstants.Charisma, SkillConstants.Foci.Perform.Dance)]
        [TestCase(SkillConstants.Perform, StatConstants.Charisma, SkillConstants.Foci.Perform.KeyboardInstruments)]
        [TestCase(SkillConstants.Perform, StatConstants.Charisma, SkillConstants.Foci.Perform.Oratory)]
        [TestCase(SkillConstants.Perform, StatConstants.Charisma, SkillConstants.Foci.Perform.PercussionInstruments)]
        [TestCase(SkillConstants.Perform, StatConstants.Charisma, SkillConstants.Foci.Perform.Sing)]
        [TestCase(SkillConstants.Perform, StatConstants.Charisma, SkillConstants.Foci.Perform.StringInstruments)]
        [TestCase(SkillConstants.Perform, StatConstants.Charisma, SkillConstants.Foci.Perform.WindInstruments)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Adviser)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Alchemist)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.AnimalGroomer)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.AnimalTrainer)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Apothecary)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Appraiser)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Architect)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Armorer)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Barrister)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Blacksmith)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Bookbinder)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Bowyer)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Brazier)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Brewer)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Butler)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Carpenter)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Cartographer)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Cartwright)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Chandler)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.CityGuide)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Clerk)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Cobbler)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Coffinmaker)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Coiffeur)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Cook)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Coppersmith)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Craftsman)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Dowser)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Dyer)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Embalmer)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Engineer)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Entertainer)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.ExoticAnimalTrainer)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Farmer)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Fletcher)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Footman)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Gemcutter)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Goldsmith)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Governess)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Haberdasher)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Healer)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Horner)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Interpreter)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Jeweler)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Laborer)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Launderer)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Limner)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.LocalCourier)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Locksmith)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Maid)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Masseuse)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Matchmaker)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Midwife)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Miller)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Navigator)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Nursemaid)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.OutOfTownCourier)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Painter)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Parchmentmaker)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Pewterer)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Polisher)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Porter)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Potter)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Sage)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.SailorCrewmember)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.SailorMate)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Scribe)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Sculptor)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Shepherd)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Shipwright)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Silversmith)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Skinner)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Soapmaker)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Soothsayer)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Tanner)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Teacher)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Teamster)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Trader)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Valet)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Vintner)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Weaponsmith)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Weaver)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.Wheelwright)]
        [TestCase(SkillConstants.Profession, StatConstants.Wisdom, SkillConstants.Foci.Profession.WildernessGuide)]
        public void SkillSelectionData(string skillName, string baseStat, string focus)
        {
            var name = $"{skillName}/{focus}";

            var collection = new string[4];
            collection[DataIndexConstants.SkillSelectionData.BaseStatName] = baseStat;
            collection[DataIndexConstants.SkillSelectionData.RandomFociQuantity] = "0";
            collection[DataIndexConstants.SkillSelectionData.SkillName] = skillName;
            collection[DataIndexConstants.SkillSelectionData.Focus] = focus;

            base.OrderedCollection(name, collection);
        }
    }
}
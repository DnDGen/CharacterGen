using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.Infrastructure.Mappers.Collections;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Feats.Data.CharacterClasses
{
    [TestFixture]
    public class CrossClassFeatDataTests : TableTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Collection.ClassNameGroups;
            }
        }

        private CollectionMapper collectionsMapper;

        [SetUp]
        public void Setup()
        {
            collectionsMapper = GetNewInstanceOf<CollectionMapper>();
        }

        [Test]
        public void AllClassesHaveFeatDataTables()
        {
            var classNameGroups = collectionsMapper.Map(TableNameConstants.Set.Collection.ClassNameGroups);
            var allClasses = classNameGroups[GroupConstants.All];

            foreach (var className in allClasses)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, className);
                var featsData = collectionsMapper.Map(tableName);

                Assert.That(featsData, Is.Not.Null);
            }
        }

        [Test]
        public void AllDomainsHaveFeatDataTables()
        {
            var domains = new[]
            {
                CharacterClassConstants.Domains.Air,
                CharacterClassConstants.Domains.Animal,
                CharacterClassConstants.Domains.Chaos,
                CharacterClassConstants.Domains.Death,
                CharacterClassConstants.Domains.Destruction,
                CharacterClassConstants.Domains.Earth,
                CharacterClassConstants.Domains.Evil,
                CharacterClassConstants.Domains.Fire,
                CharacterClassConstants.Domains.Good,
                CharacterClassConstants.Domains.Healing,
                CharacterClassConstants.Domains.Knowledge,
                CharacterClassConstants.Domains.Law,
                CharacterClassConstants.Domains.Luck,
                CharacterClassConstants.Domains.Magic,
                CharacterClassConstants.Domains.Plant,
                CharacterClassConstants.Domains.Protection,
                CharacterClassConstants.Domains.Strength,
                CharacterClassConstants.Domains.Sun,
                CharacterClassConstants.Domains.Travel,
                CharacterClassConstants.Domains.Trickery,
                CharacterClassConstants.Domains.War,
                CharacterClassConstants.Domains.Water,
            };

            foreach (var domain in domains)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, domain);
                var featsData = collectionsMapper.Map(tableName);

                Assert.That(featsData, Is.Not.Null);
            }
        }

        [Test]
        public void AllSchoolsHaveFeatDataTables()
        {
            var schools = new[]
            {
                CharacterClassConstants.Schools.Abjuration,
                CharacterClassConstants.Schools.Conjuration,
                CharacterClassConstants.Schools.Divination,
                CharacterClassConstants.Schools.Enchantment,
                CharacterClassConstants.Schools.Evocation,
                CharacterClassConstants.Schools.Illusion,
                CharacterClassConstants.Schools.Necromancy,
                CharacterClassConstants.Schools.Transmutation,
            };

            foreach (var school in schools)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, school);
                var featsData = collectionsMapper.Map(tableName);

                Assert.That(featsData, Is.Not.Null);
            }
        }
    }
}

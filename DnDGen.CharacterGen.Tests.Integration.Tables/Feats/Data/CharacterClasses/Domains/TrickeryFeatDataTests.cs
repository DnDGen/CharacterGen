using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Feats.Data.CharacterClasses.Domains
{
    [TestFixture]
    public class TrickeryFeatDataTests : CharacterClassFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Domains.Trickery); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Empty<string>();
            AssertCollectionNames(names);
        }
    }
}

using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Feats.Data.Racial.Metaraces
{
    [TestFixture]
    public class NoneFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.Metaraces.None);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Empty<string>();
            AssertCollectionNames(names);
        }
    }
}

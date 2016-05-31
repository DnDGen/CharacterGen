using CharacterGen.Abilities.Feats;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Tables.Combats
{
    [TestFixture]
    public class ArmorClassModifiersTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.ArmorClassModifiers; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                GroupConstants.Deflection,
                GroupConstants.NaturalArmor,
                GroupConstants.DodgeBonus,
                GroupConstants.ArmorBonus
            };

            AssertCollectionNames(names);
        }

        [TestCase(GroupConstants.Deflection,
            RingConstants.Protection)]
        [TestCase(GroupConstants.NaturalArmor,
            FeatConstants.NaturalArmor,
            FeatConstants.ArmorBonus,
            WondrousItemConstants.AmuletOfNaturalArmor)]
        [TestCase(GroupConstants.DodgeBonus,
            FeatConstants.DodgeBonus,
            FeatConstants.Dodge)]
        [TestCase(GroupConstants.ArmorBonus,
            WondrousItemConstants.BracersOfArmor)]
        public override void DistinctCollection(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}

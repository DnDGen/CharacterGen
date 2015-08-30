using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Tables;
using NUnit.Framework;
using System;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Tables.Combats
{
    [TestFixture]
    public class ArmorClassModifiersTests : CollectionTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Collection.ArmorClassModifiers; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                GroupConstants.Deflection,
                GroupConstants.NaturalArmor
            };

            AssertCollectionNames(names);
        }

        [TestCase(GroupConstants.Deflection, RingConstants.Protection)]
        [TestCase(GroupConstants.NaturalArmor,
            FeatConstants.NaturalArmor,
            FeatConstants.ArmorBonus,
            WondrousItemConstants.AmuletOfNaturalArmor)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}

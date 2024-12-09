using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Magics;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class TravelSpellLevelsTests : CollectionTests
    {
        protected override string tableName => string.Format(TableNameConstants.Formattable.Collection.CLASSSpellLevels, CharacterClassConstants.Domains.Travel);

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Range(1, 9).Select(n => n.ToString());
            AssertCollectionNames(names);
        }

        [TestCase("1", SpellConstants.Longstrider)]
        [TestCase("2", SpellConstants.LocateObject)]
        [TestCase("3", SpellConstants.Fly)]
        [TestCase("4", SpellConstants.DimensionDoor)]
        [TestCase("5", SpellConstants.Teleport)]
        [TestCase("6", SpellConstants.FindThePath)]
        [TestCase("7", SpellConstants.Teleport_Greater)]
        [TestCase("8", SpellConstants.PhaseDoor)]
        [TestCase("9", SpellConstants.AstralProjection)]
        public override void Collection(string name, params string[] collection)
        {
            base.Collection(name, collection);
        }
    }
}

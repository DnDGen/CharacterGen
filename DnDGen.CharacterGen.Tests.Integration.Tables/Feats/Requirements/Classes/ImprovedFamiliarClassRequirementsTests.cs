using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Feats;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Feats.Requirements.Classes
{
    [TestFixture]
    public class ImprovedFamiliarClassRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.FEATClassRequirements, FeatConstants.ImprovedFamiliar);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var classes = new[]
            {
                CharacterClassConstants.Sorcerer,
                CharacterClassConstants.Wizard
            };

            AssertCollectionNames(classes);
        }

        [TestCase(CharacterClassConstants.Sorcerer, 1)]
        [TestCase(CharacterClassConstants.Wizard, 1)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}

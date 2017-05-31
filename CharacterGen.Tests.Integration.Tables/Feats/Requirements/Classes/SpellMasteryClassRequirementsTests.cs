using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Feats;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Feats.Requirements.Classes
{
    [TestFixture]
    public class SpellMasteryClassRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.FEATClassRequirements, FeatConstants.SpellMastery);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var classes = new[] { CharacterClassConstants.Wizard };
            AssertCollectionNames(classes);
        }

        [TestCase(CharacterClassConstants.Wizard, 1)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}

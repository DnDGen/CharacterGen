using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Feats;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Feats.Requirements.Classes
{
    [TestFixture]
    public class GreaterWeaponSpecializationClassRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATClassRequirements, FeatConstants.GreaterWeaponSpecialization); }
        }

        [Test]
        public override void CollectionNames()
        {
            var classes = new[]
            {
                CharacterClassConstants.Fighter
            };

            AssertCollectionNames(classes);
        }

        [TestCase(CharacterClassConstants.Fighter, 12)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}

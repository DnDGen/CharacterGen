using CharacterGen.Common.CharacterClasses;
using CharacterGen.Tables;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Magics.Spells.Wizards
{
    [TestFixture]
    public class Level2WizardSpellQuantitiesTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get
            {
                return String.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSSpellQuantities, 2, CharacterClassConstants.Wizard);
            }
        }

        public override void CollectionNames()
        {
            var names = Enumerable.Range(0, 7).Cast<String>();
            AssertCollectionNames(names);
        }

        [TestCase(0, 4)]
        [TestCase(1, 2)]
        public void Adjustment(Int32 spellLevel, Int32 quantity)
        {
            base.Adjustment(spellLevel.ToString(), quantity);
        }
    }
}

using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Magics.Spells.Wizards
{
    [TestFixture]
    public class Level9WizardSpellsPerDayTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSSpellsPerDay, 9, CharacterClassConstants.Wizard);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Range(0, 6).Select(i => i.ToString());
            AssertCollectionNames(names);
        }

        [TestCase(0, 4)]
        [TestCase(1, 4)]
        [TestCase(2, 4)]
        [TestCase(3, 3)]
        [TestCase(4, 2)]
        [TestCase(5, 1)]
        public void Adjustment(Int32 spellLevel, Int32 quantity)
        {
            base.Adjustment(spellLevel.ToString(), quantity);
        }
    }
}

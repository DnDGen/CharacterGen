using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.CharacterClasses
{
    [TestFixture]
    public class IllusionFeatDataTests : CharacterClassFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Schools.Illusion); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.SkillBonus
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SkillBonus,
            FeatConstants.SkillBonus,
            SkillConstants.Spellcraft + " (Learn Illusion spells)",
            0,
            "",
            "",
            1,
            0,
            2)]
        public override void Data(String name, String feat, String focusType, Int32 frequencyQuantity, String frequencyQuantityStat, String frequencyTimePeriod, Int32 minimumLevel, Int32 maximumLevel, Int32 strength)
        {
            base.Data(name, feat, focusType, frequencyQuantity, frequencyQuantityStat, frequencyTimePeriod, minimumLevel, maximumLevel, strength);
        }
    }
}

using System;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Items;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats.Data.CharacterClasses
{
    [TestFixture]
    public class ClericFeatDataTests : CollectionTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Cleric); }
        }

        [TestCase(FeatConstants.SimpleWeaponProficiencyId,
            FeatConstants.SimpleWeaponProficiencyId,
            "0",
            WeaponProficiencyConstants.All,
            "0",
            "0",
            "")]
        [TestCase(FeatConstants.LightArmorProficiencyId,
            FeatConstants.LightArmorProficiencyId,
            "0",
            "",
            "0",
            "0",
            "")]
        [TestCase(FeatConstants.MediumArmorProficiencyId,
            FeatConstants.MediumArmorProficiencyId,
            "0",
            "",
            "0",
            "0",
            "")]
        [TestCase(FeatConstants.HeavyArmorProficiencyId,
            FeatConstants.HeavyArmorProficiencyId,
            "0",
            "",
            "0",
            "0",
            "")]
        [TestCase(FeatConstants.ShieldProficiencyId,
            FeatConstants.ShieldProficiencyId,
            "0",
            "",
            "0",
            "0",
            "")]
        [TestCase(FeatConstants.TurnUndeadId,
            FeatConstants.TurnUndeadId,
            "0",
            "",
            "0",
            "0",
            "")]
        public override void OrderedCollection(String name, params String[] collection)
        {
            base.OrderedCollection(name, collection);
        }
    }
}
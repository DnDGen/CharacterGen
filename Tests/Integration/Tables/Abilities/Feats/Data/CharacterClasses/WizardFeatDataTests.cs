using System;
using EquipmentGen.Common.Items;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats.Data.CharacterClasses
{
    [TestFixture]
    public class WizardFeatDataTests : CollectionTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Wizard); }
        }

        [TestCase(FeatConstants.ScribeScrollId,
            FeatConstants.ScribeScrollId,
            "0",
            "",
            "0",
            "0",
            "")]
        [TestCase(FeatConstants.SimpleWeaponProficiencyId + WeaponConstants.Club,
            FeatConstants.SimpleWeaponProficiencyId,
            "0",
            WeaponConstants.Club,
            "0",
            "0",
            "")]
        [TestCase(FeatConstants.SimpleWeaponProficiencyId + WeaponConstants.Dagger,
            FeatConstants.SimpleWeaponProficiencyId,
            "0",
            WeaponConstants.Dagger,
            "0",
            "0",
            "")]
        [TestCase(FeatConstants.SimpleWeaponProficiencyId + WeaponConstants.HeavyCrossbow,
            FeatConstants.SimpleWeaponProficiencyId,
            "0",
            WeaponConstants.HeavyCrossbow,
            "0",
            "0",
            "")]
        [TestCase(FeatConstants.SimpleWeaponProficiencyId + WeaponConstants.LightCrossbow,
            FeatConstants.SimpleWeaponProficiencyId,
            "0",
            WeaponConstants.LightCrossbow,
            "0",
            "0",
            "")]
        [TestCase(FeatConstants.SimpleWeaponProficiencyId + WeaponConstants.Quarterstaff,
            FeatConstants.SimpleWeaponProficiencyId,
            "0",
            WeaponConstants.Quarterstaff,
            "0",
            "0",
            "")]
        public override void OrderedCollection(String name, params String[] collection)
        {
            base.OrderedCollection(name, collection);
        }
    }
}

using System;
using System.Collections.Generic;
using D20Dice;
using Moq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Items;
using NPCGen.Generators.Abilities.Feats;
using NPCGen.Generators.Interfaces.Abilities.Feats;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Abilities.Feats
{
    [TestFixture]
    public class FeatFocusGeneratorTests
    {
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IDice> mockDice;
        private IFeatFocusGenerator featFocusGenerator;
        private List<String> requiredFeatIds;
        private List<Feat> otherFeat;
        private CharacterClass characterClass;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockDice = new Mock<IDice>();
            featFocusGenerator = new FeatFocusGenerator(mockCollectionsSelector.Object, mockDice.Object);
            requiredFeatIds = new List<String>();
            otherFeat = new List<Feat>();
            characterClass = new CharacterClass();

            characterClass.ClassName = "class name";
            mockDice.Setup(d => d.Roll(1).d(It.IsAny<Int32>())).Returns(1);
        }

        [Test]
        public void FeatsWithoutFociDoNotFill()
        {
            var schools = new[] { "school 1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "")).Returns(schools);

            var focus = featFocusGenerator.GenerateFrom("featToFill", String.Empty, requiredFeatIds, otherFeat, characterClass);
            mockCollectionsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, It.IsAny<String>()), Times.Never);
            Assert.That(focus, Is.Empty);
        }

        [Test]
        public void FocusGenerated()
        {
            var schools = new[] { "school 1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "focus type")).Returns(schools);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", requiredFeatIds, otherFeat, characterClass);
            Assert.That(focus, Is.EqualTo("school 1"));
        }

        [Test]
        public void FocusRandomlyGenerated()
        {
            var schools = new[] { "school 1", "school 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "focus type")).Returns(schools);

            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", requiredFeatIds, otherFeat, characterClass);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void DoNotGetDuplicateFocus()
        {
            otherFeat.Add(new Feat());
            otherFeat[0].Name.Id = "featToFill";
            otherFeat[0].Focus = "school 1";

            var schools = new[] { "school 1", "school 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "focus type")).Returns(schools);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", requiredFeatIds, otherFeat, characterClass);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void SpellcastersCanSelectRayForWeaponFoci()
        {
            var weapons = new[] { WeaponProficiencyConstants.Ray, "weapon" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, GroupConstants.WeaponsWithUnarmedAndGrappleAndRay))
                .Returns(weapons);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters))
                .Returns(new[] { characterClass.ClassName });

            var focus = featFocusGenerator.GenerateFrom("featToFill", GroupConstants.WeaponsWithUnarmedAndGrappleAndRay, requiredFeatIds, otherFeat, characterClass);
            Assert.That(focus, Is.EqualTo(WeaponProficiencyConstants.Ray));
        }

        [Test]
        public void NonSpellcastersCannotSelectRayForWeaponFoci()
        {
            var weapons = new[] { WeaponProficiencyConstants.Ray, "weapon" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, GroupConstants.WeaponsWithUnarmedAndGrappleAndRay))
                .Returns(weapons);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters))
                .Returns(new[] { "other class name" });

            var focus = featFocusGenerator.GenerateFrom("featToFill", GroupConstants.WeaponsWithUnarmedAndGrappleAndRay, requiredFeatIds, otherFeat, characterClass);
            Assert.That(focus, Is.EqualTo("weapon"));
        }

        [Test]
        public void FeatsWithoutFociButWithRequirementsThatHaveFociDoNotUseSameFocus()
        {
            requiredFeatIds.Add("feat1");
            otherFeat.Add(new Feat());
            otherFeat[0].Name.Id = "feat1";
            otherFeat[0].Focus = "focus";

            var schools = new[] { "school 1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, String.Empty)).Returns(schools);

            var focus = featFocusGenerator.GenerateFrom("featToFill", String.Empty, requiredFeatIds, otherFeat, characterClass);
            Assert.That(focus, Is.Empty);
        }

        [Test]
        public void FeatsWithFociAndRequirementsThatHaveFociUseFocus()
        {
            requiredFeatIds.Add("feat1");
            otherFeat.Add(new Feat());
            otherFeat[0].Name.Id = "feat1";
            otherFeat[0].Focus = "focus";

            var schools = new[] { "other focus", "focus" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "focus type")).Returns(schools);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", requiredFeatIds, otherFeat, characterClass);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void IfFeatRequirementHasMultipleFoci_PickRandomlyAmongThem()
        {
            requiredFeatIds.Add("feat1");
            otherFeat.Add(new Feat());
            otherFeat.Add(new Feat());

            otherFeat[0].Name.Id = "feat1";
            otherFeat[0].Focus = "focus";
            otherFeat[1].Name.Id = "feat1";
            otherFeat[1].Focus = "other focus";

            var schools = new[] { "other focus", "focus" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "focus type")).Returns(schools);

            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", requiredFeatIds, otherFeat, characterClass);
            Assert.That(focus, Is.EqualTo("other focus"));
        }

        [Test]
        public void IfFocusTypeIsSchoolOfMagic_CannotPickProhibitedFieldAsFocus()
        {
            var schools = new[] { "school 1", "school 2", "school 3", "school 4" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, GroupConstants.SchoolsOfMagic)).Returns(schools);

            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);
            characterClass.ProhibitedFields = new[] { "school 1", "school 3" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", GroupConstants.SchoolsOfMagic, requiredFeatIds, otherFeat, characterClass);
            Assert.That(focus, Is.EqualTo("school 4"));
        }

        [Test]
        public void IfFeatRequirementHasAllAsFoci_ExplodeIt()
        {
            requiredFeatIds.Add("feat1");
            otherFeat.Add(new Feat());
            otherFeat[0].Name.Id = "feat1";
            otherFeat[0].Focus = WeaponProficiencyConstants.All;

            var schools = new[] { "school 2", "school 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "focus type")).Returns(schools);

            var prereqFoci = new[] { "school 1", "school 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "feat1")).Returns(prereqFoci);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", requiredFeatIds, otherFeat, characterClass);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void IfWeaponFamiliarityAndAllMartialOnRequirement_AddInFamiliarityTypes()
        {
            requiredFeatIds.Add(FeatConstants.MartialWeaponProficiencyId);
            otherFeat.Add(new Feat());
            otherFeat.Add(new Feat());

            otherFeat[0].Name.Id = FeatConstants.MartialWeaponProficiencyId;
            otherFeat[0].Focus = WeaponProficiencyConstants.All;
            otherFeat[1].Name.Id = FeatConstants.WeaponFamiliarityId;
            otherFeat[1].Focus = "weird weapon";

            var schools = new[] { "school 2", "school 3", "weird weapon" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "focus type")).Returns(schools);

            var prereqFoci = new[] { "school 1", "school 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, FeatConstants.MartialWeaponProficiencyId)).Returns(prereqFoci);

            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", requiredFeatIds, otherFeat, characterClass);
            Assert.That(focus, Is.EqualTo("weird weapon"));
        }

        [Test]
        public void ProficiencyFulfillsProficiencyRequirement()
        {
            requiredFeatIds.Add(GroupConstants.Proficiency);
            otherFeat.Add(new Feat());
            otherFeat[0].Name.Id = "proficiency2";
            otherFeat[0].Focus = "school 2";

            var schools = new[] { "school 1", "school 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "focus type")).Returns(schools);

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.Proficiency)).Returns(proficiencyFeats);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", requiredFeatIds, otherFeat, characterClass);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void ProficiencyWithAllFulfillsProficiencyRequirement()
        {
            requiredFeatIds.Add(GroupConstants.Proficiency);
            otherFeat.Add(new Feat());
            otherFeat[0].Name.Id = "proficiency2";
            otherFeat[0].Focus = WeaponProficiencyConstants.All;

            var schools = new[] { "school 1", "school 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "focus type")).Returns(schools);

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.Proficiency)).Returns(proficiencyFeats);

            var proficiencySchools = new[] { "school 2", "school 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, "proficiency2")).Returns(proficiencySchools);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", requiredFeatIds, otherFeat, characterClass);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void IfWeaponFamiliarityAndMartialWeaponFocusType_AddInFamiliarityTypes()
        {
            otherFeat.Add(new Feat());
            otherFeat[0].Name.Id = FeatConstants.WeaponFamiliarityId;
            otherFeat[0].Focus = "weird weapon";

            var schools = new[] { "school 2", "school 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, FeatConstants.MartialWeaponProficiencyId)).Returns(schools);

            mockDice.Setup(d => d.Roll(1).d(3)).Returns(3);

            var focus = featFocusGenerator.GenerateFrom("featToFill", FeatConstants.MartialWeaponProficiencyId, requiredFeatIds, otherFeat, characterClass);
            Assert.That(focus, Is.EqualTo("weird weapon"));
        }
    }
}

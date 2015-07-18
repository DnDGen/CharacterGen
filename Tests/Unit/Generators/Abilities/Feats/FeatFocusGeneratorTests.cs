using System;
using System.Collections.Generic;
using D20Dice;
using Moq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Items;
using NPCGen.Generators.Abilities.Feats;
using NPCGen.Generators.Interfaces.Abilities.Feats;
using NPCGen.Selectors.Interfaces;
using NPCGen.Selectors.Interfaces.Objects;
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
        private List<RequiredFeat> requiredFeats;
        private List<Feat> otherFeats;
        private CharacterClass characterClass;
        private Dictionary<String, Skill> skills;
        private Dictionary<String, IEnumerable<String>> focusTypes;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockDice = new Mock<IDice>();
            featFocusGenerator = new FeatFocusGenerator(mockCollectionsSelector.Object, mockDice.Object);
            requiredFeats = new List<RequiredFeat>();
            otherFeats = new List<Feat>();
            characterClass = new CharacterClass();
            skills = new Dictionary<String, Skill>();
            focusTypes = new Dictionary<String, IEnumerable<String>>();

            characterClass.ClassName = "class name";
            mockDice.Setup(d => d.Roll(1).d(It.IsAny<Int32>())).Returns(1);
            mockCollectionsSelector.Setup(s => s.SelectAllFrom(TableNameConstants.Set.Collection.FeatFoci)).Returns(focusTypes);
        }

        [Test]
        public void FeatsWithoutFociDoNotFill()
        {
            focusTypes[""] = new[] { "school 1" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", String.Empty, skills, requiredFeats, otherFeats, characterClass);
            mockCollectionsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, It.IsAny<String>()), Times.Never);
            Assert.That(focus, Is.Empty);
        }

        [Test]
        public void FocusGenerated()
        {
            focusTypes["focus type"] = new[] { "school 1" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("school 1"));
        }

        [Test]
        public void FocusRandomlyGenerated()
        {
            focusTypes["focus type"] = new[] { "school 1", "school 2" };
            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void DoNotGetDuplicateFocus()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "featToFill";
            otherFeats[0].Focus = "school 1";

            focusTypes["focus type"] = new[] { "school 1", "school 2" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void SpellcastersCanSelectRayForWeaponFoci()
        {
            focusTypes[GroupConstants.WeaponsWithUnarmedAndGrappleAndRay] = new[] { ProficiencyConstants.Ray, "weapon" };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters))
                .Returns(new[] { characterClass.ClassName });

            var focus = featFocusGenerator.GenerateFrom("featToFill", GroupConstants.WeaponsWithUnarmedAndGrappleAndRay, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo(ProficiencyConstants.Ray));
        }

        [Test]
        public void NonSpellcastersCannotSelectRayForWeaponFoci()
        {
            focusTypes[GroupConstants.WeaponsWithUnarmedAndGrappleAndRay] = new[] { ProficiencyConstants.Ray, "weapon" };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters))
                .Returns(new[] { "other class name" });

            var focus = featFocusGenerator.GenerateFrom("featToFill", GroupConstants.WeaponsWithUnarmedAndGrappleAndRay, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("weapon"));
        }

        [Test]
        public void FeatsWithoutFociButWithRequirementsThatHaveFociDoNotUseSameFocus()
        {
            requiredFeats.Add(new RequiredFeat { Feat = "feat1" });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Focus = "focus";

            focusTypes[""] = new[] { "school 1" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", String.Empty, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.Empty);
        }

        [Test]
        public void FeatsWithFociAndRequirementsThatHaveFociUseFocus()
        {
            requiredFeats.Add(new RequiredFeat { Feat = "feat1" });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Focus = "focus";

            focusTypes["focus type"] = new[] { "other focus", "focus" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void IfFeatRequirementHasMultipleFoci_PickRandomlyAmongThem()
        {
            requiredFeats.Add(new RequiredFeat { Feat = "feat1" });
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());

            otherFeats[0].Name = "feat1";
            otherFeats[0].Focus = "focus";
            otherFeats[1].Name = "feat1";
            otherFeats[1].Focus = "other focus";

            focusTypes["focus type"] = new[] { "other focus", "focus" };

            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("other focus"));
        }

        [Test]
        public void IfFocusTypeIsSchoolOfMagic_CannotPickProhibitedFieldAsFocus()
        {
            focusTypes[GroupConstants.SchoolsOfMagic] = new[] { "school 1", "school 2", "school 3", "school 4" };

            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);
            characterClass.ProhibitedFields = new[] { "school 1", "school 3" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", GroupConstants.SchoolsOfMagic, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("school 4"));
        }

        [Test]
        public void IfFeatRequirementHasAllAsFoci_ExplodeIt()
        {
            requiredFeats.Add(new RequiredFeat { Feat = "feat1" });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Focus = ProficiencyConstants.All;

            focusTypes["focus type"] = new[] { "school 2", "school 3" };
            focusTypes["feat1"] = new[] { "school 1", "school 2" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void IfWeaponFamiliarityAndAllMartialOnRequirement_AddInFamiliarityTypes()
        {
            requiredFeats.Add(new RequiredFeat { Feat = FeatConstants.MartialWeaponProficiency });
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());

            otherFeats[0].Name = FeatConstants.MartialWeaponProficiency;
            otherFeats[0].Focus = ProficiencyConstants.All;
            otherFeats[1].Name = FeatConstants.WeaponFamiliarity;
            otherFeats[1].Focus = "weird weapon";

            focusTypes["focus type"] = new[] { "school 2", "school 3", "weird weapon" };
            focusTypes[FeatConstants.MartialWeaponProficiency] = new[] { "school 1", "school 2" };

            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("weird weapon"));
        }

        [Test]
        public void ProficiencyFulfillsProficiencyRequirement()
        {
            requiredFeats.Add(new RequiredFeat { Feat = GroupConstants.Proficiency });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "proficiency2";
            otherFeats[0].Focus = "school 2";

            focusTypes["focus type"] = new[] { "school 1", "school 2" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.Proficiency)).Returns(proficiencyFeats);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void ProficiencyWithAllFulfillsProficiencyRequirement()
        {
            requiredFeats.Add(new RequiredFeat { Feat = GroupConstants.Proficiency });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "proficiency2";
            otherFeats[0].Focus = ProficiencyConstants.All;

            focusTypes["focus type"] = new[] { "school 1", "school 2" };
            focusTypes["proficiency2"] = new[] { "school 2", "school 3" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.Proficiency)).Returns(proficiencyFeats);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void IfWeaponFamiliarityAndMartialWeaponFocusType_AddInFamiliarityTypes()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = FeatConstants.WeaponFamiliarity;
            otherFeats[0].Focus = "weird weapon";

            focusTypes[FeatConstants.MartialWeaponProficiency] = new[] { "school 2", "school 3" };

            mockDice.Setup(d => d.Roll(1).d(3)).Returns(3);

            var focus = featFocusGenerator.GenerateFrom(FeatConstants.MartialWeaponProficiency, ProficiencyConstants.All, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("weird weapon"));
        }

        [Test]
        public void GeneratingFromSkillsReturnsEmptyFocusForEmptyFocusType()
        {
            focusTypes[""] = new[] { "school 1" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", String.Empty, skills);
            mockCollectionsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, It.IsAny<String>()), Times.Never);
            Assert.That(focus, Is.Empty);
        }

        [Test]
        public void GeneratingFromSkillsReturnsFocus()
        {
            focusTypes["focus type"] = new[] { "school 1" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills);
            Assert.That(focus, Is.EqualTo("school 1"));
        }

        [Test]
        public void GeneratingFromSkillsReturnsRandomFocus()
        {
            focusTypes["focus type"] = new[] { "school 1", "school 2" };

            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void IfAvailableFociFromSkillsContainsSkills_OnlyUseProvidedSkills()
        {
            focusTypes["focus type"] = new[] { "skill 1", "skill 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.Skills)).Returns(new[] { "skill 1", "skill 2", "skill 3" });

            skills["skill 2"] = new Skill();
            skills["skill 3"] = new Skill();

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills);
            Assert.That(focus, Is.EqualTo("skill 2"));
        }

        [Test]
        public void IfNoWorkingSkillFociFromSkills_ReturnNoFocus()
        {
            focusTypes["focus type"] = new[] { "skill 1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.Skills)).Returns(new[] { "skill 1", "skill 2" });

            skills["skill 2"] = new Skill();

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills);
            Assert.That(focus, Is.EqualTo(ProficiencyConstants.All));
        }

        [Test]
        public void IfNotAFocusTypeWhenGeneratingWithSkills_ReturnWhatWasProvided()
        {
            focusTypes["focus type"] = new[] { "school 1" };
            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus", skills);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void IfNotAFocusType_ReturnWhatWasProvided()
        {
            focusTypes["focus type"] = new[] { "school 1" };
            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void IfWeaponFamiliarityAndExoticWeaponProficiency_DoNotPickFamiliarityFocus()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = FeatConstants.WeaponFamiliarity;
            otherFeats[0].Focus = "weird weapon";

            focusTypes[FeatConstants.ExoticWeaponProficiency] = new[] { "weird weapon", "school 2" };

            var focus = featFocusGenerator.GenerateFrom(FeatConstants.ExoticWeaponProficiency, ProficiencyConstants.All, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void IfNoWeaponFamiliarity_UseOnlyMartialWeapons()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "other feat";
            otherFeats[0].Focus = "weird weapon";

            focusTypes[FeatConstants.MartialWeaponProficiency] = new[] { "school 2" };

            mockDice.Setup(d => d.Roll(1).d(2)).Returns(2);

            var focus = featFocusGenerator.GenerateFrom(FeatConstants.MartialWeaponProficiency, ProficiencyConstants.All, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void IfNoWeaponFamiliarity_UseAllExoticWeapons()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "other feat";
            otherFeats[0].Focus = "weird weapon";

            focusTypes[FeatConstants.ExoticWeaponProficiency] = new[] { "weird weapon", "school 2" };

            var focus = featFocusGenerator.GenerateFrom(FeatConstants.ExoticWeaponProficiency, ProficiencyConstants.All, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("weird weapon"));
        }

        [Test]
        public void CanBeProficientInAllFromSkills()
        {
            focusTypes["feat"] = new[] { "focus" };
            var focus = featFocusGenerator.GenerateAllowingFocusOfAllFrom("feat", ProficiencyConstants.All, skills);
            Assert.That(focus, Is.EqualTo(ProficiencyConstants.All));
        }

        [Test]
        public void CannotBeProficientInAllFromSkills()
        {
            focusTypes["feat"] = new[] { "focus" };
            var focus = featFocusGenerator.GenerateFrom("feat", ProficiencyConstants.All, skills);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void CannotBeProficientInAll()
        {
            focusTypes["feat"] = new[] { "focus" };
            var focus = featFocusGenerator.GenerateFrom("feat", ProficiencyConstants.All, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void CanAlwaysFocusInUnarmedStrikeWhenProficiencyIsRequirement()
        {
            requiredFeats.Add(new RequiredFeat { Feat = GroupConstants.Proficiency });
            focusTypes[GroupConstants.WeaponsWithUnarmedAndGrapple] = new[] { "weapon", ProficiencyConstants.UnarmedStrike };
            focusTypes[GroupConstants.Weapons] = new[] { "weapon" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.Proficiency)).Returns(proficiencyFeats);

            var focus = featFocusGenerator.GenerateFrom("featToFill", GroupConstants.WeaponsWithUnarmedAndGrapple, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo(ProficiencyConstants.UnarmedStrike));
        }

        [Test]
        public void CanAlwaysFocusInGrappleWhenProficiencyIsRequirement()
        {
            requiredFeats.Add(new RequiredFeat { Feat = GroupConstants.Proficiency });
            focusTypes[GroupConstants.WeaponsWithUnarmedAndGrapple] = new[] { "weapon", ProficiencyConstants.Grapple };
            focusTypes[GroupConstants.Weapons] = new[] { "weapon" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.Proficiency)).Returns(proficiencyFeats);

            var focus = featFocusGenerator.GenerateFrom("featToFill", GroupConstants.WeaponsWithUnarmedAndGrapple, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo(ProficiencyConstants.Grapple));
        }

        [Test]
        public void CanFocusInRayWhenProficiencyIsRequirementAndSpellcaster()
        {
            requiredFeats.Add(new RequiredFeat { Feat = GroupConstants.Proficiency });
            focusTypes[GroupConstants.WeaponsWithUnarmedAndGrappleAndRay] = new[] { "weapon", ProficiencyConstants.Ray };
            focusTypes[GroupConstants.Weapons] = new[] { "weapon" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.Proficiency)).Returns(proficiencyFeats);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters))
                .Returns(new[] { characterClass.ClassName });

            var focus = featFocusGenerator.GenerateFrom("featToFill", GroupConstants.WeaponsWithUnarmedAndGrappleAndRay, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo(ProficiencyConstants.Ray));
        }

        [Test]
        public void CannotFocusInRayWhenProficiencyIsRequirementAndNotSpellcaster()
        {
            requiredFeats.Add(new RequiredFeat { Feat = GroupConstants.Proficiency });
            focusTypes[GroupConstants.WeaponsWithUnarmedAndGrappleAndRay] = new[] { "weapon", ProficiencyConstants.Ray };
            focusTypes[GroupConstants.Weapons] = new[] { "weapon" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.Proficiency)).Returns(proficiencyFeats);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters))
                .Returns(new[] { "other class" });

            otherFeats.Add(new Feat());
            otherFeats[0].Name = "proficiency2";
            otherFeats[0].Focus = "weapon";

            var focus = featFocusGenerator.GenerateFrom("featToFill", GroupConstants.WeaponsWithUnarmedAndGrappleAndRay, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("weapon"));
        }

        [Test]
        public void CannotChooseFocusWhenFocusedInAll()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "featId";
            otherFeats[0].Focus = ProficiencyConstants.All;

            focusTypes["focus type"] = new[] { "school 1" };

            var focus = featFocusGenerator.GenerateFrom("featId", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo(ProficiencyConstants.All));
        }

        [Test]
        public void CannotChooseFocusWhenAllAlreadyTaken()
        {
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());

            otherFeats[0].Name = "featId";
            otherFeats[0].Focus = "school 1";
            otherFeats[1].Name = "featId";
            otherFeats[1].Focus = "school 2";

            focusTypes["focus type"] = new[] { "school 1", "school 2" };

            var focus = featFocusGenerator.GenerateFrom("featId", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo(ProficiencyConstants.All));
        }

        [Test]
        public void IfAvailableFociContainsSkills_OnlyUseProvidedSkills()
        {
            focusTypes["focus type"] = new[] { "skill 1", "skill 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.Skills)).Returns(new[] { "skill 1", "skill 2", "skill 3" });

            skills["skill 2"] = new Skill();
            skills["skill 3"] = new Skill();

            var focus = featFocusGenerator.GenerateFrom("featId", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("skill 2"));
        }

        [Test]
        public void IfNoWorkingSkillFoci_ReturnNoFocus()
        {
            focusTypes["focus type"] = new[] { "skill 1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.Skills)).Returns(new[] { "skill 1", "skill 2" });

            skills["skill 2"] = new Skill();

            var focus = featFocusGenerator.GenerateFrom("featId", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo(ProficiencyConstants.All));
        }
    }
}
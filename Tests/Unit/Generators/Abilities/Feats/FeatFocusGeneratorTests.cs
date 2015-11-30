using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Abilities.Feats;
using CharacterGen.Generators.Domain.Abilities.Feats;
using CharacterGen.Selectors;
using CharacterGen.Selectors.Objects;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Unit.Generators.Abilities.Feats
{
    [TestFixture]
    public class FeatFocusGeneratorTests
    {
        private Mock<ICollectionsSelector> mockCollectionsSelector;
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
            featFocusGenerator = new FeatFocusGenerator(mockCollectionsSelector.Object);
            requiredFeats = new List<RequiredFeat>();
            otherFeats = new List<Feat>();
            characterClass = new CharacterClass();
            skills = new Dictionary<String, Skill>();
            focusTypes = new Dictionary<String, IEnumerable<String>>();

            characterClass.ClassName = "class name";
            mockCollectionsSelector.Setup(s => s.SelectAllFrom(TableNameConstants.Set.Collection.FeatFoci)).Returns(focusTypes);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<String>>())).Returns((IEnumerable<String> c) => c.First());
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
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<String>>())).Returns((IEnumerable<String> c) => c.ElementAt(1));

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void DoNotGetDuplicateFocus()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "featToFill";
            otherFeats[0].Foci = new[] { "school 1" };

            focusTypes["focus type"] = new[] { "school 1", "school 2" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void DoNotGetDuplicateFoci()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "featToFill";
            otherFeats[0].Foci = new[] { "school 1", "school 2" };

            focusTypes["focus type"] = new[] { "school 1", "school 2", "school 3" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("school 3"));
        }

        [Test]
        public void SpellcastersCanSelectRayForWeaponFoci()
        {
            focusTypes[FeatConstants.Foci.WeaponsWithUnarmedAndGrappleAndRay] = new[] { FeatConstants.Foci.Ray, "weapon" };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters))
                .Returns(new[] { characterClass.ClassName });

            var focus = featFocusGenerator.GenerateFrom("featToFill", FeatConstants.Foci.WeaponsWithUnarmedAndGrappleAndRay, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.Ray));
        }

        [Test]
        public void NonSpellcastersCannotSelectRayForWeaponFoci()
        {
            focusTypes[FeatConstants.Foci.WeaponsWithUnarmedAndGrappleAndRay] = new[] { FeatConstants.Foci.Ray, "weapon" };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters))
                .Returns(new[] { "other class name" });

            var focus = featFocusGenerator.GenerateFrom("featToFill", FeatConstants.Foci.WeaponsWithUnarmedAndGrappleAndRay, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("weapon"));
        }

        [Test]
        public void FeatsWithoutFociButWithRequirementsThatHaveFociDoNotUseSameFocus()
        {
            requiredFeats.Add(new RequiredFeat { Feat = "feat1" });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Foci = new[] { "focus" };

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
            otherFeats[0].Foci = new[] { "focus" };

            focusTypes["focus type"] = new[] { "other focus", "focus" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void IfFeatRequirementHasMultipleFoci_PickRandomlyAmongThem()
        {
            requiredFeats.Add(new RequiredFeat { Feat = "feat1" });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Foci = new[] { "focus", "other focus" };

            focusTypes["focus type"] = new[] { "other focus", "focus" };
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<String>>())).Returns((IEnumerable<String> c) => c.ElementAt(1));

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("other focus"));
        }

        [Test]
        public void IfFocusTypeIsSchoolOfMagic_CannotPickProhibitedFieldAsFocus()
        {
            focusTypes[GroupConstants.SchoolsOfMagic] = new[] { "school 1", "school 2", "school 3", "school 4" };
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<String>>())).Returns((IEnumerable<String> c) => c.ElementAt(1));

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
            otherFeats[0].Foci = new[] { FeatConstants.Foci.All };

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
            otherFeats[0].Foci = new[] { FeatConstants.Foci.All };
            otherFeats[1].Name = FeatConstants.WeaponFamiliarity;
            otherFeats[1].Foci = new[] { "weird weapon" };

            focusTypes["focus type"] = new[] { "school 2", "school 3", "weird weapon" };
            focusTypes[FeatConstants.MartialWeaponProficiency] = new[] { "school 1", "school 2" };

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<String>>())).Returns((IEnumerable<String> c) => c.ElementAt(1));

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("weird weapon"));
        }

        [Test]
        public void ProficiencyFulfillsProficiencyRequirement()
        {
            requiredFeats.Add(new RequiredFeat { Feat = ItemTypeConstants.Weapon + GroupConstants.Proficiency });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "proficiency2";
            otherFeats[0].Foci = new[] { "school 2" };

            focusTypes["focus type"] = new[] { "school 1", "school 2" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, ItemTypeConstants.Weapon + GroupConstants.Proficiency)).Returns(proficiencyFeats);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void ProficiencyWithAllFulfillsProficiencyRequirement()
        {
            requiredFeats.Add(new RequiredFeat { Feat = ItemTypeConstants.Weapon + GroupConstants.Proficiency });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "proficiency2";
            otherFeats[0].Foci = new[] { FeatConstants.Foci.All };

            focusTypes["focus type"] = new[] { "school 1", "school 2" };
            focusTypes["proficiency2"] = new[] { "school 2", "school 3" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, ItemTypeConstants.Weapon + GroupConstants.Proficiency)).Returns(proficiencyFeats);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void IfWeaponFamiliarityAndMartialWeaponFocusType_AddInFamiliarityTypes()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = FeatConstants.WeaponFamiliarity;
            otherFeats[0].Foci = new[] { "weird weapon" };

            focusTypes[FeatConstants.MartialWeaponProficiency] = new[] { "school 2", "school 3" };

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<String>>())).Returns((IEnumerable<String> c) => c.ElementAt(2));

            var focus = featFocusGenerator.GenerateFrom(FeatConstants.MartialWeaponProficiency, FeatConstants.Foci.All, skills, requiredFeats, otherFeats, characterClass);
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

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<String>>())).Returns((IEnumerable<String> c) => c.ElementAt(1));

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
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.All));
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
            otherFeats[0].Foci = new[] { "weird weapon" };

            focusTypes[FeatConstants.ExoticWeaponProficiency] = new[] { "weird weapon", "school 2" };

            var focus = featFocusGenerator.GenerateFrom(FeatConstants.ExoticWeaponProficiency, FeatConstants.Foci.All, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void IfNoWeaponFamiliarity_UseOnlyMartialWeapons()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "other feat";
            otherFeats[0].Foci = new[] { "weird weapon" };

            focusTypes[FeatConstants.MartialWeaponProficiency] = new[] { "school 2" };

            var focus = featFocusGenerator.GenerateFrom(FeatConstants.MartialWeaponProficiency, FeatConstants.Foci.All, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void IfNoWeaponFamiliarity_UseAllExoticWeapons()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "other feat";
            otherFeats[0].Foci = new[] { "weird weapon" };

            focusTypes[FeatConstants.ExoticWeaponProficiency] = new[] { "weird weapon", "school 2" };

            var focus = featFocusGenerator.GenerateFrom(FeatConstants.ExoticWeaponProficiency, FeatConstants.Foci.All, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("weird weapon"));
        }

        [Test]
        public void CanBeProficientInAllFromSkills()
        {
            focusTypes["feat"] = new[] { "focus" };
            var focus = featFocusGenerator.GenerateAllowingFocusOfAllFrom("feat", FeatConstants.Foci.All, skills);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.All));
        }

        [Test]
        public void CannotBeProficientInAllFromSkills()
        {
            focusTypes["feat"] = new[] { "focus" };
            var focus = featFocusGenerator.GenerateFrom("feat", FeatConstants.Foci.All, skills);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void CannotBeProficientInAll()
        {
            focusTypes["feat"] = new[] { "focus" };
            var focus = featFocusGenerator.GenerateFrom("feat", FeatConstants.Foci.All, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void CanAlwaysFocusInUnarmedStrikeWhenProficiencyIsRequirement()
        {
            requiredFeats.Add(new RequiredFeat { Feat = ItemTypeConstants.Weapon + GroupConstants.Proficiency });
            focusTypes[FeatConstants.Foci.WeaponsWithUnarmed] = new[] { "weapon", FeatConstants.Foci.UnarmedStrike };
            focusTypes[FeatConstants.Foci.Weapons] = new[] { "weapon" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, ItemTypeConstants.Weapon + GroupConstants.Proficiency)).Returns(proficiencyFeats);

            var focus = featFocusGenerator.GenerateFrom("featToFill", FeatConstants.Foci.WeaponsWithUnarmed, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.UnarmedStrike));
        }

        [Test]
        public void CanAlwaysFocusInGrappleWhenProficiencyIsRequirement()
        {
            requiredFeats.Add(new RequiredFeat { Feat = ItemTypeConstants.Weapon + GroupConstants.Proficiency });
            focusTypes[FeatConstants.Foci.WeaponsWithUnarmedAndGrapple] = new[] { "weapon", FeatConstants.Foci.Grapple };
            focusTypes[FeatConstants.Foci.Weapons] = new[] { "weapon" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, ItemTypeConstants.Weapon + GroupConstants.Proficiency)).Returns(proficiencyFeats);

            var focus = featFocusGenerator.GenerateFrom("featToFill", FeatConstants.Foci.WeaponsWithUnarmedAndGrapple, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.Grapple));
        }

        [Test]
        public void CanFocusInRayWhenProficiencyIsRequirementAndSpellcaster()
        {
            requiredFeats.Add(new RequiredFeat { Feat = ItemTypeConstants.Weapon + GroupConstants.Proficiency });
            focusTypes[FeatConstants.Foci.WeaponsWithUnarmedAndGrappleAndRay] = new[] { "weapon", FeatConstants.Foci.Ray };
            focusTypes[FeatConstants.Foci.Weapons] = new[] { "weapon" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, ItemTypeConstants.Weapon + GroupConstants.Proficiency)).Returns(proficiencyFeats);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters))
                .Returns(new[] { characterClass.ClassName });

            var focus = featFocusGenerator.GenerateFrom("featToFill", FeatConstants.Foci.WeaponsWithUnarmedAndGrappleAndRay, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.Ray));
        }

        [Test]
        public void CannotFocusInRayWhenProficiencyIsRequirementAndNotSpellcaster()
        {
            requiredFeats.Add(new RequiredFeat { Feat = ItemTypeConstants.Weapon + GroupConstants.Proficiency });
            focusTypes[FeatConstants.Foci.WeaponsWithUnarmedAndGrappleAndRay] = new[] { "weapon", FeatConstants.Foci.Ray };
            focusTypes[FeatConstants.Foci.Weapons] = new[] { "weapon" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, ItemTypeConstants.Weapon + GroupConstants.Proficiency)).Returns(proficiencyFeats);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters))
                .Returns(new[] { "other class" });

            otherFeats.Add(new Feat());
            otherFeats[0].Name = "proficiency2";
            otherFeats[0].Foci = new[] { "weapon" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", FeatConstants.Foci.WeaponsWithUnarmedAndGrappleAndRay, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("weapon"));
        }

        [Test]
        public void CannotChooseFocusWhenFocusedInAll()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "featId";
            otherFeats[0].Foci = new[] { FeatConstants.Foci.All };

            focusTypes["focus type"] = new[] { "school 1" };

            var focus = featFocusGenerator.GenerateFrom("featId", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.All));
        }

        [Test]
        public void CannotChooseFocusWhenAllAlreadyTaken()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat";
            otherFeats[0].Foci = new[] { "school 1", "school 2" };

            focusTypes["focus type"] = new[] { "school 1", "school 2" };

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.All));
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
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.All));
        }

        [Test]
        public void FeatWithRequiredFeatThatHasWeaponFocusHonorsOnlyThatWeaponFocus()
        {
            requiredFeats.Add(new RequiredFeat { Feat = "feat1" });

            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Foci = new[] { "weapon" };
            otherFeats[1].Name = "all proficiency";
            otherFeats[1].Foci = new[] { FeatConstants.Foci.All };
            otherFeats[2].Name = "specific proficiency";
            otherFeats[2].Foci = new[] { "specific weapon" };

            focusTypes[FeatConstants.Foci.WeaponsWithUnarmedAndGrappleAndRay] = new[] { "specific weapon", "other weapon", "weapon", FeatConstants.Foci.UnarmedStrike, FeatConstants.Foci.Grapple, FeatConstants.Foci.Ray };
            focusTypes[FeatConstants.Foci.Weapons] = new[] { "specific weapon", "other weapon", "weapon" };
            focusTypes["all proficiency"] = new[] { "proficiency weapon", "other weapon", "weapon" };

            var proficiencyFeats = new[] { "all proficiency", "specific proficiency" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, ItemTypeConstants.Weapon + GroupConstants.Proficiency)).Returns(proficiencyFeats);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<String>>(c => c.Single() == "weapon"))).Returns("only weapon");

            var focus = featFocusGenerator.GenerateFrom("featToFill", FeatConstants.Foci.WeaponsWithUnarmedAndGrappleAndRay, skills, requiredFeats, otherFeats, characterClass);
            Assert.That(focus, Is.EqualTo("only weapon"));
        }
    }
}
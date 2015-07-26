using System;
using System.Collections.Generic;
using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Selectors.Objects;
using CharacterGen.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Selectors.Objects
{
    [TestFixture]
    public class AdditionalFeatSelectionTests
    {
        private AdditionalFeatSelection selection;
        private List<Feat> feats;
        private Dictionary<String, Stat> stats;
        private Dictionary<String, Skill> skills;
        private CharacterClass characterClass;

        [SetUp]
        public void Setup()
        {
            selection = new AdditionalFeatSelection();
            feats = new List<Feat>();
            stats = new Dictionary<String, Stat>();
            skills = new Dictionary<String, Skill>();
            characterClass = new CharacterClass();
        }

        [Test]
        public void FeatSelectionInitialized()
        {
            Assert.That(selection.Feat, Is.Empty);
            Assert.That(selection.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(selection.RequiredFeats, Is.Empty);
            Assert.That(selection.RequiredSkillRanks, Is.Empty);
            Assert.That(selection.RequiredStats, Is.Empty);
            Assert.That(selection.RequiredCharacterClasses, Is.Empty);
            Assert.That(selection.FocusType, Is.Empty);
            Assert.That(selection.Strength, Is.EqualTo(0));
            Assert.That(selection.Frequency, Is.Not.Null);
        }

        [Test]
        public void ImmutableRequirementsMetIfNoRequirements()
        {
            var met = selection.ImmutableRequirementsMet(0, stats, skills, characterClass);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MutableRequirementsMetIfNoRequirements()
        {
            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void BaseAttackRequirementNotMet()
        {
            selection.RequiredBaseAttack = 2;
            var met = selection.ImmutableRequirementsMet(1, stats, skills, characterClass);
            Assert.That(met, Is.False);
        }

        [Test]
        public void StatRequirementsNotMet()
        {
            selection.RequiredStats["stat"] = 16;
            stats["stat"] = new Stat { Value = 15 };
            stats["other stat"] = new Stat { Value = 17 };

            var met = selection.ImmutableRequirementsMet(1, stats, skills, characterClass);
            Assert.That(met, Is.False);
        }

        [Test]
        public void SkillRequirementsWithCrossClassSkillNotMet()
        {
            selection.RequiredSkillRanks["skill"] = 5;
            skills["skill"] = new Skill { Ranks = 9, ClassSkill = false };
            skills["other skill"] = new Skill { Ranks = 10, ClassSkill = false };

            var met = selection.ImmutableRequirementsMet(1, stats, skills, characterClass);
            Assert.That(met, Is.False);
        }

        [Test]
        public void SkillRequirementsWithClassSkillNotMet()
        {
            selection.RequiredSkillRanks["skill"] = 5;
            skills["skill"] = new Skill { Ranks = 4, ClassSkill = true };
            skills["other skill"] = new Skill { Ranks = 5, ClassSkill = true };

            var met = selection.ImmutableRequirementsMet(1, stats, skills, characterClass);
            Assert.That(met, Is.False);
        }

        [Test]
        public void ClassNameRequirementsNotMet()
        {
            selection.RequiredCharacterClasses["class name"] = 1;
            selection.RequiredCharacterClasses["other class name"] = 2;
            characterClass.ClassName = "different class name";
            characterClass.Level = 3;

            var met = selection.ImmutableRequirementsMet(1, stats, skills, characterClass);
            Assert.That(met, Is.False);
        }

        [Test]
        public void ClassLevelRequirementsNotMet()
        {
            selection.RequiredCharacterClasses["class name"] = 1;
            selection.RequiredCharacterClasses["other class name"] = 2;
            characterClass.ClassName = "other class name";
            characterClass.Level = 1;

            var met = selection.ImmutableRequirementsMet(1, stats, skills, characterClass);
            Assert.That(met, Is.False);
        }

        [Test]
        public void AllImmutableRequirementsMet()
        {
            selection.RequiredBaseAttack = 2;

            selection.RequiredStats["stat"] = 16;
            stats["stat"] = new Stat { Value = 16 };
            stats["other stat"] = new Stat { Value = 15 };

            selection.RequiredSkillRanks["class skill"] = 5;
            selection.RequiredSkillRanks["cross-class skill"] = 5;
            skills["class skill"] = new Skill { Ranks = 10, ClassSkill = false };
            skills["other class skill"] = new Skill { Ranks = 9, ClassSkill = false };
            skills["cross-class skill"] = new Skill { Ranks = 5, ClassSkill = true };
            skills["other cross-class skill"] = new Skill { Ranks = 4, ClassSkill = true };

            selection.RequiredCharacterClasses["class name"] = 1;
            characterClass.ClassName = "class name";
            characterClass.Level = 1;

            var met = selection.ImmutableRequirementsMet(2, stats, skills, characterClass);
            Assert.That(met, Is.True);
        }

        [Test]
        public void FeatRequirementsNotMet()
        {
            feats.Add(new Feat());
            feats[0].Name = "feat";
            selection.RequiredFeats = new[]
            {
                new RequiredFeat { Feat = "feat" }, 
                new RequiredFeat { Feat = "other required feat" }
            };

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void AllMutableRequirementsMet()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[1].Name = "other required feat";
            selection.RequiredFeats = new[]
            {
                new RequiredFeat { Feat = "feat" }, 
                new RequiredFeat { Feat = "other required feat" }
            };

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void ExtraFeatDoNotMatter()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[1].Name = "other required feat";
            feats[2].Name = "yet another feat";

            selection.RequiredFeats = new[]
            {
                new RequiredFeat { Feat = "feat" }, 
                new RequiredFeat { Feat = "other required feat" }
            };

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void ProficiencyRequirementDoesNotAffectMutableRequirements()
        {
            selection.RequiredFeats = new[]
            {
                new RequiredFeat { Feat = GroupConstants.Proficiency }
            };

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void WithProficiencyRequirement_OtherRequirementsNotIgnored()
        {
            selection.RequiredFeats = new[]
            {
                new RequiredFeat { Feat = GroupConstants.Proficiency },
                new RequiredFeat { Feat = "feat" }
            };

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void WithProficiencyRequirement_OtherRequirementsAreHonored()
        {
            feats.Add(new Feat());
            feats[0].Name = "feat";
            selection.RequiredFeats = new[]
            { 
                new RequiredFeat { Feat = GroupConstants.Proficiency },
                new RequiredFeat { Feat = "feat" }
            };

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }
    }
}
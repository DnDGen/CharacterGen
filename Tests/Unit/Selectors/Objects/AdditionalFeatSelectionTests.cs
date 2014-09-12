using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Selectors.Interfaces.Objects;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors.Objects
{
    [TestFixture]
    public class AdditionalFeatSelectionTests
    {
        private AdditionalFeatSelection selection;
        private List<Feat> feats;
        private Dictionary<String, Stat> stats;
        private Dictionary<String, Skill> skills;

        [SetUp]
        public void Setup()
        {
            selection = new AdditionalFeatSelection();
            feats = new List<Feat>();
            stats = new Dictionary<String, Stat>();
            skills = new Dictionary<String, Skill>();
        }

        [Test]
        public void FeatSelectionInitialized()
        {
            Assert.That(selection.FeatName, Is.Empty);
            Assert.That(selection.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(selection.RequiredFeats, Is.Empty);
            Assert.That(selection.RequiredSkillRanks, Is.Empty);
            Assert.That(selection.RequiredStats, Is.Empty);
            Assert.That(selection.RequiredClassNames, Is.Empty);
            Assert.That(selection.IsFighterFeat, Is.False);
            Assert.That(selection.SpecificApplicationType, Is.Empty);
            Assert.That(selection.IsWizardFeat, Is.False);
        }

        [Test]
        public void ImmutableRequirementsMetIfNoRequirements()
        {
            var met = selection.ImmutableRequirementsMet(0, stats, skills, String.Empty);
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
            var met = selection.ImmutableRequirementsMet(1, stats, skills, String.Empty);
            Assert.That(met, Is.False);
        }

        [Test]
        public void StatRequirementsNotMet()
        {
            selection.RequiredStats["stat"] = 16;
            stats["stat"] = new Stat { Value = 15 };
            stats["other stat"] = new Stat { Value = 17 };

            var met = selection.ImmutableRequirementsMet(1, stats, skills, String.Empty);
            Assert.That(met, Is.False);
        }

        [Test]
        public void SkillRequirementsWithCrossClassSkillNotMet()
        {
            selection.RequiredSkillRanks["skill"] = 5;
            skills["skill"] = new Skill { Ranks = 9, ClassSkill = false };
            skills["other skill"] = new Skill { Ranks = 10, ClassSkill = false };

            var met = selection.ImmutableRequirementsMet(1, stats, skills, String.Empty);
            Assert.That(met, Is.False);
        }

        [Test]
        public void SkillRequirementsWithClassSkillNotMet()
        {
            selection.RequiredSkillRanks["skill"] = 5;
            skills["skill"] = new Skill { Ranks = 4, ClassSkill = true };
            skills["other skill"] = new Skill { Ranks = 5, ClassSkill = true };

            var met = selection.ImmutableRequirementsMet(1, stats, skills, String.Empty);
            Assert.That(met, Is.False);
        }

        [Test]
        public void ClassNameRequirementsNotMet()
        {
            selection.RequiredClassNames = new[] { "class name", "other class name" };

            var met = selection.ImmutableRequirementsMet(1, stats, skills, "different class name");
            Assert.That(met, Is.False);
        }

        [TestCase("class name")]
        [TestCase("other class name")]
        public void AnyMatchingClassNameMeetsRequirement(String className)
        {
            selection.RequiredClassNames = new[] { "class name", "other class name" };

            var met = selection.ImmutableRequirementsMet(2, stats, skills, className);
            Assert.That(met, Is.True);
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

            selection.RequiredClassNames = new[] { "class name", "other class name" };

            var met = selection.ImmutableRequirementsMet(2, stats, skills, "class name");
            Assert.That(met, Is.True);
        }

        [Test]
        public void FeatRequirementsNotMet()
        {
            feats.Add(new Feat { Name = "feat" });
            selection.RequiredFeats = new[] { "feat", "other required feat" };

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void AllMutableRequirementsMet()
        {
            feats.Add(new Feat { Name = "feat" });
            feats.Add(new Feat { Name = "other required feat" });
            selection.RequiredFeats = new[] { "feat", "other required feat" };

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void ExtraFeatDoNotMatter()
        {
            feats.Add(new Feat { Name = "feat" });
            feats.Add(new Feat { Name = "other required feat" });
            feats.Add(new Feat { Name = "yet another feat" });
            selection.RequiredFeats = new[] { "feat", "other required feat" };

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }
    }
}
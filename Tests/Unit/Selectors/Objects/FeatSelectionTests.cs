using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Selectors.Interfaces.Objects;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors.Objects
{
    [TestFixture]
    public class FeatSelectionTests
    {
        private FeatSelection selection;
        private List<String> feats;
        private Dictionary<String, Stat> stats;
        private Dictionary<String, Skill> skills;

        [SetUp]
        public void Setup()
        {
            selection = new FeatSelection();
            feats = new List<String>();
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
        }

        [Test]
        public void AllRequirementsMetIfNoRequirements()
        {
            var met = selection.RequirementsMet(feats, 0, stats, skills, String.Empty);
            Assert.That(met, Is.True);
        }

        [Test]
        public void BaseAttackRequirementNotMet()
        {
            selection.RequiredBaseAttack = 2;
            var met = selection.RequirementsMet(feats, 1, stats, skills, String.Empty);
            Assert.That(met, Is.False);
        }

        [Test]
        public void StatRequirementsNotMet()
        {
            selection.RequiredStats["stat"] = 16;
            stats["stat"] = new Stat { Value = 15 };
            stats["other stat"] = new Stat { Value = 17 };

            var met = selection.RequirementsMet(feats, 1, stats, skills, String.Empty);
            Assert.That(met, Is.False);
        }

        [Test]
        public void SkillRequirementsNotMet()
        {
            selection.RequiredSkillRanks["skill"] = 5;
            skills["skill"] = new Skill { Ranks = 9, ClassSkill = false };
            skills["other skill"] = new Skill { Ranks = 9, ClassSkill = true };

            var met = selection.RequirementsMet(feats, 1, stats, skills, String.Empty);
            Assert.That(met, Is.False);
        }

        [Test]
        public void FeatRequirementsNotMet()
        {
            feats.Add("feat");
            feats.Add("other feat");
            selection.RequiredFeats.Add("feat");
            selection.RequiredFeats.Add("other required feat");

            var met = selection.RequirementsMet(feats, 1, stats, skills, String.Empty);
            Assert.That(met, Is.False);
        }

        [Test]
        public void ClassNameRequirementsNotMet()
        {
            selection.RequiredClassNames.Add("class name");
            selection.RequiredClassNames.Add("other class name");

            var met = selection.RequirementsMet(feats, 1, stats, skills, "different class name");
            Assert.That(met, Is.False);
        }

        [Test]
        public void AllRequirementsMet()
        {
            selection.RequiredBaseAttack = 2;

            selection.RequiredStats["stat"] = 16;
            stats["stat"] = new Stat { Value = 16 };
            stats["other stat"] = new Stat { Value = 15 };

            selection.RequiredSkillRanks["skill"] = 5;
            skills["skill"] = new Skill { Ranks = 10, ClassSkill = false };
            skills["other skill"] = new Skill { Ranks = 1 };

            feats.Add("feat");
            feats.Add("other required feat");
            feats.Add("other feat");
            selection.RequiredFeats.Add("feat");
            selection.RequiredFeats.Add("other required feat");

            selection.RequiredClassNames.Add("class name");
            selection.RequiredClassNames.Add("other class name");

            var met = selection.RequirementsMet(feats, 2, stats, skills, "class name");
            Assert.That(met, Is.True);
        }
    }
}
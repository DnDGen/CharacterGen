using System.Collections.Generic;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Selectors.Interfaces.Objects;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors.Objects
{
    [TestFixture]
    public class RequiredFeatTests
    {
        private RequiredFeat requiredFeat;
        private List<Feat> otherFeats;

        [SetUp]
        public void Setup()
        {
            requiredFeat = new RequiredFeat();
            otherFeats = new List<Feat>();
        }

        [Test]
        public void RequiredFeatInitialized()
        {
            Assert.That(requiredFeat.Feat, Is.Empty);
            Assert.That(requiredFeat.Focus, Is.Empty);
        }

        [Test]
        public void RequirementMetIfOtherFeatsContainFeatId()
        {
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Name = "feat2";

            requiredFeat.Feat = "feat2";

            var met = requiredFeat.RequirementMet(otherFeats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementNotMetIfOtherFeatDoNotContainFeatId()
        {
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Name = "feat2";

            requiredFeat.Feat = "feat3";

            var met = requiredFeat.RequirementMet(otherFeats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementMetIfOtherFeatsContainFeatIdAndNoRequiredFocus()
        {
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Name = "feat2";
            otherFeats[0].Focus = "focus";

            requiredFeat.Feat = "feat2";

            var met = requiredFeat.RequirementMet(otherFeats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementMetIfOtherFeatsContainFeatIdAndRequiredFocusIsOnFeat()
        {
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Name = "feat2";
            otherFeats[0].Focus = "focus";

            requiredFeat.Feat = "feat2";
            requiredFeat.Focus = "focus";

            var met = requiredFeat.RequirementMet(otherFeats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementMetIfOtherFeatsContainFeatIdAndRequiredFocusIsOnAtLeastOneFeat()
        {
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat2";
            otherFeats[0].Focus = "other focus";
            otherFeats[0].Name = "feat2";
            otherFeats[0].Focus = "focus";

            requiredFeat.Feat = "feat2";
            requiredFeat.Focus = "focus";

            var met = requiredFeat.RequirementMet(otherFeats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementNotMetIfOtherFeatsContainFeatIdAndRequiredFocusIsNotOnMatchingFeat()
        {
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Focus = "focus";
            otherFeats[0].Name = "feat2";
            otherFeats[0].Focus = "other focus";

            requiredFeat.Feat = "feat2";
            requiredFeat.Focus = "focus";

            var met = requiredFeat.RequirementMet(otherFeats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementNotMetIfOtherFeatsContainFeatIdAndRequiredFocusDoesNotMatch()
        {
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Name = "feat2";
            otherFeats[0].Focus = "other focus";

            requiredFeat.Feat = "feat2";
            requiredFeat.Focus = "focus";

            var met = requiredFeat.RequirementMet(otherFeats);
            Assert.That(met, Is.False);
        }
    }
}
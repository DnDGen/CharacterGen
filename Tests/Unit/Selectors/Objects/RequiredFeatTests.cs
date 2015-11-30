using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Selectors.Objects;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Selectors.Objects
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
        public void RequirementMetIfOtherFeatsContainFeatName()
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
        public void RequirementNotMetIfOtherFeatDoNotContainFeatName()
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
        public void RequirementMetIfOtherFeatsContainFeatNAmeAndNoRequiredFocus()
        {
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Name = "feat2";
            otherFeats[0].Foci = new[] { "focus" };

            requiredFeat.Feat = "feat2";

            var met = requiredFeat.RequirementMet(otherFeats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementMetIfOtherFeatsContainFeatNameAndRequiredFocusIsOnFeat()
        {
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Name = "feat2";
            otherFeats[0].Foci = new[] { "focus" };

            requiredFeat.Feat = "feat2";
            requiredFeat.Focus = "focus";

            var met = requiredFeat.RequirementMet(otherFeats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementMetIfOtherFeatsContainFeatNameAndRequiredFocusIsOnAtLeastOneFeat()
        {
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat2";
            otherFeats[0].Foci = new[] { "other focus", "focus" };

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
            otherFeats[0].Foci = new[] { "focus" };
            otherFeats[0].Name = "feat2";
            otherFeats[0].Foci = new[] { "other focus" };

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
            otherFeats[0].Foci = new[] { "other focus" };

            requiredFeat.Feat = "feat2";
            requiredFeat.Focus = "focus";

            var met = requiredFeat.RequirementMet(otherFeats);
            Assert.That(met, Is.False);
        }
    }
}
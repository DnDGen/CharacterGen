using System.Linq;
using Moq;
using NPCGen.Selectors;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors
{
    [TestFixture]
    public class FeatSelectorTests
    {
        private IFeatsSelector selector;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            selector = new FeatsSelector();
        }

        [Test]
        public void GetRacialFeats()
        {
            var racialFeats = selector.SelectRacial();
            Assert.That(racialFeats.Count(), Is.EqualTo(2));

            var first = racialFeats.First();
            var last = racialFeats.Last();

            Assert.That(first.FeatName, Is.EqualTo("racial feat 1"));
            Assert.That(first.FeatStrength, Is.EqualTo(9266));
            Assert.That(first.SizeRequirement, Is.EqualTo("ginormous"));
            Assert.That(first.BaseRaceRequirements, Contains.Item("base race 1"));
            Assert.That(first.BaseRaceRequirements, Contains.Item("base race 2"));
            Assert.That(first.BaseRaceRequirements.Count(), Is.EqualTo(2));
            Assert.That(first.HitDieRequirements, Contains.Item(42));
            Assert.That(first.HitDieRequirements, Contains.Item(90210));
            Assert.That(first.HitDieRequirements.Count(), Is.EqualTo(2));
            Assert.That(first.MetaraceRequirements, Contains.Item("metarace 1"));
            Assert.That(first.MetaraceRequirements, Contains.Item("metarace 2"));
            Assert.That(first.MetaraceRequirements.Count(), Is.EqualTo(2));
            Assert.That(first.MetaraceSpeciesRequirements, Contains.Item("species 1"));
            Assert.That(first.MetaraceSpeciesRequirements, Contains.Item("species 2"));
            Assert.That(first.MetaraceSpeciesRequirements.Count(), Is.EqualTo(2));

            Assert.That(last.FeatName, Is.EqualTo("racial feat 2"));
            Assert.That(last.FeatStrength, Is.EqualTo(6629));
            Assert.That(last.SizeRequirement, Is.EqualTo("miniscule"));
            Assert.That(last.BaseRaceRequirements, Contains.Item("base race 2"));
            Assert.That(last.BaseRaceRequirements, Contains.Item("base race 3"));
            Assert.That(last.BaseRaceRequirements.Count(), Is.EqualTo(2));
            Assert.That(last.HitDieRequirements, Contains.Item(24));
            Assert.That(last.HitDieRequirements, Contains.Item(1209));
            Assert.That(last.HitDieRequirements.Count(), Is.EqualTo(2));
            Assert.That(last.MetaraceRequirements, Contains.Item("metarace 2"));
            Assert.That(last.MetaraceRequirements, Contains.Item("metarace 3"));
            Assert.That(last.MetaraceRequirements.Count(), Is.EqualTo(2));
            Assert.That(last.MetaraceSpeciesRequirements, Contains.Item("species 3"));
            Assert.That(last.MetaraceSpeciesRequirements, Contains.Item("species 2"));
            Assert.That(last.MetaraceSpeciesRequirements.Count(), Is.EqualTo(2));
        }
    }
}
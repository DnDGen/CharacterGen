using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.IoC.Modules
{
    [TestFixture]
    public class SelectorsModuleTests : IoCTests
    {
        [Test]
        public void LanguageSelectorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ILanguageCollectionsSelector>();
        }

        [Test]
        public void LevelAdjustmentsSelectorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IAdjustmentsSelector>();
        }

        [Test]
        public void PercentileResultSelectorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IPercentileSelector>();
        }

        [Test]
        public void StatAdjustmentsSelectorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IAbilityAdjustmentsSelector>();
        }

        [Test]
        public void CollectionsSelectorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ICollectionsSelector>();
        }

        [Test]
        public void CollectionsSelectorsIsDecorated()
        {
            AssertIsInstanceOf<ICollectionsSelector, CollectionsSelectorEventGenDecorator>();
        }

        [Test]
        public void SkillSelectorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ISkillSelector>();
        }

        [Test]
        public void FeatsSelectorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IFeatsSelector>();
        }

        [Test]
        public void FeatsSelectorsIsDecorated()
        {
            AssertIsInstanceOf<IFeatsSelector, FeatsSelectorEventGenDecorator>();
        }

        [Test]
        public void BooleanPercentileSelectorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IBooleanPercentileSelector>();
        }

        [Test]
        public void LeadershipSelectorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ILeadershipSelector>();
        }
    }
}
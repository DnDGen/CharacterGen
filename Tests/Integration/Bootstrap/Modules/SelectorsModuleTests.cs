﻿using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Bootstrap.Modules
{
    [TestFixture]
    public class SelectorsModuleTests : BootstrapTests
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
        public void StatPrioritySelectorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IStatPrioritySelector>();
        }

        [Test]
        public void StatAdjustmentsSelectorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IStatAdjustmentsSelector>();
        }

        [Test]
        public void CollectionsSelectorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ICollectionsSelector>();
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
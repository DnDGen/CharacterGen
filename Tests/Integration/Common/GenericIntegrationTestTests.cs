using System;
using System.Diagnostics;
using Ninject;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NPCGen.Core.Generation.Randomizers.Stats;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Common
{
    [TestFixture]
    public class GenericIntegrationTestTests : IntegrationTest
    {
        [Inject]
        public IAlignmentRandomizer AlignmentRandomizer { get; set; }
        [Inject]
        public IClassNameRandomizer ClassNameRandomizer { get; set; }
        [Inject]
        public ILevelRandomizer LevelRandomizer { get; set; }
        [Inject]
        public IBaseRaceRandomizer BaseRaceRandomizer { get; set; }
        [Inject]
        public IMetaraceRandomizer MetaraceRandomizer { get; set; }
        [Inject]
        public IStatsRandomizer StatsRandomizer { get; set; }

        [Test]
        public void GenericAlignmentRandomizerIsAnyAlignmentRandomizer()
        {
            Assert.That(AlignmentRandomizer, Is.InstanceOf<AnyAlignmentRandomizer>());
        }

        [Test]
        public void GenericClassNameRandomizerIsAnyClassNameRandomizer()
        {
            Assert.That(ClassNameRandomizer, Is.InstanceOf<AnyClassNameRandomizer>());
        }

        [Test]
        public void GenericLevelRandomizerIsAnyLevelRandomizer()
        {
            Assert.That(LevelRandomizer, Is.InstanceOf<AnyLevelRandomizer>());
        }

        [Test]
        public void GenericBaseRaceRandomizerIsAnyBaseRaceRandomizer()
        {
            Assert.That(BaseRaceRandomizer, Is.InstanceOf<AnyBaseRaceRandomizer>());
        }

        [Test]
        public void GenericMetaraceRandomizerIsAnyMetaraceRandomizer()
        {
            Assert.That(MetaraceRandomizer, Is.InstanceOf<AnyMetaraceRandomizer>());
        }

        [Test]
        public void GenericMetaraceRandomizerAllowsEmptyMetaraces()
        {
            var anyMetaraceRandomizer = MetaraceRandomizer as AnyMetaraceRandomizer;
            Assert.That(anyMetaraceRandomizer.AllowNoMetarace, Is.True);
        }

        [Test]
        public void GenericStatsRandomizerIsRawStatsRandomizer()
        {
            Assert.That(StatsRandomizer, Is.InstanceOf<RawStatsRandomizer>());
        }

        [Test]
        public void GetNewInstanceOfTypeReturnsNewInstances()
        {
            var first = GetNewInstanceOf<DependentDataCollection>();
            var second = GetNewInstanceOf<DependentDataCollection>();
            Assert.That(AreEqual(first, second), Is.False);
        }

        private Boolean AreEqual(DependentDataCollection first, DependentDataCollection second)
        {
            if (first.Alignment.Goodness != second.Alignment.Goodness)
                return false;

            if (first.Alignment.Lawfulness != second.Alignment.Lawfulness)
                return false;

            if (first.CharacterClassPrototype.ClassName != second.CharacterClassPrototype.ClassName)
                return false;

            if (first.CharacterClassPrototype.Level != second.CharacterClassPrototype.Level)
                return false;

            if (first.Race.BaseRace != second.Race.BaseRace)
                return false;

            if (first.Race.Metarace != second.Race.Metarace)
                return false;

            if (first.Race.Male != second.Race.Male)
                return false;

            return true;
        }

        [Test]
        public void IncompatibleDependentDataIsNeverGenerated()
        {
            var iterations = 0;
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            while (iterations++ < 1000000 && stopwatch.Elapsed.Seconds < 1)
            {
                var data = GetNewInstanceOf<DependentDataCollection>();

                Assert.That(data.CharacterClass.ClassName, Is.EqualTo(data.CharacterClassPrototype.ClassName));
                Assert.That(data.CharacterClass.Level, Is.EqualTo(data.CharacterClassPrototype.Level));

                var classNames = ClassNameRandomizer.GetAllPossibleResults(data.Alignment);
                Assert.That(classNames, Is.Not.Empty);

                var baseRaces = BaseRaceRandomizer.GetAllPossibleResults(data.Alignment.Goodness, data.CharacterClassPrototype);
                Assert.That(baseRaces, Is.Not.Empty);

                var metaraces = MetaraceRandomizer.GetAllPossibleResults(data.Alignment.Goodness, data.CharacterClassPrototype);
                Assert.That(metaraces, Is.Not.Empty);
            }

            Assert.Pass("Iterations: {0}", iterations);
        }
    }
}
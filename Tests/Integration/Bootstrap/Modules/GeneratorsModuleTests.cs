using System;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Generators.Interfaces.Verifiers;
using NPCGen.Generators.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Bootstrap.Modules
{
    [TestFixture]
    public class GeneratorsModuleTests : BootstrapTests
    {
        [Test]
        public void AlignmentFactoriesAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IAlignmentGenerator>();
        }

        [Test]
        public void CharacterFactoriesAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ICharacterGenerator>();
        }

        [Test]
        public void CharacterClassFactoriesAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ICharacterClassGenerator>();
        }

        [Test]
        public void HitPointsFactoriesAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IHitPointsGenerator>();
        }

        [Test]
        public void LanguageFactoriesAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ILanguageGenerator>();
        }

        [Test]
        public void RaceFactoriesAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IRaceGenerator>();
        }

        [Test]
        public void RandomizerVerifiersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IRandomizerVerifier>();
        }

        [Test]
        public void StatsFactoriesAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IStatsGenerator>();
        }

        [Test]
        public void AlignmentRandomzierNamedAnyIsAnyAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, AnyAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);
        }

        [Test]
        public void AlignmentRandomzierNamedChaoticIsChaoticAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, ChaoticAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Chaotic);
        }

        [Test]
        public void AlignmentRandomzierNamedEvilIsEvilAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, EvilAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Evil);
        }

        [Test]
        public void AlignmentRandomzierNamedGoodIsGoodAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, GoodAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Good);
        }

        [Test]
        public void AlignmentRandomzierNamedLawfulIsLawfulAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, LawfulAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Lawful);
        }

        [Test]
        public void AlignmentRandomzierNamedNeutralIsNeutralAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, NeutralAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Neutral);
        }

        [Test]
        public void AlignmentRandomzierNamedNonChaoticIsNonChaoticAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, NonChaoticAlignmentRandomizer>(AlignmentRandomizerTypeConstants.NonChaotic);
        }

        [Test]
        public void AlignmentRandomzierNamedNonEvilIsNonEvilAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, NonEvilAlignmentRandomizer>(AlignmentRandomizerTypeConstants.NonEvil);
        }

        [Test]
        public void AlignmentRandomzierNamedNonGoodIsNonGoodAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, NonGoodAlignmentRandomizer>(AlignmentRandomizerTypeConstants.NonGood);
        }

        [Test]
        public void AlignmentRandomzierNamedNonLawfulIsNonLawfulAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, NonLawfulAlignmentRandomizer>(AlignmentRandomizerTypeConstants.NonLawful);
        }

        [Test]
        public void AlignmentRandomzierNamedNonNeutralIsNonNeutralAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, NonNeutralAlignmentRandomizer>(AlignmentRandomizerTypeConstants.NonNeutral);
        }

        [TestCase(AlignmentRandomizerTypeConstants.Any)]
        [TestCase(AlignmentRandomizerTypeConstants.Chaotic)]
        [TestCase(AlignmentRandomizerTypeConstants.Evil)]
        [TestCase(AlignmentRandomizerTypeConstants.Good)]
        [TestCase(AlignmentRandomizerTypeConstants.Lawful)]
        [TestCase(AlignmentRandomizerTypeConstants.Neutral)]
        [TestCase(AlignmentRandomizerTypeConstants.NonChaotic)]
        [TestCase(AlignmentRandomizerTypeConstants.NonEvil)]
        [TestCase(AlignmentRandomizerTypeConstants.NonGood)]
        [TestCase(AlignmentRandomizerTypeConstants.NonLawful)]
        [TestCase(AlignmentRandomizerTypeConstants.NonNeutral)]
        public void AlignmentRandomizerIsNotGeneratedAsSingleton(String name)
        {
            AssertNotSingleton<IAlignmentRandomizer>(name);
        }

        [TestCase(ClassNameRandomizerTypeConstants.Any)]
        [TestCase(ClassNameRandomizerTypeConstants.Healer)]
        [TestCase(ClassNameRandomizerTypeConstants.Mage)]
        [TestCase(ClassNameRandomizerTypeConstants.NonSpellcaster)]
        [TestCase(ClassNameRandomizerTypeConstants.Spellcaster)]
        [TestCase(ClassNameRandomizerTypeConstants.Stealth)]
        [TestCase(ClassNameRandomizerTypeConstants.Warrior)]
        public void ClassNameRandomizerIsNotGeneratedAsSingleton(String name)
        {
            AssertNotSingleton<IClassNameRandomizer>(name);
        }

        [TestCase(LevelRandomizerTypeConstants.Any)]
        [TestCase(LevelRandomizerTypeConstants.High)]
        [TestCase(LevelRandomizerTypeConstants.Low)]
        [TestCase(LevelRandomizerTypeConstants.Medium)]
        [TestCase(LevelRandomizerTypeConstants.VeryHigh)]
        public void LevelRandomizerIsNotGeneratedAsSingleton(String name)
        {
            AssertNotSingleton<ILevelRandomizer>(name);
        }

        [TestCase(BaseRaceRandomizerTypeConstants.Any)]
        [TestCase(BaseRaceRandomizerTypeConstants.Evil)]
        [TestCase(BaseRaceRandomizerTypeConstants.Good)]
        [TestCase(BaseRaceRandomizerTypeConstants.Neutral)]
        [TestCase(BaseRaceRandomizerTypeConstants.NonEvil)]
        [TestCase(BaseRaceRandomizerTypeConstants.NonGood)]
        [TestCase(BaseRaceRandomizerTypeConstants.NonNeutral)]
        [TestCase(BaseRaceRandomizerTypeConstants.NonStandard)]
        [TestCase(BaseRaceRandomizerTypeConstants.Standard)]
        public void BaseRaceRandomizerIsNotGeneratedAsSingleton(String name)
        {
            AssertNotSingleton<IBaseRaceRandomizer>(name);
        }

        [TestCase(MetaraceRandomizerTypeConstants.Any)]
        [TestCase(MetaraceRandomizerTypeConstants.AnyForced)]
        [TestCase(MetaraceRandomizerTypeConstants.Evil)]
        [TestCase(MetaraceRandomizerTypeConstants.EvilForced)]
        [TestCase(MetaraceRandomizerTypeConstants.Genetic)]
        [TestCase(MetaraceRandomizerTypeConstants.GeneticForced)]
        [TestCase(MetaraceRandomizerTypeConstants.Good)]
        [TestCase(MetaraceRandomizerTypeConstants.GoodForced)]
        [TestCase(MetaraceRandomizerTypeConstants.Lycanthrope)]
        [TestCase(MetaraceRandomizerTypeConstants.LycanthropeForced)]
        [TestCase(MetaraceRandomizerTypeConstants.Neutral)]
        [TestCase(MetaraceRandomizerTypeConstants.NeutralForced)]
        [TestCase(MetaraceRandomizerTypeConstants.None)]
        [TestCase(MetaraceRandomizerTypeConstants.NonEvil)]
        [TestCase(MetaraceRandomizerTypeConstants.NonEvilForced)]
        [TestCase(MetaraceRandomizerTypeConstants.NonGood)]
        [TestCase(MetaraceRandomizerTypeConstants.NonGoodForced)]
        [TestCase(MetaraceRandomizerTypeConstants.NonNeutral)]
        [TestCase(MetaraceRandomizerTypeConstants.NonNeutralForced)]
        public void MetaraceRandomizerIsNotGeneratedAsSingleton(String name)
        {
            AssertNotSingleton<IMetaraceRandomizer>(name);
        }

        [TestCase(StatsRandomizerTypeConstants.Average)]
        [TestCase(StatsRandomizerTypeConstants.BestOfFour)]
        [TestCase(StatsRandomizerTypeConstants.Good)]
        [TestCase(StatsRandomizerTypeConstants.Heroic)]
        [TestCase(StatsRandomizerTypeConstants.OnesAsSixes)]
        [TestCase(StatsRandomizerTypeConstants.Poor)]
        [TestCase(StatsRandomizerTypeConstants.Raw)]
        [TestCase(StatsRandomizerTypeConstants.TwoTenSidedDice)]
        public void StatRandomizerIsNotGeneratedAsSingleton(String name)
        {
            AssertNotSingleton<IStatsRandomizer>(name);
        }
    }
}
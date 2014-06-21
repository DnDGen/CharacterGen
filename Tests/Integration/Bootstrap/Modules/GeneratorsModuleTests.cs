using System;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Generators.Interfaces.Verifiers;
using NPCGen.Generators.Randomizers.Alignments;
using NPCGen.Generators.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Generators.Randomizers.CharacterClasses.Levels;
using NPCGen.Generators.Randomizers.Races.BaseRaces;
using NPCGen.Generators.Randomizers.Races.Metaraces;
using NPCGen.Generators.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Bootstrap.Modules
{
    [TestFixture]
    public class GeneratorsModuleTests : BootstrapTests
    {
        [Test]
        public void AlignmentGeneratorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IAlignmentGenerator>();
        }

        [Test]
        public void CharacterGeneratorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ICharacterGenerator>();
        }

        [Test]
        public void CharacterClassGeneratorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ICharacterClassGenerator>();
        }

        [Test]
        public void HitPointsGeneratorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IHitPointsGenerator>();
        }

        [Test]
        public void LanguageGeneratorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<ILanguageGenerator>();
        }

        [Test]
        public void RaceGeneratorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IRaceGenerator>();
        }

        [Test]
        public void RandomizerVerifiersAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IRandomizerVerifier>();
        }

        [Test]
        public void StatsGeneratorsAreNotGeneratedAsSingletons()
        {
            AssertNotSingleton<IStatsGenerator>();
        }

        [Test]
        public void AlignmentRandomizerNamedAnyIsAnyAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, AnyAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);
        }

        [Test]
        public void AlignmentRandomizerNamedChaoticIsChaoticAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, ChaoticAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Chaotic);
        }

        [Test]
        public void AlignmentRandomizerNamedEvilIsEvilAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, EvilAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Evil);
        }

        [Test]
        public void AlignmentRandomizerNamedGoodIsGoodAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, GoodAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Good);
        }

        [Test]
        public void AlignmentRandomizerNamedLawfulIsLawfulAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, LawfulAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Lawful);
        }

        [Test]
        public void AlignmentRandomizerNamedNeutralIsNeutralAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, NeutralAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Neutral);
        }

        [Test]
        public void AlignmentRandomizerNamedNonChaoticIsNonChaoticAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, NonChaoticAlignmentRandomizer>(AlignmentRandomizerTypeConstants.NonChaotic);
        }

        [Test]
        public void AlignmentRandomizerNamedNonEvilIsNonEvilAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, NonEvilAlignmentRandomizer>(AlignmentRandomizerTypeConstants.NonEvil);
        }

        [Test]
        public void AlignmentRandomizerNamedNonGoodIsNonGoodAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, NonGoodAlignmentRandomizer>(AlignmentRandomizerTypeConstants.NonGood);
        }

        [Test]
        public void AlignmentRandomizerNamedNonLawfulIsNonLawfulAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, NonLawfulAlignmentRandomizer>(AlignmentRandomizerTypeConstants.NonLawful);
        }

        [Test]
        public void AlignmentRandomizerNamedNonNeutralIsNonNeutralAlignmentRandomizer()
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

        [Test]
        public void ClassNameRandomizerNamedAnyIsAnyClassNameRandomizer()
        {
            AssertNamedIsInstanceOf<IClassNameRandomizer, AnyClassNameRandomizer>(ClassNameRandomizerTypeConstants.Any);
        }

        [Test]
        public void ClassNameRandomizerNamedHealerIsHealerClassNameRandomizer()
        {
            AssertNamedIsInstanceOf<IClassNameRandomizer, HealerClassNameRandomizer>(ClassNameRandomizerTypeConstants.Healer);
        }

        [Test]
        public void ClassNameRandomizerNamedMageIsMageClassNameRandomizer()
        {
            AssertNamedIsInstanceOf<IClassNameRandomizer, MageClassNameRandomizer>(ClassNameRandomizerTypeConstants.Mage);
        }

        [Test]
        public void ClassNameRandomizerNamedNonSpellcasterIsNonSpellcasterClassNameRandomizer()
        {
            AssertNamedIsInstanceOf<IClassNameRandomizer, NonSpellcasterClassNameRandomizer>(ClassNameRandomizerTypeConstants.NonSpellcaster);
        }

        [Test]
        public void ClassNameRandomizerNamedSpellcasterIsSpellcasterClassNameRandomizer()
        {
            AssertNamedIsInstanceOf<IClassNameRandomizer, SpellcasterClassNameRandomizer>(ClassNameRandomizerTypeConstants.Spellcaster);
        }

        [Test]
        public void ClassNameRandomizerNamedStealthIsStealthClassNameRandomizer()
        {
            AssertNamedIsInstanceOf<IClassNameRandomizer, StealthClassNameRandomizer>(ClassNameRandomizerTypeConstants.Stealth);
        }

        [Test]
        public void ClassNameRandomizerNameWarriorIsWarriorClassNameRandomizer()
        {
            AssertNamedIsInstanceOf<IClassNameRandomizer, WarriorClassNameRandomizer>(ClassNameRandomizerTypeConstants.Warrior);
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

        [Test]
        public void LevelRandomizerNamedAnyIsAnyLevelRandomizer()
        {
            AssertNamedIsInstanceOf<ILevelRandomizer, AnyLevelRandomizer>(LevelRandomizerTypeConstants.Any);
        }

        [Test]
        public void LevelRandomizerNamedHighIsHighLevelRandomizer()
        {
            AssertNamedIsInstanceOf<ILevelRandomizer, HighLevelRandomizer>(LevelRandomizerTypeConstants.High);
        }

        [Test]
        public void LevelRandomizerNamedLowIsLowLevelRandomizer()
        {
            AssertNamedIsInstanceOf<ILevelRandomizer, LowLevelRandomizer>(LevelRandomizerTypeConstants.Low);
        }

        [Test]
        public void LevelRandomizerNamedMediumIsMediumLevelRandomizer()
        {
            AssertNamedIsInstanceOf<ILevelRandomizer, MediumLevelRandomizer>(LevelRandomizerTypeConstants.Medium);
        }

        [Test]
        public void LevelRandomizerNamedVeryHighIsVeryHighLevelRandomizer()
        {
            AssertNamedIsInstanceOf<ILevelRandomizer, VeryHighLevelRandomizer>(LevelRandomizerTypeConstants.VeryHigh);
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

        [Test]
        public void BaseRaceRandomizerNamedAnyIsAnyBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<IBaseRaceRandomizer, AnyBaseRaceRandomizer>(BaseRaceRandomizerTypeConstants.Any);
        }

        [Test]
        public void BaseRaceRandomizerNamedNonStandardIsNonStandardBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<IBaseRaceRandomizer, NonStandardBaseRaceRandomizer>(BaseRaceRandomizerTypeConstants.NonStandard);
        }

        [Test]
        public void BaseRaceRandomizerNamedEvilIsEvilBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<IBaseRaceRandomizer, EvilBaseRaceRandomizer>(BaseRaceRandomizerTypeConstants.Evil);
        }

        [Test]
        public void BaseRaceRandomizerNamedGoodIsGoodBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<IBaseRaceRandomizer, GoodBaseRaceRandomizer>(BaseRaceRandomizerTypeConstants.Good);
        }

        [Test]
        public void BaseRaceRandomizerNamedStandardIsStandardBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<IBaseRaceRandomizer, StandardBaseRaceRandomizer>(BaseRaceRandomizerTypeConstants.Standard);
        }

        [Test]
        public void BaseRaceRandomizerNamedNeutralIsNeutralBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<IBaseRaceRandomizer, NeutralBaseRaceRandomizer>(BaseRaceRandomizerTypeConstants.Neutral);
        }

        [Test]
        public void BaseRaceRandomizerNamedNonEvilIsNonEvilBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<IBaseRaceRandomizer, NonEvilBaseRaceRandomizer>(BaseRaceRandomizerTypeConstants.NonEvil);
        }

        [Test]
        public void BaseRaceRandomizerNamedNonGoodIsNonGoodBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<IBaseRaceRandomizer, NonGoodBaseRaceRandomizer>(BaseRaceRandomizerTypeConstants.NonGood);
        }

        [Test]
        public void BaseRaceRandomizerNamedNonNeutralIsNonNeutralBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<IBaseRaceRandomizer, NonNeutralBaseRaceRandomizer>(BaseRaceRandomizerTypeConstants.NonNeutral);
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

        [Test]
        public void MetaraceRandomizerNamedAnyIsAnyMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IMetaraceRandomizer, AnyMetaraceRandomizer>(MetaraceRandomizerTypeConstants.Any);
        }

        [Test]
        public void MetaraceRandomizerNamedGeneticIsGeneticMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IMetaraceRandomizer, GeneticMetaraceRandomizer>(MetaraceRandomizerTypeConstants.Genetic);
        }

        [Test]
        public void MetaraceRandomizerNamedEvilIsEvilMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IMetaraceRandomizer, EvilMetaraceRandomizer>(MetaraceRandomizerTypeConstants.Evil);
        }

        [Test]
        public void MetaraceRandomizerNamedGoodIsGoodMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IMetaraceRandomizer, GoodMetaraceRandomizer>(MetaraceRandomizerTypeConstants.Good);
        }

        [Test]
        public void MetaraceRandomizerNamedLycanthropeIsLycanthropeMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IMetaraceRandomizer, LycanthropeMetaraceRandomizer>(MetaraceRandomizerTypeConstants.Lycanthrope);
        }

        [Test]
        public void MetaraceRandomizerNamedNeutralIsNeutralMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IMetaraceRandomizer, NeutralMetaraceRandomizer>(MetaraceRandomizerTypeConstants.Neutral);
        }

        [Test]
        public void MetaraceRandomizerNamedNonEvilIsNonEvilMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IMetaraceRandomizer, NonEvilMetaraceRandomizer>(MetaraceRandomizerTypeConstants.NonEvil);
        }

        [Test]
        public void MetaraceRandomizerNamedNonGoodIsNonGoodMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IMetaraceRandomizer, NonGoodMetaraceRandomizer>(MetaraceRandomizerTypeConstants.NonGood);
        }

        [Test]
        public void MetaraceRandomizerNamedNonNeutralIsNonNeutralMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IMetaraceRandomizer, NonNeutralMetaraceRandomizer>(MetaraceRandomizerTypeConstants.NonNeutral);
        }

        [Test]
        public void MetaraceRandomizerNamedAnyForcedIsAnyForcedMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IMetaraceRandomizer, AnyForcedMetaraceRandomizer>(MetaraceRandomizerTypeConstants.AnyForced);
        }

        [Test]
        public void MetaraceRandomizerNamedGeneticForcedIsGeneticForcedMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IMetaraceRandomizer, GeneticForcedMetaraceRandomizer>(MetaraceRandomizerTypeConstants.GeneticForced);
        }

        [Test]
        public void MetaraceRandomizerNamedEvilForcedIsEvilForcedMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IMetaraceRandomizer, EvilForcedMetaraceRandomizer>(MetaraceRandomizerTypeConstants.EvilForced);
        }

        [Test]
        public void MetaraceRandomizerNamedGoodForcedIsGoodForcedMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IMetaraceRandomizer, GoodForcedMetaraceRandomizer>(MetaraceRandomizerTypeConstants.GoodForced);
        }

        [Test]
        public void MetaraceRandomizerNamedLycanthropeForcedIsLycanthropeForcedMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IMetaraceRandomizer, LycanthropeForcedMetaraceRandomizer>(MetaraceRandomizerTypeConstants.LycanthropeForced);
        }

        [Test]
        public void MetaraceRandomizerNamedNeutralForcedIsNeutralForcedMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IMetaraceRandomizer, NeutralForcedMetaraceRandomizer>(MetaraceRandomizerTypeConstants.NeutralForced);
        }

        [Test]
        public void MetaraceRandomizerNamedNonEvilForcedIsNonEvilForcedMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IMetaraceRandomizer, NonEvilForcedMetaraceRandomizer>(MetaraceRandomizerTypeConstants.NonEvilForced);
        }

        [Test]
        public void MetaraceRandomizerNamedNonGoodForcedIsNonGoodForcedMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IMetaraceRandomizer, NonGoodForcedMetaraceRandomizer>(MetaraceRandomizerTypeConstants.NonGoodForced);
        }

        [Test]
        public void MetaraceRandomizerNamedNonNeutralForcedIsNonNeutralForcedMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IMetaraceRandomizer, NonNeutralForcedMetaraceRandomizer>(MetaraceRandomizerTypeConstants.NonNeutralForced);
        }

        [Test]
        public void MetaraceRandomizerNamedNoneIsNoMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IMetaraceRandomizer, NoMetaraceRandomizer>(MetaraceRandomizerTypeConstants.None);
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

        [Test]
        public void StatsRandomizerNamedAverageIsAverageStatsRandomizer()
        {
            AssertNamedIsInstanceOf<IStatsRandomizer, AverageStatsRandomizer>(StatsRandomizerTypeConstants.Average);
        }

        [Test]
        public void StatsRandomizerNamedBestOfFourIsBestOfFourStatsRandomizer()
        {
            AssertNamedIsInstanceOf<IStatsRandomizer, BestOfFourStatsRandomizer>(StatsRandomizerTypeConstants.BestOfFour);
        }

        [Test]
        public void StatsRandomizerNamedGoodIsGoodStatsRandomizer()
        {
            AssertNamedIsInstanceOf<IStatsRandomizer, GoodStatsRandomizer>(StatsRandomizerTypeConstants.Good);
        }

        [Test]
        public void StatsRandomizerNamedHeroicIsHeroicStatsRandomizer()
        {
            AssertNamedIsInstanceOf<IStatsRandomizer, HeroicStatsRandomizer>(StatsRandomizerTypeConstants.Heroic);
        }

        [Test]
        public void StatsRandomizerNamedOnesAsSixesIsOnesAsSixesStatsRandomizer()
        {
            AssertNamedIsInstanceOf<IStatsRandomizer, OnesAsSixesStatsRandomizer>(StatsRandomizerTypeConstants.OnesAsSixes);
        }

        [Test]
        public void StatsRandomizerNamedPoorIsPoorStatsRandomizer()
        {
            AssertNamedIsInstanceOf<IStatsRandomizer, PoorStatsRandomizer>(StatsRandomizerTypeConstants.Poor);
        }

        [Test]
        public void StatsRandomizerNamedRawIsRawStatsRandomizer()
        {
            AssertNamedIsInstanceOf<IStatsRandomizer, RawStatsRandomizer>(StatsRandomizerTypeConstants.Raw);
        }

        [Test]
        public void StatsRandomizerNamedTwoTenSidedDiceIsTwoTenSidedDiceStatsRandomizer()
        {
            AssertNamedIsInstanceOf<IStatsRandomizer, TwoTenSidedDiceStatsRandomizer>(StatsRandomizerTypeConstants.TwoTenSidedDice);
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
using System;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Combats;
using NPCGen.Generators.Interfaces.Items;
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
        public void SetAlignmentRandomizerIsNotGeneratedAsSingleton()
        {
            AssertNotSingleton<ISetAlignmentRandomizer>();
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
        public void SetClassNameRandomizerIsNotGeneratedAsSingleton()
        {
            AssertNotSingleton<ISetClassNameRandomizer>();
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
        public void SetLevelRandomizerIsNotGeneratedAsSingleton()
        {
            AssertNotSingleton<ISetLevelRandomizer>();
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
        public void SetBaseRaceRandomizerIsNotGeneratedAsSingleton()
        {
            AssertNotSingleton<ISetBaseRaceRandomizer>();
        }

        [Test]
        public void MetaraceRandomizerNamedAnyIsAnyMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, AnyMetaraceRandomizer>(MetaraceRandomizerTypeConstants.Any);
            AssertNamedIsInstanceOf<IMetaraceRandomizer, AnyMetaraceRandomizer>(MetaraceRandomizerTypeConstants.Any);
        }

        [Test]
        public void MetaraceRandomizerNamedGeneticIsGeneticMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, GeneticMetaraceRandomizer>(MetaraceRandomizerTypeConstants.Genetic);
            AssertNamedIsInstanceOf<IMetaraceRandomizer, GeneticMetaraceRandomizer>(MetaraceRandomizerTypeConstants.Genetic);
        }

        [Test]
        public void MetaraceRandomizerNamedEvilIsEvilMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, EvilMetaraceRandomizer>(MetaraceRandomizerTypeConstants.Evil);
            AssertNamedIsInstanceOf<IMetaraceRandomizer, EvilMetaraceRandomizer>(MetaraceRandomizerTypeConstants.Evil);
        }

        [Test]
        public void MetaraceRandomizerNamedGoodIsGoodMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, GoodMetaraceRandomizer>(MetaraceRandomizerTypeConstants.Good);
            AssertNamedIsInstanceOf<IMetaraceRandomizer, GoodMetaraceRandomizer>(MetaraceRandomizerTypeConstants.Good);
        }

        [Test]
        public void MetaraceRandomizerNamedLycanthropeIsLycanthropeMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, LycanthropeMetaraceRandomizer>(MetaraceRandomizerTypeConstants.Lycanthrope);
            AssertNamedIsInstanceOf<IMetaraceRandomizer, LycanthropeMetaraceRandomizer>(MetaraceRandomizerTypeConstants.Lycanthrope);
        }

        [Test]
        public void MetaraceRandomizerNamedNeutralIsNeutralMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, NeutralMetaraceRandomizer>(MetaraceRandomizerTypeConstants.Neutral);
            AssertNamedIsInstanceOf<IMetaraceRandomizer, NeutralMetaraceRandomizer>(MetaraceRandomizerTypeConstants.Neutral);
        }

        [Test]
        public void MetaraceRandomizerNamedNonEvilIsNonEvilMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, NonEvilMetaraceRandomizer>(MetaraceRandomizerTypeConstants.NonEvil);
            AssertNamedIsInstanceOf<IMetaraceRandomizer, NonEvilMetaraceRandomizer>(MetaraceRandomizerTypeConstants.NonEvil);
        }

        [Test]
        public void MetaraceRandomizerNamedNonGoodIsNonGoodMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, NonGoodMetaraceRandomizer>(MetaraceRandomizerTypeConstants.NonGood);
            AssertNamedIsInstanceOf<IMetaraceRandomizer, NonGoodMetaraceRandomizer>(MetaraceRandomizerTypeConstants.NonGood);
        }

        [Test]
        public void MetaraceRandomizerNamedNonNeutralIsNonNeutralMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, NonNeutralMetaraceRandomizer>(MetaraceRandomizerTypeConstants.NonNeutral);
            AssertNamedIsInstanceOf<IMetaraceRandomizer, NonNeutralMetaraceRandomizer>(MetaraceRandomizerTypeConstants.NonNeutral);
        }

        [Test]
        public void MetaraceRandomizerNamedNoneIsNoMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IMetaraceRandomizer, NoMetaraceRandomizer>(MetaraceRandomizerTypeConstants.None);
        }


        [TestCase(MetaraceRandomizerTypeConstants.Any)]
        [TestCase(MetaraceRandomizerTypeConstants.Evil)]
        [TestCase(MetaraceRandomizerTypeConstants.Genetic)]
        [TestCase(MetaraceRandomizerTypeConstants.Good)]
        [TestCase(MetaraceRandomizerTypeConstants.Lycanthrope)]
        [TestCase(MetaraceRandomizerTypeConstants.Neutral)]
        [TestCase(MetaraceRandomizerTypeConstants.None)]
        [TestCase(MetaraceRandomizerTypeConstants.NonEvil)]
        [TestCase(MetaraceRandomizerTypeConstants.NonGood)]
        [TestCase(MetaraceRandomizerTypeConstants.NonNeutral)]
        public void MetaraceRandomizerIsNotGeneratedAsSingleton(String name)
        {
            AssertNotSingleton<IForcableMetaraceRandomizer>(name);
            AssertNotSingleton<IMetaraceRandomizer>(name);
        }

        [Test]
        public void SetMetaraceRandomizerIsNotGeneratedAsSingleton()
        {
            AssertNotSingleton<ISetMetaraceRandomizer>();
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

        [Test]
        public void SetStatsRandomizerIsNotGeneratedAsSingleton()
        {
            AssertNotSingleton<ISetStatsRandomizer>();
        }

        [Test]
        public void AbilitiesGeneratorIsNotGeneratedAsSingleton()
        {
            AssertNotSingleton<IAbilitiesGenerator>();
        }

        [Test]
        public void CombatGeneratorIsNotGeneratedAsSingleton()
        {
            AssertNotSingleton<ICombatGenerator>();
        }

        [Test]
        public void EquipmentGeneratorIsNotGeneratedAsSingleton()
        {
            AssertNotSingleton<IEquipmentGenerator>();
        }

        [Test]
        public void SkillsGeneratorIsNotGeneratedAsSingleton()
        {
            AssertNotSingleton<ISkillsGenerator>();
        }

        [Test]
        public void FeatsGeneratorIsNotGeneratedAsSingleton()
        {
            AssertNotSingleton<IFeatsGenerator>();
        }

        [Test]
        public void SavingThrowsGeneratorIsNotGeneratedAsSingleton()
        {
            AssertNotSingleton<ISavingThrowsGenerator>();
        }
    }
}
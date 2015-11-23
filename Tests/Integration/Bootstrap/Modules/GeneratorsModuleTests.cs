using CharacterGen.Generators;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Abilities.Feats;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Domain.Abilities;
using CharacterGen.Generators.Domain.Combats;
using CharacterGen.Generators.Domain.Randomizers.Alignments;
using CharacterGen.Generators.Domain.Randomizers.CharacterClasses.ClassNames;
using CharacterGen.Generators.Domain.Randomizers.CharacterClasses.Levels;
using CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces;
using CharacterGen.Generators.Domain.Randomizers.Races.Metaraces;
using CharacterGen.Generators.Domain.Randomizers.Stats;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Magics;
using CharacterGen.Generators.Randomizers.Alignments;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Randomizers.Stats;
using CharacterGen.Generators.Verifiers;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Bootstrap.Modules
{
    [TestFixture]
    public class GeneratorsModuleTests : BootstrapTests
    {
        [Test]
        public void AlignmentGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IAlignmentGenerator>();
        }

        [Test]
        public void CharacterGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ICharacterGenerator>();
        }

        [Test]
        public void CharacterClassGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ICharacterClassGenerator>();
        }

        [Test]
        public void HitPointsGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IHitPointsGenerator>();
        }

        [Test]
        public void LanguageGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ILanguageGenerator>();
        }

        [Test]
        public void RaceGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IRaceGenerator>();
        }

        [Test]
        public void RandomizerVerifierIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IRandomizerVerifier>();
        }

        [Test]
        public void StatsGeneratorIsNotBuiltAsSingleton()
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
        public void AlignmentRandomizerIsNotBuiltAsSingleton(String name)
        {
            AssertNotSingleton<IAlignmentRandomizer>(name);
        }

        [Test]
        public void SetAlignmentRandomizerIsNotBuiltAsSingleton()
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
        public void ClassNameRandomizerIsNotBuiltAsSingleton(String name)
        {
            AssertNotSingleton<IClassNameRandomizer>(name);
        }

        [Test]
        public void SetClassNameRandomizerIsNotBuiltAsSingleton()
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
        public void LevelRandomizerIsNotBuiltAsSingleton(String name)
        {
            AssertNotSingleton<ILevelRandomizer>(name);
        }

        [Test]
        public void SetLevelRandomizerIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ISetLevelRandomizer>();
        }

        [Test]
        public void BaseRaceRandomizerNamedAnyIsAnyBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<RaceRandomizer, AnyBaseRaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.AnyBase);
        }

        [Test]
        public void BaseRaceRandomizerNamedAnimalIsAnimalBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<RaceRandomizer, AnimalBaseRaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.AnimalBase);
        }

        [Test]
        public void BaseRaceRandomizerNamedNonStandardIsNonStandardBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<RaceRandomizer, NonStandardBaseRaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.NonStandardBase);
        }

        [Test]
        public void BaseRaceRandomizerNamedEvilIsEvilBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<RaceRandomizer, EvilBaseRaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.EvilBase);
        }

        [Test]
        public void BaseRaceRandomizerNamedGoodIsGoodBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<RaceRandomizer, GoodBaseRaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.GoodBase);
        }

        [Test]
        public void BaseRaceRandomizerNamedStandardIsStandardBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<RaceRandomizer, StandardBaseRaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.StandardBase);
        }

        [Test]
        public void BaseRaceRandomizerNamedNeutralIsNeutralBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<RaceRandomizer, NeutralBaseRaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.NeutralBase);
        }

        [Test]
        public void BaseRaceRandomizerNamedNonEvilIsNonEvilBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<RaceRandomizer, NonEvilBaseRaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.NonEvilBase);
        }

        [Test]
        public void BaseRaceRandomizerNamedNonGoodIsNonGoodBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<RaceRandomizer, NonGoodBaseRaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.NonGoodBase);
        }

        [Test]
        public void BaseRaceRandomizerNamedNonNeutralIsNonNeutralBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<RaceRandomizer, NonNeutralBaseRaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.NonNeutralBase);
        }

        [TestCase(RaceRandomizerTypeConstants.BaseRace.AnimalBase)]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.AnyBase)]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.EvilBase)]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.GoodBase)]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.NeutralBase)]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.NonEvilBase)]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.NonGoodBase)]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.NonNeutralBase)]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.NonStandardBase)]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.StandardBase)]
        public void BaseRaceRandomizerIsNotBuiltAsSingleton(String name)
        {
            AssertNotSingleton<RaceRandomizer>(name);
        }

        [Test]
        public void SetBaseRaceRandomizerIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ISetBaseRaceRandomizer>();
        }

        [Test]
        public void MetaraceRandomizerNamedAnyIsAnyMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, AnyMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.AnyMeta);
            AssertNamedIsInstanceOf<RaceRandomizer, AnyMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.AnyMeta);
        }

        [Test]
        public void MetaraceRandomizerNamedGeneticIsGeneticMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, GeneticMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.GeneticMeta);
            AssertNamedIsInstanceOf<RaceRandomizer, GeneticMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.GeneticMeta);
        }

        [Test]
        public void MetaraceRandomizerNamedEvilIsEvilMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, EvilMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.EvilMeta);
            AssertNamedIsInstanceOf<RaceRandomizer, EvilMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.EvilMeta);
        }

        [Test]
        public void MetaraceRandomizerNamedGoodIsGoodMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, GoodMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.GoodMeta);
            AssertNamedIsInstanceOf<RaceRandomizer, GoodMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.GoodMeta);
        }

        [Test]
        public void MetaraceRandomizerNamedLycanthropeIsLycanthropeMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, LycanthropeMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.LycanthropeMeta);
            AssertNamedIsInstanceOf<RaceRandomizer, LycanthropeMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.LycanthropeMeta);
        }

        [Test]
        public void MetaraceRandomizerNamedNeutralIsNeutralMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, NeutralMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.NeutralMeta);
            AssertNamedIsInstanceOf<RaceRandomizer, NeutralMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.NeutralMeta);
        }

        [Test]
        public void MetaraceRandomizerNamedNonEvilIsNonEvilMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, NonEvilMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.NonEvilMeta);
            AssertNamedIsInstanceOf<RaceRandomizer, NonEvilMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.NonEvilMeta);
        }

        [Test]
        public void MetaraceRandomizerNamedNonGoodIsNonGoodMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, NonGoodMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.NonGoodMeta);
            AssertNamedIsInstanceOf<RaceRandomizer, NonGoodMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.NonGoodMeta);
        }

        [Test]
        public void MetaraceRandomizerNamedNonNeutralIsNonNeutralMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, NonNeutralMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.NonNeutralMeta);
            AssertNamedIsInstanceOf<RaceRandomizer, NonNeutralMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.NonNeutralMeta);
        }

        [Test]
        public void MetaraceRandomizerNamedNoneIsNoMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<RaceRandomizer, NoMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.NoMeta);
        }

        [Test]
        public void MetaraceRandomizerNamedUndeadIsUndeadMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, UndeadMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.UndeadMeta);
            AssertNamedIsInstanceOf<RaceRandomizer, UndeadMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.UndeadMeta);
        }

        [TestCase(RaceRandomizerTypeConstants.Metarace.AnyMeta)]
        [TestCase(RaceRandomizerTypeConstants.Metarace.EvilMeta)]
        [TestCase(RaceRandomizerTypeConstants.Metarace.GeneticMeta)]
        [TestCase(RaceRandomizerTypeConstants.Metarace.GoodMeta)]
        [TestCase(RaceRandomizerTypeConstants.Metarace.LycanthropeMeta)]
        [TestCase(RaceRandomizerTypeConstants.Metarace.NeutralMeta)]
        [TestCase(RaceRandomizerTypeConstants.Metarace.NonEvilMeta)]
        [TestCase(RaceRandomizerTypeConstants.Metarace.NonGoodMeta)]
        [TestCase(RaceRandomizerTypeConstants.Metarace.NonNeutralMeta)]
        [TestCase(RaceRandomizerTypeConstants.Metarace.UndeadMeta)]
        public void MetaraceRandomizerIsNotBuiltAsSingleton(String name)
        {
            AssertNotSingleton<IForcableMetaraceRandomizer>(name);
            AssertNotSingleton<RaceRandomizer>(name);
        }

        [Test]
        public void NoMetaraceRandomizerIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.NoMeta);
        }

        [Test]
        public void SetMetaraceRandomizerIsNotBuiltAsSingleton()
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
        public void StatRandomizerIsNotBuiltAsSingleton(String name)
        {
            AssertNotSingleton<IStatsRandomizer>(name);
        }

        [Test]
        public void SetStatsRandomizerIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ISetStatsRandomizer>();
        }

        [Test]
        public void AbilitiesGeneratorNamedCharacterIsCharacterAbilitiesGenerator()
        {
            AssertNamedIsInstanceOf<IAbilitiesGenerator, CharacterAbilitiesGenerator>(AbilitiesGeneratorTypeConstants.Character);
        }

        [Test]
        public void AbilitiesGeneratorNamedAnimalIsAnimalAbilitiesGenerator()
        {
            AssertNamedIsInstanceOf<IAbilitiesGenerator, AnimalAbilitiesGenerator>(AbilitiesGeneratorTypeConstants.Animal);
        }

        [TestCase(AbilitiesGeneratorTypeConstants.Animal)]
        [TestCase(AbilitiesGeneratorTypeConstants.Character)]
        public void AbilitiesGeneratorIsNotBuiltAsSingleton(String name)
        {
            AssertNotSingleton<IAbilitiesGenerator>(name);
        }

        [Test]
        public void CombatGeneratorNamedCharacterIsCharacterCombatGenerator()
        {
            AssertNamedIsInstanceOf<ICombatGenerator, CharacterCombatGenerator>(AbilitiesGeneratorTypeConstants.Character);
        }

        [Test]
        public void CombatGeneratorNamedAnimalIsAnimalCombatGenerator()
        {
            AssertNamedIsInstanceOf<ICombatGenerator, AnimalCombatGenerator>(AbilitiesGeneratorTypeConstants.Animal);
        }

        [TestCase(AbilitiesGeneratorTypeConstants.Animal)]
        [TestCase(AbilitiesGeneratorTypeConstants.Character)]
        public void CombatGeneratorIsNotBuiltAsSingleton(String name)
        {
            AssertNotSingleton<ICombatGenerator>(name);
        }

        [Test]
        public void EquipmentGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IEquipmentGenerator>();
        }

        [Test]
        public void SkillsGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ISkillsGenerator>();
        }

        [Test]
        public void FeatsGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IFeatsGenerator>();
        }

        [Test]
        public void AdditionalFeatsGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IAdditionalFeatsGenerator>();
        }

        [Test]
        public void ClassFeatsGeneratorIsNotASingleton()
        {
            AssertNotSingleton<IClassFeatsGenerator>();
        }

        [Test]
        public void RacialFeatsGeneratorIsNotASingleton()
        {
            AssertNotSingleton<IRacialFeatsGenerator>();
        }

        [Test]
        public void SavingThrowsGeneratorIsNotASingleton()
        {
            AssertNotSingleton<ISavingThrowsGenerator>();
        }

        [Test]
        public void FeatFocusGeneratorIsNotASingleton()
        {
            AssertNotSingleton<IFeatFocusGenerator>();
        }

        [Test]
        public void ArmorGeneratorIsNotASingleton()
        {
            AssertNotSingleton<IArmorGenerator>();
        }

        [Test]
        public void WeaponGeneratorIsNotASingleton()
        {
            AssertNotSingleton<IWeaponGenerator>();
        }

        [Test]
        public void MagicGeneratorIsNotASingleton()
        {
            AssertNotSingleton<IMagicGenerator>();
        }

        [Test]
        public void SpellsGeneratorIsNotASingleton()
        {
            AssertNotSingleton<ISpellsGenerator>();
        }

        [Test]
        public void AnimalGeneratorIsNotASingleton()
        {
            AssertNotSingleton<IAnimalGenerator>();
        }

        [Test]
        public void IterativeGeneratorIsNotASingleton()
        {
            AssertNotSingleton<Generator>();
        }

        [Test]
        public void LeadershipGeneratorIsNotASingleton()
        {
            AssertNotSingleton<ILeadershipGenerator>();
        }
    }
}
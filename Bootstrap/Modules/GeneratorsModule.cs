using Ninject.Modules;
using NPCGen.Generators;
using NPCGen.Generators.Abilities;
using NPCGen.Generators.Combats;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Combats;
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
using NPCGen.Generators.Verifiers;

namespace NPCGen.Bootstrap.Modules
{
    public class GeneratorsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAlignmentGenerator>().To<AlignmentGenerator>();
            Bind<ICharacterClassGenerator>().To<CharacterClassGenerator>();
            Bind<ICharacterGenerator>().To<CharacterGenerator>();
            Bind<IHitPointsGenerator>().To<HitPointsGenerator>();
            Bind<ILanguageGenerator>().To<LanguageGenerator>();
            Bind<IRaceGenerator>().To<RaceGenerator>();
            Bind<IRandomizerVerifier>().To<RandomizerVerifier>();
            Bind<IStatsGenerator>().To<StatsGenerator>();

            Bind<IAlignmentRandomizer>().To<AnyAlignmentRandomizer>().Named(AlignmentRandomizerTypeConstants.Any);
            Bind<IAlignmentRandomizer>().To<ChaoticAlignmentRandomizer>().Named(AlignmentRandomizerTypeConstants.Chaotic);
            Bind<IAlignmentRandomizer>().To<EvilAlignmentRandomizer>().Named(AlignmentRandomizerTypeConstants.Evil);
            Bind<IAlignmentRandomizer>().To<GoodAlignmentRandomizer>().Named(AlignmentRandomizerTypeConstants.Good);
            Bind<IAlignmentRandomizer>().To<LawfulAlignmentRandomizer>().Named(AlignmentRandomizerTypeConstants.Lawful);
            Bind<IAlignmentRandomizer>().To<NeutralAlignmentRandomizer>().Named(AlignmentRandomizerTypeConstants.Neutral);
            Bind<IAlignmentRandomizer>().To<NonChaoticAlignmentRandomizer>().Named(AlignmentRandomizerTypeConstants.NonChaotic);
            Bind<IAlignmentRandomizer>().To<NonEvilAlignmentRandomizer>().Named(AlignmentRandomizerTypeConstants.NonEvil);
            Bind<IAlignmentRandomizer>().To<NonGoodAlignmentRandomizer>().Named(AlignmentRandomizerTypeConstants.NonGood);
            Bind<IAlignmentRandomizer>().To<NonLawfulAlignmentRandomizer>().Named(AlignmentRandomizerTypeConstants.NonLawful);
            Bind<IAlignmentRandomizer>().To<NonNeutralAlignmentRandomizer>().Named(AlignmentRandomizerTypeConstants.NonNeutral);

            Bind<IClassNameRandomizer>().To<AnyClassNameRandomizer>().Named(ClassNameRandomizerTypeConstants.Any);
            Bind<IClassNameRandomizer>().To<HealerClassNameRandomizer>().Named(ClassNameRandomizerTypeConstants.Healer);
            Bind<IClassNameRandomizer>().To<MageClassNameRandomizer>().Named(ClassNameRandomizerTypeConstants.Mage);
            Bind<IClassNameRandomizer>().To<NonSpellcasterClassNameRandomizer>().Named(ClassNameRandomizerTypeConstants.NonSpellcaster);
            Bind<IClassNameRandomizer>().To<SpellcasterClassNameRandomizer>().Named(ClassNameRandomizerTypeConstants.Spellcaster);
            Bind<IClassNameRandomizer>().To<StealthClassNameRandomizer>().Named(ClassNameRandomizerTypeConstants.Stealth);
            Bind<IClassNameRandomizer>().To<WarriorClassNameRandomizer>().Named(ClassNameRandomizerTypeConstants.Warrior);

            Bind<ILevelRandomizer>().To<AnyLevelRandomizer>().Named(LevelRandomizerTypeConstants.Any);
            Bind<ILevelRandomizer>().To<HighLevelRandomizer>().Named(LevelRandomizerTypeConstants.High);
            Bind<ILevelRandomizer>().To<LowLevelRandomizer>().Named(LevelRandomizerTypeConstants.Low);
            Bind<ILevelRandomizer>().To<MediumLevelRandomizer>().Named(LevelRandomizerTypeConstants.Medium);
            Bind<ILevelRandomizer>().To<VeryHighLevelRandomizer>().Named(LevelRandomizerTypeConstants.VeryHigh);

            Bind<IBaseRaceRandomizer>().To<AnyBaseRaceRandomizer>().Named(BaseRaceRandomizerTypeConstants.Any);
            Bind<IBaseRaceRandomizer>().To<EvilBaseRaceRandomizer>().Named(BaseRaceRandomizerTypeConstants.Evil);
            Bind<IBaseRaceRandomizer>().To<GoodBaseRaceRandomizer>().Named(BaseRaceRandomizerTypeConstants.Good);
            Bind<IBaseRaceRandomizer>().To<NeutralBaseRaceRandomizer>().Named(BaseRaceRandomizerTypeConstants.Neutral);
            Bind<IBaseRaceRandomizer>().To<NonEvilBaseRaceRandomizer>().Named(BaseRaceRandomizerTypeConstants.NonEvil);
            Bind<IBaseRaceRandomizer>().To<NonGoodBaseRaceRandomizer>().Named(BaseRaceRandomizerTypeConstants.NonGood);
            Bind<IBaseRaceRandomizer>().To<NonNeutralBaseRaceRandomizer>().Named(BaseRaceRandomizerTypeConstants.NonNeutral);
            Bind<IBaseRaceRandomizer>().To<NonStandardBaseRaceRandomizer>().Named(BaseRaceRandomizerTypeConstants.NonStandard);
            Bind<IBaseRaceRandomizer>().To<StandardBaseRaceRandomizer>().Named(BaseRaceRandomizerTypeConstants.Standard);

            Bind<IMetaraceRandomizer>().To<AnyMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.Any);
            Bind<IMetaraceRandomizer>().To<EvilMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.Evil);
            Bind<IMetaraceRandomizer>().To<GeneticMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.Genetic);
            Bind<IMetaraceRandomizer>().To<GoodMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.Good);
            Bind<IMetaraceRandomizer>().To<LycanthropeMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.Lycanthrope);
            Bind<IMetaraceRandomizer>().To<NeutralMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.Neutral);
            Bind<IMetaraceRandomizer>().To<NoMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.None);
            Bind<IMetaraceRandomizer>().To<NonEvilMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.NonEvil);
            Bind<IMetaraceRandomizer>().To<NonGoodMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.NonGood);
            Bind<IMetaraceRandomizer>().To<NonNeutralMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.NonNeutral);
            Bind<IMetaraceRandomizer>().To<AnyForcedMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.AnyForced);
            Bind<IMetaraceRandomizer>().To<EvilForcedMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.EvilForced);
            Bind<IMetaraceRandomizer>().To<GeneticForcedMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.GeneticForced);
            Bind<IMetaraceRandomizer>().To<GoodForcedMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.GoodForced);
            Bind<IMetaraceRandomizer>().To<LycanthropeForcedMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.LycanthropeForced);
            Bind<IMetaraceRandomizer>().To<NeutralForcedMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.NeutralForced);
            Bind<IMetaraceRandomizer>().To<NonEvilForcedMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.NonEvilForced);
            Bind<IMetaraceRandomizer>().To<NonGoodForcedMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.NonGoodForced);
            Bind<IMetaraceRandomizer>().To<NonNeutralForcedMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.NonNeutralForced);

            Bind<IStatsRandomizer>().To<AverageStatsRandomizer>().Named(StatsRandomizerTypeConstants.Average);
            Bind<IStatsRandomizer>().To<BestOfFourStatsRandomizer>().Named(StatsRandomizerTypeConstants.BestOfFour);
            Bind<IStatsRandomizer>().To<GoodStatsRandomizer>().Named(StatsRandomizerTypeConstants.Good);
            Bind<IStatsRandomizer>().To<HeroicStatsRandomizer>().Named(StatsRandomizerTypeConstants.Heroic);
            Bind<IStatsRandomizer>().To<OnesAsSixesStatsRandomizer>().Named(StatsRandomizerTypeConstants.OnesAsSixes);
            Bind<IStatsRandomizer>().To<PoorStatsRandomizer>().Named(StatsRandomizerTypeConstants.Poor);
            Bind<IStatsRandomizer>().To<RawStatsRandomizer>().Named(StatsRandomizerTypeConstants.Raw);
            Bind<IStatsRandomizer>().To<TwoTenSidedDiceStatsRandomizer>().Named(StatsRandomizerTypeConstants.TwoTenSidedDice);
        }
    }
}
using Ninject.Modules;
using CharacterGen.Bootstrap.Factories;
using CharacterGen.Generators;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Abilities.Feats;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Domain;
using CharacterGen.Generators.Domain.Abilities;
using CharacterGen.Generators.Domain.Abilities.Feats;
using CharacterGen.Generators.Domain.Combats;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Randomizers.Alignments;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Randomizers.Stats;
using CharacterGen.Generators.Verifiers;
using CharacterGen.Generators.Domain.Items;
using CharacterGen.Generators.Domain.Randomizers.Alignments;
using CharacterGen.Generators.Domain.Randomizers.CharacterClasses.ClassNames;
using CharacterGen.Generators.Domain.Randomizers.CharacterClasses.Levels;
using CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces;
using CharacterGen.Generators.Domain.Randomizers.Races.Metaraces;
using CharacterGen.Generators.Domain.Randomizers.Stats;
using CharacterGen.Generators.Domain.Verifiers;

namespace CharacterGen.Bootstrap.Modules
{
    public class GeneratorsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAlignmentGenerator>().To<AlignmentGenerator>();
            Bind<ICharacterClassGenerator>().To<CharacterClassGenerator>();
            Bind<ICharacterGenerator>().ToMethod(c => CharacterGeneratorFactory.CreateWith(c.Kernel));
            Bind<IHitPointsGenerator>().To<HitPointsGenerator>();
            Bind<ILanguageGenerator>().To<LanguageGenerator>();
            Bind<IRaceGenerator>().To<RaceGenerator>();
            Bind<IRandomizerVerifier>().To<RandomizerVerifier>();
            Bind<IStatsGenerator>().To<StatsGenerator>();
            Bind<IAbilitiesGenerator>().To<AbilitiesGenerator>();
            Bind<ICombatGenerator>().To<CombatGenerator>();
            Bind<IEquipmentGenerator>().To<EquipmentGenerator>();
            Bind<ISkillsGenerator>().To<SkillsGenerator>();
            Bind<IFeatsGenerator>().To<FeatsGenerator>();
            Bind<IArmorClassGenerator>().To<ArmorClassGenerator>();
            Bind<ISavingThrowsGenerator>().To<SavingThrowsGenerator>();
            Bind<IAdditionalFeatsGenerator>().To<AdditionalFeatsGenerator>();
            Bind<IClassFeatsGenerator>().To<ClassFeatsGenerator>();
            Bind<IRacialFeatsGenerator>().To<RacialFeatsGenerator>();
            Bind<IFeatFocusGenerator>().To<FeatFocusGenerator>();

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

            Bind<IForcableMetaraceRandomizer>().To<AnyMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.Any);
            Bind<IForcableMetaraceRandomizer>().To<EvilMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.Evil);
            Bind<IForcableMetaraceRandomizer>().To<GeneticMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.Genetic);
            Bind<IForcableMetaraceRandomizer>().To<GoodMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.Good);
            Bind<IForcableMetaraceRandomizer>().To<LycanthropeMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.Lycanthrope);
            Bind<IForcableMetaraceRandomizer>().To<NeutralMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.Neutral);
            Bind<IForcableMetaraceRandomizer>().To<NonEvilMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.NonEvil);
            Bind<IForcableMetaraceRandomizer>().To<NonGoodMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.NonGood);
            Bind<IForcableMetaraceRandomizer>().To<NonNeutralMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.NonNeutral);

            Bind<IMetaraceRandomizer>().To<AnyMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.Any);
            Bind<IMetaraceRandomizer>().To<EvilMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.Evil);
            Bind<IMetaraceRandomizer>().To<GeneticMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.Genetic);
            Bind<IMetaraceRandomizer>().To<GoodMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.Good);
            Bind<IMetaraceRandomizer>().To<LycanthropeMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.Lycanthrope);
            Bind<IMetaraceRandomizer>().To<NeutralMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.Neutral);
            Bind<IMetaraceRandomizer>().To<NonEvilMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.NonEvil);
            Bind<IMetaraceRandomizer>().To<NonGoodMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.NonGood);
            Bind<IMetaraceRandomizer>().To<NonNeutralMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.NonNeutral);
            Bind<IMetaraceRandomizer>().To<NoMetaraceRandomizer>().Named(MetaraceRandomizerTypeConstants.None);

            Bind<IStatsRandomizer>().To<AverageStatsRandomizer>().Named(StatsRandomizerTypeConstants.Average);
            Bind<IStatsRandomizer>().To<BestOfFourStatsRandomizer>().Named(StatsRandomizerTypeConstants.BestOfFour);
            Bind<IStatsRandomizer>().To<GoodStatsRandomizer>().Named(StatsRandomizerTypeConstants.Good);
            Bind<IStatsRandomizer>().To<HeroicStatsRandomizer>().Named(StatsRandomizerTypeConstants.Heroic);
            Bind<IStatsRandomizer>().To<OnesAsSixesStatsRandomizer>().Named(StatsRandomizerTypeConstants.OnesAsSixes);
            Bind<IStatsRandomizer>().To<PoorStatsRandomizer>().Named(StatsRandomizerTypeConstants.Poor);
            Bind<IStatsRandomizer>().To<RawStatsRandomizer>().Named(StatsRandomizerTypeConstants.Raw);
            Bind<IStatsRandomizer>().To<TwoTenSidedDiceStatsRandomizer>().Named(StatsRandomizerTypeConstants.TwoTenSidedDice);

            Bind<ISetAlignmentRandomizer>().To<SetAlignmentRandomizer>();
            Bind<ISetClassNameRandomizer>().To<SetClassNameRandomizer>();
            Bind<ISetLevelRandomizer>().To<SetLevelRandomizer>();
            Bind<ISetBaseRaceRandomizer>().To<SetBaseRaceRandomizer>();
            Bind<ISetMetaraceRandomizer>().To<SetMetaraceRandomizer>();
            Bind<ISetStatsRandomizer>().To<SetStatsRandomizer>();
        }
    }
}
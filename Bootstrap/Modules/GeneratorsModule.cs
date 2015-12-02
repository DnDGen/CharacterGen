using CharacterGen.Bootstrap.Factories;
using CharacterGen.Generators;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Abilities.Feats;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Domain;
using CharacterGen.Generators.Domain.Abilities;
using CharacterGen.Generators.Domain.Abilities.Feats;
using CharacterGen.Generators.Domain.Combats;
using CharacterGen.Generators.Domain.Items;
using CharacterGen.Generators.Domain.Magics;
using CharacterGen.Generators.Domain.Randomizers.Alignments;
using CharacterGen.Generators.Domain.Randomizers.CharacterClasses.ClassNames;
using CharacterGen.Generators.Domain.Randomizers.CharacterClasses.Levels;
using CharacterGen.Generators.Domain.Randomizers.Races.BaseRaces;
using CharacterGen.Generators.Domain.Randomizers.Races.Metaraces;
using CharacterGen.Generators.Domain.Randomizers.Stats;
using CharacterGen.Generators.Domain.Verifiers;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Magics;
using CharacterGen.Generators.Randomizers.Alignments;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Randomizers.Stats;
using CharacterGen.Generators.Verifiers;
using Ninject;
using Ninject.Modules;

namespace CharacterGen.Bootstrap.Modules
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
            Bind<ILeadershipGenerator>().ToMethod(c => LeadershipGeneratorFactory.Create(c.Kernel));
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
            Bind<IArmorGenerator>().ToMethod(c => ArmorGeneratorFactory.CreateWith(c.Kernel));
            Bind<IWeaponGenerator>().ToMethod(c => WeaponGeneratorFactory.CreateWith(c.Kernel));
            Bind<IMagicGenerator>().To<MagicGenerator>();
            Bind<ISpellsGenerator>().To<SpellsGenerator>();
            Bind<IAnimalGenerator>().To<AnimalGenerator>();
            Bind<Generator>().To<IterativeGenerator>();

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

            Bind<RaceRandomizer>().To<AnyBaseRaceRandomizer>().Named(RaceRandomizerTypeConstants.BaseRace.AnyBase);
            Bind<RaceRandomizer>().To<EvilBaseRaceRandomizer>().Named(RaceRandomizerTypeConstants.BaseRace.EvilBase);
            Bind<RaceRandomizer>().To<GoodBaseRaceRandomizer>().Named(RaceRandomizerTypeConstants.BaseRace.GoodBase);
            Bind<RaceRandomizer>().To<NeutralBaseRaceRandomizer>().Named(RaceRandomizerTypeConstants.BaseRace.NeutralBase);
            Bind<RaceRandomizer>().To<NonEvilBaseRaceRandomizer>().Named(RaceRandomizerTypeConstants.BaseRace.NonEvilBase);
            Bind<RaceRandomizer>().To<NonGoodBaseRaceRandomizer>().Named(RaceRandomizerTypeConstants.BaseRace.NonGoodBase);
            Bind<RaceRandomizer>().To<NonNeutralBaseRaceRandomizer>().Named(RaceRandomizerTypeConstants.BaseRace.NonNeutralBase);
            Bind<RaceRandomizer>().To<NonStandardBaseRaceRandomizer>().Named(RaceRandomizerTypeConstants.BaseRace.NonStandardBase);
            Bind<RaceRandomizer>().To<StandardBaseRaceRandomizer>().Named(RaceRandomizerTypeConstants.BaseRace.StandardBase);

            Bind<IForcableMetaraceRandomizer>().To<AnyMetaraceRandomizer>().Named(RaceRandomizerTypeConstants.Metarace.AnyMeta);
            Bind<IForcableMetaraceRandomizer>().To<EvilMetaraceRandomizer>().Named(RaceRandomizerTypeConstants.Metarace.EvilMeta);
            Bind<IForcableMetaraceRandomizer>().To<GeneticMetaraceRandomizer>().Named(RaceRandomizerTypeConstants.Metarace.GeneticMeta);
            Bind<IForcableMetaraceRandomizer>().To<GoodMetaraceRandomizer>().Named(RaceRandomizerTypeConstants.Metarace.GoodMeta);
            Bind<IForcableMetaraceRandomizer>().To<LycanthropeMetaraceRandomizer>().Named(RaceRandomizerTypeConstants.Metarace.LycanthropeMeta);
            Bind<IForcableMetaraceRandomizer>().To<NeutralMetaraceRandomizer>().Named(RaceRandomizerTypeConstants.Metarace.NeutralMeta);
            Bind<IForcableMetaraceRandomizer>().To<NonEvilMetaraceRandomizer>().Named(RaceRandomizerTypeConstants.Metarace.NonEvilMeta);
            Bind<IForcableMetaraceRandomizer>().To<NonGoodMetaraceRandomizer>().Named(RaceRandomizerTypeConstants.Metarace.NonGoodMeta);
            Bind<IForcableMetaraceRandomizer>().To<NonNeutralMetaraceRandomizer>().Named(RaceRandomizerTypeConstants.Metarace.NonNeutralMeta);
            Bind<IForcableMetaraceRandomizer>().To<UndeadMetaraceRandomizer>().Named(RaceRandomizerTypeConstants.Metarace.UndeadMeta);

            Bind<RaceRandomizer>().ToMethod(c => c.Kernel.Get<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.AnyMeta)).Named(RaceRandomizerTypeConstants.Metarace.AnyMeta);
            Bind<RaceRandomizer>().ToMethod(c => c.Kernel.Get<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.EvilMeta)).Named(RaceRandomizerTypeConstants.Metarace.EvilMeta);
            Bind<RaceRandomizer>().ToMethod(c => c.Kernel.Get<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.GeneticMeta)).Named(RaceRandomizerTypeConstants.Metarace.GeneticMeta);
            Bind<RaceRandomizer>().ToMethod(c => c.Kernel.Get<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.GoodMeta)).Named(RaceRandomizerTypeConstants.Metarace.GoodMeta);
            Bind<RaceRandomizer>().ToMethod(c => c.Kernel.Get<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.LycanthropeMeta)).Named(RaceRandomizerTypeConstants.Metarace.LycanthropeMeta);
            Bind<RaceRandomizer>().ToMethod(c => c.Kernel.Get<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.NeutralMeta)).Named(RaceRandomizerTypeConstants.Metarace.NeutralMeta);
            Bind<RaceRandomizer>().ToMethod(c => c.Kernel.Get<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.NonEvilMeta)).Named(RaceRandomizerTypeConstants.Metarace.NonEvilMeta);
            Bind<RaceRandomizer>().ToMethod(c => c.Kernel.Get<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.NonGoodMeta)).Named(RaceRandomizerTypeConstants.Metarace.NonGoodMeta);
            Bind<RaceRandomizer>().ToMethod(c => c.Kernel.Get<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.NonNeutralMeta)).Named(RaceRandomizerTypeConstants.Metarace.NonNeutralMeta);
            Bind<RaceRandomizer>().ToMethod(c => c.Kernel.Get<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.UndeadMeta)).Named(RaceRandomizerTypeConstants.Metarace.UndeadMeta);
            Bind<RaceRandomizer>().To<NoMetaraceRandomizer>().Named(RaceRandomizerTypeConstants.Metarace.NoMeta);

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
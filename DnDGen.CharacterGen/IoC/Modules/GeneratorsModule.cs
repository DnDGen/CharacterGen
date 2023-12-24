using DnDGen.CharacterGen.Generators;
using DnDGen.CharacterGen.Generators.Abilities;
using DnDGen.CharacterGen.Generators.Alignments;
using DnDGen.CharacterGen.Generators.Characters;
using DnDGen.CharacterGen.Generators.Classes;
using DnDGen.CharacterGen.Generators.Combats;
using DnDGen.CharacterGen.Generators.Feats;
using DnDGen.CharacterGen.Generators.Items;
using DnDGen.CharacterGen.Generators.Languages;
using DnDGen.CharacterGen.Generators.Magics;
using DnDGen.CharacterGen.Generators.Races;
using DnDGen.CharacterGen.Generators.Randomizers.Abilities;
using DnDGen.CharacterGen.Generators.Randomizers.Alignments;
using DnDGen.CharacterGen.Generators.Randomizers.CharacterClasses.ClassNames;
using DnDGen.CharacterGen.Generators.Randomizers.CharacterClasses.Levels;
using DnDGen.CharacterGen.Generators.Randomizers.Races.BaseRaces;
using DnDGen.CharacterGen.Generators.Randomizers.Races.Metaraces;
using DnDGen.CharacterGen.Generators.Skills;
using DnDGen.CharacterGen.Generators.Verifiers;
using DnDGen.CharacterGen.Leaders;
using DnDGen.CharacterGen.Randomizers.Abilities;
using DnDGen.CharacterGen.Randomizers.Alignments;
using DnDGen.CharacterGen.Randomizers.CharacterClasses;
using DnDGen.CharacterGen.Randomizers.Races;
using DnDGen.CharacterGen.Verifiers;
using Ninject;
using Ninject.Modules;

namespace DnDGen.CharacterGen.IoC.Modules
{
    internal class GeneratorsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRandomizerVerifier>().To<RandomizerVerifier>();

            Bind<IHitPointsGenerator>().To<HitPointsGenerator>();
            Bind<ILeadershipGenerator>().To<LeadershipGenerator>();
            Bind<IArmorClassGenerator>().To<ArmorClassGenerator>();
            Bind<ISavingThrowsGenerator>().To<SavingThrowsGenerator>();
            Bind<IArmorGenerator>().To<ArmorGenerator>();
            Bind<IWeaponGenerator>().To<WeaponGenerator>();
            Bind<ISpellsGenerator>().To<SpellsGenerator>();
            Bind<IAnimalGenerator>().To<AnimalGenerator>();

            BindDecoratedGenerators();
            BindRandomizers();
        }

        private void BindDecoratedGenerators()
        {
            Bind<ICharacterGenerator>().To<CharacterGenerator>();
            Bind<IAlignmentGenerator>().To<AlignmentGenerator>();
            Bind<ICharacterClassGenerator>().To<CharacterClassGenerator>();
            Bind<IRaceGenerator>().To<RaceGenerator>();
            Bind<IAbilitiesGenerator>().To<AbilitiesGenerator>();
            Bind<ICombatGenerator>().To<CombatGenerator>();
            Bind<ISkillsGenerator>().To<SkillsGenerator>();
            Bind<IFeatsGenerator>().To<FeatsGenerator>();
            Bind<IAdditionalFeatsGenerator>().To<AdditionalFeatsGenerator>();
            Bind<IClassFeatsGenerator>().To<ClassFeatsGenerator>();
            Bind<IRacialFeatsGenerator>().To<RacialFeatsGenerator>();
            Bind<IFeatFocusGenerator>().To<FeatFocusGenerator>();
            Bind<ILanguageGenerator>().To<LanguageGenerator>();
            Bind<IEquipmentGenerator>().To<EquipmentGenerator>();
            Bind<IMagicGenerator>().To<MagicGenerator>();
        }

        private void BindRandomizers()
        {
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

            Bind<IClassNameRandomizer>().To<AnyPlayerClassNameRandomizer>().Named(ClassNameRandomizerTypeConstants.AnyPlayer);
            Bind<IClassNameRandomizer>().To<AnyNPCClassNameRandomizer>().Named(ClassNameRandomizerTypeConstants.AnyNPC);
            Bind<IClassNameRandomizer>().To<DivineSpellcasterClassNameRandomizer>().Named(ClassNameRandomizerTypeConstants.DivineSpellcaster);
            Bind<IClassNameRandomizer>().To<ArcaneSpellcasterClassNameRandomizer>().Named(ClassNameRandomizerTypeConstants.ArcaneSpellcaster);
            Bind<IClassNameRandomizer>().To<NonSpellcasterClassNameRandomizer>().Named(ClassNameRandomizerTypeConstants.NonSpellcaster);
            Bind<IClassNameRandomizer>().To<SpellcasterClassNameRandomizer>().Named(ClassNameRandomizerTypeConstants.Spellcaster);
            Bind<IClassNameRandomizer>().To<StealthClassNameRandomizer>().Named(ClassNameRandomizerTypeConstants.Stealth);
            Bind<IClassNameRandomizer>().To<PhysicalCombatClassNameRandomizer>().Named(ClassNameRandomizerTypeConstants.PhysicalCombat);

            Bind<ILevelRandomizer>().To<AnyLevelRandomizer>().Named(LevelRandomizerTypeConstants.Any);
            Bind<ILevelRandomizer>().To<HighLevelRandomizer>().Named(LevelRandomizerTypeConstants.High);
            Bind<ILevelRandomizer>().To<LowLevelRandomizer>().Named(LevelRandomizerTypeConstants.Low);
            Bind<ILevelRandomizer>().To<MediumLevelRandomizer>().Named(LevelRandomizerTypeConstants.Medium);
            Bind<ILevelRandomizer>().To<VeryHighLevelRandomizer>().Named(LevelRandomizerTypeConstants.VeryHigh);

            Bind<RaceRandomizer>().To<AnyBaseRaceRandomizer>().Named(RaceRandomizerTypeConstants.BaseRace.AnyBase);
            Bind<RaceRandomizer>().To<AquaticBaseRaceRandomizer>().Named(RaceRandomizerTypeConstants.BaseRace.AquaticBase);
            Bind<RaceRandomizer>().To<MonsterBaseRaceRandomizer>().Named(RaceRandomizerTypeConstants.BaseRace.MonsterBase);
            Bind<RaceRandomizer>().To<NonMonsterBaseRaceRandomizer>().Named(RaceRandomizerTypeConstants.BaseRace.NonMonsterBase);
            Bind<RaceRandomizer>().To<NonStandardBaseRaceRandomizer>().Named(RaceRandomizerTypeConstants.BaseRace.NonStandardBase);
            Bind<RaceRandomizer>().To<StandardBaseRaceRandomizer>().Named(RaceRandomizerTypeConstants.BaseRace.StandardBase);

            Bind<IForcableMetaraceRandomizer>().To<AnyMetaraceRandomizer>().Named(RaceRandomizerTypeConstants.Metarace.AnyMeta);
            Bind<IForcableMetaraceRandomizer>().To<GeneticMetaraceRandomizer>().Named(RaceRandomizerTypeConstants.Metarace.GeneticMeta);
            Bind<IForcableMetaraceRandomizer>().To<LycanthropeMetaraceRandomizer>().Named(RaceRandomizerTypeConstants.Metarace.LycanthropeMeta);
            Bind<IForcableMetaraceRandomizer>().To<UndeadMetaraceRandomizer>().Named(RaceRandomizerTypeConstants.Metarace.UndeadMeta);

            Bind<RaceRandomizer>().ToMethod(c => c.Kernel.Get<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.AnyMeta)).Named(RaceRandomizerTypeConstants.Metarace.AnyMeta);
            Bind<RaceRandomizer>().ToMethod(c => c.Kernel.Get<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.GeneticMeta)).Named(RaceRandomizerTypeConstants.Metarace.GeneticMeta);
            Bind<RaceRandomizer>().ToMethod(c => c.Kernel.Get<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.LycanthropeMeta)).Named(RaceRandomizerTypeConstants.Metarace.LycanthropeMeta);
            Bind<RaceRandomizer>().ToMethod(c => c.Kernel.Get<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.UndeadMeta)).Named(RaceRandomizerTypeConstants.Metarace.UndeadMeta);
            Bind<RaceRandomizer>().To<NoMetaraceRandomizer>().Named(RaceRandomizerTypeConstants.Metarace.NoMeta);

            Bind<IAbilitiesRandomizer>().To<AverageAbilitiesRandomizer>().Named(AbilitiesRandomizerTypeConstants.Average);
            Bind<IAbilitiesRandomizer>().To<BestOfFourAbilitiesRandomizer>().Named(AbilitiesRandomizerTypeConstants.BestOfFour);
            Bind<IAbilitiesRandomizer>().To<GoodAbilitiesRandomizer>().Named(AbilitiesRandomizerTypeConstants.Good);
            Bind<IAbilitiesRandomizer>().To<HeroicAbilitiesRandomizer>().Named(AbilitiesRandomizerTypeConstants.Heroic);
            Bind<IAbilitiesRandomizer>().To<OnesAsSixesAbilitiesRandomizer>().Named(AbilitiesRandomizerTypeConstants.OnesAsSixes);
            Bind<IAbilitiesRandomizer>().To<PoorAbilitiesRandomizer>().Named(AbilitiesRandomizerTypeConstants.Poor);
            Bind<IAbilitiesRandomizer>().To<RawAbilitiesRandomizer>().Named(AbilitiesRandomizerTypeConstants.Raw);
            Bind<IAbilitiesRandomizer>().To<TwoTenSidedDiceAbilitiesRandomizer>().Named(AbilitiesRandomizerTypeConstants.TwoTenSidedDice);

            Bind<ISetAlignmentRandomizer>().To<SetAlignmentRandomizer>();
            Bind<ISetClassNameRandomizer>().To<SetClassNameRandomizer>();
            Bind<ISetLevelRandomizer>().To<SetLevelRandomizer>();
            Bind<ISetBaseRaceRandomizer>().To<SetBaseRaceRandomizer>();
            Bind<ISetMetaraceRandomizer>().To<SetMetaraceRandomizer>();
            Bind<ISetAbilitiesRandomizer>().To<SetAbilitiesRandomizer>();
        }
    }
}
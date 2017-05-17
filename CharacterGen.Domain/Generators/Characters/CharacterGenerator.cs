using CharacterGen.Abilities.Skills;
using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Characters;
using CharacterGen.Domain.Generators.Abilities;
using CharacterGen.Domain.Generators.Alignments;
using CharacterGen.Domain.Generators.Classes;
using CharacterGen.Domain.Generators.Combats;
using CharacterGen.Domain.Generators.Items;
using CharacterGen.Domain.Generators.Magics;
using CharacterGen.Domain.Generators.Races;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using CharacterGen.Randomizers.Alignments;
using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Randomizers.Races;
using CharacterGen.Randomizers.Stats;
using CharacterGen.Verifiers;
using CharacterGen.Verifiers.Exceptions;
using System.Linq;
using TreasureGen.Items;

namespace CharacterGen.Domain.Generators.Characters
{
    internal class CharacterGenerator : ICharacterGenerator
    {
        private IAlignmentGenerator alignmentGenerator;
        private ICharacterClassGenerator characterClassGenerator;
        private IRaceGenerator raceGenerator;
        private IAdjustmentsSelector adjustmentsSelector;
        private IRandomizerVerifier randomizerVerifier;
        private IPercentileSelector percentileSelector;
        private IAbilitiesGenerator abilitiesGenerator;
        private ICombatGenerator combatGenerator;
        private IEquipmentGenerator equipmentGenerator;
        private IMagicGenerator magicGenerator;
        private Generator generator;
        private ICollectionsSelector collectionsSelector;

        public CharacterGenerator(IAlignmentGenerator alignmentGenerator,
            ICharacterClassGenerator characterClassGenerator,
            IRaceGenerator raceGenerator,
            IAdjustmentsSelector adjustmentsSelector,
            IRandomizerVerifier randomizerVerifier,
            IPercentileSelector percentileSelector,
            IAbilitiesGenerator abilitiesGenerator,
            ICombatGenerator combatGenerator,
            IEquipmentGenerator equipmentGenerator,
            IMagicGenerator magicGenerator,
            Generator generator,
            ICollectionsSelector collectionsSelector)
        {
            this.alignmentGenerator = alignmentGenerator;
            this.characterClassGenerator = characterClassGenerator;
            this.raceGenerator = raceGenerator;
            this.abilitiesGenerator = abilitiesGenerator;
            this.combatGenerator = combatGenerator;
            this.equipmentGenerator = equipmentGenerator;
            this.generator = generator;

            this.adjustmentsSelector = adjustmentsSelector;
            this.randomizerVerifier = randomizerVerifier;
            this.percentileSelector = percentileSelector;
            this.magicGenerator = magicGenerator;
            this.collectionsSelector = collectionsSelector;
        }

        public Character GenerateWith(IAlignmentRandomizer alignmentRandomizer,
            IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer,
            RaceRandomizer baseRaceRandomizer,
            RaceRandomizer metaraceRandomizer,
            IStatsRandomizer statsRandomizer)
        {
            VerifyRandomizers(alignmentRandomizer, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer);

            var character = GenerateCharacter(alignmentRandomizer, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer, statsRandomizer);

            return character;
        }

        private Character GenerateCharacter(IAlignmentRandomizer alignmentRandomizer,
            IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer,
            RaceRandomizer baseRaceRandomizer,
            RaceRandomizer metaraceRandomizer,
            IStatsRandomizer statsRandomizer)
        {
            var character = new Character();

            character.Alignment = GenerateAlignment(alignmentRandomizer, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer);
            character.Class = GenerateCharacterClass(classNameRandomizer, levelRandomizer, character.Alignment, baseRaceRandomizer, metaraceRandomizer);
            character.Race = GenerateRace(baseRaceRandomizer, metaraceRandomizer, character.Alignment, character.Class, levelRandomizer);

            character.Class = EditCharacterClass(character.Alignment, character.Class, character.Race, levelRandomizer);

            var stats = abilitiesGenerator.GenerateStats(character.Class, character.Race, statsRandomizer);
            var baseAttack = combatGenerator.GenerateBaseAttackWith(character.Class, character.Race, stats);

            character.Ability = abilitiesGenerator.GenerateWith(character.Class, character.Race, stats, baseAttack);
            character.Equipment = equipmentGenerator.GenerateWith(character.Ability.Feats, character.Class, character.Race);

            var armorCheckPenaltySkills = character.Ability.Skills.Where(s => s.HasArmorCheckPenalty);

            foreach (var skill in armorCheckPenaltySkills)
            {
                skill.ArmorCheckPenalty += ComputeArmorCheckPenalty(character.Equipment.Armor, skill.Name);

                if (character.Equipment.OffHand != null && character.Equipment.OffHand.Attributes.Contains(AttributeConstants.Shield))
                {
                    skill.ArmorCheckPenalty += ComputeArmorCheckPenalty(character.Equipment.OffHand, skill.Name);
                }
            }

            character.Combat = combatGenerator.GenerateWith(baseAttack, character.Class, character.Race, character.Ability.Feats, character.Ability.Stats, character.Equipment);
            character.InterestingTrait = percentileSelector.SelectFrom(TableNameConstants.Set.Percentile.Traits);
            character.Magic = magicGenerator.GenerateWith(character.Alignment, character.Class, character.Race, character.Ability.Stats, character.Ability.Feats, character.Equipment);

            return character;
        }

        private int ComputeArmorCheckPenalty(Item item, string skillName)
        {
            if (item == null || !(item is Armor))
                return 0;

            var armor = item as Armor;

            if (skillName == SkillConstants.Swim && armor.Name == ArmorConstants.PlateArmorOfTheDeep)
                return 0;

            if (skillName == SkillConstants.Swim)
                return armor.TotalArmorCheckPenalty * 2;

            return armor.TotalArmorCheckPenalty;
        }

        private CharacterClass EditCharacterClass(Alignment alignment, CharacterClass characterClass, Race race, ILevelRandomizer levelRandomizer)
        {
            characterClass.LevelAdjustment += adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments, race.BaseRace);
            characterClass.LevelAdjustment += adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments, race.Metarace);

            if (!(levelRandomizer is ISetLevelRandomizer) || (levelRandomizer as ISetLevelRandomizer).AllowAdjustments)
            {
                characterClass.Level -= characterClass.LevelAdjustment;
            }

            characterClass.SpecialistFields = characterClassGenerator.RegenerateSpecialistFields(alignment, characterClass, race);

            return characterClass;
        }

        private void VerifyRandomizers(IAlignmentRandomizer alignmentRandomizer,
            IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer,
            RaceRandomizer baseRaceRandomizer,
            RaceRandomizer metaraceRandomizer)
        {
            var verified = randomizerVerifier.VerifyCompatibility(alignmentRandomizer, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer);

            if (verified == false)
                throw new IncompatibleRandomizersException();
        }

        private Alignment GenerateAlignment(IAlignmentRandomizer alignmentRandomizer,
            IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer,
            RaceRandomizer baseRaceRandomizer,
            RaceRandomizer metaraceRandomizer)
        {
            var alignment = generator.Generate(
                () => alignmentGenerator.GenerateWith(alignmentRandomizer),
                "alignment",
                a => randomizerVerifier.VerifyAlignmentCompatibility(a, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer),
                () => DefaultIsIncompatible<Alignment>(),
                "Incompatible alignment");

            return alignment;
        }

        private T DefaultIsIncompatible<T>()
        {
            throw new IncompatibleRandomizersException();
        }

        private CharacterClass GenerateCharacterClass(IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer,
            Alignment alignment,
            RaceRandomizer baseRaceRandomizer,
            RaceRandomizer metaraceRandomizer)
        {
            var characterClass = generator.Generate(
                () => characterClassGenerator.GenerateWith(alignment, levelRandomizer, classNameRandomizer),
                "character class",
                c => randomizerVerifier.VerifyCharacterClassCompatibility(alignment, c, levelRandomizer, baseRaceRandomizer, metaraceRandomizer),
                () => DefaultIsIncompatible<CharacterClass>(),
                "Incompatible character class");

            return characterClass;
        }

        private Race GenerateRace(RaceRandomizer baseRaceRandomizer,
            RaceRandomizer metaraceRandomizer,
            Alignment alignment,
            CharacterClass characterClass,
            ILevelRandomizer levelRandomizer)
        {
            var race = generator.Generate(
                () => raceGenerator.GenerateWith(alignment, characterClass, baseRaceRandomizer, metaraceRandomizer),
                "race",
                r => randomizerVerifier.VerifyRaceCompatibility(r, characterClass, levelRandomizer),
                () => DefaultIsIncompatible<Race>(),
                "Incompatible race");

            return race;
        }
    }
}
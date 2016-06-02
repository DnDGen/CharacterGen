using CharacterGen.Abilities.Skills;
using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators.Abilities;
using CharacterGen.Domain.Generators.Combats;
using CharacterGen.Domain.Generators.Items;
using CharacterGen.Domain.Generators.Magics;
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
using System;
using System.Linq;
using TreasureGen.Items;

namespace CharacterGen.Domain.Generators
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

        public CharacterGenerator(IAlignmentGenerator alignmentGenerator, ICharacterClassGenerator characterClassGenerator, IRaceGenerator raceGenerator,
            IAdjustmentsSelector adjustmentsSelector, IRandomizerVerifier randomizerVerifier, IPercentileSelector percentileSelector,
            IAbilitiesGenerator abilitiesGenerator, ICombatGenerator combatGenerator, IEquipmentGenerator equipmentGenerator,
            IMagicGenerator magicGenerator, Generator generator, ICollectionsSelector collectionsSelector)
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

        public Character GenerateWith(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer,
            IStatsRandomizer statsRandomizer)
        {
            VerifyRandomizers(alignmentRandomizer, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer);

            var character = GenerateCharacter(alignmentRandomizer, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer,
                statsRandomizer);

            return character;
        }

        private Character GenerateCharacter(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer,
            IStatsRandomizer statsRandomizer)
        {
            var character = new Character();

            character.Alignment = GenerateAlignment(alignmentRandomizer, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer);
            character.Class = GenerateCharacterClass(classNameRandomizer, levelRandomizer, character.Alignment, baseRaceRandomizer, metaraceRandomizer);
            character.Race = GenerateRace(baseRaceRandomizer, metaraceRandomizer, character.Alignment, character.Class, levelRandomizer);

            if ((levelRandomizer is ISetLevelRandomizer) == false || (levelRandomizer as ISetLevelRandomizer).AllowAdjustments)
            {
                var levelAdjustments = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments);
                character.Class.Level += levelAdjustments[character.Race.BaseRace];
                character.Class.Level += levelAdjustments[character.Race.Metarace];
            }

            character.Class.SpecialistFields = characterClassGenerator.RegenerateSpecialistFields(character.Alignment, character.Class, character.Race);

            var baseAttack = combatGenerator.GenerateBaseAttackWith(character.Class, character.Race);

            character.Ability = abilitiesGenerator.GenerateWith(character.Class, character.Race, statsRandomizer, baseAttack);
            character.Equipment = equipmentGenerator.GenerateWith(character.Ability.Feats, character.Class, character.Race);

            var armorCheckPenaltySkills = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.ArmorCheckPenalty);
            var armorCheckPenalties = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.ArmorCheckPenalties);

            if (character.Equipment.Armor != null)
            {
                foreach (var skill in armorCheckPenaltySkills)
                {
                    if (character.Ability.Skills.ContainsKey(skill) == false)
                        continue;

                    if (skill == SkillConstants.Swim && character.Equipment.Armor.Name == ArmorConstants.PlateArmorOfTheDeep)
                        continue;

                    character.Ability.Skills[skill].ArmorCheckPenalty += armorCheckPenalties[character.Equipment.Armor.Name];

                    var specialMaterials = character.Equipment.Armor.Traits.Intersect(armorCheckPenalties.Keys);
                    foreach (var material in specialMaterials)
                        character.Ability.Skills[skill].ArmorCheckPenalty += armorCheckPenalties[material];

                    character.Ability.Skills[skill].ArmorCheckPenalty = Math.Min(0, character.Ability.Skills[skill].ArmorCheckPenalty);
                }
            }

            if (character.Equipment.OffHand != null && character.Equipment.OffHand.Attributes.Contains(AttributeConstants.Shield))
            {
                foreach (var skill in armorCheckPenaltySkills)
                {
                    if (character.Ability.Skills.ContainsKey(skill) == false)
                        continue;

                    character.Ability.Skills[skill].ArmorCheckPenalty += armorCheckPenalties[character.Equipment.OffHand.Name];

                    var specialMaterials = character.Equipment.OffHand.Traits.Intersect(armorCheckPenalties.Keys);
                    foreach (var material in specialMaterials)
                        character.Ability.Skills[skill].ArmorCheckPenalty += armorCheckPenalties[material];

                    character.Ability.Skills[skill].ArmorCheckPenalty = Math.Min(0, character.Ability.Skills[skill].ArmorCheckPenalty);
                }
            }

            if (character.Ability.Skills.ContainsKey(SkillConstants.Swim))
                character.Ability.Skills[SkillConstants.Swim].ArmorCheckPenalty *= 2;

            character.Combat = combatGenerator.GenerateWith(baseAttack, character.Class, character.Race, character.Ability.Feats, character.Ability.Stats, character.Equipment);
            character.InterestingTrait = percentileSelector.SelectFrom(TableNameConstants.Set.Percentile.Traits);
            character.Magic = magicGenerator.GenerateWith(character.Alignment, character.Class, character.Race, character.Ability.Stats, character.Ability.Feats, character.Equipment);

            return character;
        }

        private void VerifyRandomizers(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer)
        {
            var verified = randomizerVerifier.VerifyCompatibility(alignmentRandomizer, classNameRandomizer, levelRandomizer,
                baseRaceRandomizer, metaraceRandomizer);

            if (verified == false)
                throw new IncompatibleRandomizersException();
        }

        private Alignment GenerateAlignment(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer)
        {
            var alignment = generator.Generate(() => alignmentGenerator.GenerateWith(alignmentRandomizer),
                a => randomizerVerifier.VerifyAlignmentCompatibility(a, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer));

            if (alignment == null)
                throw new IncompatibleRandomizersException();

            return alignment;
        }

        private CharacterClass GenerateCharacterClass(IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer,
            Alignment alignment, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer)
        {
            var characterClass = generator.Generate(() => characterClassGenerator.GenerateWith(alignment, levelRandomizer, classNameRandomizer),
                c => randomizerVerifier.VerifyCharacterClassCompatibility(alignment, c, levelRandomizer, baseRaceRandomizer, metaraceRandomizer));

            if (characterClass == null)
                throw new IncompatibleRandomizersException();

            return characterClass;
        }

        private Race GenerateRace(RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer, Alignment alignment, CharacterClass characterClass, ILevelRandomizer levelRandomizer)
        {
            var race = generator.Generate(() => raceGenerator.GenerateWith(alignment, characterClass, baseRaceRandomizer, metaraceRandomizer),
                r => randomizerVerifier.VerifyRaceCompatibility(r, characterClass, levelRandomizer));

            if (race == null)
                throw new IncompatibleRandomizersException();

            return race;
        }
    }
}
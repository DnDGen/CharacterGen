﻿using CharacterGen.Common;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Magics;
using CharacterGen.Generators.Randomizers.Alignments;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using CharacterGen.Generators.Randomizers.Races;
using CharacterGen.Generators.Randomizers.Stats;
using CharacterGen.Generators.Verifiers;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Common.Items;

namespace CharacterGen.Generators.Domain
{
    public class CharacterGenerator : ICharacterGenerator
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

            var levelAdjustments = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments);
            character.Race = GenerateRace(baseRaceRandomizer, metaraceRandomizer, levelAdjustments, character.Alignment, character.Class);

            if ((levelRandomizer is ISetLevelRandomizer) == false || (levelRandomizer as ISetLevelRandomizer).AllowAdjustments)
            {
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
            character.Magic = magicGenerator.GenerateWith(character.Alignment, character.Class, character.Race, character.Ability.Stats, character.Ability.Feats);

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

            return alignment;
        }

        private CharacterClass GenerateCharacterClass(IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer,
            Alignment alignment, RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer)
        {
            var characterClass = generator.Generate(() => characterClassGenerator.GenerateWith(alignment, levelRandomizer, classNameRandomizer),
                c => randomizerVerifier.VerifyCharacterClassCompatibility(alignment, c, baseRaceRandomizer, metaraceRandomizer));

            return characterClass;
        }

        private Race GenerateRace(RaceRandomizer baseRaceRandomizer, RaceRandomizer metaraceRandomizer, Dictionary<String, Int32> levelAdjustments,
            Alignment alignment, CharacterClass characterClass)
        {
            var race = generator.Generate(() => raceGenerator.GenerateWith(alignment, characterClass, baseRaceRandomizer, metaraceRandomizer),
                r => characterClass.Level + levelAdjustments[r.BaseRace] + levelAdjustments[r.Metarace] > 0);

            if (race == null)
                throw new IncompatibleRandomizersException();

            return race;
        }
    }
}
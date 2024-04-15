using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Characters;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Generators.Abilities;
using DnDGen.CharacterGen.Generators.Alignments;
using DnDGen.CharacterGen.Generators.Classes;
using DnDGen.CharacterGen.Generators.Combats;
using DnDGen.CharacterGen.Generators.Feats;
using DnDGen.CharacterGen.Generators.Items;
using DnDGen.CharacterGen.Generators.Languages;
using DnDGen.CharacterGen.Generators.Magics;
using DnDGen.CharacterGen.Generators.Races;
using DnDGen.CharacterGen.Generators.Skills;
using DnDGen.CharacterGen.Items;
using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Randomizers.Abilities;
using DnDGen.CharacterGen.Randomizers.Alignments;
using DnDGen.CharacterGen.Randomizers.CharacterClasses;
using DnDGen.CharacterGen.Randomizers.Races;
using DnDGen.CharacterGen.Selectors.Collections;
using DnDGen.CharacterGen.Skills;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Verifiers;
using DnDGen.CharacterGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Percentiles;
using DnDGen.TreasureGen.Items;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CharacterGen.Generators.Characters
{
    internal class CharacterGenerator : ICharacterGenerator
    {
        private readonly IAlignmentGenerator alignmentGenerator;
        private readonly ICharacterClassGenerator characterClassGenerator;
        private readonly IRaceGenerator raceGenerator;
        private readonly IAdjustmentsSelector adjustmentsSelector;
        private readonly IRandomizerVerifier randomizerVerifier;
        private readonly IPercentileSelector percentileSelector;
        private readonly ICombatGenerator combatGenerator;
        private readonly IEquipmentGenerator equipmentGenerator;
        private readonly IMagicGenerator magicGenerator;
        private readonly ICollectionSelector collectionsSelector;
        private readonly IAbilitiesGenerator abilitiesGenerator;
        private readonly ILanguageGenerator languageGenerator;
        private readonly ISkillsGenerator skillsGenerator;
        private readonly IFeatsGenerator featsGenerator;

        public CharacterGenerator(IAlignmentGenerator alignmentGenerator,
            ICharacterClassGenerator characterClassGenerator,
            IRaceGenerator raceGenerator,
            IAdjustmentsSelector adjustmentsSelector,
            IRandomizerVerifier randomizerVerifier,
            IPercentileSelector percentileSelector,
            ICombatGenerator combatGenerator,
            IEquipmentGenerator equipmentGenerator,
            IMagicGenerator magicGenerator,
            ICollectionSelector collectionsSelector,
            IAbilitiesGenerator abilitiesGenerator,
            ILanguageGenerator languageGenerator,
            ISkillsGenerator skillsGenerator,
            IFeatsGenerator featsGenerator)
        {
            this.alignmentGenerator = alignmentGenerator;
            this.characterClassGenerator = characterClassGenerator;
            this.raceGenerator = raceGenerator;
            this.abilitiesGenerator = abilitiesGenerator;
            this.combatGenerator = combatGenerator;
            this.equipmentGenerator = equipmentGenerator;
            this.languageGenerator = languageGenerator;
            this.skillsGenerator = skillsGenerator;
            this.featsGenerator = featsGenerator;
            this.magicGenerator = magicGenerator;

            this.adjustmentsSelector = adjustmentsSelector;
            this.randomizerVerifier = randomizerVerifier;
            this.percentileSelector = percentileSelector;
            this.collectionsSelector = collectionsSelector;
        }

        public Character GenerateWith(IAlignmentRandomizer alignmentRandomizer,
            IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer,
            RaceRandomizer baseRaceRandomizer,
            RaceRandomizer metaraceRandomizer,
            IAbilitiesRandomizer statsRandomizer)
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
            IAbilitiesRandomizer statsRandomizer)
        {
            var character = new Character();
            var prototype = GeneratePrototypeWith(alignmentRandomizer, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer);

            character.Alignment = alignmentGenerator.GenerateWith(prototype.Alignment);
            character.Class = characterClassGenerator.GenerateWith(character.Alignment, prototype.CharacterClass);
            character.Race = raceGenerator.GenerateWith(character.Alignment, character.Class, prototype.Race);

            character.Class = EditCharacterClass(character.Alignment, character.Class, character.Race);

            character.Abilities = abilitiesGenerator.GenerateWith(statsRandomizer, character.Class, character.Race);
            var baseAttack = combatGenerator.GenerateBaseAttackWith(character.Class, character.Race, character.Abilities);

            character.Skills = skillsGenerator.GenerateWith(character.Class, character.Race, character.Abilities);

            character.Feats = featsGenerator.GenerateWith(character.Class, character.Race, character.Abilities, character.Skills, baseAttack);
            character.Skills = UpdateSkillsFromFeats(character.Skills, character.Feats.All);

            character.Equipment = equipmentGenerator.GenerateWith(character.Feats.All, character.Class, character.Race);
            character.Skills = UpdateSkillsFromEquipment(character.Skills, character.Equipment);

            character.Languages = languageGenerator.GenerateWith(character.Race, character.Class, character.Abilities, character.Skills);

            character.Combat = combatGenerator.GenerateWith(baseAttack, character.Class, character.Race, character.Feats.All, character.Abilities, character.Equipment);
            character.InterestingTrait = percentileSelector.SelectFrom(TableNameConstants.Set.Percentile.Traits);
            character.Magic = magicGenerator.GenerateWith(character.Alignment, character.Class, character.Race, character.Abilities, character.Feats.All, character.Equipment);

            return character;
        }

        private IEnumerable<Skill> UpdateSkillsFromFeats(IEnumerable<Skill> skills, IEnumerable<Feat> feats)
        {
            var allFeatGrantingSkillBonuses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, FeatConstants.SkillBonus);
            var featGrantingSkillBonuses = feats.Where(f => allFeatGrantingSkillBonuses.Contains(f.Name));
            var allSkillFocusNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, GroupConstants.Skills);

            foreach (var feat in featGrantingSkillBonuses)
            {
                if (feat.Foci.Any())
                {
                    foreach (var focus in feat.Foci)
                    {
                        if (!allSkillFocusNames.Any(s => focus.StartsWith(s)))
                            continue;

                        var skillName = allSkillFocusNames.First(s => focus.StartsWith(s));
                        var skill = skills.FirstOrDefault(s => s.IsEqualTo(skillName));

                        if (skill == null)
                            continue;

                        var circumstantial = !allSkillFocusNames.Contains(focus);
                        skill.CircumstantialBonus |= circumstantial;

                        if (!circumstantial)
                            skill.Bonus += feat.Power;
                    }
                }
                else
                {
                    var skillsToReceiveBonus = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, feat.Name);

                    foreach (var skillName in skillsToReceiveBonus)
                    {
                        var skill = skills.FirstOrDefault(s => s.IsEqualTo(skillName));

                        if (skill != null)
                            skill.Bonus += feat.Power;
                    }
                }
            }

            return skills;
        }

        private IEnumerable<Skill> UpdateSkillsFromEquipment(IEnumerable<Skill> skills, Equipment equipment)
        {
            var armorCheckPenaltySkills = skills.Where(s => s.HasArmorCheckPenalty);

            foreach (var skill in armorCheckPenaltySkills)
            {
                skill.ArmorCheckPenalty += ComputeArmorCheckPenalty(equipment.Armor, skill.Name);

                if (equipment.OffHand != null && equipment.OffHand.Attributes.Contains(AttributeConstants.Shield))
                {
                    skill.ArmorCheckPenalty += ComputeArmorCheckPenalty(equipment.OffHand, skill.Name);
                }
            }

            return skills;
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

        private CharacterClass EditCharacterClass(Alignment alignment, CharacterClass characterClass, Race race)
        {
            characterClass.LevelAdjustment += adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments, race.BaseRace);
            characterClass.LevelAdjustment += adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments, race.Metarace);
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

            if (!verified)
                throw new IncompatibleRandomizersException();
        }

        public CharacterPrototype GeneratePrototypeWith(
            IAlignmentRandomizer alignmentRandomizer,
            IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer,
            RaceRandomizer baseRaceRandomizer,
            RaceRandomizer metaraceRandomizer)
        {
            var prototype = new CharacterPrototype();

            var alignments = alignmentGenerator.GeneratePrototypes(alignmentRandomizer);
            var validAlignments = randomizerVerifier.FilterAlignments(alignments, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer);

            if (!validAlignments.Any())
                throw new IncompatibleRandomizersException();

            prototype.Alignment = alignmentGenerator.GeneratePrototype(alignmentRandomizer);

            if (!validAlignments.Contains(prototype.Alignment))
                prototype.Alignment = collectionsSelector.SelectRandomFrom(validAlignments);

            var characterClasses = characterClassGenerator.GeneratePrototypes(prototype.Alignment, classNameRandomizer, levelRandomizer);
            var validCharacterClasses = randomizerVerifier.FilterCharacterClasses(characterClasses, prototype.Alignment, baseRaceRandomizer, metaraceRandomizer);

            if (!validCharacterClasses.Any())
                throw new IncompatibleRandomizersException();

            prototype.CharacterClass = characterClassGenerator.GeneratePrototype(prototype.Alignment, classNameRandomizer, levelRandomizer);

            if (!validCharacterClasses.Contains(prototype.CharacterClass))
                prototype.CharacterClass = collectionsSelector.SelectRandomFrom(validCharacterClasses);

            var races = raceGenerator.GeneratePrototypes(prototype.Alignment, prototype.CharacterClass, baseRaceRandomizer, metaraceRandomizer);
            var validRaces = randomizerVerifier.FilterRaces(races, prototype.Alignment, prototype.CharacterClass);

            if (!validRaces.Any())
                throw new IncompatibleRandomizersException();

            prototype.Race = raceGenerator.GeneratePrototype(prototype.Alignment, prototype.CharacterClass, baseRaceRandomizer, metaraceRandomizer);

            if (!validRaces.Contains(prototype.Race))
            {
                //TODO: Filter out metaraces, only allow "None"
                //If there are no races with No Metarace, then just let it be
                //Otherwise, metaraces appear way too often, for how rare they are supposed to be
                if (validRaces.Any(r => r.Metarace == RaceConstants.Metaraces.None))
                    validRaces = validRaces.Where(r => r.Metarace == RaceConstants.Metaraces.None);

                prototype.Race = collectionsSelector.SelectRandomFrom(validRaces);
            }

            return prototype;
        }
    }
}
using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Characters;
using CharacterGen.Domain.Generators.Abilities;
using CharacterGen.Domain.Generators.Alignments;
using CharacterGen.Domain.Generators.Classes;
using CharacterGen.Domain.Generators.Combats;
using CharacterGen.Domain.Generators.Feats;
using CharacterGen.Domain.Generators.Items;
using CharacterGen.Domain.Generators.Languages;
using CharacterGen.Domain.Generators.Magics;
using CharacterGen.Domain.Generators.Races;
using CharacterGen.Domain.Generators.Skills;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Feats;
using CharacterGen.Items;
using CharacterGen.Races;
using CharacterGen.Randomizers.Abilities;
using CharacterGen.Randomizers.Alignments;
using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Randomizers.Races;
using CharacterGen.Skills;
using CharacterGen.Verifiers;
using CharacterGen.Verifiers.Exceptions;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Items;

namespace CharacterGen.Domain.Generators.Characters
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
        private readonly Generator generator;
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
            Generator generator,
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
            this.generator = generator;
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

            character.Class = EditCharacterClass(character.Alignment, character.Class, character.Race, levelRandomizer);

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

        private CharacterClass EditCharacterClass(Alignment alignment, CharacterClass characterClass, Race race, ILevelRandomizer levelRandomizer)
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

        public CharacterPrototype GeneratePrototypeWith(IAlignmentRandomizer alignmentRandomizer,
            IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer,
            RaceRandomizer baseRaceRandomizer,
            RaceRandomizer metaraceRandomizer)
        {
            var prototype = new CharacterPrototype();

            prototype.Alignment = generator.Generate(
                () => alignmentGenerator.GeneratePrototype(alignmentRandomizer),
                a => randomizerVerifier.VerifyAlignmentCompatibility(a, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer),
                () => DefaultIsIncompatible<Alignment>(),
                a => $"{a.Full} is not compatible with the randomizers",
                "Incompatible alignment");

            prototype.CharacterClass = generator.Generate(
                () => characterClassGenerator.GeneratePrototype(prototype.Alignment, classNameRandomizer, levelRandomizer),
                c => randomizerVerifier.VerifyCharacterClassCompatibility(prototype.Alignment, c, baseRaceRandomizer, metaraceRandomizer),
                () => DefaultIsIncompatible<CharacterClassPrototype>(),
                c => $"{c.Summary} is not compatible with {prototype.Alignment} and the randomizers",
                "Incompatible character class");

            prototype.Race = generator.Generate(
                () => raceGenerator.GeneratePrototype(prototype.Alignment, prototype.CharacterClass, baseRaceRandomizer, metaraceRandomizer),
                r => randomizerVerifier.VerifyRaceCompatibility(prototype.Alignment, prototype.CharacterClass, r),
                () => DefaultIsIncompatible<RacePrototype>(),
                r => $"{r.Summary} is not compatible with {prototype.Alignment}, {prototype.CharacterClass.Summary}, and the randomizers",
                "Incompatible race");

            return prototype;
        }

        private T DefaultIsIncompatible<T>()
        {
            throw new IncompatibleRandomizersException();
        }
    }
}
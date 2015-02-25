using System;
using System.Collections.Generic;
using NPCGen.Common;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Combats;
using NPCGen.Generators.Interfaces.Items;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Generators.Interfaces.Verifiers;
using NPCGen.Generators.Interfaces.Verifiers.Exceptions;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Generators
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

        public CharacterGenerator(IAlignmentGenerator alignmentGenerator, ICharacterClassGenerator characterClassGenerator, IRaceGenerator raceGenerator,
            IAdjustmentsSelector adjustmentsSelector, IRandomizerVerifier randomizerVerifier, IPercentileSelector percentileSelector,
            IAbilitiesGenerator abilitiesGenerator, ICombatGenerator combatGenerator, IEquipmentGenerator equipmentGenerator)
        {
            this.alignmentGenerator = alignmentGenerator;
            this.characterClassGenerator = characterClassGenerator;
            this.raceGenerator = raceGenerator;
            this.abilitiesGenerator = abilitiesGenerator;
            this.combatGenerator = combatGenerator;
            this.equipmentGenerator = equipmentGenerator;

            this.adjustmentsSelector = adjustmentsSelector;
            this.randomizerVerifier = randomizerVerifier;
            this.percentileSelector = percentileSelector;
        }

        public Character GenerateWith(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer,
            IStatsRandomizer statsRandomizer)
        {
            VerifyRandomizers(alignmentRandomizer, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer);

            var character = new Character();

            character.Alignment = GenerateAlignment(alignmentRandomizer, classNameRandomizer, levelRandomizer, baseRaceRandomizer, metaraceRandomizer);
            character.Class = GenerateCharacterClass(classNameRandomizer, levelRandomizer, character.Alignment, baseRaceRandomizer, metaraceRandomizer);

            var levelAdjustments = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments);
            character.Race = GenerateRace(baseRaceRandomizer, metaraceRandomizer, levelAdjustments, character.Alignment, character.Class);

            character.Class.Level -= levelAdjustments[character.Race.BaseRace.Id];
            character.Class.Level -= levelAdjustments[character.Race.Metarace.Id];

            var baseAttack = combatGenerator.GenerateBaseAttackWith(character.Class, character.Race);

            character.Ability = abilitiesGenerator.GenerateWith(character.Class, character.Race, statsRandomizer, baseAttack);
            character.Equipment = equipmentGenerator.GenerateWith(character.Ability.Feats, character.Class);

            var armorCheckPenalties = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.ArmorCheckPenalties);

            foreach (var skill in character.Ability.Skills)
            {
                if (skill.Value.ArmorCheckPenalty)
                    skill.Value.Bonus -= armorCheckPenalties[character.Equipment.Armor.Name];

                if (skill.Key == SkillConstants.Swim)
                    skill.Value.Bonus -= armorCheckPenalties[character.Equipment.Armor.Name];
            }

            character.Combat = combatGenerator.GenerateWith(baseAttack, character.Class, character.Race, character.Ability.Feats, character.Ability.Stats, character.Equipment);
            character.InterestingTrait = percentileSelector.SelectFrom(TableNameConstants.Set.Percentile.Traits);

            return character;
        }

        private void VerifyRandomizers(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            var verified = randomizerVerifier.VerifyCompatibility(alignmentRandomizer, classNameRandomizer, levelRandomizer,
                baseRaceRandomizer, metaraceRandomizer);

            if (!verified)
                throw new IncompatibleRandomizersException();
        }

        private Alignment GenerateAlignment(IAlignmentRandomizer alignmentRandomizer, IClassNameRandomizer classNameRandomizer,
            ILevelRandomizer levelRandomizer, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            Alignment alignment;

            do alignment = alignmentGenerator.GenerateWith(alignmentRandomizer);
            while (!randomizerVerifier.VerifyAlignmentCompatibility(alignment, classNameRandomizer, levelRandomizer, baseRaceRandomizer,
                metaraceRandomizer));

            return alignment;
        }

        private CharacterClass GenerateCharacterClass(IClassNameRandomizer classNameRandomizer, ILevelRandomizer levelRandomizer,
            Alignment alignment, IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer)
        {
            CharacterClass characterClass;

            do characterClass = characterClassGenerator.GenerateWith(alignment, levelRandomizer, classNameRandomizer);
            while (!randomizerVerifier.VerifyCharacterClassCompatibility(alignment.Goodness, characterClass, baseRaceRandomizer, metaraceRandomizer));

            return characterClass;
        }

        private Race GenerateRace(IBaseRaceRandomizer baseRaceRandomizer, IMetaraceRandomizer metaraceRandomizer, Dictionary<String, Int32> levelAdjustments,
            Alignment alignment, CharacterClass characterClass)
        {
            Race race;

            do race = raceGenerator.GenerateWith(alignment, characterClass, baseRaceRandomizer, metaraceRandomizer);
            while (levelAdjustments[race.BaseRace.Id] + levelAdjustments[race.Metarace.Id] >= characterClass.Level);

            return race;
        }
    }
}
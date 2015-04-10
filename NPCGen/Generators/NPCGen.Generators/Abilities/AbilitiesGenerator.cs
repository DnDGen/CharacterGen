using System;
using System.Linq;
using NPCGen.Common.Abilities;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Generators.Abilities
{
    public class AbilitiesGenerator : IAbilitiesGenerator
    {
        private IStatsGenerator statsGenerator;
        private ILanguageGenerator languageGenerator;
        private ISkillsGenerator skillsGenerator;
        private IFeatsGenerator featsGenerator;
        private IAdjustmentsSelector adjustmentsSelector;

        public AbilitiesGenerator(IStatsGenerator statsGenerator, ILanguageGenerator languageGenerator, ISkillsGenerator skillsGenerator,
            IFeatsGenerator featsGenerator, IAdjustmentsSelector adjustmentsSelector)
        {
            this.statsGenerator = statsGenerator;
            this.languageGenerator = languageGenerator;
            this.skillsGenerator = skillsGenerator;
            this.featsGenerator = featsGenerator;
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public Ability GenerateWith(CharacterClass characterClass, Race race, IStatsRandomizer statsRandomizer, BaseAttack baseAttack)
        {
            var ability = new Ability();

            ability.Stats = statsGenerator.GenerateWith(statsRandomizer, characterClass, race);
            ability.Languages = languageGenerator.GenerateWith(race, characterClass.ClassName, ability.Stats[StatConstants.Intelligence].Bonus);
            ability.Skills = skillsGenerator.GenerateWith(characterClass, race, ability.Stats);
            ability.Feats = featsGenerator.GenerateWith(characterClass, race, ability.Stats, ability.Skills, baseAttack);

            foreach (var feat in ability.Feats)
            {
                var tableName = String.Format(TableNameConstants.Formattable.Adjustments.FEATSkillAdjustments, feat.Name.Id);
                var adjustments = adjustmentsSelector.SelectFrom(tableName);

                foreach (var adjustment in adjustments)
                    ability.Skills[adjustment.Key].Bonus += adjustment.Value;
            }

            var skillFoci = ability.Feats.Where(f => f.Name.Id == FeatConstants.SkillFocusId);
            foreach (var feat in skillFoci)
                ability.Skills[feat.SpecificApplication].Bonus += 3;

            return ability;
        }
    }
}
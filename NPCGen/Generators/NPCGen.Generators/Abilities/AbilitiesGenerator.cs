using System;
using NPCGen.Common.Abilities;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Selectors.Interfaces;

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
            ability.Skills = skillsGenerator.GenerateWith(characterClass, ability.Stats);
            ability.Feats = featsGenerator.GenerateWith(characterClass, race, ability.Stats, ability.Skills);

            foreach (var feat in ability.Feats)
            {
                var tableName = String.Format("{0}SkillAdjustments", feat);
                var adjustments = adjustmentsSelector.SelectFrom(tableName);

                foreach (var adjustment in adjustments)
                    ability.Skills[adjustment.Key].Bonus += adjustment.Value;
            }

            return ability;
        }
    }
}
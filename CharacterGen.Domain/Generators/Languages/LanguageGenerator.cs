using CharacterGen.Abilities;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Races;
using CharacterGen.Skills;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Languages
{
    internal class LanguageGenerator : ILanguageGenerator
    {
        private ILanguageCollectionsSelector languagesSelector;
        private ICollectionsSelector collectionsSelector;

        public LanguageGenerator(ILanguageCollectionsSelector languagesSelector, ICollectionsSelector collectionsSelector)
        {
            this.languagesSelector = languagesSelector;
            this.collectionsSelector = collectionsSelector;
        }

        public IEnumerable<string> GenerateWith(Race race, CharacterClass characterClass, Dictionary<string, Ability> abilities, IEnumerable<Skill> skills)
        {
            var languages = new List<string>();

            var automaticLanguages = languagesSelector.SelectAutomaticLanguagesFor(race, characterClass.Name);
            languages.AddRange(automaticLanguages);

            var bonusLanguages = languagesSelector.SelectBonusLanguagesFor(race.BaseRace, characterClass.Name);
            var remainingBonusLanguages = bonusLanguages.Except(languages).ToList();
            var numberOfBonusLanguages = abilities[AbilityConstants.Intelligence].Bonus;

            if (IsInterpreter(skills))
                numberOfBonusLanguages = Math.Max(1, abilities[AbilityConstants.Intelligence].Bonus + 1);

            if (numberOfBonusLanguages >= remainingBonusLanguages.Count)
            {
                languages.AddRange(remainingBonusLanguages);
                return languages;
            }

            while (numberOfBonusLanguages-- > 0 && remainingBonusLanguages.Any())
            {
                var language = collectionsSelector.SelectRandomFrom(remainingBonusLanguages);
                languages.Add(language);
                remainingBonusLanguages.Remove(language);
            }

            return languages;
        }

        private bool IsInterpreter(IEnumerable<Skill> skills)
        {
            return skills.Any(s => s.Name == SkillConstants.Profession && s.Focus == SkillConstants.Foci.Profession.Interpreter);
        }
    }
}
using CharacterGen.Common.Abilities;
using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Combats;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Abilities.Feats;
using CharacterGen.Generators.Randomizers.Stats;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Abilities
{
    public class AbilitiesGenerator : IAbilitiesGenerator
    {
        private IStatsGenerator statsGenerator;
        private ILanguageGenerator languageGenerator;
        private ISkillsGenerator skillsGenerator;
        private IFeatsGenerator featsGenerator;
        private ICollectionsSelector collectionsSelector;

        public AbilitiesGenerator(IStatsGenerator statsGenerator, ILanguageGenerator languageGenerator, ISkillsGenerator skillsGenerator,
            IFeatsGenerator featsGenerator, ICollectionsSelector collectionsSelector)
        {
            this.statsGenerator = statsGenerator;
            this.languageGenerator = languageGenerator;
            this.skillsGenerator = skillsGenerator;
            this.featsGenerator = featsGenerator;
            this.collectionsSelector = collectionsSelector;
        }

        public Ability GenerateWith(CharacterClass characterClass, Race race, IStatsRandomizer statsRandomizer, BaseAttack baseAttack)
        {
            var ability = new Ability();

            ability.Stats = statsGenerator.GenerateWith(statsRandomizer, characterClass, race);
            ability.Languages = languageGenerator.GenerateWith(race, characterClass.ClassName, ability.Stats[StatConstants.Intelligence].Bonus);
            ability.Skills = skillsGenerator.GenerateWith(characterClass, race, ability.Stats);
            ability.Feats = featsGenerator.GenerateWith(characterClass, race, ability.Stats, ability.Skills, baseAttack);

            var allFeatGrantingSkillBonuses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, FeatConstants.SkillBonus);
            var featIds = ability.Feats.Select(f => f.Name);
            var featIdsGrantingSkillBonuses = allFeatGrantingSkillBonuses.Intersect(featIds);
            var featGrantingSkillBonuses = ability.Feats.Where(f => featIdsGrantingSkillBonuses.Contains(f.Name));
            var allSkills = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.Skills);

            foreach (var feat in featGrantingSkillBonuses)
            {
                var skillsToReceiveBonus = GetSkillsToReceiveBonuses(feat);
                var skillHasCircumstantialBonus = skillsToReceiveBonus.Intersect(allSkills).Any() == false;

                if (skillHasCircumstantialBonus == false)
                {
                    foreach (var skill in skillsToReceiveBonus)
                        if (ability.Skills.ContainsKey(skill))
                            ability.Skills[skill].Bonus += feat.Strength;
                }
                else
                {
                    var skill = allSkills.First(s => feat.Focus.StartsWith(s));

                    if (ability.Skills.ContainsKey(skill))
                        ability.Skills[skill].CircumstantialBonus = true;
                }
            }

            return ability;
        }

        private IEnumerable<String> GetSkillsToReceiveBonuses(Feat feat)
        {
            if (feat.Focus == String.Empty)
                return collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, feat.Name);

            return new[] { feat.Focus };
        }
    }
}
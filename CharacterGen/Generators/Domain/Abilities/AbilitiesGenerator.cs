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
            var featNames = ability.Feats.Select(f => f.Name);
            var featNamesGrantingSkillBonuses = allFeatGrantingSkillBonuses.Intersect(featNames);
            var featGrantingSkillBonuses = ability.Feats.Where(f => featNamesGrantingSkillBonuses.Contains(f.Name));
            var allSkills = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.Skills);

            foreach (var feat in featGrantingSkillBonuses)
            {
                if (feat.Foci.Any())
                {
                    foreach (var focus in feat.Foci)
                    {
                        if (allSkills.Any(s => focus.StartsWith(s)) == false)
                            continue;

                        var skill = allSkills.First(s => focus.StartsWith(s));

                        if (ability.Skills.ContainsKey(skill) == false)
                            continue;

                        var circumstantial = allSkills.Contains(focus) == false;
                        ability.Skills[skill].CircumstantialBonus |= circumstantial;

                        if (circumstantial == false)
                            ability.Skills[skill].Bonus += feat.Power;
                    }
                }
                else
                {
                    var skillsToReceiveBonus = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, feat.Name);

                    foreach (var skill in skillsToReceiveBonus)
                        if (ability.Skills.ContainsKey(skill))
                            ability.Skills[skill].Bonus += feat.Power;
                }
            }

            return ability;
        }
    }
}
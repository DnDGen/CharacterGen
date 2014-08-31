using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Abilities
{
    public class FeatsGenerator : IFeatsGenerator
    {
        private ICollectionsSelector collectionsSelector;
        private IAdjustmentsSelector adjustmentsSelector;
        private IFeatsSelector featsSelector;
        private IDice dice;

        public FeatsGenerator(ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector,
            IFeatsSelector featsSelector, IDice dice)
        {
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.featsSelector = featsSelector;
            this.dice = dice;
        }

        public IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats,
            Dictionary<String, Skill> skills, BaseAttack baseAttack)
        {
            var baseRacialFeats = collectionsSelector.SelectFrom("RacialFeats", race.BaseRace).Select(f => new Feat { Name = f });
            var metaracialFeats = collectionsSelector.SelectFrom("RacialFeats", race.Metarace).Select(f => new Feat { Name = f });
            var classFeats = GetClassFeats(characterClass);
            var skillSynergyFeats = GetSkillSynergyFeats(skills);
            var additionalFeats = GetAdditionalFeats(characterClass, race, stats, skills, baseAttack);

            var feats = new List<Feat>();
            feats.AddRange(baseRacialFeats);
            feats.AddRange(metaracialFeats);
            feats.AddRange(classFeats);
            feats.AddRange(skillSynergyFeats);

            if (characterClass.ClassName == CharacterClassConstants.Fighter)
            {
                var fighterFeats = GetFighterFeats(characterClass, race, stats, skills, baseAttack);
                feats.AddRange(fighterFeats);
            }

            return feats;
        }

        private IEnumerable<Feat> GetClassFeats(CharacterClass characterClass)
        {
            var allClassFeats = collectionsSelector.SelectFrom("ClassFeats", characterClass.ClassName);
            var tableName = String.Format("{0}FeatLevelRequirements", characterClass.ClassName);
            var levelRequirements = adjustmentsSelector.SelectFrom(tableName);

            return allClassFeats.Where(f => levelRequirements[f] <= characterClass.Level).Select(f => new Feat { Name = f });
        }

        private IEnumerable<Feat> GetSkillSynergyFeats(Dictionary<String, Skill> skills)
        {
            var synergyFeats = new List<String>();
            var synergyQualifyingSkills = skills.Where(kvp => kvp.Value.EffectiveRanks >= 5).Select(kvp => kvp.Key);

            foreach (var skill in synergyQualifyingSkills)
            {
                var synergy = collectionsSelector.SelectFrom("SkillSynergyFeats", skill);
                synergyFeats.AddRange(synergy);
            }

            return synergyFeats.Select(f => new Feat { Name = f });
        }

        private IEnumerable<Feat> GetAdditionalFeats(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats, Dictionary<String, Skill> skills, BaseAttack baseAttack)
        {
            var additionalFeats = featsSelector.SelectAll();
            var numberOfAdditionalFeats = characterClass.Level / 3 + 1;
            var feats = new List<String>();

            if (race.BaseRace == RaceConstants.BaseRaces.Human)
                numberOfAdditionalFeats++;

            while (numberOfAdditionalFeats-- > 0)
            {
                var availableFeats = additionalFeats.Where(f => f.RequirementsMet(feats, baseAttack.Bonus, stats, skills, characterClass.ClassName))
                                                   .Select(f => f.FeatName)
                                                   .Except(feats);

                var index = dice.Roll().d(availableFeats.Count());
                var feat = availableFeats.ElementAt(index);
                feats.Add(feat);
            }

            return feats.Select(f => new Feat { Name = f });
        }

        private IEnumerable<Feat> GetFighterFeats(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats, Dictionary<String, Skill> skills, BaseAttack baseAttack)
        {
            var additionalFeats = featsSelector.SelectAll();
            var numberOfFighterFeats = characterClass.Level / 2 + 1;
            var feats = new List<String>();

            while (numberOfFighterFeats-- > 0)
            {
                var availableFeats = additionalFeats.Where(f => f.IsFighterFeat)
                                                   .Where(f => f.RequirementsMet(feats, baseAttack.Bonus, stats, skills, characterClass.ClassName))
                                                   .Select(f => f.FeatName)
                                                   .Except(feats);

                var index = dice.Roll().d(availableFeats.Count());
                var feat = availableFeats.ElementAt(index);
                feats.Add(feat);
            }

            return feats.Select(f => new Feat { Name = f });
        }
    }
}
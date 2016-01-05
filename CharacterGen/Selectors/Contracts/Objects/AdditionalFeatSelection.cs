using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Tables;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Common.Items;

namespace CharacterGen.Selectors.Objects
{
    public class AdditionalFeatSelection
    {
        public string Feat { get; set; }
        public Frequency Frequency { get; set; }
        public int Power { get; set; }
        public IEnumerable<RequiredFeat> RequiredFeats { get; set; }
        public int RequiredBaseAttack { get; set; }
        public Dictionary<string, int> RequiredStats { get; set; }
        public Dictionary<string, int> RequiredSkillRanks { get; set; }
        public Dictionary<string, int> RequiredCharacterClasses { get; set; }
        public string FocusType { get; set; }

        public AdditionalFeatSelection()
        {
            Feat = string.Empty;
            RequiredFeats = Enumerable.Empty<RequiredFeat>();
            RequiredStats = new Dictionary<string, int>();
            RequiredSkillRanks = new Dictionary<string, int>();
            RequiredCharacterClasses = new Dictionary<string, int>();
            FocusType = string.Empty;
            Frequency = new Frequency();
        }

        public bool ImmutableRequirementsMet(int baseAttack, Dictionary<string, Stat> stats, Dictionary<string, Skill> skills, CharacterClass characterClass)
        {
            foreach (var stat in RequiredStats)
                if (stats[stat.Key].Value < stat.Value)
                    return false;

            if (RequiredCharacterClasses.Any() && (RequiredCharacterClasses.ContainsKey(characterClass.ClassName) == false || RequiredCharacterClasses[characterClass.ClassName] > characterClass.Level))
                return false;

            if (baseAttack < RequiredBaseAttack)
                return false;

            if (RequiredSkillRanks.Any() == false)
                return true;

            var requiredSkills = skills.Keys.Intersect(RequiredSkillRanks.Keys);
            return requiredSkills.Any(s => skills[s].EffectiveRanks >= RequiredSkillRanks[s]);
        }

        public bool MutableRequirementsMet(IEnumerable<Feat> feats)
        {
            var proficiencyRequirement = RequiredFeats.FirstOrDefault(f => f.Feat == ItemTypeConstants.Weapon + GroupConstants.Proficiency);
            var requirementsWithoutProficiency = RequiredFeats.Except(new[] { proficiencyRequirement });

            return requirementsWithoutProficiency.All(f => f.RequirementMet(feats));
        }
    }
}
using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Items;

namespace CharacterGen.Domain.Selectors.Selections
{
    internal class AdditionalFeatSelection
    {
        public string Feat { get; set; }
        public Frequency Frequency { get; set; }
        public int Power { get; set; }
        public IEnumerable<RequiredFeatSelection> RequiredFeats { get; set; }
        public int RequiredBaseAttack { get; set; }
        public Dictionary<string, int> RequiredStats { get; set; }
        public IEnumerable<RequiredSkillSelection> RequiredSkills { get; set; }
        public Dictionary<string, int> RequiredCharacterClasses { get; set; }
        public string FocusType { get; set; }

        public AdditionalFeatSelection()
        {
            Feat = string.Empty;
            RequiredFeats = Enumerable.Empty<RequiredFeatSelection>();
            RequiredStats = new Dictionary<string, int>();
            RequiredSkills = Enumerable.Empty<RequiredSkillSelection>();
            RequiredCharacterClasses = new Dictionary<string, int>();
            FocusType = string.Empty;
            Frequency = new Frequency();
        }

        public bool ImmutableRequirementsMet(int baseAttack, Dictionary<string, Stat> stats, IEnumerable<Skill> skills, CharacterClass characterClass)
        {
            foreach (var stat in RequiredStats)
                if (stats[stat.Key].Value < stat.Value)
                    return false;

            if (RequiredCharacterClasses.Any() && (RequiredCharacterClasses.ContainsKey(characterClass.Name) == false || RequiredCharacterClasses[characterClass.Name] > characterClass.Level))
                return false;

            if (baseAttack < RequiredBaseAttack)
                return false;

            return !RequiredSkills.Any() || RequiredSkills.Any(s => s.RequirementMet(skills));
        }

        public bool MutableRequirementsMet(IEnumerable<Feat> feats)
        {
            var proficiencyRequirement = RequiredFeats.FirstOrDefault(f => f.Feat == ItemTypeConstants.Weapon + GroupConstants.Proficiency);
            var requirementsWithoutProficiency = RequiredFeats.Except(new[] { proficiencyRequirement });

            return requirementsWithoutProficiency.All(f => f.RequirementMet(feats));
        }
    }
}
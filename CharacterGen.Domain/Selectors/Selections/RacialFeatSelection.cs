using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Stats;
using CharacterGen.Races;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Selectors.Selections
{
    internal class RacialFeatSelection
    {
        public string Feat { get; set; }
        public int MinimumHitDieRequirement { get; set; }
        public int MaximumHitDieRequirement { get; set; }
        public string SizeRequirement { get; set; }
        public Frequency Frequency { get; set; }
        public string FocusType { get; set; }
        public int Power { get; set; }
        public Dictionary<string, int> MinimumStats { get; set; }
        public string RandomFociQuantity { get; set; }
        public IEnumerable<RequiredFeatSelection> RequiredFeats { get; set; }

        public RacialFeatSelection()
        {
            Feat = string.Empty;
            SizeRequirement = string.Empty;
            Frequency = new Frequency();
            FocusType = string.Empty;
            MinimumStats = new Dictionary<string, int>();
            RandomFociQuantity = string.Empty;
            RequiredFeats = Enumerable.Empty<RequiredFeatSelection>();
        }

        public bool RequirementsMet(Race race, int monsterHitDice, Dictionary<string, Stat> stats, IEnumerable<Feat> feats)
        {
            if (string.IsNullOrEmpty(SizeRequirement) == false && SizeRequirement != race.Size)
                return false;

            if (MaximumHitDieRequirement > 0 && monsterHitDice > MaximumHitDieRequirement)
                return false;

            if (MinimumStatMet(stats) == false)
                return false;

            foreach (var requirement in RequiredFeats)
            {
                var requirementFeats = feats.Where(f => f.Name == requirement.Feat);

                if (requirementFeats.Any() == false)
                    return false;

                if (requirement.Focus != string.Empty && requirementFeats.Any(f => f.Foci.Contains(requirement.Focus)) == false)
                    return false;
            }

            return monsterHitDice >= MinimumHitDieRequirement;
        }

        private bool MinimumStatMet(Dictionary<string, Stat> stats)
        {
            if (MinimumStats.Any() == false)
                return true;

            foreach (var stat in MinimumStats)
                if (stats[stat.Key].Value >= stat.Value)
                    return true;

            return false;
        }
    }
}
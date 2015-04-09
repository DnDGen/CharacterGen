using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Races;

namespace NPCGen.Selectors.Interfaces.Objects
{
    public class RacialFeatSelection
    {
        public NameModel Name { get; set; }
        public Int32 FeatStrength { get; set; }
        public IEnumerable<String> BaseRaceIdRequirements { get; set; }
        public IEnumerable<String> MetaraceIdRequirements { get; set; }
        public IEnumerable<String> MetaraceSpeciesRequirements { get; set; }
        public IEnumerable<Int32> HitDieRequirements { get; set; }
        public String SizeRequirement { get; set; }

        public RacialFeatSelection()
        {
            Name = new NameModel();
            BaseRaceIdRequirements = Enumerable.Empty<String>();
            MetaraceIdRequirements = Enumerable.Empty<String>();
            MetaraceSpeciesRequirements = Enumerable.Empty<String>();
            HitDieRequirements = Enumerable.Empty<Int32>();
            SizeRequirement = String.Empty;
        }

        public Boolean RequirementsMet(Race race, Int32 monsterHitDice)
        {
            if (BaseRaceIdRequirements.Any() && !BaseRaceIdRequirements.Contains(race.BaseRace.Id))
                return false;

            if (MetaraceIdRequirements.Any() && !MetaraceIdRequirements.Contains(race.Metarace.Id))
                return false;

            if (MetaraceSpeciesRequirements.Any() && !MetaraceSpeciesRequirements.Contains(race.MetaraceSpecies))
                return false;

            if (HitDieRequirements.Any() && !HitDieRequirements.Contains(monsterHitDice))
                return false;

            if (SizeRequirement.Any() && SizeRequirement != race.Size)
                return false;

            return true;
        }
    }
}
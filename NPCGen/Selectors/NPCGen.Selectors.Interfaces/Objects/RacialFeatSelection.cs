using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Races;

namespace NPCGen.Selectors.Interfaces.Objects
{
    public class RacialFeatSelection
    {
        public String FeatName { get; set; }
        public Int32 FeatStrength { get; set; }
        public IEnumerable<String> BaseRaceRequirements { get; set; }
        public IEnumerable<String> MetaraceRequirements { get; set; }
        public IEnumerable<String> MetaraceSpeciesRequirements { get; set; }
        public IEnumerable<Int32> HitDieRequirements { get; set; }
        public String SizeRequirement { get; set; }

        public RacialFeatSelection()
        {
            FeatName = String.Empty;
            BaseRaceRequirements = Enumerable.Empty<String>();
            MetaraceRequirements = Enumerable.Empty<String>();
            MetaraceSpeciesRequirements = Enumerable.Empty<String>();
            HitDieRequirements = Enumerable.Empty<Int32>();
            SizeRequirement = String.Empty;
        }

        public Boolean RequirementsMet(Race race, Int32 monsterHitDice)
        {
            if (BaseRaceRequirements.Any() && !BaseRaceRequirements.Contains(race.BaseRace.Id))
                return false;

            if (MetaraceRequirements.Any() && !MetaraceRequirements.Contains(race.Metarace.Id))
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
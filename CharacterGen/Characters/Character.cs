using CharacterGen.Abilities;
using CharacterGen.Abilities.Feats;
using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Items;
using CharacterGen.Magics;
using CharacterGen.Races;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Characters
{
    public class Character
    {
        public Alignment Alignment { get; set; }
        public CharacterClass Class { get; set; }
        public Race Race { get; set; }
        public string InterestingTrait { get; set; }
        public Combat Combat { get; set; }
        public Ability Ability { get; set; }
        public Equipment Equipment { get; set; }
        public Magic Magic { get; set; }

        public bool IsLeader
        {
            get
            {
                return Ability.Feats.Any(f => f.Name == FeatConstants.Leadership);
            }
        }

        public string Summary
        {
            get
            {
                if (Class.Level == 0)
                    return string.Empty;

                var summary = $"{Alignment.Full} {Race.Summary} {Class.Summary}";

                return summary;
            }
        }

        public double ChallengeRating
        {
            get
            {
                if (specialChallengeRatings.Contains(Race.BaseRace))
                {
                    var extra = Class.IsNPC ? 0 : 1;
                    return Class.Level + extra + Race.ChallengeRating;
                }

                var divisor = Class.IsNPC ? 2d : 1d;
                var classChallengeRating = Class.Level / divisor;

                var challengeRating = Race.ChallengeRating + classChallengeRating;

                if (challengeRating > 1)
                    return Math.Floor(challengeRating);

                return challengeRating;
            }
        }

        private readonly IEnumerable<string> specialChallengeRatings;

        public Character()
        {
            Alignment = new Alignment();
            Class = new CharacterClass();
            Race = new Race();
            InterestingTrait = string.Empty;
            Combat = new Combat();
            Ability = new Ability();
            Equipment = new Equipment();
            Magic = new Magic();

            specialChallengeRatings = new[]
            {
                RaceConstants.BaseRaces.Drow,
                RaceConstants.BaseRaces.DuergarDwarf,
                RaceConstants.BaseRaces.Svirfneblin,
            };
        }
    }
}
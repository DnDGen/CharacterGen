using CharacterGen.Abilities;
using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Feats;
using CharacterGen.Items;
using CharacterGen.Magics;
using CharacterGen.Races;
using CharacterGen.Skills;
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
        public IEnumerable<Skill> Skills { get; set; }
        public IEnumerable<string> Languages { get; set; }
        public IEnumerable<Feat> Feats { get; set; }
        public Dictionary<string, Ability> Abilities { get; set; }
        public Equipment Equipment { get; set; }
        public Magic Magic { get; set; }

        public bool IsLeader
        {
            get
            {
                return Feats.Any(f => f.Name == FeatConstants.Leadership);
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
            Skills = Enumerable.Empty<Skill>();
            Languages = Enumerable.Empty<string>();
            Feats = Enumerable.Empty<Feat>();
            Abilities = new Dictionary<string, Ability>();
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
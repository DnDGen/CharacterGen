using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Combats;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Items;
using DnDGen.CharacterGen.Magics;
using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Skills;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CharacterGen.Characters
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
        public FeatCollections Feats { get; set; }
        public Dictionary<string, Ability> Abilities { get; set; }
        public Equipment Equipment { get; set; }
        public Magic Magic { get; set; }

        public bool IsLeader
        {
            get
            {
                return Feats.All.Any(f => f.Name == FeatConstants.Leadership);
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
            Feats = new FeatCollections();
            Abilities = new Dictionary<string, Ability>();
            Equipment = new Equipment();
            Magic = new Magic();

            specialChallengeRatings = new[]
            {
                RaceConstants.BaseRaces.Drow,
                RaceConstants.BaseRaces.DuergarDwarf,
                RaceConstants.BaseRaces.Githyanki,
                RaceConstants.BaseRaces.Githzerai,
                RaceConstants.BaseRaces.Svirfneblin,
            };
        }
    }
}
using CharacterGen.Abilities.Stats;
using System;

namespace CharacterGen.Abilities.Skills
{
    public class Skill
    {
        public readonly string Name;
        public readonly Stat BaseStat;
        public readonly string Focus;

        public int Bonus { get; set; }
        public bool ClassSkill { get; set; }
        public int ArmorCheckPenalty { get; set; }
        public bool CircumstantialBonus { get; set; }
        public int RankCap { get; set; }

        public double EffectiveRanks
        {
            get
            {
                if (ClassSkill)
                    return ranks;

                return ranks / 2d;
            }
        }

        public bool RanksMaxedOut
        {
            get
            {
                return Ranks == RankCap;
            }
        }

        private int ranks;

        public int Ranks
        {
            get
            {
                return ranks;
            }
            set
            {
                if (value > RankCap)
                    throw new InvalidOperationException("Ranks cannot exceed the Rank Cap");

                ranks = value;
            }
        }

        public bool QualifiesForSkillSynergy
        {
            get
            {
                return EffectiveRanks >= 5;
            }
        }

        public int TotalBonus
        {
            get
            {
                var total = EffectiveRanks + Bonus + BaseStat.Bonus + ArmorCheckPenalty;
                var floor = Math.Floor(total);

                return Convert.ToInt32(floor);
            }
        }

        public Skill(string name, Stat baseStat, int rankCap, string focus = "")
        {
            Name = name;
            BaseStat = baseStat;
            RankCap = rankCap;
            Focus = focus;
        }

        public bool IsEqualTo(Skill skill)
        {
            return skill.Name == Name && skill.Focus == Focus;
        }

        public bool IsEqualTo(string skill)
        {
            var sections = skill.Split('/');
            var name = sections[0];
            var focus = sections.Length > 1 ? sections[1] : string.Empty;

            return name == Name && focus == Focus;
        }
    }
}
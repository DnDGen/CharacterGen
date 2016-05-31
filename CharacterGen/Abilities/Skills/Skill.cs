using CharacterGen.Abilities.Stats;

namespace CharacterGen.Abilities.Skills
{
    public class Skill
    {
        public int Ranks { get; set; }
        public Stat BaseStat { get; set; }
        public int Bonus { get; set; }
        public bool ClassSkill { get; set; }
        public int ArmorCheckPenalty { get; set; }
        public bool CircumstantialBonus { get; set; }
        public double EffectiveRanks
        {
            get
            {
                if (ClassSkill)
                    return Ranks;

                return (double)Ranks / 2;
            }
        }
    }
}
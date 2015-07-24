using System;
using System.Collections.Generic;
using System.Linq;

namespace NPCGen.Common
{
    public class Leadership
    {
        public Character Cohort { get; set; }
        public IEnumerable<Character> Followers { get; set; }
        public Int32 Score { get; set; }
        public IEnumerable<String> LeadershipModifiers { get; set; }

        public Leadership()
        {
            Followers = Enumerable.Empty<Character>();
            LeadershipModifiers = Enumerable.Empty<String>();
        }
    }
}
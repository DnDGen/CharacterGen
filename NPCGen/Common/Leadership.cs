using System;
using System.Collections.Generic;
using System.Linq;

namespace NPCGen.Common
{
    public class Leadership
    {
        public Boolean IsFollower { get; set; }
        public Character Cohort { get; set; }
        public IEnumerable<Character> Followers { get; set; }
        public Int32 Score { get; set; }

        public Leadership()
        {
            Followers = Enumerable.Empty<Character>();
        }
    }
}
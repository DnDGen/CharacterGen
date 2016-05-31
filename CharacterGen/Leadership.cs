using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen
{
    public class Leadership
    {
        public int Score { get; set; }
        public IEnumerable<string> LeadershipModifiers { get; set; }
        public int CohortScore { get; set; }
        public FollowerQuantities FollowerQuantities { get; set; }

        public Leadership()
        {
            LeadershipModifiers = Enumerable.Empty<String>();
            FollowerQuantities = new FollowerQuantities();
        }
    }
}
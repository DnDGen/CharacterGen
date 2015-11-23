using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Common
{
    public class Leadership
    {
        public Int32 Score { get; set; }
        public IEnumerable<String> LeadershipModifiers { get; set; }
        public Int32 CohortScore { get; set; }
        public FollowerQuantities FollowerQuantities { get; set; }

        public Leadership()
        {
            LeadershipModifiers = Enumerable.Empty<String>();
            FollowerQuantities = new FollowerQuantities();
        }
    }
}
using System;
using System.Collections.Generic;

namespace NPCGen.Core.Characters.Data.Classes
{
    public class BaseAttack
    {
        public List<Int32> Attacks { get; set; }

        public BaseAttack()
        {
            Attacks = new List<Int32>();
        }

        public override String ToString()
        {
            if (Attacks.Count == 0)
                return String.Empty;

            var output = String.Format("+{0}", Attacks[0]);
            for (var i = 1; i < Attacks.Count; i++)
                output += String.Format("/+{0}", Attacks[i]);

            return output;
        }
    }
}
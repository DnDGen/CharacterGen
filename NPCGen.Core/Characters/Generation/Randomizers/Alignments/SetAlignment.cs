using NPCGen.Core.Characters.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPCGen.Core.Characters.Generation.Randomizers.Alignments
{
    public class SetAlignment : IAlignmentRandomizer
    {
        private Alignment alignment;

        public SetAlignment(Int32 lawfulness, Int32 goodness)
        {
            alignment = new Alignment();
            alignment.Lawfulness = lawfulness;
            alignment.Goodness = goodness;
        }

        public Alignment Randomize()
        {
            return alignment;
        }
    }
}
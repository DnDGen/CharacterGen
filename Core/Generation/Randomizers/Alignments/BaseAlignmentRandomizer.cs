using D20Dice.Dice;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using System;
using System.Collections.Generic;

namespace NPCGen.Core.Generation.Randomizers.Alignments
{
    public abstract class BaseAlignmentRandomizer : IAlignmentRandomizer
    {
        private IDice dice;
        private IPercentileResultProvider percentileResultProvider;

        private const String table = "AlignmentGoodness";

        public BaseAlignmentRandomizer(IDice dice, IPercentileResultProvider percentileResultProvider)
        {
            this.dice = dice;
            this.percentileResultProvider = percentileResultProvider;
        }

        public abstract Alignment Randomize();

        protected Int32 RollLawfulness()
        {
            var roll = dice.d3();

            if (roll == 1)
                return AlignmentConstants.Chaotic;
            else if (roll == 2)
                return AlignmentConstants.Neutral;

            return AlignmentConstants.Lawful;
        }

        protected Int32 RollGoodness()
        {
            var result = percentileResultProvider.GetPercentileResult(table);
            return Convert.ToInt32(result);
        }

        public abstract IEnumerable<Alignment> GetAllPossibleResults();

        protected IEnumerable<Int32> GetAllGoodnesses()
        {
            var stringGoodnesses = percentileResultProvider.GetAllResults(table);
            var intGoodnesses = new List<Int32>();

            foreach (var goodness in stringGoodnesses)
                intGoodnesses.Add(Convert.ToInt32(goodness));

            return intGoodnesses;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Common.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NPCGen.Generators.Interfaces.Verifiers.Exceptions;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators.Randomizers.Alignments
{
    public abstract class BaseAlignmentRandomizer : IAlignmentRandomizer
    {
        private IDice dice;
        private IPercentileSelector percentileResultProvider;

        private const String table = "AlignmentGoodness";

        public BaseAlignmentRandomizer(IDice dice, IPercentileSelector percentileResultProvider)
        {
            this.dice = dice;
            this.percentileResultProvider = percentileResultProvider;
        }

        public Alignment Randomize()
        {
            var possibleAlignments = GetAllPossibleResults();
            if (!possibleAlignments.Any())
                throw new IncompatibleRandomizersException();

            var alignment = new Alignment();

            do
            {
                alignment.Lawfulness = RollLawfulness();
                alignment.Goodness = RollGoodness();
            } while (!possibleAlignments.Any(a => a.Lawfulness == alignment.Lawfulness && a.Goodness == alignment.Goodness));

            return alignment;
        }

        private String RollLawfulness()
        {
            switch (dice.d3())
            {
                case 1: return AlignmentConstants.Chaotic;
                case 2: return AlignmentConstants.Neutral;
                case 3: return AlignmentConstants.Lawful;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private String RollGoodness()
        {
            var result = percentileResultProvider.GetPercentileResult(table);
            return result;
        }

        public IEnumerable<Alignment> GetAllPossibleResults()
        {
            var alignments = new List<Alignment>();
            var goodnesses = percentileResultProvider.GetAllResults(table);

            foreach (var goodness in goodnesses)
                foreach (var lawfulness in AlignmentConstants.GetLawfulnesses())
                    alignments.Add(new Alignment() { Goodness = goodness, Lawfulness = lawfulness });

            return alignments.Where(a => AlignmentIsAllowed(a));
        }

        protected abstract Boolean AlignmentIsAllowed(Alignment alignment);
    }
}
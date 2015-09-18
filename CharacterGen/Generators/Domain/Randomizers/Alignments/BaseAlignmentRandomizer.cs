using CharacterGen.Common.Alignments;
using CharacterGen.Generators.Randomizers.Alignments;
using CharacterGen.Generators.Verifiers.Exceptions;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Randomizers.Alignments
{
    public abstract class BaseAlignmentRandomizer : IterativeBuilder, IAlignmentRandomizer
    {
        private IPercentileSelector percentileResultSelector;

        public BaseAlignmentRandomizer(IPercentileSelector percentileResultSelector)
        {
            this.percentileResultSelector = percentileResultSelector;
        }

        public Alignment Randomize()
        {
            var possibleAlignments = GetAllPossibleResults();
            if (possibleAlignments.Any() == false)
                throw new IncompatibleRandomizersException();

            return Build(BuildAlignment, a => possibleAlignments.Contains(a));
        }

        private Alignment BuildAlignment()
        {
            var alignment = new Alignment();

            alignment.Lawfulness = percentileResultSelector.SelectFrom(TableNameConstants.Set.Percentile.AlignmentLawfulness);
            alignment.Goodness = percentileResultSelector.SelectFrom(TableNameConstants.Set.Percentile.AlignmentGoodness);

            return alignment;
        }

        public IEnumerable<Alignment> GetAllPossibleResults()
        {
            var alignments = new List<Alignment>();
            var goodnesses = percentileResultSelector.SelectAllFrom(TableNameConstants.Set.Percentile.AlignmentGoodness);
            var lawfulnesses = percentileResultSelector.SelectAllFrom(TableNameConstants.Set.Percentile.AlignmentLawfulness);

            foreach (var goodness in goodnesses)
                foreach (var lawfulness in lawfulnesses)
                    alignments.Add(new Alignment { Goodness = goodness, Lawfulness = lawfulness });

            return alignments.Where(a => AlignmentIsAllowed(a));
        }

        protected abstract Boolean AlignmentIsAllowed(Alignment alignment);
    }
}
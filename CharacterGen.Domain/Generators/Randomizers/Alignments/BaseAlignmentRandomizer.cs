using CharacterGen.Alignments;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Randomizers.Alignments;
using CharacterGen.Verifiers.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Alignments
{
    internal abstract class BaseAlignmentRandomizer : IAlignmentRandomizer
    {
        private IPercentileSelector percentileResultSelector;
        private Generator generator;
        private ICollectionsSelector collectionsSelector;

        public BaseAlignmentRandomizer(IPercentileSelector percentileResultSelector, Generator generator, ICollectionsSelector collectionsSelector)
        {
            this.percentileResultSelector = percentileResultSelector;
            this.generator = generator;
            this.collectionsSelector = collectionsSelector;
        }

        public Alignment Randomize()
        {
            var possibleAlignments = GetAllPossibleResults();
            if (possibleAlignments.Any() == false)
                throw new IncompatibleRandomizersException();

            return generator.Generate(
                GenerateAlignment,
                a => possibleAlignments.Contains(a),
                () => collectionsSelector.SelectRandomFrom(possibleAlignments));
        }

        private Alignment GenerateAlignment()
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

        protected abstract bool AlignmentIsAllowed(Alignment alignment);
    }
}
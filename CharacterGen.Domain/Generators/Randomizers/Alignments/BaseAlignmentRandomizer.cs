using CharacterGen.Alignments;
using CharacterGen.Domain.Tables;
using CharacterGen.Randomizers.Alignments;
using CharacterGen.Verifiers.Exceptions;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Randomizers.Alignments
{
    internal abstract class BaseAlignmentRandomizer : IAlignmentRandomizer
    {
        private readonly IPercentileSelector percentileResultSelector;
        private readonly Generator generator;
        private readonly ICollectionSelector collectionsSelector;

        public BaseAlignmentRandomizer(IPercentileSelector percentileResultSelector, Generator generator, ICollectionSelector collectionsSelector)
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
                () => collectionsSelector.SelectRandomFrom(possibleAlignments),
                a => $"{a.Full} is not from [{string.Join(",", possibleAlignments)}]",
                $"alignment from [{string.Join(",", possibleAlignments)}]");
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
            var goodnesses = percentileResultSelector.SelectAllFrom(TableNameConstants.Set.Percentile.AlignmentGoodness);
            var lawfulnesses = percentileResultSelector.SelectAllFrom(TableNameConstants.Set.Percentile.AlignmentLawfulness);

            foreach (var goodness in goodnesses)
            {
                foreach (var lawfulness in lawfulnesses)
                {
                    var alignment = new Alignment { Goodness = goodness, Lawfulness = lawfulness };
                    if (AlignmentIsAllowed(alignment))
                        yield return alignment;
                }
            }
        }

        protected abstract bool AlignmentIsAllowed(Alignment alignment);
    }
}
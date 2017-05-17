using CharacterGen.Alignments;
using CharacterGen.Randomizers.Alignments;
using EventGen;

namespace CharacterGen.Domain.Generators.Alignments
{
    internal class AlignmentGeneratorEventGenDecorator : IAlignmentGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly IAlignmentGenerator innerGenerator;

        public AlignmentGeneratorEventGenDecorator(IAlignmentGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public Alignment GenerateWith(IAlignmentRandomizer alignmentRandomizer)
        {
            eventQueue.Enqueue("CharacterGen", "Beginning alignment generation");
            var alignment = innerGenerator.GenerateWith(alignmentRandomizer);
            eventQueue.Enqueue("CharacterGen", $"Completed generation of {alignment.Full}");

            return alignment;
        }
    }
}

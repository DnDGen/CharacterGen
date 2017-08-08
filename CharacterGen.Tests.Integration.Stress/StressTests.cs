using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Randomizers.Alignments;
using CharacterGen.Randomizers.CharacterClasses;
using CharacterGen.Randomizers.Races;
using CharacterGen.Verifiers;
using CharacterGen.Verifiers.Exceptions;
using DnDGen.Stress;
using DnDGen.Stress.Events;
using EventGen;
using Ninject;
using NUnit.Framework;
using System.Reflection;

namespace CharacterGen.Tests.Integration.Stress
{
    [TestFixture]
    [Stress]
    public abstract class StressTests : IntegrationTests
    {
        [Inject]
        public IRandomizerVerifier RandomizerVerifier { get; set; }
        [Inject, Named(AlignmentRandomizerTypeConstants.Any)]
        public IAlignmentRandomizer AlignmentRandomizer { get; set; }
        [Inject, Named(ClassNameRandomizerTypeConstants.AnyPlayer)]
        public IClassNameRandomizer ClassNameRandomizer { get; set; }
        [Inject, Named(LevelRandomizerTypeConstants.Any)]
        public ILevelRandomizer LevelRandomizer { get; set; }
        [Inject, Named(RaceRandomizerTypeConstants.BaseRace.AnyBase)]
        public RaceRandomizer BaseRaceRandomizer { get; set; }
        [Inject, Named(RaceRandomizerTypeConstants.Metarace.AnyMeta)]
        public RaceRandomizer MetaraceRandomizer { get; set; }

        protected Stressor stressor;

        [OneTimeSetUp]
        public void StressSetup()
        {
            var options = new StressorWithEventsOptions();
            options.RunningAssembly = Assembly.GetExecutingAssembly();
            options.TimeLimitPercentage = .7;

#if STRESS
            options.IsFullStress = true;
#else
            options.IsFullStress = false;
#endif

            options.ClientIdManager = GetNewInstanceOf<ClientIDManager>();
            options.EventQueue = GetNewInstanceOf<GenEventQueue>();
            options.Source = "CharacterGen";

            stressor = new StressorWithEvents(options);
        }

        protected Alignment GetNewAlignment()
        {
            var compatible = RandomizerVerifier.VerifyCompatibility(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer);

            if (compatible == false)
                throw new IncompatibleRandomizersException();

            var alignment = stressor.Generate(
                () => AlignmentRandomizer.Randomize(),
                a => RandomizerVerifier.VerifyAlignmentCompatibility(a, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer));

            return alignment;
        }

        protected CharacterClass GetNewCharacterClass(Alignment alignment)
        {
            var characterClass = stressor.Generate(
                () => GenerateCharacterClass(alignment),
                c => RandomizerVerifier.VerifyCharacterClassCompatibility(alignment, c, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer));

            return characterClass;
        }

        private CharacterClass GenerateCharacterClass(Alignment alignment)
        {
            var characterClass = new CharacterClass();
            characterClass.Name = ClassNameRandomizer.Randomize(alignment);
            characterClass.Level = LevelRandomizer.Randomize();

            return characterClass;
        }
    }
}
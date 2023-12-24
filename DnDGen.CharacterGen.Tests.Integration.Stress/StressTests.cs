using DnDGen.CharacterGen.Characters;
using DnDGen.CharacterGen.Generators.Characters;
using DnDGen.CharacterGen.Randomizers.Alignments;
using DnDGen.CharacterGen.Randomizers.CharacterClasses;
using DnDGen.CharacterGen.Randomizers.Races;
using DnDGen.CharacterGen.Verifiers;
using DnDGen.Stress;
using Ninject;
using NUnit.Framework;
using System.Reflection;

namespace DnDGen.CharacterGen.Tests.Integration.Stress
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
        [Inject]
        public ICharacterGenerator CharacterGenerator { get; set; }

        protected Stressor stressor;

        [OneTimeSetUp]
        public void StressSetup()
        {
            var options = new StressorOptions();
            options.RunningAssembly = Assembly.GetExecutingAssembly();
            options.TimeLimitPercentage = .90;

#if STRESS
            options.IsFullStress = true;
#else
            options.IsFullStress = false;
#endif

            stressor = new Stressor(options);
        }

        protected CharacterPrototype GetCharacterPrototype()
        {
            var prototype = CharacterGenerator.GeneratePrototypeWith(AlignmentRandomizer, ClassNameRandomizer, LevelRandomizer, BaseRaceRandomizer, MetaraceRandomizer);
            return prototype;
        }
    }
}
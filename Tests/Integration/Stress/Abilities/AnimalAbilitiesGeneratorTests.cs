using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress.Abilities
{
    [TestFixture]
    public class AnimalAbilitiesGeneratorTests : StressTests
    {
        [Inject, Named(AbilitiesGeneratorTypeConstants.Animal)]
        public IAbilitiesGenerator AnimalAbilitiesGenerator { get; set; }
        [Inject]
        public ISetStatsRandomizer SetStatsRandomizer { get; set; }
        [Inject, Named(AbilitiesGeneratorTypeConstants.Animal)]
        public ICombatGenerator AnimalCombatGenerator { get; set; }

        [TestCase("AnimalAbilitiesGenerator"), Ignore("Not yet implemented")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
        }
    }
}

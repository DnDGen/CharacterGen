using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public class DependentDataCollectionTests : StressTests
    {
        protected override void MakeAssertions()
        {
            var data = GetNewDependentData();

            Assert.That(data.CharacterClass.ClassName, Is.EqualTo(data.CharacterClassPrototype.ClassName));
            Assert.That(data.CharacterClass.Level, Is.EqualTo(data.CharacterClassPrototype.Level));

            var classNames = ClassNameRandomizer.GetAllPossibleResults(data.Alignment);
            Assert.That(classNames, Is.Not.Empty);

            var baseRaces = BaseRaceRandomizer.GetAllPossibleResults(data.Alignment.Goodness, data.CharacterClassPrototype);
            Assert.That(baseRaces, Is.Not.Empty);

            var metaraces = MetaraceRandomizer.GetAllPossibleResults(data.Alignment.Goodness, data.CharacterClassPrototype);
            Assert.That(metaraces, Is.Not.Empty);
        }
    }
}
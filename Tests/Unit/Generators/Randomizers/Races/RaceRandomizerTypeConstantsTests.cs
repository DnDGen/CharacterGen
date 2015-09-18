using CharacterGen.Generators.Randomizers.Races;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races
{
    [TestFixture]
    public class RaceRandomizerTypeConstantsTests
    {
        [TestCase(RaceRandomizerTypeConstants.BaseRace.AnimalBase, "Animal Base")]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.AnyBase, "Any Base")]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.EvilBase, "Evil Base")]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.GoodBase, "Good Base")]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.NeutralBase, "Neutral Base")]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.NonEvilBase, "Non-Evil Base")]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.NonGoodBase, "Non-Good Base")]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.NonNeutralBase, "Non-Neutral Base")]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.NonStandardBase, "Non-Standard Base")]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.StandardBase, "Standard Base")]
        [TestCase(RaceRandomizerTypeConstants.Metarace.AnyMeta, "Any Meta")]
        [TestCase(RaceRandomizerTypeConstants.Metarace.EvilMeta, "Evil Meta")]
        [TestCase(RaceRandomizerTypeConstants.Metarace.GoodMeta, "Good Meta")]
        [TestCase(RaceRandomizerTypeConstants.Metarace.NeutralMeta, "Neutral Meta")]
        [TestCase(RaceRandomizerTypeConstants.Metarace.NonEvilMeta, "Non-Evil Meta")]
        [TestCase(RaceRandomizerTypeConstants.Metarace.NonGoodMeta, "Non-Good Meta")]
        [TestCase(RaceRandomizerTypeConstants.Metarace.NonNeutralMeta, "Non-Neutral Meta")]
        [TestCase(RaceRandomizerTypeConstants.Metarace.GeneticMeta, "Genetic Meta")]
        [TestCase(RaceRandomizerTypeConstants.Metarace.LycanthropeMeta, "Lycanthrope Meta")]
        [TestCase(RaceRandomizerTypeConstants.Metarace.NoMeta, "No Meta")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}

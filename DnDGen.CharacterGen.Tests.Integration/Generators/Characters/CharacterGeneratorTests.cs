using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Generators.Characters;
using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Randomizers.Abilities;
using DnDGen.CharacterGen.Randomizers.Alignments;
using DnDGen.CharacterGen.Randomizers.CharacterClasses;
using DnDGen.CharacterGen.Randomizers.Races;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Integration.Generators.Characters
{
    internal class CharacterGeneratorTests : IntegrationTests
    {
        private ICharacterGenerator characterGenerator;

        [SetUp]
        public void Setup()
        {
            characterGenerator = GetNewInstanceOf<ICharacterGenerator>();
        }

        [Test]
        public void BUG_GenerateWith_ReturnsCharacter_WithoutMetarace()
        {
            var abilitiesRandomizer = GetNewInstanceOf<IAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.Raw);
            var metaraceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.AnyMeta);
            var baseRaceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.AnyBase);
            var levelRandomizer = GetNewInstanceOf<ILevelRandomizer>(LevelRandomizerTypeConstants.Any);
            var classNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyPlayer);
            var alignmentRandomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);

            //INFO: will try 2 times to see if we get a character without metarace. Should happen at least once, if not more than once
            var hasMeta = true;
            var attempts = 2;

            while (attempts-- > 0 && hasMeta)
            {
                var character = characterGenerator.GenerateWith(
                    alignmentRandomizer,
                    classNameRandomizer,
                    levelRandomizer,
                    baseRaceRandomizer,
                    metaraceRandomizer,
                    abilitiesRandomizer);
                Assert.That(character, Is.Not.Null);
                Assert.That(character.Summary, Is.Not.Empty);
                Assert.That(character.Alignment.Full, Is.Not.Empty);
                Assert.That(character.Class.Level, Is.AtLeast(1));
                Assert.That(character.Class.Summary, Is.Not.Empty);
                Assert.That(character.Race.Summary, Is.Not.Empty);

                hasMeta = character.Race.Metarace != RaceConstants.Metaraces.None;
            }

            Assert.That(hasMeta, Is.False);
        }

        [Repeat(1000)]
        [Test]
        public void BUG_GenerateWith_ReturnsCharacter_SpecialistWizard()
        {
            var abilitiesRandomizer = GetNewInstanceOf<IAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.Raw);
            var metaraceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.NoMeta);
            var alignmentRandomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);

            var baseRaceRandomizer = GetNewInstanceOf<ISetBaseRaceRandomizer>();
            baseRaceRandomizer.SetBaseRace = RaceConstants.BaseRaces.Human;

            var levelRandomizer = GetNewInstanceOf<ISetLevelRandomizer>();
            levelRandomizer.SetLevel = 1;

            var classNameRandomizer = GetNewInstanceOf<ISetClassNameRandomizer>();
            classNameRandomizer.SetClassName = CharacterClassConstants.Wizard;

            var character = characterGenerator.GenerateWith(
                alignmentRandomizer,
                classNameRandomizer,
                levelRandomizer,
                baseRaceRandomizer,
                metaraceRandomizer,
                abilitiesRandomizer);
            Assert.That(character, Is.Not.Null);
            Assert.That(character.Summary, Is.Not.Empty);

            if (character.Class.SpecialistFields.Any())
            {
                var message = $"{character.Summary}; S: {string.Join(", ", character.Class.SpecialistFields)}; P: {string.Join(", ", character.Class.ProhibitedFields)}";
                Assert.That(character.Class.SpecialistFields.Count(), Is.EqualTo(1), message);

                var intersect = character.Class.SpecialistFields.Intersect(character.Class.ProhibitedFields);
                Assert.That(intersect, Is.Empty, message);

                if (character.Class.SpecialistFields.First() == CharacterClassConstants.Schools.Divination)
                {
                    Assert.That(character.Class.ProhibitedFields.Count(), Is.EqualTo(1), message);
                }
                else
                {
                    Assert.That(character.Class.ProhibitedFields.Count(), Is.EqualTo(2), message);
                }
            }
        }
    }
}

using CharacterGen.Abilities.Feats;
using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators;
using CharacterGen.Domain.Generators.Magics;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Magics
{
    [TestFixture]
    public class AnimalGeneratorTests
    {
        private const string Animal = "animal";

        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Generator generator;
        private IAnimalGenerator animalGenerator;
        private CharacterClass characterClass;
        private List<Feat> feats;
        private List<string> animals;
        private List<string> druidAnimals;
        private Race characterRace;
        private Alignment alignment;
        private Dictionary<string, int> levelAdjustments;
        private List<string> animalsForSize;
        private List<string> improvedFamiliars;
        private List<string> animalsForMetarace;
        private List<string> mages;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            generator = new ConfigurableIterationGenerator();
            animalGenerator = new AnimalGenerator(mockCollectionsSelector.Object, mockAdjustmentsSelector.Object, generator);

            characterClass = new CharacterClass();
            characterRace = new Race();
            feats = new List<Feat>();
            animals = new List<string>();
            alignment = new Alignment();
            levelAdjustments = new Dictionary<string, int>();
            animalsForSize = new List<string>();
            improvedFamiliars = new List<string>();
            druidAnimals = new List<string>();
            animalsForMetarace = new List<string>();
            mages = new List<string>();

            characterRace.BaseRace = "character race";
            characterRace.Metarace = "character metarace";
            characterRace.Size = "character size";
            characterClass.Level = 9266;
            characterClass.ClassName = "class name";
            animals.Add(Animal);
            animalsForSize.Add(Animal);
            animalsForMetarace.Add(Animal);
            levelAdjustments[Animal] = 0;

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.Animals, characterClass.ClassName)).Returns(animals);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.Animals, CharacterClassConstants.Druid)).Returns(druidAnimals);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.Animals, characterRace.Size)).Returns(animalsForSize);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.Animals, characterRace.Metarace)).Returns(animalsForMetarace);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.Animals, FeatConstants.ImprovedFamiliar)).Returns(improvedFamiliars);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Mages)).Returns(mages);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.LevelAdjustments)).Returns(levelAdjustments);
        }

        [Test]
        public void GenerateNoAnimalIfNoneAvailable()
        {
            animals.Clear();
            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.Empty);
        }

        [Test]
        public void GenerateNoAnimalIfNoneAvailableWithinSize()
        {
            animals.Clear();
            animals.Add("other animal");
            levelAdjustments["other animal"] = 0;

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.Empty);
        }

        [Test]
        public void GenerateNoAnimalIfNoneMatchSize()
        {
            animalsForSize.Clear();
            animalsForSize.Add("other animal");

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.Empty);
        }

        [Test]
        public void GenerateNoAnimalIfNoneFitWithinLevel()
        {
            levelAdjustments[Animal] = characterClass.Level * -1;
            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.Empty);
        }

        [Test]
        public void FilterByMetaraceIfClassIsMage()
        {
            animalsForMetarace.Clear();
            animalsForMetarace.Add("other animal");
            mages.Add(characterClass.ClassName);

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.Empty);
        }

        [Test]
        public void DoNotFilterByMetaraceIfClassIsNotMage()
        {
            animalsForMetarace.Clear();
            animalsForMetarace.Add("other animal");
            mages.Add("other class");

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.EqualTo(Animal));
        }

        [Test]
        public void GenerateAnimal()
        {
            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.EqualTo(Animal));
        }

        [Test]
        public void GenerateImprovedFamiliar()
        {
            animals.Add("other animal");
            levelAdjustments["other animal"] = 0;
            improvedFamiliars.Add(Animal);

            feats.Add(new Feat { Name = FeatConstants.ImprovedFamiliar });

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.EqualTo(Animal));
        }

        [Test]
        public void DoNotGenerateImprovedFamiliarBecauseFamiliarIsNotImproved()
        {
            animals.Add("other animal");
            levelAdjustments["other animal"] = 0;
            improvedFamiliars.Add("other animal");

            feats.Add(new Feat { Name = FeatConstants.ImprovedFamiliar });

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.EqualTo(Animal));
        }

        [Test]
        public void DoNotGenerateImprovedFamiliarBecauseCharacterDoesNotHaveImprovedFamiliarFeat()
        {
            animals.Add("other animal");
            levelAdjustments["other animal"] = 0;
            improvedFamiliars.Add("other animal");

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.EqualTo(Animal));
        }

        [Test]
        public void DoNotGenerateImprovedFamiliarBecauseMetaraceDoesNotAllowIt()
        {
            animals.Add("other animal");
            levelAdjustments["other animal"] = 0;
            improvedFamiliars.Add("other animal");
            improvedFamiliars.Add(Animal);
            mages.Add(characterClass.ClassName);

            feats.Add(new Feat { Name = FeatConstants.ImprovedFamiliar });

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.EqualTo(Animal));
        }

        [Test]
        public void RangersUseHalfTheirLevelAsDruidLevel()
        {
            characterClass.ClassName = CharacterClassConstants.Ranger;
            druidAnimals.Add("other animal");
            druidAnimals.Add(Animal);
            levelAdjustments["other animal"] = characterClass.Level / 2 + 1;

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.EqualTo(Animal));
        }

        [Test]
        public void OriginalClassNotModified()
        {
            characterClass.ClassName = CharacterClassConstants.Ranger;
            druidAnimals.Add("other animal");
            druidAnimals.Add(Animal);
            levelAdjustments["other animal"] = characterClass.Level / -2 - 1;

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(characterClass.ClassName, Is.EqualTo(CharacterClassConstants.Ranger));
            Assert.That(characterClass.Level, Is.EqualTo(9266));
        }

        [Test]
        public void AdeptDoesNotGetAnimalAtLevel1()
        {
            characterClass.ClassName = CharacterClassConstants.Adept;
            characterClass.Level = 1;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.Animals, CharacterClassConstants.Adept)).Returns(animals);

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.Empty);
        }

        [Test]
        public void AdeptGetsAnimalAtLevel2()
        {
            characterClass.ClassName = CharacterClassConstants.Adept;
            characterClass.Level = 2;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.Animals, CharacterClassConstants.Adept)).Returns(animals);

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.EqualTo(Animal));
        }
    }
}

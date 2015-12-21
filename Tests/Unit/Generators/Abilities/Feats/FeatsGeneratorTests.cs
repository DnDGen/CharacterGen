using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Combats;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Abilities.Feats;
using CharacterGen.Generators.Domain.Abilities.Feats;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Abilities.Feats
{
    [TestFixture]
    public class FeatsGeneratorTests
    {
        private IFeatsGenerator featsGenerator;
        private Mock<IRacialFeatsGenerator> mockRacialFeatsGenerator;
        private Mock<IClassFeatsGenerator> mockClassFeatsGenerator;
        private Mock<IAdditionalFeatsGenerator> mockAdditionalFeatsGenerator;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private CharacterClass characterClass;
        private Race race;
        private Dictionary<string, Stat> stats;
        private Dictionary<string, Skill> skills;
        private BaseAttack baseAttack;
        private List<Feat> racialFeats;

        [SetUp]
        public void Setup()
        {
            mockRacialFeatsGenerator = new Mock<IRacialFeatsGenerator>();
            mockClassFeatsGenerator = new Mock<IClassFeatsGenerator>();
            mockAdditionalFeatsGenerator = new Mock<IAdditionalFeatsGenerator>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            featsGenerator = new FeatsGenerator(mockRacialFeatsGenerator.Object, mockClassFeatsGenerator.Object, mockAdditionalFeatsGenerator.Object,
                mockCollectionsSelector.Object);

            characterClass = new CharacterClass();
            race = new Race();
            stats = new Dictionary<string, Stat>();
            skills = new Dictionary<string, Skill>();
            baseAttack = new BaseAttack();
            racialFeats = new List<Feat>();

            mockRacialFeatsGenerator.Setup(g => g.GenerateWith(race, skills, stats)).Returns(racialFeats);
        }

        [Test]
        public void GetRacialFeats()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Foci = new[] { "focus" };
            racialFeats[0].Name = "racialFeat1";
            racialFeats[0].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 42;
            racialFeats[1].Frequency.TimePeriod = "fortnight";
            racialFeats[1].Name = "racialFeat2";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name, Is.EqualTo("racialFeat1"));
            Assert.That(first.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(first.Strength, Is.EqualTo(9266));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);

            Assert.That(last.Name, Is.EqualTo("racialFeat2"));
            Assert.That(last.Foci, Is.Empty);
            Assert.That(last.Strength, Is.EqualTo(0));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetClassFeats()
        {
            var classFeats = new List<Feat>();
            classFeats.Add(new Feat());
            classFeats.Add(new Feat());

            classFeats[0].Foci = new[] { "focus" };
            classFeats[0].Name = "classFeat1";
            classFeats[0].Strength = 9266;
            classFeats[1].Frequency.Quantity = 42;
            classFeats[1].Frequency.TimePeriod = "fortnight";
            classFeats[1].Name = "classFeat2";

            mockClassFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, racialFeats, skills)).Returns(classFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name, Is.EqualTo("classFeat1"));
            Assert.That(first.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(first.Strength, Is.EqualTo(9266));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);

            Assert.That(last.Name, Is.EqualTo("classFeat2"));
            Assert.That(last.Foci, Is.Empty);
            Assert.That(last.Strength, Is.EqualTo(0));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAdditionalFeats()
        {
            var additionalFeats = new List<Feat>();
            additionalFeats.Add(new Feat());
            additionalFeats.Add(new Feat());

            additionalFeats[0].Foci = new[] { "focus" };
            additionalFeats[0].Name = "feat1";
            additionalFeats[0].Strength = 9266;
            additionalFeats[1].Frequency.Quantity = 42;
            additionalFeats[1].Frequency.TimePeriod = "fortnight";
            additionalFeats[1].Name = "feat2";

            mockAdditionalFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack, It.IsAny<IEnumerable<Feat>>())).Returns(additionalFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(first.Strength, Is.EqualTo(9266));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.Empty);

            Assert.That(last.Name, Is.EqualTo("feat2"));
            Assert.That(last.Foci, Is.Empty);
            Assert.That(last.Strength, Is.EqualTo(0));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllFeats()
        {
            racialFeats.Add(new Feat());

            racialFeats[0].Foci = new[] { "focus" };
            racialFeats[0].Name = "racialFeat1";
            racialFeats[0].Strength = 9266;

            var additionalFeats = new List<Feat>();
            additionalFeats.Add(new Feat());

            additionalFeats[0].Foci = new[] { "other focus" };
            additionalFeats[0].Name = "feat1";
            additionalFeats[0].Strength = 9266;
            additionalFeats[0].Frequency.Quantity = 42;
            additionalFeats[0].Frequency.TimePeriod = "fortnight";

            var classFeats = new List<Feat>();
            classFeats.Add(new Feat());

            classFeats[0].Name = "classFeat1";
            classFeats[0].Strength = 9266;

            mockClassFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, racialFeats, skills)).Returns(classFeats);
            mockAdditionalFeatsGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack, It.IsAny<IEnumerable<Feat>>())).Returns(additionalFeats);

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(feats.Count(), Is.EqualTo(3));
        }

        [Test]
        public void IfNameAndFocusAndStrengthAndFrequencyTimePeriodAreEqual_CombineFrequencyQuantities()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Foci = new[] { "focus" };
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 1;
            racialFeats[0].Frequency.TimePeriod = "fortnight";
            racialFeats[1].Foci = new[] { "focus" };
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 5;
            racialFeats[1].Frequency.TimePeriod = "fortnight";
            racialFeats[2].Name = "feat2";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(6));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Name, Is.EqualTo("feat2"));
            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNamesDoNotMatch_DoNotCombineFrequencyQuantities()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Foci = new[] { "focus" };
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 1;
            racialFeats[0].Frequency.TimePeriod = "fortnight";
            racialFeats[1].Foci = new[] { "focus" };
            racialFeats[1].Name = "feat2";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 5;
            racialFeats[1].Frequency.TimePeriod = "fortnight";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(1));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(5));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(last.Name, Is.EqualTo("feat2"));
            Assert.That(last.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfFociDoNotMatch_DoNotCombineFrequencyQuantities()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Foci = new[] { "focus" };
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 4;
            racialFeats[0].Frequency.TimePeriod = "fortnight";
            racialFeats[1].Foci = new[] { "focus2" };
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 2;
            racialFeats[1].Frequency.TimePeriod = "fortnight";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(4));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Foci.Single(), Is.EqualTo("focus2"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(2));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(last.Name, Is.EqualTo("feat1"));
            Assert.That(last.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfStrengthsDoNotMatch_DoNotCombineFrequencyQuantities()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Foci = new[] { "focus" };
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 3;
            racialFeats[0].Frequency.TimePeriod = "fortnight";
            racialFeats[1].Foci = new[] { "focus" };
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 42;
            racialFeats[1].Frequency.Quantity = 3;
            racialFeats[1].Frequency.TimePeriod = "fortnight";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(3));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(3));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(last.Name, Is.EqualTo("feat1"));
            Assert.That(last.Strength, Is.EqualTo(42));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfFrequencyTimePeriodsDoNotMatch_DoNotCombineFrequencyQuantities()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Foci = new[] { "focus" };
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 1;
            racialFeats[0].Frequency.TimePeriod = "day";
            racialFeats[1].Foci = new[] { "focus" };
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 5;
            racialFeats[1].Frequency.TimePeriod = "fortnight";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(1));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("day"));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(5));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
            Assert.That(last.Name, Is.EqualTo("feat1"));
            Assert.That(last.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNoFrequencyTimePeriods_DoNotCombineFrequencyQuantities()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Foci = new[] { "focus" };
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 1;
            racialFeats[1].Foci = new[] { "focus" };
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 5;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            //INFO: There is still only 1 feat here because strengths will filter out the duplicate
            var first = feats.Single();

            Assert.That(first.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(1));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));
        }

        [Test]
        public void IfNamesAndFocusAndStrengthAreEqual_ConstantWinsOut()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Foci = new[] { "focus" };
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 5;
            racialFeats[0].Frequency.TimePeriod = FeatConstants.Frequencies.Day;
            racialFeats[1].Foci = new[] { "focus" };
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 0;
            racialFeats[1].Frequency.TimePeriod = FeatConstants.Frequencies.Constant;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();

            Assert.That(first.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Constant));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(1));
        }

        [Test]
        public void IfNamesAndFocusAndStrengthAreEqual_AtWillWinsOut()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Foci = new[] { "focus" };
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 5;
            racialFeats[0].Frequency.TimePeriod = FeatConstants.Frequencies.Day;
            racialFeats[1].Foci = new[] { "focus" };
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 0;
            racialFeats[1].Frequency.TimePeriod = FeatConstants.Frequencies.AtWill;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();

            Assert.That(first.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.AtWill));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ConstantFrequencyBeatsAtWillFrequency()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Foci = new[] { "focus" };
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.Quantity = 0;
            racialFeats[0].Frequency.TimePeriod = FeatConstants.Frequencies.AtWill;
            racialFeats[1].Foci = new[] { "focus" };
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 0;
            racialFeats[1].Frequency.TimePeriod = FeatConstants.Frequencies.Constant;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();

            Assert.That(first.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Constant));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(1));
        }

        [Test]
        public void IfNoFrequencyAndRestMatchesButUnequalStrengths_KeepHigherStrength()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Foci = new[] { "focus" };
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 42;
            racialFeats[1].Foci = new[] { "focus" };
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[2].Name = "feat2";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Name, Is.EqualTo("feat2"));
            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfHasFrequencyAndRestMatchesButUnequalStrengths_DoNotRemoveStrengths()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Foci = new[] { "focus" };
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 42;
            racialFeats[0].Frequency.Quantity = 0;
            racialFeats[0].Frequency.TimePeriod = "time period";
            racialFeats[1].Foci = new[] { "focus" };
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.Quantity = 0;
            racialFeats[1].Frequency.TimePeriod = "time period";

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(first.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(first.Frequency.TimePeriod, Is.EqualTo("time period"));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(42));

            Assert.That(last.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("time period"));
            Assert.That(last.Name, Is.EqualTo("feat1"));
            Assert.That(last.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNoFrequencyButStrengthsAndNamesDoNotMatch_DoNotRemoveStrengths()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Foci = new[] { "focus" };
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 42;
            racialFeats[1].Foci = new[] { "focus" };
            racialFeats[1].Name = "feat2";
            racialFeats[1].Strength = 9266;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(42));

            Assert.That(last.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(last.Name, Is.EqualTo("feat2"));
            Assert.That(last.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNoFrequencyButStrengthAndFociDoNotMatch_DoNotRemoveStrengths()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Foci = new[] { "focus" };
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 42;
            racialFeats[1].Foci = new[] { "focus2" };
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 9266;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(42));

            Assert.That(last.Foci.Single(), Is.EqualTo("focus2"));
            Assert.That(last.Name, Is.EqualTo("feat1"));
            Assert.That(last.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNoFrequencyAndFeatNamesMatchAndFociMatchAndEqualStrength_RemoveDuplicateStrengths()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Foci = new[] { "focus" };
            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 42;
            racialFeats[1].Foci = new[] { "focus" };
            racialFeats[1].Name = "feat1";
            racialFeats[1].Strength = 42;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();

            Assert.That(first.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Strength, Is.EqualTo(42));

            Assert.That(feats.Count(), Is.EqualTo(1));
        }

        [Test]
        public void FeatsThatCanBeTakenMultipleTimesAreAllowed()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Name = "feat1";
            racialFeats[1].Name = "feat1";
            racialFeats[2].Name = "feat2";
            racialFeats[2].Foci = new[] { "focus 1", "focus 2" };
            racialFeats[2].Strength = 2;
            racialFeats[3].Name = "feat2";
            racialFeats[3].Foci = new[] { "focus 2", "focus 3" };
            racialFeats[3].Strength = 8;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TakenMultipleTimes)).Returns(new[] { "feat1", "feat2" });

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var featsList = feats.ToList();

            Assert.That(featsList.Count, Is.EqualTo(4));
            Assert.That(featsList[0].Name, Is.EqualTo("feat1"));
            Assert.That(featsList[1].Name, Is.EqualTo("feat1"));
            Assert.That(featsList[2].Name, Is.EqualTo("feat2"));
            Assert.That(featsList[2].Strength, Is.EqualTo(2));
            Assert.That(featsList[2].Foci, Contains.Item("focus 1"));
            Assert.That(featsList[2].Foci, Contains.Item("focus 2"));
            Assert.That(featsList[2].Foci.Count(), Is.EqualTo(2));
            Assert.That(featsList[3].Name, Is.EqualTo("feat2"));
            Assert.That(featsList[3].Strength, Is.EqualTo(8));
            Assert.That(featsList[3].Foci, Contains.Item("focus 2"));
            Assert.That(featsList[3].Foci, Contains.Item("focus 3"));
            Assert.That(featsList[3].Foci.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfFeatHasFocusOfAll_RemoveOtherInstancesOfTheFeat()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Name = "feat1";
            racialFeats[0].Foci = new[] { "focus" };
            racialFeats[1].Name = "feat1";
            racialFeats[1].Foci = new[] { FeatConstants.Foci.All };
            racialFeats[2].Name = "feat2";
            racialFeats[3].Name = "feat3";
            racialFeats[3].Foci = new[] { "focus" };

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var feat1 = feats.First(f => f.Name == "feat1");
            var feat2 = feats.First(f => f.Name == "feat2");
            var feat3 = feats.First(f => f.Name == "feat3");

            Assert.That(feat1.Name, Is.EqualTo("feat1"));
            Assert.That(feat1.Foci.Single(), Is.EqualTo(FeatConstants.Foci.All));
            Assert.That(feat2.Name, Is.EqualTo("feat2"));
            Assert.That(feat2.Foci, Is.Empty);
            Assert.That(feat3.Name, Is.EqualTo("feat3"));
            Assert.That(feat3.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(feats.Count(), Is.EqualTo(3));
        }

        [Test]
        public void IfFeatWithFocusOfAllIsDuplicated_KeepOne()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Name = "feat1";
            racialFeats[0].Foci = new[] { FeatConstants.Foci.All };
            racialFeats[1].Name = "feat1";
            racialFeats[1].Foci = new[] { FeatConstants.Foci.All };
            racialFeats[2].Name = "feat2";
            racialFeats[3].Name = "feat3";
            racialFeats[3].Foci = new[] { FeatConstants.Foci.All };

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var feat1 = feats.First(f => f.Name == "feat1");
            var feat2 = feats.First(f => f.Name == "feat2");
            var feat3 = feats.First(f => f.Name == "feat3");

            Assert.That(feat1.Name, Is.EqualTo("feat1"));
            Assert.That(feat1.Foci.Single(), Is.EqualTo(FeatConstants.Foci.All));
            Assert.That(feat2.Name, Is.EqualTo("feat2"));
            Assert.That(feat2.Foci, Is.Empty);
            Assert.That(feat3.Name, Is.EqualTo("feat3"));
            Assert.That(feat3.Foci.Single(), Is.EqualTo(FeatConstants.Foci.All));
            Assert.That(feats.Count(), Is.EqualTo(3));
        }

        [Test]
        public void IfMultipleFeatWithFocusOfAllAreDuplicated_KeepOneOfEach()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Name = "feat1";
            racialFeats[0].Foci = new[] { FeatConstants.Foci.All };
            racialFeats[1].Name = "feat1";
            racialFeats[1].Foci = new[] { FeatConstants.Foci.All };
            racialFeats[2].Name = "feat2";
            racialFeats[2].Foci = new[] { FeatConstants.Foci.All };
            racialFeats[3].Name = "feat2";
            racialFeats[3].Foci = new[] { FeatConstants.Foci.All };

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var feat1 = feats.First(f => f.Name == "feat1");
            var feat2 = feats.First(f => f.Name == "feat2");

            Assert.That(feat1.Name, Is.EqualTo("feat1"));
            Assert.That(feat1.Foci.Single(), Is.EqualTo(FeatConstants.Foci.All));
            Assert.That(feat2.Name, Is.EqualTo("feat2"));
            Assert.That(feat2.Foci.Single(), Is.EqualTo(FeatConstants.Foci.All));
            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNameAndStrengthMatchAndNoFrequency_CombineFoci()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Name = "feat1";
            racialFeats[0].Foci = new[] { "focus 1", "focus 2" };
            racialFeats[0].Strength = 9266;
            racialFeats[1].Name = "feat1";
            racialFeats[1].Foci = new[] { "focus 3", "focus 4" };
            racialFeats[1].Strength = 9266;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo("feat1"));
            Assert.That(onlyFeat.Foci, Contains.Item("focus 1"));
            Assert.That(onlyFeat.Foci, Contains.Item("focus 2"));
            Assert.That(onlyFeat.Foci, Contains.Item("focus 3"));
            Assert.That(onlyFeat.Foci, Contains.Item("focus 4"));
            Assert.That(onlyFeat.Foci.Count(), Is.EqualTo(4));
            Assert.That(onlyFeat.Strength, Is.EqualTo(9266));
        }

        [Test]
        public void DoNotCombineFociIfFociIntersect()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Name = "feat1";
            racialFeats[0].Foci = new[] { "focus 1", "focus 2" };
            racialFeats[0].Strength = 9266;
            racialFeats[1].Name = "feat1";
            racialFeats[1].Foci = new[] { "focus 3", "focus 1" };
            racialFeats[1].Strength = 9266;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first, Is.Not.EqualTo(last));

            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(first.Foci, Contains.Item("focus 2"));
            Assert.That(first.Foci, Contains.Item("focus 1"));
            Assert.That(first.Foci.Count(), Is.EqualTo(2));
            Assert.That(first.Strength, Is.EqualTo(9266));

            Assert.That(last.Name, Is.EqualTo("feat1"));
            Assert.That(last.Foci, Contains.Item("focus 1"));
            Assert.That(last.Foci, Contains.Item("focus 3"));
            Assert.That(last.Foci.Count(), Is.EqualTo(2));
            Assert.That(last.Strength, Is.EqualTo(9266));

            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNamesDoNotMatch_DoNotCombineFoci()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Name = "feat1";
            racialFeats[0].Foci = new[] { "focus 1", "focus 2" };
            racialFeats[0].Strength = 9266;
            racialFeats[1].Name = "feat2";
            racialFeats[1].Foci = new[] { "focus 3", "focus 4" };
            racialFeats[1].Strength = 9266;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo("feat1"));
            Assert.That(firstFeat.Foci, Contains.Item("focus 1"));
            Assert.That(firstFeat.Foci, Contains.Item("focus 2"));
            Assert.That(firstFeat.Foci.Count(), Is.EqualTo(2));
            Assert.That(firstFeat.Strength, Is.EqualTo(9266));

            Assert.That(lastFeat.Name, Is.EqualTo("feat2"));
            Assert.That(lastFeat.Foci, Contains.Item("focus 3"));
            Assert.That(lastFeat.Foci, Contains.Item("focus 4"));
            Assert.That(lastFeat.Foci.Count(), Is.EqualTo(2));
            Assert.That(lastFeat.Strength, Is.EqualTo(9266));
        }

        [Test]
        public void IfStrengthsDoNotMatch_DoNotCombineFoci()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Name = "feat1";
            racialFeats[0].Foci = new[] { "focus 1", "focus 2" };
            racialFeats[0].Strength = 9266;
            racialFeats[1].Name = "feat1";
            racialFeats[1].Foci = new[] { "focus 3", "focus 4" };
            racialFeats[1].Strength = 90210;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo("feat1"));
            Assert.That(firstFeat.Foci, Contains.Item("focus 1"));
            Assert.That(firstFeat.Foci, Contains.Item("focus 2"));
            Assert.That(firstFeat.Foci.Count(), Is.EqualTo(2));
            Assert.That(firstFeat.Strength, Is.EqualTo(9266));

            Assert.That(lastFeat.Name, Is.EqualTo("feat1"));
            Assert.That(lastFeat.Foci, Contains.Item("focus 3"));
            Assert.That(lastFeat.Foci, Contains.Item("focus 4"));
            Assert.That(lastFeat.Foci.Count(), Is.EqualTo(2));
            Assert.That(lastFeat.Strength, Is.EqualTo(90210));
        }

        [Test]
        public void IfFeatHasFrequency_DoNotCombineFoci()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Name = "feat1";
            racialFeats[0].Foci = new[] { "focus 1", "focus 2" };
            racialFeats[0].Strength = 9266;
            racialFeats[1].Name = "feat1";
            racialFeats[1].Foci = new[] { "focus 3", "focus 4" };
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.TimePeriod = "time period";
            racialFeats[1].Frequency.Quantity = 42;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo("feat1"));
            Assert.That(firstFeat.Foci, Contains.Item("focus 1"));
            Assert.That(firstFeat.Foci, Contains.Item("focus 2"));
            Assert.That(firstFeat.Foci.Count(), Is.EqualTo(2));
            Assert.That(firstFeat.Strength, Is.EqualTo(9266));
            Assert.That(firstFeat.Frequency.Quantity, Is.EqualTo(0));
            Assert.That(firstFeat.Frequency.TimePeriod, Is.Empty);

            Assert.That(lastFeat.Name, Is.EqualTo("feat1"));
            Assert.That(lastFeat.Foci, Contains.Item("focus 3"));
            Assert.That(lastFeat.Foci, Contains.Item("focus 4"));
            Assert.That(lastFeat.Foci.Count(), Is.EqualTo(2));
            Assert.That(lastFeat.Strength, Is.EqualTo(9266));
            Assert.That(lastFeat.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(lastFeat.Frequency.TimePeriod, Is.EqualTo("time period"));
        }

        [Test]
        public void IfBothFeatHaveMatchingFrequencies_DoNotCombineFoci()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Name = "feat1";
            racialFeats[0].Foci = new[] { "focus 1", "focus 2" };
            racialFeats[0].Strength = 9266;
            racialFeats[0].Frequency.TimePeriod = "time period";
            racialFeats[0].Frequency.Quantity = 42;
            racialFeats[1].Name = "feat1";
            racialFeats[1].Foci = new[] { "focus 3", "focus 4" };
            racialFeats[1].Strength = 9266;
            racialFeats[1].Frequency.TimePeriod = "time period";
            racialFeats[1].Frequency.Quantity = 42;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(firstFeat.Name, Is.EqualTo("feat1"));
            Assert.That(firstFeat.Foci, Contains.Item("focus 1"));
            Assert.That(firstFeat.Foci, Contains.Item("focus 2"));
            Assert.That(firstFeat.Foci.Count(), Is.EqualTo(2));
            Assert.That(firstFeat.Strength, Is.EqualTo(9266));
            Assert.That(firstFeat.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(firstFeat.Frequency.TimePeriod, Is.EqualTo("time period"));

            Assert.That(lastFeat.Name, Is.EqualTo("feat1"));
            Assert.That(lastFeat.Foci, Contains.Item("focus 3"));
            Assert.That(lastFeat.Foci, Contains.Item("focus 4"));
            Assert.That(lastFeat.Foci.Count(), Is.EqualTo(2));
            Assert.That(lastFeat.Strength, Is.EqualTo(9266));
            Assert.That(lastFeat.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(lastFeat.Frequency.TimePeriod, Is.EqualTo("time period"));
        }

        [Test]
        public void DoNotCombineFociIfNoFoci()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Name = "feat1";
            racialFeats[0].Strength = 9266;
            racialFeats[1].Name = "feat1";
            racialFeats[1].Foci = new[] { "focus 1" };
            racialFeats[1].Strength = 9266;
            racialFeats[2].Name = "feat2";
            racialFeats[2].Strength = 9266;
            racialFeats[2].Foci = new[] { "focus 2" };
            racialFeats[3].Name = "feat2";
            racialFeats[3].Strength = 9266;

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.ElementAt(0);
            var secondFeat = feats.ElementAt(1);
            var thirdFeat = feats.ElementAt(2);
            var fourthFeat = feats.ElementAt(3);

            Assert.That(firstFeat.Name, Is.EqualTo("feat1"));
            Assert.That(firstFeat.Foci, Is.Empty);
            Assert.That(firstFeat.Strength, Is.EqualTo(9266));

            Assert.That(secondFeat.Name, Is.EqualTo("feat1"));
            Assert.That(secondFeat.Foci.Single(), Is.EqualTo("focus 1"));
            Assert.That(secondFeat.Strength, Is.EqualTo(9266));

            Assert.That(thirdFeat.Name, Is.EqualTo("feat2"));
            Assert.That(thirdFeat.Foci.Single(), Is.EqualTo("focus 2"));
            Assert.That(thirdFeat.Strength, Is.EqualTo(9266));

            Assert.That(fourthFeat.Name, Is.EqualTo("feat2"));
            Assert.That(fourthFeat.Foci, Is.Empty);
            Assert.That(fourthFeat.Strength, Is.EqualTo(9266));
        }

        [Test]
        public void CanOnlyBeFocusedInAll()
        {
            racialFeats.Add(new Feat());

            racialFeats[0].Name = "feat1";
            racialFeats[0].Foci = new[] { FeatConstants.Foci.All, "focus 2" };

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var onlyFeat = feats.Single();
            Assert.That(onlyFeat.Foci.Single(), Is.EqualTo(FeatConstants.Foci.All));
        }

        [Test]
        public void CombineFociOfMultipleFeatOfDifferentStrengths()
        {
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());
            racialFeats.Add(new Feat());

            racialFeats[0].Name = "feat1";
            racialFeats[0].Foci = new[] { "focus 1" };
            racialFeats[0].Strength = 2;
            racialFeats[1].Name = "feat1";
            racialFeats[1].Foci = new[] { "focus 2" };
            racialFeats[1].Strength = 2;
            racialFeats[2].Name = "feat1";
            racialFeats[2].Foci = new[] { "focus 1" };
            racialFeats[2].Strength = 8;
            racialFeats[3].Name = "feat1";
            racialFeats[3].Foci = new[] { "focus 2" };
            racialFeats[3].Strength = 8;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TakenMultipleTimes)).Returns(new[] { "feat1" });

            var feats = featsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            var firstFeat = feats.First();
            var lastFeat = feats.Last();

            Assert.That(feats.Count(), Is.EqualTo(2));

            Assert.That(firstFeat.Strength, Is.EqualTo(2));
            Assert.That(firstFeat.Foci, Contains.Item("focus 1"));
            Assert.That(firstFeat.Foci, Contains.Item("focus 2"));
            Assert.That(firstFeat.Foci.Count(), Is.EqualTo(2));

            Assert.That(lastFeat.Strength, Is.EqualTo(8));
            Assert.That(lastFeat.Foci, Contains.Item("focus 1"));
            Assert.That(lastFeat.Foci, Contains.Item("focus 2"));
            Assert.That(lastFeat.Foci.Count(), Is.EqualTo(2));
        }
    }
}
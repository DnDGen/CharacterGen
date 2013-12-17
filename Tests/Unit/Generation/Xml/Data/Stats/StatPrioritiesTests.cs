using System;
using System.Collections.Generic;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Xml.Parsers;
using NPCGen.Core.Generation.Xml.Parsers.Objects;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Stats
{
    [TestFixture]
    public class StatPrioritiesTests
    {
        private Dictionary<String, StatPriorityObject> statPriorities;

        [SetUp]
        public void Setup()
        {
            var streamLoader = new EmbeddedResourceStreamLoader();
            var parser = new StatPriorityXmlParser(streamLoader);
            statPriorities = parser.Parse("StatPriorities.xml");
        }

        [Test]
        public void BarbarianFirstPriorityIsStrength()
        {
            var priorities = statPriorities[CharacterClassConstants.Barbarian];
            Assert.That(priorities.FirstPriority, Is.EqualTo(StatConstants.Strength));
        }

        [Test]
        public void BarbarianSecondPriorityIsDexterity()
        {
            var priorities = statPriorities[CharacterClassConstants.Barbarian];
            Assert.That(priorities.SecondPriority, Is.EqualTo(StatConstants.Dexterity));
        }

        [Test]
        public void BardFirstPriorityIsCharisma()
        {
            var priorities = statPriorities[CharacterClassConstants.Bard];
            Assert.That(priorities.FirstPriority, Is.EqualTo(StatConstants.Charisma));
        }

        [Test]
        public void BardSecondPriorityIsIntelligence()
        {
            var priorities = statPriorities[CharacterClassConstants.Bard];
            Assert.That(priorities.SecondPriority, Is.EqualTo(StatConstants.Intelligence));
        }

        [Test]
        public void ClericFirstPriorityIsWisdom()
        {
            var priorities = statPriorities[CharacterClassConstants.Cleric];
            Assert.That(priorities.FirstPriority, Is.EqualTo(StatConstants.Wisdom));
        }

        [Test]
        public void ClericSecondPriorityIsConstitution()
        {
            var priorities = statPriorities[CharacterClassConstants.Cleric];
            Assert.That(priorities.SecondPriority, Is.EqualTo(StatConstants.Constitution));
        }

        [Test]
        public void DruidFirstPriorityIsWisdom()
        {
            var priorities = statPriorities[CharacterClassConstants.Druid];
            Assert.That(priorities.FirstPriority, Is.EqualTo(StatConstants.Wisdom));
        }

        [Test]
        public void DruidSecondPriorityIsDexterity()
        {
            var priorities = statPriorities[CharacterClassConstants.Druid];
            Assert.That(priorities.SecondPriority, Is.EqualTo(StatConstants.Dexterity));
        }

        [Test]
        public void FighterFirstPriorityIsStrength()
        {
            var priorities = statPriorities[CharacterClassConstants.Fighter];
            Assert.That(priorities.FirstPriority, Is.EqualTo(StatConstants.Strength));
        }

        [Test]
        public void FighterSecondPriorityIsConstitution()
        {
            var priorities = statPriorities[CharacterClassConstants.Fighter];
            Assert.That(priorities.SecondPriority, Is.EqualTo(StatConstants.Constitution));
        }

        [Test]
        public void MonkFirstPriorityIsWisdom()
        {
            var priorities = statPriorities[CharacterClassConstants.Monk];
            Assert.That(priorities.FirstPriority, Is.EqualTo(StatConstants.Wisdom));
        }

        [Test]
        public void MonkSecondPriorityIsStrength()
        {
            var priorities = statPriorities[CharacterClassConstants.Monk];
            Assert.That(priorities.SecondPriority, Is.EqualTo(StatConstants.Strength));
        }

        [Test]
        public void PaladinFirstPriorityIsCharisma()
        {
            var priorities = statPriorities[CharacterClassConstants.Paladin];
            Assert.That(priorities.FirstPriority, Is.EqualTo(StatConstants.Charisma));
        }

        [Test]
        public void PaladinSecondPriorityIsStrength()
        {
            var priorities = statPriorities[CharacterClassConstants.Paladin];
            Assert.That(priorities.SecondPriority, Is.EqualTo(StatConstants.Strength));
        }

        [Test]
        public void RangerFirstPriorityIsDexterity()
        {
            var priorities = statPriorities[CharacterClassConstants.Ranger];
            Assert.That(priorities.FirstPriority, Is.EqualTo(StatConstants.Dexterity));
        }

        [Test]
        public void RangerSecondPriorityIsStrength()
        {
            var priorities = statPriorities[CharacterClassConstants.Ranger];
            Assert.That(priorities.SecondPriority, Is.EqualTo(StatConstants.Strength));
        }

        [Test]
        public void RogueFirstPriorityIsDexterity()
        {
            var priorities = statPriorities[CharacterClassConstants.Rogue];
            Assert.That(priorities.FirstPriority, Is.EqualTo(StatConstants.Dexterity));
        }

        [Test]
        public void RogueSecondPriorityIsIntelligence()
        {
            var priorities = statPriorities[CharacterClassConstants.Rogue];
            Assert.That(priorities.SecondPriority, Is.EqualTo(StatConstants.Intelligence));
        }

        [Test]
        public void SorcererFirstPriorityIsCharisma()
        {
            var priorities = statPriorities[CharacterClassConstants.Sorcerer];
            Assert.That(priorities.FirstPriority, Is.EqualTo(StatConstants.Charisma));
        }

        [Test]
        public void SorcererSecondPriorityIsDexterity()
        {
            var priorities = statPriorities[CharacterClassConstants.Sorcerer];
            Assert.That(priorities.SecondPriority, Is.EqualTo(StatConstants.Dexterity));
        }

        [Test]
        public void WizardFirstPriorityIsIntelligence()
        {
            var priorities = statPriorities[CharacterClassConstants.Wizard];
            Assert.That(priorities.FirstPriority, Is.EqualTo(StatConstants.Intelligence));
        }

        [Test]
        public void WizardSecondPriorityIsDexterity()
        {
            var priorities = statPriorities[CharacterClassConstants.Wizard];
            Assert.That(priorities.SecondPriority, Is.EqualTo(StatConstants.Dexterity));
        }

        [Test]
        public void PrioritiesContainsAllClasses()
        {
            foreach (var className in CharacterClassConstants.GetClassNames())
                Assert.That(statPriorities.ContainsKey(className), Is.True);
        }
    }
}
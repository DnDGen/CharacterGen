using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Stats;
using NPCGen.Mappers.Interfaces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Stats
{
    [TestFixture]
    public class StatPrioritiesTests : IntegrationTest
    {
        [Inject]
        public IStatPriorityXmlParser StatPriorityXmlParser { get; set; }

        private Dictionary<String, StatPriority> statPriorities;

        [SetUp]
        public void Setup()
        {
            statPriorities = StatPriorityXmlParser.Parse("StatPriorities.xml");
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
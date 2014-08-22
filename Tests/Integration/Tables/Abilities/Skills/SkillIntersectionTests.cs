using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NPCGen.Common.CharacterClasses;
using NPCGen.Mappers.Interfaces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills
{
    [TestFixture]
    public class SkillIntersectionTests : IntegrationTests
    {
        [Inject]
        public ICollectionsMapper Mapper { get; set; }

        private Dictionary<String, IEnumerable<String>> classSkills;
        private Dictionary<String, IEnumerable<String>> crossClassSkills;

        [SetUp]
        public void Setup()
        {
            classSkills = Mapper.Map("ClassSkills");
            crossClassSkills = Mapper.Map("CrossClassSkills");
        }

        [TestCase(CharacterClassConstants.Barbarian)]
        [TestCase(CharacterClassConstants.Bard)]
        [TestCase(CharacterClassConstants.Cleric)]
        [TestCase(CharacterClassConstants.Druid)]
        [TestCase(CharacterClassConstants.Fighter)]
        [TestCase(CharacterClassConstants.Monk)]
        [TestCase(CharacterClassConstants.Paladin)]
        [TestCase(CharacterClassConstants.Ranger)]
        [TestCase(CharacterClassConstants.Rogue)]
        [TestCase(CharacterClassConstants.Sorcerer)]
        [TestCase(CharacterClassConstants.Wizard)]
        public void NoIntersectionBetweenClassAndCrossClassSkills(String className)
        {
            var intersect = classSkills[className].Intersect(crossClassSkills[className]);
            Assert.That(intersect, Is.Empty);
        }
    }
}
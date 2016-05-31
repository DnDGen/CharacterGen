using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Alignments
{
    [TestFixture]
    public class AlignmentGroupsTests : CollectionTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Collection.AlignmentGroups;
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                "Lawful Good",
                "Neutral Good",
                "Chaotic Good",
                "Lawful Neutral",
                "True Neutral",
                "Chaotic Neutral",
                "Lawful Evil",
                "Neutral Evil",
                "Chaotic Evil"
            };
        }

        [TestCase("Lawful Good",
            "Lawful Good",
            "Neutral Good",
            "Lawful Neutral",
            "True Neutral")]
        [TestCase("Neutral Good",
            "Lawful Good",
            "Neutral Good",
            "Chaotic Good",
            "Lawful Neutral",
            "True Neutral",
            "Chaotic Neutral")]
        [TestCase("Chaotic Good",
            "Neutral Good",
            "Chaotic Good",
            "Chaotic Neutral",
            "True Neutral")]
        [TestCase("Lawful Neutral",
            "Neutral Good",
            "Lawful Good",
            "Lawful Neutral",
            "True Neutral",
            "Neutral Evil",
            "Lawful Evil")]
        [TestCase("True Neutral",
            "Lawful Good",
            "Neutral Good",
            "Chaotic Good",
            "Lawful Neutral",
            "True Neutral",
            "Chaotic Neutral",
            "Lawful Evil",
            "Neutral Evil",
            "Chaotic Evil")]
        [TestCase("Chaotic Neutral",
            "Neutral Good",
            "Chaotic Good",
            "Chaotic Neutral",
            "True Neutral",
            "Neutral Evil",
            "Chaotic Evil")]
        [TestCase("Lawful Evil",
            "Lawful Evil",
            "Neutral Evil",
            "Lawful Neutral",
            "True Neutral")]
        [TestCase("Neutral Evil",
            "Lawful Evil",
            "Neutral Evil",
            "Chaotic Evil",
            "Lawful Neutral",
            "True Neutral",
            "Chaotic Neutral")]
        [TestCase("Chaotic Evil",
            "Neutral Evil",
            "Chaotic Evil",
            "Chaotic Neutral",
            "True Neutral")]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}

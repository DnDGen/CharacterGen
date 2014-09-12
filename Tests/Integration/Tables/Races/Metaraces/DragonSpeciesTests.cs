using System;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces
{
    [TestFixture]
    public class DragonSpeciesTests : CollectionTests
    {
        protected override String tableName
        {
            get { return "DragonSpecies"; }
        }

        [TestCase("Lawful Good",
            "Bronze",
            "Gold",
            "Silver")]
        [TestCase("Neutral Good",
            "Bronze",
            "Gold",
            "Silver",
            "Brass",
            "Copper")]
        [TestCase("Chaotic Good",
            "Brass",
            "Copper")]
        [TestCase("Lawful Evil",
            "Blue",
            "Green")]
        [TestCase("Neutral Evil",
            "Blue",
            "Green",
            "Black",
            "Red",
            "White")]
        [TestCase("Chaotic Evil",
            "Black",
            "Red",
            "White")]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}
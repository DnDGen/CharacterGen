using System;
using CharacterGen.Races;
using CharacterGen.Domain.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.Metaraces
{
    [TestFixture]
    public class DragonSpeciesTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.DragonSpecies; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] 
            {
                "Lawful Good",
                "Neutral Good",
                "Chaotic Good",
                "Lawful Evil",
                "Neutral Evil",
                "Chaotic Evil"
            };

            AssertCollectionNames(names);
        }

        [TestCase("Lawful Good",
            RaceConstants.Metaraces.Species.Bronze,
            RaceConstants.Metaraces.Species.Gold,
            RaceConstants.Metaraces.Species.Silver)]
        [TestCase("Neutral Good",
            RaceConstants.Metaraces.Species.Bronze,
            RaceConstants.Metaraces.Species.Gold,
            RaceConstants.Metaraces.Species.Silver,
            RaceConstants.Metaraces.Species.Brass,
            RaceConstants.Metaraces.Species.Copper)]
        [TestCase("Chaotic Good",
            RaceConstants.Metaraces.Species.Brass,
            RaceConstants.Metaraces.Species.Copper)]
        [TestCase("Lawful Evil",
            RaceConstants.Metaraces.Species.Blue,
            RaceConstants.Metaraces.Species.Green)]
        [TestCase("Neutral Evil",
            RaceConstants.Metaraces.Species.Blue,
            RaceConstants.Metaraces.Species.Green,
            RaceConstants.Metaraces.Species.Black,
            RaceConstants.Metaraces.Species.Red,
            RaceConstants.Metaraces.Species.White)]
        [TestCase("Chaotic Evil",
            RaceConstants.Metaraces.Species.Black,
            RaceConstants.Metaraces.Species.Red,
            RaceConstants.Metaraces.Species.White)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}
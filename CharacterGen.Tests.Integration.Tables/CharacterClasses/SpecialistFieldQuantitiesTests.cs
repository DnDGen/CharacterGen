﻿using System;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class SpecialistFieldQuantitiesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.SpecialistFieldQuantities; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] 
            {
                CharacterClassConstants.Barbarian,
                CharacterClassConstants.Bard,
                CharacterClassConstants.Cleric,
                CharacterClassConstants.Druid,
                CharacterClassConstants.Fighter, 
                CharacterClassConstants.Monk,
                CharacterClassConstants.Paladin, 
                CharacterClassConstants.Ranger, 
                CharacterClassConstants.Rogue, 
                CharacterClassConstants.Sorcerer, 
                CharacterClassConstants.Wizard
            };

            AssertCollectionNames(names);
        }

        [TestCase(CharacterClassConstants.Barbarian, 0)]
        [TestCase(CharacterClassConstants.Bard, 0)]
        [TestCase(CharacterClassConstants.Cleric, 2)]
        [TestCase(CharacterClassConstants.Druid, 0)]
        [TestCase(CharacterClassConstants.Fighter, 0)]
        [TestCase(CharacterClassConstants.Monk, 0)]
        [TestCase(CharacterClassConstants.Paladin, 0)]
        [TestCase(CharacterClassConstants.Ranger, 0)]
        [TestCase(CharacterClassConstants.Rogue, 0)]
        [TestCase(CharacterClassConstants.Sorcerer, 0)]
        [TestCase(CharacterClassConstants.Wizard, 1)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
using System;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Tables
{
    [TestFixture]
    public class TableNameConstantsTests
    {
        [TestCase(TableNameConstants.Set.Collection.ArmorCheckPenalties, "ArmorCheckPenalties")]
        [TestCase(TableNameConstants.Set.Collection.LevelAdjustments, "LevelAdjustments")]
        [TestCase(TableNameConstants.Set.Percentile.AlignmentGoodness, "AlignmentGoodness")]
        [TestCase(TableNameConstants.Set.Percentile.Traits, "Traits")]
        [TestCase(TableNameConstants.Formattable.Percentile.GOODNESSCharacterClasses, "{0}CharacterClasses")]
        [TestCase(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, "{0}{1}BaseRaces")]
        [TestCase(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, "{0}{1}Metaraces")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class AdjustmentsTests : CollectionTests
    {
        protected const String TrueString = "True";
        protected const String FalseString = "False";

        public virtual void Adjustment(String name, Int32 adjustment)
        {
            Assert.That(table.Keys, Contains.Item(name), tableName);

            var actualAdjustment = Convert.ToInt32(table[name].Single());
            Assert.That(actualAdjustment, Is.EqualTo(adjustment));
        }
    }
}
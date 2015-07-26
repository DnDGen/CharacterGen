using System;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class AdjustmentsTests : CollectionTests
    {
        public virtual void Adjustment(String name, Int32 adjustment)
        {
            var collection = new[] { Convert.ToString(adjustment) };
            Collection(name, collection);
        }

        public virtual void NoAdjustments()
        {
            EmptyCollection();
        }
    }
}
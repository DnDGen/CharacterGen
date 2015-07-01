using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class DataTests : CollectionTests
    {
        protected void Data(String name, IEnumerable<String> data)
        {
            OrderedCollection(name, data.ToArray());
        }
    }
}
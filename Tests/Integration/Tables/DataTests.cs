using System;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class DataTests : CollectionTests
    {
        protected void Data(String name, params String[] data)
        {
            OrderedCollection(name, data);
        }
    }
}
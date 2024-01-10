using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class DataTests : CollectionTests
    {
        protected void Data(string name, IEnumerable<string> data)
        {
            OrderedCollection(name, data.ToArray());
        }
    }
}
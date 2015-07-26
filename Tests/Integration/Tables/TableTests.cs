using System;
using CharacterGen.Tests.Integration.Common;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables
{
    [TestFixture]
    [Table]
    public abstract class TableTests : IntegrationTests
    {
        protected abstract String tableName { get; }
    }
}
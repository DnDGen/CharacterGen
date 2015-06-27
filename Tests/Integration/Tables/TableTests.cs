using System;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables
{
    [TestFixture]
    [Table]
    public abstract class TableTests : IntegrationTests
    {
        protected abstract String tableName { get; }
    }
}
using System;
using NPCGen.Tests.Integration.Common;

namespace NPCGen.Tests.Integration.Tables
{
    public abstract class TableTests : IntegrationTests
    {
        protected abstract String tableName { get; }
    }
}
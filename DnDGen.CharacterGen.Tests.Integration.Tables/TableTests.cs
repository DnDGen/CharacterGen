using DnDGen.Infrastructure.IoC;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables
{
    [TestFixture]
    [Table]
    public abstract class TableTests : IntegrationTests
    {
        protected abstract string tableName { get; }

        [OneTimeSetUp]
        public void TableOneTimeSetup()
        {
            var coreLoader = new InfrastructureModuleLoader();
            coreLoader.ReplaceAssemblyLoaderWith<CharacterGenAssemblyLoader>(kernel);
        }
    }
}
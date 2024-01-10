using DnDGen.CharacterGen.Tables;
using DnDGen.Infrastructure.Tables;
using System.Reflection;

namespace DnDGen.CharacterGen.Tests.Integration.Tables
{
    public class CharacterGenAssemblyLoader : AssemblyLoader
    {
        public Assembly GetRunningAssembly()
        {
            var type = typeof(TableNameConstants);
            return type.Assembly;
        }
    }
}

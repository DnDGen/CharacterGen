using NPCGen.Tables.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NPCGen.Tables
{
    public class EmbeddedResourceStreamLoader : IStreamLoader
    {
        public Stream LoadStream(String filename)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var resources = executingAssembly.GetManifestResourceNames();

            if (!resources.Any(r => r.Contains(filename)))
                throw new FileNotFoundException(filename);

            var streamSource = resources.First(r => r.Contains(filename));

            return executingAssembly.GetManifestResourceStream(streamSource);
        }
    }
}
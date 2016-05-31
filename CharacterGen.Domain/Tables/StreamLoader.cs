using System.IO;

namespace CharacterGen.Domain.Tables
{
    internal interface StreamLoader
    {
        Stream LoadFor(string filename);
    }
}
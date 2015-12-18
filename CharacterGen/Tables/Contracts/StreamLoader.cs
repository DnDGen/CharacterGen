using System.IO;

namespace CharacterGen.Tables
{
    public interface StreamLoader
    {
        Stream LoadFor(string filename);
    }
}
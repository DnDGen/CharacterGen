using System;
using System.IO;

namespace CharacterGen.Tables
{
    public interface IStreamLoader
    {
        Stream LoadFor(String filename);
    }
}
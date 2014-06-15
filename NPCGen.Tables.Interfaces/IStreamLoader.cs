using System;
using System.IO;

namespace NPCGen.Tables.Interfaces
{
    public interface IStreamLoader
    {
        Stream LoadFor(String filename);
    }
}
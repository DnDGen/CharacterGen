using System;
using System.IO;

namespace NPCGen.Tables.Interfaces
{
    public interface IStreamLoader
    {
        Stream LoadStream(String filename);
    }
}
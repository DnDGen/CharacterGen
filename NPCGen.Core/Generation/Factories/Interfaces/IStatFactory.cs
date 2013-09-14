using NPCGen.Core.Data.Stats;
using System;
using System.Collections.Generic;

namespace NPCGen.Core.Generation.Factories.Interfaces
{
    public interface IStatFactory
    {
        Dictionary<String, Stat> Generate();
    }
}
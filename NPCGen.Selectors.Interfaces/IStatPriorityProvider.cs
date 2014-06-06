using System;
using NPCGen.Core.Generation.Xml.Parsers.Objects;

namespace NPCGen.Core.Generation.Providers.Interfaces
{
    public interface IStatPriorityProvider
    {
        StatPriorityObject GetStatPriorities(String className);
    }
}
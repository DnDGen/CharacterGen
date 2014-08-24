using System.Collections.Generic;

namespace NPCGen.Selectors.Interfaces
{
    public interface IFeatsSelector
    {
        IEnumerable<FeatSelection> SelectAll();
    }
}
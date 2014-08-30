﻿using System.Collections.Generic;
using NPCGen.Selectors.Interfaces.Objects;

namespace NPCGen.Selectors.Interfaces
{
    public interface IFeatsSelector
    {
        IEnumerable<FeatSelection> SelectAll();
    }
}
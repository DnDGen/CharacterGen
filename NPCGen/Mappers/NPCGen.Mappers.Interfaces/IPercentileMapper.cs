﻿using System;
using System.Collections.Generic;

namespace NPCGen.Mappers.Interfaces
{
    public interface IPercentileMapper
    {
        Dictionary<Int32, String> Map(String tableName);
    }
}
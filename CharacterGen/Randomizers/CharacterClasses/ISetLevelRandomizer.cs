﻿namespace CharacterGen.Randomizers.CharacterClasses
{
    public interface ISetLevelRandomizer : ILevelRandomizer
    {
        int SetLevel { get; set; }
    }
}
﻿namespace CharacterGen.Races
{
    public class Age
    {
        public int Years { get; set; }
        public string Stage { get; set; }
        public int Maximum { get; set; }

        public Age()
        {
            Stage = string.Empty;
        }
    }
}
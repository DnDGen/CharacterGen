using System;

namespace NPCGen.Common.Races
{
    public class NameModel
    {
        public String Name { get; set; }
        public String Id { get; set; }

        public NameModel()
        {
            Name = String.Empty;
            Id = String.Empty;
        }
    }
}
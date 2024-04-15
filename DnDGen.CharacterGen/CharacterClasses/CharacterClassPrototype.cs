namespace DnDGen.CharacterGen.CharacterClasses
{
    public class CharacterClassPrototype
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public bool IsNPC { get; set; }

        public string Summary
        {
            get
            {
                return $"Level {Level} {Name}";
            }
        }

        public CharacterClassPrototype()
        {
            Name = string.Empty;
        }

        public override string ToString()
        {
            return Summary;
        }

        public override bool Equals(object toCompare)
        {
            if (!(toCompare is CharacterClassPrototype))
                return false;

            var alignment = toCompare as CharacterClassPrototype;
            return Summary == alignment.Summary;
        }

        public override int GetHashCode()
        {
            return Summary.GetHashCode();
        }
    }
}
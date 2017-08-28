namespace CharacterGen.CharacterClasses
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
    }
}
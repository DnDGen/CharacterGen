namespace CharacterGen.Abilities.Stats
{
    public class Stat
    {
        public readonly string Name;

        public int Value { get; set; }
        public int Bonus
        {
            get
            {
                var even = Value - Value % 2;
                return (even - 10) / 2;
            }
        }

        public Stat(string name)
        {
            Name = name;
            Value = 10;
        }
    }
}
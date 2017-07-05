namespace CharacterGen.Abilities
{
    public class Ability
    {
        public string Name { get; private set; }
        public int Value { get; set; }
        public int Bonus
        {
            get
            {
                var even = Value - Value % 2;
                return (even - 10) / 2;
            }
        }

        public Ability(string name)
        {
            Name = name;
            Value = 10;
        }
    }
}
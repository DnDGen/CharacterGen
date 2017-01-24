namespace CharacterGen.Magics
{
    public class SpellQuantity
    {
        public string Source { get; set; }
        public int Level { get; set; }
        public int Quantity { get; set; }
        public bool HasDomainSpell { get; set; }

        public SpellQuantity()
        {
            Source = string.Empty;
        }
    }
}

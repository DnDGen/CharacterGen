namespace CharacterGen.Domain.Selectors.Selections
{
    internal class SkillSelection
    {
        public string BaseStatName { get; set; }
        public int RandomFociQuantity { get; set; }
        public string SkillName { get; set; }

        public SkillSelection()
        {
            BaseStatName = string.Empty;
            SkillName = string.Empty;
        }
    }
}
namespace CharacterGen.Randomizers.CharacterClasses
{
    public interface ISetClassNameRandomizer : IClassNameRandomizer
    {
        string SetClassName { get; set; }
    }
}
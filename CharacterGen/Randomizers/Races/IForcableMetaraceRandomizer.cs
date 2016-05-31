namespace CharacterGen.Randomizers.Races
{
    public interface IForcableMetaraceRandomizer : RaceRandomizer
    {
        bool ForceMetarace { get; set; }
    }
}
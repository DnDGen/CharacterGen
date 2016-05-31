namespace CharacterGen.Domain.Selectors.Percentiles
{
    internal interface IBooleanPercentileSelector
    {
        bool SelectFrom(string tableName);
    }
}
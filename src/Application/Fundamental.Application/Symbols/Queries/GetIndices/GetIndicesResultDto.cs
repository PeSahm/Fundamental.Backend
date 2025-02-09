namespace Fundamental.Application.Symbols.Queries.GetIndices;

public sealed class GetIndicesResultDto
{
    public string Isin { get; init; }

    public string TseInsCode { get; init; }

    public string Title { get; init; }

    public string Name { get; init; }

    public List<GetIndicesResulItem> Data { get; set; } = new();

    public decimal Average { get; set; }

    public GetIndicesResultDto CalculatePercentage()
    {
        Data = Data.OrderBy(x => x.Date).ToList();
        for (int i = 1; i < Data.Count; i++)
        {
            Data[i].Change = Data[i].Value - Data[i - 1].Value;
            Data[i].ChangePercent = Data[i].Change / Data[i - 1].Value * 100;
        }

        return this;
    }
}
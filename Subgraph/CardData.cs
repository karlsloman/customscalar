namespace Subgraph;

public class CardData
{
    [GraphQLType(typeof(CardNumberType))]
    public string? CardNumber { get; set; }
    public string? Title { get; set;}
}
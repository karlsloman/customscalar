namespace Subgraph;

public class CardDataQuery
{
    public CardData GetCardData(string cardNumber) =>
        new CardData
        {
            CardNumber = cardNumber,
            Title = "Test card"
        };
}

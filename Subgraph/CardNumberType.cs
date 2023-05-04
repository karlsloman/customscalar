namespace Subgraph;


public sealed class CardNumberType : ScalarType<string, StringValueNode>
{
    public CardNumberType() : base(nameof(CardNumberType), BindingBehavior.Implicit)
    {
        Description = "Card number";
    }

    public override IValueNode ParseResult(object? resultValue)
    {
        if (resultValue is string cardNumber)
        {
            return new StringValueNode(null, cardNumber, false);
        }

        throw new Exception("Test");
    }

    protected override string ParseLiteral(StringValueNode valueSyntax)
    {
        if (!Regex.IsMatch(valueSyntax.Value, "[0-9]{16}"))
        {
            throw new SerializationException("Card number is invalid", this);
        }

        return valueSyntax.Value;
    }

    protected override StringValueNode ParseValue(string runtimeValue)
    {
        return new StringValueNode(runtimeValue);
    }

    public override object? Serialize(object? runtimeValue)
    {
        if (runtimeValue is string cardNumber)
        {
            return cardNumber;
        }

        return base.Serialize(runtimeValue);
    }
}

namespace Mecteral.UnitConversion
{
    public interface IConversionExpressionWithValue : IConversionExpression
    {
        decimal Value { get; set; }
    }
}

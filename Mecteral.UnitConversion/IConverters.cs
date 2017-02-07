namespace Mecteral.UnitConversion
{
    public interface IConverters
    {
        IConversionExpressionWithValue Convert(IConversionExpression expression);
    }
}
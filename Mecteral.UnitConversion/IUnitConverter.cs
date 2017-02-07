namespace Mecteral.UnitConversion
{
    public interface IUnitConverter
    {
        IConversionExpressionWithValue Convert(IConversionExpression expression, bool toMetric);
    }
}
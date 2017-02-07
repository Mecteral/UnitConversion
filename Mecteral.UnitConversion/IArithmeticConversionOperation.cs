namespace Mecteral.UnitConversion
{
    public interface IArithmeticConversionOperation : IConversionExpression
    {
        IConversionExpression Left { get; set; }
        IConversionExpression Right { get; set; }
    }
}

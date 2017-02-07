namespace Mecteral.UnitConversion
{
    public class ConversionSubtraction : AnArithmeticConversionOperation
    {
        public override void Accept(IConversionExpressionVisitor visitor) => visitor.Visit(this);
    }
}

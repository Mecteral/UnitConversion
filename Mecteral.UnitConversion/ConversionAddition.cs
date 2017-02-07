namespace Mecteral.UnitConversion
{
    public class ConversionAddition : AnArithmeticConversionOperation
    {
        public override void Accept(IConversionExpressionVisitor visitor) => visitor.Visit(this);
    }
}

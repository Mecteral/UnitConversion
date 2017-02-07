namespace Mecteral.UnitConversion
{
    public class MetricLengthToken : AConversionTokens, IConversionToken
    {
        public MetricLengthToken(string asText, string arg) : base(asText, arg) {}

        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

namespace Mecteral.UnitConversion
{
    public class MetricMassToken : AConversionTokens, IConversionToken
    {
        public MetricMassToken(string asText, string arg) : base(asText, arg) {}
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

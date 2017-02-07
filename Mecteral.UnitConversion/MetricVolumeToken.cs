namespace Mecteral.UnitConversion
{
    public class MetricVolumeToken : AConversionTokens, IConversionToken
    {
        public MetricVolumeToken(string asText, string arg) : base(asText, arg) {}
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

namespace Mecteral.UnitConversion
{
    public class ImperialVolumeToken : AConversionTokens, IConversionToken
    {
        public ImperialVolumeToken(string asText, string arg) : base(asText, arg) {}
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

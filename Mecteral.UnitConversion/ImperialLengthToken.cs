namespace Mecteral.UnitConversion
{
    public class ImperialLengthToken : AConversionTokens, IConversionToken
    {
        public ImperialLengthToken(string asText, string arg) : base(asText, arg) {}

        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

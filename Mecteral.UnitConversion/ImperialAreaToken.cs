namespace Mecteral.UnitConversion
{
    public class ImperialAreaToken : AConversionTokens, IConversionToken
    {
        public ImperialAreaToken(string asText, string arg) : base(asText, arg) {}
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
